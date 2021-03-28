using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Stable_Lib.Models;


namespace Stable_Frontend.Pages
{
    public partial class AnnouncementPost
    {
        // string Title = "This is the title";
        // string myMarkup = "<p class='markup'>This is a <em>markup string</em>.</p>";
        // string Author = "Eric Sung";
        // string Date = "01:01:2021";

        [Parameter]
        public string Id { get; set; }
        public string AuthorName { get; set; }

        public Stable_Lib.Models.Announcement Announcement = new Stable_Lib.Models.Announcement();

        protected override async Task OnInitializedAsync()
        {
            var task = await Handler.GetPost("announcements", Id);
            Announcement = new Stable_Lib.Models.Announcement(task.ToDictionary());
            var task2 = await User.FromUid(Announcement.Author);
            AuthorName = task2.Name;
            Console.WriteLine(Announcement.ToString());
        }

        private void MoveBack()
        {
            NavigationManager.NavigateTo("announcement");
        }

    }
}
