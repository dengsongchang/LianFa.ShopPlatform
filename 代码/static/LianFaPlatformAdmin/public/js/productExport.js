$(function () {
    ProductExport.init();
})

var ProductExport = {
    cateTpl: `
        {{each CategoryList as value i}}
            <option value="{{CategoryList[i].CateId}}">{{CategoryList[i].Name}}</option>
        {{/each}}
    `,
    isOnSale: "",
    isOutSale: "",
    pId:0,
    PidList:[],
    init: function () {
        ProductExport.initBootstrapTable();
        // 初始化日期控件
        Common.initLaydateWithTime();

        // 初始化switch开关控件
        Common.initSwitch();

        ProductExport.getProductCategory();

        //点击除外按钮
        $(".content").on("click",".status_delete",function () {
            ProductExport.pId=parseInt($(this).attr("data-id"));

            console.log(ProductExport.pId);

            if ($('#onSell').is(':checked')) {
                ProductExport.isOnSale = 1;
            } else {
                ProductExport.isOnSale = 0;
            }
            if ($('#outSell').is(':checked')) {
                ProductExport.isOutSale = 1;
            } else {
                ProductExport.isOutSale = 0;
            }
            ProductExport.PidList.push(ProductExport.pId);
            var data = {
                "Name": $("#goodsName").val(),
                "PSn": $("#goodsCode").val(),
                "StartTime": $("#start").val(),
                "EndTime": $("#end").val(),
                "CateId": parseInt($("#CateId").val()),
                "IsOnSale": ProductExport.isOnSale,
                "IsOutSale": ProductExport.isOutSale,
                "PidList":ProductExport.PidList,
            };
            ProductExport.projectQuery(data);
        });

        //    点击查询按钮
        $(".option-list").on("click", "#lookFor", function () {
            if ($('#onSell').is(':checked')) {
                ProductExport.isOnSale = 1;
            } else {
                ProductExport.isOnSale = 0;
            }
            if ($('#outSell').is(':checked')) {
                ProductExport.isOutSale = 1;
            } else {
                ProductExport.isOutSale = 0;
            }
            var data = {
                "Name": $("#goodsName").val(),
                "PSn": $("#goodsCode").val(),
                "StartTime": $("#start").val(),
                "EndTime": $("#end").val(),
                "CateId": parseInt($("#CateId").val()),
                "IsOnSale": ProductExport.isOnSale,
                "IsOutSale": ProductExport.isOutSale,
                "PidList": [
                    0
                ],
            };
            ProductExport.projectQuery(data);
        })

        //点击导出数据按钮
        $(".operate-box").on("click", "#exportBtn", function () {
            if ($('#onSell').is(':checked')) {
                ProductExport.isOnSale = 1;
            } else {
                ProductExport.isOnSale = 0;
            }
            if ($('#outSell').is(':checked')) {
                ProductExport.isOutSale = 1;
            } else {
                ProductExport.isOutSale = 0;
            }
            var name = $("#goodsName").val();
            var pSn = $("#goodsCode").val();
            var startTime = $("#start").val();
            var endTime = $("#end").val();
            var cateId = parseInt($("#CateId").val());
            var isOnSale = ProductExport.isOnSale;
            var isOutSale = ProductExport.isOutSale;
            var pidList = ProductExport.PidList;
            if($('#switch').hasClass('switch-on')){
                var isExportImg = 1;
            }else{
                var isExportImg = 0;
            }
            ProductExport.exportProductList(name,pSn,startTime,endTime,cateId,isOnSale,isOutSale,isExportImg,pidList);
        });
    },
    // 获取商品分类
    getProductCategory: function () {
        var methodName = "/category/AdminCategoryList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            console.log()
            if (data.Code == "100") {
                var render = template.compile(ProductExport.cateTpl);
                var html = render(data.Data);
                $("#CateId").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

    //导出商品列表数据
    exportProductList: function (name,psn,starttime,endtime,cateid,isonsale,isoutsale,isexportimg,pidlist) {
        var methodName = "/product/ExportAdminProductList";
        var data = {
            "Name": name,
            "PSn": psn,
            "StartTime": starttime,
            "EndTime": endtime,
            "CateId": cateid,
            "IsOnSale": isonsale,
            "IsOutSale": isoutsale,
            "IsExportImg": isexportimg,
            "PidList": pidlist,
        };
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
        $('#cashTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/product/AdminProductExportList',
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
            queryParams: ProductExport.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: ProductExport.responseHandler,
            columns: [
                {
                    field: 'ShowImg',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<img src=' + value + ' class="brandImg">';

                        return e;
                    }
                },
                {
                    field: 'Name',
                    title: '商品',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'PSn',
                    title: '商家编码',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'PNumber',
                    title: '库存',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'ShopPrice',
                    title: '价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'PId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var html = '<span  class="status_delete" data-id="'+value+'">除外</span>';

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
                $('.exportNum').text(data.total)

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
        var methodName = "/product/AdminProductExportList";

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
            "Name": "",
            "PSn": "",
            "StartTime": "",
            "EndTime": "",
            "CateId": 0,
            "IsOnSale": 1,
            "IsOutSale": 0,
            "PidList": [ProductExport.pId],
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.ExportProductList,
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
        var methodName = "/product/AdminProductExportList";


        $('#cashTable').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/product/AdminProductExportList',
                query: obj
            }
        );
    },


}