<template>

    <div class="allBox">
        <div class="addressItem">
            <div class="title">{{userName ? "姓名" : ""}}</div>
            <div class="inputItem">
                <input type="text" placeholder="姓名" v-model="userName" class="item">
            </div>
        </div>
        <div class="addressItem">
            <div class="title">{{phone ? "手机号码" : ""}}</div>
            <div class="inputItem">
                <input type="text" placeholder="手机号码" v-model="phone" class="item">
            </div>
        </div>
        <div class="addressItem" @click="popup">
            <div class="title">{{address ? "省市区" : ""}}</div>
            <div class="inputItem">
                <input type="text" placeholder="省市区" readonly v-model="address" class="item">
                <img src="../../static/images/order/right.png" class="icon" alt="">
            </div>
        </div>
        <div class="addressItem">
            <div class="title">{{detail ? "详细地址" : ""}}</div>
            <div class="inputItem">
                <input type="text" placeholder="详细地址" v-model="detail" class="item">
            </div>
        </div>
        <div class="btn" @click="submit">
            确定
        </div>
        <van-popup v-model="show" position="bottom">
            <van-area :area-list="area" v-if="area" style="width: 100%;" :loading="!area" @cancel="cancelbtn"
                @confirm="determine" />
        </van-popup>

    </div>


</template>

<script>
    import {
        Area,
        Popup
    } from 'vant';

    export default {
        name: 'addAddress',
        data() {
            return {
                RegionList: [],
                area: "",
                province_list: {},
                city_list: {},
                county_list: {},
                userName: "",
                phone: "",
                address: "",
                detail: "",
                show: false,
            };
        },
        components: {
            "van-area": Area,
            "van-popup": Popup,
        },
        methods: {
            submit: function() {
                var that = this;
                if (that.userName == "") {
                    that.Common.showMsg("请输入姓名");
                    return false;
                } else if (that.phone == "") {
                    that.Common.showMsg("请输入手机号码");
                    return false;
                } else if (that.address == "") {
                    that.Common.showMsg("清选择省市区");
                    return false;
                } else if (that.detail == "") {
                    that.Common.showMsg("请输入详细地址");
                    return false;
                }
                that.addAddress();
            },
            getMobileRegionList: function(SaId, index) {
                var that = this;
                //请求数据
                var requestData = {};
                that.postData("/Tool/NewRegionList", requestData).then(function(data) {

                    if (data.data.Code == "100") {
                        that.area = data.data.Data;

                        console.log(that.area)
                    } else {

                    }
                });

            },
            cancelbtn() {
                this.show = false;
            },
            popup() {
                this.show = true;
            },
            determine(val) {
                var that = this;
                console.log(val)
                that.show = false;
                that.address = val[0].name + "/" + val[1].name + "/" + val[2].name;
                that.RegionId = val[2].RegionId;
                that.ProvinceName = val[0].name;
                that.CityName = val[1].name;
                that.RName = val[2].name;
            },
            //添加收货地址
            addAddress: function() {
                var that = this;
                //请求数据
                var requestData = {
                    RegionId: that.RegionId,
                    Consignee: that.userName,
                    Address: that.detail,
                    Mobile: that.phone,
                    Flag: [{
                        Flag1: 0,
                        Flag2: 0,
                        Flag3: 0
                    }],
                    IsDefault: 1
                };
                that.postData("/ShipAddress/AddShipAddress", requestData).then(function(data) {
                    if (data.data.Code == "100") {
                        that.Common.showMsg("添加成功", function() {
                            that.$router.go(-1)
                        })
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }
                });
            },
        },
        mounted() {
            var that = this;

        },
        created() {
            var that = this;
            that.getMobileRegionList();
        },
    }
</script>
<style scoped src="../../static/css/addAddress.css">

</style>