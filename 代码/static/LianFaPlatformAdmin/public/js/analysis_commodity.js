var AnalysisCommodity = {
    //排序类别
    DisplayOrderOptions:"VisitCount",
    //排序方式
    DisplayOrder:"asc",
    //开始时间
    start:"",
    //结束时间
    end:"",

    init: function () {

        //初始化商品类目分析
        var startTime = moment().subtract('days', 1).format('YYYY-MM-D');
        var endTime = moment().format('YYYY-MM-D');
        AnalysisCommodity.start = moment().subtract('days', 1).format('YYYY-MM-D');
        AnalysisCommodity.end = moment().format('YYYY-MM-D');
        AnalysisCommodity.adminGetFcProductList(startTime,endTime);
        AnalysisCommodity.initBootstrapTable()


        //商品类目销售分析日历
        var option_send_type = {
            locale: {
                fromLabel: '开始日期',
                toLabel: '结束日期'
            },
            maxDate: new Date(new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + (new Date().getDate() + 1)), //双日历允许最大的结束日期
            opens: 'left', //日历与输入框的对其方式,当前为右对齐
            showDropdowns: true, //这个属性可以实现下拉选择年
            minDate: "2014-12-01"
        };

        $('CalendarWatch ').html(moment().subtract('days', 29).format('MM-DD-YY') + ' - ' + moment().format('YYYY-MM-D'));

        $('#CalendarWatch').daterangepicker(option_send_type, function (start, end, label) {
            $('#CalendarWatch').html(moment(start).format('MM-DD-YY') + ' - ' + moment(end).format('YYYY-MM-D'));
            var startTime = moment(start).format('YYYY-MM-D');
            var endTime = moment(end).format('YYYY-MM-D');
            AnalysisCommodity.adminGetFcProductList(startTime,endTime)

        });

        //商品销售情况日历
        var option_send_type = {
            locale: {
                fromLabel: '开始日期',
                toLabel: '结束日期'
            },
            maxDate: new Date(new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + (new Date().getDate() + 1)), //双日历允许最大的结束日期
            opens: 'left', //日历与输入框的对其方式,当前为右对齐
            showDropdowns: true, //这个属性可以实现下拉选择年
            minDate: "2014-12-01"
        };

        $('calendar ').html(moment().subtract('days', 29).format('MM-DD-YY') + ' - ' + moment().format('YYYY-MM-D'));

        $('#calendar').daterangepicker(option_send_type, function (start, end, label) {
            $('#calendar').html(moment(start).format('MM-DD-YY') + ' - ' + moment(end).format('YYYY-MM-D'));
            var startTime = moment(start).format('YYYY-MM-D');
            var endTime = moment(end).format('YYYY-MM-D');
            AnalysisCommodity.start = startTime;
            AnalysisCommodity.end = endTime;
            var data = {
                StartTime:startTime,
                EndTime:endTime,
                DisplayOrderOptions:AnalysisCommodity.DisplayOrderOptions,
                DisplayOrder:AnalysisCommodity.DisplayOrder
            };
            AnalysisCommodity.projectQuery(data)

        });

        $(".img_toggle").click(function () {
            $(this).parents().next(".row").find('.chart_1').slideToggle();
            $(this).children().toggle();
        });
        //tips
        $(".queryImg").on("mouseenter", function () {
            $(this).parents(".r_title").find(".tipBox").css("display", "block");
        });
        $(".queryImg").on("mouseleave", function () {
            $(this).parents(".r_title").find(".tipBox").css("display", "none");
        });

        //商品类目销售分析的最近日期点击
        $('body').on('click','#dateUl li',function(){
            $(this).addClass('active').siblings('li').removeClass('active');
            var type = $(this).attr('svalue');
            if(type == "inOneDay"){
                var startTime = moment().subtract('days', 1).format('YYYY-MM-D');
                var endTime = moment().format('YYYY-MM-D');
                AnalysisCommodity.adminGetFcProductList(startTime,endTime)
            }else if(type == "inOneWeek"){
                var startTime = moment().subtract('days', 7).format('YYYY-MM-D');
                var endTime = moment().format('YYYY-MM-D');
                AnalysisCommodity.adminGetFcProductList(startTime,endTime)
            }else if(type == "inOneMonth"){
                var startTime = moment().subtract('days', 30).format('YYYY-MM-D');
                var endTime = moment().format('YYYY-MM-D');
                AnalysisCommodity.adminGetFcProductList(startTime,endTime)
            }
        });
        //商品销售情况最近日期点击
        $('body').on('click','#dateUl1 li',function(){
            $(this).addClass('active').siblings('li').removeClass('active');
            var type = $(this).attr('svalue');
            if(type == "inOneDay"){
                AnalysisCommodity.start = moment().subtract('days', 1).format('YYYY-MM-D');
                AnalysisCommodity.end = moment().format('YYYY-MM-D');
                var data = {
                    StartTime:AnalysisCommodity.start,
                    EndTime:AnalysisCommodity.end,
                    DisplayOrderOptions:AnalysisCommodity.DisplayOrderOptions,
                    DisplayOrder:AnalysisCommodity.DisplayOrder
                };
                AnalysisCommodity.projectQuery(data)
            }else if(type == "inOneWeek"){
                AnalysisCommodity.start = moment().subtract('days',7).format('YYYY-MM-D');
                AnalysisCommodity.end = moment().format('YYYY-MM-D');
                var data = {
                    StartTime:AnalysisCommodity.start,
                    EndTime:AnalysisCommodity.end,
                    DisplayOrderOptions:AnalysisCommodity.DisplayOrderOptions,
                    DisplayOrder:AnalysisCommodity.DisplayOrder
                };
                AnalysisCommodity.projectQuery(data)
            }else if(type == "inOneMonth"){
                AnalysisCommodity.start = moment().subtract('days', 30).format('YYYY-MM-D');
                AnalysisCommodity.end = moment().format('YYYY-MM-D');
                var data = {
                    StartTime:AnalysisCommodity.start,
                    EndTime:AnalysisCommodity.end,
                    DisplayOrderOptions:AnalysisCommodity.DisplayOrderOptions,
                    DisplayOrder:AnalysisCommodity.DisplayOrder
                };
                AnalysisCommodity.projectQuery(data)
            }else if(type == "preThreeMonth"){
                AnalysisCommodity.start = moment().subtract('days', 90).format('YYYY-MM-D');
                AnalysisCommodity.end = moment().format('YYYY-MM-D');
                var data = {
                    StartTime:AnalysisCommodity.start,
                    EndTime:AnalysisCommodity.end,
                    DisplayOrderOptions:AnalysisCommodity.DisplayOrderOptions,
                    DisplayOrder:AnalysisCommodity.DisplayOrder
                };
                AnalysisCommodity.projectQuery(data)
            }
        });
        $('body').on('click','.export',function(){
            AnalysisCommodity.adminExportProductTheSalesList(AnalysisCommodity.start,AnalysisCommodity.end)

        });
        // 分页条数设置
        $("#pagesize_dropdown").on("change",function(){
            AnalysisCommodity.projectDestoryQuery();
        });



    },
    //获取一级分类商品类目销售分析
    adminGetFcProductList:function(start,end){
        //请求方法
        var methodName = "/statistics/AdminGetProductSalesAnalysisList";
        var data = {
            "StartTime": start,
            "EndTime": end
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                //名字
                var name = [];
                data.Data.ProductSalesAnalysisList.forEach(function(item,index){
                    name.push(item.Name)
                })
                //数据列表
                var list = [];
                data.Data.ProductSalesAnalysisList.forEach(function(item,index){
                    var Item = {};
                    Item.name = item.Name;
                    Item.value = item.CSaleCount;
                    list.push(Item)
                })
                var data = {
                    name:name,
                    list:list,
                }
                AnalysisCommodity.updateAnalysis_one(data)


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //更新一级分类商品类目销售分析
    updateAnalysis_one: function (Data) {
        var newChart_VermicelliMembership = echarts.init(myChart_VermicelliMembership);
        option = {
            title: {
                text: '一级分类商品',
                x: 'center',
                y: '4%'
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                x: 'center',
                y: '86%',
                icon: 'circle',
                data: Data.name
            },

            calculable: true,
            series: [
                {
                    name: '一级分类商品销售数量',
                    type: 'pie',
                    radius: [20, 110],
                    center: ['25%', '50%'],
                    roseType: 'area',
                    label: {
                        normal: {
                            show: true
                        },
                        emphasis: {
                            show: true
                        }
                    },
                    lableLine: {
                        normal: {
                            show: true
                        },
                        emphasis: {
                            show: true
                        }
                    },
                    data:Data.list,
                },
                {
                    name: '一级分类商品销售金额',
                    type: 'pie',
                    radius: [20, 110],
                    center: ['75%', '50%'],
                    roseType: 'area',
                    data: Data.list
                }
            ],
            color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
            backgroundColor: "#fafafa"
        };
        newChart_VermicelliMembership.setOption(option);
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#goodsStatistics').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/statistics/AdminGetProductTheSalesList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],
            idField: "Id", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            sortable:true,
            sortName:'VisitCount',
            sortOrder:'asc',
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: AnalysisCommodity.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: AnalysisCommodity.responseHandler,
            columns: [

                {
                    field: 'Name',
                    title: '商品名称',
                    align: 'center',
                    valign: 'middle',

                },
                {
                    field: 'VisitCount',
                    title: '浏览量',
                    align: 'center',
                    valign: 'middle',
                    sortable:true,

                },
                {
                    field: 'VisitNumber',
                    title: '浏览人数',
                    align: 'center',
                    valign: 'middle',
                    sortable:true,
                },
                {
                    field: 'PayNumber',
                    title: '付款人数',
                    align: 'center',
                    valign: 'middle',
                    sortable:true,
                },
                {
                    field: 'ConversionRate',
                    title: '单品转化率',
                    align: 'center',
                    valign: 'middle',
                    sortable:true,
                },
                {
                    field: 'SaleCount',
                    title: '销售数量',
                    align: 'center',
                    valign: 'middle',
                    sortable:true,
                },
                {
                    field: 'SalePrice',
                    title: '销售金额',
                    align: 'center',
                    valign: 'middle',
                    sortable:true,
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
            onSort:function(name,order){
                console.log(name)
                AnalysisCommodity.DisplayOrderOptions = name;
                AnalysisCommodity.DisplayOrder = order;

            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/statistics/AdminGetProductTheSalesList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            sortOrder: params.order,//排序
            sortName:params.sort,//排序字段
            StartTime:AnalysisCommodity.start,
            EndTime:AnalysisCommodity.end,




        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.ProductTheSalesList,
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
            obj = {



            };
        } else {
            obj = parame;
        }

        $('#goodsStatistics').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/statistics/AdminGetProductTheSalesList',
                query: obj
            }
        );

    },
    //表格先销毁刷新
    projectDestoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            obj = {
                page: {
                    PageSize: $("#pagesize_dropdown").val(),
                    PageIndex: 1
                },

                sortName:AnalysisCommodity.DisplayOrderOptions,
                sortOrder:AnalysisCommodity.DisplayOrder,
                StartTime:AnalysisCommodity.start,
                EndTime:AnalysisCommodity.end,
            };
        } else {
            obj = parame;
        }

        $('#goodsStatistics').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/statistics/AdminGetProductTheSalesList',
                query: obj
            }
        );
        AnalysisCommodity.initBootstrapTable()
    },
    //后台导出商品类目销售情况列表
    adminExportProductTheSalesList:function(start,end){
        //请求方法
        var methodName = "/statistics/AdminExportProductTheSalesList";
        var data = {
            "StartTime": start,
            "EndTime": end,
            "sortName": AnalysisCommodity.DisplayOrderOptions,
            "sortOrder": AnalysisCommodity.DisplayOrder
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                location.href = data.Data;

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }

};


$(function () {

    AnalysisCommodity.init()

})

var myChart_VermicelliMembership = document.getElementById('VermicelliMembership')
if (myChart_VermicelliMembership != null) {
    var newChart_VermicelliMembership = echarts.init(myChart_VermicelliMembership);
    option = {
        title: {
            text: '一级分类商品',
            x: 'center',
            y: '4%'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            x: 'center',
            y: '86%',
            icon: 'circle',
            data: ['婚礼视频', '沙画定制', '宝宝成长视频',]
        },

        calculable: true,
        series: [
            {
                name: '半径模式',
                type: 'pie',
                radius: [20, 110],
                center: ['25%', '50%'],
                roseType: 'area',
                label: {
                    normal: {
                        show: true
                    },
                    emphasis: {
                        show: true
                    }
                },
                lableLine: {
                    normal: {
                        show: true
                    },
                    emphasis: {
                        show: true
                    }
                },
                data: [
                    {value: 33, name: '婚礼视频'},
                    {value: 33, name: '沙画定制'},
                    {value: 33, name: '宝宝成长视频'},

                ]
            },
            {
                name: '面积模式',
                type: 'pie',
                radius: [20, 110],
                center: ['75%', '50%'],
                roseType: 'area',
                data: [
                    {value: 33, name: '婚礼视频'},
                    {value: 33, name: '沙画定制'},
                    {value: 33, name: '宝宝成长视频'}

                ]
            }
        ],
        color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
        backgroundColor: "#fafafa"
    };
    newChart_VermicelliMembership.setOption(option);
}




  


