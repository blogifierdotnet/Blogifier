<br>
<h3 align="center">Blogifier</h3>
<p align="center">
    Blogifier is a self-hosted open source publishing platform written in ASP.NET and Blazor WebAssembly. It can be used to quickly and easily set up a lightweight, but fully functional personal or group blog.
</p>

<br><br>
## Installation

Steps to install compiled application on the server for a self-hosting:

1. .NET Core Runtime (currently 6.0) must be installed on your host server.
2. [Download](https://github.com/blogifierdotnet/Blogifier/releases) the latest release.
3. Unzip and copy to your host server.<br>
4. Restart your website.
5. Open your website and only the first time you'll be redirected to the register page.<br> `example.com/admin/register/`
6. Register, and then log in.<br> `example.com/admin/login/`
7. Done, enjoy.

<br><br>
## Development
If you want to customize the Blogifier, or contribute:

1. [Download](https://dotnet.microsoft.com/download/dotnet) and Install .NET SDK.
2. Download, fork, or clone the repository.
3. Open the project with your favorite IDE (VS Code, Visual Studio, Atom, etc).
4. Run the app with your IDE or these commands:

```
$ cd /your-local-path/Blogifier/src/Blogifier/
$ dotnet run
```
Then you can open `localhost:5000` with your browser, Also login to the admin panel `localhost:5000/admin/`.
```
username: admin@example.com
password: admin
```



<br><br>
## Debugging Notes
```
pkill -9 Blogifier

kubectl port-forward pod/neon-system-db-0 5432 --namespace neon-system
#to backup restore db use pg-admin4. To restore make sure to select the role name
```

<br><br>
## Deploy to kubernetes
```
docker-compose build
docker push neon-registry.4e88-13d3-b83a-9fc9.neoncluster.io/leenet/blogifier:1.11.26
helm upgrade blogifier ./chart --namespace leenet

```