Blogifier does not expose any endpoints to push notifications to the blog - 
it only pulls notifications from known sources. Notifications can be sent
to individual user or to all application users. 

### Removing notificaton
Every notification has an `active`
flag and by closing specific notification in UI this flag is set to `false` 
and notification will no longer appear in the UI, but still exists on the back-end. 
There should be not too many notifications, mostly system generated. For now
we just display 5 latest active notifications and do not clear old messages
from database.

### Checking for Latest Release
On admin page load, notification service calls Github API to check latest
release available in Blogifier repository. This call is cached for 10 
minutes so it does not slow down admin site. If version in repository is newer,
service will generate notification to all users providing link to download page.
Eventually, this will be transformed into blog auto-update functionality with
message sent only to admins and ability to upgrade blog from admin panel.