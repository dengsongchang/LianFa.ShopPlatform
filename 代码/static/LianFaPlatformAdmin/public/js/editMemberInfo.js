var EditMemberInfo = {
    userRankTpl: `
        {{each UserRankList as value i}}
        <option value="{{UserRankList[i].UserRId}}">{{UserRankList[i].Title}}</option>
        {{/each}}
    `,
    init:function(){
        EditMemberInfo.adminStoreManagerInfo()
        EditMemberInfo.time();
        //点击确认按钮
        $('body').on('click','#baseConfirm',function(){
            //店长账户
            if (!Validate.emptyValidateAndFocusAndColor("#MemberName", "请输入店长账户", "")) {
                return false;
            }
            var phoneReg = /^1[3|4|5|8][0-9]\d{4,8}$/;
            if($('#phone').val() == "" || !phoneReg.test($('#phone').val())){
                Common.showInfoMsg('请输入正确的手机号码')
                return false
            }
            //验证码
            if (!Validate.emptyValidateAndFocusAndColor("#code", "请输入验证码", "")) {
                return false;
            }
            // //密码
            // if (!Validate.emptyValidateAndFocusAndColor("#password", "请输入密码", "")) {
            //     return false;
            // }
            // //确认密码
            // if (!Validate.emptyValidateAndFocusAndColor("#repassword", "请输入确认密码", "")) {
            //     return false;
            // }
            EditMemberInfo.editMember()


        })

    },
    time:function(){
        var btn = $("#getVCode");
        timerButton.verify("#getVCode", {
            time: 60,//倒计时时间
            event: "click",//事件触发方式
            //执行条件，可以是function也可以是Boolean值，如果是函数则需返回true才会执行
            condition: function () {
                var phoneReg = /^1[3|4|5|8][0-9]\d{4,8}$/,
                    flag = phoneReg.test($("#phone").val());
                if (!flag) {
                    Common.showInfoMsg("请输入正确的手机号")
                    return false;
                }
                return true;
            },
            unableClass: "unabled",//按钮不能使用时的class
            runningText: " s后重发",//计时正在进行中时按钮显示的文字
            timeUpText: "重新获取",//时间到了时按钮显示的文字
            progress: function (time) {//计时正在进行中时的回调
                btn.html(time + " s后重发");
            },
            timeUp: function (time) {//计时结束时执行的回调
                btn.html("重新获取");
            },
            abort: function () {//中断计时
                btn.html("重新获取");
            },
            eventFn: function () {//事件执行后的回调
                //获取当前时间戳
                var timeStamp = Date.parse(new Date()) / 1000;

                //获取签名
                var sign = SignRequest.getSign("api/merchants" + '/storeManager/SendMobileVerifyCode', timeStamp);
                var requestData = {
                    "ContactsPhone": $("#phone").val()
                };
                //获取请求数据
                var jsonData = JSON.stringify(requestData);

                //获取token
                var token = SignRequest.getToken();

                // 解决IE10以下不能请求
                jQuery.support.cors = true;

                $.ajax({
                    type: "post",
                    url: SignRequest.urlPrefix+"/storeManager/SendMobileVerifyCode",
                    data: {
                        "ContactsPhone": $("#phone").val()
                    },
                    dataType: "json",
                    headers: {
                        "TimeStamp": timeStamp,
                        "Sign": sign,
                        "MethodName": "api/Merchants" + '/storeManager/SendMobileVerifyCode',
                        "Token": SignRequest.getToken()
                    },
                    success: function (data) {
                        if(data.Code == "100"){
                            // Common.showSuccessMsg(data.Message)
                        }else{
                            Common.showErrorMsg(data.Message)
                        }

                    },
                    error: function () {
                        Common.showErrorMsg('获取验证码失败, 请稍后重新获取')
                    }
                });
            }
        });
    },
    // 编辑会员
   editMember:function () {
        var methodName = "/storeManager/AdminEditStoreManager";
        var data = {
            "UId": Common.getUrlParam('id'),
            "UserName": $('#MemberName').val(),
            "Password": $('#password').val(),
            "ConfirmPassword": $('#repassword').val(),
            "VerifyCode":$('#code').val(),
            "Mobile":$('#phone').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("编辑成功",function(){
                    location.href = '/store/managerList'
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取店长信息
    adminStoreManagerInfo:function(){
        var methodName = "/storeManager/AdminStoreManagerInfo";
        var data = {
            "UId": Common.getUrlParam('id'),
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $('#MemberName').val(data.Data.AdminStoreManagerInfo.UserName);
                $('#phone').val(data.Data.AdminStoreManagerInfo.Mobile)
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

}
$(function(){

    EditMemberInfo.init()

})