function loadBlogCategory() {
    loadingSpinner("#loadingGif-BlogCategoryList");
    $.ajax({
        method: "POST",
        url: "/Admin/LoadBlogCategory",
        success: function (data) {
            $("#blogCategory").html(data);
            exitSpinner("#loadingGif-BlogCategoryList");
        }
    });
}
function loadBlogList() {
    $.ajax({
        method: "POST",
        url: "/Admin/LoadBlogList",
        success: function (data) {
            $("#blogList").html(data);
        }
    })
}
function actionBlogCategorySuccess(data) {
    if (data.status == "success") {
        toastr.success(data.message);
        loadBlogCategory();
    }
    else
        toastr.error(data.message);
    $("#ModalTemplateNormal").modal("hide");
}

$(document).on("click", ".edit-blog-category", function () {
    loadingSpinner("#modalNormal-loading");
    $.ajax({
        method: "GET",
        url: "/Admin/LoadEditBlogCategory",
        data: { blogCategoryId: $(this).data("id") },
        success: function (data) {
            $("#modalContentNormal").html(data);
            exitSpinner("#modalNormal-loading");
        }
    })
})

$(document).on("click",".delete-blog-category", function () {
    var blogCategoryId = $(this).data("id");
    var r = confirm("Bạn có muốn xóa danh mục này không?");
    if (r == true) {
        $("#ModalTemplateNormal").modal("hide");
        $.ajax({
            method: "POST",
            url: "/Admin/DeleteBlogCategory",
            data: { blogCategoryId: blogCategoryId },
            success: function (data) {
                if (data.status == "success") {
                    toastr.success(data.message);
                    loadBlogCategory();
                }
            }
        })
    }
})

$("#Cover").click(function () {
    var cover = $(this).val();
    loadLibraryImage("imageCover", false, cover);
});

function addCover() {
    var hasCover = false;
    $(".image-item").each(function () {
        if ($(this).data("selected") == "True") {
            $("#Cover").val($(this).attr("data-name"));
            $("#previewCover").attr("src", "../Content/Upload/Images/" + $(this).attr("data-name"));
            $("#removeCover").removeClass("d-none");
            hasCover = true;
        }
    });
    if (!hasCover) {
        $("#Cover").val(null);
        $("#previewCover").attr("src", "../Content/Upload/Images/No_Picture.JPG");
        $("#removeCover").addClass("d-none");
    }
}
$("#removeCover").click(function () {
    $("#previewCover").attr("src", "../Content/Upload/Images/No_Picture.jpg");
    $("#Cover").val("No_Picture.JPG");
    $("#removeCover").addClass("d-none");
});

$(document).on("click", ".delete-blog", function () {
    var blogId = $(this).data("id");
    var r = confirm("Bạn có muốn xóa bài viết '" + $(this).data("name") + "' này không?");
    if (r == true) {
        $.ajax({
            method: "POST",
            url: "/Admin/DeleteBlog",
            data: { blogId: blogId },
            success: function (data) {
                if (data.status == "success") {
                    toastr.success(data.message);
                    loadBlogList();
                }
            }
        })
    }
})
function loadPageList() {
    $.ajax({
        method: "POST",
        url: "/Admin/LoadPageList",
        success: function (data) {
            $("#pageList").html(data);
        }
    })
};
$(document).on("click", ".delete-page", function () {
    var pageId = $(this).data("id");
    var r = confirm("Bạn có muốn xóa trang '" + $(this).data("name") + "' này không?");
    if (r == true) {
        $.ajax({
            method: "POST",
            url: "/Admin/DeletePage",
            data: { pageId: pageId },
            success: function (data) {
                if (data.status == "success") {
                    toastr.success(data.message);
                    loadPageList();
                }
            }
        })
    }
})
