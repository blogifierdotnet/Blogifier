using System.Reflection;

namespace Blogifier.Core.Common
{
	public class Settings
    {
		public static string Title { get; set; } = "Application Name";

		public static string Version
		{
			get
			{
				return typeof(Settings)
					.GetTypeInfo()
					.Assembly
					.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
					.InformationalVersion;
			}
		}

		public static string OSDescription
		{
			get
			{
				return System.Runtime.InteropServices.RuntimeInformation.OSDescription;
			}
		}
	}
}
