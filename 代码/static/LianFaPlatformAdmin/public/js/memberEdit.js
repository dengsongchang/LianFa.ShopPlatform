$(function () {
    MemberEdit.init();
})

var MemberEdit = {
    listTpl:`
        <option selected="selected" value="0">{{CommonText}}</option>
        {{each RegionsList as value i}}
            <option value="{{RegionsList[i].RegionId}}">{{RegionsList[i].Name}}</option>
        {{/each}}
    `,
    userRankTpl: `
        {{each UserRankList as value i}}
            <option value="{{UserRankList[i].UserRId}}">{{UserRankList[i].Title}}</option>
        {{/each}}
    `,
    labelTpl: `
        {{each UserLabelList as value i}}
            <li>
                <p data-id="{{UserLabelList[i].LId}}">{{UserLabelList[i].Title}}</p>
            </li>
        {{/each}}
    `,
    init:function () {
        // 返回上一页
        $('body').on('click','.backBtn',function(){
            window.history.go(-1);
        })
        // 基本信息编辑保存
        $("#baseConfirm").on("click",function () {
            if($("#rankList").val() == 0){
                Common.showInfoMsg("请选择会员等级");
            }
            else {
                if($("#street").val() == 0){
                    Common.showInfoMsg("请选择街道");
                }
                else {
                    MemberEdit.editMemberBaseInfo();
                }
            }
        });

        // 密码编辑保存
        $("#passwordConfirm").on("click",function () {
            if($("#ConfirmPwd").val() == $("#Password").val() && $("#ConfirmPwd").val() != ""){
                MemberEdit.editMemberPassword();
            }
            else {
                Common.showInfoMsg("输入的密码不一致");
            }
        });

        // 选择省
        $("#province").on("change",function () {
            var selectId = $(this).val();
            MemberEdit.getCityList(selectId);
            var areaHtml = '<option selected="selected" value="0">区/县</option>';
            $("#area").html(areaHtml);
            var streetHtml = '<option selected="selected" value="0">街道</option>';
            $("#street").html(streetHtml);
        });

        // 选择市
        $("#city").on("change",function () {
            var selectId = $(this).val();
            MemberEdit.getDistrictList(selectId);
            var streetHtml = '<option selected="selected" value="0">街道</option>';
            $("#street").html(streetHtml);
        });

        // 选择县
        $("#area").on("change",function () {
            var selectId = $(this).val();
            MemberEdit.getStreetList(selectId);
        });

        MemberEdit.initLaydateWithBirthyDay();
        MemberEdit.getUserRankList();
        MemberEdit.getProvinceList();
    },
    // 初始化生日日期控件
    initLaydateWithBirthyDay:function () {
        var BDay = laydate.render({
            elem: '#BDay',
            type: 'date',
            max: 0, //最大日期
            istoday: false,
            done: function (value,date) {

            }
        })
    },
    //编辑会员密码
    editMemberPassword:function(){
        var methodName = "/user/AdminEditUserPwd";
        var data = {
            UId: Common.getUrlParam("UId"),
            Password: $("#Password").val(),
            ConfirmPwd: $("#ConfirmPwd").val()  
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("密码修改成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //编辑会员基本信息
    editMemberBaseInfo:function(){
        var methodName = "/user/AdminEditUser";
        var data = {
            UId: Common.getUrlParam("UId"),
            NickName: $("#NickName").val(),
            RealName: $("#RealName").val(),
            BDay: $("#BDay").val(),
            UserRId: $("#rankList").val(),
            Gender: $('input:radio[name="gender"]:checked').val(),
            Email: $("#Email").val(),
            RegionId: $("#street").val(),
            Address: $("#Address").val(),
            Mobile: $("#Mobile").val()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("编辑基本信息成功",function(){
                    location.href = "/member"
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取会员等级列表
    getUserRankList:function(){
        var methodName = "/user/AdminUserRankList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(MemberEdit.userRankTpl);
                var html = render(data.Data);
                $("#rankList").append(html);

                MemberEdit.getProvinceList();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取会员详情信息
    getMemberDetailInfo:function () {
        var methodName = "/user/AdminGetUserInfo";
        var data = {
            UId: Common.getUrlParam("UId")
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var result = data.Data;
                // 会员名、用户名
                $("#MemberName").text(result.UserInfo.UserName);
                $("#UserName").text(result.UserInfo.UserName);
                // 会员等级
                $("#rankList").val(result.UserInfo.UserRId);
                // 姓名
                $("#RealName").val(result.UserInfo.RealName)
                // 生日
                $("#BDay").val(result.UserInfo.BDay.substring(0, 10))
                // 性别
                var genderList = $("#Gender input");
                for(var i=0;i<genderList.length;i++){
                    if(genderList.eq(i).val() == result.UserInfo.Gender){
                        genderList.eq(i).prop("checked",true);
                    }
                }
                // 电子邮件地址
                $("#Email").val(result.UserInfo.Email);
                //省、市、县、街道
                if(result.UserInfo.RegionId != 0){
                    $("#province").val(result.UserInfo.ProvinceId);
                    MemberEdit.getCityList(result.UserInfo.ProvinceId,function () {
                        $("#city").val(result.UserInfo.CityId);
                        MemberEdit.getDistrictList(result.UserInfo.CityId,function () {
                            $("#area").val(result.UserInfo.CountId);
                            MemberEdit.getStreetList(result.UserInfo.CountId,function () {
                                $("#street").val(result.UserInfo.RegionId);
                            })
                        })
                    });
                }
                // 详细地址
                $("#Address").val(result.UserInfo.Address);
                // 昵称
                $("#NickName").val(result.UserInfo.NickName);
                // 手机号码
                $("#Mobile").val(result.UserInfo.Mobile);
                // 注册日期
                $("#RegisterTime").text(result.UserInfo.RegisterTime.replace("T"," ").substring(0, 16));
                // 消费总金额
                $("#OrderAmount").text(result.StatisticsInfo.OrderAmount);
                // 会员标签
                var render = template.compile(MemberEdit.labelTpl);
                var html = render(data.Data);
                $("#labelList").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取省/自治区/直辖市列表
    getProvinceList:function () {
        var methodName = "/regions/AdminProvinceList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(MemberEdit.listTpl);
                data.Data.CommonText = "请选择省";
                var html = render(data.Data);
                $("#province").html(html);

                MemberEdit.getMemberDetailInfo();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取地级市列表
    getCityList:function (ParentId,callback) {
        var methodName = "/regions/AdminCityList";
        var data = {
            ParentId: ParentId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(MemberEdit.listTpl);
                data.Data.CommonText = "市";
                var html = render(data.Data);
                $("#city").html(html);

                if ($.isFunction(callback)) {
                    callback();
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取区县列表
    getDistrictList:function (ParentId,callback) {
        var methodName = "/regions/AdminMunicipalDistrictList";
        var data = {
            ParentId: ParentId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(MemberEdit.listTpl);
                data.Data.CommonText = "区/县";
                var html = render(data.Data);
                $("#area").html(html);

                if ($.isFunction(callback)) {
                    callback();
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取街道列表
    getStreetList:function (ParentId,callback) {
        var methodName = "/regions/AdminVillagesTownsList";
        var data = {
            ParentId: ParentId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(MemberEdit.listTpl);
                data.Data.CommonText = "街道";
                var html = render(data.Data);
                $("#street").html(html);

                if ($.isFunction(callback)) {
                    callback();
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}