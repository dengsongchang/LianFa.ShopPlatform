$(function () {
    basicSetup.init();
})

var basicSetup={
    init:function () {
        basicSetup.adminGetAppNotifyOverTime(); //初始化时效性设置数据


        //完成按钮的点击
        $('.basicsetup-btn').on('click','#nextStep',function(){
            var goodsKind = $('#goodsKind').val();
            //商品种类验证
            if (!Validate.emptyValidateAndFocus("#OverTime", "请输入时效性", "")) {
                return false;
            }

            basicSetup.adminSetAppNotifyOverTime();
        });
    },

//    时效性设置
    adminSetAppNotifyOverTime:function () {
        //请求方法
        var methodName = "/appNotify/AdminSetAppNotifyOverTime";
        var data = {
            "OverTime": $('#OverTime').val()
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('设置成功!',function(){
                    basicSetup.adminGetAppNotifyOverTime()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

    //    获得时效性设置
    adminGetAppNotifyOverTime:function () {
        //请求方法
        var methodName = "/appNotify/AdminGetAppNotifyOverTime";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $('#OverTime').val(data.Data)
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }

}