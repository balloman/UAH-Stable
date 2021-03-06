using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;


namespace Stable_Frontend.Pages
{
    public partial class PetitionEditor
    {
        private string strSavedContent = "";
        private ElementReference divEditorElement;
        private string EditorContent;
        private string EditorHTMLContent;
        private string Title;
        private string Category;
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
            var postRef = await Handler.UploadNewPost("petitions", new Stable_Lib.Models.Petition()
            {
                Author = Handler.user.Uid,
                Body = EditorContent,
                Created = DateTime.Now,
                Title = Title,
                Category = Category
            });
            await Handler.AddPostToUser(Handler.user.Uid, postRef);
            NavigationManager.NavigateTo("petition");
        }
        
    }
}

