<template>

    <div class="allBox">
        <div class="giftImg">
            <img :src="itme.CouponImg" alt="">
        </div>
        <div class="giftTitle">
            {{itme.Name}}
        </div>
        <!--标题-->
        <div class="titleItem">
            <img src="../../static/images/gift/icon.png" class="icon" alt="">
            <div class="titleName">礼品展示</div>
        </div>
        <div class="productBox">
            <img :src="itme.ProductImg " alt="" class="giftProduct">
            <div class="productRight">
                <div class="productTitle">
                    {{itme.Name}}
                </div>
                <div class="productItem" v-for="item,index in itme.Content" :key="index">
                    {{item}}
                </div>

                <!-- <div class="productItem">
                    金龙鱼葵花油5L*1
                </div> -->
            </div>

        </div>
        <div class="titleItem">
            <img src="../../static/images/gift/icon.png" class="icon" alt="">
            <div class="titleName">送货要求</div>
        </div>
        <textarea v-model="mark" class="mark" placeholder="填写相应的送货要求，若无要求则默认发货"></textarea>
        <div class="titleItem">
            <img src="../../static/images/gift/icon.png" class="icon" alt="">
            <div class="titleName">送货地址</div>
        </div>
        <!--地址-->
        <div class="addressBox">
            <div class="addressItem">
                <div class="addressIcon">
                    <img src="../../static/images/order/dingwei.png" class="dingIcon" alt="">
                    <span>配送至</span>
                </div>
                <!--超出范围-->
                <div class="beyond" v-if="itme.IsDeliveryArea == false && itme.Address !=null">该地址无法配送，请更换地址</div>
                <div class="addressTop">
                    <div class="address">
                        {{ProvinceName}}{{CityName}}{{CountyName}}</div>
                    <img src="../../static/images/order/right.png" class="bottomIcon" alt="" @click="popHandle">
                </div>
                <div class="addressBottom">
                    {{Address}}
                </div>
            </div>
        </div>
        <!--按钮-->
        <div class="btn" @click="SubmitCouponOrder(CouponId,SAId)">确定兑换</div>
        <!--地址选择-->
        <van-popup v-model="show" position="bottom" :overlay="true" style="background: transparent">
            <div class="chooseAddressBox">
                <div class="chooseTitle">
                    <div class="titleLeft">
                        我的地址
                    </div>
                    <div class="titleRight" @click="getaddAddress()">
                        新增地址
                    </div>
                </div>
                <div class="chooseAddress">
                    <div class="chooseAddressItem" v-for="(item,index) in ShipAddressList" :key="index"
                        @click="getaddress(item.SAId)">
                        <div class="chooseAddressItemLeft">
                            <div class="chooseAddressProvince" v-if="ShipAddressList.length !=0">
                                {{item.ProvinceName}}{{item.CityName}}{{item.RName}}
                            </div>
                            <div class="chooseAddressText">
                                {{item.Address}}
                            </div>
                        </div>
                        <img src="../../static/images/address/editA.png" class="chooseAddressItemRight" alt=""
                            @click="geteditaddress(item.SAId)" @click.stop>
                    </div>

                </div>
            </div>
        </van-popup>
    </div>


</template>

<script>
    import {
        Popup
    } from 'vant';

    export default {
        name: 'cardPayOrder',
        data() {
            return {
                mark: "",
                show: false,
                NameNoe: "",
                NameTow: "",
                ProvinceName: "",
                CityName: "",
                CountyName: "",
                Address: "",
                itme: {},
                id: "",
                ShipAddressList: [],
                addressID: "",

            };
        },
        computed: {},
        components: {
            "van-popup": Popup,
        },
        methods: {
            popHandle: function() {
                var that = this;
                that.show = true;
                var data = {

                };
                that.postData("/ShipAddress/ShipAddressList", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.ShipAddressList = data.data.Data.ShipAddressList;
                    }
                });
            },
            SetDefaultShipAddress: function(SAId) {
                var that = this;
                var data = {
                    SaId: SAId
                };
                that.postData("/ShipAddress/SetDefaultShipAddress", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.ShipAddressList = data.data.Data.ShipAddressList;
                    }
                });
            },
            SubmitCouponOrder: function(CouponId, SAId) {
                var that = this;
                var data = {
                    CouponId: that.CouponId,
                    SAId: that.SAId,
                    BuyerRemark: that.mark,

                };
                that.postData("/OrderHandel/SubmitCouponOrder", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.OId = data.data.Data.OId
                        that.$router.push({
                            path: "/checkSuccess",
                            query: {
                                OId: that.OId,
                            }
                        });
                    } else {
                        that.Common.showMsg(data.data.Message);
                    }
                });
            },
            GetCouponInfo: function(SAId) {

                var that = this;
                var data = {
                    CouponId: that.$route.query.CouponId,
                    SaId: SAId ? SAId : 0

                };
                that.postData("/Coupon/GetCouponInfo", data).then(function(data) {
                    if (data.data.Code == "100") {
                        if (data.data.Data.Address != null) {
                            that.ProvinceName = data.data.Data.Address.ProvinceName;
                            that.CityName = data.data.Data.Address.CityName;
                            that.CountyName = data.data.Data.Address.CountyName;
                            that.Address = data.data.Data.Address.Address;
                            that.SAId = data.data.Data.Address.SAId;

                        }


                        that.CouponId = data.data.Data.CouponId;
                        that.itme = data.data.Data;
                    }
                });
            },
            getaddress: function(SAId) {
                var that = this;
                console.log(SAId)
                that.show = false;
                that.SetDefaultShipAddress(SAId)
                that.GetCouponInfo(SAId)



            },
            geteditaddress: function(SAId) {
                var that = this;
                that.$router.push({
                    path: "/editAddress",
                    query: {
                        SAId: SAId,

                    }
                });
            },
            getaddAddress: function() {
                var that = this;
                that.$router.push({
                    path: "/addAddress",

                });
            }
        },
        mounted() {
            var that = this;
            that.GetCouponInfo();
        },
        created() {
            var that = this;

        },

    }
</script>
<style scoped src="../../static/css/cardPayOrder.css">

</style>