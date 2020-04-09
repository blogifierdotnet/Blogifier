using Blogifier.Core;
using Blogifier.Core.Data;
using Blogifier.Core.Data.Models;
using Blogifier.Core.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Sotsera.Blazor.Toaster;
using System;
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

        protected Author Author { get; set; }
        protected bool Edit { get; set; }
        
        protected ChangePasswordModel PwdModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        public async Task Load()
        {
            var authState = await AuthenticationStateTask;

            Author = await DataService.Authors.GetItem(
                a => a.AppUserName == authState.User.Identity.Name);

            PwdModel = new ChangePasswordModel {
                UserName = authState.User.Identity.Name
            };
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

        protected async Task ChangePwd()
        {
            try
            {
                if (AppSettings.DemoMode)
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