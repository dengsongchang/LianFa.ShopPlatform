var NoticeAdd={
    init:function () {
        //初始化富文本编辑器
        var ue = UE.getEditor('hcEditor');

        //门店id
        localStorage.setItem('Sid',Common.getUrlParam('id'));
        //点击跳转
        $('body').on('click','.listBtn',function(){
            location.href = '/homePage/noticeList?id='+localStorage.getItem('Sid')+''
        })
        $('body').on('click','.addBtn',function(){
            location.href = '/homePage/AddNoticeList?id='+localStorage.getItem('Sid')+''
        })
        //保存按钮点击
        $('body').on('click','#nextStep',function(){
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
            if(ue.getContent() == "" || ue.getContent() == null){
                Common.showInfoMsg('请输入公告内容')
                return false
            }
            //排序验证
            if (!Validate.emptyValidateAndFocus("#sort", "请输入排序", "")) {
                return false;
            }
            NoticeAdd.addStoreAnnouncement(content,sort);
        })
    },
//    提交新增数据
    addStoreAnnouncement:function (nMessage,nSort) {
        var ue = UE.getEditor('hcEditor');
        var Description = ue.getContent();
        //请求方法
        var methodName = "/announcement/AdminAddAnnouncement";
        var data = {
            "Title":$('#title').val(),
            "Content": Description,
            "DisplayOrder": nSort
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功',function(){
                    location.href = '/homePage/noticeList?id='+localStorage.getItem('Sid')+''
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}
$(function () {
    NoticeAdd.init();
})