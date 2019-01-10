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