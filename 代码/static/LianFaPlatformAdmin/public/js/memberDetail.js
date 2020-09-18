var MemberDetail = {
    integralTpl: `
        {{each CreditActionInfoList as value i}}
        <option value="{{CreditActionInfoList[i].ActionId}}">{{CreditActionInfoList[i].ActionName}}</option>
        {{/each}}
    `,
    init: function () {
        //初始化表格
        MemberDetail.initBootstrapTable();
        MemberDetail.getIntegralLogin();
        //查询按钮点击
        $('body').on('click','#searchBtn',function(){
            var data = {
                "CreditAction":$("#orginSelect").val(),
                "UId": Common.getUrlParam("id")
            };
            MemberDetail.projectQuery(data);
        });
    //    点击导出按钮
        $(".integral-btn").on("click","#exportBtn",function () {
           var uId=Common.getUrlParam("id");
            MemberDetail.exportCreditList(uId);
        });

    },
    //获得积分来源
    getIntegralLogin:function(){
        var methodName = "/credit/AdminGetUserCreditActionList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(MemberDetail.integralTpl);
                var html = render(data.Data);
                $("#orginSelect").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //导出积分明细列表
    exportCreditList:function (uid) {
        //请求方法
        var methodName = "/credit/ExportAdminCreditDetailList";
        var data = {
            "Page": {
                "PageSize": 10,
                "PageIndex": 1
            },
            "CreditAction": 0,
            "UId": Common.getUrlParam("id")
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('导出成功');
                window.location.href=data.Data;
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#detailTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/credit/AdminGetUserCreditDetailList',
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
            queryParams: MemberDetail.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: MemberDetail.responseHandler,
            columns: [
                {
                    field: 'Title',
                    title: '积分来源',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';
                        return e;
                    }
                },
                {
                    field: 'PayCredits',
                    title: '积分变化',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

                        return e;
                    }
                },
                {
                    field: 'ActionTime',
                    title: '时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var time=moment(value).format("YYYY-MM-DD HH:mm:ss");

                        var e = '<span>'+time+'</span>';

                        return e;
                    }
                },
                {
                    field: 'ActionDes',
                    title: '备注',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

                        return e;
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
                // console.log(data);

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
        var methodName = "/credit/AdminGetUserCreditDetailList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "CreditAction": 0,
            "UId":Common.getUrlParam("id")

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res.Data);
            $("#memberName").text(res.Data.UserCreditDetailInfo.UserName);
            $("#memberCredit").text(res.Data.UserCreditDetailInfo.PayCredits);
            $("#historyCredit").text(res.Data.UserCreditDetailInfo.HistoryCredits);
            return {
                "rows": res.Data.UserCreditDetailList,
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
                "CreditAction": 0,
                "UId":Common.getUrlParam("id")
            };
        } else {
            var obj = parame;
        }

        $('#detailTable').bootstrapTable(
            "refresh", {
                url:SignRequest.urlPrefix + '/credit/AdminGetUserCreditDetailList',
                query: obj
            }
        );

    },


};


$(function () {

    MemberDetail.init();

})







