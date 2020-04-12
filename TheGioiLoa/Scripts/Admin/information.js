//Logo
$(document).on("click", "#selectLogo", function () {
    loadLibraryImage("logo", false, $("#Logo").val());
})
function addLogo() {
    var hasLogo = false;
    $(".image-item").each(function () {
        if ($(this).attr("data-selected") == "True") {
            $("#Logo").val($(this).attr("data-name"));
            $("#previewLogo").attr("src", "../Content/Upload/Images/" + $(this).attr("data-name"));
            $("#removeLogo").removeClass("d-none");
            hasLogo = true;
        }
    });
    if (!hasLogo) {
        $("#Logo").val("");
        $("#previewLogo").attr("src", "../Content/Upload/Images/No_Picture.JPG");
        $("#removeLogo").addClass("d-none");
    }
}
$(document).on("click", "#removeLogo", function () {
    $("#previewLogo").attr("src", "../Content/Upload/Images/No_Picture.jpg");
    $("#Logo").val(null);
    $("#removeLogo").addClass("d-none");
});
function updateLogoSuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        reloadLogoPartial();
    }
    else {
        toastr.error(data.message);
    }
}
function reloadLogoPartial() {
    $.ajax({
        method: "POST",
        url: "/Admin/LoadEditLogo",
        success: function (data) {
            $("#renderLogoPartial").html(data);
        }
    })
}

//Contact
function updateContactSuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        reloadContactPartial();
    }
    else {
        toastr.error(data.message);
    }
}
function reloadContactPartial() {
    $.ajax({
        method: "POST",
        url: "/Admin/LoadEditContact",
        success: function (data) {
            $("#renderContactPartial").html(data);
        }
    })
}
//Social Network
function updateSocialSuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        reloadSocialPartial();
    }
    else {
        toastr.error(data.message);
    }
}
function reloadSocialPartial() {
    $.ajax({
        method: "POST",
        url: "/Admin/LoadEditSocial",
        success: function (data) {
            $("#renderMenuFooterPartial").html(data);
        }
    })
}

//Menu Footer
$(document).on("click", "#addMenu", function () {
    var e = $(this);
    id = e.data("footer");
    loadingSpinner("#modalNormal-loading");
    $.ajax({
        method: "POST",
        url: e.data("url"),
        data: {
            type: e.data("footer")
        },
        success: function (data) {
            $("#modalContentNormal").html(data);
            exitSpinner("#modalNormal-loading");
        }
    })
})
$(document).on("click", ".edit-menu", function () {
    loadingSpinner("#modalNormal-loading");
    $.ajax({
        method: "POST",
        url: $(this).data("url"),
        data: {
            menuId: $(this).data("id")
        },
        success: function (data) {
            $("#modalContentNormal").html(data);
            exitSpinner("#modalNormal-loading");
        }
    })
})
$(document).on("click", ".delete-menu", function () {
    var menuId = $(this).data("id");
    var type = $(this).data("type");
    var r = confirm("Bạn có muốn xóa menu '" + $(this).attr("data-name") + "' này không?");
    if (r == true) {
        $.ajax({
            method: "POST",
            url: "/Admin/DeleteMenu",
            data: { menuId: menuId },
            success: function (data) {
                if (data.status == "success") {
                    toastr.success(data.message);
                    reloadFooter(type);
                }
            }
        })
    }
})
function FooterSuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        reloadFooter($("#Type").val());
    }
    else {
        toastr.error(data.message);
    }
    $("#ModalTemplateNormal").modal("hide");
}
function reloadFooter(footerId) {
    loadingSpinner("#updateMenuFooter" + footerId + "-Loading");
    $.ajax({
        method: "POST",
        url: "/Admin/LoadMenuFooter",
        data: {
            type: footerId
        },
        success: function (data) {
            $("#renderMenuFooter" + footerId).html(data);
            exitSpinner("#updateMenuFooter" + footerId + "-Loading");
        }
    })
}

//Slider
$(document).on("click", "#addImageToSlider", function () {
    loadingSpinner("#modalNormal-loading");
    $.ajax({
        method: "POST",
        url: "/Admin/LoadCreateSliderModal",
        data: { type: 1 },
        success: function (data) {
            $("#modalContentNormal").html(data);
            exitSpinner("#modalNormal-loading");
        }
    })

})
$(document).on("click", "#ImageIdLabel", function () {
    loadingSpinner("#modal-loading");
    $("#ModalTemplate").modal("show");
    loadLibraryImage("slider", false);
})

function addToSlider() {
    $(".image-item").each(function () {
        if ($(this).attr("data-selected") == "True") {
            $("#ImageId").val($(this).attr("data-name"));
            $("#previewImageSlider").attr("src", "../Content/Upload/Images/" + $(this).attr("data-name"));
        }
    });
}

function addImageToSliderSuccess(data) {
    $("#ModalTemplateNormal").modal("hide");
    if (data.status == "success") {
        toastr.success(data.message);
        reloadSlider();
    }
    else {
        toastr.error(data.message);
    }
}
function reloadSlider() {
    loadingSpinner("#updateSlider-Loading");
    $.ajax({
        method: "POST",
        url: "/Admin/LoadEditSlider",
        data: { type: 1 },
        success: function (data) {
            $("#renderEditSlider").html(data);
            exitSpinner("#updateSlider-Loading");
        }
    })
}
$(document).on("click", ".delete-slider", function () {
    var sliderId = $(this).data("value");
    var r = confirm("Bạn có muốn xóa slider này không?");
    if (r == true) {
        $.ajax({
            method: "POST",
            url: "/Admin/DeleteSlider",
            data: { sliderId: sliderId },
            success: function (data) {
                if (data.status == "success") {
                    toastr.success(data.message);
                    reloadSlider();
                }
                else
                    toastr.error(data.message)
            }
        })
    }
})

//ProductHomePage
function updateProductHomePageSuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        reloadProductHomePage();
    }
    else {
        toastr.error(data.message);
    }
}
function reloadProductHomePage() {
    $.ajax({
        method: "POST",
        url: "/Admin/LoadProductHomePage",
        success: function (data) {
        }
    })
}

//Footer COntact
function updateFooterContactSuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        location.reload();
    }
    else {
        toastr.error(data.message);
    }
}
