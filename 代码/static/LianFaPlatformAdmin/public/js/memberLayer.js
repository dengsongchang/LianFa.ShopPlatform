$(function () {
    MemberLayer.init();
})

var MemberLayer = {
    newRegisterUserInfoTpl: `
        <li>
            <p class="data-num">{{DayUserNum}}</p>
            <p class="data-text">今日新注册会员</p>
        </li>
        <li>
            <p class="data-num">{{WeekUserNum}}</p>
            <p class="data-text">本周新注册会员</p>
        </li>
        <li>
            <p class="data-num">{{MonthUserNum}}</p>
            <p class="data-text">本月新注册会员</p>
        </li>
    `,
    userTpl: `
        <div class="user-data">
            <div>
                <p>{{NoConsumeUserNum}}</p>
                <p>{{ConsumeUserNum}}</p>
            </div>
        </div>
    `,
    activeTpl: `
        <div class="active-data">
            <div>
                <p>{{OneMonthActiveUserNum}}</p>
                <p>{{ThreeMonthActiveUserNum}}</p>
                <p>{{SixMonthActiveUserNum}}</p>
            </div>
            <p class="total-active">共<span>{{activeTotal}}</span>人，占总会员<span>{{prop}}</span>%</p>
        </div>
    `,
    sleepTpl: `
        <div class="sleep-data">
            <div>
                <p>{{OneMonthDormantUserNum}}</p>
                <p>{{ThreeMonthDormantUserNum}}</p>
                <p>{{SixMonthDormantUserNum}}</p>
                <p>{{NineMonthDormantUserNum}}</p>
                <p>{{TwelveMonthDormantUserNum}}</p>
            </div>
            <p class="total-sleep">共<span>{{sleepTotal}}</span>人，占总会员<span>{{prop}}</span>%</p>
        </div>
    `,
    init:function () {

        MemberLayer.getUserLayer();

    },
    // 获取会员分层数据
    getUserLayer:function(){
        var methodName = "/user/AdminGetUserLayer";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(MemberLayer.newRegisterUserInfoTpl);
                var html = render(data.Data.NewRegisterUserInfo);
                $("#newUser").html(html);

                // 会员总数
                var total = data.Data.ConsumeUserInfo.ConsumeUserNum + data.Data.ConsumeUserInfo.NoConsumeUserNum;

                MemberLayer.initMemberSpendingChart(data.Data.ConsumeUserInfo);
                MemberLayer.initActiveMemberChart(data.Data.ActiveUserInfo,total);
                MemberLayer.initSleepMemberChart(data.Data.DormantUserInfo,total);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 会员消费图
    initMemberSpendingChart:function (data) {
        var option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                left: '100px',
                top: '100px',
                data: [{
                    name: '未消费会员',
                    icon: 'circle'
                }, {
                    name: '有消费会员',
                    icon: 'circle'
                }]
            },
            series: [{
                name: '访问来源',
                type: 'pie',
                radius: ["0%", "120px"],
                center: ['50%', '50%'],
                data: [{
                    value: data.NoConsumeUserNum,
                    name: '未消费会员'
                }, {
                    value: data.ConsumeUserNum,
                    name: '有消费会员'
                }],
                itemStyle: {
                    normal: {
                        borderWidth: 2,
                        borderColor: "#ffffff",
                        borderType: "solid",
                        label: {
                            show: true,
                            formatter: '{d}%'
                        }
                    }
                }
            }],
            color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
            backgroundColor: "#fafafa"
        };

        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById('memberSpend'));
        // 使用刚指定的配置项和数据显示图表
        myChart.setOption(option);

        var render = template.compile(MemberLayer.userTpl);
        var html = render(data);
        $("#userBox").append(html);
    },
    // 活跃会员图
    initActiveMemberChart:function (data,total) {
        var option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                left: '100px',
                top: '100px',
                data: [{
                    name: '1个月活跃会员',
                    icon: 'circle'
                }, {
                    name: '3个月活跃会员',
                    icon: 'circle'
                }, {
                    name: '6个月活跃会员',
                    icon: 'circle'
                }]
            },
            series: [{
                name: '访问来源',
                type: 'pie',
                radius: ["0%", "120px"],
                center: ['50%', '50%'],
                data: [{
                    value: data.OneMonthActiveUserNum,
                    name: '1个月活跃会员'
                }, {
                    value: data.ThreeMonthActiveUserNum,
                    name: '3个月活跃会员'
                }, {
                    value: data.SixMonthActiveUserNum,
                    name: '6个月活跃会员'
                }],
                itemStyle: {
                    normal: {
                        borderWidth: 2,
                        borderColor: "#ffffff",
                        borderType: "solid",
                        label: {
                            show: true,
                            formatter: '{d}%'
                        }
                    }
                }
            }],
            color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
            backgroundColor: "#fafafa"
        };

        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById('activeMember'));
        // 使用刚指定的配置项和数据显示图表
        myChart.setOption(option);

        var activeTotal = data.OneMonthActiveUserNum + data.ThreeMonthActiveUserNum + data.SixMonthActiveUserNum;
        var prop = ((activeTotal / total) * 100).toFixed(2);
        data.activeTotal = activeTotal;
        data.prop = prop;
        var render = template.compile(MemberLayer.activeTpl);
        var html = render(data);
        $("#activeBox").append(html);
    },
    // 休眠会员图
    initSleepMemberChart:function (data,total) {
        var option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                left: '100px',
                top: '100px',
                data: [{
                    name: '1个月休眠会员',
                    icon: 'circle'
                }, {
                    name: '3个月休眠会员',
                    icon: 'circle'
                }, {
                    name: '6个月休眠会员',
                    icon: 'circle'
                }, {
                    name: '9个月休眠会员',
                    icon: 'circle'
                }, {
                    name: '12个月休眠会员',
                    icon: 'circle'
                }]
            },
            grid: {
                top:'5%',
            },
            series: [{
                name: '访问来源',
                type: 'pie',
                radius: ["0%", "120px"],
                center: ['50%', '50%'],
                data: [{
                    value: data.OneMonthDormantUserNum,
                    name: '1个月休眠会员'
                }, {
                    value: data.ThreeMonthDormantUserNum,
                    name: '3个月休眠会员'
                }, {
                    value: data.SixMonthDormantUserNum,
                    name: '6个月休眠会员'
                }, {
                    value: data.NineMonthDormantUserNum,
                    name: '9个月休眠会员'
                }, {
                    value: data.TwelveMonthDormantUserNum,
                    name: '12个月休眠会员'
                }],
                itemStyle: {
                    normal: {
                        borderWidth: 2,
                        borderColor: "#ffffff",
                        borderType: "solid",
                        label: {
                            show: true,
                            formatter: '{d}%'
                        }
                    }
                }
            }],
            color: ["#FF7878", "#68c1b8", "#fdbf74", "#a4adbd", "#686d78", "#bfe573", "#77d97c", "#50b4e5", "#7a95e5", "#fa96cf", "#a4adbd", "#686d78"],
            backgroundColor: "#fafafa"

        };

        // 基于准备好的dom，初始化echarts实例
        var myChart = echarts.init(document.getElementById('sleepMember'));
        // 使用刚指定的配置项和数据显示图表
        myChart.setOption(option);

        var sleepTotal = data.OneMonthDormantUserNum + data.ThreeMonthDormantUserNum + data.SixMonthDormantUserNum + data.NineMonthDormantUserNum + data.TwelveMonthDormantUserNum;
        var prop = ((sleepTotal / total) * 100).toFixed(2);
        data.sleepTotal = sleepTotal;
        data.prop = prop;
        var render = template.compile(MemberLayer.sleepTpl);
        var html = render(data);
        $("#sleepMember").append(html);
    }
}