$(function () {
    NavigationAdd.init();
})

var NavigationAdd = {
    init: function () {
        //上传小图标
        uploadIconPic('#small_upload_pick', '#small_icon', '/indexDatas/AdminUploadNavigationBarIcon');
        //保存按钮点击
        $('body').on('click', '#nextStep', function () {
            var Name = $('#consultingName').val();
            var Icon = $('#small_icon').attr('data-src');
            var DisplayOrder = $('#sort').val();
            var Url = $("#url").val();
            //咨询名称验证
            if (!Validate.emptyValidateAndFocus("#consultingName", "请输入标题名称", "")) {
                return false;
            }
            //链接地址验证
            // if (!Validate.emptyValidateAndFocus("#url", "请输入链接地址", "")) {
            //     return false;
            // }
            //排序验证
            if (!Validate.emptyValidateAndFocus("#sort", "请输入排序", "")) {
                return false;
            }
            //图片验证
            if (Icon == null || Icon == undefined) {
                Common.showInfoMsg('请先上传图片')
                return false;
            }
            // 调用编辑接口这个方法
            NavigationAdd.adminProvinceList(Name, Icon, DisplayOrder,Url);
        })
    },
    //获取添加接口
    adminProvinceList: function (Name, Icon, DisplayOrder,url) {
        //请求方法
        var methodName = "/indexDatas/AdminAddNavigationBar";
        var data = {
            "Name": Name,
            "Icon": Icon,
            "Url": url,
            "DisplayOrder": DisplayOrder,
            "Type":0,
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功', function () {
                    location.href = '/homePage/navSetting'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

}