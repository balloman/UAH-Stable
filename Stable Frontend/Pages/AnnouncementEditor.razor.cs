using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;


namespace Stable_Frontend.Pages
{
    public partial class AnnouncementEditor
    {
        private string strSavedContent = "";
        private ElementReference divEditorElement;
        private string EditorContent;
        private string EditorHTMLContent;
        private string Title;
        private bool[] Colleges = {false, false, false, false, false, false, false};
        private bool EditorEnabled = true;
    
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeAsync<string>("QuillFunctions.createQuill", divEditorElement);
            }
        }
    
    
        async Task GetText()
        {
            EditorHTMLContent = "";
            EditorContent = await JSRuntime.InvokeAsync<string>("QuillFunctions.getQuillText", divEditorElement);
        }
        async Task GetHTML()
        {
            EditorContent = "";
            EditorContent = await JSRuntime.InvokeAsync<string>("QuillFunctions.getQuillHTML", divEditorElement);
        }
        async Task GetEditorContent()
        {
            EditorHTMLContent = "";
            EditorContent = await JSRuntime.InvokeAsync<string>("QuillFunctions.getQuillContent", divEditorElement);
        }
        async Task SaveContent()
        {
            strSavedContent = await JSRuntime.InvokeAsync<string>("QuillFunctions.getQuillContent", divEditorElement);
        }
        async Task LoadContent()
        {
            var QuillDelta = await JSRuntime.InvokeAsync<object>("QuillFunctions.loadQuillContent", divEditorElement, strSavedContent);
        }
    
        async Task DisableQuillEditor()
        {
            EditorEnabled = false;
            await JSRuntime.InvokeAsync<object>("QuillFunctions.disableQuillEditor", divEditorElement);
        }
        private async Task SubmitContents()
        {
            await GetHTML();
            var postRef = await Handler.UploadNewPost("announcements", new Stable_Lib.Models.Announcement {
                Author = Handler.user.Uid,
                Body = EditorContent,
                LastModified = DateTime.Now,
                Title = Title,
                College = EvaluateColleges()
            });
            await Handler.AddPostToUser(Handler.user.Uid, postRef);
            NavigationManager.NavigateTo("announcement");
        }

        private List<string> EvaluateColleges()
        {
            var retList = new List<string>();
            var colleges = new List<string>
                {"AHSS", "Business", "Education", "Engineering", "Nursing", "Science", "Honors"};
            for (var i = 0; i < 7; i++) {
                if (Colleges[i]) {
                    retList.Add(colleges[i]);
                }
            }
            return retList;
        }
    }
}
