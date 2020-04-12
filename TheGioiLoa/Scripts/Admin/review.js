$(".change-comment-status").click(function () {
    $.ajax({
        method: "POST",
        url: "/Admin/ChageCommentStatus",
        data: {
            reviewId : $(this).data("id"),
            status: $(this).data("status")
        },
        success: function (data) {
            if (data.status == "success") {
                toastr.success(data.message);
                location.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    })
})