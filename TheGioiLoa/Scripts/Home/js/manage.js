function LoadingGifRightCard() {
    $("#LoadingGifRightCard").show();
}
function UpdateInformationNoti(data) {
    if (data.status == "success") {
        toastr.success(data.message);
    }
    else {
        toastr.error(data.message);
    }
    $("#LoadingGifRightCard").hide();
}
function reloadRightCard(partial) {
    $.ajax({
        method: "GET",
        url: "/Manage/Area/",
        data: { partial: partial },
        success: function (data) {
            $("#renderData").html(data);
            $("#LoadingGifRightCard").hide();
        }
    })
}

var active = "@Model.PartialView";

$(document).ready(function () {
    $(".menu-left-sub-ul .partial-view").each(function () {
        if ($(this).data("partial") == active) {
            $(this).addClass("active");
            return false;
        }
    })
})

$(function () {
    $('#BirthDayInput').datetimepicker({
        format: "L",
        locale: moment.locale("vi")
    })
});
$(document).on("click", ".order-details", function () {
    var elm = $(this);
    $("#modal-loading").show();
    $.ajax({
        method: "POST",
        url: "/Manage/LoadOrderDetails",
        data: { orderId : elm.data("id") },
        success: function (data) {
            $("#modalContent").html(data);
            $("#modal-loading").hide();
        },
        error: function (){
            $("#modalContent").html('<p class="text-center text-danger mt-4">Có lỗi xảy ra. Vui lòng thử lại</p>');
            $("#modal-loading").hide();
        }
    })
})