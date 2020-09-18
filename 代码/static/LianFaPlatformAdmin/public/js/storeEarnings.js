var withdrawalList = {
    startTime: "",
    endTime: "",
    init: function () {
        withdrawalList.initBootstrapTable();
        //订单筛选时间
        laydate.render({
            elem: '#start_time', //指定元素
            type: 'datetime'
        });
        laydate.render({
            elem: '#end_time', //指定元素
            type: 'datetime'
        });
        //查询按钮点击事件
        $('body').on('click', '#seachBtn', function () {
            withdrawalList.startTime = $('#start_time').val() ? moment($('#start_time').val()).format("X") : $('#start_time').val();
            withdrawalList.endTime = $('#end_time').val() ? moment($('#end_time').val()).format("X") : $('#end_time').val();
            withdrawalList.projectQuery()
        });
        //提现按钮点击，模态框出现
        $('body').on('click','#withdrawalBtn',function(){
            $('#myWithdrawalModal').modal('show');
        })
        //确认按钮点击
        $('body').on('click','#submitBtn',function(){
            if($('#money').val() == ""){
                Common.showInfoMsg('请输入金额')
                return false
            }
            withdrawalList.merchantsAddWithdrawCashHistory();
        })
        //查看备注按钮点击
        $('body').on('click','.seeBtn',function(){
            var text = $(this).attr('data-text');
            $('.remark1').val(text);
            $('#mySeeModal').modal("show");
        })
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#orderBox').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/storeAmount/AdminStoreAmountList',
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
            queryParams: withdrawalList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: withdrawalList.responseHandler,
            columns: [

                {
                    field: 'OSn',
                    title: '订单编号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span style="display:block;position: relative">' +
                            '<a style="color:#44b8fd" href="#">' + value + '</a></span>';
                        return e;
                    }
                },
                {
                    field: 'StoreName',
                    title: '门店名称',
                    align: 'center',
                    valign: 'middle',

                },
                {
                    field: 'AddTimes',
                    title: '交易时间',
                    align: 'center',
                    valign: 'middle',

                },
                {
                    field: 'Amount',
                    title: '金额',
                    align: 'center',
                    valign: 'middle',

                },
                {
                    field: 'Event',
                    title: '来源',
                    align: 'center',
                    valign: 'middle',

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
                $('.amount').html(data.AmountSum);

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
        var methodName = "/storeAmount/AdminStoreAmountList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            StartTime: withdrawalList.startTime,
            EndTime: withdrawalList.endTime,
            "IsIncome": $('#state_box').val(),
            "SId": 0,

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.AdminStoreAmountList,
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
                StartTime: withdrawalList.startTime,
                EndTime: withdrawalList.endTime,
                "IsIncome": $('#state_box').val(),
                "SId": 0,

            };
        } else {
            var obj = parame;
        }

        $('#orderBox').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/storeAmount/AdminStoreAmountList',
                query: obj
            }
        );

    },
    //表格先销毁刷新
    projectDestoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                Page: {
                    PageSize: $("#pagesize_dropdown").val(),//页面大小,
                    PageIndex: 1,//页码
                },
                StartTime: withdrawalList.startTime,
                EndTime: withdrawalList.endTime,
                "IsIncome": $('#state_box').val(),
                "SId": 0,

            };
        } else {
            var obj = parame;
        }

        $('#orderBox').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/storeAmount/AdminStoreAmountList ',
                query: obj
            }
        );
        withdrawalList.initBootstrapTable()
    },
};
$(function () {

    withdrawalList.init()
})