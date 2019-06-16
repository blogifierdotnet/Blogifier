var target = Argument("target", "Default");
var publishDirectory = Directory("./publish/");
var configuration = "Release";
var demo = Argument("demo", "false");
var home = Argument("home", "false");

Task("Clean").Does(() =>
{
	CleanDirectory(publishDirectory);

	if(home == "true")
	{
		// set blog route
		var blogCtrl = File("../src/App/Controllers/BlogController.cs");
		var fileContent = System.IO.File.ReadAllText(blogCtrl);
		fileContent = fileContent.Replace("//[Route(\"blog\")]", "[Route(\"blog\")]"); 
		System.IO.File.WriteAllText(blogCtrl, fileContent, Encoding.UTF8);

		// enable home page
		MoveFile("../src/App/Pages/_Index.cshtml", "../src/App/Pages/Index.cshtml");
	}
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
	
	CopyFileToDirectory("../plugins/Common/bin/Release/netcoreapp2.2/Common.dll", "./publish");

	CopyFileToDirectory("../src/Upgrade/bin/Release/netcoreapp2.2/Upgrade.dll", "./publish");
	CopyFileToDirectory("../src/Upgrade/bin/Release/netcoreapp2.2/Upgrade.deps.json", "./publish");
	CopyFileToDirectory("../src/Upgrade/bin/Release/netcoreapp2.2/Upgrade.runtimeconfig.json", "./publish");
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
        Framework = "netcoreapp2.2",
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

	if(home == "true")
	{
		// set back default route
		var blogCtrl = File("../src/App/Controllers/BlogController.cs");
		var fileContent = System.IO.File.ReadAllText(blogCtrl);
		fileContent = fileContent.Replace("[Route(\"blog\")]", "//[Route(\"blog\")]"); 
		System.IO.File.WriteAllText(blogCtrl, fileContent, Encoding.UTF8);

		// disable home page
		MoveFile("../src/App/Pages/Index.cshtml", "../src/App/Pages/_Index.cshtml");
	}
});

RunTarget(target);