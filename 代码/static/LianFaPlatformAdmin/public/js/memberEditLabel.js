$(function () {
    MemberLabel.init();
})


var MemberLabel = {

    IsDefault:"",


    init: function () {
        MemberLabel.adminUserLabelInfo()
        //完成按钮点击
        $('body').on('click', '#submitBtn', function () {
            var name = $('#txtTagName').val();
            var orderCount = $('#txtOrderCount').val();
            var OrderTotal = $('#txtOrderTotalAmount').val();


            if (!Validate.emptyValidateAndFocus("#txtTagName", "请输入标签名", "")) {
                return false;
            }
            if (!Validate.emptyValidateAndFocus("#txtOrderCount", "请输入累计成功笔数", "")) {
                return false;
            }
            if (!Validate.emptyValidateAndFocus("#txtOrderTotalAmount", "请输入累计购买金额", "")) {
                return false;
            }
            MemberLabel.adminEditUserLabel(name,orderCount,OrderTotal)
        })



    },
    //后台编辑会员标签
    adminEditUserLabel: function (name,orderCount,OrderTotal) {
        //请求方法
        var methodName = "/userLabel/AdminEditUserLabel";
        var data = {
            "LId": Common.getUrlParam('id'),
            "BuyCount": orderCount,
            "AmountTotal": OrderTotal,
            "Title": name
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功', function () {
                    location.href='/member/memberLabel'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台会员标签信息
    adminUserLabelInfo:function () {
        //请求方法
        var methodName = "/userLabel/AdminUserLabelInfo";
        var data = {
            "LId": Common.getUrlParam('id'),
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#txtTagName').val(data.Data.UserLabelInfo.Title);
                $('#txtOrderCount').val(data.Data.UserLabelInfo.BuyCount);
                $('#txtOrderTotalAmount').val(data.Data.UserLabelInfo.AmountTotal);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }

}
