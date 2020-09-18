$(function () {
    historicalWithdrawal.init();
})

var historicalWithdrawal = {
    //分销员id
    UId:0,
    gradeTpl: `
         <option value="0">请选择</option>
        {{each AdminDistributionGradeList as value i}}
            <option value="{{AdminDistributionGradeList[i].GradeId}}">{{AdminDistributionGradeList[i].Title}}</option>
        {{/each}}
    `,
    init: function () {
        // 日期控件初始化
        Common.initLaydateWithTime();
        historicalWithdrawal.initBootstrapTable();
        //表格分页每页显示数据
        $("#pagesize_dropdown").on("change", function () {
            historicalWithdrawal.projectDestroyQuery();
        });
        //查询按钮点击
        $('body').on('click', '#search', function () {
            historicalWithdrawal.projectDestroyQuery();
        })


    },

    //导出后台分销员列表
    exportAdminDistributionList:function(){
        var methodName = "/Distribution/ExportAdminDistributionList";
        var data = {
            "UserName": $('#UserName').val(),
            "SatrtTime": $('#start').val() ? moment($('#start').val()).format("X") : "",
            "EndTime": $('#end').val() ? moment($('#start').val()).format("X") : "",
            "IsRepeled": false,
            "DistGradeId": 0,
            "DisplayOrder": 0,
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
            url: SignRequest.urlPrefix + '/SplitCommWithdraw/AdminSplitCommWithdrawHistory',
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
            queryParams: historicalWithdrawal.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: historicalWithdrawal.responseHandler,
            columns: [
                {
                    field: 'NickName',
                    title: '分销员名称',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'RequestTimeStr',
                    title: '申请时间',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'Amount',
                    title: '申请金额',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'UserName',
                    title: '提现人名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Remark',
                    title: '银行',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Account',
                    title: '银行卡账号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'AuditStatusStr',
                    title: '状态',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'AccountTimeStr',
                    title: '结算时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'ManagerRemark',
                    title: '管理员备注',
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

                $('.caret').remove()

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
        var methodName = "/SplitCommWithdraw/AdminSplitCommWithdrawHistory";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            "NickName": $('#UserName').val(),
            "StartTime": $('#start').val() ? moment($('#start').val()).format("X") : "",
            "EndTime": $('#end').val() ? moment($('#end').val()).format("X") : "",
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
                "rows": res.Data.List,
                "total": res.Data.Total
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
        var methodName = "/SplitCommWithdraw/AdminSplitCommWithdrawHistory";

        if (parame == "" || parame == undefined) {
            var obj = {
                "NickName": $('#UserName').val(),
                "StartTime": $('#start').val() ? moment($('#start').val()).format("X") : "",
                "EndTime": $('#end').val() ? moment($('#end').val()).format("X") : "",
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
        var methodName = "/SplitCommWithdraw/AdminSplitCommWithdrawHistory";

        if (parame == "" || parame == undefined) {
            var obj = {
                "NickName": $('#UserName').val(),
                "StartTime": $('#start').val() ? moment($('#start').val()).format("X") : "",
                "EndTime": $('#end').val() ? moment($('#end').val()).format("X") : "",
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

        historicalWithdrawal.initBootstrapTable();

    },
}
