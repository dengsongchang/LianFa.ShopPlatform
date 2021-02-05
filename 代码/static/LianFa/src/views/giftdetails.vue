<!--礼品卡详情 -->
<template>
    <div class="content">

        <div class="tltle">
            <div class="tltle_top">
                联发行
            </div>
            <div class="tltle_buttom">
                <span>专注于高效的礼品卡服务</span>
                <div class="line"></div>
            </div>
        </div>
        <div class="giftcard_tltle">
            <div class="giftcard_neme">
                {{couponDetail.Name}}
            </div>
            <div class="giftcard_img">
                <img :src="couponDetail.CouponImg" alt="">
            </div>
        </div>

        <div class="giftdisplay">
            <div class="display_tltle">礼品展示</div>
            <div class="display_box">
                <div class="display_commodity">
                    <div class="commodity_img">
                        <img :src="couponDetail.ProductImg" alt="">
                    </div>
                    <div class="commodity_left">
                        <div class="commodity_tltle">{{couponDetail.Name}}</div>
                        <div class="commodtiy_details" v-for="item,index in couponDetail.Content">{{item}}</div>
                    </div>
                </div>
            </div>
        </div>

        <div class="price_box">
            <div class="price">
                单价
            </div>
            <div class="Amount_box">
                <span class="Amount" v-if="couponDetail.IsCostPrice == 0">
                    ¥<span class="blod">{{couponDetail.Money}}</span>
                </span>
                <span class="Amount" v-if="couponDetail.IsCostPrice == 1">
                    ¥<span class="blod">{{couponDetail.CostPrice}}</span>
                </span>
                <div class="countBox">
                    <div class="reduceBtn" @click.stop="reduceHandle()">
                        <img src="../../static/images/cart/reduce.png" class="reduceIcon" alt="">
                    </div>
                    <input type="number" readonly class="count" v-model="count">
                    <div class="addBtn" @click.stop="addHandle()">
                        <img src="../../static/images/cart/add.png" class="addIcon" alt="">
                    </div>
                </div>

            </div>
            <div class="Originalprice">
                <span class="Originalprice_span" v-if="couponDetail.IsCostPrice == 1">
                    ¥<span class="Originalprice_Amount">{{couponDetail.Money}}</span>
                </span>
            </div>
            <div class="total">
                <span class="total_span" v-if="couponDetail.IsCostPrice == 1">
                    <span class="total_tltle">合计</span>
                    <span class="total_Amount">¥ <span class="total_Money">{{orderAmount}}</span></span>
                </span>
                <span class="total_span" v-if="couponDetail.IsCostPrice == 0">
                    <span class="total_tltle">合计</span>
                    <span class="total_Amount">¥ <span class="total_Money">{{totalMoney}}</span></span>
                </span>
            </div>
        </div>


        <van-button size="large" class="loginBtn" type="submit" @click="actionBuyHandle(count)">立即购买 </van-button>


    </div>


</template>

<script>
    import {
        Button

    } from "vant";

    export default {
        name: 'giftcard',
        data() {
            return {
                couponDetail: "",
                CouponTypeId: 0,
                count: 1,

            }
        },
        computed: {
            orderAmount: function() {
                var that = this;
                return (that.count * that.couponDetail.CostPrice).toFixed(2)
            },
            totalMoney: function() {
                var that = this;
                return (that.count * that.couponDetail.Money).toFixed(2)
            },
        },
        components: {
            "van-button": Button
        },
        watch: {
            '$route': {
                handler: function(to, from) {
                    var that = this;
                    console.log(to)
                    that.gId = to.query.gId;
                    that.couponDetailMethod();
                },
                immediate: true
            }
        },
        methods: {
            reduceHandle: function() {
                var that = this;
                var count = Number(that.count)
                if (count > 1) {
                    that.count = count - 1;
                }
            },
            addHandle: function() {
                var that = this;

                var count = Number(that.count)

                that.count = count + 1;
            },
            //商品详情接口
            couponDetailMethod: function() {
                var that = this;
                //请求数据
                var requestData = {
                    "CouponTypeId": that.gId
                };
                that.postData("/Coupon/CouponDetail", requestData).then(function(data) {

                    if (data.data.Code == "100") {
                        that.couponDetail = data.data.Data;
                        that.CouponTypeId = data.data.Data.CouponTypeId;
                        // that.CostPrice = data.data.Data.CostPrice
                        // that.total = data.data.Data.Money
                        // that.IsCostPrice == data.data.Data.IsCostPrice
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }

                });

            },

            //立即购买
            actionBuyHandle: function(count) {
                var that = this;
                that.Common.confirmDialog('物流停运，2月22号恢复发货，是否继续兑换',function(){
                    if (that.count == 0) {
                        that.Common.showMsg("数量不能为0")
                    } else {
                        that.$router.push({
                            path: '/giftPayOrder',
                            query: {
                                count: that.count,
                                gId: that.gId,
                                isCartBuy: false
                            }
                        })
                    }
                })

            },
            // //立即购买
            // actionBuyHandle: function(count) {
            //     var that = this;
            //     if (that.count == 0) {
            //         that.Common.showMsg("数量不能为0")
            //     } else {
            //         that.$router.push({
            //             path: '/giftPayOrder',
            //             query: {
            //                 count: that.count,
            //                 gId: that.gId,
            //                 isCartBuy: false
            //             }
            //         })
            //     }



            // },
        },
        mounted() {

        },
        created() {
            var that = this;

        },

    }
</script>
<style scoped src="../../static/css/giftdetails.css">

</style>