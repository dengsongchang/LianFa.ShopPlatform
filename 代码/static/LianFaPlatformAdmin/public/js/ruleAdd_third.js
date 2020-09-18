$(function(){
    //点击修改规则值,记录点击的是谁
    $("#reviseRuleValueModal").on("show.bs.modal", function(e) {
        //获取是哪个元素点击
        var invoker_dom = $(e.relatedTarget);
        //获取触发的该元素父元素所属的data-id
        var dataId=invoker_dom.parents('.item-table').attr('data-id');
        //此时给该弹窗赋予data-id，与触发元素的父元素data-id相对应
        $("#reviseRuleValueModal").attr('data-id',dataId);
        var txt_previous=invoker_dom.parents('.item-table').find('.rule_txt_v').text();
        $('#reviseRuleVal-input').val(txt_previous);

    });
    //点击 确认按钮 修改规则值
    $('#reviseRuleVal-confirm').click(function(){
        var dataId=$("#reviseRuleValueModal").attr('data-id');
        var array_val=$('#reviseRuleVal-input').val();
        //var array_val=propertyVal_into_Array(addVal);//这里不用处理数据格式
        $('.item-table[data-id='+dataId+']').find('.rule_txt_v').text(array_val);
        $("#reviseRuleValueModal").modal('hide');
    });
    //删除属性值
    $('.table_body').on('click','.delete_rule',function () {
        var this_p=$(this).parents('.item-table');
        Common.confirmDialog('你确定把该数据删除',function () {
            this_p.remove();
            swal("删除成功", "", "success");
        },'提示');
    });
    //点击 确认按钮 添加规则值
    $('#addRuleVal-confirm').click(function(){
        var array_val=$('#addRuleVal-input').val();
        array_val=propertyVal_into_Array(array_val);//这里要处理数据格式
        var html_str='';
        for(var u=0;u<array_val.length;u++){
            html_str+='<div class="item-table">'+//这里注意要添加data-id!!!!!
                '<div class="col-xs-5 rule_txt_v">'+array_val[u]+'</div>'+
                '<div class="col-xs-3">'+
                '<img src="/public/images/up_order.png" alt="">'+
                '<img src="/public/images/down_order.png" alt="">'+
                '</div>'+
                '<div class="col-xs-4 operation_dv">'+
                '<span class="delete_rule">删除</span>'+
                '<a data-toggle="modal" data-target="#reviseRuleValueModal">修改</a>'+
                '</div>'+
                '</div>';
        }
        $('.table_body').append(html_str);
        $("#addRuleValueModal").modal('hide');
    });

    //每次添加规格值时候都清空input中原来的值
    $("#addRuleValueModal").on("show.bs.modal", function(e) {
        $('#addRuleVal-input').val('');
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