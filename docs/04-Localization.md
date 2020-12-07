### Blazor Internationalization(I18n) Text

Localization and internationalization is done on admin UI level using 
[Blazor Internationalization](https://github.com/jsakamoto/Toolbelt.Blazor.I18nText) package. 
It uses JSON files instead of standard XML resources used by MS frameworks and compiles resources on build making them strongly typed.

Resource language files located under `Blogifier.Admin/i18ntext` folder. 
If browser set to use one of the cultures in this folder, admin UI should display accordingly.