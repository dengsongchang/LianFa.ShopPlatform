var inboxReply={
    init:function () {
        $('.submitBtn').on('click',function(){
            if (!Validate.emptyValidateAndFocus("#title", "请输入标题", "")) {
                return false;
            }
            if (!Validate.emptyValidateAndFocus("#content", "请输入内容", "")) {
                return false;
            }
            inboxReply.adminInboxReply()
        })

    },
    //收件箱回复
    adminInboxReply:function(){
        var methodName = "/standinsideletter/AdminInboxReply";
        var data = {
            "InBoxId": Common.getUrlParam('id'),
            "ReplyRecordId": 0,
            "Title": $('#title').val(),
            "Content": $('#content').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('回复成功!',function(){
                    location.href = '/mail'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};

$(function(){

    inboxReply.init()

})