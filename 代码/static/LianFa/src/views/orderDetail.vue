<template>

    <div class="allBox">
        <!--地址-->
        <div class="addressBox">
            <div class="addressLeft">
                <div class="leftTop">
                    <img src="../../static/images/order/dingwei.png" class="addressIcon" alt="">
                    <div class="userName">{{OrderInfo.Consignee}}</div>
                    <div class="phone">{{OrderInfo.Mobile}}</div>
                </div>
                <div class="leftBottom">{{OrderInfo.Address}}</div>
            </div>
        </div>
        <div class="title">
            商品详情
        </div>
        <!--商品列表-->
        <div class="productBox">
            <!--商家-->

            <div class="storeBox">
                <!--商家的商品列表-->
                <div class="productItem" v-for="item,index in ProductsList" :key="index">
                    <img :src="item.ShowImg" class="productImg" alt="">
                    <div class="productNameBox">
                        <div class="productName">
                            {{item.ProductName}}
                        </div>
                        <div class="productPrice">
                            <div class="leftCount">x{{item.RealCount}}</div>
                            <div class="rightPrice">
                                <span class="priceIcon">¥</span>{{item.ShopPrice}}
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <!--描述-->
        <div class="descritionBox">
            <div class="descritionItem">
                <div class="descritionItemLeft">
                    商品价格
                </div>
                <div class="descritionItemRight">
                    {{OrderInfo.ProductAmount}}元
                </div>
            </div>
            <div class="descritionItem">
                <div class="descritionItemLeft">
                    运费
                </div>
                <div class="descritionItemRight">
                    {{OrderInfo.ShipFee}}元
                </div>
            </div>
            <!-- <div class="descritionItem">
                <div class="descritionItemLeft">
                    优惠
                </div>
                <div class="descritionItemRight">
                    0.00元
                </div>
            </div> -->
            <div class="descritionItem">
                <div class="descritionItemLeft">
                    合计
                </div>
                <div class="descritionItemRight">
                    {{OrderInfo.OrderAmount}}元
                </div>
            </div>
        </div>
        <div class="title">
            备注
        </div>
        <div class="markBox" v-if="!OrderInfo.BuyerRemark">
            {{OrderInfo.BuyerRemark}}
        </div>
        <div class="markBox" v-else>
            无
        </div>
        <div class="title">
            订单信息
        </div>
        <!--交易时间-->
        <div class="timeBox">
            <div class="timeItem">
                <div class="boxLeft">
                    订单编号
                </div>
                <div class="boxRight">
                    {{OrderInfo.OSn}}
                </div>
            </div>
            <div class="timeItem">
                <div class="boxLeft">
                    创建时间
                </div>
                <div class="boxRight">
                    {{OrderInfo.AddTimes}}
                </div>
            </div>
            <div class="timeItem">
                <div class="boxLeft">
                    付款时间
                </div>
                <div class="boxRight">
                    {{OrderInfo.AddTimes}}

                </div>
            </div>
        </div>
        <!-- <div class="btton">
            <div class="btn" @click="goTdelorder(OrderInfo.OId)">取消订单</div>
        </div> -->
        <van-button size="large" class="loginBtn" type="submit" @click="goTdelorder(OrderInfo.OId)">取消订单 </van-button>
    </div>


</template>

<script>
    import {
        Button

    } from "vant";

    export default {

        name: 'orderDetail',
        data() {
            return {
                OrderInfo: {},

                ProductsList: [],
            };
        },
        computed: {},
        components: {
            "van-button": Button
        },
        methods: {
            getOrderInfo: function() {
                var that = this;
                var data = {
                    oid: that.$route.query.OId
                };
                that.postData("/UCenter/OrderInfo", data).then(function(data) {
                    if (data.data.Code == "100") {
                        console.log(data.data.Data, "123131")
                        that.OrderInfo = data.data.Data.OrderInfo;
                        that.ProductsList = data.data.Data.ProductsList;

                    }
                });
            },
            goTdelorder: function(OId) {
                var that = this;
                var data = {
                    oid: OId
                }
                that.Common.confirmDialog("确定删除订单吗", function() {
                    that.postData("/UCenter/DelOrder", data).then(function(data) {
                        if (data.data.Code == "100") {

                            that.$router.go(-1)

                        } else {
                            that.Common.showMsg(data.data.Message);
                        }
                    });
                });

            },


        },
        mounted() {
            var that = this;
            console.log(that.$router)
            that.getOrderInfo();
        },
        created() {
            var that = this;

        },
    }
</script>
<style scoped src="../../static/css/orderDetail.css">

</style>