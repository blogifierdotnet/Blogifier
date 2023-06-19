<br>
<h3 align="center">Blogifier</h3>
<p align="center">
  Blogifier是一个用ASP编写的自托管开源发布平台。. NET和Blazor WebAssembly。
  它可以用来快速，轻松地建立一个轻量级的，但功能齐全的个人或团体博客。
</p>
<br>

[English](README.md) | 简体中文

## 安装

### 源代码构建 [未发布]

你可以在未在 macOS 上测试的 windows linux 中构建，我更喜欢在 docker 中部署测试。

#### 本机构建

1. [ 下载 ](https://dotnet.microsoft.com/zh-cn/download) .NET 7.0 SDK 选择在您的主机上安装系统版本。[ 下载 ](https://nodejs.org/) Nodejs 14 及更高版本并将其安装在您的主机上。对于 linux，您可以使用包管理工具
2. 进入项目根目录，在widnows命令行运行./publish.cmd，在linux命令行运行sh ./publish.sh。
3. 当命令执行完成，没有报错，你会在项目根目录下看到dist文件夹，就是发布后的应用。您可以复制它以在任何地方运行。在windows下可以直接点击运行dist文件夹下的Blogifier.exe，在linux下请先授权Blogifier二进制文件的可执行权限，然后在命令行点击或运行。[注意] 由于release中不存在app_data目录，所以程序启动时可能会报错。重新开始吧。
4. 然后就可以localhost:5000用浏览器打开了
5. 完成，享受。

#### docker 构建

首先，请确保您的主机中已经安装了docker、docker-compose。

1. 进入项目根目录运行docker-compose up -d 命令，稍等一会……
2. 然后就可以localhost:8080用浏览器打开了
3. 完成，享受。

### 3.0之前的版本 [已发布]

在服务器上安装已编译应用程序以进行自托管的步骤：

1. .NET Core 运行时（当前为 7.0）必须安装在您的主机服务器上。
2. [ 下载 ](https://github.com/blogifierdotnet/Blogifier/releases) 最新版本。
3. 解压缩并复制到您的主机服务器。
4. 重新启动您的网站。
5. 打开您的网站，只有第一次您会被重定向到注册页面。<br> `example.com/admin/register/`
6. 注册，然后登录。<br> `example.com/admin/login/`
7. 完成，享受。

## 开发

如果你想自定义 Blogifier，或者贡献：

1. [ 下载 ](https://dotnet.microsoft.com/download/dotnet) 并安装 .NET SDK。
2. [ 下载 ](https://nodejs.org/) 下载并安装 NodeJs。
3. 下载、派生或克隆存储库。
4. 使用您喜欢的 IDE（VS Code、Visual Studio、Atom 等）打开项目。
5. 使用您的 IDE 或以下命令运行应用程序：
```
$ cd /your-local-path/Blogifier/src/Blogifier/
$ dotnet run
```
然后就可以localhost:5000用浏览器打开了

## 贡献


目前的Blogifier还不够完善，如果你是需要一个功能完善稳定的博客系统或许wordpress更适合，
相对这种成熟的博客系统这个项目这个项目还有很多功能没有实现。
所以最新版本暂未发布，目前项目更适合开发者自行搭建和使用。
我们可以共同改进，实现一个完全由dotnet技术实现的博客系统。
更快、更简单、更小的个人博客.

可以先在issues中提出功能，在pull requests中开发，这样可以跟踪开发进度。
欢迎大家一起参与开发。让我们一起学习和探索dotnet的最新技术。


## 团队

[![@dorthl](https://avatars.githubusercontent.com/u/13906219?s=60&v=4)](https://github.com/dorthl) &nbsp;
[![@farzindev](https://avatars.githubusercontent.com/u/6384978?s=60&v=4)](https://github.com/farzindev) &nbsp;
[![@rxtur](https://avatars.githubusercontent.com/u/1932785?s=60&v=4)](https://github.com/rxtur)

## Copyright and License
Code released under the MIT License. Docs released under Creative Commons.<br>
Copyright 2017–2023 Blogifier
