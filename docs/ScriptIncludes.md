Blogifier allows to add custom HTML tags, styles and scripts to the page header and footer. 
This can be useful if blogger wants to include a stylesheet or meta tag on-the-fly, 
without application restart.

### Header scripts
For a theme to support header scripts, this partial view should be included to the page header just before closing `head` tag:
```xml
<head>
  <meta charset="utf-8">
  <!-- tags removed for brevity... -->
  <partial name="~/Views/Shared/HeaderScript.cshtml" />
</head>
```

> Included header script will also automatically add page title, description, canonical and alternate URLs.

### Footer scripts
For a theme to support footer scripts, this should be included in the page just before closing `body` tag:
```xml
<body>
  <!-- page content removed for brevity... -->
  <partial name="~/Views/Shared/FooterScript.cshtml" />
</body>
```

The most common use for a trailing scripts would be things like Disqus comments or Google 
Analytics. 

### Disqus comments
To support Disqus comments, theme needs to include a placeholder in the `Post.cshtml`
 where comment thread should appear:
```xml
<div id="disqus_thread"></div>
```

And, for the comment counts to work, another placeholder should be added in the `Post.cshtml`
 for every post:
```xml
<a href="posts/@post.Slug#disqus_thread"></a>
```

Then blogger can sign up for Disqus account and include provided scripts in the trailing footer
 script in the admin/settings/scripts:

```html
<script>
  (function () {  // DON'T EDIT BELOW THIS LINE
    var d = document, s = d.createElement('script');
    s.src = '//yoursite.disqus.com/embed.js';
    s.setAttribute('data-timestamp', +new Date());
    (d.head || d.body).appendChild(s);
  })();
</script>
<script id="dsq-count-scr" src="//yoursite.disqus.com/count.js" async></script>
```

### Google Analytics
To support Google Analytics, [sign up for account with Google](https://google.com/analytics/) 
and simply add analytics script to your footer scripts, replacing `UA-1234567` with your own ID:

```xml
<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src="https://www.googletagmanager.com/gtag/js?id=UA-1234567"></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());
  gtag('config', 'UA-1234567');
</script>
```