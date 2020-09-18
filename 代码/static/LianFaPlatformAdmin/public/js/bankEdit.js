var bankEdit = {

    init:function(){
        //获取银行卡信息
        bankEdit.adminGetBank();
        //图片上传按钮
        // uploadFoodPic('#brandbox','#uploader_food_btn','/brand/AdminUploadBrandLogo');
        //查询按钮点击
        // $('body').on('click','#searchBtn',function(){
        //     var data = {
        //         "Name": $('#brand_name').val(),
        //     };
        //     bankEdit.projectQuery(data)
        // });
        //添加里面的完成按钮点击
        $('body').on('click','#submit',function(){
            //银行名称
            if (!Validate.emptyValidateAndFocus("#bankName", "请输入银行名称", "")) {
                return false;
            }
            // //银行编号
            // if (!Validate.emptyValidateAndFocus("#bankNum", "请输入银行编号", "")) {
            //     return false;
            // }
            //图片验证
            // if($('#brandbox').attr('data-src') == null || $('#brandbox').attr('data-src') == ""){
            //     Common.showErrorMsg("请上传图片!")
            //     return false;
            // }
            bankEdit.adminUpdateBank()


        });
    },
    //编辑银行
    adminUpdateBank:function(name,logo){
        //请求方法
        var methodName = "/SplitCommWithdraw/AdminUpdateBank";
        var data = {
            "BId": Common.getUrlParam('id'),
            "BankName": $('#bankName').val(),
            "BankCode": $('#bankNum').val(),
            "Icon": ""
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功',function(){
                    location.href = '/distribution/bankList'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取银行信息
    adminGetBank:function(name,logo){
        //请求方法
        var methodName = "/SplitCommWithdraw/AdminGetBank";
        var data = {
            "BId": Common.getUrlParam('id'),
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#bankName').val(data.Data.BankName)
                $('#bankNum').val(data.Data.BankCode)
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

}



$(function () {

    bankEdit.init()

})