Newsletter provides a way for blog visitors to subscribe to new posts by email, so it relies 
on [email service been configured](https://github.com/blogifierdotnet/Blogifier/blob/master/docs/SendGrid.md).
To test if email service works properly, blogger can navigate to `Newsletter` page in the admin 
and use form on the `Email` tab. Given [SendGrid API key](https://github.com/blogifierdotnet/Blogifier/blob/master/docs/SendGrid.md) 
set in the `appsettings.json` configuration file, should be able to send a test email to yourself.

When visitor subscribes to Newsletter, visitor information added to the list shown in the Newsletter admin page.
Along with user email address, Blogifier collects user IP, country and region to track basic usage statistics.

When new post published, Blogifier checks if email service available and if any subscriptions were added to the Newsletter. 
If so, Blogifier sends newsletter to every email address on subscriptions list and displays confirmation with number of emails sent.