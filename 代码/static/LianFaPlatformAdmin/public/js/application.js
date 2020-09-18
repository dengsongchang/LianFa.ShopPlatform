$(function() {
	Application.init();
});

var Application = {
	init: function() {
		// 初始化switch开关控件
		Common.initSwitch();
        Application.distributionSettingInfo()
        $('input[type="text"],input[type="number"]').on('blur', function () {
            $(this).css('border', '1px solid #ccc')
        })
        // Application.distributionApplySettingInfo()
		//switch控件判断
		$("#switch").on("click", function() {
			if($(this).hasClass("switch-on")) {
				$(".xianshi").css({"display":"block"});
			}else{
				$(".xianshi").css({"display": "none"});
			}
		})
        $("#Registerswitch").on("click", function () {
            if ($(this).hasClass("switch-on")) {
                $('.hideBox').hide()
            }
            else {
                $('.hideBox').show()

            }
        })

        $('input').iCheck({
            checkboxClass: 'icheckbox_flat-blue',
            radioClass: 'iradio_flat-blue',
            increaseArea: '20%' // optional
        });
        //分佣规则提交按钮点击
		$('body').on('click','#Submission',function(){

            //基础佣金比例
            if (!Validate.emptyValidateAndFocusAndColor("#Proportion", "请输入基础佣金比例", "")) {
                return false;
            }
            //合伙人佣金比例
            if (!Validate.emptyValidateAndFocusAndColor("#QueenProportion", "请输入合伙人佣金比例", "")) {
                return false;
            }
            //合伙人佣金比例
            if (!Validate.emptyValidateAndFocusAndColor("#KingProportion", "请输入合伙人佣金比例", "")) {
                return false;
            }
            //合伙人育成佣金比例
            if (!Validate.emptyValidateAndFocusAndColor("#ParentQueenProportion", "请输入合伙人育成佣金比例", "")) {
                return false;
            }
            //合伙人育成佣金比例
            if (!Validate.emptyValidateAndFocusAndColor("#ParentKingProportion", "请输入合伙人育成佣金比例", "")) {
                return false;
            }

            Application.settingDistribution()
		})
		//分销申请设置按钮点击
		$('body').on('click','#submitBtn',function(){
            Application.settingDistributionApply();
		})


	},
	//后台获取分销申请设置信息
    distributionApplySettingInfo:function(){
        var methodName = "/Distribution/DistributionApplySettingInfo";
        var data = {
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
            	var result = data.Data.DistributionApplySettingInfo;
                $('#continuSign').val(result.Amount);
                if(result.IsRegistered == "1"){
                    //代表开启自购是计算佣金
                    $('#Registerswitch').addClass('switch-on');
                    $('.hideBox').hide();
                }else{
                    $('#Registerswitch').removeClass('switch-on')
                    $('.hideBox').show();
                }
                if(result.IsRealName == "1"){
					$('#realName').iCheck('check');
				}
                if(result.IsMobile == "1"){
                    $('#mobile').iCheck('check');
                }
                if(result.IsEmail == "1"){
                    $('#email').iCheck('check');
                }
                if(result.IsAddress == "1"){
                    $('#address').iCheck('check');
                }
                if(result.Conditions == "1"){
                    $('#conditional').iCheck('check');
                    $('#noconditional').iCheck('uncheck');
                }else{
                    $('#conditional').iCheck('uncheck');
                    $('#noconditional').iCheck('check');
				}


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
	//后台设置分销申请
    settingDistributionApply:function(){
        var methodName = "/Distribution/SettingDistributionApply";
        if($('#Registerswitch').hasClass('switch-on')){
            var data = {
                "IsRegistered":$('#Registerswitch').hasClass('switch-on') ? '1' : '0' ,
                "IsRealName": '0',
                "IsMobile": '0',
                "IsVerifyMobile": 0,
                "IsEmail":'0',
                "IsAddress": '0',
                "Conditions":'0',
                "Amount":"0",
            };
		}else{
            var data = {
                "IsRegistered":$('#Registerswitch').hasClass('switch-on') ? '1' : '0' ,
                "IsRealName": $("#realName").is(':checked') ? '1' : '0',
                "IsMobile": $("#mobile").is(':checked') ? '1' : '0',
                "IsVerifyMobile": 0,
                "IsEmail": $("#email").is(':checked') ? '1' : '0',
                "IsAddress": $("#address").is(':checked') ? '1' : '0',
                "Conditions":$('input[name="state"]:checked').val(),
                "Amount": $('input[name="state"]:checked').val() == "1" ? $('#continuSign').val() : "0",
            };
		}

        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('设置成功',function () {
                    location.href = '/application/application'
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
	//后台获取分销设置信息
 	distributionSettingInfo:function(){
        var methodName = "/Distribution/DistributionSettingInfo";
        var data = {
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
            	$('#KingProportion').val(data.Data.DistributionSettingInfo.KingProportion)
                $('#ParentKingProportion').val(data.Data.DistributionSettingInfo.ParentKingProportion)
                $('#ParentQueenProportion').val(data.Data.DistributionSettingInfo.ParentQueenProportion)
                $('#Proportion').val(data.Data.DistributionSettingInfo.Proportion)
                $('#QueenProportion').val(data.Data.DistributionSettingInfo.QueenProportion)

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
	},
	//后台设置分销规则
    settingDistribution:function(){
        var methodName = "/Distribution/SettingDistribution";
        var data = {
            "QueenProportion": $('#QueenProportion').val(),
            "KingProportion": $('#KingProportion').val(),
            "ParentQueenProportion": $('#ParentQueenProportion').val(),
            "ParentKingProportion": $('#ParentKingProportion').val(),
            "Proportion": $('#Proportion').val()
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
               Common.showSuccessMsg('设置成功',function () {
				   location.href = '/application/application'
               })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
}