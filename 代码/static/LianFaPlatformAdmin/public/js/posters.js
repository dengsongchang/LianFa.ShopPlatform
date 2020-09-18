var poster = {
    init:function(){
        poster.distributionPostersSettingInfo()
        uploadIconPic('#small_upload_pick', '#small_icon', '/Distribution/AdminDistributionPosters');
        $('body').on('click','#preservation',function(){
            if($('#small_icon').attr('data-src') == ""){
                Common.showInfoMsg('请上传图片')
                return false
            }
            poster.settingDistributionPosters();
        })
    },
    //后台设置分销海报
    settingDistributionPosters:function(){
        //请求方法
        var methodName = "/Distribution/SettingDistributionPosters";
        var data = {
            "Posters": $('#small_icon').attr('data-src'),
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
               Common.showSuccessMsg("保存成功",function(){
                   location.href = '/application/posters';
               })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });

    },
    //后台获取分销海报
    distributionPostersSettingInfo:function(){
        //请求方法
        var methodName = "/Distribution/DistributionPostersSettingInfo";
        var data = {

        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                if(data.Data.DistributionPostersSettingInfo.Posters != ""){
                    $('#small_icon').attr('data-src',data.Data.DistributionPostersSettingInfo.Posters);
                    $('#small_icon').attr('src',data.Data.DistributionPostersSettingInfo.PostersFull);
                    $('.exitbusinesscard').attr('src',data.Data.DistributionPostersSettingInfo.PostersFull);
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });

    },
}

$(function(){
    poster.init();
})


