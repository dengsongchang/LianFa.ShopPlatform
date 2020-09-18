<template>

    <div class="allBox">
        <!--地址-->
        <div class="addressBox" @click="toAddress">
            <!--存在默认地址，并且没选择其他地址-->
            <div class="addressLeft" v-if="defaultFullShipAddressInfo && !shipAddressInfo">
                <div class="leftTop">
                    <img src="../../static/images/order/dingwei.png" class="addressIcon" alt="">
                    <div class="userName">{{defaultFullShipAddressInfo.Consignee}}</div>
                    <div class="phone">{{defaultFullShipAddressInfo.Mobile}}</div>
                </div>
                <div class="leftBottom">
                    {{defaultFullShipAddressInfo.ProvinceName}}{{defaultFullShipAddressInfo.CityName}}{{defaultFullShipAddressInfo.CountyName}}{{defaultFullShipAddressInfo.Address}}
                </div>
            </div>
            <!--选择了其他地址-->
            <div class="addressLeft" v-if="shipAddressInfo">
                <div class="leftTop">
                    <img src="../../static/images/order/dingwei.png" class="addressIcon" alt="">
                    <div class="userName">{{shipAddressInfo.Consignee}}</div>
                    <div class="phone">{{shipAddressInfo.Mobile}}</div>
                </div>
                <div class="leftBottom">
                    {{shipAddressInfo.ProvinceName}}{{shipAddressInfo.CityName}}{{shipAddressInfo.CountyName}}{{shipAddressInfo.Address}}
                </div>
            </div>
            <!--没有默认地址或者选了地址之后被删掉-->
            <div class="addressLeft" v-if="!defaultFullShipAddressInfo && !shipAddressInfo">
                <div class="leftTopNo">
                    <img src="../../static/images/order/dingwei.png" class="addressIcon" alt="">
                    <div class="userName">请设置收货地址</div>
                </div>
            </div>
            <div class="addressRight">
                <img src="../../static/images/order/right.png" class="rightIcon" alt="">
            </div>
        </div>
        <!--支付方式-->
        <div class="payBox">
            <div class="payTitle">支付方式</div>

            <van-radio-group v-model="radio">
                <div class="payItem">
                    <img src="../../static/images/order/weixin.png" class="payIcon" alt="">
                    <div class="payText">微信支付</div>
                    <div class="radioBox">
                        <van-radio class="radioBtn" name="1" checked-color="#D94E3A"></van-radio>
                    </div>
                </div>
            </van-radio-group>
        </div>
        <!--商品列表-->
        <div class="productBox">
            <div class="productTitle">商品详情</div>
            <!--商家-->
            <div class="storeBox">
                <!--商家的商品列表-->
                <div class="productItem" v-for="item,index in CartProductList" :key="index" v-if="item.IsSelected">
                    <img :src="item.OrderProductInfo.ShowImg" class="productImg" alt="">
                    <div class="productNameBox">
                        <div class="productName">
                            {{item.OrderProductInfo.Name}}
                        </div>
                        <div class="count">
                            x{{item.OrderProductInfo.BuyCount}}
                        </div>
                        <div class="productPrice" v-if="item.IsCostPrice ==0">
                            <span class="money">¥</span>
                            {{item.OrderProductInfo.BuyCount * item.OrderProductInfo.ShopPrice}}
                        </div>
                        <div class="productPrice" v-if="item.IsCostPrice ==1">
                            <span class="money">¥</span>
                            {{item.OrderProductInfo.BuyCount * item.OrderProductInfo.CostPrice}}
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--描述-->
        <div class="descritionBox">
            <div class="productTitle">订单详情</div>
            <div class="descritionItem">
                <div class="descritionItemLeft">
                    商品价格
                </div>
                <div class="descritionItemRight">
                    ¥{{confirmInfo.ProductAmount}}
                </div>
            </div>
            <div class="descritionItem">
                <div class="descritionItemLeft">
                    运费
                </div>
                <div class="descritionItemRight">
                    ¥{{confirmInfo.ShipFee}}
                </div>
            </div>
            <div class="descritionItem">
                <div class="descritionItemLeft">
                    合计
                </div>
                <div class="descritionItemRight">
                    ¥{{confirmInfo.OrderAmount}}
                </div>
            </div>
        </div>
        <!--留言-->
        <div class="markBox">
            <div class="markTitle">备注</div>
            <van-field v-model="mark" placeholder="填写相关的信息" class="mark" />
        </div>
        <!--结算-->
        <div class="footerBox">
            <div class="moneyBox">
                <span class="allPrice">合计：</span><span class="icon">¥</span>{{confirmInfo.OrderAmount}}
            </div>
            <button class="payBtn" @click="submitOrderHandle()" v-if="!disabled">去付款</button>
            <button class="payBtn" @click="submitOrderHandle()" v-if="disabled" disabled="disabled">去付款</button>
        </div>
    </div>


</template>

<script>
    import {
        RadioGroup,
        Radio,
        Checkbox,
        Field
    } from 'vant';
    import Common from "../../static/js/common";

    export default {
        name: 'payOrder',
        data() {
            return {
                //2代表微信支付
                payType: "2",
                checked: false,
                mark: "",
                isShow: false,
                count: 1,
                pId: 0,
                isCartBuy: "",
                saId: 0,
                confirmInfo: "",
                defaultFullShipAddressInfo: "",
                shipAddressInfo: "",
                cartStr: "",
                disabled: false,
                isWeChat: "",
                radio: '1',
                CartProductList: [],
            };
        },
        computed: {},
        components: {
            "van-radio-group": RadioGroup,
            "van-radio": Radio,
            "van-checkbox": Checkbox,
            "van-field": Field,
        },
        methods: {
            //确认订单接口
            confirmOrderHandle: function() {
                var that = this;
                var PId = that.$route.query.PId;
                //如果本地缓存存在said
                if (localStorage.SAId) {
                    that.saId = localStorage.SAId;
                    //说明选择了其他地址
                    that.getShipAddress()
                } else {
                    that.saId = 0;
                }
                //判断是什么方式购买
                if (that.isCartBuy != "false") {
                    var str = that.isCartBuy == 'false' ? "" : that.cartStr.split(",");
                    //购物车购买
                    var methodName = '/OrderHandel/ConfirmOrder'
                    var requestData = {
                        "PId": that.pId,
                        "Count": that.count,
                        "SaId": that.saId,
                        "Sku": "",
                        "SelectedCartItemKeyList": str
                    };
                } else {
                    //直接购买
                    var methodName = '/OrderHandel/DirectConfirmOrder'
                    var requestData = {
                        "PId": that.pId,
                        "Count": that.count,
                        "SaId": that.saId,
                        "Sku": "",
                        // "SelectedCartItemKeyList": [that.pId]
                    };
                }
                that.postData(methodName, requestData).then(function(data) {

                    if (data.data.Code == "100") {
                        //判断是否存在默认地址
                        if (data.data.Data.DefaultFullShipAddressInfo) {
                            that.defaultFullShipAddressInfo = data.data.Data.DefaultFullShipAddressInfo;
                            that.saId = data.data.Data.DefaultFullShipAddressInfo.SAId;
                        } else {
                            that.defaultFullShipAddressInfo = ""
                        }

                        that.confirmInfo = data.data.Data;
                        that.CartProductList = data.data.Data.CartInfo.CartProductList
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }

                });

            },
            //提交订单处理
            submitOrderHandle: function() {
                var that = this;
                var AppId = that.GLOBAL.appId;
                var code = that.$route.query.code;
                var local = encodeURIComponent('' + that.GLOBAL.urlPrefixNoAdmin + '/middlePage?type=product');
                //支付宝支付
                if (that.payType == 1) {
                    if (that.isWeChat) {
                        that.Common.showMsg('请在非微信浏览器打开')
                        return false
                    } else {
                        that.submitOrder();
                    }
                } else {
                    //H5支付
                    if (that.payType == 2 && !that.isWeChat) {
                        that.submitOrder();
                    } else {
                        if (that.saId > 0) {
                            //微信浏览器支付(去授权)
                            var str = that.isCartBuy == 'false' ? "" : that.cartStr.split(",");
                            var requestData = {
                                "IsDirectBuy": that.isCartBuy == 'false' ? '1' : '0',
                                "Count": that.isCartBuy == 'false' ? that.count : 0,
                                "SaId": that.saId,
                                "PayType": that.payType,
                                "SelectedCartItemKeyList": that.isCartBuy == 'false' ? [that.pId] : str,
                                "PayCreditCount": 0,
                                "BuyerRemark": that.mark,
                                "PId": that.pId,
                            };

                            var obj = JSON.stringify(requestData)
                            localStorage.setItem('payObj', obj)
                            var url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + AppId +
                                "&redirect_uri=" + local +
                                "&response_type=code&scope=snsapi_base#wechat_redirect";
                            window.location.href = url;
                        } else {
                            that.Common.showMsg("请选择配送地址")
                        }


                    }
                }
            },
            //提交订单接口
            submitOrder: function() {
                var that = this;
                var methodName = '/OrderHandel/SubmitOrder'
                var str = that.isCartBuy == 'false' ? "" : that.cartStr.split(",");
                var requestData = {
                    "IsDirectBuy": that.isCartBuy == 'false' ? '1' : '0',
                    "Count": that.isCartBuy == 'false' ? that.count : 0,
                    "SaId": that.saId,
                    "PayType": that.payType,
                    "SelectedCartItemKeyList": that.isCartBuy == 'false' ? [that.pId] : str,
                    "PayCreditCount": 0,
                    "BuyerRemark": that.mark,
                    "PId": that.pId,
                };
                that.disabled = true;
                that.postData(methodName, requestData).then(function(data) {

                    if (data.data.Code == "100") {

                        if (that.payType == 1) {
                            //支付宝支付
                            that.disabled = false;
                            location.href = data.data.Data.ALiParameter;
                            return false;
                        }

                        if (that.payType == 2 && !that.isWeChat) {
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
            //获取收货地址信息接口
            getShipAddress: function() {
                var that = this;
                //购物车购买
                var methodName = '/ShipAddress/GetShipAddress'
                var requestData = {
                    "SaId": that.saId,
                };
                that.postData(methodName, requestData).then(function(data) {

                    if (data.data.Code == "100") {
                        that.shipAddressInfo = data.data.Data.ShipAddressInfo;

                    } else {
                        //that.Common.showMsg(data.data.Message)
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
            toAddress: function() {
                var that = this;
                that.$router.push('/orderAddress')
            },
        },
        mounted() {
            var that = this;
            that.confirmOrderHandle();
        },
        created() {
            var that = this;
            //获取参数
            that.count = that.$route.query.count;
            that.pId = that.$route.query.PId;
            //是否是购物车购买
            that.isCartBuy = (that.$route.query.isCartBuy).toString();
            if (that.$route.query.isCartBuy != "false") {
                that.cartStr = that.$route.query.cartStr
            }
            that.isWeChatMethod();


        },
        activated() {
            var that = this;
            //调用确认订单接口

        },
        deactivated() {
            var that = this;
        },
        beforeRouteLeave(to, from, next) {
            if (to.name === 'orderAddress') {
                //表示去过地址管理
                sessionStorage.setItem('isToAddress', true)

            } else {
                //如果不是去选地址的页面，就将本地缓存的said清除
                localStorage.removeItem('SAId')
                sessionStorage.removeItem('isToAddress')
            }
            next()
        },
    }
</script>
<style scoped src="../../static/css/payOrder.css">

</style>