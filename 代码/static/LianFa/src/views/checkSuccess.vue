<template>

    <div class="allBox">
        <img src="../../static/images/cart/check.png" class="icon" alt="">
        <div class="text">礼品兑换成功，正快马加鞭送到</div>
        <div class="buttonBox">
            <div class="button" @click="Giftredemption()">继续兑换</div>
            <div class="button" @click="giftOrderDetail()">查看订单</div>
        </div>
    </div>


</template>

<script>
    export default {
        name: 'buySuccess',
        data() {
            return {};
        },
        computed: {},
        components: {},
        methods: {
            Giftredemption: function() {
                var that = this;
                that.$router.push({
                    path: "/giftExchange",

                });
            },
            giftOrderDetail: function() {
                var that = this;
                that.$router.push({
                    path: "/giftOrderDetail",
                    query: {
                        OId: that.$route.query.OId,
                    }
                });
            }
        },
        mounted() {
            var that = this;
        },
        created() {
            var that = this;

        },
    }
</script>
<style scoped src="../../static/css/buySuccess.css">

</style>