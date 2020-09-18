<template>
    <div class="content">
        <div class="information">
            <router-link to="/personaldetails">
                <div class="information_img">

                    <img src="../../static/images/user/Center.png" alt="" v-if="AvatarStr =='' && AvatarStr == null ">
                    <img :src="AvatarStr" alt="" v-else>
                </div>
                <div class="information_name" v-if="NickName != '' && NickName != null">{{NickName}}</div>

            </router-link>
        </div>

        <div class="myorder">
            <div class="myorder_box">
                <div class="myorder_left">
                    <img src="../../static/images/user/decoration.png" alt="">
                    <span>我的订单</span>
                </div>
                <div class="myorder_right" @click="getmyOrder()">
                    <span>全部订单</span>
                    <img src="../../static/images/user/left.png" alt="">
                </div>
            </div>
            <van-swipe :loop="false" :show-indicators="false" indicator-color="white" class="zoneSwiper"
                v-if="orderlist.length > 0">
                <van-swipe-item v-for="(item, index) in orderlist" :key="index" class="zoneSwiper_item">
                    <div class="order_commodity">
                        <div class="commodity_img">
                            <img :src="item.ShowImg" alt="">
                        </div>
                        <div class="commodity_status">订单状态：{{item.OrderStateDec}}</div>
                        <div class="commodity_Amount">支付金额：¥{{item.SurplusMoney}}</div>
                        <div class="commodity_date">购买日期：{{item.AddTimeStr}}</div>
                        <div class="commodity_number">订单号：{{item.OSn}}</div>
                        <div class="see_details" @click="getOrderInfo(item.OId)">
                            <div>查看详情</div>

                        </div>
                    </div>
                </van-swipe-item>
            </van-swipe>
            <div class="noProduct" v-if="orderlist.length ==0" @click="getHome">
                <img src="../../static/images/cart/no.png" class="noProductImg" alt="">
                <div class="noText">暂无订单，去首页看看吧</div>
            </div>
        </div>

        <div class="exchange">
            <div class="exchange_box">
                <div class="exchange_left">
                    <img src="../../static/images/user/decoration.png" alt="">
                    <span>兑换记录</span>
                </div>
                <div class="exchange_right">
                    <span>全部记录</span>
                    <img src="../../static/images/user/left.png" alt="" @click="giftOrder()">
                </div>
            </div>
            <van-swipe :loop="false" :show-indicators="false" class="zoneSwiper" v-if="exchangelist.length > 0">
                <van-swipe-item v-for="(items, index) in exchangelist" :key="index" class="zoneSwiper_item">
                    <div class="order_commodity">
                        <div class="exchangelist_img">
                            <img :src="items.CouponImgFull" alt="">
                        </div>
                        <div class="exchange_status">{{items.Name}}</div>
                        <div class="exchange_Amount">序列号：{{items.OSn}}</div>
                        <div class="exchange_date">订单状态：{{items.OrderStateDec}}</div>
                        <div class="exchange_number">快递单号：{{items.OSn }}</div>
                        <div class="exchange_details" @click="getorderdetails(items.OId)">
                            <div>查看详情</div>

                        </div>
                    </div>
                </van-swipe-item>
            </van-swipe>
            <div class="noProduct" v-if="exchangelist.length ==0" @click="getHome">
                <img src="../../static/images/cart/no.png" class="noProductImg" alt="">
                <div class="noText">暂无订单，去首页看看吧</div>
            </div>
        </div>

        <!--底部-->
        <footerNav></footerNav>
    </div>

</template>
<script>
    import {
        Swipe,
        SwipeItem,
    } from 'vant';
    import footerNav from "../components/footer.vue";

    export default {
        name: 'user',
        data() {
            return {
                AvatarImg: require("../../static/images/user/Center.png"),
                NickName: "",
                Avatar: "",
                AvatarStr: "",
                orderlist: [],
                exchangelist: []
            };
        },
        computed: {},
        components: {
            "footerNav": footerNav,
            "van-swipe": Swipe,
            "van-swipe-item": SwipeItem,
        },
        methods: {
            getmyOrder: function() {
                var that = this;
                that.$router.push({
                    path: "/myOrder",

                });
            },
            giftOrder: function() {
                var that = this;
                that.$router.push({
                    path: "/giftOrder",

                });
            },
            getHome: function() {
                var that = this;
                that.$router.push({
                    path: "/",

                });
            },
            getUCenterData: function() {
                var that = this;
                var data = {};
                that.postData("/UCenter/UCenterData", data).then(function(data) {
                    console.log(data.data);
                    if (data.data.Code == "100") {
                        that.NickName = data.data.Data.Info.NickName;
                        that.AvatarStr = data.data.Data.Info.AvatarStr;
                        that.Avatar = data.data.Data.Info.Avatar;
                        that.UId = data.data.Data.Info.UId;
                        localStorage.setItem("UId", data.data.Data.UId);
                    }
                });
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
            //卡片详情页
            getorderdetails: function(OId) {
                var that = this;
                that.$router.push({
                    path: "/giftOrderDetail",
                    query: {
                        OId: OId,
                    }
                });
            },
            getOrdinaryorder: function() {
                var that = this;
                var data = {
                    orderState: 0,
                    Type: 0,

                };
                that.postData("/UCenter/OrderList", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.orderlist = data.data.Data.OrderList;
                    }
                });

            },
            getRedemptionorder: function() {
                var that = this;
                var data = {
                    OrderState: 0,
                };
                that.postData("/UCenter/CouponOrderList", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.exchangelist = data.data.Data.CouponOrderList;
                    }
                });
            }
        },
        mounted() {
            var that = this;
            that.getUCenterData();
            that.getOrdinaryorder();
            that.getRedemptionorder()
        },
        created() {
            var that = this;
            //底部导航栏
            that.$store.commit('setFooterNav', 'person')

        },
    }
</script>
<style scoped src="../../static/css/user.css">

</style>