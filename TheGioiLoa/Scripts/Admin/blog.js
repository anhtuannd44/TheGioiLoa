$("#Cover").click(function () {
    var cover = $(this).val();
    loadLibraryImage("imageCover", false, cover);
});

function addCover() {
    var hasCover = false;
    $(".image-item").each(function () {
        if ($(this).attr("data-selected") == "True") {
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
