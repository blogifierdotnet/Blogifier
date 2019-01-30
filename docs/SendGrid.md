Blogifier uses [SendGrid](https://sendgrid.com/) as email provider. It is one of the cloud 
email providers recommended for ASP.NET Core. It is free for under 100 daily emails and has 
paid options if someone needs more.

### SendGrid API
To use SendGrid, blogger would need to sign up for service and get 
[API key](https://sendgrid.com/solutions/email-api/). Once you have API key, all that
required is to update `appsettings.json`: 

```cmd
{
  "Blogifier": {
    "SendGridApiKey": "YOUR-SENDGRID-API-KEY",
    "SendGridEmailFrom": "admin@blog.com",
    "SendGridEmailFromName": "Blog admin"
  }
}
```

Currently, SendGrid only used by Newsletter widget. When guest subscribes to the blog via Newsletter, 
every time new post gets published, Blogifier goes over list of subscribers and sends notification 
about new publication to every subscriber.

### Why not SMTP?
Plain SMTP client routes calls to actual SMTP email server, like Gmail or HotMail.
Unfortunately, most SMTP providers and hosters block 3rd party email traffic. 
Cloud providers have limitations, but much more reliable.

### Newsletter Template

There is default HTML template for a Newsletter: `/wwwroot/templates/newsletter.html`.
Newsletter emails will use this template as HTML content.

```
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Newsletter</title>
</head>
<body>
    <h2>{3}</h2>
    <div>Just published on {0}</div>
    <div>{4}</div>
    <div><a href="{10}/posts/{6}">Keep reading...</a></div>
</body>
</html>
```

### Template Arguments

You can customize Newsletter template and use any of the arguments below putting 
numeric values in the curly brackets. For example use "{0}" in template to output blog title.


```
var htmlContent = string.Format(template,
	blog.Title, // 0
	blog.Logo,  // 1
	blog.Cover, // 2
	post.Title, // 3
	post.Description, // 4 
	post.Content, // 5
	post.Slug, // 6
	post.Published, // 7 
	post.Cover, // 8
	post.Author, // 9
	siteUrl); // 10
```