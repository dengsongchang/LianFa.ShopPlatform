$(function () {
    basicSetup.init();
})

var basicSetup={
    init:function () {
        basicSetup.adminGetWithdrawConfig(); //初始化获取基本设置数据


        //完成按钮的点击
        $('.basicsetup-btn').on('click','#nextStep',function(){
            var goodsKind = $('#goodsKind').val();
            //商品种类验证
            if (!Validate.emptyValidateAndFocus("#AutoWithdrawAmount", "请输入自动提现限额", "")) {
                return false;
            }

            basicSetup.adminSaveWithdrawConfig();
        });
    },

//    提现设置
    adminSaveWithdrawConfig:function () {
        //请求方法
        var methodName = "/SplitCommWithdraw/AdminSaveWithdrawConfig";
        var data = {
            "AutoWithdrawAmount": $('#AutoWithdrawAmount').val()
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('设置成功!',function(){
                    basicSetup.adminGetWithdrawConfig()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

    //    获得基本设置
    adminGetWithdrawConfig:function () {
        //请求方法
        var methodName = "/SplitCommWithdraw/AdminGetWithdrawConfig";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $('#AutoWithdrawAmount').val(data.Data.AutoWithdrawAmount)
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }

}