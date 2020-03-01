﻿$(document).ready(function () {
    loadCategoryList();
});
function loadCategoryList() {
    loadingGif();
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
    extiLoadingGif();
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
function loadingGif() {
    $("#loadingGift").css("z-index", "9999");
    $("#loadingGift").css("opacity", "1");
};
function extiLoadingGif() {
    $("#loadingGif").css("z-index", "-1");
    $("#loadingGif").css("opacity", "0");
}
function loadingNoti(data) {
    switch (data) {
        case "successed":
            toastr.success('Thành công!');
            loadCategoryList();
            loadSelectCategory();
            break;
        case "error":
            toastr.error('Thất bại! Vui lòng kiểm tra và thử lại!');
            break;
        case "existed":
            toastr.error('Thất bại! Dữ liệu đã tồn tại');
            break;
        case "editfaild":
            toastr.error('Thất bại! Danh mục chưa được chính sửa!');
            break;
        case "empty":
            toastr.error('Thất bại! Tên danh mục không được để trống!');
            break;
        default:
            toastr.warning('Có lỗi xảy ra, vui lòng thử lại!');
    };
};
function showModalEditCategory(a) {
    $("#editCategoryModal").modal('show');
    var elm = $(a);
    $.ajax({
        type: "POST",
        url: "/Admin/LoadEditPartial",
        data: { categoryId: elm.attr("data-id") },
        success: function (data) {
            $("#editCategoryContent").html(data);
        },
        error: function (data) {
            toastr.error("Không thể tải!");
        },
    });


    //if (elm.attr("data-parent-id") == "") {
    //    $("#categoryParentEdit").addClass("d-none");
    //}
    //else {
    //    $("#categoryParentEdit").removeClass("d-none");
    //}
    //$('#categoryParentEdit > select').val(elm.attr("data-parent-id")).change();
    //$("#loadingEditModal").removeClass().addClass("d-none");

};
function showModalRemoveCategory(a) {
    $("#removeCategoryModal").modal('show');
    var elm = $(a);
    var delValue = (elm.attr("data-id"));
    $("#removeCategoryContent #CategoryId").val(delValue);
    $("#CategoryTitle").html(elm.attr("data-name"));
    $("#loadingRemoveModal").removeClass().addClass("d-none");
};
function showBgOverlay() {
    $("#loadingModal").addClass("overlay d-flex justify-content-center align-items-center");
};
function closeModal() {
    $("#editCategoryModal").modal('hide');
    $(".modal-backdrop").remove();
    $("body").removeClass("modal-open").css("padding-right", '0')
};

function editCategorySuccess(data) {
    loadingNoti(data);
};