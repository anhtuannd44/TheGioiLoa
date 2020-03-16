
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

$(function () {
    // Summernote
    $('.textarea').summernote({
        height: 250,
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
        callbacks: {
            onImageUpload: function (files) {
                for (let i = 0; i < files.length; i++) {
                    uploadImage(files[i], $(this).attr("id"));
                }
            },
            onMediaDelete: function (file) {
                var image = $(file).attr("src");
                var lastIndex = image.lastIndexOf('/') + 1;
                var imageName = image.slice(lastIndex);
                deleteImage(imageName);
            }
        },
        buttons: {
            image: uploadImageButton
        }
    });
    function uploadImage(file, id) {
        var url = '/Admin/UploadImageSummerNote';
        formData = new FormData();
        formData.append("aUploadedFile", file);
        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            success: function (FileUrl) {
                var imgNode = document.createElement('img');
                imgNode.src = FileUrl;
                $("#" + id).summernote('insertNode', imgNode);
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
    function deleteImage(name) {
        $.ajax({
            type: 'POST',
            url: '/Admin/DeleteImageSummerNote',
            data: { imageName: name },
            cache: false,
            success: function (data) {
                if (data.status == "success")
                    toastr.success(data.message);
                else
                    toastr.error(data.message);
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
});

$("#Status").change(function () {
    if ($(this).val() == "4") {
        $("#deleteNoti").removeClass("d-none");
        $("#submitFormButton").html("Xóa");
    }
    else {
        $("#deleteNoti").addClass("d-none");
        $("#submitFormButton").html("Đăng");
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
                console.log(tagInputResult, tagId, id, $("#Tag").val());
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

    console.log(tagId, tagInputResult, id, $("#Tag").val());
};

$(function () {
    $('#drag-and-drop-zone').dmUploader({
        url: '/Admin/UploadImage',
        maxFileSize: 3000000, // 3 Megs
        onDragEnter: function () {
            // Happens when dragging something over the DnD area
            this.addClass('active');
        },
        onDragLeave: function () {
            // Happens when dragging something OUT of the DnD area
            this.removeClass('active');
        },
        onInit: function () {
            // Plugin is ready to use
            ui_add_log('Penguin initialized :)', 'info');
        },
        onComplete: function () {
            // All files in the queue are processed (success or error)
            ui_add_log('All pending tranfers finished');
        },
        onNewFile: function (id, file) {
            // When a new file is added using the file selector or the DnD area
            ui_add_log('New file added #' + id);
            ui_multi_add_file(id, file);
        },
        onBeforeUpload: function (id) {
            // about tho start uploading a file
            ui_add_log('Starting the upload of #' + id);
            ui_multi_update_file_status(id, 'uploading', 'Đang đăng ...');
            ui_multi_update_file_progress(id, 0, '', true);
        },
        onUploadCanceled: function (id) {
            // Happens when a file is directly canceled by the user.
            ui_multi_update_file_status(id, 'warning', 'Canceled by User');
            ui_multi_update_file_progress(id, 0, 'warning', false);
        },
        onUploadProgress: function (id, percent) {
            // Updating file progress
            ui_multi_update_file_progress(id, percent);
        },
        onUploadSuccess: function (id) {
            // A file was successfully uploaded
            ui_multi_update_file_status(id, 'success', 'Thành công');
            ui_multi_update_file_progress(id, 100, 'success', false);
            loadLibraryImage(true);
        },
        onUploadError: function (id, xhr, status, message) {
            ui_multi_update_file_status(id, 'danger', message);
            ui_multi_update_file_progress(id, 0, 'danger', false);
        },
        onFileSizeError: function (file) {
            ui_add_log('Hình \'' + file.name + '\' không thể đăng: Vượt quá dung lượng cho phép', 'danger');
        }
    });
});

function ui_add_log(message, color) {
    var d = new Date();

    var dateString = (('0' + d.getHours())).slice(-2) + ':' +
        (('0' + d.getMinutes())).slice(-2) + ':' +
        (('0' + d.getSeconds())).slice(-2);

    color = (typeof color === 'undefined' ? 'muted' : color);

    var template = $('#debug-template').text();
    template = template.replace('%%date%%', dateString);
    template = template.replace('%%message%%', message);
    template = template.replace('%%color%%', color);

    $('#debug').find('li.empty').fadeOut(); // remove the 'no messages yet'
    $('#debug').prepend(template);
}

// Creates a new file and add it to our list
function ui_multi_add_file(id, file) {
    var template = $('#files-template').text();
    template = template.replace('%%filename%%', file.name);

    template = $(template);
    template.prop('id', 'uploaderFile' + id);
    template.data('file-id', id);

    $('#files').find('li.empty').fadeOut(); // remove the 'no files yet'
    $('#files').prepend(template);
}

// Changes the status messages on our list
function ui_multi_update_file_status(id, status, message) {
    $('#uploaderFile' + id).find('span').html(message).prop('class', 'status text-' + status);
}

// Updates a file progress, depending on the parameters it may animate it or change the color.
function ui_multi_update_file_progress(id, percent, color, active) {
    color = (typeof color === 'undefined' ? false : color);
    active = (typeof active === 'undefined' ? true : active);

    var bar = $('#uploaderFile' + id).find('div.progress-bar');

    bar.width(percent + '%').attr('aria-valuenow', percent);
    bar.toggleClass('progress-bar-striped progress-bar-animated', active);

    if (percent === 0) {
        bar.html('');
    } else {
        bar.html(percent + '%');
    }

    if (color !== false) {
        bar.removeClass('bg-success bg-info bg-warning bg-danger');
        bar.addClass('bg-' + color);
    }
}

function loadLibraryImage(IsMultiple, imageList, selectedImage) {
    $.ajax({
        method: "POST",
        url: "/Admin/LoadLibraryImage",
        data: {
            IsMultiple: IsMultiple,
            imageList: imageList,
            selectedImage: selectedImage
        },
        success: function (data) {
            $("#renderLibrary").html(data);
        }
    });
}


$("#uploadImageList").click(function () {
    var imageList = $("#Image").val();
    loadLibraryImage(true, imageList, null);
});

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

function addImage() {
    var listImage = "";
    $(".image-item").each(function () {
        if ($(this).attr("data-selected") == "True") {
            listImage += $(this).attr("data-name") + ",";
        }
    });

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
    $("#uploadImage").modal("hide");
}


$("#Cover").click(function () {
    var cover = $(this).val();
    loadLibraryImage(false, null, cover);
});
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
$("#removeCover").click(function () {
    $("#previewCover").attr("src", "../Content/Upload/Images/No_Picture.jpg");
    $("#Cover").val(null);
    $("#removeCover").addClass("d-none");
});

$(document).on("click", "#submitModalImage", function () {
    if ($(this).attr("data-multiple") == "True")
        addImage();
    else {
        addCover();
    }
});

function addCover() {
    $("#uploadImage").modal("hide");
    var hasCover = false;
    $(".image-item").each(function () {
        if ($(this).attr("data-selected") == "True") {
            $("#Cover").val($(this).attr("data-name"));
            $("#previewCover").attr("src", "../Content/Upload/Images/" + $(this).attr("data-name"));
            $("#removeCover").removeClass("d-none");
            hasCover = true;
        }
    });
    if (!hasCover) {
        $("#Cover").val(null);
        $("#previewCover").attr("src", "../Content/Upload/Images/No_Picture.JPG");
        $("#removeCover").addClass("d-none");
    }
}

var uploadImageButton = function (context) {
    var ui = $.summernote.ui;
    var button = ui.button({
        contents: '<i class="fas fa-image"/>',
        tooltip: 'Upload hình ảnh',
        click: function () {
            $("#uploadImage").modal("show");
            loadLibraryImage(false);
        }
    });
    return button.render();
}