$(document).ready(function () {
    var $gallery = $('.gallery');

    $gallery.vitGallery({
        debag: true,
        thumbnailMargin: 5,
        fullscreen: false,
        description: false,
        animateSpeed: 600
    })
    $(".rating").rating();

    var productCharacteristicsContent = $("#productCharacteristicsContent");
    if (productCharacteristicsContent.innerHeight() >= 700) {
        $(".btn-more").show();
    }
})

$(".btn-more").click(function () {
    var elm = $(this);
    elm.hide();
    $("#productCharacteristicsContent").css("max-height", "100%");
})

var image;
function changeImageCover(e) {
    $("#imageCover").animate({ left: '100%' }, 0);

    image = $(e).attr("data-name");
    setTimeout(changeImg, 200);

};

function changeImg() {
    $("#imageCover").css("background-image", "url('../Content/Upload/Images/" + image + "')");
    $("#imageCover").animate({ left: '0' }, 0);
};

$(function () {
    $(".add-to-cart").click(function () {
        $("#orderLoading").show();
        pid = $(this).attr("data-id");
        $.ajax({
            method: "POST",
            url: "/Cart/Add",
            data: { productId: pid },
            success: function () {
                window.location = "../../../Cart";
            }
        });
    });
});
function sendReviewSuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        $.ajax({
            method: "GET",
            url: "/Product/LoadReview",
            data: { productId: $("#reviewPartial").data("id") },
            success: function (data) {
                $("#reviewPartial").html(data);
                exitSpinner('#loadingGif-reviewContent');
            }
        })
    }
    else {
        toastr.error(data.error);
        exitSpinner('#loadingGif-reviewContent');
    }
}