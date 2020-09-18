$(function () {
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

    $('#CalendarWatch ').html(moment().subtract('days', 29).format('MM-DD-YY') + ' - ' + moment().format('YYYY-MM-D'));

    $('#CalendarWatch').daterangepicker(option_send_type, function (start, end, label) {
        $('#CalendarWatch').html(moment(start).format('MM-DD-YY') + ' - ' + moment(end).format('YYYY-MM-D'));
        var startTime = moment(start).format('X');
        var endTime = moment(end).format('X');


    });
})
$(".img_toggle").click(function () {
    $(this).parents().next(".row").find('.chart_1').slideToggle();
    $(this).children().toggle();
})

$(function () {
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
        var startTime = moment(start).format('X');
        var endTime = moment(end).format('X');


    });
})

var newChart_MembershipGrowth = document.getElementById('getPageview')
if (newChart_MembershipGrowth != null) {
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
            data: ['浏览次数（pv）', '独立访客（UV）'],
            bottom: 0,
            icon: 'circle',
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
            data: ['2018-01-29', '2018-01-30', '2018-01-31', '2018-02-01', '2018-02-02', '2018-02-03', '2018-02-04']
        }],
        yAxis: [{
            type: 'value'
        }],
        series: [{
                name: '浏览次数（pv）',
                type: 'line',
                stack: '总量',
                // areaStyle: {
                // 	normal: {}
                // },
                data: [120, 132, 101, 134, 90, 230, 210]
            },
            {
                name: '独立访客（UV）',
                type: 'line',
                stack: '总量',
                // areaStyle: {
                // 	normal: {}
                // },
                data: [220, 182, 191, 234, 290, 330, 310]
            }

        ]
    };
    newChart_Membership.setOption(option_curve);
}

var myChart_VermicelliMembership = document.getElementById('VermicelliMembership')
if (myChart_VermicelliMembership != null) {
    var newChart_VermicelliMembership = echarts.init(myChart_VermicelliMembership);
    option = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            left: '60%',
            top: '30%',
            icon: 'circle',
            orient: 'vertical',
            data: ['PC端', '微信端', 'app', '其他'],
        },
        toolbox: {
            show: true,
            feature: {
                mark: {
                    show: true
                },
                magicType: {
                    show: true,
                    type: ['pie', 'funnel']
                },
            }
        },
        calculable: true,
        series: [{
            name: '访问来源',
            type: 'pie',
            radius: ["0%", "120px"],
            center: ['30%', '50%'],
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
            data: [{
                    value: 25,
                    name: 'PC端'
                   
                },
                {
                    value: 25,
                    name: '微信端'
                    
                },
                {
                    value: 25,
                    name: 'app'
                },
                {
                    value: 25,
                    name: '其他'
                },
            ]

        }],
        color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
        backgroundColor: "#fafafa"
    };
    newChart_VermicelliMembership.setOption(option);
}
