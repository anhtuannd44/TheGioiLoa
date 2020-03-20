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

function loadLibraryImage(target, IsMultiple, selectedImage) {
    $("#modal-loading").show();
    $.ajax({
        method: "POST",
        url: "/Admin/LoadLibraryImage",
        data: {
            IsMultiple: IsMultiple,
            selectedImage: selectedImage
        },
        success: function (data) {
            $("#modalContent").html(data);
            $("#submitModalImage").attr("data-image-target", target);
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
        default:
            break;
    }
    $("#ModalTemplate").modal("hide");
    $("#renderLibrary").html("");
});