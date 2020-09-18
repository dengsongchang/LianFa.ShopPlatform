$(function () {
    //订单筛选时间
    laydate.render({
        elem: '#start_time', //指定元素
        type: 'datetime'
    });
    laydate.render({
        elem: '#end_time', //指定元素
        type: 'datetime'
    });
    //删除
    $('.table_content_box').on('click','.delete_coupon',function () {
        $(this).parents('tr').remove();
        swal("删除成功", "", "success");
    });
    //复制到剪切板
    // $("#copy_btn").zclip({
    //     path:'ZeroClipboard.swf',
    //     copy:$('#txt_link_words').val(),
    //     beforeCopy:function(){
    //         //some code
    //     },
    //     copy:function(){
    //         return $('#txt_link_words').val();
    //     }
    // });
    console.log($("#copy_btn"));
    $("#copy_btn").zclip({
        path:'ZeroClipboard.swf',
        copy:$('#txt_link_words').val(),
        beforeCopy:function(){
            //some code

        },
        afterCopy:function(){
           console.log($("#txt_link_words").val());
        }
    });

});

