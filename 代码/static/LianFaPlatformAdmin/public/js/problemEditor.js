$(function () {
    ProblemEditor.init();
});

var ProblemEditor={
    init:function () {
        //获取Id
        var hId = Common.getUrlParam("id");
        //获取轮播信息
        ProblemEditor.getNavInfo(hId);
        //保存按钮点击
        $('body').on('click', '#nextStep', function () {
            var Title = $('#ProblemTitle').val();
            var description = $('#description').val();
            var DisplayOrder = $('#sort').val();
            //标题验证
            if (!Validate.emptyValidateAndFocus("#ProblemTitle", "请输入标题名称", "")) {
                return false;
            }
            //描述验证
            if (!Validate.emptyValidateAndFocus("#description", "请输入问题描述", "")) {
                return false;
            }
            //排序验证
            if (!Validate.emptyValidateAndFocus("#sort", "请输入排序", "")) {
                return false;
            }
            // 调用编辑接口这个方法
            ProblemEditor.adminProvinceList(hId, Title, description, DisplayOrder);
        })
    },

    //获取编辑接口
    adminProvinceList: function (hId, title, description, displayOrder) {
        //请求方法
        var methodName = "/helps/AdminUpHeples";
        var data = {
            "HId": hId,
            "Title": title,
            "Description": description,
            "DisplayOrder": displayOrder
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功', function () {
                    location.href = '/homePage/problem_list';
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取信息
    getNavInfo: function (HId) {
        var methodName = "/helps/AdminHeplesInfo";
        var data = {
            "HId": HId,
        }
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $('#ProblemTitle').val(data.Data.AdminHelps.Title);
                $('#description').val(data.Data.AdminHelps.Description);
                $('#sort').val(data.Data.AdminHelps.DisplayOrder);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
}