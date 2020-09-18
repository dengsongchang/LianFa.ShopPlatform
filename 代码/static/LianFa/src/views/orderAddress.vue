<template>
    <div class="allBox">
        <div class="outBox">
            <!--收货地址-->
            <div class="addressBox" v-for="item,index in shipAddressList" :key="this" @click="selectHandle(item.SAId)">
                <div class="addressLeft">
                    <div class="leftTop">
                        <img src="../../static/images/order/dingwei.png" class="addressIcon" alt="">
                        <div class="userName">{{item.Consignee}}</div>
                        <div class="phone">{{item.Mobile}}</div>
                    </div>
                    <div class="leftBottom">{{item.ProvinceName}}{{item.CityName}}{{item.RName}}{{item.Address}}</div>
                    <div class="actionBox">
                        <img src="../../static/images/address/noSelect.png" v-show="!item.IsDefault" class="defaultIcon"
                             @click.stop="setDefaultHandle(item.SAId)" alt="">
                        <img src="../../static/images/address/select.png" v-show="item.IsDefault" class="defaultIcon"
                             @click.stop="setDefaultHandle(item.SAId)" alt="">
                        <div class="defaultText">默认地址</div>
                        <div class="delectText" @click.stop="delShipAddress(item.SAId)">删除</div>
                        <img src="../../static/images/address/delect.png" class="delectIcon" alt="" @click.stop="delShipAddress(item.SAId)">
                        <div class="editText" @click.stop="toEdit(item.SAId)">修改</div>
                        <img src="../../static/images/address/edit.png" class="editIcon" alt=""
                             @click.stop="toEdit(item.SAId)">
                    </div>

                </div>
            </div>
            <!--按钮-->
            <div class="addBtn" @click.stop="toAdd">新增收货地址</div>
        </div>
    </div>


</template>

<script>
    import {mapState} from 'vuex'

    export default {
        name: 'orderAddress',
        data() {
            return {
                shipAddressList: [],
            }
        },
        computed: {

        },
        components: {},
        methods: {
            //收货地址列表
            shipAddressListHandle: function () {
                var that = this;
                var requstData = {}
                that.postData("/ShipAddress/ShipAddressList", requstData).then(function (data) {
                    if (data.data.Code == "100") {
                        that.shipAddressList = data.data.Data.ShipAddressList;
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }

                });
            },
            //设置默认地址
            setDefaultHandle: function (id) {
                var that = this;
                var requstData = {
                    "SaId": id
                }
                that.postData("/ShipAddress/SetDefaultShipAddress", requstData).then(function (data) {
                    if (data.data.Code == "100") {
                        that.shipAddressListHandle();
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }
                });
            },
            //删除收货地址
            delShipAddress: function (id) {
                var that = this;
                //获取本地缓存的id
                if(localStorage.SAId == id){
                    localStorage.removeItem('SAId')
                }
                var requstData = {
                    "SaId": id
                }
                that.postData("/ShipAddress/DelShipAddress", requstData).then(function (data) {
                    if (data.data.Code == "100") {
                        that.shipAddressListHandle();
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }
                });
            },
            //编辑收货地址
            toEdit: function (saId) {
                var that = this;
                that.$router.push({path: '/editAddress', query: {saId: saId}})
            },
            //前往添加收货地址
            toAdd:function(){
                var that = this;
                that.$router.push({path: '/addAddress'})
            },
            //选择地址
            selectHandle: function (id) {
                var that = this;
                //选择的时候将id存在本地缓存
                localStorage.setItem('SAId',id)
                that.$router.go(-1)
            },
        },
        mounted() {
            var that = this;
            that.shipAddressListHandle();
        },
        created() {
            var that = this;

        },
    }

</script>
<style scoped src="../../static/css/orderAddress.css">


</style>
