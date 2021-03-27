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

        protected override async Task OnInitializedAsync()
        {
            Results = new Dictionary<string, Stable_Lib.Models.Announcement>();
            Console.WriteLine("Attemtping serverside grab");
            var task = await Handler.GetPostsAsync("announcements");
            foreach (var post in task)
            {
                var announcement = new Stable_Lib.Models.Announcement();
                announcement.FromDict(post.ToDictionary());
                Results.Add(post.Id, announcement);
            }
        }

        private void AddPost()
        {
            NavManager.NavigateTo("announcement editor");
        }

        private void GoToPost(string id)
        {
            NavManager.NavigateTo($"post/{id}");
        }
    }
}
