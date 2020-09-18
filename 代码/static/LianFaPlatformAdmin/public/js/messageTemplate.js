var Message = {

    init:function(){
        //初始化表格
        Message.getTemplateData();
        //   点击下载按钮
        $(".content").on("click","#uploadHelp",function () {
            console.log($(this).attr("data-id"));
            window.location.href=$(this).attr("data-id");
        });
        //点击保存设置
        $(".content").on("click","#saveSet",function () {
            var refundApplicat=$("#refundApplicat").val();
            var unPay=$("#unpay").val();
            var paySuccess=$("#paySuccess").val();
            var refund=$("#refund").val();
            var orderShip=$("#orderShip").val();
            var refundFail=$("#refundFail").val();
            Message.submitSetTemplate(refundApplicat,unPay,paySuccess,refund,orderShip,refundFail);
        })
    },
    //    获取消息模板数据
    getTemplateData:function () {
        //请求方法
        var methodName = "/shoppingsetting/AdminGetTemplateData";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $("#refundApplicat").val(data.Data.AfterSaleNotifyId);
                $("#unpay").val(data.Data.OrderCreatedId);
                $("#paySuccess").val(data.Data.OrderPaymentId);
                $("#refund").val(data.Data.OrderRefundId);
                $("#orderShip").val(data.Data.OrderShippingId);
                $("#refundFail").val(data.Data.RefundFailedId);
                $("#uploadHelp").attr("data-id",data.Data.HelpWordUrl);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
//    提交保存设置数据
    submitSetTemplate:function (refundapplicat,unpay,paysuccess,refund,ordership,refundfail) {
        //请求方法
        var methodName = "/shoppingsetting/AdminSetTemplate";
        var data = {
            "AfterSaleNotifyId": refundapplicat,
            "OrderCreatedId": unpay,
            "OrderPaymentId": paysuccess,
            "OrderRefundId": refund,
            "OrderShippingId": ordership,
            "RefundFailedId": refundfail
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('设置成功');
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}


$(function () {

    Message.init();

})