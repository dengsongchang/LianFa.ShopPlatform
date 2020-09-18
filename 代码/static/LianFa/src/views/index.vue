<template>
    <div class="allBox">
        <!-- 首页主图 -->

        <div class="home_img ">
            <van-swipe :autoplay="3000" indicator-color="#fff" class="bannerSwiper">
                <van-swipe-item v-for="(item, index) in BannerList" :key="index" :show-indicators="true">
                    <img :src="item.ShowImg" />
                </van-swipe-item>
            </van-swipe>
        </div>
        <!-- 礼品卡 -->
        <div class="giftcard">
            <div class="giftcard_name">
                <div class="giftcard_name_left">
                    <img src="../../static/images/index/giftcard.png" alt="">
                    <span>礼品卡</span>
                </div>

                <router-link to="/giftcard">
                    <div class="giftcard_name_right">
                        <span>更多</span>
                        <img src="../../static/images/index/left.png" alt="">
                    </div>
                </router-link>
            </div>
            <div class="giftcard_list">
                <ul>
                    <li v-for="(item, index) in CouponList" :key="index" @click="gettcarddetails(item.CouponTypeId)">
                        <img :src="item.CouponImg" alt="">
                        <span>{{item.Name}}</span>
                    </li>

                </ul>

            </div>
        </div>
        <!-- 最新产品 -->
        <!-- 最新产品 -->
        <div class="latest_products">
            <div class="products_text">
                <span class="products_name">最新产品</span>
                <div class="products_right" @click="activeshow">
                    <span>筛选</span>
                    <img src="../../static/images/index/filter.png" alt="">
                </div>
            </div>
            <div class="Sliding_list" v-show="isShrink">
                <div class="Sliding_list_box">
                    <div class="label_list">
                        <div class="labellist_name">品牌：</div>
                        <div class="list_name">
                            <div class="Selected" v-for="(item,index) in BrandList" :key="index"
                                :class="{ active:index==isActive }" @click="tabHandle(index,item.Id)">
                                {{item.Name}}
                            </div>
                        </div>

                    </div>
                    <div class="label_list">
                        <div class="labellist_name">分类：</div>
                        <div class="list_name">
                            <div class="Selected" v-for="(item,index) in CategoryList"
                                :class="{ active:index==showClass }" :key="index" @click="changeValue(index,item.Id)">
                                {{item.Name}}
                            </div>
                        </div>

                    </div>
                </div>
                <div class="btn">
                    <div class="btn_box">
                        <div class="reset" @click=getReset()>重置</div>
                        <div class="determine" @click="determine()">确定</div>
                    </div>

                </div>

            </div>
            <van-list v-model="loading" :loading-text="loadText" :finished="finished" finished-text="没有更多了"
                :error.sync="error" error-text="请求失败，点击重新加载" :immediate-check="true" @load="getProductList"
                :off-set="10">

                <div class="commodity">
                    <div class="commodity_list">
                        <ul>
                            <li v-for="(value,index) in ProductList" :key="index">
                                <div class="commodity_img" @click="Jumpdetails(value.PId)">
                                    <img :src="value.ShowImg" alt="">
                                </div>
                                <span class="commodity_name">{{value.Name}}</span>
                                <div class="commodity_price" v-if="value.IsCostPrice == 0">
                                    <span class="ShopPrice"> <span>¥</span>{{value.ShopPrice}}</span>
                                </div>
                                <span class="commodity_price" v-if="value.IsCostPrice == 1">
                                    ¥{{value.CostPrice}} <span class="CostPrice"> ¥{{value.ShopPrice}}</span>
                                </span>



                            </li>
                        </ul>
                    </div>

                </div>
            </van-list>

        </div>

        <!-- 广告图弹窗 -->
        <div class="model" v-show="mask_hide">
            <div class="dialog">
                <div class="dialog_img">
                    <img :src="ScreenImg" alt="">
                </div>
                <div class="dialog_off">
                    <img src="../../static/images/index/off_icon.png" alt="" v-on:click.once="noactive">
                </div>

            </div>
        </div>





        <footerNav v-show="footerNav_hide"></footerNav>
    </div>
</template>

<script>
    import footerNav from "../components/footer.vue";
    import {
        List,
        Popup,
        Swipe,
        SwipeItem

    } from 'vant';
    export default {
        name: "index",
        components: {
            "van-popup": Popup,
            "footerNav": footerNav,
            "van-list": List,
            "van-swipe": Swipe,
            "van-swipe-item": SwipeItem,
        },
        data() {

            return {
                Height: 200,
                finished: false, //加载完成
                error: false,
                total: 0, //列表总数
                loading: false, //加载
                // 商品列表
                ProductList: [],

                loadText: '加载中…',
                pageIndex: 1,

                footerNav_hide: false,
                mask_hide: true,
                ScreenImg: "",
                CouponList: [],
                BannerList: [],
                // 筛选开关
                isShrink: false,
                // 筛选分类列表
                CategoryList: [],
                // 筛选品牌列表
                BrandList: [],

                //品牌ID
                BrandId: 0,
                // 分类ID
                CateId: 0,
                // 品牌选中
                isActive: null,
                // 分类选中
                showClass: null,
            };
        },
        computed: {

        },

        methods: {
            // 跳转商品详情页，
            Jumpdetails: function(PId) {
                var that = this;
                that.$router.push({
                    path: "/productDetail",
                    query: {
                        PId: PId,
                    }
                });
            },
            gettcarddetails: function(CouponTypeId) {
                var that = this;
                that.$router.push({
                    path: "/giftdetails",
                    query: {
                        gId: CouponTypeId,
                    }
                });
            },
            // 品牌选中；
            tabHandle(item, Id) {
                var that = this;
                that.isActive = item;
                that.BrandId = Id;
            },
            // 分类选中；
            changeValue(item, Id) {
                var that = this;

                that.showClass = item;
                that.CateId = Id;

            },
            // 重置
            getReset: function() {
                var that = this;
                that.showClass = null;
                that.isActive = null;

                that.CateId = 0;
                that.BrandId = 0;
                that.ProductList = [];
                that.pageIndex = 1;
                that.loading = false;
                that.finished = false;
            },
            // 筛选确定
            determine: function() {
                var that = this;
                that.isShrink = false;

                that.ProductList = [];
                that.pageIndex = 1;
                that.getProductList();
            },

            // 商品列表
            getProductList: function() {
                var that = this;
                var data = {
                    "BrandId": that.BrandId ? that.BrandId : 0,
                    "CateId": that.CateId ? that.CateId : 0,
                    "Page": {
                        "PageIndex": that.pageIndex,
                        "PageSize": 6
                    }
                };
                console.log(data)
                that.postData("/IndexData/ProductList", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.ProductList = that.ProductList.concat(data.data.Data.ProductList);
                        that.total = data.data.Data.Total;
                        that.pageIndex += 1;
                        // 加载状态结束
                        that.loading = false;
                        // 数据全部加载完成

                        if (that.ProductList.length >= that.total) {
                            that.finished = true;
                        }
                    } else {
                        that.error = true;
                        that.Common.showMsg(data.data.Message);
                    }
                });

            },
            noactive() {
                this.mask_hide = false;
                this.footerNav_hide = true;

            },
            activeshow() {

                this.isShrink = !this.isShrink;

            },

            GetScreenInfo: function() {
                var that = this;
                var data = {};
                that.postData("/IndexData/GetScreenInfo", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.ScreenImg = data.data.Data.ScreenImgFull
                    }
                });
            },
            getCouponList: function() {
                var that = this;
                var data = {
                    Page: {
                        PageIndex: 1,
                        PageSize: 4
                    }
                };
                that.postData("/IndexData/GetCouponList", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.CouponList = data.data.Data.CouponList;
                    }
                });
            },
            getGetBannerList: function() {
                var that = this;
                var methodName = "/IndexData/GetBannerList";
                var requestData = {};
                that.postData(methodName, requestData).then(function(data) {
                    if (data.data.Code == "100") {
                        that.BannerList = data.data.Data.BannerList;
                    } else {
                        that.Common.showMsg(data.data.Message);
                    }
                });
            },
            getBrandAndCategoryList: function() {
                var that = this;
                var methodName = "/IndexData/GetBrandAndCategoryList";
                var requestData = {};
                that.postData(methodName, requestData).then(function(data) {
                    if (data.data.Code == "100") {
                        that.BrandList = data.data.Data.BrandList;
                        that.CategoryList = data.data.Data.CategoryList;
                        that.BrandId = data.data.Data.BrandList.Id;
                        that.CateId = data.data.Data.CategoryList.Id;
                        // that.getProductList();
                    } else {
                        that.Common.showMsg(data.data.Message);
                    }
                });
            },


        },
        mounted() {
            var that = this;
            that.GetScreenInfo();
            that.getCouponList();
            that.getGetBannerList();
            that.getBrandAndCategoryList();
        },
        created() {
            var that = this;
            //底部导航栏
            that.$store.commit('setFooterNav', 'index')
            if (sessionStorage.getItem("mask_hide") == "false") {
                that.mask_hide = false;
                that.footerNav_hide = true;
            } else {
                that.mask_hide = true;
                that.footerNav_hide = false;
            }
            sessionStorage.setItem("mask_hide", "false");
        },
    }
</script>

<style scoped src="../../static/css/index.css">

</style>