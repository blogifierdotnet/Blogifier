# Blogifier
[![Build Status](https://dev.azure.com/rtur/Blogifier/_apis/build/status/blogifierdotnet.Blogifier)](https://dev.azure.com/rtur/Blogifier/_build/latest?definitionId=3)

Blogifier is a single-user personal blog written in ASP.NET Core MVC and Blazor. It is small, easy to use and highly customizable 
via theming engine and Blazor components.

> If you are looking for a multi-user Angular version of Blogifier, please find it under [Blogifier.SPA](https://github.com/blogifierdotnet/Blogifier.SPA) repository.
> Both versions are fully supported.


## System Requirements

* Windows, Mac or Linux
* ASP.NET Core 3.1
* Visual Studio 2019, VS Code or other code editor (Atom, Sublime etc)
* SQLite by default, MS SQL Server, PostgreSQL and MySQL out of the box, EF compatible databases should work

## Getting Started

1. Clone or download source code
2. Run application in Visual Studio or using your code editor
3. Use admin/admin to log in

## Features

#### Markdown editor
Uses Markdown editor with built-in support for common functionality to make writing posts easy and productive process.

1. File upload - support for file and image upload with a click of a button
2. [HTML5 video/audio tags](https://github.com/blogifierdotnet/Blogifier/blob/master/docs/VideoAudio.md) - 
    upload and playback video and audio, YouTube video playback
3. Thumbnail generation - uploading large image automatically generates corresponding image thumbnail

#### Theme engine

1. Built-in themes - there are number of themes to choose ranging from simple and minimalistic to media-rich and sophisticated and list is growing.
2. Social buttons - no programming required, just add your social accounts
3. Themes are easy to adopt or build from scratch with minimal effort

#### Newsletter
Visitors can subscribe to the blog to be notified on new publications by email via 
[newsletter](https://github.com/blogifierdotnet/Blogifier/blob/master/docs/Newsletter.md) (requires 
[SendGrid](https://github.com/blogifierdotnet/Blogifier/blob/master/docs/SendGrid.md) email account)

#### Localization
Uses excellent intuitive [JSON localizer](https://github.com/blogifierdotnet/Blogifier/blob/master/docs/Localization.md) 
with number of preset languages. If your language is missing it can be easily added to the list

#### RSS feed
Supports standard RSS syndication feed

#### Search
Quick content search across the blog posts

#### Disqus comments
Using Disqus as a commenting service, here are 
[instructions](https://github.com/blogifierdotnet/Blogifier/blob/master/docs/ScriptIncludes.md) on how to setup and configure

#### Google Analytics
Google Analytics can be added to the site by the blogger as described in 
the [documentation](https://github.com/blogifierdotnet/Blogifier/blob/master/docs/ScriptIncludes.md) 

#### Multi-platform support
You can host your blog on Windows or Lunux server, use SQLite, SQL Server, MySQL or PostgreSQL databases out of the box. 
Even if you don't care about technology used, flexibility in hosting helps save money and make many decisions easier.

## Other Projects
There are two applications currently supported -
1. Main blogifier application is MVC-based with Blazor admin (this repository)
2. [SPA application](https://github.com/blogifierdotnet/Blogifier.SPA) with Angular front-end on top of ASP.NET Core web APIs

![blogifier-dgm](https://user-images.githubusercontent.com/1932785/81506457-1611e580-92bc-11ea-927e-b826c56ba21b.png)
> Blogifier publishes `Blogifier.Core` shared library to the Nuget gallery.
This package is used by Blogifier.SPA and can be used by any other application.

## Demo site

The [demo site](http://blogifier.net) is a playground to check out Blogifier features. You can write and publish posts, upload files and test application before install.

![philosophy](https://user-images.githubusercontent.com/1932785/81521511-0e2e6180-930d-11ea-8ad5-35d3cf2b6e8c.jpg)

The [developer's blog](http://rtur.net/blog).