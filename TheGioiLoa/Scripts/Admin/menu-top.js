function addMenuSuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        reloadMenuTopList();
    }
    else {
        toastr.error(data.message);
    }
}

$(document).on("click", ".edit-menu-top", function () {
    $("#modalNormal-loading").show();
    $("#ModalTemplateNormal").modal("show");
    var e = $(this);
    $.ajax({
        method: "POST",
        url: "/Admin/LoadEditMenu",
        data: {
            menuId: e.data("id")
        },
        success: function (data) {
            $("#modalContentNormal").html(data);
            $("#modalNormal-loading").hide();
        }
    })
})
function editMenuSuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        $("#ModalTemplateNormal").modal("hide");
        reloadMenuTopList();
    }
    else {
        toastr.error(data.message);
        $("#modalNormal-loading").hide();
    }
}

$(document).on("click", ".delete-menu-top", function () {
    var menuId = $(this).data("id");
    var r = confirm("Bạn có muốn xóa menu '" + $(this).attr("data-name") + "' này không?");
    if (r == true) {
        $.ajax({
            method: "POST",
            url: "/Admin/DeleteMenu",
            data: { menuId: menuId },
            success: function (data) {
                if (data.status == "success") {
                    toastr.success(data.message);
                    reloadMenuTopList();
                }
            }
        })
    }
})

function reloadMenuTopList() {
    loadingSpinner("#menuTopListLoading");
    $.ajax({
        method: "GET",
        url: "/Admin/LoadMenuList",
        data: {
            type: 0
        },
        success: function (data) {
            $("#menuTopList").html(data);
            exitSpinner("#menuTopListLoading");
        }
    })
}