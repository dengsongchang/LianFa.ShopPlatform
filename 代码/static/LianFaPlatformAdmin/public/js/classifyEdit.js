var ClassifyAdd = {
    CateTemplate: `
              <option selected="selected" value="0" data-layer="0">请选择上级分类</option>
              {{each CategoryList as value i}}
                 {{if CategoryList[i].Layer != "3"}}
                    <option value={{CategoryList[i].CateId}} data-layer="{{CategoryList[i].Layer}}">{{CategoryList[i].Name}}</option>
                  {{/if}}
              {{/each}}
`,
    init: function () {

        ClassifyAdd.adminCategoryList();
        //上传小图标
        uploadIconPic('#small_upload_pick', '#small_icon','/category/AdminUploadCategorySmallIcon');
        //上传大图标
        uploadIconPic('#big_upload_pick', '#big_icon','/category/AdminUploadCategoryBigIcon');

        //cateBox变化的时候
        $('body').on('change','#cateBox',function(){
            if($('#cateBox option:selected').attr('data-layer') == "2"){
                $('#smallBox').show()
                $('#bigBox').hide()
            }
            //代表自己是一级分类
            if($('#cateBox option:selected').attr('data-layer') == "0"){
                $('#bigBox').show()
                $('#smallBox').hide()
            }
            //代表自己是二级分类
            if($('#cateBox option:selected').attr('data-layer') == "1"){
                $('#bigBox').hide()
                $('#smallBox').hide()
            }
        })
        //完成按钮点击
        $('body').on('click','.finish_bth_add',function(){
            var name = $('#classify_name').val();
            var parentsId = $('#cateBox option:selected').val();
            // var smallicon = $('#small_icon').attr("data-src");
            if($("#smallBox").is(":visible")){
                var smallicon = $('#small_icon').attr('data-src');

            };
            if($("#bigBox").is(":visible")){
                var bigicon = $('#big_icon').attr('data-src');
            };
            //分类名称
            if (!Validate.emptyValidateAndFocus("#classify_name", "请输入分类名称", "")) {
                return false;
            }
            ClassifyAdd.adminEditCategory(name,parentsId,smallicon,bigicon)

        });



    },
    //后台分类列表
    adminCategoryList: function () {
        //请求方法
        var methodName = "/category/AdminCategoryList";
        var data = {};
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(ClassifyAdd.CateTemplate);
                var html = render(data.Data);
                $('#cateBox').html(html)
                ClassifyAdd.adminCategoryInfo()
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台编辑分类
    adminEditCategory:function(name,parentsId,smallicon,bigicon){
        //请求方法
        var methodName = "/category/AdminEditCategory";
        var data = {
            "CateId": Common.getUrlParam('id'),
            "Name": name,
            "ParentId": parentsId,
            "BigIcon": bigicon != undefined ? bigicon : "",
            "SmallIcon": smallicon != undefined ? smallicon : "",
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功',function(){
                    location.href="/classify/classifyList?id="+Common.getUrlParam('id')+"";
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台分类信息
    adminCategoryInfo:function(){
        //请求方法
        var methodName = "/category/AdminCategoryInfo";
        var data = {
            "CateId": Common.getUrlParam('id')
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#classify_name').val(data.Data.CategoryInfo.Name);
                if(data.Data.CategoryInfo.Layer == "3"){
                    $('#smallBox').show()
                    $('#bigBox').hide()
                }
                if(data.Data.CategoryInfo.Layer == "2"){
                    $('#smallBox').hide()
                    $('#bigBox').hide()
                }
                if(data.Data.CategoryInfo.Layer == "1"){
                    $('#smallBox').hide()
                    $('#bigBox').show()
                }

                if(data.Data.CategoryInfo.SmallIcon != null && data.Data.CategoryInfo.SmallIcon != ""){
                    $('#small_icon').attr('data-src',data.Data.CategoryInfo.SmallIcon);
                    $('#small_icon').attr('src',data.Data.CategoryInfo.SmallIconFull)
                }else{
                    $('#small_icon').attr('data-src',"");
                }
                if(data.Data.CategoryInfo.BigIcon != null && data.Data.CategoryInfo.BigIcon != ""){
                    $('#big_icon').attr('data-src',data.Data.CategoryInfo.BigIcon);
                    $('#big_icon').attr('src',data.Data.CategoryInfo.BigIconFull)
                }else{
                    $('#big_icon').attr('data-src',"");
                }
                $('#cateBox').val(data.Data.CategoryInfo.ParentId);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }

};


$(function () {
    ClassifyAdd.init()

});