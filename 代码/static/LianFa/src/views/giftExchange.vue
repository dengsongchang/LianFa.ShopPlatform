<template>

    <div class="allBox">
        <img :src="CouponFullImg" class="giftImg" alt="">
        <div class="contentBox">
            <div class="contentItem">
                <img src="../../static/images/gift/icon1.png" class="icon" alt="">
                <van-field v-model="CouponSn" placeholder="请输入序列号" class="inputItem" />
            </div>
            <div class="contentItem">
                <img src="../../static/images/gift/icon2.png" class="icon2" alt="">
                <van-field v-model="Password " placeholder="请输入验证码/密码" class="inputItem" />
            </div>
        </div>
        <router-link to="/giftinquiry">
            <div class="check">礼品卡有效期查询</div>
        </router-link>
        <div class="btn" @click="jumpgiftdetails(CouponSn,Password)">兑换</div>

        <footerNav></footerNav>
    </div>


</template>

<script>
    import {
        Field
    } from 'vant';
    import footerNav from "../components/footer.vue";

    export default {
        name: 'giftExchange',
        data() {
            return {
                CouponSn: "",
                Password: "",
                CouponId: "",
                CouponFullImg: "",

            };
        },
        computed: {},
        components: {
            "van-field": Field,
            "footerNav": footerNav,
        },
        methods: {
            // getFile: function() {
            //     var that = this;
            //     if (localStorage.token == null || localStorage.token == '') {
            //         that.Common.showMsg("请登录", function() {
            //             that.$router.push("/login");
            //         });
            //     } else {
            //         return
            //     }
            // },
            jumpgiftdetails: function() {
                var that = this;
                if (that.CouponSn == "") {
                    that.Common.showMsg("请输入序列号");
                } else if (that.Password == "") {
                    that.Common.showMsg("请输入密码");
                } else {
                    var data = {
                        CouponSn: that.CouponSn,
                        Password: that.Password,

                    };
                    that.postData("/Coupon/VerifyCouponInfo", data).then(function(data) {
                        if (data.data.Code == "100") {
                            that.CouponId = data.data.Data.CouponId
                            that.$router.push({
                                path: "/cardPayOrder",
                                query: {
                                    CouponId: that.CouponId,

                                }
                            });
                        } else {
                            that.Common.showMsg(data.data.Message)
                        }
                    });
                }

            },
            IsLogin: function() {
                var that = this;

                var data = {


                }
                that.postData("/Coupon/IsLogin", data).then(function(data) {
                    if (data.data.Code == "100") {

                    } else {
                        //返回未登录状态提示请登录
                        if (!data.data.Data) {
                            that.Common.showMsg("请登录", function() {
                                //未登录时将登录前页面记录
                                localStorage.removeItem("beforeLoginUrl");
                                localStorage.beforeLoginUrl = window.location.href;
                                that.$router.push("/login");
                            });
                        }
                    }
                });
            },

            getCouponImgInfo: function() {
                var that = this;

                var data = {


                }
                that.postData("/Coupon/GetCouponImgInfo", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.CouponImg = data.data.Data.CouponImg
                        that.CouponFullImg = data.data.Data.CouponFullImg
                        that.IsLogin();
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }
                });
            }

        },
        mounted() {
            var that = this;

            that.getCouponImgInfo();

            that.Common.showMsg('您好，因春节期间物流暂停收货，2月3号~2月21号的单暂停发货，您可先行兑换，2月22号恢复发货，造成不便，敬请谅解，谢谢！')
            // localStorage.getItem("token");
        },
        created() {
            var that = this;
            var that = this;
            //底部导航栏
            that.$store.commit('setFooterNav', 'gift')
        },
    }
</script>
<style scoped src="../../static/css/giftExchange.css">

</style>