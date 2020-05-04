using Blogifier.Core.Data;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.FeatureManagement;
using Sotsera.Blazor.Toaster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blogifier.Widgets
{
    public partial class Profile
    {
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        protected IDataService DataService { get; set; }
        [Inject]
        protected IToaster Toaster { get; set; }
        [Inject]
        protected SignInManager<AppUser> SignInManager { get; set; }
        [Inject]
        protected UserManager<AppUser> UserManager { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        IFeatureManager FeatureManager { get; set; }

        protected Author Author { get; set; }
        protected IEnumerable<CustomField> UserFields { get; set; }
        protected CustomField CurrentField { get; set; }
        protected bool Edit { get; set; }
        
        protected ChangePasswordModel PwdModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        public async Task Load()
        {
            var authState = await AuthenticationStateTask;

            if(authState == null || !authState.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("account/login?returnUrl=/admin");
            }

            Author = await DataService.Authors.GetItem(
                a => a.AppUserName == authState.User.Identity.Name);

            PwdModel = new ChangePasswordModel {
                UserName = authState.User.Identity.Name
            };

            CurrentField = new CustomField { AuthorId = Author.Id, Name = "", Content = "" };
            UserFields = DataService.CustomFields.Find(f => f.AuthorId == Author.Id);
        }

        protected async Task Save()
        {
            try
            {
                await DataService.Authors.Save(Author);
                Toaster.Success("Saved");
            }
            catch (Exception ex)
            {
                Toaster.Error(ex.Message);
            }
        }

        protected void SaveCustom()
        {
            if(string.IsNullOrEmpty(CurrentField.Name) || string.IsNullOrEmpty(CurrentField.Content))
            {
                Toaster.Error("Name and content required");
            }
            else
            {
                try
                {
                    var existing = DataService.CustomFields.Single(
                        f => f.AuthorId == Author.Id && f.Name == CurrentField.Name);

                    if (existing == null)
                    {
                        DataService.CustomFields.Add(CurrentField);
                        DataService.Complete();
                        CurrentField = new CustomField { AuthorId = Author.Id, Name = "", Content = "" };
                    }
                    Toaster.Success(Localizer["completed"]);
                }
                catch (Exception ex)
                {
                    Toaster.Error(ex.Message);
                }
            }
        }

        protected void RemoveField(int id)
        {
            var existing = DataService.CustomFields.Single(f => f.Id == id);
            if(existing != null)
            {
                DataService.CustomFields.Remove(existing);
                DataService.Complete();
                Toaster.Success(Localizer["completed"]);
                CurrentField = new CustomField { AuthorId = Author.Id, Name = "", Content = "" };
                UserFields = DataService.CustomFields.Find(f => f.AuthorId == Author.Id);
            }
            else
            {
                Toaster.Error($"Error removing field #{id}");
            }
        }

        protected async Task ChangePwd()
        {
            try
            {
                if (FeatureManager.IsEnabledAsync(nameof(AppFeatureFlags.Demo)).Result)
                {
                    Toaster.Error("Running in demo mode - change password disabled");
                }
                else
                {
                    var authState = await AuthenticationStateTask;
                    var user = await UserManager.GetUserAsync(authState.User);
                    var result = await UserManager.ChangePasswordAsync(user, PwdModel.OldPassword, PwdModel.NewPassword);

                    if (!result.Succeeded)
                    {
                        Toaster.Error("Error changing password");
                    }
                    else
                    {                     
                        Toaster.Success(Localizer["saved"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Toaster.Error(ex.Message);
            }
        }

        protected void ShowEditor()
        {
            Edit = true;
            StateHasChanged();
        }

        protected async Task HideEditor()
        {
            Edit = false;
            await Load();
            StateHasChanged();
        }
    }
}