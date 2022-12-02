<br>
<h3 align="center">Blogifier</h3>
<p align="center">
    Blogifier is a self-hosted open source publishing platform written in ASP.NET and Blazor WebAssembly. It can be used to quickly and easily set up a lightweight, but fully functional personal or group blog.
</p>

<br><br>
## Installation

Steps to install compiled application on the server for a self-hosting:

1. .NET Core Runtime (currently 6.0) must be installed on your host server.
2. [Download](https://github.com/maddadder/Blogifier/releases) the latest release.
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
4. Rename appsettings.sqlite.json to appsettings.Development.json
5. Run the app with your IDE or these commands:

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
## Db Migrations
```
cd src
dotnet-ef migrations add Init --verbose --project Blogifier.Core --startup-project Blogifier

To undo this action, use 'ef migrations remove --verbose --project Blogifier.Core --startup-project Blogifier'
```

<br><br>
## Debugging Notes
```
pkill -9 Blogifier
while true; do kubectl port-forward --namespace default pod/neon-system-db-0 5432:5432; done
while true; do kubectl port-forward --namespace default pod/acid-minimal-cluster-0 5432:5432; done
#to backup restore db use pg-admin4. To restore make sure to select the role name
```

<br><br>
## Deploy to kubernetes
```
#plhhoa
docker-compose build
docker push neon-registry.4e88-13d3-b83a-9fc9.neoncluster.io/leenet/blogifier:1.11.52
helm upgrade blogifier-plhhoa -f ./chart/values.yaml -f ./chart/values.plhhoa.yaml ./chart --namespace leenet


#zambonigirl
docker-compose build
docker push neon-registry.4e88-13d3-b83a-9fc9.neoncluster.io/leenet/blogifier:1.11.52
helm upgrade blogifier-zambonigirl -f ./chart/values.yaml -f ./chart/values.zambonigirl.yaml ./chart --namespace leenet

#paintedravendesign
docker-compose build
docker push neon-registry.4e88-13d3-b83a-9fc9.neoncluster.io/leenet/blogifier:1.11.52
helm upgrade blogifier-paintedravendesign -f ./chart/values.yaml -f ./chart/values.paintedravendesign.yaml ./chart --namespace leenet

#pawsnclaws
docker-compose build
docker push neon-registry.4e88-13d3-b83a-9fc9.neoncluster.io/leenet/blogifier:1.11.52
helm upgrade blogifier-pawsnclaws -f ./chart/values.yaml -f ./chart/values.pawsnclaws.yaml ./chart --namespace leenet

#plhhoa-t30
docker-compose build
docker push 192.168.1.151:32000/blogifier:1.11.52
helm upgrade blogifier-plhhoa -f ./chart/values.yaml -f ./chart/values.plhhoa-t30.yaml ./chart --namespace default


#zambonigirl-t30
docker-compose build
docker push 192.168.1.151:32000/blogifier:1.11.52
helm upgrade blogifier-zambonigirl -f ./chart/values.yaml -f ./chart/values.zambonigirl-t30.yaml ./chart --namespace default


#paintedravendesign-t30
docker-compose build
docker push 192.168.1.151:32000/blogifier:1.11.52
helm upgrade blogifier-paintedravendesign -f ./chart/values.yaml -f ./chart/values.paintedravendesign-t30.yaml ./chart --namespace default


#pawsnclaws-t30
docker-compose build
docker push 192.168.1.151:32000/blogifier:1.11.52
helm upgrade blogifier-pawsnclaws -f ./chart/values.yaml -f ./chart/values.pawsnclaws-t30.yaml ./chart --namespace default


#ollie-t30
docker-compose build
docker push 192.168.1.151:32000/blogifier:1.11.52
helm upgrade blogifier-ollie -f ./chart/values.yaml -f ./chart/values.ollie-t30.yaml ./chart --namespace default

```