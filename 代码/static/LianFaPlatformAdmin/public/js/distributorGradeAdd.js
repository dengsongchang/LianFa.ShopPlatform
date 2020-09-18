var distributorGradeAdd = {
    init:function(){

        $('body').on('click','.finish_bth_add',function() {
            //分销员等级名称
            if (!Validate.emptyValidateAndFocus("#gradeName", "请输入分销员等级名称", "")) {
                return false;
            }
            //佣金门槛
            if (!Validate.emptyValidateAndFocus("#threshold", "请输入佣金门槛", "")) {
                return false;
            }
            distributorGradeAdd.adminAddDistributionGrade();
        })
    },
    //后台添加分销员等级
    adminAddDistributionGrade:function(){
        var methodName = "/DistributionGrade/AdminAddDistributionGrade";
        var data = {
            "Title": $('#gradeName').val(),
            "Amount": $('#threshold').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("添加成功",function(){
                    location.href = '/distribution/distributorGrade'
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

};
$(function(){
    distributorGradeAdd.init()
})