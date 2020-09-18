$(function () {
    MemberList.init();
})

var MemberList = {
    userLabelTpl: `
        {{each UserLabelList as value i}}
        <option value="{{UserLabelList[i].LId}}">{{UserLabelList[i].Title}}</option>
        {{/each}}
    `,
    setLabelTpl: `
        {{each UserLabelList as value i}}
        <label class="checkbox-inline">
            <input class="set-checkbox" type="checkbox" value="{{UserLabelList[i].LId}}"> {{UserLabelList[i].Title}}
        </label>
        {{/each}}
    `,
    userRankTpl: `
        {{each UserRankList as value i}}
        <option value="{{UserRankList[i].UserRId}}">{{UserRankList[i].Title}}</option>
        {{/each}}
    `,
    init: function () {

        //关闭弹窗
        $(".mask").on("click", ".close", function () {
            $(".mask").hide();
        });

        //每页显示数量
        $("#pagesize_dropdown").on("change", function () {
            MemberList.projectQuery();
        });

        //查询
        $("#search").on("click", function () {
            MemberList.refreshQuery();
        });

        //单个删除
        $("#memberTable").on("click", ".status_delete", function () {
            var UId = $(this).attr("data-id");
            Common.confirmDialog("确认删除该店长吗？", function () {
                MemberList.deleteMember(UId);
            });
        });

        MemberList.initBootstrapTable();
    },

    // 删除会员
    deleteMember: function (UId) {
        var methodName = "/storeManager/AdminDelStoreManager";
        var data = {
            UId: UId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                MemberList.refreshQuery();
                Common.showSuccessMsg("删除成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //会员列表bootstrapTable
    initBootstrapTable: function () {
        $('#memberTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/storeManager/AdminStoreManagerList',
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
            queryParams: MemberList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: MemberList.responseHandler,
            columns: [

                {
                    field: 'NickName',
                    title: '店长账户',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },

                {
                    field: 'UId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = "";
                        html += "<a class='status_edit' href='/store/editMemberInfo?id=" + value + "' data-id='" + value + "' style='margin-left: 10px;'>编辑</a>";
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
    //bootstrap table post 参数 queryParams
    queryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/storeManager/AdminStoreManagerList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            UserName: $("#NickName").val(),
            Page: {
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
                "rows": res.Data.StoreManagerList,
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
    refreshQuery: function (parame) {
        //方法名
        var methodName = "/storeManager/AdminStoreManagerList";

        if (parame == "" || parame == undefined) {
            var obj = {
                UserName: $("#NickName").val(),
            };
        } else {
            var obj = parame;
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
        var methodName = "/storeManager/AdminStoreManagerList";

        if (parame == "" || parame == undefined) {
            var obj = {
                UserName: $("#NickName").val(),

                Page: {
                    PageSize: $("#pagesize_dropdown").val(),//页面大小,
                    PageIndex: 1,//页码
                }
            };
        } else {
            var obj = parame;
        }


        $('#memberTable').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );

        MemberList.initBootstrapTable();
    },


}