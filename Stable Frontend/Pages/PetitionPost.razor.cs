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
        private int Votes;
        private int counter = 0;

        protected override async Task OnInitializedAsync()
        {
            var task = await Handler.GetPost("petitions", Id);
            Petition = new Stable_Lib.Models.Petition(task.ToDictionary());
            var task2 = await User.FromUid(Petition.Author);
            AuthorName = task2.Name;
            Votes = Petition.Votes;
            Console.WriteLine(Petition.ToString());
            counter = 0;
        }

        
        public void UpVote()
        {
            if (counter == 0)
            {
                Votes++;
                Handler.SetPetitionVotes(Id, Votes);
            }
            counter++;
        }
        public void DownVote()
        {
            if (counter == 0)
            {
                Votes--;
                Handler.SetPetitionVotes(Id, Votes);
            }
            counter++;
        }
    }
}
