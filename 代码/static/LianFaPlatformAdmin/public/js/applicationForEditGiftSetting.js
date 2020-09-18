var setGuideImg = {
    imgTpl: `
        {{each List as value i}}
            <div class="img-container">
                <img class="select-img" src="{{List[i].FullUrl}}" data-src="{{List[i].Url}}">
                <input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">
                <img class="close-img" src="/public/images/close.png" style="width: 100%;height: 100%;">
            </div>
        {{/each}}
        {{if List.length < 10}}
            <div class="img-container">
                <img class="select-img" src="/public/images/addImg.png">
                <input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">
                <img class="close-img" src="/public/images/close.png">
            </div>
        {{/if}}
    `,

    init: function () {
        //图片上传按钮
        uploadFoodPic('#brandbox','#uploader_food_btn','/product/AdminUploadProductImg');
        setGuideImg.adminGetGiftBagInfo()
        //点击保存的时候
        $('body').on('click', '#Submission', function () {

            //库存
            if (!Validate.emptyValidateAndFocusAndColor("#Title", "请输入标题", "")) {
                return false;
            }
            //库存
            if (!Validate.emptyValidateAndFocusAndColor("#ShopPrice", "请输入销售价", "")) {
                return false;
            }
            //库存
            if (!Validate.emptyValidateAndFocusAndColor("#CostPrice", "请输入成本价", "")) {
                return false;
            }
            //赠送熊豆数量
            if (!Validate.emptyValidateAndFocusAndColor("#Amount", "请输入赠送熊豆数量", "")) {
                return false;
            }
            if($('#brandbox').attr('data-src') == ""){
                Common.showInfoMsg('请上传封面图')
                return false;
            }

            var list = [];
            $('.select-img').each(function (index, item) {
                if ($(this).attr('data-src') != "" && $(this).attr('data-src') != undefined) {
                    list.push($(item).attr('data-src'))
                }
            })
            console.log(list)
            if (list.length < 1) {
                Common.showInfoMsg('请上传至少一张图片')
                return false;
            }
            setGuideImg.adminEditGiftBag(list);
        })


    },
    //编辑礼包
    adminEditGiftBag: function (imgList) {
        //请求方法
        var methodName = "/Distribution/AdminEditGiftBag";
        var data = {
            "GId": Common.getUrlParam('id'),
            "Title": $('#Title').val(),
            "ShopPrice": $('#ShopPrice').val(),
            "CostPrice": $('#CostPrice').val(),
            "ImgList": imgList,
            "Amount":$('#Amount').val(),
            "ShowImg":$('#brandbox').attr('data-src')
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg("编辑成功", function () {
                    location.href = '/application/applicationForGiftSetting'
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取礼包信息
    adminGetGiftBagInfo: function () {
        //请求方法
        var methodName = "/Distribution/AdminGetGiftBagInfo";
        var data = {
            "GId": Common.getUrlParam('id')
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(setGuideImg.imgTpl);
                var html = render(data.Data);
                $("#productImg").html(html);
                $('#Title').val(data.Data.Title);
                $('#ShopPrice').val(data.Data.ShopPrice);
                $('#CostPrice').val(data.Data.CostPrice);
                $('#Amount').val(data.Data.Amount);
                $('#brandbox').attr('data-src',data.Data.ShowImgs.Url);
                $('#brandbox').attr('src',data.Data.ShowImgs.FullUrl);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }

}
$(function () {

    setGuideImg.init();

})