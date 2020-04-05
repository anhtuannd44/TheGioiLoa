$(document).ready(function () {
    $('#ProductForm').each(function () {
        if ($(this).data('validator'))
            $(this).data('validator').settings.ignore = ".note-editor *";
    });
})

$(".category-click").click(function (e) {
    if ($(this).is(":checked")) {
        if ($(this).attr("data-level") == "child") {
            $("#createProduct #" + $(this).attr("data-parent-id")).prop('checked', true);
        }
        if ($(this).attr("data-level") == "subChild") {
            var elm = $("#createProduct #" + $(this).attr("data-parent-id"));
            elm.prop('checked', true);
            $("#createProduct #" + elm.attr("data-parent-id")).prop('checked', true);
        }
    }
    else {
        if ($(this).attr("data-level") == "child") {
            $("#createProduct .childCategory-" + $(this).attr("id") + " .sub-child-category").prop('checked', false);
        }
        if ($(this).attr("data-level") == "parent") {
            $("#createProduct .parentCategory-" + $(this).attr("id") + " .child-category").prop('checked', false);
            $("#createProduct .parentCategory-" + $(this).attr("id") + " .sub-child-category").prop('checked', false);
        }
    }
});

$("#uploadImageList").click(function () {
    var imageList = $("#imageAlbum").val();
    loadLibraryImage("imageAlbum", true, imageList);
});

$("#ImageId").click(function () {
    var cover = $(this).val();
    loadLibraryImage("ImageId", false, cover);
});


function addImage() {
    var listImage = "";
    $(".image-item").each(function () {
        if ($(this).attr("data-selected") == "True") {
            listImage += $(this).attr("data-name") + ",";
        }
    });
    renderSelectedImage(listImage);
}

function renderSelectedImage(listImage) {
    $.ajax({
        method: "POST",
        url: "/Admin/LoadSelectImage",
        data: {
            imageAlbum: listImage
        },
        success: function (data) {
            $("#renderImage").html(data);
            $("#imageAlbum").val(listImage);
        }
    });
}

function addCover() {
    var hasCover = false;
    $(".image-item").each(function () {
        if ($(this).attr("data-selected") == "True") {
            $("#ImageId").val($(this).attr("data-name"));
            $("#previewCover").attr("src", "../Content/Upload/Images/" + $(this).attr("data-name"));
            $("#removeCover").removeClass("d-none");
            hasCover = true;
        }
    });
    if (!hasCover) {
        $("#ImageId").val(null);
        $("#previewCover").attr("src", "../Content/Upload/Images/No_Picture.JPG");
        $("#removeCover").addClass("d-none");
    }
}
$("#removeCover").click(function () {
    $("#previewCover").attr("src", "../Content/Upload/Images/No_Picture.jpg");
    $("#ImageId").val("No_Picture.JPG");
    $("#removeCover").addClass("d-none");
});

