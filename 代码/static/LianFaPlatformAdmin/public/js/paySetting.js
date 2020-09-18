$(function () {
    paySetting.init();
});
var paySetting = {
    paySetId: "",
    CertificateMicroLetter: '',

    init: function () {

        paySetting.getPaySetting(); //初始化获得支付设置的数据
        // 初始化switch开关控件
        Common.initSwitch();
        // 选择文件
        $(".form-group").on("change", "#weChatCerti", function () {
            paySetting.uploadFileSelect(this);
        });
        //完成按钮的点击
        $('body').on('click', '#submitBtn', function () {
            var appId = $('#appId').val();
            var mchId = $('#mchId').val();
            var key = $('#key').val();
            //appId验证
            if (!Validate.emptyValidateAndFocus("#appId", "请输入appId", "")) {
                return false;
            }
            //mchId验证
            if (!Validate.emptyValidateAndFocus("#mchId", "请输入mchId", "")) {
                return false;
            }
            //key验证
            if (!Validate.emptyValidateAndFocus("#key", "请输入key", "")) {
                return false;
            }

            if (paySetting.CertificateMicro == "") {
                Common.showInfoMsg('请上传微信证书')
                return false;
            }
            paySetting.submitPaySetting();
        });
    },
//    提交支付设置
    submitPaySetting: function () {
        //请求方法
        var methodName = "/SplitCommWithdraw/AdminSaveWithdrawConfig";
        var data = {
            "AppId": $('#appId').val(),
            "MchId": $('#mchId').val(),
            "Key": $('#key').val(),
            "CertificateMicroLetter": paySetting.CertificateMicroLetter,
            "CheckName": $('#serviceModel').hasClass('switch-on') ? true : false,
            "CertificateMicro": paySetting.CertificateMicro,
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data);
            if (data.Code == "100") {
                Common.showSuccessMsg('提交成功!')
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //    获得支付设置
    getPaySetting: function () {
        //请求方法
        var methodName = "/SplitCommWithdraw/AdminGetWithdrawConfig";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data);
            if (data.Code == "100") {
                var result = data.Data;
                $('#appId').val(result.AppId);
                $('#mchId').val(result.MchId);
                $('#key').val(result.Key);
                paySetting.CertificateMicroLetter = result.CertificateMicroLetter;
                if (result.CheckName == true) {
                    $('#serviceModel').addClass('switch-on')
                } else {
                    $('#serviceModel').removeClass('switch-on');
                }
                if(result.CertificateMicroLetter){
                    // $('#Letter')
                    var index = result.CertificateMicroLetter.lastIndexOf('\\');
                    var fileName = result.CertificateMicroLetter.slice(index+1);
                    console.log(fileName)
                    $('#Letter').text(fileName)
                }
                paySetting.CertificateMicro = result.CertificateMicro;

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

//  文件上传
    uploadFileSelect: function (thisInput) {
        // 上传文件
        var formData = new FormData();
        formData.append("file", $(thisInput)[0].files[0]);
        console.log($(thisInput)[0].files[0]);
        $.ajax({
            url: SignRequest.urlPrefix + "/SplitCommWithdraw/UploadFileToPaySetWeixinCert",
            type: "post",
            dataType: "json",
            data: formData,
            cache: false,
            processData: false,
            contentType: false
        }).done(function (data) {
            console.log("上传之后",data);
            if (data.Code == "100") {
                Common.showSuccessMsg('上传成功!',function(){
                    paySetting.CertificateMicro = data.Data;
                })
            }
            else {
                Common.showErrorMsg(data.Message);
            }
        }).fail(function () {
            Common.showErrorMsg("上传文件失败");
        })

    }

}