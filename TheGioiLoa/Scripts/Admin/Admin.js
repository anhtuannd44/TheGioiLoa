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
    exitLoadingGif();
};

function loadingNoti(data) {
    switch (data) {
        case "successed":
            toastr.success('Thành công!');
            loadCategoryList();
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
    var elm = $(a);
    $.ajax({
        type: "POST",
        url: "/Admin/LoadEditPartial",
        data: { categoryId : elm.attr("data-id")},
        success: function (data) {
            $("#editCategoryContent").html(data);
        },
        error: function (data) {
            toastr.error("Không thể tải!");
        },
    });

};
function showModalRemoveCategory(a) {
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
    $("body").removeClass("modal-open").css("padding-right",'0')
};

function editCategorySuccess(data) {
    loadingNoti(data);
};
