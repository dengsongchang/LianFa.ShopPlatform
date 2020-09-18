<template>

    <div class="allBox">
        <img src="../../static/images/cart/success.png" class="icon" alt="">
        <div class="text">商品购买成功，正快马加鞭送到</div>
        <div class="buttonBox">
            <div class="button" @click="toHome">返回首页</div>
            <div class="button" @click="toOrder">查看订单</div>
        </div>
    </div>


</template>

<script>
    export default {
        name: 'buySuccess',
        data() {
            return {
            };
        },
        computed: {},
        components: {
        },
        methods: {
            toHome:function(){
                var that = this;
                that.$router.push('/')
            },
            toOrder:function(){
              var that = this;
                that.$router.push('/myOrder')
            },
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
