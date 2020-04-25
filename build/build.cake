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
});

Task("Test").IsDependentOn("Build").Does(() =>
{
	//var projects = GetFiles("../tests/**/*.csproj");
	//foreach(var project in projects)
	//{
	//	DotNetCoreTest(project.ToString());
	//}
});

Task("Default").IsDependentOn("Test").Does(() =>
{
	var settings = new DotNetCorePublishSettings
    {
        Framework = "netcoreapp3.1",
        Configuration = "Release",
        OutputDirectory = "./publish/"
    };
    DotNetCorePublish("../src/Blogifier/Blogifier.csproj", settings);

	if(demo == "true")
	{
		var appjson = File("./publish/appsettings.json");
		var fileContent = System.IO.File.ReadAllText(appjson);
		fileContent = fileContent.Replace("\"Demo\": false", "\"Demo\": true"); 
		System.IO.File.WriteAllText(appjson, fileContent);
	}
});

RunTarget(target);