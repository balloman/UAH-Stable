using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Stable_Lib.Models;


namespace Stable_Frontend.Pages
{
    public partial class PetitionPost
    {
        [Parameter]
        public string Id { get; set; }
        public string AuthorName { get; set; }
        public Stable_Lib.Models.Petition Petition = new Stable_Lib.Models.Petition();

        protected override async Task OnInitializedAsync()
        {
            var task = await Handler.GetPost("petitions", Id);
            Petition = new Stable_Lib.Models.Petition(task.ToDictionary());
            var task2 = await User.FromUid(Petition.Author);
            AuthorName = task2.Name;
            Console.WriteLine(Petition.ToString());
        }
    }
}
