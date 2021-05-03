# Contributing to Blogifier
Thanks for contributing to the Blogifier. If you haven't already, please setup your environment and [run the Blogifier locally](https://github.com/blogifierdotnet/Blogifier/blob/main/README.md#development).

Here is the list of things that you can contribute:

1. [Translation](#translation)
2. [Test and report bugs](#test-and-report-bugs)

# Translation

In the "[/src/Blogifier.Shared/Resouces/](https://github.com/blogifierdotnet/Blogifier/tree/main/src/Blogifier.Shared/Resources)" folder we keep all the resources files.

The `Resource.resx` is the main English language that you can copy and create another Resource file for any other languages.<br> For example, `Resouce.es.resx` is created for Spanish with `es` language code. So if your language is not in the Resources folder, copy the `Resource.resx` and rename it and add your language code at the end.

### With Visual Studio
Visual studio has a built-in GUI for the `.resx` files which shows you the name and value. which you only edit/translate the value.

![resx](https://user-images.githubusercontent.com/6384978/116900447-fd79a880-ac4d-11eb-89e3-a4dc1f250720.png)

### With VS Code or any other IDE
Open the `.resx` file and scroll down and you'll see lines like this:

```
<data name="general" xml:space="preserve">
    <value>General</value>
</data>
```

Basically, you just need to translate what's in the `<value>` tag.

### Testing
1. In Google Chrome or any other browser, go to the browser settings and add the language to the top of the list.
2. Run the Blogifier with `dotnet watch run` command on the `/src/blogifier/`.
3. login to the blogifier admin panel. and see the results as you are translating.

### Pull request
After It's done and ready, please give it a pull request and we review it and merge it with the main branch.

# Test and report bugs
coming soon.
