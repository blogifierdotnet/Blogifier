Import function allows to import posts and resources (images, file attachments) from existing RSS feed.

### Save RSS feed to local drive as XML file.
For example, I can go to `http://myblog.com/syndication.axd?maxitems=1000` in Chrome browser and, 
when page loaded, right-click and save as `MyBlog.xml` to local `c:` drive.

### Load feed into Blogifier
In the Blogifier admin panel, go to settings -> import and select saved .xml file. This should start
import process and, when done, display a report on what was imported.

Note: `?maxitems=12345` is specific to BlogEngine.NET, other blogs may have different 
ways to let you specify maximum number of items to export to RSS feed, or create RSS export 
from control panel or some other way.