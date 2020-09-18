var distributorGradeEdit = {
    init: function () {
        distributorGradeEdit.adminDistributionGradeInfo()
        $('body').on('click', '.finish_bth_edit', function () {
            //分销员等级名称
            if (!Validate.emptyValidateAndFocus("#gradeName", "请输入分销员等级名称", "")) {
                return false;
            }
            //销售额
            if (!Validate.emptyValidateAndFocus("#Sales", "请输入销售额", "")) {
                return false;
            }
            //团队销售额
            if (!Validate.emptyValidateAndFocus("#TeamSales", "请输入团队销售额", "")) {
                return false;
            }
            //团队数量
            if (!Validate.emptyValidateAndFocus("#TeamCount", "请输入团队数量", "")) {
                return false;
            }
            distributorGradeEdit.adminEditDistributionGrade();
        })

    },
    //后台分销员等级信息
    adminDistributionGradeInfo: function () {
        var methodName = "/DistributionGrade/AdminDistributionGradeInfo";
        var data = {
            "GradeId": Common.getUrlParam('id'),
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $('#gradeName').val(data.Data.DistributionGradeInfo.Title);
                $('#Sales').val(data.Data.DistributionGradeInfo.Sales)
                $('#TeamSales').val(data.Data.DistributionGradeInfo.TeamSales)
                $('#TeamCount').val(data.Data.DistributionGradeInfo.TeamCount)
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台编辑分销员等级
    adminEditDistributionGrade: function () {
        var methodName = "/DistributionGrade/AdminEditDistributionGrade";
        var data = {
            "GradeId": Common.getUrlParam('id'),
            "Title": $('#gradeName').val(),
            "Sales": $('#Sales').val(),
            "TeamSales": $('#TeamSales').val(),
            "TeamCount": $('#TeamCount').val()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("编辑成功", function () {
                    location.href = '/distribution/distributorGrade'
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

};
$(function () {
    distributorGradeEdit.init()
})