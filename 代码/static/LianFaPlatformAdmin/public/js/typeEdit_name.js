var typeEdit_name = {
    init:function(){
        typeEdit_name.attributeGroupInfo();
        //将id存到本地缓存
        localStorage.setItem('typeId',Common.getUrlParam('id'));
        //编辑按钮点击
        $('body').on('click','#editBtn',function(){
            //商品类型名称
            if (!Validate.emptyValidateAndFocusAndColor("#classify_name", "请输入商品类型名称", "")) {
                return false;
            }
            //备注
            if (!Validate.emptyValidateAndFocusAndColor("#Remark", "请输入备注", "")) {
                return false;
            }
            typeEdit_name.EditAttributeGroup();
        })
    },
    //获取后台类型信息
    attributeGroupInfo:function(){
        var methodName = "/productSku/AttributeGroupInfo";
        var data = {
            "AttrGroupId": Common.getUrlParam('id'),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#classify_name').val(data.Data.AttributeGroupInfo.Name);
                $('#Remark').val(data.Data.AttributeGroupInfo.Remark);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //编辑后台类型信息
    EditAttributeGroup:function(){
        var methodName = "/productSku/EditAttributeGroup";
        var data = {
            "AttrGroupId": Common.getUrlParam('id'),
            "Name": $('#classify_name').val(),
            "Remark": $('#Remark').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功',function(){

                    location.href = '/classify/typeList';

                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};
$(function(){
    typeEdit_name.init()
})