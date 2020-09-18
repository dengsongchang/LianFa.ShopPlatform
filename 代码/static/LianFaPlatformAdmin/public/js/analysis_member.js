var AnalysisMember = {
    init:function(){
        //会员增长初始化
        var year = moment().format('YYYY');
        var month = moment().format('MM')
        AnalysisMember.adminGetAddUserAnalyzeList(year,month);
        // AnalysisMember.adminGetUserPortSourceList(year,month);
        // AnalysisMember.adminGetUserAndFansList();
        AnalysisMember.adminGetUserAmountList();
        //会员增长情况日历(月)
        $('#MemberYearMonth').datetimepicker({
            language: 'zh-CN',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 3,
            forceParse: 0,
            showMeridian: 1,
            minView: 3,
            todayHighlight: true,
            format: 'yyyy-mm',
            startDate:"2014-12",
            // dateFormat: 'yy-mm-dd',
            // maxDate: null,//最大日期
            // minDate: new Date(),
            // showMonthAfterYear: true,
            // monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
            // dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
            // dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
            // dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
            // onSelect: function (selectedDate) {//选择日期后执行的操作
            //     $('#ui-datepicker-div').hide();
            //     group_news_obj.date = moment(selectedDate).format('X');
            // }
        }).on('changeDate', function (ev) {
            var month = $('#MemberYearMonth').val().split('-')[1];
            var year = $('#MemberYearMonth').val().split('-')[0];
            AnalysisMember.adminGetAddUserAnalyzeList(year,month)
        });;
        $('#MemberYearMonth').val(moment().subtract('days', 1).format('YYYY-MM'));
        //会员增长情况日历(年)
        $('#MemberYearMonth2').datetimepicker({
            language: 'zh-CN',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 4,
            forceParse: 0,
            showMeridian: 1,
            minView: 4,
            todayHighlight: true,
            format: 'yyyy',
            startDate:"2014-12",
            // dateFormat: 'yy-mm-dd',
            // maxDate: null,//最大日期
            // minDate: new Date(),
            // showMonthAfterYear: true,
            // monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
            // dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
            // dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
            // dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
            // onSelect: function (selectedDate) {//选择日期后执行的操作
            //     $('#ui-datepicker-div').hide();
            //     group_news_obj.date = moment(selectedDate).format('X');
            // }
        }).on('changeDate', function (ev) {
            var year = $('#MemberYearMonth2').val();
            AnalysisMember.adminGetAddUserAnalyzeList(year,0)
        });;
        $('#MemberYearMonth2').val(moment().subtract('days', 1).format('YYYY'));
        //会员端口来源日历(月)
        $('#MemberYearMonth_second').datetimepicker({
            language: 'zh-CN',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 3,
            forceParse: 0,
            showMeridian: 1,
            minView: 3,
            todayHighlight: true,
            format: 'yyyy-mm',
            startDate:"2014-12",
            // dateFormat: 'yy-mm-dd',
            // maxDate: null,//最大日期
            // minDate: new Date(),
            // showMonthAfterYear: true,
            // monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
            // dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
            // dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
            // dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
            // onSelect: function (selectedDate) {//选择日期后执行的操作
            //     $('#ui-datepicker-div').hide();
            //     group_news_obj.date = moment(selectedDate).format('X');
            // }
        }).on('changeDate', function (ev) {
            var month = $('#MemberYearMonth_second').val().split('-')[1];
            var year = $('#MemberYearMonth_second').val().split('-')[0];
            AnalysisMember.adminGetUserPortSourceList(year,month)
        });;
        $('#MemberYearMonth_second').val(moment().subtract('days', 1).format('YYYY-MM'));
        //会员端口来源日历(年)
        $('#MemberYearMonth2_second').datetimepicker({
            language: 'zh-CN',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 4,
            forceParse: 0,
            showMeridian: 1,
            minView: 4,
            todayHighlight: true,
            format: 'yyyy',
            startDate:"2014-12",
            // dateFormat: 'yy-mm-dd',
            // maxDate: null,//最大日期
            // minDate: new Date(),
            // showMonthAfterYear: true,
            // monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
            // dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
            // dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
            // dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
            // onSelect: function (selectedDate) {//选择日期后执行的操作
            //     $('#ui-datepicker-div').hide();
            //     group_news_obj.date = moment(selectedDate).format('X');
            // }
        }).on('changeDate', function (ev) {
            var year = $('#MemberYearMonth2_second').val();
            AnalysisMember.adminGetUserPortSourceList(year,0)

        });;
        $('#MemberYearMonth2_second').val(moment().subtract('days', 1).format('YYYY'));


        //会员增长情况导出按钮点击
        $('body').on('click','.export',function(){
            //判断是年还是月
            if($('#myOne option:selected').val() == "1"){
                //月
                var month = $('#MemberYearMonth').val().split('-')[1];
                var year = $('#MemberYearMonth').val().split('-')[0];
                AnalysisMember.exportAdminUserAnalyzeList(year,month)
            }else if($('#myOne option:selected').val() == "2"){
                var year = $('#MemberYearMonth2').val();
                AnalysisMember.exportAdminUserAnalyzeList(year,0)
            }
        })


        $('body').on('change','#myOne',function(){
            if($('#myOne option:selected').val() == "1"){
                $('#MemberYearMonth').show();
                $('#MemberYearMonth2').hide();
                $('#MemberYearMonth').val(moment().subtract('days', 1).format('YYYY-MM'));
                var month = $('#MemberYearMonth').val().split('-')[1];
                var year = $('#MemberYearMonth').val().split('-')[0];
                AnalysisMember.adminGetAddUserAnalyzeList(year,month)

            }else if($('#myOne option:selected').val() == "2"){
                $('#MemberYearMonth').hide();
                $('#MemberYearMonth2').show();
                $('#MemberYearMonth2').val(moment().format('YYYY'));
                var year = $('#MemberYearMonth2').val();
                AnalysisMember.adminGetAddUserAnalyzeList(year,0)
            }
        })
        $('body').on('change','#myOne2',function(){
            if($('#myOne2 option:selected').val() == "1"){
                $('#MemberYearMonth_second').show();
                $('#MemberYearMonth2_second').hide();
                $('#MemberYearMonth_second').val(moment().subtract('days', 1).format('YYYY-MM'));
                var month = $('#MemberYearMonth_second').val().split('-')[1];
                var year = $('#MemberYearMonth_second').val().split('-')[0];
                AnalysisMember.adminGetUserPortSourceList(year,month)
            }else if($('#myOne2 option:selected').val() == "2"){
                $('#MemberYearMonth_second').hide();
                $('#MemberYearMonth2_second').show();
                $('#MemberYearMonth2_second').val(moment().format('YYYY'));
                var year = $('#MemberYearMonth2_second').val();
                AnalysisMember.adminGetUserPortSourceList(year,0)

            }
        })
        // $('body').on('change','#myOne3',function(){
        //     if($('#myOne3 option:selected').val() == "1"){
        //         $('#MemberYearMonth_third').show();
        //         $('#MemberYearMonth2_third').hide();
        //         $('#MemberYearMonth_third').val(moment().subtract('days', 1).format('YYYY-MM-D'));
        //     }else if($('#myOne3 option:selected').val() == "2"){
        //         $('#MemberYearMonth_third').hide();
        //         $('#MemberYearMonth2_third').show();
        //         $('#MemberYearMonth2_third').val(moment().format('YYYY'));
        //     }
        // })
        // $('body').on('change','#myOne4',function(){
        //     if($('#myOne4 option:selected').val() == "1"){
        //         $('#MemberYearMonth_four').show();
        //         $('#MemberYearMonth2_four').hide();
        //         $('#MemberYearMonth_four').val(moment().subtract('days', 1).format('YYYY-MM-D'));
        //     }else if($('#myOne4 option:selected').val() == "2"){
        //         $('#MemberYearMonth_four').hide();
        //         $('#MemberYearMonth2_four').show();
        //         $('#MemberYearMonth2_four').val(moment().format('YYYY'));
        //     }
        // })
        //收缩效果
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
    //获取会员增长情况列表
    adminGetAddUserAnalyzeList:function(year,month){
        //请求方法
        var methodName = "/statistics/AdminGetAddUserAnalyzeList";
        var data = {
            "Year": year,
            "Month": month
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                //日期
                var dateList = [];
                //会员数量
                var userCount = [];
                data.Data.List.forEach(function(item,index){
                    dateList.push(item.Time);
                    userCount.push(item.UserCount);
                })

                var data = {
                    dateList:dateList,
                    userCount:userCount,
                }
                AnalysisMember.updateAnalysis_one(data)




            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //更新会员增长情况列表
    updateAnalysis_one: function (Data) {
        var newChart_Membership = echarts.init(newChart_MembershipGrowth);
        option_curve = {
            title: {
                //text: '消息趋势分析'
            },
            color: ['#ff2c53', '#479ac8'],
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'cross',
                    label: {
                        backgroundColor: '#6a7985'
                    }
                }
            },
            legend: {
                data: ['新增会员数'],
                bottom: 0,

            },
            toolbox: {
                feature: {
                    // saveAsImage: {},

                    mark: {
                        show: true
                    },
                    dataView: {
                        show: true,
                        readOnly: false
                    },
                    magicType: {
                        show: true,
                        type: ['line', 'funnel', 'bar']
                    },
                    restore: {
                        show: true
                    },
                    saveAsImage: {
                        show: true
                    },
                    //brush:{}
                },
                width: 30,
                orient: 'vertical',
                right: 0,
            },
            grid: {
                left: '3%',
                right: '8%',
                bottom: '16%',
                containLabel: true
            },
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                data: Data.dateList,
            }],
            yAxis: [{
                type: 'value'
            }],
            series: [{
                name: '新增会员数',
                type: 'line',
                stack: '总量',

                data: Data.userCount
            },

            ]
        };
        newChart_Membership.setOption(option_curve);
    },
    //后台导出会员增长情况列表
    exportAdminUserAnalyzeList:function(year,month){
        //请求方法
        var methodName = "/statistics/ExportAdminUserAnalyzeList";
        var data = {
            "Year": year,
            "Month": month
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
    },
    //获取会员端口来源信息
    adminGetUserPortSourceList:function(year,month){
        //请求方法
        var methodName = "/statistics/AdminGetUserPortSourceList";
        var data = {
            "Year": year,
            "Month": month
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                //名字列表
                var nameList = [];
                data.Data.List.forEach(function(item,index){
                    nameList.push(item.Name);
                })
                var wayCount = [];
                data.Data.List.forEach(function(item,index){
                    var data_item = {};
                    data_item.name = item.Name;
                    data_item.value = item.Count;
                    wayCount.push(data_item)
                });
                var data = {
                    nameList:nameList,
                    wayCount:wayCount,
                }
                AnalysisMember.updateAnalysis_two(data)





            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //更新会员端口来源信息
    updateAnalysis_two: function (Data) {
        console.log(Data)
        var newChart_MemberPortSource = echarts.init(myChart_MemberPortSource);
        option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                icon: 'circle',
                right: 210,
                top: 100,
                itemGap: 20,
                data: Data.nameList
            },
            series: [
                {
                    name: '访问来源',
                    type: 'pie',
                    radius: '75%',
                    center: ['25%', '50%'],
                    data:Data.wayCount,
                    color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
                    backgroundColor: "#fafafa"
                }
            ]
        };
        newChart_MemberPortSource.setOption(option);
    },
    //获取粉丝与会员构成信息
    adminGetUserAndFansList:function(){
        //请求方法
        var methodName = "/statistics/AdminGetUserAndFansList";
        var data = {

        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var member = data.Data.UserCount;
                var fan = data.Data.FansCount;
                var fanCount = data.Data.UserFansCount;
                var data = {
                    member:member,
                    fan:fan,
                    fanCount:fanCount,
                }
                AnalysisMember.updateAnalysis_third(data)


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //更新粉丝与会员构成信息
    updateAnalysis_third: function (Data) {
        console.log(Data)
        var newChart_VermicelliMembership = echarts.init(myChart_VermicelliMembership);
        option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                icon: 'circle',
                right: 210,
                top: 100,
                itemGap: 20,
                data: ['会员数','粉丝数','粉丝会员数']
            },
            series: [
                {
                    name: '会员数量',
                    type: 'pie',
                    radius: '75%',
                    center: ['25%', '50%'],
                    data:[
                        {
                        name:'会员数',value:Data.member,
                        },
                        {
                            name:'粉丝数',value:Data.fan,
                        },
                        {
                            name:'粉丝会员数',value:Data.fanCount,
                        }
                    ],
                    color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
                    backgroundColor: "#fafafa"
                }
            ]
        };
        newChart_VermicelliMembership.setOption(option);
    },
    //获取会员累计消费金额分布信息列表
    adminGetUserAmountList:function(){
        //请求方法
        var methodName = "/statistics/AdminGetUserAmountList";
        var data = {

        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var name = [];
                var value = [];
                data.Data.List.forEach(function(item,index){
                    name.push(item.Name);
                    value.push(item.UserCount)
                })

                var data = {
                    name:name,
                    value:value,
                }
                AnalysisMember.updateAnalysis_four(data)


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //更新粉丝与会员构成信息
    updateAnalysis_four: function (Data) {
        console.log(Data)
        var newChart_DistributionCumulative = echarts.init(myChart_DistributionCumulative);
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
                    name: '会员数',
                    type: 'bar',
                    barWidth: '40%',
                    data: Data.value,
                }
            ],
            color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
            backgroundColor: "#fafafa"
        };
        newChart_DistributionCumulative.setOption(option);
    },
};

$(function(){
    AnalysisMember.init()
})


var newChart_MembershipGrowth = document.getElementById('MembershipGrowth')
if (newChart_MembershipGrowth != null) {
    var newChart_Membership = echarts.init(newChart_MembershipGrowth);
}

// var myChart_MemberPortSource = document.getElementById('MemberPortSource')
// if (myChart_MemberPortSource != null) {
//     var newChart_MemberPortSource = echarts.init(myChart_MemberPortSource);
//
// }
//
// var myChart_VermicelliMembership = document.getElementById('VermicelliMembership')
// if (myChart_VermicelliMembership != null) {
//     var newChart_VermicelliMembership = echarts.init(myChart_VermicelliMembership);
//
// }

var myChart_DistributionCumulative = document.getElementById('DistributionCumulative')
if (myChart_DistributionCumulative != null) {
    var newChart_DistributionCumulative = echarts.init(myChart_DistributionCumulative);

}