<template>

    <div class="allBox">
        <!--轮播图-->
        <van-swipe class="vanSwipe" :autoplay="3000" indicator-color="#999" :show-indicators="false">
            <van-swipe-item class="swipeItem" v-for="item,index in productDetails.ShowImgList" :key="index">
                <img :src="item" alt="">
                <div class="swipIndex"><span class="weight">{{index+1}}</span>/{{productDetails.ShowImgList.length}}
                </div>
            </van-swipe-item>
        </van-swipe>
        <!--商品名称-->
        <div class="productBox">
            <div class="priceBox" v-if="productDetails.IsCostPrice == 0">
                <div class="priceIcon">
                    ¥
                </div>
                <div class="price">
                    {{productDetails.ShopPrice}}
                </div>
            </div>
            <div class="priceBox" v-if="productDetails.IsCostPrice == 1">
                    <div class="CostPrice">
                        <span class="CostPrice_Amount">
                            ¥ <span class="CostPrice_span">{{productDetails.CostPrice}}</span>
                        </span>
                        <span class="ShopPrice"> ¥ {{productDetails.ShopPrice}}</span>
                    </div>



            </div>
            <div class="productName">
                {{productDetails.Name}}
            </div>

        </div>
        <!--tab-->
        <div class="tab">
            <div class="tabItem" v-for="item,index in tabList" :key="index" ref="tabItem"
                :class="active == index ? 'tabActive' : ''" @click="tabHandle(index)">
                <div style="text-align: center">{{item.title}}</div>
            </div>
            <div class="middleLine"></div>
        </div>
        <!--content-->
        <div class="tabContext" v-show="active == 0" v-html="productDetails.Description">

        </div>
        <div class="tabContext" v-show="active == 1" v-html="productDetails.Summary">

        </div>
        <!--底部-->
        <div class="productFooterBox">
            <div class="footerItem iconBox">
                <div class="iconLeftBox" @click="toHome">
                    <img src="../../static/images/productDetail/home.png" class="homeIcon" alt="">
                    <div class="text">商城</div>
                </div>
                <div class="iconRightBox" @click="toCart">
                    <img src="../../static/images/productDetail/kefu.png" class="homeIcon" alt="">
                    <div class="text">购物车</div>
                </div>

            </div>
            <div class="footerItem cartBtn" @click="buyHandle">加入购物车</div>
            <div class="footerItem soonBtn" @click="buyHandle">立即购买</div>
        </div>

        <!--购买弹窗-->
        <div class="mark" v-if="isShow">

        </div>
        <div class="skuModel" v-if="isShow">
            <div class="skuContentBox">
                <div class="skuTopBox">
                    <img :src="productDetails.ShowImgFull" class="skuImg" alt="">
                    <div class="skuTopRight">
                        <div class="skuPrice" v-if="productDetails.IsCostPrice == 0">
                            <span class="iconText">¥</span>{{productDetails.ShopPrice}}
                        </div>
                        <div class="skuPrice" v-if="productDetails.IsCostPrice == 1">
                                <span class="iconText">¥</span>{{productDetails.CostPrice}}
                            </div>
                        <img src="../../static/images/productDetail/cha.png" class="delectIcon" alt=""
                            @click.prevent="closeHandle">
                    </div>
                </div>
                <!--数量选择-->
                <div class="countBox">
                    <div class="countText">数量</div>
                    <div class="countChangeBox">
                        <img src="../../static/images/productDetail/reduce.png"
                            @click.prevent="changeCountHandle('reduce')" class="addIcon" alt="">
                        <div class="count">{{count}}</div>
                        <img src="../../static/images/productDetail/jia.png" @click.prevent="changeCountHandle('add')"
                            class="addIcon" alt="">
                    </div>
                </div>
            </div>
            <div class="btnBox">
                <div class="btnLeft" @click="addCartHandle(productDetails.PId)">
                    加入购物车
                </div>
                <div class="btnRight" @click="actionBuyHandle(productDetails.PId)">
                    立即购买
                </div>
            </div>
        </div>
    </div>


</template>

<script>
    import {
        Swipe,
        SwipeItem
    } from 'vant';

    export default {
        name: 'productDetail',
        data() {
            return {
                tabList: [{
                    'title': "商品描述"
                }, {
                    'title': "参数"
                }, ],
                active: 0,
                left: 0,
                lineWidth: 0,
                productDetails: "",
                isShow: "",
                count: 1,
            };
        },
        computed: {},
        components: {
            "van-swipe": Swipe,
            "van-swipe-item": SwipeItem,
        },
        watch: {
            '$route': {
                handler: function(to, from) {
                    var that = this;
                    console.log(to)
                    that.pId = to.query.pId;
                    that.init();
                    that.productDetailMethod();
                },
                immediate: true
            }
        },
        methods: {
            //更改数量
            changeCountHandle: function(type) {
                var that = this;
                if (type == 'add') {
                    that.count += 1;
                } else {
                    if (that.count > 1) {
                        that.count -= 1;
                    }
                }

            },
            //加入购物车
            addCartHandle: function(PId) {
                var that = this;

                var requestData = {
                    "PId": PId,
                    "Count": that.count,
                    "Sku": ""
                };
                that.postData("/Cart/AddProduct", requestData).then(function(data) {
                    if (data.data.Code == "100") {
                        that.Common.showMsg("加入成功", function() {
                            that.$router.push('/cart')
                        })
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }

                });
            },
            //商品详情接口
            productDetailMethod: function() {
                var that = this;
                //请求数据
                var requestData = {
                    "PId": that.$route.query.PId,
                };
                that.postData("/IndexData/ProductDetail", requestData).then(function(data) {

                    if (data.data.Code == "100") {
                        that.productDetails = data.data.Data;
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }

                });

            },
            //点击切换
            tabHandle: function(index) {
                var that = this;
                that.active = index;
                that.init();

            },
            //初始化
            init: function() {
                var that = this;
                that.$nextTick(function() {
                    var tabs = that.$refs.tabItem;
                    if (!tabs || !tabs[that.active]) {
                        return
                    }
                    var currentTab = tabs[that.active];
                    var docEl = document.documentElement;
                    var rem = docEl.clientWidth / 10;
                    var lineWidth = Math.ceil(60 / 37.5 * rem);
                    //获取距离最左边的距离
                    var currentTabLeft = Math.ceil(currentTab.offsetLeft);
                    //获取tab的宽度
                    var tabWidth = Math.ceil(currentTab.offsetWidth);
                    var left = Math.ceil(currentTabLeft + (currentTab.offsetWidth - lineWidth) / 2);
                    that.left = left + 'px';
                    that.lineWidth = lineWidth + 'px';
                    console.log("左边距", that.left)
                    console.log('线宽', that.lineWidth)
                })
            },
            //前往首页
            toHome: function() {
                var that = this;
                that.$router.push('/')
            },
            toCart: function() {
                var that = this;
                that.$router.push('/cart')
            },
            //前往评价页
            toEvaluation: function() {
                var that = this;
                that.$router.push('/evaluation')
            },
            //前往店铺
            toShop: function() {
                var that = this;
                that.$router.push('/businessShop')
            },
            //弹窗出现
            buyHandle: function() {
                var that = this;
                that.isShow = true;
            },
            //立即购买
            actionBuyHandle: function(PId) {
                var that = this;
                //库存
                var num = that.productDetails.Stock;
                //判断购买的数量与库存的关系
                if (that.count <= num) {
                    that.$router.push({
                        path: '/payOrder',
                        query: {
                            count: that.count,
                            PId: PId,
                            isCartBuy: false
                        }
                    })
                } else {
                    that.Common.showMsg('库存不足')
                }
            },
            closeHandle: function() {
                var that = this;
                that.isShow = "";
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
<style scoped src="../../static/css/productDetail.css">

</style>