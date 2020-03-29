$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
})

$(document).on("click",".is-show-menu", function () {
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

$(document).on("click",".edit-category",function () {
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
$(document).on("click",".delete-category", function () {
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
