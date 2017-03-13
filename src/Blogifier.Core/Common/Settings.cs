using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
	}
}
