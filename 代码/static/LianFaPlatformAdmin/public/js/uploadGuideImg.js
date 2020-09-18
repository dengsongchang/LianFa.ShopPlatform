$(function () {
    // 选择图片
    $(".productImg").on("change",".img-input",function () {
        uploadImg.uploadImageAfterSelect(this);
    });

    //删除图片
    $(".productImg").on("click",".close-imgG",function () {
        $(this).parents('.img-container').remove();
    });
})

var uploadImg = {
    //预览图片
    updateImg: function(thisInput,imgPath){
        var file = ($(thisInput)[0].files[0]);
        var reader = new FileReader();
        var imgFile;
        reader.readAsDataURL(file);
        reader.onload = function(e){
            imgFile = e.target.result;
            var ele = `
                <div class="img-container">
                  <img class="select-img" src="${imgFile}" data-src="${imgPath}">
                  <input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">
                  <div class="close-imgG" style="width: 100%;height: 100%;">X</div>
                </div>
                `
            $("#dowebok").append(ele);
            var list = $(".productImg .img-container");
            var mark = true;
            for(var i=0;i<list.length;i++){
                if(list.eq(i).find(".select-img").attr("src") == "/public/images/addImg.png"){
                    mark = false;
                }
            }
            if(mark && list.length < 10){
                var html = '<div class="img-container noSelect">' +
                    '<img class="select-img" src="/public/images/addImg.png">' +
                    '<input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">' +
                    '<img class="close-img" src="/public/images/close.png">' +
                    '</div>';
                $(".productImg").append(html);
            }
        }
        setGuideImg.preHandle();
    },
    // 上传图片
    uploadImageAfterSelect:function(thisInput){
        var formData = new FormData();
        formData.append("file",$(thisInput)[0].files[0]);
        Common.showUploading();
        $.ajax({
            url: SignRequest.urlPrefix + "/indexDatas/AdminUploadGuideImg",
            type: "post",
            dataType: "json",
            data: formData,
            cache: false,
            processData: false,
            contentType: false
        }).done(function (data) {
            swal.close()
            if (data.Code == "100") {
                uploadImg.updateImg(thisInput,data.Data);

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


