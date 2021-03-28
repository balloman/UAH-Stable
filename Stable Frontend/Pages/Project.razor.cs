using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stable_Lib.Models;

namespace Stable_Frontend.Pages
{
    public partial class Project
    {
        public Dictionary<string, Stable_Lib.Models.Project> Results { get; set; }
        public Dictionary<string, string> Authors { get; set; }

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
            Results = new Dictionary<string, Stable_Lib.Models.Project>();
            Authors = new Dictionary<string, string>();
            Console.WriteLine("Attempting serverside grab");
            var task = await Handler.GetPostsAsync("projects", "created", true);
            foreach (var post in task) {
                var project = new Stable_Lib.Models.Project(post.ToDictionary());
                var author = await User.FromUid(project.Author);
                Results.Add(post.Id, project);
                Authors.Add(post.Id, author.Name);
            }
        }
        
        private void AddPetition()
        {
            //NavManager.NavigateTo("petitioneditor");
        }

        private void GoToPost(string id)
        {
            //NavManager.NavigateTo($"petitionost/{id}");
        }
    }
}