using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stable_Lib.Handlers;

namespace Stable_Frontend.Pages
{
    public partial class Announcement
    {
        public Dictionary<string, Stable_Lib.Models.Announcement> Results { get; set; }
        public bool[] Checked = {false, false, false, false, false, false, false};
        private string[] Colleges = {"AHSS", "Business", "Education", "Engineering", "Nursing", "Science", "Honors"};

        protected override async Task OnInitializedAsync()
        {
            Results = new Dictionary<string, Stable_Lib.Models.Announcement>();
            Console.WriteLine("Attemtping serverside grab");
            var task = await Handler.GetPostsAsync("announcements", "lastModified", true);
            foreach (var post in task)
            {
                var announcement = new Stable_Lib.Models.Announcement();
                announcement.FromDict(post.ToDictionary());
                Results.Add(post.Id, announcement);
            }
        }

        private void AddPost()
        {
            NavManager.NavigateTo("announcementeditor");
        }

        private void GoToPost(string id)
        {
            NavManager.NavigateTo($"post/{id}");
        }

        private async Task Sort()
        {
            
        }
    }
}
