$(document).ready(function () {
    renderSelectedImage($("#Image").val());
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

//AddTag
$("#addTag").click(function (e) {
    var tag = "";
    tag = $("#AddTag").val().replace(/\s{2,}/g, ' ');
    tag = tag.trim();
    var check = true;
    if (id != 0) {
        for (var a in tagInputResult) {
            if (tagInputResult[a] == tag) {
                check = false;
                break;
            }
        }
    }
    if (check) {
        $.ajax({
            method: "POST",
            url: "/Admin/CreateTag",
            data: { tag: tag },
            success: function (data) {

                tagInputResult[id] = data.Name;
                tagId[id] = data.TagId;

                var result = "<div id='parent" + tagId[id] + "'><a href='#' data-name=" + tagInputResult[id] + " onclick='removeTag(this)' class='removeTag' data-id='" + tagId[id] + "'><i class='fas fa-times-circle small text-danger mb-1'></i></a><span class='mr-2 badge badge-light border' value='" + tagId[id] + "'>" + tagInputResult[id] + "</span></div>";
                $("#renderTag").append(result);

                $("#Tag").val(tagId.join('|'));
                id++;
                $("#AddTag").val("");

            }
        });
    }
});

function removeTag(e) {
    tagInputResult.splice(tagInputResult.indexOf($(e).attr("data-name")), 1);
    tagId.splice(tagId.indexOf($(e).attr("data-id")), 1);
    $("#Tag").val(tagId.join('|'));
    id--;
    $("#parent" + $(e).attr("data-id")).remove();
};


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

$("#uploadImageList").click( function () {
    var imageList = $("#Image").val();
    loadLibraryImage("imageList", true, imageList);
});

$("#CoverName").click(function () {
    var cover = $(this).val();
    loadLibraryImage("imageCover", false, cover);
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
            imageList: listImage
        },
        success: function (data) {
            $("#renderImage").html(data);
            $("#Image").val(listImage);
        }
    });
}

function addCover() {
    var hasCover = false;
    $(".image-item").each(function () {
        if ($(this).attr("data-selected") == "True") {
            $("#CoverName").val($(this).attr("data-name"));
            $("#previewCover").attr("src", "../Content/Upload/Images/" + $(this).attr("data-name"));
            $("#removeCover").removeClass("d-none");
            hasCover = true;   
        }
    });
    if (!hasCover) {
        $("#CoverName").val(null);
        $("#previewCover").attr("src", "../Content/Upload/Images/No_Picture.JPG");
        $("#removeCover").addClass("d-none");
    }
}
$("#removeCover").click(function () {
    $("#previewCover").attr("src", "../Content/Upload/Images/No_Picture.jpg");
    $("#CoverName").val(null);
    $("#removeCover").addClass("d-none");
});

