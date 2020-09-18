$(function () {
    NavigationEditor.init();
})

var NavigationEditor = {
    init: function () {
        //上传小图标
        uploadIconPic('#small_upload_pick', '#small_icon', '/indexDatas/AdminUploadNavigationBarIcon');
        //获取Id
        var nbId = Common.getUrlParam("id");
        //获取导航信息 
        NavigationEditor.getNavInfo(nbId);
        
        //保存按钮点击
        $('body').on('click', '#nextStep', function () {
            var Name = $('#consultingName').val();
            var Icon = $('#small_icon').attr('data-src');
            var DisplayOrder = $('#sort').val() ? $('#sort').val() : 0;
            var Url = $("#url").val();
            
            //咨询名称验证
            if (!Validate.emptyValidateAndFocus("#consultingName", "请输入标题名称", "")) {
                return false;
            }

            //链接地址验证
            // if (!Validate.emptyValidateAndFocus("#url", "请输入链接地址", "")) {
            //     return false;
            // }

            //排序验证
            if ($("#type").val() != 2 &&!Validate.emptyValidateAndFocus("#sort", "请输入排序", "")) {
                return false;
            }

            //图片验证
            if (Icon == null || Icon == undefined) {
                Common.showInfoMsg('请先上传图片')
                return false;
            }
            // 调用编辑接口这个方法
            NavigationEditor.adminProvinceList(nbId, Name, Icon, DisplayOrder,Url);
        })
    },
    //获取编辑接口
    adminProvinceList: function (NBId, Name, Icon, DisplayOrder,Url) {
        //请求方法
        var methodName = "/indexDatas/AdminEdidNavigationBar";
        var data = {
            "NBId": NBId,
            "Name": Name,
            "Icon": Icon,
            "Url": Url,
            "DisplayOrder": DisplayOrder,
            "Type":0,
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功', function () {
                    location.href = '/homePage/navSetting'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取导航信息     
    getNavInfo: function (NBId) {
        var methodName = "/indexDatas/AdminNavigationBarInfo";
        var data = {
            "NBId": NBId,
        }
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#consultingName').val(data.Data.NavigationBarInfo.Name);
                $('#url').val(data.Data.NavigationBarInfo.Url);
                $('#small_icon').attr('src', data.Data.NavigationBarInfo.IconFull);
                $('#small_icon').attr('data-src', data.Data.NavigationBarInfo.Icon);
                $("#type").val(data.Data.NavigationBarInfo.Type);
                if(data.Data.NavigationBarInfo.Type == 2)
                {
                    $('#sortGroup').hide();
                }
                else{
                    $('#sortGroup').show();
                    $('#sort').val(data.Data.NavigationBarInfo.DisplayOrder);
                }

                // $('input[value='+data.Data.NavigationBarInfo.Type+']').prop('checked',true);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },



}