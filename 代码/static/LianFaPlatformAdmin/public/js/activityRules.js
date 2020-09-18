var ActivityRules = {
    init:function(){
        ActivityRules.adminGetActivityRules();
        $('body').on('click','#addBtn',function(){
            //活动规则
            if (!Validate.emptyValidateAndFocus("#AContent", "请输入活动规则", "")) {
                return false;
            }
            //领取规则
            if (!Validate.emptyValidateAndFocus("#GContent", "请输入领取规则", "")) {
                return false;
            }
            ActivityRules.adminSetActivityRules()
        })
    },
    //设置活动规则
    adminSetActivityRules: function () {
        //请求方法
        var methodName = "/RulesSetting/AdminSetActivityRules";
        var data = {
            "ActivityRules": $('#AContent').val(),
            "ReceiveRules": $('#GContent').val()
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('设置成功',function(){
                    ActivityRules.adminGetActivityRules()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

    //获取活动规则
    adminGetActivityRules: function () {
        //请求方法
        var methodName = "/RulesSetting/AdminGetActivityRules";
        var data = {

        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#AContent').val(data.Data.ActivityRules)
                $('#GContent').val(data.Data.ReceiveRules)
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};
$(function(){
    ActivityRules.init()
})