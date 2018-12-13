
### Localization
Currently Blogifier supports server-side localization in admin panel, with plans to add client-side (JS) 
localization. There are two languages at the moment included out of the box, with English (US) set as default. 
If you can translate to other languages, I'll be happy to include more. This should be easy.

### Adding new language
To add another language, translations has to be added to `Resources/admin.json`. 
Here is example for adding French:

```cmd
[
  {
    "Key": "general",
    "Values": {
      "en-US": "General",
      "ru-RU": "Главная",
      "fr-FR": "Général"
    }
  }
  ...
]
```

Then add this language to supported cultures in `Startup.cs`

```cmd
var supportedCultures = new[]
{
  new CultureInfo("en-US"),
  new CultureInfo("ru-RU"),
  new CultureInfo("fr-FR")
};
```

After these 2 steps, new language will appear in `admin/settings/general` languages dropdown and can be
enabled as default language for admin UI. 

> Blogifier uses [Askmethat-Aspnet-JsonLocalizer](https://github.com/AlexTeixeira/Askmethat-Aspnet-JsonLocalizer)
to allow JSON files instead of default `.resx` required by Visual Studio. This should enable to share resources 
with client-side JavaScript natively.
