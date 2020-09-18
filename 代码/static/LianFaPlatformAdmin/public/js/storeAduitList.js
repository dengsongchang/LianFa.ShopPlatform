var StoreList = {
    //门店名称
    storeName: "",
    //门店联系人
    userName: "",
    //门店联系电话
    userPhone: "",

    init: function () {

        //初始化表格
        StoreList.initBootstrapTable();
        //时间范围
        laydate.render({
            elem: '#time'
            , type: 'time'
            , range: true

        });
        //每页显示数量
        $("#pagesize_dropdown").on("change", function () {
            StoreList.projectDectoryQuery();
        });
        //点击删除
        $('body').on('click', '.status_delete', function () {
            var id = $(this).attr('data-id');
            var list = [id];
            Common.confirmDialog('确认要删除?', function () {
                StoreList.adminDelStore(list)
            })

        })
        //点击查询
        $('body').on('click', '#searchBtn', function () {
            StoreList.storeName = $('#storeName').val();
            StoreList.userName = $('#userName').val();
            StoreList.userPhone = $('#userPhone').val();
            StoreList.projectQuery()
        })
        //点击全选
        $('body').on('click', '#check_classify_delete_all', function () {
            if (this.checked) {
                $('.order_checkbox').each(function (index, item) {
                    this.checked = true
                })
            } else {
                $('.order_checkbox').each(function (index, item) {
                    this.checked = false
                })
            }
        });
        //排序修改
        $('body').on('change', '.order-disp', function () {
            var id = $(this).attr('data-id');
            var sort = $(this).val();
            StoreList.adminUpStoreSort(id, sort)
        })
        //全部删除按钮点击
        $('body').on('click', '#all_delte_box', function () {
            var ids = [];
            $('.order_checkbox').each(function (index, item) {
                if (this.checked) {
                    ids.push($(item).attr('data-id'))
                }
            })
            Common.confirmDialog('确认要删除?', function () {
                StoreList.adminDelStore(ids)
            })

        })
        //通过按钮点击
        $('body').on('click', '.status_pass', function () {
            var id = $(this).attr('data-id');
            StoreList.auditStore(id, true)
        })
        //拒绝按钮点击
        $('body').on('click', '.status_refund', function () {
            var id = $(this).attr('data-id');
            StoreList.auditStore(id, false)
        })


    },
    //后台删除品牌
    adminDelStore: function (id) {
        //请求方法
        var methodName = "/stores/AdminDelStore";
        var data = {
            "SId": id
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功', function () {
                    //删除成功之后刷新表格
                    StoreList.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //修改排序接口
    adminUpStoreSort: function (id, sort) {
        //请求方法
        var methodName = "/stores/AdminUpStoreSort";
        var data = {
            "SId": id,
            "ShowSort": sort
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('修改成功', function () {
                    //删除成功之后刷新表格
                    StoreList.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //审核门店
    auditStore: function (id, Ispass) {
        //请求方法
        var methodName = "/stores/AuditStore";
        var data = {
            "SId": id,
            "Ispass": Ispass
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('审核成功', function () {
                    //删除成功之后刷新表格
                    StoreList.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

    //bootstrapTable
    initBootstrapTable: function () {
        $('#brand_box').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/stores/StoreAuditList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: 1,
            pageList: [5, 10, 20, 50, 100, 500, 1000, 2000, 5000, 10000],
            idField: "Id", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            sortable: true,
            sortName: 'SId',
            sortOrder: "desc",
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: StoreList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: StoreList.responseHandler,
            columns: [
                // {
                //     field: 'SN',
                //     title: '门店编号',
                //     align: 'center',
                //     valign: 'middle',
                //     class: "addressOver",
                //     formatter: function (value, row, index) {
                //         return value;
                //     }
                // },
                {
                    field: 'Name',
                    title: '门店名称',
                    align: 'center',
                    valign: 'middle',
                    class: "addressOver",
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Contacts',
                    title: '门店联系人',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'ContactsPhone',
                    title: '门店联系电话',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var html = `${value}`

                        return html;
                    }
                },
                {
                    field: 'Address',
                    title: '门店地址',
                    align: 'center',
                    valign: 'middle',
                    class: "addressOver",
                    formatter: function (value, row, index) {

                        var html = `${value}`

                        return html;
                    }
                },
                {
                    field: 'AddTimes',
                    title: '申请时间',
                    align: 'center',
                    valign: 'middle',
                    class: "addressOver",
                    formatter: function (value, row, index) {

                        var html = `${value}`

                        return html;
                    }
                },
                {
                    field: 'Status',
                    title: '状态',
                    align: 'center',
                    valign: 'middle',
                    class: "addressOver",
                    formatter: function (value, row, index) {
                        if(row.Status == "0"){
                            var html = `开启`
                        }else if(row.Status == "1"){
                            var html = `关闭`
                        }else if(row.Status == "2"){
                            var html = `待审核`
                        }else if(row.Status == "3"){
                            var html = `审核失败`
                        }
                        return html;
                    }
                },
                {
                    field: 'SN',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    class: "operation",
                    width: 350,
                    formatter: function (value, row, index) {
                        if(row.Status == "2"){
                            var html =
                                '<span style="padding: 0 6px;color: #3c8dbc;cursor: pointer" class="status_pass " data-id="' + row.SId + '">通过</span>' +
                                '<span style="padding: 0 6px;color: #3c8dbc;cursor: pointer" class="status_refund " data-id="' + row.SId + '">拒绝</span>' +
                                '<a class="editor" href="/store/storeEdit?id=' + row.SId + '&isCheck=true">查看</a>';


                        }else{
                            var html = "";
                        }
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
        var methodName = "/stores/StoreAuditList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            pageSize: params.limit, //页面大小
            pageNumber: (params.offset / params.limit) + 1, //页码
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            sortOrder: params.order,//排序
            sortName: params.sort,//排序字段
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            Name: $('#brand_name').val(),
            Type: $('#Storetype').val(),

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.StoresList,
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
                Name: StoreList.storeName,
                Contacts: StoreList.userName,
                ContactsPhone: StoreList.userPhone,
                Type: $('#Storetype').val(),

            };
        } else {
            var obj = parame;
        }
        //方法名
        var methodName = "/stores/StoreAuditList";


        $('#brand_box').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/stores/StoreAuditList',
                query: obj
            }
        );
        // StoreList.initBootstrapTable()
    },
    projectDectoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                Name: StoreList.storeName,
                Contacts: StoreList.userName,
                ContactsPhone: StoreList.userPhone,
                Type: $('#Storetype').val(),

            };
        } else {
            var obj = parame;
        }
        //方法名
        var methodName = "/stores/StoreAuditList";


        $('#brand_box').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/stores/StoreAuditList',
                query: obj
            }
        );
        StoreList.initBootstrapTable()
    },


}


$(function () {

    StoreList.init()

})