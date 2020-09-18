<template>

    <div class="allBox">
        <div class="titleBox">
            <div class="title">
                联发行
            </div>
            <div class="titleDecript">
                专注于高效的礼品卡服务
                <div class="titleLine"></div>
            </div>
        </div>
        <div class="giftTitle">
            三月花朝节礼品卡
        </div>
        <div class="giftImg">
            <img src="../../static/images/productDetail/gift.png" alt="">
        </div>
        <div class="showTitle">
            礼品展示
        </div>
        <!--商品-->
        <div class="productBox">
            <img src="../../static/images/productDetail/giftProduct.png" alt="" class="giftProduct">
            <div class="productRight">
                <div class="productTitle">
                    三月花朝节礼品卡三月花朝节礼品卡三月花朝节礼品卡
                </div>
                <div class="productItem">
                    金龙鱼芝麻香油（400ml）*1
                </div>
                <div class="productItem">
                    金龙鱼面条51优+荞麦面条600g*1
                </div>
                <div class="productItem">
                    金龙鱼葵花油5L*1
                </div>
            </div>

        </div>
        <div class="priceTitle">
            价格
        </div>
        <div class="price">
            <span class="icon">￥</span>299.00
        </div>
        <div class="btn">
            立即购买
        </div>

    </div>


</template>

<script>
    import {Swipe, SwipeItem} from 'vant';

    export default {
        name: 'giftDetail',
        data() {
            return {

            };
        },
        computed: {},
        components: {
            "van-swipe": Swipe,
            "van-swipe-item": SwipeItem,
        },
        methods: {
            //立即购买
            buyHandle:function(){
              var that = this;
              that.$router.push('/payOrder')
            },
        },
        mounted() {
            var that = this;
        },
        created() {
            var that = this;

        },
        beforeRouteLeave(to, from, next) {
            var that = this;
            if (to.name === 'payOrder') {
                this.changeKeepAlive('payOrder', false)
            }
            next()
        }
    }

</script>
<style scoped src="../../static/css/giftDetail.css">


</style>
