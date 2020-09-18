$(function() {
    // 选择图片
    $(".productImg").on("change", ".img-input", function() {
        uploadImg.uploadImageAfterSelect(this);
    });

    //删除图片
    $(".productImg").on("click", ".close-img", function() {

        var length = $(".productImg .img-container").length;
        var flag = false;
        $(".productImg .img-container").each(function(index, item) {
            console.log($(item).find('.select-img').attr('data-src'))
            if ($(item).find('.select-img').attr('data-src') == undefined) {
                flag = true;
            }
        });
        if (!flag && length == 10) {
            $(this).parents('.img-container').remove();
            var html = '<div class="img-container">' +
                '<img class="select-img" src="/public/images/addImg.png">' +
                '<input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">' +
                '<img class="close-img" src="/public/images/close.png">' +
                '</div>';
            $(".productImg").append(html);
        } else {
            $(this).parents('.img-container').remove();
        }


    });
})

var uploadImg = {
    //预览图片
    updateImg: function(thisInput, imgPath) {
        var file = ($(thisInput)[0].files[0]);
        var reader = new FileReader();
        var imgFile;
        reader.readAsDataURL(file);
        reader.onload = function(e) {
            imgFile = e.target.result;
            $(thisInput).siblings(".select-img").attr("src", imgFile);
            $(thisInput).siblings(".select-img").attr("data-src", imgPath);
            $(thisInput).siblings(".close-img").css("width", "100%");
            $(thisInput).siblings(".close-img").css("height", "100%");
            var list = $(".productImg .img-container");
            var mark = true;
            for (var i = 0; i < list.length; i++) {
                if (list.eq(i).find(".select-img").attr("src") == "/public/images/addImg.png") {
                    mark = false;
                }
            }
            if (mark && list.length < 10) {
                var html = '<div class="img-container">' +
                    '<img class="select-img" src="/public/images/addImg.png">' +
                    '<input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">' +
                    '<img class="close-img" src="/public/images/close.png">' +
                    '</div>';
                $(".productImg").append(html);
            }

        }
    },
    // 上传图片
    uploadImageAfterSelect: function(thisInput) {
        var formData = new FormData();
        formData.append("file", $(thisInput)[0].files[0]);
        Common.showUploading();
        $.ajax({
            url: SignRequest.urlPrefix + "/product/AdminUploadProductImg",
            type: "post",
            dataType: "json",
            data: formData,
            cache: false,
            processData: false,
            contentType: false
        }).done(function(data) {
            swal.close()
            if (data.Code == "100") {
                uploadImg.updateImg(thisInput, data.Data);
            } else {
                $(thisInput).val("");
                Common.showErrorMsg(data.Message);
            }
        }).fail(function() {
            $(thisInput).val("");
            Common.showErrorMsg("上传图片失败");
        })
    }
}