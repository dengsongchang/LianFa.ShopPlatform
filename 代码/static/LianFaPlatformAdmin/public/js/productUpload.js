$(function () {
    ProductUpload.init();
})

var ProductUpload = {
    init: function () {
        $('#btnUpload').on('click',function(){
            ProductUpload.uploadFile()
        })
        $('#contentHolder_btnImport').on('click',function(){
            if(!$('#contentHolder_btnImport').attr('data-src')){
                Common.showInfoMsg('请先上传文件');
                return false;
            }else{
                ProductUpload.batchUploadAdminProduct()
            }
        })
    },
    //上传文件
    uploadFile:function(){
      var formData = new FormData();
        formData.append('file',$('#contentHolder_fileUploader')[0].files[0]);
        $.ajax({
            url: SignRequest.urlPrefix + "/product/AdminUploadExcel",
            type: "post",
            dataType: "json",
            data: formData,
            cache: false,
            processData: false,
            contentType: false
        }).done(function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('上传文件成功',function(){
                    $('#contentHolder_btnImport').attr('data-src',data.Data.ExcelUrl)
                });
            }
            else {
                $("#contentHolder_fileUploader").val("");
                Common.showErrorMsg(data.Message);
            }
        }).fail(function () {
            $("#contentHolder_fileUploader").val("");
            Common.showErrorMsg("上传文件失败");
        })
    },
    //批量导入Excel数据
    batchUploadAdminProduct:function(){
        //请求方法
        var methodName = "/product/BatchUploadAdminProduct";
        var data = {
            "ExcelUrl": $('#contentHolder_btnImport').attr('data-src'),
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('导入成功!',function(){
                    $('#contentHolder_btnImport').attr('data-src',"");
                    $("#contentHolder_fileUploader").val("");
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

}
