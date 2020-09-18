$(function () {
    MemberGrade.init();
})


var MemberGrade = {

    IsDefault:"",


    init: function () {

        MemberGrade.adminUserRank();

        $('body').on('click','#editsubmit',function(){
            var name = $('#memberName').val().trim();
            var memberPrice = $('#memberPrice').val().trim();
            if (!Validate.emptyValidateAndFocus("#memberName", "请输入会员等级名称", "")) {
                return false;
            }
            if (!Validate.emptyValidateAndFocus("#memberPrice", "请输入会员等级价格", "")) {
                return false;
            }
            MemberGrade.adminEditUserRank(name,memberPrice)
        })

        $('input').iCheck({
            checkboxClass: 'icheckbox_flat-blue',
            radioClass: 'iradio_flat-blue',
            increaseArea: '20%' // optional
        });
        $('#stuCheckBox').on('ifChecked', function (event) { //ifCreated 事件应该在插件初始化之前绑定
            MemberGrade.IsDefault = 1;
        });
        $('#stuCheckBox2').on('ifChecked', function (event) { //ifCreated 事件应该在插件初始化之前绑定
            MemberGrade.IsDefault = 0;
        });



    },
    //后台编辑bi信息
    adminEditUserRank: function (name,memberPrice) {
        //请求方法
        var methodName = "/userRank/AdminEditUserRank";
        var data = {
            "UserRId": Common.getUrlParam('id'),
            "Title": name,
            "RankPrice": memberPrice
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功', function () {
                    location.href="/member/memberGrade"
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取后台会员等级信息
    adminUserRank:function(){
        //请求方法
        var methodName = "/userRank/AdminUserRank";
        var data = {
            "UserRId": Common.getUrlParam('id')
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#memberName').val(data.Data.UserRankInfo.Title);
                $('#memberPrice').val(data.Data.UserRankInfo.RankPrice);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },


}
