var proprietarySet = {
    imgTpl:`
        {{each AdminProprietaryProductImgList as value i}}
            <div class="img-container">
                <img class="select-img" src="{{AdminProprietaryProductImgList[i].ShowImgFull}}" data-src="{{AdminProprietaryProductImgList[i].ShowImg}}">
                <input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">
                <img class="close-img" src="/public/images/close.png" style="width: 100%;height: 100%;">
            </div>
        {{/each}}
        {{if AdminProprietaryProductImgList.length < 10}}
            <div class="img-container">
                <img class="select-img" src="/public/images/addImg.png">
                <input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">
                <img class="close-img" src="/public/images/close.png">
            </div>
        {{/if}}
    `,

    init:function(){
        //点击保存的时候
        $('body').on('click','#save',function(){
            var list = [];
            $('.select-img').each(function(index,item){
                if($(this).attr('data-src') != "" && $(this).attr('data-src') != undefined){
                    list.push($(item).attr('data-src'))
                }
            })
            console.log(list)
            if(list.length<1){
                Common.showInfoMsg('请上传至少一张图片')
                return false;
            }
            proprietarySet.adminSetProprietaryProductImg(list);
        })


        proprietarySet.adminProprietaryProductImgList();

    },
    //设置自营轮播接口
    adminSetProprietaryProductImg:function(imgList){
        //请求方法
        var methodName = "/product/AdminSetProprietaryProductImg";
        var data = {
            "ShowImgList":imgList
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg("设置成功",function(){
                    proprietarySet.adminProprietaryProductImgList();
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取广告列表
    adminProprietaryProductImgList:function(){
        //请求方法
        var methodName = "/product/AdminProprietaryProductImgList";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(proprietarySet.imgTpl);
                var html = render(data.Data);
                $("#productImg").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }

}
$(function(){

    proprietarySet.init();

})