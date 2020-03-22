function loadLibraryImage(target, IsMultiple, selectedImage, notLoadAll) {
    $("#modal-loading").show();
    var replaceElm = "#modalContent";
    if (notLoadAll == "notLoadAll")
        var replaceElm = "#libraryImageList";
    $.ajax({
        method: "POST",
        url: "/Admin/LoadLibraryImage",
        data: {
            target: target,
            IsMultiple: IsMultiple,
            selectedImage: selectedImage,
            notLoadAll: notLoadAll
        },
        success: function (data) {
            $(replaceElm).html(data);
            $("#modal-loading").hide();
        }
    });
}
//Select Multiple Images
function addImageToList(e) {
    $(e).children().removeClass("d-none");
    $(e).attr("onclick", "removeImageFromList(this)")
    $(e).attr("data-selected", "True");
}
function removeImageFromList(e) {
    $(e).children().addClass("d-none");
    $(e).attr("onclick", "addImageToList(this)")
    $(e).attr("data-selected", "False");
}
//Select 1 Image
function selectImage(e) {
    $(".image-item").attr("onclick", "selectImage(this)");
    $(".image-item").attr("data-selected", "False");
    $(".image-item").children().addClass("d-none");
    $(e).children().removeClass("d-none");
    $(e).attr("onclick", "removeSelectedImage(this)");
    $(e).attr("data-selected", "True");
}
function removeSelectedImage(e) {
    $(".image-item").children().addClass("d-none");
    $(".image-item").attr("onclick", "selectImage(this)");
    $(e).attr("data-selected", "False");
}

function addToTextArea() {
    $(".image-item").each(function () {
        if ($(this).attr("data-selected") == "True") {
            $('.textarea').summernote('editor.restoreRange');
            $('.textarea').summernote('editor.focus');
            $('.textarea').summernote('editor.insertImage', $(this).css('background-image').replace(/^url\(['"](.+)['"]\)/, '$1'));
            return false;
        }
    });
}

$(document).on("click", "#submitModalImage", function () {
    switch ($(this).attr("data-image-target")) {
        case "imageList":
            addImage();
            break;
        case "imageCover":
            addCover();
            break;
        case "imageTextarea":
            addToTextArea($(this).attr("data-textarea"));
            break;
        case "logo":
            addLogo();
            break;
        case "slider":
            addToSlider();
        default:
            break;
    }
    $("#ModalTemplate").modal("hide");
    $("#renderLibrary").html("");
});