var ProductBrand = {

    init:function(){
        //获取编辑的资料
        ProductBrand.adminBrandInfo()
        //图片上传按钮
        uploadFoodPic('#brandbox','#uploader_food_btn','/brand/AdminUploadBrandLogo');
        //编辑里面的编辑按钮点击
        $('body').on('click','#submit',function(){
            var name = $('#brandName').val();
            var src = $('#brandbox').attr('data-src');
            var sort = $('#sort_name').val()
            //品牌名验证
            if (!Validate.emptyValidateAndFocus("#brandName", "请输入品牌名", "")) {
                return false;
            }
            //图片验证
            // if($('#brandbox').attr('data-src') == null || $('#brandbox').attr('data-src') == ""){
            //     Common.showErrorMsg("请上传图片!")
            //     return false;
            // }
            ProductBrand.adminEditBrand(name,src,sort)


        })
        //品牌数量的改变
        // $(".inputNum").change(function () {
        //     var reg = /^[1-9]\d*$/;
        //     if(!reg.test($(this).val())){
        //         swal({ title: '提示!', text: '请输入正确数量', timer: 3000, type: 'success' })
        //     }else{
        //         swal({ title: '提示!', text: '批量更新排序成功', timer: 3000, type: 'success' })
        //     }
        // });

    },
    //后台编辑品牌
    adminEditBrand:function(name,logo,sort){
        //请求方法
        var methodName = "/brand/AdminEditBrand";
        var data = {
            "BrandId": Common.getUrlParam('id'),
            "Name": name,
            "Logo": logo,
            "DisplayOrder": sort
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功',function(){
                    location.href='/brand'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台品牌信息
    adminBrandInfo:function(){
        //请求方法
        var methodName = "/brand/AdminBrandInfo";
        var data = {
            "BrandId": Common.getUrlParam('id')
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#brandName').val(data.Data.BrandInfo.Name);
                $('#sort_name').val(data.Data.BrandInfo.DisplayOrder);
                $('#brandbox').attr('src',data.Data.BrandInfo.LogoFull);
                $('#brandbox').attr('data-src',data.Data.BrandInfo.Logo);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },




}



$(function () {

    ProductBrand.init()

})