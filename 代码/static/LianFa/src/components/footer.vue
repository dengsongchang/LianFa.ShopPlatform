<template>
    <div class="footerBox" ref="footMenu" v-show="isOriginHeight">
        <div class="footItem" @click="tabHandle('gift')">
            <img src="../../static/images/tab/home.png" v-show="active != 'gift'" class="homeIcon" alt="">
            <img src="../../static/images/tab/homeSelect.png" v-show="active == 'gift'" class="homeIcon" alt="">
            <div class="footText" :class="active == 'gift' ? 'active' : ''">礼品券</div>
        </div>
        <div class="footItem" @click="tabHandle('index')">
            <img src="../../static/images/tab/shop.png" v-show="active != 'index'" class="homeIcon" alt="">
            <img src="../../static/images/tab/shopSelect.png" v-show="active == 'index'" class="homeIcon" alt="">
            <div class="footText" :class="active == 'index' ? 'active' : ''">商城</div>
        </div>
        <div class="footItem" @click="tabHandle('cart')">
            <img src="../../static/images/tab/cart.png" v-show="active != 'cart'" class="homeIcon" alt="">
            <img src="../../static/images/tab/cartSelect.png" v-show="active == 'cart'" class="homeIcon" alt="">
            <div class="footText" :class="active == 'cart' ? 'active' : ''">购物车</div>
        </div>
        <div class="footItem" @click="tabHandle('person')">
            <img src="../../static/images/tab/person.png" v-show="active != 'person'" class="homeIcon" alt="">
            <img src="../../static/images/tab/personSelect.png" v-show="active == 'person'" class="homeIcon" alt="">
            <div class="footText" :class="active == 'person' ? 'active' : ''">个人中心</div>
        </div>
    </div>
</template>

<script>
    import {
        mapState
    } from 'vuex'

    export default {
        name: 'footerNav',
        data() {
            return {
                footClass: 'showClass',
                isOriginHeight: true,
                oHeight: 0,
                screenHeight: document.documentElement.clientHeight,
                originHeight: document.documentElement.clientHeight,
            };
        },
        computed: {
            ...mapState({
                active: function(state) {
                    return this.$store.state.footerActive;
                },
            }),
        },
        methods: {
            tabHandle: function(type) {
                var that = this;
                if (type == 'gift') {
                    that.$router.push({
                        path: '/giftExchange'
                    })
                } else if (type == 'index') {
                    that.$router.push({
                        path: '/'
                    })
                } else if (type == 'cart') {
                    that.$router.push({
                        path: '/cart'
                    })
                } else if (type == 'person') {
                    that.$router.push({
                        path: '/user'
                    })
                }

            },
        },
        mounted() {
            var that = this;


        },
        mounted() {
            let self = this;
            window.onresize = function() {
                return (function() {
                    self.screenHeight = document.documentElement.clientHeight;
                })()
            }
        },
        watch: {
            screenHeight(val) {
                if (this.originHeight > val + 100) { //加100为了兼容华为的返回键
                    this.isOriginHeight = false;
                } else {
                    this.isOriginHeight = true;
                }
            }
        }

    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped src="../../static/css/footer.css">

</style>