var NoticeEdit = {
    init: function () {
        //初始化富文本编辑器
        var ue = UE.getEditor('hcEditor');

        ue.ready(function () {
            ue.setHeight(500);
            //初始化公告信息
            NoticeEdit.adminConsultInfo();
        });


        //点击跳转
        $('body').on('click', '.listBtn', function () {
            location.href = '/homePage/noticeList?id=' + localStorage.getItem('Sid') + ''
        })
        $('body').on('click', '.addBtn', function () {
            location.href = '/homePage/EditNoticeList?id=' + localStorage.getItem('Sid') + ''
        })
        //保存按钮点击
        $('body').on('click', '#nextStep', function () {
            var content = $('#noticeContent').val();
            var sort = $('#sort').val();
            //咨询名称验证
            if (!Validate.emptyValidateAndFocus("#title", "请输入公告标题", "")) {
                return false;
            }
            //咨询名称验证
            // if (!Validate.emptyValidateAndFocus("#noticeContent", "请输入公告内容", "")) {
            //     return false;
            // }
            if (ue.getContent() == "" || ue.getContent() == null) {
                Common.showInfoMsg('请输入公告内容')
                return false
            }
            //排序验证
            if (!Validate.emptyValidateAndFocus("#sort", "请输入排序", "")) {
                return false;
            }
            NoticeEdit.upStoreAnnouncement(content, sort);
        })
    },
//    获取公告信息
    adminConsultInfo: function () {
        //请求方法
        var methodName = "/announcement/AdminAnnouncementInfo";
        var data = {
            "RecordId": Common.getUrlParam('id')
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var ue = UE.getEditor('hcEditor');
                ue.setContent(data.Data.AnnouncementInfo.Content);
                // $('#noticeContent').val(data.Data.AnnouncementInfo.Content);
                $('#sort').val(data.Data.AnnouncementInfo.DisplayOrder);
                $('#title').val(data.Data.AnnouncementInfo.Title)
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
        var methodName = "/announcement/AdminEditAnnouncement";
        var data = {
            "RecordId": Common.getUrlParam('id'),
            "Title": $('#title').val(),
            "Content": Description,
            "DisplayOrder": nSort
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功', function () {
                    location.href = '/homePage/noticeList?id=' + localStorage.getItem('Sid') + ''
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}
$(function () {
    NoticeEdit.init();
})