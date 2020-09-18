<!-- 礼品查询 -->
<template>

    <div class="content" style="overflow: hidden;">
        <div class="personal_name">
            <span class="nmae">姓名</span>
            <input v-model="NickName" style="width:70%" class="name_span">
        </div>
        <div class="personal_name">
            <span class="nmae">手机号码</span>
            <input v-model="Mobile" clearable type="number" style="width:70%" class="name_span">
        </div>
        <div class="personal_Avatar">
            <div class="Avatar_name">头像</div>
            <div class="Avatar_img">
                <!-- <van-uploader :after-read="onRead" accept="image/*" multiple>
                    <img class="head-img" :src="url + AvatarStr" />

                </van-uploader> -->
                <van-uploader class="img" :after-read="onRead" accept="image/*">

                    <img src="../../static/images/user/Center.png" alt="" v-if="Avatar == ''"  id="img">
                    <img :src="Avatar" id="img" v-else>
                </van-uploader>
            </div>
        </div>
        <div class="address">
            <div class="address_left">地址管理</div>
            <div class="address_right" @click="getaddAddress()">新增地址</div>
        </div>

        <div class="address_box" v-for="(item,index) in ShipAddressList" :key="index">
            <div class="address_top">
                <div class="area">{{item.Region}}</div>
                <div class="detailed">{{item.Address}}</div>
            </div>
            <div class="address_icon">
                <img src="../../static/images/user/delete.png" alt="" class="delete" @click="delShipAddress(item.SaId)">
                <img src="../../static/images/user/modify.png" alt="" class="modify" @click="geteditaddress(item.SaId)">
            </div>
        </div>

        <div size="large" class="addbtn" @click="submit">确定</div>



    </div>

</template>

<script>
    import {
        Button,
        Uploader,
    } from "vant";
    export default {
        name: 'giftcard',
        data() {
            return {
                NickName: "",
                Mobile: "",
                Avatar: "",
                uploadImg: "",
                ShipAddressList: [],
                // url: "http://192.168.31.141:8955"

            }
        },
        computed: {},
        components: {
            "van-uploader": Uploader
        },
        methods: {
            //个人信息
            getUCenterData: function() {
                var that = this;
                var data = {};
                that.postData("/UCenter/UCenterData", data).then(function(data) {
                    console.log(data.data);
                    if (data.data.Code == "100") {
                        that.NickName = data.data.Data.Info.NickName;
                        that.Mobile = data.data.Data.Info.Mobile;
                        that.uploadImg = data.data.Data.Info.Avatar;
                        that.Avatar = data.data.Data.Info.AvatarStr;
                        that.UId = data.data.Data.Info.UId;
                        localStorage.setItem("UId", data.data.Data.UId);
                        that.ShipAddressList = data.data.Data.AddressList;
                    }
                });
            },
            // 确认编辑
            submit: function() {
                var that = this;
                var myreg = 11 && /^((13|14|15|17|18)[0-9]{1}\d{8})$/;
                if (that.Mobile == "") {
                    that.Common.showMsg("手机号不能为空！");
                } else if (!myreg.test(that.Mobile)) {
                    that.Common.showMsg("手机号格式错误，请重新输入！");

                } else if (that.NickName == "") {
                    that.Common.showMsg("请输入姓名");
                } else if (that.Avatar == "") {
                    that.Common.showMsg("请上传头像");
                } else {
                    //请求数据
                    var requestData = {
                        Mobile: that.Mobile,
                        NickName: that.NickName,
                        Avatar: that.uploadImg,
                    };
                    that.postData("/UCenter/EditUser", requestData).then(function(data) {
                        if (data.data.Code == "100") {
                            that.Common.showMsg("编辑成功！", function() {
                                that.$router.push("/user");
                            });
                        } else {
                            that.Common.showMsg(data.data.Message);
                        }
                    });
                }
            },
            // 删除收货地址
            delShipAddress: function(SAId) {
                var that = this;
                var data = {
                    "SaId": SAId
                };
                that.postData("/ShipAddress/DelShipAddress", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.Common.showMsg("删除成功！", function() {
                            that.getUCenterData();
                        });
                    }
                });
            },

            getaddAddress: function() {
                var that = this;
                that.$router.push({
                    path: "/addAddress",

                });
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

            onRead(file) {
                console.log(file);
                var that = this;
                var formData = new FormData();
                formData.append("file", file.file);

                that.postData("/UCenter/UploadUsersImg", formData).then(function(data) {
                    if (data.data.Code == "100") {
                        that.uploadImg = data.data.Data;
                        that.Avatar = file.content;
                    } else {
                        that.Common.showMsg(data.data.Message);
                    }

                });
            }
        },
        mounted() {
            var that = this;
            that.getUCenterData();

        },
        created() {


        },

    }
</script>
<style scoped src="../../static/css/personaldetails.css">

</style>