
$(function(){

    marketQuota.init()

})

var marketQuota={
    init:function () {
        // 编辑
        $("#manageTable").on("click",".status_edit",function () {
            var Pmid = $(this).attr("data-id");
            var PType = $(this).attr("data-type");
            location.href = "/marketMode/marketQuotaEdit?Pmid=" + Pmid + "&PType=" + PType;
        });

        // 删除
        $("#manageTable").on("click",".status_delete",function () {
            var Pmid = $(this).attr("data-id");
            var PType = $(this).attr("data-type");
            Common.confirmDialog("确认进行删除操作吗？",function(){
                marketQuota.deleteQuota(Pmid,PType);
            });
        });

        marketQuota.initBootstrapTable();
    },
    // 删除优惠活动
    deleteQuota:function (Pmid,PType) {
        //请求方法
        var methodName = "/promotion/DelFullQuotaPromotion";
        var data = {
            Pmid: Pmid,
            PType: PType
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                marketQuota.refreshQuery();
                Common.showSuccessMsg("删除成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable:function(){
        $('#manageTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/promotion/FullQuotaPromotionList',
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
            queryParams: marketQuota.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: marketQuota.responseHandler,
            columns: [
                {
                    field: 'Name',
                    title: '活动名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'PTypeName',
                    title: '活动类型',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'StartTime',
                    title: '开始时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value.replace("T"," ").substring(0,19);
                    }
                },
                {
                    field: 'EndTime',
                    title: '结束时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value.replace("T"," ").substring(0,19);
                    }
                },
                {
                    field: 'PMId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = "";
                        html += "<span class='status_edit' data-id='" + value + "' data-type='"+ row.PType +"'>编辑</span>";
                        html += "<span class='status_delete' data-id='" + value + "' data-type='"+ row.PType +"'style='margin-left: 10px;'>删除</span>";
                        return html;
                    }
                }
            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function() {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function() { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function(data) {
                console.log(data);

                $('.caret').remove()

            },
            onLoadError: function(data) {
                $('#manageTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function(row, tr, flied) {
                // 书写自己的方法

            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function(rows) {

                // for (var i = 0; i < rows.length; i++) {
                //     dishes_list.UserIdsList.push(rows[i].User.Id);
                //     dishes_list.UserOpenIds.push(rows[i].User.OpenId);
                // }

            },
            onUncheckAll: function(rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function(row) {


            },
            //取消每一个单选框时对应的操作；
            onUncheck: function(row) {
                Array.prototype.remove = function(val) {
                    var index = this.indexOf(val);
                    if (index > -1) {
                        this.splice(index, 1);
                    }
                };

            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryParams: function(params) {
        //配置参数
        //方法名
        var methodName = "/promotion/FullQuotaPromotionList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,//页面大小,
                PageIndex: (params.offset / params.limit) + 1,//页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function(res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.FullQuotaPromotionInfoList,
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
    refreshQuery: function(parame) {
        //方法名
        var methodName = "/promotion/FullQuotaPromotionList";

        if (parame == "" || parame == undefined) {
            var obj = {
                // Page: {
                //     PageSize: 10,//页面大小,
                //     PageIndex: 1,//页码
                // }
            };
        } else {
            var obj = parame;
        }

        $('#manageTable').bootstrapTable(
            "refresh", {
                url:SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    }
};

