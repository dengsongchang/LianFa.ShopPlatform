var NoticeAdd={
    init:function () {
        var ue = UE.getEditor('hcEditor');
        ue.ready(function () {
            ue.setHeight(500);
            NoticeAdd.adminGetAboutUs();
        });

        //初始化富文本编辑器
        var ue = UE.getEditor('hcEditor');

        //保存按钮点击
        $('body').on('click','#nextStep',function(){
            var content = $('#noticeContent').val();
            var sort = $('#sort').val();
            if(ue.getContent() == "" || ue.getContent() == null){
                Common.showInfoMsg('请输入公告内容')
                return false
            }
            NoticeAdd.addStoreAnnouncement(content,sort);
        })
    },
//    设置
    addStoreAnnouncement:function (nMessage,nSort) {
        var ue = UE.getEditor('hcEditor');
        var Description = ue.getContent();
        //请求方法
        var methodName = "/helps/AdminSetAboutUs";
        var data = {
            "AboutUs": Description,
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('设置成功',function(){
                    location.href = '/homePage/aboutUs'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取关于我们信息
    adminGetAboutUs:function () {
        var ue = UE.getEditor('hcEditor');

        //请求方法
        var methodName = "/helps/AdminGetAboutUs";
        var data = {

        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                ue.setContent(data.Data.AboutUs);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
}
$(function () {
    NoticeAdd.init();
})