using Askmethat.Aspnet.JsonLocalizer.Localizer;
using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Sotsera.Blazor.Toaster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class SocialAdmin : ComponentBase
    {
        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IJsonStringLocalizer<EmailForm> Localizer { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }

        protected List<SocialField> SocialFields { get; set; }
        protected SocialField CurrentField { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        protected async Task Load()
        {
            SocialFields = await DataService.CustomFields.GetSocial();
            CurrentField = new SocialField { AuthorId = 0 };
            StateHasChanged();
        }

        protected async Task Save()
        {
            var newField = new SocialField
            {
                Title = CurrentField.Title.Capitalize(),
                Content = CurrentField.Content,
                Icon = $"fa-{CurrentField.Title.ToLower()}",
                AuthorId = 0,
                Name = $"social|{CurrentField.Title.ToLower()}|1",
                Rank = 1
            };

            await DataService.CustomFields.SaveSocial(newField);
            await Load();
            Toaster.Success("Updated");
        }

        protected async Task RemoveField(int id)
        {
            var existing = DataService.CustomFields.Single(f => f.Id == id);
            if (existing != null)
            {
                DataService.CustomFields.Remove(existing);
                DataService.Complete();
                Toaster.Success(Localizer["completed"]);
                await Load();
            }
            else
            {
                Toaster.Error($"Error removing field #{id}");
            }
        }

        protected async Task RankField(int id, bool up)
        {
            var existing = DataService.CustomFields.Single(f => f.Id == id);
            if (existing != null)
            {
                var fieldArray = existing.Name.Split('|');
                if(fieldArray.Length > 2)
                {
                    int oldRank = int.Parse(fieldArray[2]);
                    int newRank = 1;

                    if (up && oldRank > 1) newRank = oldRank - 1;
                    if (!up) newRank = oldRank + 1;                 

                    var toUpdate = new SocialField
                    {
                        Id = id,
                        Title = fieldArray[1].Capitalize(),
                        Icon = $"fa-{fieldArray[1]}",
                        Rank = newRank,
                        Name = $"{fieldArray[0]}|{fieldArray[1]}|{newRank}",
                        AuthorId = existing.AuthorId,
                        Content = existing.Content
                    };
                    await DataService.CustomFields.SaveSocial(toUpdate);
                    DataService.Complete();
                    Toaster.Success(Localizer["completed"]);
                    await Load();
                }
                else
                {
                    Toaster.Error($"Error - unexpected field format: {existing.Name}");
                }
            }
            else
            {
                Toaster.Error($"Error moving field #{id}");
            }
        }
    }
}