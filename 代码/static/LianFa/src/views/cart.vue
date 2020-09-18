<template>

    <div class="allBox">
        <!--有商品的时候-->
        <div class="cartBox" v-if="isShow">
            <!--编辑按钮-->
            <div class="editBox">
                <div class="editBtn" @click="changeHandle">{{editShow ? '完成' : "编辑"}}</div>
            </div>
            <van-swipe-cell :right-width="65" :left-width="0" v-for="item,index in CartInfoList" :key="index">
                <div class="cartItem">
                    <!--店铺商品列表-->
                    <div class="productItem" @click=Jumpdetails(item.OrderProductInfo.PId)>
                        <div class="productIcon">
                            <img src="../../static/images/cart/noSelect.png" class="iconImg" alt=""
                                 v-if="!item.IsSelected" @click.stop="singleClickHandle(index)">
                            <img src="../../static/images/cart/select.png" class="iconImg" alt="" v-if="item.IsSelected"
                                 @click.stop="singleClickHandle(index)">
                        </div>
                        <img :src="item.OrderProductInfo.ShowImg" class="productImg" alt="">
                        <div class="productRightBox">
                            <div class="productName">
                                {{item.OrderProductInfo.Name}}
                            </div>
                            <div class="priceBox">
                                <div class="price" v-if="item.IsCostPrice == 0"><span class="icon">¥</span>{{item.OrderProductInfo.ShopPrice}}</div>
                                <div class="price" v-if="item.IsCostPrice == 1"><span class="icon">¥</span>{{item.OrderProductInfo.CostPrice}}</div>
                                <!--数量加减-->
                                <div class="countBox">
                                    <div class="reduceBtn"
                                         @click.stop="reduceHandle(item.OrderProductInfo.PId,item.OrderProductInfo.BuyCount)">
                                        <img src="../../static/images/cart/reduce.png" class="reduceIcon" alt="">
                                    </div>
                                    <input type="number" readonly class="count"
                                           v-model="item.OrderProductInfo.BuyCount">
                                    <div class="addBtn"
                                         @click.stop="addHandle(item.OrderProductInfo.PId,item.OrderProductInfo.BuyCount)">
                                        <img src="../../static/images/cart/add.png" class="addIcon" alt="">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <span slot="right" class="delectBtn" @click="delectHandle(item.OrderProductInfo.PId)">删除</span>
            </van-swipe-cell>
        </div>
        <!--去结算-->
        <div class="payBox" v-if="isShow">
            <div class="payLeft">
                <div class="paySelectIcon">
                    <img src="../../static/images/cart/noSelect.png" class="iconImg" alt="" v-if="!allSelect"
                         @click="allClickHandle()">
                    <img src="../../static/images/cart/select.png" class="iconImg" alt="" v-if="allSelect"
                         @click="allClickHandle()">
                </div>
                <div class="allS">全选</div>

            </div>

            <div class="payRight" v-show="!editShow" @click="toPay">
                结算
            </div>
            <div class="payRight" v-show="editShow" @click="delectAllHandle">
                删除
            </div>
            <div class="payMiddle">
                <div class="left">
                    合计：
                </div>
                <div class="left color">
                    ¥{{OrderAmount}}
                </div>
            </div>

        </div>

        <!--没商品的时候-->
        <div class="noProduct" v-if="!isShow">
        <img src="../../static/images/cart/no.png" class="noProductImg" alt="">
        <div class="noText">购物车内暂无商品，去首页看看吧</div>
        <div class="goHome" @click="gethomepege">去首页</div>
        </div>
        <footerNav></footerNav>
    </div>


</template>

<script>
    import {
        mapState
    } from 'vuex'
    import {
        Checkbox,
        Stepper,
        SwipeCell
    } from 'vant'
    import footerNav from "../components/footer.vue";

    export default {
        name: 'cart',
        data() {
            return {
                checked: false,
                value: 1,
                count: 1,
                icon: {
                    normal: require('../../static/images/cart/noSelect.png'),
                    active: require('../../static/images/cart/select.png')
                },
                editShow: false,
                CartInfoList: [],
                //是否全选
                allSelect: false,
                isShow: "",
            };
        },
        computed: {},
        components: {
            "van-checkbox": Checkbox,
            "van-stepper": Stepper,
            "van-swipe-cell": SwipeCell,
            "footerNav": footerNav,
        },
        methods: {
            //查询购物车商品列表接口
            queryCardList: function() {
                var that = this;
                //请求数据
                var requestData = {};
                that.postData("/Cart/QueryCardList", requestData).then(function(data) {
                    if (data.data.Code == "100") {
                        that.CartInfoList = data.data.Data.CartInfo.CartProductList;
                        that.isShow = data.data.Data.CartInfo.CartProductList.length;
                        that.OrderAmount = data.data.Data.OrderAmount;
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }
                });

            },
            //商品单选
            singleClickHandle: function(index) {
                var that = this;
                var state = that.CartInfoList[index].IsSelected;
                that.CartInfoList[index].IsSelected = !state;
                //计算总价
                that.dealPriceHandle();
            },
            //全选
            allClickHandle: function() {
                var that = this;
                var state = !that.allSelect;
                var cartInfoList = that.CartInfoList;
                cartInfoList.forEach(function(item, index) {
                    //如果是选中
                    if (state) {
                        if (!item.Failure) {
                            item.IsSelected = true;
                        }
                    } else {
                        if (!item.Failure) {
                            item.IsSelected = false;
                        }
                    }
                })
                that.CartInfoList = cartInfoList;
                that.allSelect = state;
                //计算价格
                that.dealPriceHandle();
            },
            //计算总价
            dealPriceHandle: function() {
                var that = this;
                var cartInfoList = that.CartInfoList;
                var total = 0;
                if (cartInfoList.length > 0) {
                    cartInfoList.forEach(function(item, index) {
                        //选中并且不是失效的商品
                        if (item.IsSelected && !item.Failure) {
                            //判断是否是特价
                            if (item.IsCostPrice) {
                                total += item.OrderProductInfo.BuyCount * item.OrderProductInfo.CostPrice
                            } else {
                                total += item.OrderProductInfo.BuyCount * item.OrderProductInfo.ShopPrice
                            }

                        }
                    })
                }
                that.OrderAmount = total.toFixed(2);
            },
            //获取选择的项
            getSelectListHandle: function() {
                var that = this;
                var cartInfoList = that.CartInfoList;
                var list = [];
                cartInfoList.forEach(function(item, index) {
                    if (!item.Failure) {
                        if (item.IsSelected) {
                            list.push(item.OrderProductInfo.PId)
                        }
                    }
                })
                return list;
            },
            toPay: function() {
                var that = this;
                var list = [];
                var cartInfoList = that.CartInfoList;
                cartInfoList.forEach(function(item, index) {
                    //排除失效的
                    if (!item.Failure) {
                        if (item.IsSelected) {
                            list.push(item.OrderProductInfo.PId)
                        }
                    }
                })
                var str = list.join(',')
                that.$router.push({
                    path: '/payOrder',
                    query: {
                        count: that.count,
                        pId: that.pId,
                        isCartBuy: true,
                        cartStr: str
                    }
                })
            },
            reduceHandle: function(pId, count) {
                var that = this;
                var list = that.getSelectListHandle();
                var num = Number(count) - 1;
                if (num >= 1) {
                    that.changeProductCount(num, pId, list)
                }
            },
            addHandle: function(pId, count) {
                var that = this;
                var list = that.getSelectListHandle();
                var num = Number(count) + 1;
                that.changeProductCount(num, pId, list)

            },
            //修改购物车中商品的数量接口
            changeProductCount: function(num, pId, list) {
                var that = this;
                var requestData = {
                    "PId": pId,
                    "Count": num,
                    "SelectedCartItemKeyList": list
                };
                that.postData("/Cart/ChangeProductCount", requestData).then(function(data) {

                    if (data.data.Code == "100") {
                        that.CartInfoList = data.data.Data.CartInfo.CartProductList;
                        that.isShow = data.data.Data.CartInfo.CartProductList.length;
                        that.OrderAmount = data.data.Data.OrderAmount;
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }

                });
            },
            gethomepege: function() {
                var that = this;
                that.$router.push({
                    path: "/",

                });
            },
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
            //删除购物车中的商品(单个)
            delectHandle: function(pId) {
                var that = this;
                var list = that.getSelectListHandle();
                var pids = [];
                pids.push(pId)
                that.delPruduct(pids, list)
            },
            //批量删除
            delectAllHandle: function() {
                var that = this;
                var list = that.getSelectListHandle();
                that.delPruduct(list, list)
            },
            //删除购物车中的商品接口
            delPruduct: function(pId, list) {
                var that = this;
                var requestData = {
                    "PIds": pId,
                    "SelectedCartItemKeyList": list,
                };
                that.postData("/Cart/DelProduct", requestData).then(function(data) {

                    if (data.data.Code == "100") {

                        that.CartInfoList = data.data.Data.CartInfo.CartProductList;
                        that.isShow = data.data.Data.CartInfo.CartProductList.length;
                        that.OrderAmount = data.data.Data.OrderAmount;
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }

                });
            },
            changeHandle: function() {
                var that = this;
                that.editShow = !that.editShow;
            },
        },
        mounted() {
            var that = this;
            that.queryCardList();
        },
        created() {
            var that = this;
            //底部导航栏
            that.$store.commit('setFooterNav', 'cart')

        },
    }
</script>
<style scoped src="../../static/css/cart.css">

</style>