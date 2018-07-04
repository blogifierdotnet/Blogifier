using Core.Data;
using Core.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace App.Pages
{
    public class AuthorsModel : PageModel
    {
        private readonly IUnitOfWork _db;

        public PostListModel Posts { get; set; }
        public Author Author { get; set; }

        public AuthorsModel(IUnitOfWork db)
        {
            _db = db;
        }

        public async void OnGetAsync(string name)
        {
            Author = await _db.Authors.GetItem(a => a.AppUserName == name);

            var pager = new Pager(1);
            var posts = await _db.BlogPosts.Find(p => p.AuthorId == Author.Id && p.Published > DateTime.MinValue, pager);

            Posts = new PostListModel { Posts = posts, Pager = pager };
        }
    }
}