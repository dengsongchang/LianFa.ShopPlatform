var ClassifySet = {
    companyTpl: `
        {{each AdminSecondLevelCategoryList as value i}}
            <label class="checkbox-inline">
                <input type="checkbox" class="classifyItem" data-id="{{AdminSecondLevelCategoryList[i].CateId}}" value="{{AdminSecondLevelCategoryList[i].CateId}}"> {{AdminSecondLevelCategoryList[i].Name}}
            </label>
        {{/each}}
    `,
    init: function () {

        ClassifySet.adminSecondLevelCategoryList()

        //完成按钮点击
        $('body').on('click','.finish_bth_add',function(){
            var list = [];
            $('.classifyItem').each(function(item,index){
                if(this.checked == true){
                    list.push($(this).val())
                }
            })

            ClassifySet.adminSettingSecondLevelCategory(list);


        });



    },
    //获取首页二级分类列表
    adminSecondLevelCategoryList: function () {
        var methodName = "/category/AdminSecondLevelCategoryList";
        var data = {
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                console.log(data)
                var render = template.compile(ClassifySet.companyTpl);
                var html = render(data.Data);
                $("#storeBox").html(html);// 门店
                var sidList = $("#storeBox label");
                for (var i = 0; i < data.Data.ShowCateIdList.length; i++) {
                    for (var j = 0; j < sidList.length; j++) {
                        if (sidList.eq(j).find("input").val() == data.Data.ShowCateIdList[i]) {
                            sidList.eq(j).find("input").prop("checked", true);
                        }
                    }
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台设置首页二级分类
    adminSettingSecondLevelCategory: function (CateIdList) {
        var methodName = "/category/AdminSettingSecondLevelCategory";
        var data = {
            "CateIdList":CateIdList,
            "IsShow": $('#isShow').prop('checked')
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
               Common.showSuccessMsg('设置成功',function(){
                   location.href = '/homePage/homeClassifyList'
               })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

};


$(function () {
    ClassifySet.init()

});