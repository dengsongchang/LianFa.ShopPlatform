$(function() {
    dialogPoster.init();
})

var dialogPoster = {
    init: function() {
        //上传图片
        uploadIconPic('#put_upload_pick', '#put_icon', '/banner/AdminSetupScreenImg');
        $("#nextStep").on("click", function() {
            Common.confirmDialog('是否确认保存广告图修改？', function() {
                dialogPoster.savePosterImg();
            })
        });

        dialogPoster.getPosterImg();
    },
    // 获取广告图
    getPosterImg: function() {
        var methodName = "/Banner/AdminScreenSetupData";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                $('#put_icon').attr('src', data.Data.ScreenFullImg);
                $('#put_icon').attr('data-src', data.Data.ScreenImg);
                $('#url').val(data.Data.ScreenLink)
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    savePosterImg: function() {
        var methodName = "/Banner/AdminScreenSetup";
        var data = {
            ScreenImg: $('#put_icon').attr('data-src'),
            ScreenLink: $('#url').val()
        };
        //请求接口
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {

                Common.showSuccessMsg('修改成功', function() {
                    dialogPoster.getPosterImg();
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}