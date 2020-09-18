$(function() {
    ExchangeBanner.init();
})

var ExchangeBanner = {
    init: function() {
        //上传图片
        uploadIconPic('#put_upload_pick', '#put_icon', '/Banner/AdminSetupScreenImg');
        $("#nextStep").on("click", function() {
            Common.confirmDialog('是否确认保存广告图修改？', function() {
                ExchangeBanner.savePosterImg();
            })
        });

        ExchangeBanner.getPosterImg();
    },
    // 获取广告图
    getPosterImg: function() {
        var methodName = "/Banner/AdminCouponImgSetupData";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                $('#put_icon').attr('src', data.Data.CouponFullImg);
                $('#put_icon').attr('data-src', data.Data.CouponImg);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    savePosterImg: function() {
        var methodName = "/Banner/AdminCouponImgSetup";
        var data = {
            CouponImg: $('#put_icon').attr('data-src'),
        };
        //请求接口
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {

                Common.showSuccessMsg('修改成功', function() {
                    ExchangeBanner.getPosterImg();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}