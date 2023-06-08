﻿using Blogifier.Core.Extensions;
using Blogifier.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface IStorageProvider
	{
		Task<IList<string>> GetThemes();
		bool FileExists(string path);
		Task<bool> UploadFormFile(IFormFile file, string path = "");
		Task<string> UploadFromWeb(Uri requestUri, string root, string path = "");
		Task<string> UploadBase64Image(string baseImg, string root, string path = "");
		Task<ThemeSettings> GetThemeSettings(string theme);
		Task<bool> SaveThemeSettings(string theme, ThemeSettings settings);
	}

	public class StorageProvider : IStorageProvider
	{
		private string _storageRoot;
		private readonly string _slash = Path.DirectorySeparatorChar.ToString();
		private readonly IConfiguration _configuration;
		
		public StorageProvider(IConfiguration configuration)
		{
			_storageRoot = $"{ContentRoot}{_slash}wwwroot{_slash}data{_slash}";
			_configuration = configuration;
		}

		public bool FileExists(string path)
		{
			Serilog.Log.Information($"File exists: {Path.Combine(ContentRoot, path)}");
			return File.Exists(Path.Combine(ContentRoot, path));
		}

		public async Task<IList<string>> GetThemes()
		{
			var themes = new List<string>();
			var themesDirectory = Path.Combine(ContentRoot, $"Views{_slash}Themes");
			try
			{
				foreach (string dir in Directory.GetDirectories(themesDirectory))
				{
					themes.Add(Path.GetFileName(dir));
				}
			}
			catch { }
			return await Task.FromResult(themes);
		}

		public async Task<ThemeSettings> GetThemeSettings(string theme)
		{
			var settings = new ThemeSettings();
			var fileName = Path.Combine(ContentRoot, $"wwwroot{_slash}themes{_slash}{theme.ToLower()}{_slash}settings.json");
			if (File.Exists(fileName))
			{
				try
				{
					string jsonString = File.ReadAllText(fileName);
					settings = JsonSerializer.Deserialize<ThemeSettings>(jsonString);
				}
				catch (Exception ex) 
				{
					Serilog.Log.Error($"Error reading theme settings: {ex.Message}");
					return null;
				}
			}

			return await Task.FromResult(settings);
		}

		public async Task<bool> SaveThemeSettings(string theme, ThemeSettings settings)
		{
			var fileName = Path.Combine(ContentRoot, $"wwwroot{_slash}themes{_slash}{theme.ToLower()}{_slash}settings.json");
			try
			{
				if (File.Exists(fileName))
					File.Delete(fileName);

				var options = new JsonSerializerOptions { WriteIndented = true, PropertyNameCaseInsensitive = true	};

				string jsonString = JsonSerializer.Serialize(settings, options);

				using FileStream createStream = File.Create(fileName);
				await JsonSerializer.SerializeAsync(createStream, settings, options);
			}
			catch (Exception ex)
			{
				Serilog.Log.Error($"Error writing theme settings: {ex.Message}");
				return false;
			}
			return true;
		}

		public async Task<bool> UploadFormFile(IFormFile file, string path = "")
		{
			path = path.Replace("/", _slash);
			VerifyPath(path);

			var fileName = GetFileName(file.FileName);

			if(InvalidFileName(fileName))
			{
				Serilog.Log.Error($"Invalid file name: {fileName}");
				return false;
			}

			var filePath = string.IsNullOrEmpty(path) ?
				 Path.Combine(_storageRoot, fileName) :
				 Path.Combine(_storageRoot, path + _slash + fileName);

			Serilog.Log.Information($"Storage root: {_storageRoot}");
			Serilog.Log.Information($"Uploading file: {filePath}");
			try
			{
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
					Serilog.Log.Information($"Uploaded file: {filePath}");
				}
			}
			catch (Exception ex)
			{
				Serilog.Log.Error($"Error uploading file: {ex.Message}");
			}

			return true;
		}

		public async Task<string> UploadFromWeb(Uri requestUri, string root, string path = "")
		{
			path = path.Replace("/", _slash);
			VerifyPath(path);

			var fileName = TitleFromUri(requestUri);
			var filePath = string.IsNullOrEmpty(path) ?
				 Path.Combine(_storageRoot, fileName) :
				 Path.Combine(_storageRoot, path + _slash + fileName);

			HttpClient client = new HttpClient();
			var response = await client.GetAsync(requestUri);
			using (var fs = new FileStream(filePath, FileMode.CreateNew))
			{
				await response.Content.CopyToAsync(fs);
				return await Task.FromResult($"![{fileName}]({root}{PathToUrl(filePath)})");
			}
		}

		public async Task<string> UploadBase64Image(string baseImg, string root, string path = "")
		{
			path = path.Replace("/", _slash);
			var fileName = "";

			VerifyPath(path);
			string imgSrc = GetImgSrcValue(baseImg);

			Random rnd = new Random();

			if (imgSrc.StartsWith("data:image/png;base64,"))
			{
				fileName = string.Format("{0}.png", rnd.Next(1000, 9999));
				imgSrc = imgSrc.Replace("data:image/png;base64,", "");
			}
			if (imgSrc.StartsWith("data:image/jpeg;base64,"))
			{
				fileName = string.Format("{0}.jpeg", rnd.Next(1000, 9999));
				imgSrc = imgSrc.Replace("data:image/jpeg;base64,", "");
			}
			if (imgSrc.StartsWith("data:image/gif;base64,"))
			{
				fileName = string.Format("{0}.gif", rnd.Next(1000, 9999));
				imgSrc = imgSrc.Replace("data:image/gif;base64,", "");
			}

			var filePath = string.IsNullOrEmpty(path) ?
				 Path.Combine(_storageRoot, fileName) :
				 Path.Combine(_storageRoot, path + _slash + fileName);

			await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(imgSrc));

			return $"![{fileName}]({root}{PathToUrl(filePath)})";
		}

		#region Private members

		private string ContentRoot
		{
			get
			{
				string path = Directory.GetCurrentDirectory();
				string testsDirectory = $"tests{_slash}Blogifier.Tests";
				string appDirectory = $"src{_slash}Blogifier";

				Serilog.Log.Information($"Current directory path: {path}");

				// development unit test run
				if (path.LastIndexOf(testsDirectory) > 0)
				{
					path = path.Substring(0, path.LastIndexOf(testsDirectory));
					Serilog.Log.Information($"Unit test path: {path}src{_slash}Blogifier");
					return $"{path}src{_slash}Blogifier";
				}

				// this needed to make sure we have correct data directory
				// when running in debug mode in Visual Studio
				// so instead of debug (src/Blogifier/bin/Debug..)
				// will be used src/Blogifier/wwwroot/data
				// !! this can mess up installs that have "src/Blogifier" in the path !!
				if (path.LastIndexOf(appDirectory) > 0)
				{
					path = path.Substring(0, path.LastIndexOf(appDirectory));
					Serilog.Log.Information($"Development debug path: {path}src{_slash}Blogifier");
					return $"{path}src{_slash}Blogifier";
				}
				Serilog.Log.Information($"Final path: {path}");
				return path;
			}
		}

		string GetFileName(string fileName)
		{
			// some browsers pass uploaded file name as short file name 
			// and others include the path; remove path part if needed
			if (fileName.Contains(_slash))
			{
				fileName = fileName.Substring(fileName.LastIndexOf(_slash));
				fileName = fileName.Replace(_slash, "");
			}
			// when drag-and-drop or copy image to TinyMce editor
			// it uses "mceclip0" as file name; randomize it for multiple uploads
			if (fileName.StartsWith("mceclip0"))
			{
				Random rnd = new Random();
				fileName = fileName.Replace("mceclip0", rnd.Next(100000, 999999).ToString());
			}
			return fileName. SanitizePath();
		}

		void VerifyPath(string path)
		{
			path = path.SanitizePath();

			if (!string.IsNullOrEmpty(path))
			{
				var dir = Path.Combine(_storageRoot, path);

				if (!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}
			}
		}

		string TitleFromUri(Uri uri)
		{
			var title = uri.ToString().ToLower();
			title = title.Replace("%2f", "/");

			if (title.EndsWith(".axdx"))
			{
				title = title.Replace(".axdx", "");
			}
			if (title.Contains("image.axd?picture="))
			{
				title = title.Substring(title.IndexOf("image.axd?picture=") + 18);
			}
			if (title.Contains("file.axd?file="))
			{
				title = title.Substring(title.IndexOf("file.axd?file=") + 14);
			}
			if (title.Contains("encrypted-tbn") || title.Contains("base64,"))
			{
				Random rnd = new Random();
				title = string.Format("{0}.png", rnd.Next(1000, 9999));
			}

			if (title.Contains("/"))
			{
				title = title.Substring(title.LastIndexOf("/"));
			}

			title = title.Replace(" ", "-");

			return title.Replace("/", "").SanitizeFileName();
		}

		string PathToUrl(string path)
		{
			string url = path.ReplaceIgnoreCase(_storageRoot, "").Replace(_slash, "/");
			return $"data/{url}";
		}

		string GetImgSrcValue(string imgTag)
		{
			if (!(imgTag.Contains("data:image") && imgTag.Contains("src=")))
				return imgTag;

			int start = imgTag.IndexOf("src=");
			int srcStart = imgTag.IndexOf("\"", start) + 1;

			if (srcStart < 2)
				return imgTag;

			int srcEnd = imgTag.IndexOf("\"", srcStart);

			if (srcEnd < 1 || srcEnd <= srcStart)
				return imgTag;

			return imgTag.Substring(srcStart, srcEnd - srcStart);
		}

		bool InvalidFileName(string fileName)
      {
			List<string> fileExtensions = new List<string>() { "png", "gif", "jpeg", "jpg", "zip", "7z", "pdf", "doc", "docx", "xls", "xlsx", "mp3", "mp4", "avi" };
         string configFileExtensions = _configuration.GetSection("Blogifier").GetValue<string>("FileExtensions");
			
			if(!string.IsNullOrEmpty(configFileExtensions))
			{
				fileExtensions = new List<string>(configFileExtensions.Split(','));
			}

			foreach(string ext in fileExtensions)
			{
				if(fileName.EndsWith(ext))
				{
					return false;
				}
			}

			return true;
      }

		#endregion
	}
}
