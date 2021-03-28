using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;


namespace Stable_Frontend.Pages
{
    public partial class RegisterPage
    {

        string Name;
        string Email;
        string Password;
        public RegisterPage()
        {

        }

        public async Task SubmitInfo()
        {
            var uid = await JSRuntime.InvokeAsync<string>("FirebaseFunctions.signup", new[] {Email, Password});
            
        }
    }
}
