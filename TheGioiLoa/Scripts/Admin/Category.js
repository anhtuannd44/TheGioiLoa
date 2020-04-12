$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
})

$(document).on("click", ".is-show-menu", function () {
    var elm = $(this);
    var categoryId = elm.attr("data-id");
    if (!$("#isShowMenu_" + categoryId).is(':checked'))
        $("#isShowMenu_" + categoryId).val("True");
    else
        $("#isShowMenu_" + categoryId).val("False");
    $.ajax({
        method: "POST",
        url: "/Admin/UpdateMenuCategory",
        data: {
            categoryId: categoryId,
            IsShowMenu: $("#isShowMenu_" + categoryId).val()
        },
        success: function (data) {
            if (data.status == "success")
                toastr.success(data.message);
            else
                toastr.error(data.message);
        }
    })
})
function loadCategoryList() {
    loadingSpinner('#loadingGif-CategoryList');
    $.ajax({
        type: "POST",
        url: "/Admin/LoadCategoryList",
        success: function (data) {
            $("#categoryList").html(data);
        },
        error: function (data) {
            toastr.error("Không thể tải dữ liệu Danh mục sản phẩm!");
        },
    });
    exitSpinner('#loadingGif-CategoryList');
};

function loadSelectCategory() {
    $.ajax({
        type: "POST",
        url: "/Admin/LoadCategorySelect",
        success: function (data) {
            $("#CategoryParentId").html(data);
        },
        error: function () {
            toastr.error("Không thể tải dữ liệu Danh mục sản phẩm!");
        }
    });
};
function createOrEditCategorySuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        loadCategoryList();
        loadSelectCategory();
    }
    else {
        toastr.error(data.message);
    }
    $("#ModalTemplateNormal").modal("hide");
}

$(document).on("click", ".edit-category", function () {
    var elm = $(this);
    $.ajax({
        type: "POST",
        url: "/Admin/LoadEditCategoryPartial",
        data: { categoryId: elm.data("id") },
        success: function (data) {
            $("#modalContentNormal").html(data);
        },
        error: function () {
            toastr.error("Không thể tải!");
        },
    });
})
$(document).on("click", ".delete-category", function () {
    var elm = $(this);
    var r = confirm("Bạn có muốn xóa danh mục " + elm.data("name") + "' không?");
    if (r == true) {
        $.ajax({
            method: "POST",
            url: "/Admin/RemoveCategory",
            data: { CategoryId: elm.data("id") },
            success: function (data) {
                if (data.status == "success") {
                    toastr.success(data.message);
                    loadCategoryList();
                    loadSelectCategory();
                }
            }
        })
    }
})

//Slider
$(document).on("click", "#addImageToSlider", function () {
    loadingSpinner("#modalNormal-loading");
    $.ajax({
        method: "POST",
        url: "/Admin/LoadCreateSliderModal",
        data: { type: 2 },
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
    loadingSpinner("#loadingGif-shoppingSlider");
    $.ajax({
        method: "POST",
        url: "/Admin/LoadEditSlider",
        data: { type: 2 },
        success: function (data) {
            $("#shoppingSlider").html(data);
            exitSpinner("#loadingGif-shoppingSlider");
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

//
$(document).on("click", "#addPromotionImage", function () {
    loadLibraryImage("promotionImage", false, null);
});
function addPromotionImage() {
    $(".image-item").each(function () {
        if ($(this).attr("data-selected") == "True") {
            var imageId = $(this).data("name"); 
            $.ajax({
                method: "POST",
                url: "/Admin/AddPromotionImage",
                data: { imageId: imageId },
                success: function (data) {
                    if (data.status == "success") {
                        toastr.success(data.message);
                        loadingSpinner("#loadingGif-promotionImage");
                        reloadPromotionImage();
                    }
                    else
                        toastr.error(data.message);
                }
            });
        }
    });
}
function reloadPromotionImage() {
    $.ajax({
        method: "POST",
        url: "/Admin/LoadPromotionImage",
        success: function (data) {
            $("#promotionImage").html(data);
            exitSpinner("#loadingGif-promotionImage");
        }
    });
}

$(document).on("click", ".delete-promotion-image", function () {
    var imageId = $(this).data("value");
    var r = confirm("Bạn có muốn xóa hình này không?");
    if (r == true) {
        $.ajax({
            method: "POST",
            url: "/Admin/DeletePromotionImage",
            data: { imageId: imageId },
            success: function (data) {
                if (data.status == "success") {
                    toastr.success(data.message);
                    reloadPromotionImage();
                }
                else
                    toastr.error(data.message)
            }
        })
    }
})