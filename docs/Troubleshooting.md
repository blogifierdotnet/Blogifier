Blogifier is server-side ASP.NET Core application that can be deployed to different environments and even operating systems. 
Below several common scenarios to run and troubleshoot locally before application deployed to host server.

### Run in Visual Studio
1. Open solution in Visual Studio, right-click `App` project in solution explorer and set it as startup project
2. Run in debug mode and make sure application runs in the web browser
3. In the browser, open developer tools, refresh page and make sure no errors show up in the console

If all looks good, application is ready for deployment

### Deploy and Run from Local Folder
Use steps outlined in the [build section](https://github.com/blogifierdotnet/Blogifier/blob/master/docs/Build.md)
to publish application to the local folder and run it using `dotnet App.dll` command.
Again, open developer tools and verify no client-side errors thrown in the JavaScript console.

### Deploy to Host Server
The output produced and verified in the local deployment step is exactly what needs to be published to the host.
Usually your host provides information how to upload and set up application on remote server.
It can differ, but important things are:

1. Verify application runs locally (steps above)
2. Move all files from the `build/publish` folder to your remote host
3. Run application on your remote hosting server
4. Verify with development tools there no errors (same as in previous steps)

If at any point during this process you run into errors, 
please post them to [Github issues](https://github.com/blogifierdotnet/Blogifier/issues) along with **steps to re-produce the error**.
Without steps to re-produce the error, it is hard or even not possible to fix it.

> It is common that application runs fine locally and only has issues when deployed, because remote host does not properly setup or configured.
It is also common that application was modified/changed to meet custom conditions.
If so, it is important to identify what is the difference between standard and modified version.