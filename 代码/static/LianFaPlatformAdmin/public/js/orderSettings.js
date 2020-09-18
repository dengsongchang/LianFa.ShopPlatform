$(function () {
    ProductExport.init();
})

var ProductExport = {
    init:function () {
        ProductExport.getOrderSetting();
        // 初始化switch开关控件
        Common.initSwitch();

        //完成按钮的点击
        $('body').on('click','#submit',function(){
            var orderTime = $('#orderTime').val();
            var orderDay = $('#orderDay').val();
            var sendDay = $('#sendDay').val();
            var finishDay = $('#finishDay').val();
            var finishEvaluate = $('#finishEvaluate').val();
            if($('#autoRefund').hasClass('switch-on')){
                var autoRefund = 1;
            }else{
                var autoRefund = 0;
            }
            if($('#smsNotice').hasClass('switch-on')){
                var smsNotice = 1;
            }else{
                var smsNotice = 0;
            }
            //限时订单超过时间验证
            if (!Validate.emptyValidateAndFocus("#orderTime", "请输入限时订单超过时间", "")) {
                return false;
            }
            //下单超过时间未付款，订单关闭验证
            if (!Validate.emptyValidateAndFocus("#orderDay", "请输入下单超过的时间未付款，订单关闭", "")) {
                return false;
            }
            //发货超过时间未付款，订单自动完成
            if (!Validate.emptyValidateAndFocus("#sendDay", "请输入发货超过时间未付款，订单自动完成", "")) {
                return false;
            }
            //订单完成超过的天数，不能售后验证
            if (!Validate.emptyValidateAndFocus("#finishDay", "请输入订单完成超过的天数，不能售后", "")) {
                return false;
            }
            //订单完成超过的天数，自动五星好评验证
            // if (!Validate.emptyValidateAndFocus("#finishEvaluate", "请输入订单完成超过的天数，自动五星好评", "")) {
            //     return false;
            // }
            ProductExport.orderSetting(orderTime,orderDay,sendDay,finishDay,finishEvaluate,autoRefund,smsNotice)
        });
    },
//    订单设置
    orderSetting:function (ordertime,orderday,sendday,finishday,finishevaluate,autorefund,smsnotice) {
        //请求方法
        var methodName = "/orderSetting/AdminEditOrderSetting";
        var data = {
            "GroupBuylimitTime": ordertime,
            "OrderLimitTime": orderday,
            "SendTime": sendday,
            "CompleteOrder": finishday,
            "AutoGoodComments": 0,
            "Autorefund": autorefund,
            "AutoSendTip": smsnotice
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('设置成功');
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
//    获得订单设置
    getOrderSetting:function () {
        //请求方法
        var methodName = "/orderSetting/AdminOrderSettingList";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                 $('#orderTime').val(data.Data.OrderSettingList[0].GroupBuylimitTime);
                 $('#orderDay').val(data.Data.OrderSettingList[0].OrderLimitTime);
                 $('#sendDay').val(data.Data.OrderSettingList[0].SendTime);
                 $('#finishDay').val(data.Data.OrderSettingList[0].CompleteOrder);
                 $('#finishEvaluate').val(data.Data.OrderSettingList[0].AutoGoodComments);
                if(data.Data.OrderSettingList[0].Autorefund==1){
                    $('#autoRefund').addClass('switch-on')
                }else{
                    $('#autoRefund').removeClass('switch-on');
                }
                if(data.Data.OrderSettingList[0].AutoSendTip==1){
                    $('#smsNotice').addClass('switch-on')
                }else{
                    $('#smsNotice').removeClass('switch-on');
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}

