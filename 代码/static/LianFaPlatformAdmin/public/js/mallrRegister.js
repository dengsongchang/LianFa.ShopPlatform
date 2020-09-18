$(function () {
    mallRegister.init();
})


var mallRegister = {

    IsDefault:"",


    init: function () {
        mallRegister.getRegisterSetting();//初始化获得注册数据
        // String.prototype.bool = function() {
        //     return (/^true$/i).test(this);
        // };
        //根据是否发送信息来显隐信息文本框
        $("input:radio[name='send']").on("ifChecked",function () {
            if($('input:radio[name="send"]:checked').val()=="false"){
                $("#message").attr("disabled",true);
            }
            if($('input:radio[name="send"]:checked').val()=="true"){
                $("#message").attr("disabled",false);
            }
        })
        //完成按钮点击
        $('body').on('click', '#submitBtn', function () {
            var register=parseInt($("input:radio[name='registerKind']:checked").val());
            var login=parseInt($("input:radio[name='loginKind']:checked").val());
            var reserveName=$("#reserveName").val();
            var registerTime=parseInt($("#registerTime").val());
            var loginFail=parseInt($("#loginFail").val());
            var remmerPassword=$("input:radio[name='password']:checked").val();
            var sendMessage=$("input:radio[name='send']:checked").val();
            var message=$("#message").val();
            //保留用户名验证
            if (!Validate.emptyValidateAndFocus("#reserveName", "请输入保留的用户名", "")) {
                return false;
            }
            //注册时间限制验证
            if (!Validate.emptyValidateAndFocus("#registerTime", "请输入注册限制的时间", "")) {
                return false;
            }
            //登录失败次数的验证
            if (!Validate.emptyValidateAndFocus("#registerTime", "请输入登录失败的次数", "")) {
                return false;
            }
            if ($("input[name='send']:checked").val()=="1" && $("#message").val()=="") {
                Common.showErrorMsg("请输入信息!");
                return false;
            }
            mallRegister.submitMallRegister(register,login,reserveName,registerTime,loginFail,remmerPassword,sendMessage,message);
        });

        $('input').iCheck({
            checkboxClass: 'icheckbox_flat-blue',
            radioClass: 'iradio_flat-blue',
            increaseArea: '20%' // optional
        });
        $('.stuCheckBox').on('ifChecked', function (event) { //ifCreated 事件应该在插件初始化之前绑定
            // MemberGrade.IsDefault = 1;
        });
        $('.stuCheckBox2').on('ifChecked', function (event) { //ifCreated 事件应该在插件初始化之前绑定
            // MemberGrade.IsDefault = 0;
        });

        // //第一个选中
        // $('.stuCheckBox').iCheck('check');



    },
    //提交注册设置
    submitMallRegister:function (register,login,reservename,registertime,loginfail,remmerpassword,sendmessage,message) {
        //请求方法
        var methodName = "/shoppingsetting/AdminRegisteredSettings";
        var data = {
            "RegistryNameType": register,
            "LoginNameType": login,
            "keepUserName": reservename,
            "RegistryTime": registertime,
            "LoginFailure": loginfail,
            "WhetherRememberPassword": remmerpassword,
            "WhetherSend": sendmessage,
            "WelcomeMessage": message
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

    //    获得注册设置
    getRegisterSetting:function () {
        //请求方法
        var methodName = "/shoppingsetting/AdminRegistrySettingsData";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var result = data.Data;
                $('input').iCheck('uncheck');
                if(result.RegistryNameType==1){
                    $("#registerName").iCheck('check');
                }
                if(result.RegistryNameType==2){
                    $("#registerMail").iCheck('check');
                }
                if(result.RegistryNameType==3){
                    $("#reginsterPhone").iCheck('check');
                }
                if(result.LoginNameType==1){
                    $("#loginName").iCheck('check');
                }
                if(result.LoginNameType==2){
                    $("#loginMail").iCheck('check');
                }
                if(result.LoginNameType==3){
                    $("#loginPhone").iCheck('check');
                }
                if(result.WhetherRememberPassword=="true"){
                    $("#isPassword").iCheck('check');
                }
                if(result.WhetherRememberPassword=="false"){
                    $("#noPassword").iCheck('check');
                }
                if(result.WhetherSend=="true"){
                    $("#isSend").iCheck('check');
                }
                if(result.WhetherSend=="false"){
                    $("#noSend").iCheck('check');
                }
                $('#reserveName').val(result.keepUserName);
                $('#registerTime').val(result.RegistryTime);
                $('#loginFail').val(result.LoginFailure);
                $('#message').val(result.WelcomeMessage);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}
