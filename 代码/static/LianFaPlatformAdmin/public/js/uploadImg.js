$(function () {
    // 选择图片
    $(".content").on("change","#listImgBtn",function () {
        uploadFile.uploadPayFile(this);
    });

    $(".content").on("click", "#listImgBtn", function () {
        //点击的时候先清空之前
        $('.img-input1').val("")
    });

})

var uploadFile = {
    //预览图片
    updateImg: function(thisInput,imgPath){
        var file = ($(thisInput)[0].files[0]);
        var reader = new FileReader();
        var imgFile;
        reader.readAsDataURL(file);
        reader.onload = function(e){
            imgFile = e.target.result;
            $('#small_icon').attr('src',imgFile)
        }
    },
    // 上传文件
    uploadPayFile:function(thisInput){

        var formData = new FormData();
        formData.append("file",$(thisInput)[0].files[0]);
        console.log($(thisInput)[0].files[0]);
        Common.showUploading();
        $.ajax({
            url: SignRequest.urlPrefix + "/adverts/AdminUploadAdvertsImg",
            type: "post",
            dataType: "json",
            data: formData,
            cache: false,
            processData: false,
            contentType: false
        }).done(function (data) {
            console.log("上传成功的返回",data);
            swal.close()
            if (data.Code == "100") {
                $('#small_icon').attr('data-src',data.Data)
                uploadFile.updateImg(thisInput,data.Data);
            }
            else {
                $(thisInput).val("");
                Common.showErrorMsg(data.Message);
            }
        }).fail(function () {
            $(thisInput).val("");
            Common.showErrorMsg("上传图片失败");
        })
    }
}


