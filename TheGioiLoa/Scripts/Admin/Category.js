var isParentCheckBox = function (a) {
    var idShow = "#categoryParent" + a;
    if ($(".IsCategoryParent"+a).is(":checked")) {
        $(idShow).removeClass("d-none");
    }
    else {
        $(idShow).addClass("d-none");
    }
};

function loadingGift() {
    $("#loadingGift").css("z-index", "9999");
    $("#loadingGift").css("opacity", "1");
};
function extiLoadingGift() {
    $("#loadingGift").css("z-index", "-1");
    $("#loadingGift").css("opacity", "0");
}
function loadingNoti() {
    $("#bgNotification").removeClass();
    switch ($("#notificationContent").val()) {
        case "successed":
            toastr.success('Thành công!');
            break;
        case "error":
            toastr.error('Thất bại! Vui lòng kiểm tra và thử lại!');
            break;
        case "existed":
            toastr.error('Thất bại! Dữ liệu đã tồn tại');
            break;
        default:
            toastr.warning('Có lỗi xảy ra, vui lòng thử lại!');
    };
};
function showModalEditCategory(a) {
    var elm = $(a);
    $("editCategoryContent #CategoryId").val(elm.attr("data-id"));
    $("#editCategoryContent #Name").val(elm.attr("data-name"));
    if (elm.attr("data-parent-id") != 'null') {
        $('#categoryParentEdit > select').val(elm.attr("data-parent-id")).change();
        $('#IsCategoryParentEdit').prop('checked', true);
        $('#categoryParentEdit').removeClass("d-none");
    }
    else {
        $('#IsCategoryParentEdit').prop('checked', false);
        $('#categoryParentEdit').addClass("d-none");
    }
    $("#loadingModal").removeClass().addClass("d-none");
  
    //$.ajax({
    //    type: "POST",
    //    url: "/Admin/GetEditCategoryForm",
    //    data: {"categoryId":$(a).attr("data-id")},
    //    success: function (data) {
    //        $("#editCategoryContent").html(data);
    //        $("#loadingModal").removeClass().addClass("d-none");
    //    },
    //    error: function (data) {
    //        toastr.error("Thất bại! Có lỗi xảy ra, vui lòng thử lại!");
    //    },
    //});
};

$("#submitForm").click( function() {
    var frm = $('#formModal');
    frm.submit(function (e) {

        e.preventDefault();

        $.ajax({
            type: frm.attr('method'),
            url: frm.attr('action'),
            data: frm.serialize(),
            success: function (data) {
                console.log('Submission was successful.');
                console.log(data);
            },
            error: function (data) {
                toastr.error("Thất bại! Có lỗi xảy ra, vui lòng thử lại!");
            },
        });
    });
});

function showBgOverlay() {
    $("#loadingModal").addClass("overlay d-flex justify-content-center align-items-center");
};
function closeModal() {
    $("#editCategoryModal").modal('hide');
};