<template>

    <div class="allBox">
    </div>


</template>

<script>
    import wx from 'weixin-js-sdk'

    export default {
        name: 'middlePage',
        data() {
            return {
                requestData: "",
                payData:"",
            };
        },
        components: {},
        methods: {
            onBridgeReady: function () {
                var that = this;
                //微信端调起支付
                WeixinJSBridge.invoke('getBrandWCPayRequest', {
                        "appId": that.GLOBAL.appId, //公众号名称，由商户传入,不用修改
                        "timeStamp": that.payData.data.Data.JsApiParameter.TimeStamp, //时间戳，由接口返回
                        "nonceStr":that.payData.data.Data.JsApiParameter.NonceStr, //随机串，由接口返回
                        "package": that.payData.data.Data.JsApiParameter.Package, //扩展包，由接口返回
                        "signType": "MD5", //微信签名方式:MD5,不用修改
                        "paySign": that.payData.data.Data.JsApiParameter.PaySign //微信签名，由接口返回
                    },
                    function (res) {
                        console.log("jsdk", res)
                        if (res.err_msg == "get_brand_wcpay_request:ok") {
                            that.Common.showMsg("支付成功", function () {
                                localStorage.removeItem('payObj')
                                that.$router.replace('/myOrder')
                            });

                        } else if (res.err_msg == "get_brand_wcpay_request:cancel") {
                            //用户取消支付后跳转地址
                            that.Common.showMsg("取消支付", function () {
                                localStorage.removeItem('payObj')
                                that.$router.replace('/myOrder')
                            });
                        } else {
                            that.Common.showMsg(res.errMsg, function () {
                                localStorage.removeItem('payObj')
                                that.$router.replace('/myOrder')
                            });

                            //用户支付失败后跳转地址
                        }
                        // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
                        //因此微信团队建议，当收到ok返回时，向商户后台询问是否收到交易成功的通知，若收到通知，前端展示交易成功的界面；若此时未收到通知，商户后台主动调用查询订单接口，查询订单的当前状态，并反馈给前端展示相应的界面。
                    });
            },
            //提交订单接口(普通商品)
            submitOrder: function () {
                var that = this;
                var methodName = '/OrderHandel/SubmitOrder'
                var requestData = that.requestData;
                that.postData(methodName, requestData).then(function (data) {
                    if (data.data.Code == "100") {
                        that.payData = data;
                        if (typeof WeixinJSBridge == "undefined") {//微信浏览器内置对象。参考微信官方文档
                            if (document.addEventListener) {
                                document.addEventListener('WeixinJSBridgeReady', that.onBridgeReady, false);
                            } else if (document.attachEvent) {
                                document.attachEvent('WeixinJSBridgeReady', that.onBridgeReady);
                                document.attachEvent('onWeixinJSBridgeReady', that.onBridgeReady);
                            }
                        } else {
                            that.onBridgeReady();
                        }
                    } else {
                        that.Common.showMsg(data.data.Message, function () {
                            that.$router.go(-1)
                        })
                    }

                });
            },
            //提交订单接口(礼品卡)
            submitOrderG: function () {
                var that = this;
                var methodName = '/OrderHandel/SubmitBuyCardOrder'
                var requestData = that.requestData
                that.disabled = true;
                that.postData(methodName, requestData).then(function (data) {

                    if (data.data.Code == "100") {
                        that.payData = data;
                        if (typeof WeixinJSBridge == "undefined") {//微信浏览器内置对象。参考微信官方文档
                            if (document.addEventListener) {
                                document.addEventListener('WeixinJSBridgeReady', that.onBridgeReady, false);
                            } else if (document.attachEvent) {
                                document.attachEvent('WeixinJSBridgeReady', that.onBridgeReady);
                                document.attachEvent('onWeixinJSBridgeReady', that.onBridgeReady);
                            }
                        } else {
                            that.onBridgeReady();
                        }


                    } else {
                        that.Common.showMsg(data.data.Message, function () {
                            that.$router.go(-1)
                        })
                    }

                });
            },
            //提交订单接口(重新提交订单)
            submitOrderA: function () {
                var that = this;
                var methodName = '/OrderHandel/ReSubmitOrder'
                var requestData = that.requestData;
                that.postData(methodName, requestData).then(function (data) {
                    if (data.data.Code == "100") {
                        that.payData = data;
                        if (typeof WeixinJSBridge == "undefined") {//微信浏览器内置对象。参考微信官方文档
                            if (document.addEventListener) {
                                document.addEventListener('WeixinJSBridgeReady', that.onBridgeReady, false);
                            } else if (document.attachEvent) {
                                document.attachEvent('WeixinJSBridgeReady', that.onBridgeReady);
                                document.attachEvent('onWeixinJSBridgeReady', that.onBridgeReady);
                            }
                        } else {
                            that.onBridgeReady();
                        }
                    } else {
                        that.Common.showMsg(data.data.Message, function () {
                            that.$router.go(-1)
                        })
                    }

                });
            },
        },
        mounted() {
            var that = this;

            if (that.type == "product") {
                that.submitOrder();
            } else if(that.type == "gift") {
                that.submitOrderG();
            }else{
                //重新提交订单
                that.submitOrderA();

            }


        },
        created() {
            var that = this;
            var obj = JSON.parse(localStorage.getItem('payObj'))
            var code = that.$route.query.code;
            obj.Code = code;
            that.requestData = obj;
            that.type = that.$route.query.type;
            console.log(obj)
        },
    }
</script>
<style>

</style>