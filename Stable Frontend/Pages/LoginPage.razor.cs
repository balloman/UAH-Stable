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
        private bool correct = false;
        private string error = "";
        public LoginPage()
        {

        }

        public async Task SubmitInfo()
        {
            var uid = await JSRuntime.InvokeAsync<string>("FirebaseFunctions.login", new[] {Email, Password});
            if (string.IsNullOrEmpty(uid)) {
                Console.WriteLine("Invalid Login...");
                error = "Invalid Login...";
                return;
            }
            var data = await Handler.GetUser(uid);
            Handler.Login(new User(data.ToDictionary()) {
                Uid = uid
            });
            NavigationManager.NavigateTo("announcement", true);
        }
    }
}
