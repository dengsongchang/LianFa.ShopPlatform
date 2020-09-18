var ClassifySet = {
    companyTpl: `
        {{each ThreeLevelCategoryList as value i}}
            <label class="checkbox-inline" style="margin-left: 0">
                <input type="checkbox" class="classifyItem" data-id="{{ThreeLevelCategoryList[i].CateId}}" value="{{ThreeLevelCategoryList[i].CateId}}"> {{ThreeLevelCategoryList[i].Name}}
            </label>
        {{/each}}
    `,
    init: function () {

        ClassifySet.adminThreeLevelCategoryList()

        //完成按钮点击
        $('body').on('click','.finish_bth_add',function(){
            var list = [];
            $('.classifyItem').each(function(item,index){
                if(this.checked == true){
                    list.push($(this).val())
                }
            })

            ClassifySet.adminSettigThreeLevelCategory(list);


        });



    },
    //获取三级分类列表
    adminThreeLevelCategoryList: function () {
        var methodName = "/category/AdminThreeLevelCategoryList";
        var data = {
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                console.log(data)
                var render = template.compile(ClassifySet.companyTpl);
                var html = render(data.Data);
                $("#storeBox").html(html);

                // 门店
                var sidList = $("#storeBox label");
                for (var i = 0; i < data.Data.ShowCateIdList.length; i++) {
                    for (var j = 0; j < sidList.length; j++) {
                        if (sidList.eq(j).find("input").val() == data.Data.ShowCateIdList[i]) {
                            sidList.eq(j).find("input").prop("checked", true);
                        }
                    }
                }
                if(data.Data.IsShow){
                    $('#isShow').prop('checked','checked')
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台设置三级分类
    adminSettigThreeLevelCategory: function (CateIdList) {
        var methodName = "/category/AdminSettigThreeLevelCategory";
        var data = {
            "CateIdList":CateIdList,
            "IsShow": $('#isShow').prop('checked')
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
               Common.showSuccessMsg('设置成功',function(){
                   location.href = '/classify/classifySet'
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