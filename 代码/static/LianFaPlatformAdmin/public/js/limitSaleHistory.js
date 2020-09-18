    var LimitSaleHistory = {
    name: "",
    pageSize: "10",
    init: function () {
        LimitSaleHistory.initBootstrapTable();
        //查询按钮点击
        $('body').on('click', '#searchBtn', function () {
            LimitSaleHistory.name = $('#proName').val();
            LimitSaleHistory.projectDectoryQuery()
        });
        //分页条数选择
        $('body').on('change', '#pagesize_dropdown', function () {
            LimitSaleHistory.pageSize = $('#pagesize_dropdown').val();
            LimitSaleHistory.projectDectoryQuery()
        });
        //全选效果
        $('#check_total_operation_all').click(function () {
            var check_is = this.checked;
            if (this.checked) {
                $('#tb_limit_content input[type="checkbox"]').each(function (index, val) {
                    this.checked = true;
                });
            } else {
                $('#tb_limit_content input[type="checkbox"]').each(function (index, val) {
                    this.checked = false;
                });
            }
        });
        //单个删除
        $('body').on('click', '.end_limit_item', function () {
            var id = $(this).attr('data-id');
            Common.confirmDialog('是否要删除?', function () {
                LimitSaleHistory.adminDelTimeProductActivity(id)
            })

        });
        //批量删除按钮点击
        $('body').on('click', '.all_delete_box', function () {
            var list = [];
            $('.order_checkbox').each(function (index, item) {
                if (this.checked) {
                    list.push($(item).attr('data-id'))
                }
            })
            Common.confirmDialog('是否要删除?', function () {
                LimitSaleHistory.adminBatchDelTimeProductActivity(list)
            })

        });
        //更改排序
        $('body').on('change', '.sortinput', function () {
            var id = $(this).attr('data-id');
            var val = $(this).val();
            if (!val) {
                Common.showInfoMsg('不能为空')
                return false
            }
            LimitSaleHistory.adminEditTimeProductActivityDisplayOrder(id, val)
        })


    },
    //更改排序
    adminEditTimeProductActivityDisplayOrder: function (id, sort) {
        //请求方法
        var methodName = "/timeproductactivity/AdminEditTimeProductActivityDisplayOrder";
        var data = {
            "ActivityId": id,
            "DisplayOrder": sort
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('排序修改成功', function () {
                    LimitSaleHistory.projectQuery();
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //单个删除接口
    adminDelTimeProductActivity: function (id) {
        //请求方法
        var methodName = "/timeproductactivity/AdminDelTimeProductActivity";
        var data = {
            "ActivityId": id
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功', function () {
                    LimitSaleHistory.projectQuery();
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //批量删除接口
    adminBatchDelTimeProductActivity: function (list) {
        //请求方法
        var methodName = "/timeproductactivity/AdminBatchDelTimeProductActivity";
        var data = {
            "TpaIdList": list
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功', function () {
                    LimitSaleHistory.projectQuery();
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#tb_limit_content').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/timeproductactivity/AdminTimeProductActivityList',
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
            queryParams: LimitSaleHistory.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: LimitSaleHistory.responseHandler,
            columns: [
                {
                    field: 'Name',
                    title: '商品名称',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'StartTime',
                    title: '开始时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var startTime = value.replace('T'," ")

                        var e = `<span>${startTime}</span>`


                        return e;
                    }
                },
                {
                    field: 'EndTime',
                    title: '结束时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var endTime = value.replace('T'," ")

                        var e = `<span>${endTime}</span>`

                        return e;
                    }
                },
                {
                    field: 'ToSnapUpPrice',
                    title: '抢购价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e =`<span>${value}</span>`


                        return e;
                    }
                },
                {
                    field: 'HasRobNumber',
                    title: '已购总量',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e = `<span>${value}</span>`


                        return e;
                    }
                },
                {
                    field: 'DisplayOrder',
                    title: '排序',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e = `<input type="number" class="sortinput" data-id="${row.ActivityId}" value=${value} style="width:80px;text-align: center">`


                        return e;
                    }

                },
                {
                    field: 'ActivityId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = `
                                    <a href="/marketMode/limitSaleDetail?ActivityId=${value}" class="edit_limit_item">活动详情</a>
                                    <a class="end_limit_item" data-id="${value}">删除</a>
`
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
        var methodName = "/timeproductactivity/AdminTimeProductActivityList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "State": 2,
            Name: LimitSaleHistory.name,

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.TimeProductActivityList,
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
                Name: LimitSaleHistory.name,
                "State": 2,
            };
        } else {
            var obj = parame;
        }

        $('#tb_limit_content').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/timeproductactivity/AdminTimeProductActivityList',
                query: obj
            }
        );

    },
    //表格刷新
    projectDectoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                Name: LimitSaleHistory.name,
                "State": 2,
                page: {
                    PageSize: LimitSaleHistory.pageSize,
                    PageIndex: 1
                }
            };
        } else {
            var obj = parame;
        }

        $('#tb_limit_content').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/timeproductactivity/AdminTimeProductActivityList',
                query: obj
            }
        );
        LimitSaleHistory.initBootstrapTable()
    },


};

$(function () {

    LimitSaleHistory.init()


});