<br>
<h3 align="center">Blogifier</h3>
<p align="center">
    Blogifier is a self-hosted open source publishing platform written in ASP.NET and Blazor WebAssembly. 
    It can be used to quickly and easily set up a lightweight, but fully functional personal or group blog.
</p>

## Installation
### Currently built from source [Not Released]
Can build in windows linux not tested on macOS, I prefer to deploy tests in docker.
#### native build
1. [Download](https://dotnet.microsoft.com/zh-cn/download) .NET 7.0 SDK Choose to install the system version on your host. [Download](https://nodejs.org/) Nodejs 14 and above and install it on your host. For linux you can use the package management tool
2. Navigate to the project root directory, run ./publish.cmd on the command line in widnows, run sh ./publish.sh on the command line in linux.
3. When the command execution is complete and there are no errors, you will see the dist folder in the project root directory, which is the application after publishing. You can copy it to run anywhere. In windows, you can directly click to run the dist folder Blogifier.exe , in linux, please authorize the executable permission of the Blogifier binary file first and then click or run it on the command line. [note] Because the app_data directory does not exist in the release, an error may occur when the program starts. Just start it again.
4. Then you can open `localhost:5000` with your browser
3. Done, enjoy.

#### docker build
First of all, please make sure that docker, docker-compose has been installed in your host.
1. Navigate to the project root directory Run the ```docker-compose up -d ``` command, wait a while ...
2. Then you can open `localhost:8080` with your browser
3. Done, enjoy.

### Versions before 3.0
Steps to install compiled application on the server for a self-hosting:

1. .NET Core Runtime (currently 7.0) must be installed on your host server.
2. [Download](https://github.com/blogifierdotnet/Blogifier/releases) the latest release.
3. Unzip and copy to your host server.<br>
4. Restart your website.
5. Open your website and only the first time you'll be redirected to the register page.<br> `example.com/admin/register/`
6. Register, and then log in.<br> `example.com/admin/login/`
7. Done, enjoy.

## Development
If you want to customize the Blogifier, or contribute:

1. [Download](https://dotnet.microsoft.com/download/dotnet) and Install .NET SDK.
2. [Download](https://nodejs.org/) and Install NodeJs.
2. Download, fork, or clone the repository.
3. Open the project with your favorite IDE (VS Code, Visual Studio, Atom, etc).
4. Run the app with your IDE or these commands:
```
$ cd /your-local-path/Blogifier/src/Blogifier/
$ dotnet run
```
Then you can open `localhost:5000` with your browser

## Contributing
Please read [contributing guidelines](https://github.com/blogifierdotnet/Blogifier/blob/main/.github/CONTRIBUTING.md). We have a list of things there that you can help us with.

## Team
[![@dorthl](https://avatars.githubusercontent.com/u/13906219?s=60&v=4)](https://github.com/rxtur)
[![@farzindev](https://avatars.githubusercontent.com/u/6384978?s=60&v=4)](https://github.com/farzindev) &nbsp;
[![@rxtur](https://avatars.githubusercontent.com/u/1932785?s=60&v=4)](https://github.com/rxtur)

## Copyright and License
Code released under the MIT License. Docs released under Creative Commons.<br>
Copyright 2017–2023 Blogifier
