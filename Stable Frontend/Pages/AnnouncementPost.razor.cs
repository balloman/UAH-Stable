using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;


namespace Stable_Frontend.Pages
{
    public partial class AnnouncementPost
    {
        string Title = "This is the title";
        string myMarkup = "<p class='markup'>This is a <em>markup string</em>.</p>";
        string Author = "Eric Sung";
        string Date = "01:01:2021";

        [Parameter]
        public string Text { get; set; }

        public Stable_Lib.Models.Announcement Announcement { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Announcement = new Stable_Lib.Models.Announcement();
        }
    }
}
