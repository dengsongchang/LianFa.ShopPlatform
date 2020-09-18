var typeAdd_name = {
    init:function(){
        $('body').on('click','#nextBtn',function(){
            //商品类型名称
            if (!Validate.emptyValidateAndFocusAndColor("#classify_name", "请输入商品类型名称", "")) {
                return false;
            }
            //备注
            if (!Validate.emptyValidateAndFocusAndColor("#Remark", "请输入备注", "")) {
                return false;
            }
            typeAdd_name.addAttributeGroup();
        })

    },
    //后台添加商品类型名称
    addAttributeGroup:function(){
        var methodName = "/productSku/AddAttributeGroup";
        var data = {
            "Name": $('#classify_name').val(),
            "Remark": $('#Remark').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功',function(){
                    //将id存到本地缓存
                    localStorage.setItem('typeId',data.Data.AttrGroupId);
                    location.href = "/classify/typeAdd";
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
}
$(function(){

    typeAdd_name.init()

})