using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Stable_Lib.Handlers;
using Stable_Lib.Models;


namespace Stable_Frontend.Pages
{
    public partial class LoginPage
    {
        string Email;
        string Password;
        public LoginPage()
        {

        }

        public async Task SubmitInfo()
        {
            var uid = await JSRuntime.InvokeAsync<string>("FirebaseFunctions.login", new[] {Email, Password});
            var data = await Handler.GetUser(uid);
            Handler.Login(new User(data.ToDictionary()));
            NavigationManager.NavigateTo("announcement");
        }
    }
}
