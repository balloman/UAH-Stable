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
        private bool sorted = false;

        protected override async Task OnInitializedAsync()
        {
            await GetAnnouncements();
        }

        private async Task GetAnnouncements()
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
            Console.WriteLine("Sorting");
            var selectedColleges = new List<string>();
            var someCheck = false;
            for (var i = 0; i < Checked.Length; i++) {
                if (Checked[i]) {
                    selectedColleges.Add(Colleges[i]);
                    someCheck = true;
                }
            }
            
            if (sorted && !someCheck) {
                await GetAnnouncements();
                return;
            }
            
            if (!someCheck) {
                return;
            }
            var task = await Handler.PostsRef.Collection("announcements")
                .WhereArrayContainsAny("college", selectedColleges)
                .OrderByDescending("lastModified")
                .GetSnapshotAsync();
            Results = new Dictionary<string, Stable_Lib.Models.Announcement>();
            foreach (var post in task) {
                var announcement = new Stable_Lib.Models.Announcement();
                announcement.FromDict(post.ToDictionary());
                Results.Add(post.Id, announcement);
            }
            sorted = true;
            StateHasChanged();
        }
    }
}
