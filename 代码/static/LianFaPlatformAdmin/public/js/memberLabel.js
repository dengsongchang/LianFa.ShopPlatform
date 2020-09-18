$(function () {
    MemberLabel.init();
})


var MemberLabel = {

    IsDefault:"",


    init: function () {

        //初始化表格
        MemberLabel.initBootstrapTable();
        //完成按钮点击
        $('body').on('click', '#submitBtn', function () {
            var name = $('#txtTagName').val();
            var orderCount = $('#txtOrderCount').val();
            var OrderTotal = $('#txtOrderTotalAmount').val();

            if (!Validate.emptyValidateAndFocus("#txtTagName", "请输入标签名", "")) {
                return false;
            }
            if (!Validate.emptyValidateAndFocus("#txtOrderCount", "请输入累计成功笔数", "")) {
                return false;
            }
            if (!Validate.emptyValidateAndFocus("#txtOrderTotalAmount", "请输入累计购买金额", "")) {
                return false;
            }
            MemberLabel.adminAddUserLabel(name,orderCount,OrderTotal)
        })
        //删除按钮点击
        $('body').on('click','.status_delete',function(){
            var id = $(this).attr('data-id');
            Common.confirmDialog('是否要删除?',function(){
                MemberLabel.adminDelUserLabel(id)
            })

        })
        //Tab栏添加按钮点击
        $('body').on('click','#addLabel',function(){
            $('#txtTagName').val("");
            $('#txtOrderCount').val("");
            $('#txtOrderTotalAmount').val("");
        })

        $('input').iCheck({
            checkboxClass: 'icheckbox_flat-blue',
            radioClass: 'iradio_flat-blue',
            increaseArea: '20%' // optional
        });
        $('#stuCheckBox').on('ifChecked', function (event) { //ifCreated 事件应该在插件初始化之前绑定
            MemberLabel.IsDefault = 1;
        });
        $('#stuCheckBox2').on('ifChecked', function (event) { //ifCreated 事件应该在插件初始化之前绑定
            MemberLabel.IsDefault = 0;
        });

        //第一个选中
        $('#stuCheckBox').iCheck('check');



    },
    //后台添加会员标签
    adminAddUserLabel: function (name,orderCount,OrderTotal) {
        //请求方法
        var methodName = "/userLabel/AdminAddUserLabel";
        var data = {
            "BuyCount": orderCount,
            "AmountTotal": OrderTotal,
            "Title": name
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功', function () {
                    $('.nav-tabs a[data-type=1]').click();
                    MemberLabel.projectQuery();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //删除后台会员标签信息
    adminDelUserLabel:function(id){
        //请求方法
        var methodName = "/userLabel/AdminDelUserLabel";
        var data = {
            "LId": id
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功', function () {

                    MemberLabel.projectQuery();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });

    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#memberLabelBox').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/userLabel/AdminUserLabelList',
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
            queryParams: MemberLabel.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: MemberLabel.responseHandler,
            columns: [
                {
                    field: 'Title',
                    title: '标签名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'Number',
                    title: '会员人数',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e = '<span>' + value + '</span>'
                        return e;
                    }
                },
                {
                    field: 'LId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var html = '<a class="editor" href="/member//memberEditLabel?id=' + value + '">编辑</a>' +
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
        var methodName = "/userLabel/AdminUserLabelList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            pageSize: params.limit, //页面大小
            pageNumber: (params.offset / params.limit) + 1, //页码
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
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
                "rows": res.Data.UserLabelList,
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
            var obj = {};
        } else {
            var obj = parame;
        }
        //方法名
        var methodName = "/userLabel/AdminUserLabelList";


        $('#memberLabelBox').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/userLabel/AdminUserLabelList',
                query: obj
            }
        );
    },
}
