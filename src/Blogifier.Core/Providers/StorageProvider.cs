using Blogifier.Core.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Blogifier.Core.Providers
{
	public interface IStorageProvider
	{
		Task<IList<string>> GetThemes();
		bool FileExists(string path);
		Task<bool> UploadFormFile(IFormFile file, string path = "");
	}

	public class StorageProvider : IStorageProvider
	{
		private readonly IWebHostEnvironment _environment;
		private readonly string _slash = Path.DirectorySeparatorChar.ToString();
		private string _storageRoot;

		public StorageProvider(IWebHostEnvironment environment)
		{
			_environment = environment;
			_storageRoot = $"{_environment.ContentRootPath}{_slash}wwwroot{_slash}data{_slash}";
		}

		public bool FileExists(string path)
		{
			return File.Exists(Path.Combine(_environment.ContentRootPath, path));
		}

		public async Task<IList<string>> GetThemes()
		{
			var themes = new List<string>();
			var themesDirectory = Path.Combine(_environment.ContentRootPath, $"Views{_slash}Themes");
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

		public async Task<bool> UploadFormFile(IFormFile file, string path = "")
		{
			path = path.Replace("/", _slash);

			VerifyPath(path);

			var fileName = GetFileName(file.FileName);
			var filePath = string.IsNullOrEmpty(path) ?
				 Path.Combine(_storageRoot, fileName) :
				 Path.Combine(_storageRoot, path + _slash + fileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}
			return true;
		}

		#region Private members

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
			return fileName.SanitizePath();
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

		#endregion
	}
}
