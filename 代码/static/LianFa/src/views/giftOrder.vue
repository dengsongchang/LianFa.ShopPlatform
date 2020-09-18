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
        <van-list v-model="loading" :finished="finished" finished-text="没有更多了" @load="adminProductList"
            :error.sync="error" error-text="请求失败，点击重新加载">

            <div v-for="item,index in CouponOrderList" :key="index" class="orderBox">
                <!--待发货-->
                    <div class="osnBox">
                        <div class="osn">
                            {{item.Name}}
                        </div>
                        <div class="stateBox">{{item.OrderStateDec}}</div>
                    </div>
                    <div class="contentBox">
                        <img :src="item.CouponImgFull" class="giftImg" alt="">
                    </div>
                    <div class="bottomBox">
                        <div class="right">

                        </div>
                        <div class="left">
                            兑换时间：{{item.UseTimeStr}}
                        </div>
                    </div>
                    <!-- <div class="actionBox" v-if="item.OrderState == 75 ">
                        <div class="grayBtn" @click="getorderdetails(item.OId)">查看订单</div>
                    </div> -->
                    <div class="actionBox" v-if="item.OrderState == 120">
                        <div class="grayBtn" @click="getorderdetails(item.OId)">查看订单</div>
                    </div>
                    <div class="actionBox" v-if="item.OrderState == 65">
                        <div class="grayBtn" @click="getorderdetails(item.OId)">查看订单</div>
                    </div>
                    <div class="actionBox" v-if="item.OrderState == 75">
                        <div class="colorBtn" @click="getReceiveOrder(item.OId)">确认收货</div>
                        <div class="grayBtn otherBtn" @click="getorderdetails(item.OId)">查看订单</div>
                        <div class="grayBtn" @click="getLogistics(item.OId)">查看物流</div>
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
        name: 'giftOrder',
        data() {
            return {
                tabList: [{
                    title: '全部订单',
                    type: '0',
                }, {
                    title: '待发货',
                    type: '65',
                }, {
                    title: '待收货',
                    type: '75',
                }, {
                    title: '已完成',
                    type: '120',
                }, ],
                active: 0,
                state: 0,
                CouponOrderList: [],
                error: false,
                loading: false,
                finished: false,
                total: 0,
                pageIndex: 1,
                left: 0,
                lineWidth: 0,
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
                console.log(index)
                that.active = index;
                that.init();
                if (that.active == 0) {
                    that.state = 0;
                } else if (that.active == 1) {
                    that.state = 65;
                } else if (that.active == 2) {
                    that.state = 100;
                } else if (that.active == 3) {
                    that.state = 120;
                }
                that.pageIndex = 1;

                that.CouponOrderList = [];
                that.$nextTick(function() {
                    that.adminProductList()
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
            //前往详情页
            getorderdetails: function(OId) {
                var that = this;
                that.$router.push({
                    path: "/giftOrderDetail",
                    query: {
                        OId: OId,
                    }
                });
            },
            getLogistics: function(OId) {
                var that = this;
                that.$router.push({
                    path: "/logistics",
                    query: {
                        OId: OId,
                    }
                });
            },
            getReceiveOrder: function(OId) {
                var that = this;
                var data = {
                    oid: that.OId
                }
                that.postData("/UCenter/CouponsOrderInfo", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.Common.showMsg("收货成功")
                    }
                });
            },
            adminProductList: function() {
                var that = this;
                //请求方法
                var methodName = "/UCenter/CouponOrderList";
                that.loading = false;
                that.finished = true;
                setTimeout(function() {
                    //请求数据
                    var reqData = {
                        "Page": {
                            "PageSize": 4,
                            "PageIndex": that.pageIndex
                        },
                        OrderState: that.state
                    };
                    that.postData(methodName, reqData).then(function(res) {
                        console.log(res)
                        if (res.data.Code == 100) {
                            that.CouponOrderList = that.CouponOrderList.concat(res.data.Data
                                .CouponOrderList);
                            that.total = res.data.Data.Total;
                            that.pageIndex += 1;
                            // 加载状态结束
                            that.loading = false;
                            // 数据全部加载完成
                            if (that.CouponOrderList.length >= that.total) {
                                that.finished = true;
                            } else {
                                that.finished = false;
                            }
                        } else {
                            this.error = true;
                            that.Common.showMsg(res.data.Message);
                        }
                    });
                }, 500)

            },
        },
        mounted() {
            var that = this;
            that.init();
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
<style scoped src="../../static/css/giftOrder.css">

</style>