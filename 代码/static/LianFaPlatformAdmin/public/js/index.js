$(function() {
    Index.init();
});
var Index = {
    indexBoxTpl: `<div class="head-list">
        <ul class="row">
            <li class="col-md-4 col-sm-6 col-xs-12">
                <div class="info-box">
                    <img class="info-img" src="/public/images/order.png">
                    <div class="text-box">
                        <p class="amount-num">￥{{TodayOrderAmount}}</p>
                        <p class="amount-text">今日订单金额</p>
                    </div>
                </div>
            </li>
            <li class="col-md-4 col-sm-6 col-xs-12">
                <div class="info-box">
                    <img class="info-img" src="/public/images/orderPay.png">
                    <div class="text-box">
                        <p class="amount-num">{{TodayOrderCount}}条</p>
                        <p class="amount-text">今日成交订单数</p>
                    </div>
                </div>
            </li>
            <li class="col-md-4 col-sm-6 col-xs-12">
                <div class="info-box">
                    <img class="info-img" src="/public/images/club.png">
                    <div class="text-box">
                        <p class="amount-num">{{TodayUsers}}</p>
                                <p class="amount-text">今日新增会员</p>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="row" style="padding: 0 15px;">
                <div class="count-part col-md-8 col-sm-12 col-xs-12">
                    <ul class="row">
                        <li class="col-md-4 col-sm-6 col-xs-12">
                            <img class="count-img" src="/public/images/member.png">
                            <p class="count-text">会员总数</p>
                            <p class="count-num">{{TotalUsers}}</p>
                        </li>
                        <li class="col-md-4 col-sm-6 col-xs-12">
                            <img class="count-img" src="/public/images/member.png">
                            <p class="count-text">商品总数</p>
                            <p class="count-num">{{TotalProducts}}</p>
                        </li>
                        <li class="col-md-4 col-sm-6 col-xs-12">
                            <img class="count-img" src="/public/images/member.png">
                            <p class="count-text">交易总额</p>
                            <p class="count-num total-count">￥{{TotalOrderAmount}}</p>
                        </li>
                    </ul>
                </div>
                <div class="compare-part col-md-4 col-sm-6 col-xs-12">
                    <ul>
                        <li class="clearfix">
                            <p class="iconfont icon-dingdan compare-text">今日较昨天订单金额新增</p>
                            <p class="compare-num">￥{{AddOrderAmount}}</p>
                        </li>
                        <li class="clearfix">
                            <p class="iconfont icon-dingdan compare-text">今日较昨天订单成交数</p>
                            <p class="compare-num">{{AddOrderCount}}条</p>
                        </li>
                        <li class="clearfix">
                            <p class="iconfont icon-06_huiyuanguanli compare-text">今日较昨天新增会员数</p>
                            <p class="compare-num">{{AddUsers}}个</p>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="chart-part">
                <div>
                    <ul class="nav nav-tabs chart-type">
                        <li class="active orderReceived" data-typeTemp="1">
                            <a data-type="0" data-toggle="tab" href="#first" class="iconfont icon-tongji"> 订单统计</a>
                        </li>
                        <li class="commodity" data-typeTemp="3">
                            <a data-type="0" data-toggle="tab" href="#second" class="iconfont icon-shangpin"> 商品统计</a>
                        </li>
                        <li class="MemberStatistic" data-typeTemp="6">
                            <a data-type="0" data-toggle="tab" href="#third" class="iconfont icon-06_huiyuanguanli"> 会员统计</a>
                        </li>
                    </ul>
                </div>
                <div class="chart-box">
                    <div>
                        <div class="row">
                            <div class="" style="height:30px;overflow:hidden" >
                                <div class="tab-pane active col-md-7 col-sm-12 col-xs-12" id="first">
                                    <ul class="nav nav-tabs subChart-type">
                                        <li class="active">
                                            <a data-type="0" data-typeTemp="1" data-toggle="tab" href="" class="iconfont icon-tongji DepositTrend"> 订单金额趋势图(已付款)</a>
                                        </li>
                                        <li>
                                            <a data-type="0" data-typeTemp="2" data-toggle="tab" href="" class="iconfont icon-tongji OrderTrend"> 客户下单趋势图(已付款)</a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="tab-pane col-md-8 col-sm-12 col-xs-12" id="second">
                                    <ul class="nav nav-tabs subChart-type">
                                        <li class="active">
                                            <a data-type="0" data-typeTemp="3" data-toggle="tab" href="" class="iconfont icon-bingtu Categories"> 类别数量占比</a>
                                        </li>
                                        <li>
                                            <a data-type="0" data-typeTemp="4" data-toggle="tab" href="" class="iconfont icon-baobao ProductShelf"> 产品上架数量趋势图</a>
                                        </li>
                                        <li>
                                            <a data-type="0" data-typeTemp="5" data-toggle="tab" href="" class="iconfont icon-paimingkaoqian-01 ProductLabel"> 产品购买排名</a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="tab-pane col-md-7 col-sm-12 col-xs-12" id="third">
                                    <ul class="nav nav-tabs subChart-type">
                                        <li class="active">
                                            <a data-type="0" data-typeTemp="6" data-toggle="tab" href="" class="iconfont icon-bingtu MenberSex"> 会员性别占比</a>
                                        </li>
                                        <li>
                                            <a data-type="0" data-typeTemp="7" data-toggle="tab" href="" class="iconfont icon-paimingkaoqian-01 MenberRegistra"> 会员注册趋势</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="row col-md-4 col-sm-12 col-xs-12 timeTemp">
                                <label class="time-input checkbox-inline" data-tip="0">
                                    <input type="radio" checked="checked" name="dataSelect">
                                    最近3天
                                </label>
                                <label class="time-input checkbox-inline" data-tip="1">
                                    <input type="radio" name="dataSelect">
                                    最近7天
                                </label>
                                <label class="time-input checkbox-inline" data-tip="2">
                                    <input type="radio" name="dataSelect">
                                    最近30天
                                </label>
                            </div>
                        </div>
                    </div>
                    <div id="index_order_amount" style="width:100%;height:420px;"></div>
                    <div id="index_order_pic" style="width:100%;height:420px;display:none"></div>
                </div>
    </div>`,
    EndTime: Date.parse(new Date()) / 1000,
    StartTime: Date.parse(new Date()) / 1000 - 24 * 60 * 60 * 3,
    typeTemp: 1,
    notIndexTemplate: `
        <div class="list_title_header" style="float:left;width:100%">
            <!-- 总容器box -->
            <div Id="indexBox" style="margin: 0 auto;margin-top: 300px;text-align: center;font-size: 36px;">
                欢迎使用联发行管理后台
            </div>
            <!-- 总容器box end -->
        </div>
    
    `,
    init: function() {

        Index.AdminGetIndexDataInfo();


        //选择时间更换数据
        $("#indexBox").on("click", ".time-input", function() {
            console.log($(this).attr("data-tip"));
            //更新时间
            Index.get_time($(this).attr("data-tip"));
            //获取首页订单金额趋势数据
            Index.typeTempObj();
        });

        //订单统计
        $("#indexBox").on("click", ".orderReceived", function() {
            $("#index_order_amount").show();
            $("#index_order_pic").hide();
            Index.typeTemp = 1;
            var Str = `<li class="active">
                    <a data-type="0" data-toggle="tab" href="" class="iconfont icon-tongji DepositTrend"> 订单金额趋势图(已付款)</a>
                </li>
                <li>
                    <a data-type="0" data-toggle="tab" href="" class="iconfont icon-tongji OrderTrend"> 客户下单趋势图(已付款)</a>
                </li>`;
            $("#first>.subChart-type").html(Str);
            $('#first').show();
            $('#second').hide()
            $('#third').hide()
                //重置时间
            Index.setTime();
            //获取首页订单金额趋势数据
            Index.AdminGetIndexOrderAmountTrend();
        });
        // 订单金额趋势图(已付款)
        $("#indexBox").on("click", ".DepositTrend", function() {
            $("#index_order_amount").show();
            $("#index_order_pic").hide();
            Index.typeTemp = 1;
            //重置时间
            Index.setTime();
            //获取首页订单金额趋势数据
            Index.AdminGetIndexOrderAmountTrend();
        });

        //客户下单趋势图(已付款)
        $("#indexBox").on("click", ".OrderTrend", function() {
            $("#index_order_amount").show();
            $("#index_order_pic").hide();
            Index.typeTemp = 2;
            //重置时间
            Index.setTime();
            //获取首页订单数量趋势数据
            Index.AdminGetIndexOrderCountTrend();
        });

        //商品统计
        $("#indexBox").on("click", ".commodity", function() {
            $("#index_order_amount").hide();
            $("#index_order_pic").show();
            Index.typeTemp = 3;
            var Str = `<li class="active">
                    <a data-type="0" data-toggle="tab" href="" class="iconfont icon-bingtu Categories"> 类别数量占比</a>
                    </li>
                    <li>
                        <a data-type="0" data-toggle="tab" href="" class="iconfont icon-baobao ProductShelf"> 产品上架数量趋势图</a>
                    </li>
                    <li>
                        <a data-type="0" data-toggle="tab" href="" class="iconfont icon-paimingkaoqian-01 ProductLabel"> 产品购买排名</a>
                    </li>`;
            $("#second>.subChart-type").html(Str);
            $('#first').hide();
            $('#second').show()
            $('#third').hide()
                //重置时间
            Index.setTime();
            Index.AdminGetIndexProductCategoryCount();
        });

        //  类别数量占比
        $("#indexBox").on("click", ".Categories", function() {
            $("#index_order_amount").hide();
            $("#index_order_pic").show();
            Index.typeTemp = 3;
            //重置时间
            Index.setTime();
            Index.AdminGetIndexProductCategoryCount();
        });
        //  产品上架数量趋势图
        $("#indexBox").on("click", ".ProductShelf", function() {
            $("#index_order_amount").show();
            $("#index_order_pic").hide();
            Index.typeTemp = 4;
            //重置时间
            Index.setTime();
            //获取首页产品上架趋势数据
            Index.AdminGetIndexProductCountTrend();
        });
        //  产品购买排名
        $("#indexBox").on("click", ".ProductLabel", function() {
            $("#index_order_amount").show();
            $("#index_order_pic").hide();
            Index.typeTemp = 5;
            //重置时间
            Index.setTime();
            //获取首页产品购买排名数据
            Index.AdminGetIndexProductBuyCount();
        });

        //会员统计
        $("#indexBox").on("click", ".MemberStatistic", function() {
            $("#index_order_amount").hide();
            $("#index_order_pic").show();
            Index.typeTemp = 6;
            var Str = `<li class="active">
                <a data-type="0" data-toggle="tab" href="" class="iconfont icon-bingtu MenberSex"> 会员性别占比</a>
                </li>
                <li>
                    <a data-type="0" data-toggle="tab" href="" class="iconfont icon-paimingkaoqian-01 MenberRegistra"> 会员注册趋势</a>
                </li>`;
            $("#third>.subChart-type").html(Str);
            $('#first').hide();
            $('#second').hide()
            $('#third').show()
                //重置时间
            Index.setTime();
            //获取首页用户注册性别占比
            Index.AdminGetIndexUserRegisterGender();
        });

        // 会员性别占比
        $("#indexBox").on("click", ".MenberSex", function() {
            $("#index_order_amount").hide();
            $("#index_order_pic").show();
            Index.typeTemp = 6;
            //重置时间
            Index.setTime();
            Index.AdminGetIndexUserRegisterGender();
        });

        // 会员注册趋势
        $("#indexBox").on("click", ".MenberRegistra", function() {
            $("#index_order_amount").show();
            $("#index_order_pic").hide();
            Index.typeTemp = 7;
            //重置时间
            Index.setTime();
            // 获取首页用户注册趋势
            Index.AdminGetIndexUserCountTrend();
        });
    },
    //获取首页数据
    AdminGetIndexDataInfo: function() {
        //请求方法
        var methodName = "/statistics/AdminGetIndexDataInfo";
        var data = {};
        console.log(data)
            //请求接口
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                //判断是否拥有首页权限
                if (localStorage.getItem('hasIndex')) {
                    var render = template.compile(Index.indexBoxTpl);
                    var html = render(data.Data);
                    $('#indexBox').html(html);
                    //获取首页订单金额趋势数据
                    Index.AdminGetIndexOrderAmountTrend();
                } else {
                    var render = template.compile(Index.notIndexTemplate);
                    var html = render(data.Data);
                    $('#indexBox').html(html);
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取首页订单金额趋势数据
    AdminGetIndexOrderAmountTrend: function() {
        //请求方法
        var methodName = "/statistics/AdminGetIndexOrderAmountTrend";
        var data = {
            StartTime: Index.StartTime,
            EndTime: Index.EndTime
        };
        console.log(data)
            //请求接口
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                if (data.Data.List.length > 0) {
                    var Data_list = Index.HandleEcharts(data);
                    Index.UpdateIndexOrderAmount(Data_list, data.Data, "订单金额趋势(已付款)");
                } else {
                    var index_order_amount_obj = document.getElementById('index_order_amount');
                    if (index_order_amount_obj != null) {
                        var index_order_amount = echarts.init(index_order_amount_obj);
                        index_order_amount.clear();
                    }
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取首页订单数量趋势数据
    AdminGetIndexOrderCountTrend: function() {
        //请求方法
        var methodName = "/statistics/AdminGetIndexOrderCountTrend";
        var data = {
            StartTime: Index.StartTime,
            EndTime: Index.EndTime
        };
        //请求接口
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                if (data.Data.List.length > 0) {
                    var Data_list = Index.HandleEcharts(data);
                    Index.UpdateIndexOrderAmount(Data_list, data.Data, "客户下单趋势(已付款)");
                } else {
                    var index_order_amount_obj = document.getElementById('index_order_amount');
                    if (index_order_amount_obj != null) {
                        var index_order_amount = echarts.init(index_order_amount_obj);
                        index_order_amount.clear();
                    }
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取首页产品类别占比数据
    AdminGetIndexProductCategoryCount: function() {
        //请求方法
        var methodName = "/statistics/AdminGetIndexProductCategoryCount";
        var data = {
            StartTime: Index.StartTime,
            EndTime: Index.EndTime
        };
        console.log(data)
            //请求接口
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                if (data.Data.List.length > 0) {
                    var Data_list = Index.HandleEcharts2(data);
                    Index.UpdateIndexOrderPie(Data_list, data.Data, "类别数量占比");
                } else {
                    var index_order_amount_obj = document.getElementById('index_order_pic');
                    if (index_order_amount_obj != null) {
                        var index_order_amount = echarts.init(index_order_amount_obj);
                        index_order_amount.clear();
                    }
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取首页产品上架趋势数据
    AdminGetIndexProductCountTrend: function() {
        //请求方法
        var methodName = "/statistics/AdminGetIndexProductCountTrend";
        var data = {
            StartTime: Index.StartTime,
            EndTime: Index.EndTime
        };
        console.log(data)
            //请求接口
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {

                if (data.Data.List.length > 0) {
                    var Data_list = Index.HandleEcharts(data);
                    Index.UpdateIndexOrderAmount(Data_list, data.Data, "首页产品上架趋势");
                } else {
                    var index_order_amount_obj = document.getElementById('index_order_amount');
                    if (index_order_amount_obj != null) {
                        var index_order_amount = echarts.init(index_order_amount_obj);
                        index_order_amount.clear();
                    }
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取首页产品购买排名数据
    AdminGetIndexProductBuyCount: function() {
        //请求方法
        var methodName = "/statistics/AdminGetIndexProductBuyCount";
        var data = {
            StartTime: Index.StartTime,
            EndTime: Index.EndTime
        };
        console.log(data)
            //请求接口
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                if (data.Data.List.length > 0) {
                    var Data_list = Index.HandleEcharts(data);
                    Index.UpdateIndexOrderAmount(Data_list, data.Data, "产品购买排名");
                } else {
                    var index_order_amount_obj = document.getElementById('index_order_amount');
                    if (index_order_amount_obj != null) {
                        var index_order_amount = echarts.init(index_order_amount_obj);
                        index_order_amount.clear();
                    }
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取首页用户注册性别占比
    AdminGetIndexUserRegisterGender: function() {
        //请求方法
        var methodName = "/statistics/AdminGetIndexUserRegisterGender";
        var data = {
            StartTime: Index.StartTime,
            EndTime: Index.EndTime
        };
        console.log(data)
            //请求接口
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                console.log(data.Data)
                if (data.Data.List.length > 0) {
                    var Data_list = Index.HandleEcharts2(data);
                    Index.UpdateIndexOrderPie(Data_list, data.Data, "会员性别占比");
                } else {
                    var index_order_pic_obj = document.getElementById('index_order_pic');
                    if (index_order_pic_obj != null) {
                        var index_order_amount = echarts.init(index_order_pic_obj);
                        index_order_amount.clear();
                    }
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取首页用户注册趋势
    AdminGetIndexUserCountTrend: function() {
        //请求方法
        var methodName = "/statistics/AdminGetIndexUserCountTrend";
        var data = {
            StartTime: Index.StartTime,
            EndTime: Index.EndTime
        };
        console.log(data)
            //请求接口
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                if (data.Data.List.length > 0) {
                    var Data_list = Index.HandleEcharts(data);
                    Index.UpdateIndexOrderAmount(Data_list, data.Data, "会员注册趋势");
                } else {
                    var index_order_amount_obj = document.getElementById('index_order_amount');
                    if (index_order_amount_obj != null) {
                        var index_order_amount = echarts.init(index_order_amount_obj);
                        index_order_amount.clear();
                    }
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //更新数据图表视图
    UpdateIndexOrderAmount: function(Data, ExData, Type) {
        option_curve = {
            title: {
                text: Type,
                textStyle: { //标题内容的样式
                    color: '#216fac', //京东红
                    fontStyle: 'normal', //主标题文字字体风格，默认normal，有italic(斜体),oblique(斜体)
                    fontWeight: "lighter", //可选normal(正常)，bold(加粗)，bolder(加粗)，lighter(变细)，100|200|300|400|500...
                    fontSize: 16 //主题文字字体大小，默认为18px
                },
                left: "center"
            },
            color: ['#347fd5', '#facd89', '#ff2c53', '#479ac8', '#666666', '#dfebde', '#ffe8c4', '#ff728c', '#82d2ff', '#bebebe', '#82d2ff', '#ed99ff'],
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
                data: Data.dataList_Name,
                right: 0,
                top: "center"
            },
            grid: {
                left: '3%',
                right: '14%',
                bottom: '16%',
                top: "8%",
                containLabel: true
            },
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                data: Data.dateList,
            }],
            yAxis: [{
                axisLine: {
                    show: false,
                },
                type: 'value'
            }],
            series: Data.Series_list
        };
        var index_order_amount_obj = document.getElementById('index_order_amount');
        if (index_order_amount_obj != null) {
            var index_order_amount = echarts.init(index_order_amount_obj);
        } else {
            return;
        }
        index_order_amount.setOption(option_curve);
    },
    //更新饼图
    UpdateIndexOrderPie: function(Data, DataMsg, Type) {
        option_pie = {
            title: {
                text: Type,
                textStyle: { //标题内容的样式
                    color: '#216fac', //京东红
                    fontStyle: 'normal', //主标题文字字体风格，默认normal，有italic(斜体),oblique(斜体)
                    fontWeight: "lighter", //可选normal(正常)，bold(加粗)，bolder(加粗)，lighter(变细)，100|200|300|400|500...
                    fontSize: 16 //主题文字字体大小，默认为18px
                },
                x: 'center',
                top: 50
            },
            color: ['#c84946', '#3f5461', '#67a3ab', '#d78c71', '#92c7ae', '#83a990', '#ca8724', '#bfa59d'],
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                right: '0',
                data: Data.dataList_Name,
                show: false
            },
            xAxis: [{
                axisLine: {
                    show: false,
                },
            }],
            yAxis: [{
                axisLine: {
                    show: false,
                }
            }],
            series: [{
                name: '访问来源',
                type: 'pie',
                radius: '35%',
                center: ['50%', '60%'],
                data: Data.Series_list,
                itemStyle: {
                    emphasis: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }]
        };
        var index_order_amount_obj = document.getElementById('index_order_pic');
        if (index_order_amount_obj != null) {
            var index_order_amount = echarts.init(index_order_amount_obj);
        } else {
            return;
        }
        index_order_amount.setOption(option_pie);
    },
    //处理echarts的方法
    HandleEcharts: function(data) {
        console.log(data)
        var Data_List = {};
        //名字列表
        var dataList_Name = [];
        data.Data.List.forEach(function(item, index) {
                dataList_Name.push(item.DataName);
            })
            // //日期
        var dateList = [];
        data.Data.List[0].DataList.forEach(function(item, index) {
            //日期
            dateList.push(item.Date);
        })
        console.log(dateList)
            //名字列表
        Data_List.dataList_Name = dataList_Name;
        //日期
        Data_List.dateList = dateList;
        //series的数据
        var Series_list = []
        data.Data.List.forEach(function(item, index) {
                var Item = {};
                var arr = [];

                Item.name = $(item)[0].DataName;
                Item.type = 'line';
                Item.stack = '总量';
                $(item)[0].DataList.forEach(function(item2, index) {
                    arr.push($(item2)[0].Value)
                })
                Item.data = arr;
                Series_list.push(Item);
            })
            //把series的数据加进去
        Data_List.Series_list = Series_list;
        //把对象返回
        return Data_List;
    },
    //处理echarts的方法
    HandleEcharts2: function(data) {
        console.log(data);
        //series的数据
        var Series_list = []

        data.Data.List.forEach(function(item, index) {
            var data = {};
            data.name = item.DataName;
            data.value = item.DataValue;
            Series_list.push(data);

        })
        console.log(Series_list)
        var Data_List = {};
        //名字列表
        var dataList_Name = [];
        data.Data.List.forEach(function(item, index) {
                dataList_Name.push(item.DataName);
            })
            //名字列表
        Data_List.dataList_Name = dataList_Name;

        //把series的数据加进去
        Data_List.Series_list = Series_list;
        //把对象返回
        return Data_List;
    },
    //获取时间
    get_time: function(which_t) {
        // 获取当前时间戳(以s为单位)
        Index.EndTime = Date.parse(new Date()) / 1000;
        if (which_t == "0") {
            Index.StartTime = Index.EndTime - 24 * 60 * 60 * 3;
        } else if (which_t == "1") {
            Index.StartTime = Index.EndTime - 24 * 60 * 60 * 7;
        } else if (which_t == "2") {
            Index.StartTime = Index.EndTime - 24 * 60 * 60 * 30;
        }
    },
    setTime: function() {
        var Str = `<label class="time-input checkbox-inline" data-tip="0">
            <input type="radio" checked="checked" name="dataSelect">
            最近3天
            </label>
            <label class="time-input checkbox-inline" data-tip="1">
                <input type="radio" name="dataSelect">
                最近7天
            </label>
            <label class="time-input checkbox-inline" data-tip="2">
                <input type="radio" name="dataSelect">
                最近30天
            </label>`;
        $(".timeTemp").html(Str);
        Index.EndTime = Date.parse(new Date()) / 1000;
        Index.StartTime = Date.parse(new Date()) / 1000 - 24 * 60 * 60 * 3;
    },
    typeTempObj: function() {
        if (Index.typeTemp == 1) {
            Index.AdminGetIndexOrderAmountTrend();
        } else if (Index.typeTemp == 2) {
            Index.AdminGetIndexOrderCountTrend();
        } else if (Index.typeTemp == 3) {
            Index.AdminGetIndexProductCategoryCount();
        } else if (Index.typeTemp == 4) {
            Index.AdminGetIndexProductCountTrend();
        } else if (Index.typeTemp == 5) {
            Index.AdminGetIndexProductBuyCount();
        } else if (Index.typeTemp == 6) {
            Index.AdminGetIndexUserRegisterGender();
        } else if (Index.typeTemp == 7) {
            Index.AdminGetIndexUserCountTrend();
        }
    }
}