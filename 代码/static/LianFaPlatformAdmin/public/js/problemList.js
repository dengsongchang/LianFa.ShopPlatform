$(function () {
    ProblemList.init();
})

var ProblemList = {

    init: function () {
        // 初始化表格
        ProblemList.initBootstrapTable();
        // 单个删除
        $("#brand_box").on("click", ".state_delete", function () {
            var hId = [];
            hId.push($(this).attr("data-id"));
            Common.confirmDialog("确认删除该数据吗？", function () {
                ProblemList.deleteProblem(hId);
            });
        });
        //保存按钮点击
        $('body').on('click', '#nextStep', function () {
            var title = $("#ProblemTitle").val();
            var description = $("#description").val();
            var displayOrder = $("#sort").val();
            //标题验证
            if (!Validate.emptyValidateAndFocus("#ProblemTitle", "请输入标题名称", "")) {
                return false;
            }
            //描述验证
            if (!Validate.emptyValidateAndFocus("#description", "请输入问题描述", "")) {
                return false;
            }
            //排序验证
            if (!Validate.emptyValidateAndFocus("#sort", "请输入排序", "")) {
                return false;
            }
            ProblemList.addProblem(title, description, displayOrder);
        });
        // 修改常见问题排序helps/AdminUpHeplesSort
        $('body').on('change','.order-disp',function(){
            var HId = $(this).attr('data-id');
            var DisplayOrder = $(this).val();
            ProblemList.adminChangeOrder(HId,DisplayOrder)
        });
    },

    // 删除数据
    deleteProblem:function (HId) {
        var methodName = "/helps/AdminDelHeples";
        var data = {
            HId: HId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                ProblemList.projectQuery();
                Common.showSuccessMsg("删除成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 添加问题
    addProblem:function (title, description, displayOrder) {
        var methodName = "/helps/AdminAddHeples";
        var data = {
            Title: title,
            DisplayOrder: displayOrder,
            Description: description
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功', function () {
                    location.href = '/homePage/problem_list';
                })
                // $("#second input").val("");
                // $("#description").val("");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //更改问题排序
    adminChangeOrder:function (HId,DisplayOrder) {
        var methodName = "/helps/AdminUpHeplesSort";
        var data = {
            "HId": HId,
            "DisplayOrder": DisplayOrder
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('排序成功', function () {
                    ProblemList.projectQuery();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        })
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#brand_box').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/helps/AdminHeplesList',
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
            queryParams: ProblemList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class\
            responseHandler: ProblemList.responseHandler,
            columns: [{
                field: 'HId',
                title: '编号',
                align: 'center',
                valign: 'middle',
                formatter: function (value, row, index) {
                    var e = '<span style="display:block;position: relative">' + value + '</span>';
                    return e;
                }
            },
                {
                    field: 'Title',
                    title: '标题',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e = '<span>' + value + '</span>';
                        return e;
                    }
                },
                {
                    field: 'DisplayOrder',
                    title: '排序',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = `<input type="number" style="text-align: center" class="order-disp" data-id="${row.HId}" value="${value}">`
                        return html;
                    }
                },
                {
                    field: 'HId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '';
                        html += '<a class="editor" href="/homePage/problemEditor?id=' + value + '" style="margin-right: 10px;">编辑</a>';
                        html += '<a class="state_delete" data-id="'+value+'" style="cursor: pointer;">删除</a>';
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
        var methodName = "/helps/AdminHeplesList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.AdminHelpsList,
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

            };
        } else {
            var obj = parame;
        }
        //方法名
        var methodName = "/helps/AdminHeplesList";
        $('#brand_box').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },

}