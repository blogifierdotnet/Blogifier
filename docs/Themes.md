### Quick Summary

- Any folder added to Blogifier under  `src/App/wwwroot/themes` will show up in admin as a theme
- When selected as active theme in admin, Blogifier will copy files from theme folder to `_active`

Blogifier uses `_active` folder as Angular application root, so any Angular application deployed there will be available at "/".

```csharp
services.AddSpaStaticFiles(configuration =>
{
  configuration.RootPath = "wwwroot/themes/_active";
});
```

In the box Blogifier themes are prebuilt Angular CLI applications with source code hosted at `https://github.com/blogifierdotnet/themes`.
Every time theme source updated, it is build to distribution folder and deployed to Blogifier.

> [Angular Theme for Blogifier - Jump Start](http://rtur.net/posts/angular-theme-for-blogifier-jump-start)

### Routing

Blogifier set up with both MVC and Angular routes, so theme can use normal Angular techniques
to navigate within application as long as it does not clash with existing Blogifeir routes
(like `/admin`, `/account` etc.). Here is simple example with `/` and `/posts/post-slug` routes.

```javascript
const routes: Routes = [
  { path: '', component: HomeComponent }, 
  { path: 'posts/:slug', component: PostsComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ]
})
```

### Pulling the Data

Blogifier exposes data to the theme via APIs. Documentation available at `/swagger`.
Some APIs require authentication or admin rights and can't be used in the theme.
All public APIs, including CORS enabled, can be used in the theme to pull data.

```javascript
export class BlogService {
  constructor(private http: HttpClient) { }
  getPosts(): Observable<IPostList> {
    return this.http.get<IPostList>('/api/posts');
  }
}
```

Then client, like HomeController, can call this service.

```javascript
import { BlogService, IPostList } from '../core/blog.service';
export class HomeComponent implements OnInit {
  postList: IPostList;
  constructor(private blogService: BlogService) { }
  ngOnInit(): void {
    this.blogService.getPosts().subscribe(
      result => { this.postList = result; }
    );
  }
}
```

And output posts in the HTML page

```javascript
<div *ngFor="let post of postList.posts">
  <h4>{{ post.title }}</h4>
</div>
```

Because data service might be commonly used in many themes, 
it is [implemented as single file](https://github.com/blogifierdotnet/themes/blob/master/simple/src/app/core/blog.service.ts) 
and included in every built-in theme. It can be copied into new theme to speed up development.

(Later it can become external package distributed via `npm install`)