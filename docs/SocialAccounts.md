Social Accounts allow blogger to add/remove custom values to and from the profile.
Themes or components can be designed to take advantage of these accounts.
For example, blogger can go to the profile and create a new social account with name of 
'twitter' and value of 'blogifierdotnet'. This value then can be retrieved in the theme like:

```html
@inject IDataService DataService
@{ 
    var twitter = DataService.CustomFields.GetCustomValue("twitter");
}
```

And to show twitter button in the UI, theme would:

```html
@if (!string.IsNullOrEmpty(twitter))
{
<a class="blog-social-link" target="_blank" href="https://twitter.com/@twitter">
  <i class="blog-social-icon fa fa-twitter"></i>
</a>
}
```

This way blogger can customize social buttons and other functionality as long as theme supports it.