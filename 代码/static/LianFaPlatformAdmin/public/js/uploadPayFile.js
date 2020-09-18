$(function () {
    // 选择图片
    $(".content").on("change","#weChatCerti",function () {
        uploadFile.uploadPayFile(this);
    });

})

var uploadFile = {

    // 上传文件
    uploadPayFile:function(thisInput){
        var formData = new FormData();
        formData.append("file",$(thisInput)[0].files[0]);
        console.log($(thisInput)[0].files[0]);
        $.ajax({
            url: SignRequest.urlPrefix + "/shoppingsetting/UploadFileToPaySetWeixinCert",
            type: "post",
            dataType: "json",
            data: formData,
            cache: false,
            processData: false,
            contentType: false
        }).done(function (data) {
            console.log(data);
            if (data.Code == "100") {
                Common.showSuccessMsg('上传成功!')
            }
            else {
                $(thisInput).val("");
                Common.showErrorMsg(data.Message);
            }
        }).fail(function () {
            $(thisInput).val("");
            Common.showErrorMsg("上传文件失败");
        })
    }
}


