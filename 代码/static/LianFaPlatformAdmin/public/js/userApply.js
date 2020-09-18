$(function () {
    userApply.init();
})

var userApply = {
    init: function () {
        // 日期控件初始化
        Common.initLaydateWithTime();
        userApply.initBootstrapTable();
        //表格分页每页显示数据
        $("#pagesize_dropdown").on("change", function () {
            userApply.projectDestroyQuery();
        });
        //查询按钮点击
        $('body').on('click', '#search', function () {
            userApply.projectDestroyQuery();
        })
        //审核按钮点击
        $('body').on('click','.checkBtn',function(){
            var id = $(this).attr('data-id');
            userApply.RecordId = id;
        })
        //审核的确认按钮点击
        $('body').on('click','#sureBtn',function(){

            if($('input[name="saleState"]:checked').val() == "1"){
                //拒绝
                //备注
                if (!Validate.emptyValidateAndFocusAndColor(".remark", "请输入备注", "")) {
                    return false;
                }
                userApply.adminAuditDistribution()
            }else{
                //通过
                userApply.adminAuditDistribution()
            }
        })
    },

    //后台审核分销员
    adminAuditDistribution:function(){
        var methodName = "/DistributionAudit/AdminAuditDistribution";
        var data = {
            "RecordId":userApply.RecordId ,
            "State": $('input[name="saleState"]:checked').val(),
            "RefusalReason": $('.remark').val(),
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('审核成功', function () {
                    userApply.projectQuery();
                    $('#myCheckModal').modal('hide');
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
            url: SignRequest.urlPrefix + '/DistributionAudit/AdminDistributionAuditList',
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
            queryParams: userApply.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: userApply.responseHandler,
            columns: [
                {
                    field: 'UserName',
                    title: '用户名',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'RequetReason',
                    title: '描述',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'ApplyTime',
                    title: '申请时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'RecordId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var html = `
                                     <span class="status_delete checkBtn" data-id="${value}" data-toggle="modal" data-target="#myCheckModal" >审核</span>
                                    `
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
        var methodName = "/DistributionAudit/AdminDistributionAuditList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            "UserName": $('#UserName').val(),
            "ApplyStartTime": $('#start').val(),
            "ApplyEndTime": $('#end').val(),
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
                "rows": res.Data.AdminDistributionAuditList,
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
        var methodName = "/DistributionAudit/AdminDistributionAuditList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "UserName": $('#UserName').val(),
                "ApplyStartTime": $('#start').val(),
                "ApplyEndTime": $('#end').val(),
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
        var methodName = "/DistributionAudit/AdminDistributionAuditList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "UserName": $('#UserName').val(),
                "ApplyStartTime": $('#start').val(),
                "ApplyEndTime": $('#end').val(),
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

        userApply.initBootstrapTable();

    }
}
