var setGuideImg = {
    imgTpl:`
        <div id="dowebok" style="float: left">
          {{each AdminGetGuideImg as value i}}
                <div class="img-container">
                    <img class="select-img"  src="{{AdminGetGuideImg[i].IconFull}}" data-src="{{AdminGetGuideImg[i].Icon}}">
                    <input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">
                    <div class="close-imgG" style="width: 100%;height: 100%;">X</div>
                </div>
           {{/each}}
        </div>

        {{if AdminGetGuideImg.length < 10}}
            <div class="img-container noSelect">
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
            setGuideImg.adminSetGuideImg(list);
        })




        setGuideImg.adminGetGuideImgList();

    },
    //设置引导图接口
    adminSetGuideImg:function(imgList){
        //请求方法
        var methodName = "/indexDatas/AdminSetGuideImg";
        var data = {
            "IconList":imgList
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg("设置成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取广告列表
    adminGetGuideImgList:function(){
        //请求方法
        var methodName = "/indexDatas/AdminGetGuideImgList";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(setGuideImg.imgTpl);
                var html = render(data.Data);
                $("#productImg").html(html);
                setGuideImg.preHandle();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //预览初始化
    preHandle:function(){
        if(setGuideImg.viewer){
            setGuideImg.viewer.destroy();
        }
        setGuideImg.viewer = new Viewer(document.getElementById('dowebok'),{
            url: 'data-original',
            show:function(){
                setGuideImg.viewer.update();
            },
        });
        $('body').on('click','#preBtn',function(){
            $('#dowebok').find('.img-container').each(function(index,item){
                var url = $(this).find('.select-img').attr('data-src');
                $(this).find('.select-img').attr('data-original',SignRequest.urlPrefixNoApi+url)
            })
            setGuideImg.viewer.show();
        })
    },

}
$(function(){

    setGuideImg.init();

})