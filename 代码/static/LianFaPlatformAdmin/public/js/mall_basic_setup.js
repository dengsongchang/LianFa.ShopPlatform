$(function () {
    basicSetup.init();
})

var basicSetup = {
    init: function () {
        basicSetup.getbasicSetting(); //初始化获取基本设置数据

        // //初始化富文本编辑器
        // var ue = UE.getEditor('regEditor');
        //
        // ue.ready(function () {
        // });

        //完成按钮的点击
        $('.basicsetup-btn').on('click', '#nextStep', function () {
            var goodsKind = $('#goodsKind').val();
            var servicePhone = $('#servicePhone').val();
            //商品种类验证
            if (!Validate.emptyValidateAndFocus("#goodsKind", "请输入地址", "")) {
                return false;
            }
            //客服电话验证
            if (!Validate.emptyValidateAndFocus("#servicePhone", "请输入客服电话", "")) {
                return false;
            }
            basicSetup.submitBasicSetup(goodsKind, servicePhone);
        });
    },

//    提交基本设置
    submitBasicSetup: function (goodskind, servicephone) {
        //请求方法
        var methodName = "/AdminSetting/AdminPlatformSetup";
        var data = {
            "Address": goodskind,
            "CustomerPhone": servicephone
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('提交成功!')
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

    //    获得基本设置
    getbasicSetting: function () {
        //请求方法
        var methodName = "/AdminSetting/AdminPlatformSetupData";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                // var ue = UE.getEditor('regEditor');
                var result = data.Data;
                $('#goodsKind').val(result.Address);
                $('#servicePhone').val(result.CustomerPhone);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }

}