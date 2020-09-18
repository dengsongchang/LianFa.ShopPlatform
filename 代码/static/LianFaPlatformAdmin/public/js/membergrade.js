$(function () {
    MemberIntegral.init();
})

var MemberIntegral = {
    init:function () {
        // 关闭弹窗
        $(".mask").on("click",".close",function () {
            $(".mask").hide();
        });

        // 初始化switch开关控件
        Common.initSwitch();
    }
}