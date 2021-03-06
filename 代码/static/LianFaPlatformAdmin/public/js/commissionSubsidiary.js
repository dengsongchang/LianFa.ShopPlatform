$(function () {
    commissionSubsidiary.init();
})

var commissionSubsidiary = {
    init: function () {

        commissionSubsidiary.initBootstrapTable();
        //订单筛选时间
        laydate.render({
            elem: '#start_time', //指定元素
            type: 'datetime'
        });
        laydate.render({
            elem: '#end_time', //指定元素
            type: 'datetime'
        });
        laydate.render({
            elem: '#start_timeForCommission', //指定元素
            type: 'datetime'
        });
        laydate.render({
            elem: '#end_timeForCommission', //指定元素
            type: 'datetime'
        });
        //表格分页每页显示数据
        $("#pagesize_dropdown").on("change", function () {
            commissionSubsidiary.projectDestroyQuery();
        });
        //查询按钮点击
        $('body').on('click', '#seachBtn', function () {
            commissionSubsidiary.projectQuery();
        })
        //导出按钮点击
        $('body').on('click','#exportBtn',function(){
            commissionSubsidiary.exportAdminAmountDetailList();
        })


    },
    //导出后台佣金明细列表
    exportAdminAmountDetailList:function(){
        var methodName = "/Distribution/ExportAdminAmountDetailList";
        var data = {
            "DistUId":Common.getUrlParam('id'),
            "OSn": $('#order_num').val(),
            "PaySatrtTime": $('#start_time').val(),
            "PayEndTime": $('#end_time').val(),
            "UserName": $('#account_name').val(),
            "Type": $('#state_box').val(),
            "SetSatrtTime": $('#start_timeForCommission').val(),
            "SetEndTime": $('#end_timeForCommission').val(),
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('导出成功', function () {
                    location.href = data.Data;
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#productTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/Distribution/AdminAmountDetailList',
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
            queryParams: commissionSubsidiary.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: commissionSubsidiary.responseHandler,
            columns: [
                {
                    field: 'OSn',
                    title: '订单编号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'NickName',
                    title: '下单者昵称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'SetTime',
                    title: '购物积分结算时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'TypeDesc',
                    title: '积分来源',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Amount',
                    title: '订单金额',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Income',
                    title: '购物积分',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
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
                if(data.info != null && data.info != undefined){
                    $('#distributionMember').text(data.info.UserName);
                    $('#withdrawalMoney').text(data.info.CurrentAmount);
                    $('#NowithdrawalMoney').text(data.info.NoSettlementAmount);
                    $('#directInferiorNumber').text(data.info.SubUserCount);
                    $('#SubUserAmount').text(data.info.SubUserAmount);
                    $('#CumulativeAmount').text(data.info.CumulativeAmount);
                    $('#DistUserName').text(data.info.DistUserName);
                }

            },
            onLoadError: function (data) {
                $('#productTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法

            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {


            },
            onUncheckAll: function (rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function (row) {


            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {
                Array.prototype.remove = function (val) {
                    var index = this.indexOf(val);
                    if (index > -1) {
                        this.splice(index, 1);
                    }
                };

            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/Distribution/AdminAmountDetailList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            "DistUId":Common.getUrlParam('id'),
            "OSn": $('#order_num').val(),
            "PaySatrtTime": $('#start_time').val(),
            "PayEndTime": $('#end_time').val(),
            "UserName": $('#account_name').val(),
            "Type": $('#state_box').val(),
            "SetSatrtTime": $('#start_timeForCommission').val(),
            "SetEndTime": $('#end_timeForCommission').val(),
            PageModel: {
                PageSize: params.limit,//页面大小,
                PageIndex: (params.offset / params.limit) + 1,//页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.AdminAmountDetailList,
                "total": res.Data.Total,
                "info":res.Data.AdminDistributionDetailInfo,
            };
        } else {
            return {
                "rows": [],
                "total": 0
            };
        }
    },
    //表格刷新(直接刷新)
    projectQuery: function (parame) {
        //方法名
        var methodName = "/Distribution/AdminAmountDetailList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "DistUId":Common.getUrlParam('id'),
                "OSn": $('#order_num').val(),
                "PaySatrtTime": $('#start_time').val(),
                "PayEndTime": $('#end_time').val(),
                "UserName": $('#account_name').val(),
                "Type": $('#state_box').val(),
                "SetSatrtTime": $('#start_timeForCommission').val(),
                "SetEndTime": $('#end_timeForCommission').val(),
            };
        } else {
            var obj = parame;
        }

        $('#productTable').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新(先销毁后初始化)
    projectDestroyQuery: function (parame) {
        //方法名
        var methodName = "/Distribution/AdminAmountDetailList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "DistUId":Common.getUrlParam('id'),
                "OSn": $('#order_num').val(),
                "PaySatrtTime": $('#start_time').val(),
                "PayEndTime": $('#end_time').val(),
                "UserName": $('#account_name').val(),
                "Type": $('#state_box').val(),
                "SetSatrtTime": $('#start_timeForCommission').val(),
                "SetEndTime": $('#end_timeForCommission').val(),
                PageModel: {
                    PageSize: $("#pagesize_dropdown").val(),//页面大小,
                    PageIndex: 1,//页码
                }
            };
        } else {
            var obj = parame;
        }

        $('#productTable').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );

        commissionSubsidiary.initBootstrapTable();

    }
}
