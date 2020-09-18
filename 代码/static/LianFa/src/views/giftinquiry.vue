<!-- 礼品查询 -->
<template>

        <div class="content">
            <div class="Inquire">
                <div class="Inquire_tltle">输入礼品卡卡号</div>
                <div class="Inquire_input">
                    <input v-model="cardnumber" clearable >
                </div>
                <van-button size="large" class="loginBtn" @click="formInquire" >查询</van-button>

                <div class="result" v-if="Inquire">
                    <div class="result_span">查询结果</div>
                    <div class="result_nmae">{{CouponName}}</div>
                    <div class="result_date">
                        <span class="result_noe">有效期：至{{ValidDates}}</span>
                        <span class="result_tow">{{StateDec}}</span>
                    </div>
                </div>


            </div>




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
                cardnumber: "",
                CouponName: "",
                ValidDates: "",
                StateDec: "",
                Inquire: false,

            }
        },
        computed: {},
        components: {
            "van-button": Button
        },
        methods: {
            formInquire: function() {
                var that = this;
                if (that.cardnumber == "") {
                    that.Common.showMsg("请输入礼品卡卡号");
                    return false;
                }
                that.GetCouponVaildDate()
            },
            GetCouponVaildDate: function() {
                var that = this;
                var methodName = "/Coupon/GetCouponVaildDate";
                //请求数据
                var reqData = {
                    CouponSn: that.cardnumber,
                };
                that.postData(methodName, reqData).then(function(res) {
                    if (res.data.Code == "100") {
                        that.StateDec = res.data.Data.StateDec;
                        that.CouponName = res.data.Data.CouponName;
                        that.ValidDates = res.data.Data.ValidDates;
                        that.Inquire = true;
                        that.Common.showMsg(res.data.Message);
                    } else {
                        that.Common.showMsg(res.data.Message);
                    }
                });
            }
        },
        mounted() {

        },
        created() {
            var that = this;

        },

    }
</script>
<style scoped src="../../static/css/giftinquiry.css">

</style>