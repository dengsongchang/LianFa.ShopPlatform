<template>
    <div class="allBox">
        <div class="tabBox">
            <div class="tabItem" v-for="item,index in tabList" :key="index" ref="tabItem"
                :class="active == index ? 'tabActive' : ''" @click="tabHandle(index)">
                <div style="text-align: center">{{item.title}}</div>
            </div>
            <div class="tabLine" ref="tabLine" :style="{transform: 'translateX(' + left + ')',width:''+lineWidth+''}">
            </div>
        </div>
        <van-list v-model="loading" :finished="finished" finished-text="没有更多了" @load="onLoad" :error.sync="error"
            error-text="请求失败，点击重新加载">
            <!--交易成功-->
            <div class="orderBox" v-for="item,index in OrderList" :key="index">
                <div class="osnBox">
                    <div class="osn">订单编号:
                        <span class="osnNumber">
                            {{item.OSn}}
                        </span>
                    </div>
                    <div class="stateBox">{{item.OrderStateDec}}</div>
                </div>
                <div class="contentBox">
                    <img :src="item.ShowImg" class="productImg">
                    <div class="productRight">
                        <div class="productName">
                            {{item.ProductName}}
                        </div>
                        <div class="priceBox">
                            <div class="price"><span class="icon">¥</span>{{item.ShopPrice}}</div>
                            <div class="count">x{{item.RealCount}}</div>
                        </div>
                    </div>
                </div>
                <div class="bottomBox">
                    <div class="right">
                        <span class="allIcon">总价：¥</span><span class="blod">{{item.OrderAmount}}</span>
                    </div>
                    <div class="left">
                        共{{item.RealCount}}件商品
                    </div>
                </div>
                <div class="actionBox" v-if="item.OrderState == 20">
                    <div class="colorBtn" @click="actionPay(item.OId)">立即付款</div>
                    <div class="grayBtn otherBtn" @click="getOrderInfo(item.OId)">查看订单</div>
                    <div class="grayBtn" @click="getCancelOrder(item.OId)">取消订单</div>
                </div>
                <div class="actionBox" v-if="item.OrderState == 120">
                    <div class="colorBtn" @click="getbuyagain(item.OId,item.Type,item.PId)">再次购买</div>
                    <div class="grayBtn" @click="getOrderInfo(item.OId)">查看订单</div>
                </div>
                <div class="actionBox" v-if="item.OrderState == 75">
                    <div class="colorBtn" @click="getReceiveOrder(item.OId)">确认收货</div>
                    <div class="grayBtn otherBtn" @click="getOrderInfo(item.OId)">查看订单</div>
                    <div class="grayBtn" @click="getLogistics(item.OId)">查看物流</div>
                </div>
                <div class="actionBox" v-if="item.OrderState == 65">
                    <div class="grayBtn" @click="getOrderInfo(item.OId)">查看订单</div>
                </div>
            </div>
            <div class="clearfix"></div>
        </van-list>
    </div>


</template>

<script>
    import {
        mapState
    } from 'vuex'
    import {
        Tab,
        Tabs,
        List
    } from 'vant'

    export default {
        name: 'myOrder',
        data() {
            return {
                tabList: [{
                    title: '全部订单',
                    active: '0',
                }, {
                    title: '待支付',
                    active: '20',
                }, {
                    title: '待发货',
                    active: '65',
                }, {
                    title: '待收货',
                    active: '75',
                }, {
                    title: '已完成',
                    active: '120',
                }, ],
                active: 0,
                OrderState: 0,
                OrderList: [],
                error: false,
                loading: false,
                finished: false,
                total: 0,
                pageIndex: 1,
                left: 0,
                lineWidth: 0,
                isWeChat: "",
            };
        },
        computed: {},
        components: {
            "van-tabs": Tabs,
            "van-tab": Tab,
            "van-list": List
        },
        methods: {
            //点击切换
            tabHandle: function(index) {
                var that = this;
                that.active = index;
                that.init();
                if (that.active == 0) {
                    that.OrderState = 0;
                } else if (that.active == 1) {
                    that.OrderState = 20;
                } else if (that.active == 2) {
                    that.OrderState = 65;
                } else if (that.active == 3) {
                    that.OrderState = 75;
                } else if (that.active == 4) {
                    that.OrderState = 120;
                }
                that.pageIndex = 1;
                that.OrderList = [];
                that.finished = false;
                that.loading = true;
                that.$nextTick(function() {
                    that.onLoad()
                })
            },
            //初始化
            init: function() {
                var that = this;
                that.$nextTick(function() {
                    var tabs = that.$refs.tabItem;
                    if (!tabs || !tabs[that.active]) {
                        return
                    }
                    var currentTab = tabs[that.active];
                    var docEl = document.documentElement;
                    var rem = docEl.clientWidth / 10;
                    var lineWidth = Math.ceil(20 / 37.5 * rem);
                    //获取距离最左边的距离
                    var currentTabLeft = Math.ceil(currentTab.offsetLeft);
                    //获取tab的宽度
                    var tabWidth = Math.ceil(currentTab.offsetWidth);
                    var left = Math.ceil(currentTabLeft + (currentTab.offsetWidth - lineWidth) / 2);
                    that.left = left + 'px';
                    that.lineWidth = lineWidth + 'px';
                    console.log("左边距", that.left)
                    console.log('线宽', that.lineWidth)
                })
            },
            //立即付款
            actionPay: function(id, type) {
                var that = this;
                var oid = id;
                var AppId = that.GLOBAL.appId;
                var local = encodeURIComponent('' + that.GLOBAL.urlPrefixNoAdmin + '/middlePage?type=againPay');

                //H5支付
                if (!that.isWeChat) {
                    that.submitOrder(oid);
                } else {
                    //   微信授权支付
                    var requestData = {
                        "OId": oid
                    };
                    var obj = JSON.stringify(requestData)
                    localStorage.setItem('payObj', obj)
                    var url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppId +
                        "&redirect_uri=" + local +
                        "&response_type=code&scope=snsapi_base#wechat_redirect";
                    window.location.href = url;
                }
            },
            //重新提交订单接口(h5支付)
            submitOrder: function(id) {
                var that = this;
                var methodName = '/OrderHandel/ReSubmitOrder'
                var requestData = {
                    "Code": "",
                    "OId": id
                };
                that.postData(methodName, requestData).then(function(data) {
                    if (data.data.Code == "100") {
                        if (!that.isWeChat) {
                            console.log(data)
                                //h5端发起微信支付
                            var referLink = document.createElement('a');
                            referLink.href = data.data.Data.WechatParameter.MWebUrl;
                            document.body.appendChild(referLink);
                            referLink.click();
                            return false
                        }
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }
                });
            },
            // 再次购买
            getbuyagain: function(OId, type, gid) {
                var that = this;
                if (type == 2) {
                    //礼包重新购买
                    that.$router.push({
                        path: "/giftPayOrder",
                        query: {
                            count: 1,
                            gId: gid,
                            isCartBuy: false
                        }
                    });
                } else {
                    //普通商品重新购买
                    var methodName = '/OrderHandel/ReComposeOrder'
                    var requestData = {
                        "OId": OId
                    };
                    that.postData(methodName, requestData).then(function(data) {
                        if (data.data.Code == "100") {
                            var list = data.data.Data.PIds;
                            if (list.length > 0) {
                                var cartStr = list.join(',')
                                that.$router.push({
                                    path: "/payOrder",
                                    query: {
                                        count: 1,
                                        isCartBuy: true,
                                        cartStr: cartStr,
                                    }
                                });
                            } else {
                                that.Common.showMsg("该商品已下架，不能重新购买")
                                return false
                            }

                        } else {
                            that.Common.showMsg(data.data.Message)
                        }
                    });
                }

            },
            // 订单详情
            getOrderInfo: function(OId) {
                var that = this;
                that.$router.push({
                    path: "/orderDetail",
                    query: {
                        OId: OId,
                    }
                });
            },
            // 查看物流
            getLogistics: function(OId) {
                var that = this;
                that.$router.push({
                    path: "/logistics",
                    query: {
                        OId: OId,
                    }
                });
            },
            // 确认收货
            getReceiveOrder: function(OId) {
                var that = this;
                var data = {
                    oid: that.OId
                }
                that.postData("/UCenter/CouponsOrderInfo", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.Common.showMsg('收货成功', function() {
                            that.pageIndex = 1;
                            that.OrderList = [];
                            that.finished = false;
                            that.loading = true;
                            that.$nextTick(function() {
                                that.onLoad()
                            })

                        })
                    } else {
                        that.Common.showMsg(data.data.Message);
                    }
                });
            },
            getCancelOrder: function(OId) {
                var that = this;
                //请求方法
                var methodName = "/UCenter/CancelOrder";
                //请求数据
                var reqData = {
                    "oid": OId
                };
                that.postData(methodName, reqData).then(function(data) {
                    console.log(data)
                    if (data.data.Code == 100) {
                        that.Common.showMsg('取消成功', function() {
                            that.pageIndex = 1;
                            that.OrderList = [];
                            that.finished = false;
                            that.loading = true;
                            that.$nextTick(function() {
                                that.onLoad()
                            })

                        })
                    } else {
                        that.Common.showMsg(data.data.Message);
                    }
                });
            },


            //前往详情页
            toDetail: function() {
                var that = this;
                that.$router.push('/orderDetail')
            },
            onLoad: function() {
                var that = this;
                //请求方法
                var methodName = "/UCenter/OrderList";

                //请求数据
                var reqData = {
                    "orderState": that.OrderState,
                    "Page": {
                        "PageSize": 4,
                        "PageIndex": that.pageIndex
                    },
                    Type: 0
                };
                that.postData(methodName, reqData).then(function(data) {
                    console.log(data)
                    if (data.data.Code == 100) {
                        that.OrderList = that.OrderList.concat(data.data.Data
                            .OrderList);
                        that.total = data.data.Data.Total;
                        that.pageIndex += 1;
                        // 加载状态结束
                        that.loading = false;
                        // 数据全部加载完成
                        if (that.OrderList.length >= that.total) {
                            that.finished = true;
                        }

                    } else {
                        that.error = true;
                        that.Common.showMsg(data.data.Message);
                    }
                });
            },
            //判断是否是微信端
            isWeChatMethod: function() {
                var that = this;
                var ua = navigator.userAgent.toLowerCase();
                var isWeixin = ua.indexOf('micromessenger') != -1;
                isWeixin ? that.isWeChat = true : that.isWeChat = false;
                return isWeixin
            },

        },
        mounted() {
            var that = this;
            that.init();
            that.isWeChatMethod();
        },
        created() {
            var that = this;

        },
        beforeRouteLeave(to, from, next) {
            if (to.name === 'orderDetail') {
                this.changeKeepAlive('myOrder', true)
            } else {
                this.changeKeepAlive('myOrder', false)
            }
            next()
        }
    }
</script>
<style scoped src="../../static/css/myOrder.css">

</style>