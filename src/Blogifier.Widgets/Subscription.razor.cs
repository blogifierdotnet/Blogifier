using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core.Data;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Helpers;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Sotsera.Blazor.Toaster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class Subscription : ComponentBase
    {
        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IJsonStringLocalizer<Subscription> Localizer { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected NewsletterModel Model { get; set; }
        protected string SearchTerm { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetSubscriptions(1);
        }

        public async Task GetSubscriptions(int page = 1)
        {
            var pager = new Pager(page);
            var items = await DataService.Newsletters.GetList(e => e.Id > 0, pager);

            Model = new NewsletterModel
            {
                Emails = items,
                Pager = pager
            };
            StateHasChanged();
        }

        protected async Task SearchKeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await SearchSubscriptions(1);
            }
        }

        protected async Task SearchSubscriptions(int page)
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                await GetSubscriptions(1);
            }
            else
            {
                var pager = new Pager(page);
                IEnumerable<Newsletter> items;

                items = await DataService.Newsletters.GetList(e => e.Email.Contains(SearchTerm) || e.Ip.Contains(SearchTerm), pager);

                Model = new NewsletterModel
                {
                    Emails = items,
                    Pager = pager
                };
            }
        }

        protected async Task RemoveSubscription(int id)
        {
            var existing = DataService.Newsletters.Single(n => n.Id == id);
            if(existing != null)
            {
                DataService.Newsletters.Remove(existing);
                DataService.Complete();
                Toaster.Success("Removed");
                await GetSubscriptions(1);
            }
        }
    }
}