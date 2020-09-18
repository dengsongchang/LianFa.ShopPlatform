var StoreAgreement = {
    init: function () {
        //初始化富文本编辑器
        var ue = UE.getEditor('hcEditor');

        ue.ready(function () {
            ue.setHeight(500);
            StoreAgreement.adminConsultInfo();
        });

        //保存按钮点击
        $('body').on('click', '#nextStep', function () {
            var content = $('#noticeContent').val();
            var sort = $('#sort').val();
            if (ue.getContent() == "" || ue.getContent() == null) {
                Common.showInfoMsg('请输入内容')
                return false
            }
            StoreAgreement.upStoreAnnouncement(content, sort);
        })
    },
//    获取入驻门店设置
    adminConsultInfo: function () {
        //请求方法
        var methodName = "/stores/AdminGetEntranceSetting";
        var data = {
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var ue = UE.getEditor('hcEditor');
                ue.setContent(data.Data.AdminGetEntranceInfo.Entrance);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
//    提交编辑数据
    upStoreAnnouncement: function (nMessage, nSort) {
        var ue = UE.getEditor('hcEditor');
        var Description = ue.getContent();
        //请求方法
        var methodName = "/stores/AdminSetEntranceSetting";
        var data = {
            "Entrance": Description
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功', function () {
                    location.href = '/store/storeAgreement'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}
$(function () {
    StoreAgreement.init();
})