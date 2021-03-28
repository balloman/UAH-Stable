using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stable_Frontend.Pages
{
    public partial class Petition
    {
        public Dictionary<string, Stable_Lib.Models.Petition> Results { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadPetitions();
        }

        protected override async Task OnParametersSetAsync()
        {
            await LoadPetitions();
        }
        private async Task LoadPetitions()
        {
            Results = new Dictionary<string, Stable_Lib.Models.Petition>();
            Console.WriteLine("Attemtpting serverside grab");
            var task = await Handler.GetPostsAsync("petitions", "created", true);
            foreach (var post in task) {
                var petition = new Stable_Lib.Models.Petition(post.ToDictionary());
                Results.Add(post.Id, petition);
            }
        }
        
        private void AddPetition()
        {
            NavManager.NavigateTo("petitioneditor");
        }

        private void GoToPost(string id)
        {
            NavManager.NavigateTo($"petitionost/{id}");
        }
    }
}