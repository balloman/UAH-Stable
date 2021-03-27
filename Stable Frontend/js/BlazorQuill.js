(function () {
    window.QuillFunctions = {
        createQuill: function (quillElement) {
            var options = {
                dbug: 'info',
                modules: {
                    toolbar: '#toolbar'
                },
                placeholder: 'Type Here',
                readOnly: false,
                theme: 'snow'
            };
            new Quill(quillElement, options);
        },
        getQuillContent: function (quillControl) {
            return JSON.stringify(quillControl.__quill.getContents());
        },
        getQuillText: function (quillControl) {
            return quillControl.__quill.getText();
        },
        getQuillHTML: function (quillControl) {
            return quillControl.__quill.root.innerHTML();
        },
        loadQuillContent: function (quillControl, quillContent) {
            content = JSON.parse(quillContent);
            return quillControl.__quill.setConents(content, 'api');
        },
    }
})();
