var RefundApplicationDetail = {
    init: function () {
        RefundApplicationDetail.adminOrderRefundInfo();
        // 返回上一页
        $('body').on('click','.backBtn',function(){
            window.history.go(-1);
        })
    },
    //后台退款订单信息
    adminOrderRefundInfo:function(){
        var methodName = "/orderRefund/AdminOrderRefundInfo";
        var data = {
            "RefundId": Common.getUrlParam('id')
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#stateName').text(data.Data.OrderRefundInfo.State);
                $('#orderNumber').text(data.Data.OrderRefundInfo.OSn);
                $('#refund_reason').text(data.Data.OrderRefundInfo.ApplyReason);
                $('#apply_momeny').text(data.Data.OrderRefundInfo.RefundMoney);
                $('#refundsNumber').text(data.Data.OrderRefundInfo.RefundSn);
                $('#applyWay').text(data.Data.OrderRefundInfo.PaySystemName);
                $('#marker').text(data.Data.OrderRefundInfo.BuyerRemark);
                $('#tatal').text(data.Data.OrderRefundInfo.PayMoney);
                $('#confirmMomeny').text(data.Data.OrderRefundInfo.RefundMoney);
                $('#applyTime').text(data.Data.OrderRefundInfo.ApplyTime.replace("T"," "));
                $('#adminMarker').text(data.Data.OrderRefundInfo.CheckResult);
                $('#sellNumber').text(data.Data.OrderRefundInfo.ASId);
                $('#applyNumber').text(data.Data.OrderRefundInfo.PaySn);
                //判断
                if(data.Data.OrderRefundInfo.RefundTime.replace("T"," ") == '1970-01-01 00:00:00' && data.Data.OrderRefundInfo.State == "失败"){
                    $('#handleTime').text("");
                }else if(data.Data.OrderRefundInfo.RefundTime.replace("T"," ") == '1970-01-01 00:00:00'){
                    $('#handleTime').text("还未处理");
                }else{
                    $('#handleTime').text(data.Data.OrderRefundInfo.RefundTime.replace("T"," "));
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
};

$(function () {

    RefundApplicationDetail.init()

})