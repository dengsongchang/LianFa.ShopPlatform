var AnalysisTransaction = {
    //开始时间
    start:"",
    //结束时间
    end:"",
    init: function () {
        var startTime = moment().subtract('days', 7).format('YYYY-MM-DD') + " 00:00:00";
        startTime =  moment(startTime).format('X');
        var endTime = moment().format('YYYY-MM-DD') + " 23:59:59";
        endTime =  moment(endTime).format('X');
        //获取订单交易分析
        AnalysisTransaction.adminGetOrderTransactionAnalysis(startTime,endTime);
        //获取订单金额分析
        AnalysisTransaction.adminGetOrderAmountAnalysis(startTime,endTime);

        //交易数据日历(获取订单交易分析)
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

        $('#AmountDate').daterangepicker(option_send_type, function (start, end, label) {
            $('#AmountDate').html(moment(start).format('MM-DD-YY') + ' - ' + moment(end).format('YYYY-MM-D'));
            var startTime = moment(start).format('YYYY-MM-DD') + " 00:00:00";
            startTime = moment(startTime).format('X');
            var endTime = moment(end).format('YYYY-MM-DD') + " 23:59:59";
            endTime = moment(endTime).format('X');
            AnalysisTransaction.adminGetOrderTransactionAnalysis(startTime,endTime)
        });

        //交易数据获取订单交易分析最近日期点击
        $('body').on('click','#dateUl1 li',function(){
            $(this).addClass('active').siblings('li').removeClass('active');
            var type = $(this).attr('svalue');
            if(type == "inOneWeek"){

                var start = moment().subtract('days', 7).format('YYYY-MM-DD') + " 00:00:00";
                start =  moment(start).format('X');
                var end = moment().format('YYYY-MM-DD') + " 23:59:59";
                end =  moment(end).format('X');
                AnalysisTransaction.adminGetOrderTransactionAnalysis(start,end)
            }else if(type == "inOneMonth"){
                var start = moment().subtract('days', 30).format('YYYY-MM-DD') + " 00:00:00";
                start =  moment(start).format('X');
                var end = moment().format('YYYY-MM-DD') + " 23:59:59";
                end =  moment(end).format('X');
                AnalysisTransaction.adminGetOrderTransactionAnalysis(start,end)
            }else if(type == "preThreeMonth"){
                var start = moment().subtract('days', 90).format('YYYY-MM-DD') + " 00:00:00";
                start =  moment(start).format('X');
                var end = moment().format('YYYY-MM-DD') + " 23:59:59";
                end =  moment(end).format('X');
                AnalysisTransaction.adminGetOrderTransactionAnalysis(start,end)
            }
        });

        //交易数据日历((获取订单金额分析))
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

        $('#AmountDate2 ').html(moment().subtract('days', 29).format('MM-DD-YY') + ' - ' + moment().format('YYYY-MM-D'));

        $('#AmountDate2').daterangepicker(option_send_type, function (start, end, label) {
            $('#AmountDate2').html(moment(start).format('MM-DD-YY') + ' - ' + moment(end).format('YYYY-MM-D'));
            var startTime = moment(start).format('YYYY-MM-DD') + " 00:00:00";
            startTime = moment(startTime).format('X');
            var endTime = moment(end).format('YYYY-MM-DD') + " 23:59:59";
            endTime = moment(endTime).format('X');
            AnalysisTransaction.adminGetOrderAmountAnalysis(startTime,endTime);
        });

        //交易数据获取订单金额分析最近日期点击
        $('body').on('click','#dateUl li',function(){
            $(this).addClass('active').siblings('li').removeClass('active');
            var type = $(this).attr('svalue');
            if(type == "inOneWeek"){
                var start = moment().subtract('days', 7).format('YYYY-MM-DD') + " 00:00:00";
                start =  moment(start).format('X');
                var end = moment().format('YYYY-MM-DD') + " 23:59:59";
                end =  moment(end).format('X');
                AnalysisTransaction.adminGetOrderAmountAnalysis(start,end)
            }else if(type == "inOneMonth"){
                var start = moment().subtract('days', 30).format('YYYY-MM-DD') + " 00:00:00";
                start =  moment(start).format('X');
                var end = moment().format('YYYY-MM-DD') + " 23:59:59";
                end =  moment(end).format('X');
                AnalysisTransaction.adminGetOrderAmountAnalysis(start,end)
            }
        });

        //切换效果
        $(".img_toggle").click(function () {
            $(this).parents().next(".row").find('.chart_1').slideToggle();
            $(this).children().toggle();
        })

        //tips
        $(".queryImg").on("mouseenter", function () {
            $(this).parents(".r_title").find(".tipBox").css("display", "block");
        });
        $(".queryImg").on("mouseleave", function () {
            $(this).parents(".r_title").find(".tipBox").css("display", "none");
        });

    },
    //获取获取订单交易分析
    adminGetOrderTransactionAnalysis:function(start,end){
        //请求方法
        var methodName = "/statistics/AdminGetOrderTransactionAnalysis";
        var data = {
            "StartTime": start,
            "EndTime": end
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var result = data.Data;
                //名字
                var name = [];
                result.OrderDataTable.List.forEach(function(item,index){
                    name.push(item.DataName)
                })
                //日期
                var date = [];
                result.OrderDataTable.List[0].DataList.forEach(function(item,index){
                    date.push(item.Date)
                })
                //数据
                var list = [];
                var a = {
                    name: '付款金额 ',
                    type: 'line',
                    data: [0,1]
                };
                result.OrderDataTable.List.forEach(function(item,index){
                    var obj = {};
                    obj.name = item.DataName;
                    obj.type = 'line';
                    var dataList = [];
                    item.DataList.forEach(function(item1,index1){
                        dataList.push(item1.Value)
                    })
                    obj.data = dataList;
                    list.push(obj);
                })


                var data = {
                    name:name,
                    list:list,
                    date:date,
                }
                AnalysisTransaction.updateAnalysis_one(data)
                $('#BroswerCount').text(result.BroswerCount);
                $('#OrderUserCount').text(result.OrderUserCount)
                $('#OrderCount').text(result.OrderCount)
                $('#ProductCount').text(result.ProductCount)
                $('#OrderAmount').text(result.OrderAmount)
                $('#RefundAmount').text(result.RefundAmount)
                $('#PayUserCount').text(result.PayUserCount)
                $('#PayOrderCount').text(result.PayOrderCount)
                $('#PayProductCount').text(result.PayProductCount)
                $('#PayAmount').text(result.PayAmount)
                $('#AvgAmount').text(result.AvgAmount)
                $('#OrderConversionRate').text(result.OrderConversionRate)
                $('#PayConversionRate').text(result.PayConversionRate)
                $('#SuccConversionRate').text(result.SuccConversionRate)





            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //更新订单交易分析
    updateAnalysis_one: function (Data) {
        var newChart_Membership = echarts.init(newChart_MembershipGrowth);
        option = {
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                bottom: 0,
                icon: 'circle',
                data: Data.name,
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '35',
                containLabel: true
            },
            xAxis: [
                {
                    type: 'category',
                    boundaryGap: false,
                    data: Data.date,
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    //splitNumber:3,
                },
                {
                    name: '转化率(%)',
                    //nameLocation: 'end',
                    //splitNumber: 4,
                    splitLine: false,
                    type: 'value',
                }
            ],
            series:Data.list,
            color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
            backgroundColor: "#fafafa"
        };
        newChart_Membership.setOption(option);
    },
    //获取订单金额分析
    adminGetOrderAmountAnalysis:function(start,end){
        //请求方法
        var methodName = "/statistics/AdminGetOrderAmountAnalysis";
        var data = {
            "StartTime": start,
            "EndTime": end
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var result = data.Data.List;
                //名字
                var name = [];
                data.Data.List.forEach(function(item,index){
                    name.push(item.DataName)
                })
                //数据列表
                var list = [];
                data.Data.List.forEach(function(item,index){
                    list.push(item.DataValue)
                })
                var data = {
                    name:name,
                    list:list,
                }
                AnalysisTransaction.updateAnalysis_two(data)
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //更新订单金额分析
    updateAnalysis_two: function (Data) {
        var newChart_Membership2 = echarts.init(newChart_MembershipGrowth2);
        option = {
            color: ['#3398DB'],
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'shadow'
                }
            },
            grid: {
                left: '3%',
                right: '4%',
                top: '6%',
                bottom: '3%',
                containLabel: true
            },
            xAxis: [
                {
                    type: 'category',
                    data: Data.name,
                    axisTick: {
                        alignWithLabel: true
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: [
                {
                    name: '订单金额',
                    type: 'bar',
                    barWidth: '40%',
                    data: Data.list,
                }
            ],
            color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
            backgroundColor: "#fafafa"
        };
        newChart_Membership2.setOption(option);
    },

};


$(function () {
    AnalysisTransaction.init()
})
//交易数据图表
var newChart_MembershipGrowth = document.getElementById('MembershipGrowth')
if (newChart_MembershipGrowth != null) {
    // var newChart_Membership = echarts.init(newChart_MembershipGrowth);
    // option = {
    //     tooltip: {
    //         trigger: 'axis'
    //     },
    //     legend: {
    //         bottom: 0,
    //         icon: 'circle',
    //         data: ['付款金额 ','退款金额 ', '付款人数', '付款件数', '下单转化率（%）', '付款转化率（%）', '成交转化率（%）']
    //     },
    //     grid: {
    //         left: '3%',
    //         right: '4%',
    //         bottom: '35',
    //         containLabel: true
    //     },
    //     xAxis: [
    //         {
    //             type: 'category',
    //             boundaryGap: false,
    //             data: ['2018-02-05','2018-02-06',]
    //         }
    //     ],
    //     yAxis: [
    //         {
    //             type: 'value',
    //             //splitNumber:3,
    //         },
    //         {
    //             name: '转化率(%)',
    //             //nameLocation: 'end',
    //             //splitNumber: 4,
    //             splitLine: false,
    //             type: 'value',
    //         }
    //     ],
    //     series: [
    //         {
    //             name: '付款金额 ',
    //             type: 'line',
    //             data: [0,1]
    //         },
    //         {
    //             name: '退款金额 ',
    //             type: 'line',
    //             data: [0,1]
    //         },
    //         {
    //             name: '付款人数',
    //             type: 'line',
    //             data: [0,1]
    //         },
    //         {
    //             name: '付款件数',
    //             type: 'line',
    //             data: [0,1]
    //         },
    //         {
    //             name: '转化率(%)',
    //             yAxisIndex: 1,
    //             name: '下单转化率（%）',
    //             type: 'line',
    //             data: [0,1]
    //         },
    //         {
    //             name: '转化率(%)',
    //             yAxisIndex: 1,
    //             name: '付款转化率（%）',
    //             type: 'line',
    //             data: [0,1]
    //         },
    //         {
    //             name: '转化率(%)',
    //             yAxisIndex: 1,
    //             name: '成交转化率（%）',
    //             type: 'line',
    //             data: [0,1]
    //         }
    //     ],
    //     color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
    //     backgroundColor: "#fafafa"
    // };
    // newChart_Membership.setOption(option);
}
//交易数据图表(第二个)
var newChart_MembershipGrowth2 = document.getElementById('MembershipGrowth2')

