<template>

    <div class="allBox">
        <img :src="item.CouponImg" class="giftImg" alt="">
        <div class="giftTitle">
            {{item.Name}}
        </div>
        <div class="giftBox">
            <div class="title">
                礼品卡信息
            </div>
            <div class="messageItem">
                <div class="messageLeft">
                    序列号:
                </div>
                <div class="messageRight">
                    {{item.CouponSn}}
                </div>
            </div>
            <div class="messageItem">
                <div class="messageLeft">
                    密码/验证码:
                </div>
                <div class="messageRight">
                    {{item.PassWord}}
                </div>
            </div>
            <div class="messageItem">
                <div class="messageLeft">
                    快递单号:
                </div>
                <div class="messageRight">
                    {{OrderInfo.ShipSn}}
                </div>
            </div>
        </div>
        <div class="giftBox">
            <div class="title">
                兑换礼品
            </div>
            <div class="messageItem" v-for="item,index in item.ContentList" :key="index">
                <div class="messageRight">
                    {{item.CouponContent}}
                </div>
            </div>

        </div>
        <div class="giftBox">
            <div class="title">
                送货地址
            </div>
            <div class="messageItem">
                <div class="addressBox">
                    <div class="userName"> {{OrderAddressInfo.Consignee}}</div>
                    <div class="phone">
                        {{OrderAddressInfo.Mobile}}
                    </div>
                </div>
                <div class="address">
                    {{OrderAddressInfo.Address}}
                </div>
            </div>
        </div>
        <div class="giftBox">
            <div class="title" style="overflow: hidden">
                <div class="left">物流信息</div>
                <div class="right" @click="seeMoreHandle(item.OId,OrderAddressInfo.IsCoupons)">查看更多
                    <img src="../../static/images/gift/right.png" class="rightIcon" alt="">
                </div>
            </div>
            <div class="time"  v-if="OrderInfo.OrderState != 20">
                {{OrderInfo.AddTimes}}
            </div>
            <div class="item"  v-if="OrderInfo.OrderState != 20">
                {{State}}
            </div>
        </div>
        <div class="btn" @click="getReceiveOrder(item.OId)" v-if="OrderInfo.OrderState == 75">
            确认收货
        </div>
    </div>


</template>

<script>
    export default {
        name: 'giftOrderDetail',
        data() {
            return {
                item: {},
                OrderAddressInfo: {},
                OrderInfo: {},
                State: ""
            };
        },
        computed: {},
        components: {},
        methods: {
            seeMoreHandle: function(OId) {
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
                    oid: OId
                }
                that.postData("/UCenter/CouponsOrderInfo", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.Common.showMsg("收货成功", function() {
                            that.$router.push("/giftOrder");
                        });
                    }
                });
            },
            getCouponsOrderInfo: function() {
                var that = this;
                var data = {
                    oid: that.$route.query.OId
                };
                that.postData("/UCenter/CouponsOrderInfo", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.item = data.data.Data.CouponInfo;
                        that.OrderAddressInfo = data.data.Data.OrderAddressInfo;
                        that.OrderInfo = data.data.Data.OrderInfo;
                        that.State = data.data.Data.OrdersLogisticsInfo.State
                    }
                });
            }
        },
        mounted() {
            var that = this;
            that.getCouponsOrderInfo();
        },
        created() {
            var that = this;

        },
    }
</script>
<style scoped src="../../static/css/giftOrderDetail.css">

</style>