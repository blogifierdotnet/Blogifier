var target = Argument("target", "Default");
var publishDirectory = Directory("./publish/");
var configuration = "Release";
var demo = Argument("demo", "false");

Task("Clean").Does(() =>
{
	CleanDirectory(publishDirectory);
});

Task("Build").IsDependentOn("Clean").Does(() =>
{
	var projects = GetFiles("../**/*.csproj");
	foreach(var project in projects)
	{
		DotNetCoreBuild(
			project.GetDirectory().FullPath,
			new DotNetCoreBuildSettings()
			{
				Configuration = configuration
			}
		);
	}
	
	CopyFileToDirectory("../src/Upgrade/bin/Release/netcoreapp3.1/Upgrade.dll", "./publish");
	CopyFileToDirectory("../src/Upgrade/bin/Release/netcoreapp3.1/Upgrade.deps.json", "./publish");
	CopyFileToDirectory("../src/Upgrade/bin/Release/netcoreapp3.1/Upgrade.runtimeconfig.json", "./publish");
});

Task("Test").IsDependentOn("Build").Does(() =>
{
	var projects = GetFiles("../tests/**/*.csproj");
	foreach(var project in projects)
	{
		DotNetCoreTest(project.ToString());
	}
});

Task("Default").IsDependentOn("Test").Does(() =>
{
	var settings = new DotNetCorePublishSettings
    {
        Framework = "netcoreapp3.1",
        Configuration = "Release",
        OutputDirectory = "./publish/"
    };
    DotNetCorePublish("../src/App/App.csproj", settings);

	if(demo == "true")
	{
		var appjson = File("./publish/appsettings.json");
		var fileContent = System.IO.File.ReadAllText(appjson);
		fileContent = fileContent.Replace("\"DemoMode\": false", "\"DemoMode\": true"); 
		System.IO.File.WriteAllText(appjson, fileContent);
	}
});

RunTarget(target);