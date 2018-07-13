function SaveImage()
{
    var data = new FormData();
    var files = $("#UploadImg").get(0).files;
    if (files.length > 0) {
        data.append("MyImages", files[0]);
    }

    $.ajax({
        url: "/Admin/UploadFile",
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (response) {
            //code after success
            $("#txtImg").val(response);
            $("#imgPreview").attr('src', '/Content/images/' + response);
        },
        error: function (er) {
            alert(er);
        }

    });
}
//$("#UploadImg").change(function () {
//    var data = new FormData();
//    var files = $("#UploadImg").get(0).files;
//    if (files.length > 0) {
//        data.append("MyImages", files[0]);
//    }

//    $.ajax({
//        url: "/Admin/UploadFile",
//        type: "POST",
//        processData: false,
//        contentType: false,
//        data: data,
//        success: function (response) {
//            //code after success
//            $("#txtImg").val(response);
//            $("#imgPreview").attr('src', '/Content/images/' + response);
//        },
//        error: function (er) {
//            alert(er);
//        }

//    });
//});
