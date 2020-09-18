var Inbox = {

    init:function(){
        //初始化表格
        Inbox.initBootstrapTable();
        //点击删除
        $('body').on('click','.status_delete',function(){
            var id = Number($(this).attr('data-id'));
            var list = [id];
            var target = $(this);
            Common.confirmDialog('确认删除?',function(){
                Inbox.delStandInsideletter(list,target)
            })
        });
        $(".allCheck").click(function () {
            if(this.checked==true){
                $(".isCheck").each(function () {
                    // this.checked==true
                    $(this).attr("checked","checked");
                })
            }else{
                $(".isCheck").each(function () {
                    // this.checked==false;
                    $(this).removeAttr("checked");
                })
            }
        });
        //删除全部按钮传
        $('body').on('click','.deleteBtn',function(){
            var list = [];
            $('.isCheck').each(function(index,item){
                if(this.checked){
                    list.push($(item).attr('data-id'))
                }
            })
            Common.confirmDialog('确认删除?',function(){
                Inbox.delStandInsideletter(list)
            })
        });
        // 分页条数设置
        $("#pagesize_dropdown").on("change",function(){
            Inbox.projectDectoryQuery();
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#inbox_Box').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/standinsideletter/AdminStandInsideletterInboxList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],
            idField: "Id", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: Inbox.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: Inbox.responseHandler,
            columns: [
                {
                    field: 'Title',
                    title: '标题',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = `<div style="position: relative"><input type="checkbox" data-id=${row.InBoxId} style="position: absolute;left: 7px;top: 3px" class="isCheck">
                                        <span>${value}</span></div>`;
                        return e;
                    }
                },
                {
                    field: 'Sender',
                    title: '发件人',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

                        return e;
                    }
                },
                {
                    field: 'AddTimes',
                    title: '时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

                        return e;
                    }
                },
                {
                    field: 'Content',
                    title: '内容',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

                        return e;
                    }
                },
                {
                    field: 'InBoxId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var html =
                            '<a class="editor" href="/mail/reply?id='+value+'">回复</a>' +
                            '<span style="padding: 0 6px" class="status_delete" data-id="' + value + '">删除</span>';

                        return html;
                    }
                },


            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function () {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function () { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function (data) {
                console.log(data);

                $('.caret').remove()

            },
            onLoadError: function (data) {
                $('#dishes_list_table').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法
                // console.log(row);
                // console.log(tr);
                // console.log(flied);
            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {

                // for (var i = 0; i < rows.length; i++) {
                //     DishesList.UserIdsList.push(rows[i].User.Id);
                //     DishesList.UserOpenIds.push(rows[i].User.OpenId);
                // }

            },
            onUncheckAll: function (rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function (row) {


            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {


            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/standinsideletter/AdminStandInsideletterInboxList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "sdstate": 0

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.StandInsideletterInboxList,
                "total": res.Data.Total
            };
        } else {
            return {
                "rows": [],
                "total": 0
            };
        }
    },
    //表格刷新
    projectQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                "sdstate": 0
            };
        } else {
            var obj = parame;
        }

        $('#inbox_Box').bootstrapTable(
            "refresh", {
                url:SignRequest.urlPrefix + '/standinsideletter/AdminStandInsideletterInboxList',
                query: obj
            }
        );

    },
    //表格刷新
    projectDectoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {

                page: {
                    PageSize: $("#pagesize_dropdown").val(),
                    PageIndex: 1
                },
                "sdstate": 0
            };
        } else {
            var obj = parame;
        }

        $('#inbox_Box').bootstrapTable(
            "destroy", {
                url:SignRequest.urlPrefix + '/standinsideletter/AdminStandInsideletterInboxList',
                query: obj
            }
        );
        Inbox.initBootstrapTable()
    },
    //删除站内信
    delStandInsideletter:function(siLid,target){
        var methodName = "/standinsideletter/AdminDelStandInsideletter";
        var data = {
            "siLid":siLid,
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功!',function(){
                    Inbox.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }

}


$(function () {

    Inbox.init();

})