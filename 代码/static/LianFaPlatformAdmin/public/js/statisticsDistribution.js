$(function () {
    Distribution.init();
});

var Distribution = {
    init:function () {
        // 选择显示天数
        $(".dayOrMonth li").on("click", function () {
            $(this).addClass("active").siblings().removeClass("active");
        });
        //查询
        $("#search").on("click",function(){

        });
    }
}