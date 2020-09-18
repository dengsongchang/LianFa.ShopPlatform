$(function () {
    businessList.init();
});

var businessList = {
    init: () => {
        // 编辑
        $("#memberTable").on("click", ".status_edit", function () {
            let sId = $(this).attr("data-id");
            $("#editConfirm").attr("data-id", sId);
            $("#editName").val($(this).attr("data-name"));
            $("#editContacts").val($(this).attr("data-cont"));
            $("#editContactsPhone").val($(this).attr("data-phone"));
            $('#editModal').modal('show');
        });

        // 删除
        $("#memberTable").on("click", ".status_delete", function () {
            let idList = [$(this).attr("data-id")];
            Common.confirmDialog("是否确认删除该上商家？", function () {
                businessList.deleteBusiness(idList);
            });
        });

        // 查询
        $("#search").on("click", function () {
            businessList.projectQuery();
        });

        // 新增
        $("#add").on("click", function () {
            $('#addModal').modal('show');
        });

        // 确认新增
        $("#addConfirm").on("click", function () {
            businessList.addStore();
        });

        // 确认编辑
        $("#editConfirm").on("click", function () {
            businessList.editStore();
        });

        businessList.initBootstrapTable();
    },
    // 删除
    deleteBusiness: function (idList) {
        var methodName = "/stores/AdminDelStore";
        var data = {
            SId: idList
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("删除成功");
                businessList.refreshQuery();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 新增
    addStore: function () {
        var methodName = "/stores/AdminAddStore";
        var data = {
            Name: $("#addName").val(),
            Contacts: $("#addContacts").val(),
            ContactsPhone: $("#addContactsPhone").val()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $('#addModal').modal('hide');
                businessList.refreshQuery();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 编辑
    editStore: function () {
        var methodName = "/stores/AdminUpStoreInfo";
        var data = {
            SId: $("#editConfirm").attr("data-id"),
            Name: $("#editName").val(),
            Contacts: $("#editContacts").val(),
            ContactsPhone: $("#editContactsPhone").val(),
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $('#editModal').modal('hide');
                businessList.refreshQuery();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#memberTable').bootstrapTable({
            method: 'post',  //服务器数据的请求方式 'get' or 'post'
            url: SignRequest.urlPrefix + '/stores/AdminStoreList',  //后台请求路径
            dataType: "json", //服务器返回的数据类型
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: 1, //首页页码
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],  //设置可供选择分页
            idField: "Id", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局-
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: businessList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: businessList.responseHandler,
            columns: [
                {
                    field: 'SId',
                    title: '商家ID',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Name',
                    title: '商家名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Contacts',
                    title: '联系人',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'ContactsPhone',
                    title: '联系电话',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'SId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = "";
                        html += "<span class='status_edit' data-id='" + value + "' data-name='" + row.Name + "' data-cont='" + row.Contacts + "' data-phone='" + row.ContactsPhone + "'>编辑</span>";
                        html += "<span class='status_delete' data-id='" + value + "' style='margin-left: 10px;'>删除</span>";
                        return html;
                    }
                }
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
                $('#memberTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法

            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {

                // for (var i = 0; i < rows.length; i++) {
                //     dishes_list.UserIdsList.push(rows[i].User.Id);
                //     dishes_list.UserOpenIds.push(rows[i].User.OpenId);
                // }

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
    // 用于server 分页
    responseHandler: function (res) {
        console.log(res)
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
    //bootstrap table post 参数 queryParams
    queryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/stores/AdminStoreList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            Name: $("#Name").val(),
            Contacts: $("#Contacts").val(),
            ContactsPhone: $("#ContactsPhone").val(),
            Page: {
                PageSize: $("#pagesize_dropdown").val(),
                PageIndex: (params.offset / params.limit) + 1, //页码
            }
        };
        return temp;
    },
    //表格刷新(直接刷新)
    refreshQuery: function (parame) {
        //方法名
        var methodName = "/stores/AdminStoreList";

        if (parame == "" || parame == undefined) {
            obj = {
                Name: $("#Name").val(),
                Contacts: $("#Contacts").val(),
                ContactsPhone: $("#ContactsPhone").val(),
                Page: {
                    PageSize: $("#pagesize_dropdown").val(), //页面大小,
                    PageIndex: 1, //页码
                }
            };
        } else {
            obj = parame;
        }

        $('#memberTable').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新（先销毁后初始化）
    projectQuery: function (parame) {
        //方法名
        var methodName = "/stores/AdminStoreList";
        if (parame == "" || parame == undefined) {
            obj = {
                Name: $("#Name").val(),
                Contacts: $("#Contacts").val(),
                ContactsPhone: $("#ContactsPhone").val(),
                Page: {
                    PageSize: $("#pagesize_dropdown").val(), //页面大小,
                    PageIndex: 1, //页码
                }
            };
        } else {
            obj = parame;
        }


        $('#memberTable').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );

        businessList.initBootstrapTable();
    }
}