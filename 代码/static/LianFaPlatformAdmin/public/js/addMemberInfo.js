var AddMemberInfo = {
    userRankTpl: `
        {{each UserRankList as value i}}
        <option value="{{UserRankList[i].UserRId}}">{{UserRankList[i].Title}}</option>
        {{/each}}
    `,
    init:function(){
        //点击确认按钮
        $('body').on('click','#baseConfirm',function(){

            //店长账户
            if (!Validate.emptyValidateAndFocusAndColor("#MemberName", "请输入店长账户", "")) {
                return false;
            }
            //密码
            if (!Validate.emptyValidateAndFocusAndColor("#password", "请输入密码", "")) {
                return false;
            }
            //确认密码
            if (!Validate.emptyValidateAndFocusAndColor("#repassword", "请输入确认密码", "")) {
                return false;
            }

            AddMemberInfo.addMember()


        })
        //input失去焦点颜色变回原来
        $('input[type="text"],input[type="number"]').on('blur',function(){
            $(this).css('border','1px solid #ccc')
        })
        $('input[type="text"],input[type="number"]').on('focus',function(){
            $(this).css('border','1px solid #3c8dbc')
        })

    },
    // 新增会员
    addMember:function () {
        var methodName = "/storeManager/AdminAddStoreManager";
        var data = {
            "UserName": $('#MemberName').val(),
            "Password": $('#password').val(),
            "ConfirmPassword": $('#repassword').val()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("添加成功",function(){
                    location.href = '/store/managerList'
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
}
$(function(){

    AddMemberInfo.init()

})