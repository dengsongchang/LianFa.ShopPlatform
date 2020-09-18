var typeList = {
    init:function(){
        typeList.initBootstrapTable();
        //点击查询按钮
        $('body').on('click','#searchBtn',function(){
            //查询
            typeList.projectDestoryQuery();
        })
        //点击删除
        $('body').on('click','.DelectBtn',function(){
            var id = $(this).attr('data-id');
            Common.confirmDialog('确认要删除?',function(){
                typeList.delAttributeGroup(id);
            })
        })
    },
    //类型删除接口
    delAttributeGroup:function(id){
        var methodName = "/productSku/DelAttributeGroup";
        var data = {
            "AttrGroupId": id
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功',function(){
                    typeList.projectQuery();
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#orderBox').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/productSku/AttributeGroupList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 10,
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
            queryParams: typeList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: typeList.responseHandler,
            columns: [

                {
                    field: 'Name',
                    title: '商品类型名称',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'Remark',
                    title: '备注',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'AttrGroupId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html =
                            '<a class="" href="/classify/typeEdit_name?id=' + value + '" style="color:#44b8fd;cursor: pointer;margin-right: 5px"  data-id="' + value + '" >编辑</a>' +
                            '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer" class="DelectBtn" data-id="' + value + '">删除</span>';
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
                $('.amount').html(data.amount);
                $('.number2').html(data.total)

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
        var methodName = "/Order/AdminOrderList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "Name": $('#classify_name').val(),



        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.AttributeGroupList,
                "total": res.Data.Total,
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
                "Name": $('#classify_name').val(),

            };
        } else {
            var obj = parame;
        }

        $('#orderBox').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/productSku/AttributeGroupList',
                query: obj
            }
        );

    },
    //表格先销毁刷新
    projectDestoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {

                Page: {
                    PageSize: 10,//页面大小,
                    PageIndex: 1,//页码
                },
                "Name": $('#classify_name').val(),

            };
        } else {
            var obj = parame;
        }

        $('#orderBox').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/productSku/AttributeGroupList',
                query: obj
            }
        );
        typeList.initBootstrapTable()
    },
};

$(function () {

    typeList.init();

});