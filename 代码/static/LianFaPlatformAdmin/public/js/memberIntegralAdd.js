$(function(){
    MemberIntegralManagement.init();
})

var MemberIntegralManagement = {

    init:function(){
        $('body').on('click','#Submission',function(){
            //会员直接上级抽佣比例
            if (!Validate.emptyValidateAndFocusAndColor("#OrderLimit", "请输入订单金额", "")) {
                return false;
            }
            //会员直接上级抽佣比例
            if (!Validate.emptyValidateAndFocusAndColor("#MaxAmount", "请输入最大使用熊豆数量", "")) {
                return false;
            }
            MemberIntegralManagement.adminAddCreditSetting();
        })


    },




    // 后台新增积分设置
    adminAddCreditSetting:function (Id) {
        var methodName = "/credit/AdminAddCreditSetting";
        var data = {
            "OrderLimit": $('#OrderLimit').val(),
            "MaxAmount": $('#MaxAmount').val()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("设置成功",function(){
                    location.href = '/member/memberIntegralManagement'
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },


}