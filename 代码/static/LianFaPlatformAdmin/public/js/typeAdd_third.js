$(function () {
    //删除属性值
    $('.step_content_container').on('click','.delete-txt',function () {
        var this_p=$(this).parents('.property-value-span');
        Common.confirmDialog('你确定把该数据删除',function () {
            this_p.remove();
            swal("删除成功", "", "success");
        },'提示');
    });

    //点击添加属性值,记录点击的是谁
    $("#addPropertyValueModal").on("show.bs.modal", function(e) {
        //获取是哪个元素点击
        var invoker_dom = $(e.relatedTarget);
        //获取触发的该元素父元素所属的data-id
        var dataId=invoker_dom.parents('.item_add_property').attr('data-id');
        //此时给该弹窗赋予data-id，与触发元素的父元素data-id相对应
        $("#addPropertyValueModal").attr('data-id',dataId);
        //清空原来模态框中的规格值
        var property_name=invoker_dom.parents('.item_add_property').find('.property-name').text();

        $('#addPropertyValueModal .modal-title .property-title-name').text(property_name);

        $('#addPropertyVal-input').val('');

    });
    //点击 确认按钮 添加新规格值
    $('#addPropertyVal-confirm').click(function(){
        var dataId=$("#addPropertyValueModal").attr('data-id');
        var addVal=$('#addPropertyVal-input').val();
        var array_val=propertyVal_into_Array(addVal);//处理数据格式
        var html_str='';
        for(var i=0;i<array_val.length;i++){
            html_str+='<span class="property-value-span">'+
                '<a class="shuxingzhi-txt">'+array_val[i]+'</a>'+
                '<a class="delete-txt">x</a>'+
                '</span>';
        }
        $('.item_add_property[data-id='+dataId+']').find('.property-v-col').append(html_str);
        $("#addPropertyValueModal").modal('hide');
    });
    //点击 确认按钮 添加新规则名称
    $('#addProperty-confirm').click(function(){

        var addProperymingchen=$('#addProperymingchen').val();
        var addPropertyzhi=$('#addPropertyzhi').val();
        var addProperty_if=$('#addProperty-if')[0].checked;
       // addPropertyzhi=propertyVal_into_Array(addPropertyzhi);//处理数据格式
        var html_str='';
        // for(var i=0;i<addPropertyzhi.length;i++){
        //     html_str+='<span class="property-value-span">'+
        //         '<a class="shuxingzhi-txt">'+addPropertyzhi[i]+'</a>'+
        //         '<a class="delete-txt">x</a>'+
        //         '</span>';
        // }
        if(addProperty_if){
            addProperty_if='√';
        }
        else{
            addProperty_if='×';
        }
        var add_item_str='<div class="item_add_property">'+//这里动态添加时候记得添加data-id
            '<div class="col-md-2 col-xs-2 text-center">'+
            '<span class="property-name">'+addProperymingchen+'</span>'+
            '<a class="revise_span" data-toggle="modal" data-target="#revisePropertyModal">修改</a>'+
            '</div>'+
            '<div class="col-md-3 col-xs-3 property-v-col">'+html_str+'</div>'+
            '<div class="col-md-1 col-xs-1 text-center  if_verity_box">'+addProperty_if+'</div>'+
            '<div class="col-md-2 col-xs-2 text-center">'+
            '<img src="/public/images/up_order.png" alt="">'+
            '<img src="/public/images/down_order.png" alt="">'+
            '</div>'+
            '<div class="col-md-4 col-xs-4 text-center add_property_operation">'+
            '<a data-toggle="modal" data-target="#addPropertyValueModal">添加规格值</a>'+
            '<span>编辑</span>'+
            '<span class="property-delete">删除</span>'+
            '</div>'+
            '</div>';

        $('.table_body_classify').append(add_item_str);
        $('#addPropertyModal').modal('hide')


    });
    //删除属性
    $('.step_content_container').on('click','.property-delete',function () {
        var this_p=$(this).parents('.item_add_property');
        Common.confirmDialog('你确定把该数据删除',function () {
            this_p.remove();
            swal("删除成功", "", "success");
        },'提示');
    });

    //点击修改,记录点击的是谁
    $("#revisePropertyModal").on("show.bs.modal", function(e) {
        //获取是哪个元素点击
        var invoker_dom = $(e.relatedTarget);
        //获取触发的该元素父元素所属的data-id
        var dataId=invoker_dom.parents('.item_add_property').attr('data-id');
        var previous_txt=invoker_dom.parents('.item_add_property').find('.property-name').text();
        $('#reviseProperymingchen').val(previous_txt);
        //此时给该弹窗赋予data-id，与触发元素的父元素data-id相对应
        $("#revisePropertyModal").attr('data-id',dataId);
    });

    //点击确认按钮，修改完成
    $('#reviseProperty-confirm').click(function () {
        var dataId=$("#revisePropertyModal").attr('data-id');
        var new_txt=$('#reviseProperymingchen').val();
        $('.item_add_property[data-id='+dataId+']').find('.property-name').text(new_txt);
        $("#revisePropertyModal").modal('hide');

    });

});
//公共函数（处理获取出来的属性值以逗号隔开的，转成数组）
function propertyVal_into_Array(val){
    var array_val=val.split(',');
    for(var i = 0 ;i<array_val.length;i++)
    {//去除空数组元素
        if(array_val[i] == "" || typeof(array_val[i]) == "undefined")
        {
            array_val.splice(i,1);
            i= i-1;
        }
    }
    return array_val;
}
