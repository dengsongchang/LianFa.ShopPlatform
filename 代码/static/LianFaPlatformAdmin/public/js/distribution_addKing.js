var distributionAddKing = {
    init:function(){
        //添加按钮点击
        $('body').on('click','.finish_bth_add',function(){
            if($('#type') == '0'){
                Common.showInfoMsg('请选择类型')
                return false
            }
            // var phoneReg = /^1[3|4|5|8][0-9]\d{4,8}$/;
            // if($('#phone').val() == "" || !phoneReg.test($('#phone').val())){
            //     Common.showInfoMsg('请输入正确的手机号码')
            //     return false
            // }
            distributionAddKing.adminCreateDistUser()
        })
        //
    },
    // 添加熊王
    adminCreateDistUser:function () {
        var methodName = "/Distribution/AdminCreateDistUser";
        var data = {
            "Mobile": $('#phone').val(),
            "Type": $('#type').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("添加成功",function(){
                    location.href = '/distribution/UserList'
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};
$(function(){
    distributionAddKing.init()
})