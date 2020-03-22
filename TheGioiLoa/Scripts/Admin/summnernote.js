$(function () {
    // Summernote
    $('.textarea').summernote({
        height: 400,
        focus: true,
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['font', ['bold', 'underline', 'clear']],
            ['fontname', ['fontname']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['link', 'image', 'video']],
            ['view', ['fullscreen', 'codeview']],
        ],
        buttons: {
            image: uploadImageButton
        }
    });
});
var textAreaId = 0;
var uploadImageButton = function () {
    var id = textAreaId;
    var ui = $.summernote.ui;
    var button = ui.button({
        className: "imageTextarea_" + id,
        contents: '<i class="note-icon-picture"/>',
        tooltip: 'Upload hình ảnh',
        click: function () {
            $("#ModalTemplate").modal("show");
            loadLibraryImage("imageTextarea", false);
            $('.text-area-' + id).summernote('editor.saveRange');
        }
    });
    textAreaId++;
    return button.render();
}