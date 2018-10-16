### Getting Started

To start with new theme, create a folder, for example `Views/Themes/Custom` and add two files,
`List.cshtml` and `Post.cshtml`.

The list declares a model and, for each post in the `Model.Posts` displays title and description.
The title is a link pointing to post detail and description is converted from Markdown to HTML.

```html
<!-- Views/Themes/Custom/List.cshtml -->
@model ListModel
<!DOCTYPE html>
<html>
<head></head>
<body>
  @if (Model.Posts != null)
  {
    foreach (var item in Model.Posts)
    {
      <h2><a href="~/posts/@item.Slug">@item.Title</a></h2>
      <div>@Html.Raw(Markdig.Markdown.ToHtml(item.Description))</div>
    }
  }
</body>
</html>
```

The post detail just outputs content of the post on the page.

```xml
<!-- Views/Themes/Custom/Post.cshtml -->
@model PostModel
<!DOCTYPE html>
<html>
<head></head>
<body>
  <div>@Html.Raw(Markdig.Markdown.ToHtml(Model.Post.Content))</div>
</body>
</html>
```

Now load your blog, navigate to admin settings and select `Custom` theme. Save and view your blog.
It will use your newly created theme - congratulations, all done!

### Improving on Design

Here is another super simple example using Bootstrap stylesheet.

```html
@model ListModel
<!DOCTYPE html>
<html>
<head>
  <title>@Model.Blog.Title</title>
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
</head>
<body>
  <div class="container">
    <div class="jumbotron">
      <h1>@Model.Blog.Title</h1>
      <p>@Model.Blog.Description</p>
    </div>
    <div class="row">
      @if (Model.Posts != null)
      {
        foreach (var item in Model.Posts)
        {
        <div class="col-lg-4">
          <h2>@item.Title</h2>
          @Html.Raw(Markdig.Markdown.ToHtml(item.Description))
          <p><a class="btn btn-primary" href="~/posts/@item.Slug" role="button">View details &raquo;</a></p>
        </div>
        }
      }
    </div>
    <footer class="footer">
      <p>&copy; @DateTime.Now.Year Company, Inc.</p>
    </footer>
  </div>
</body>
</html>
```

For your own styles, create folder under `wwwroot/themes/theme-name` and reference it from the page header.
Same goes for JavaScript or any custom images you want to use in the custom theme. 

```html
<link href="~/themes/custom/style.css" rel="stylesheet">
<script src="~/themes/custom/script.js"></script>
```

Anything available in ASP.NET layouts can be used for the theme without any restrictions, like partial views for 
headers and footers and so on. Please refer to `Standard` theme for examples on using some of the more complex elements.

### Understanding the Models

Theme authors can use models inside post lists or single post. All properties from 
the models can be accessed within theme, for example `Model.Blog.Title`.

#### ListModel

Name | Data Type | Description
--- | --- | ---
Blog | [BlogItem](https://github.com/blogifierdotnet/Blogifier/blob/master/src/Core/Data/Models/AppModel.cs) | Blog settings (title, description etc.) 
Author | [Author](https://github.com/blogifierdotnet/Blogifier/blob/master/src/Core/Data/Domain/Author.cs) | Author of the blog 
Category | string | Category (when browse by category)
Posts | IEnumerable &lt;PostItem&gt; |  List of blog posts
Pager | [Pager](https://github.com/blogifierdotnet/Blogifier/blob/master/src/Core/Helpers/Pager.cs) | Pager (older/newer links)
PostListType | PostListType | Posts type (blog, category, author, search)

#### PostModel

Name | Data Type | Description
--- | --- | ---
Blog | BlogItem | Blog settings (title, description etc.) 
Post | PostItem | Current post
Older | PostItem | Previous/older post
Newer | PostItem | Next/newer post
