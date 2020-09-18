<!-- 登录 -->
<template>
    <form @submit.prevent="formSubmit" style="overflow: hidden;">
        <div class="title">
            <div class="Landing">登录</div>
            <div class="Landing_name">SIGN IN</div>
        </div>
        <div class="inputBox">

            <input v-model="phone" value="" clearable type="number" placeholder="请输入手机号" style="width:70%">
        </div>
        <div class="inputBox">
            <span @click="sendCode" :class="{ 'textGrey' : isGrey }" class="codetxt">{{countShow}}</span>
            <!-- <span :class="{ 'textGrey' : isGrey }" class="codetxt" v-else-if="issendcode">{{countShow}}</span> -->
            <input v-model="code" type="number" placeholder="请输入验证码">
        </div>

        <van-button size="large" class="loginBtn" type="submit">登录</van-button>
        <div class="login_btn" @click="getregistered()">注册</div>
    </form>
</template>
<script>
    import {
        Button,
        Toast
    } from "vant";
    export default {
        name: "login",
        components: {
            "van-button": Button
        },
        data() {
            return {
                phone: "",

                code: "",
                count: "",
                countShow: "获取验证码",
                timer: null,
                isGrey: false,
                issendcode: false
            };
        },
        methods: {
            formSubmit() {
                var that = this;
                // var myreg = 11 && /^((13|14|15|17|18)[0-9]{1}\d{8})$/;
                // if (that.phone == "") {
                //     that.Common.showMsg("手机号不能为空！");
                //     return false;
                // }
                // if (!myreg.test(that.phone)) {
                //     that.Common.showMsg("手机号格式错误，请重新输入！");
                //     return false;
                // }
                if (that.code == "") {
                    that.Common.showMsg("请输入验证码");
                    return false;
                }
                that.mobileLogin();

            },
            getregistered: function() {
                var that = this;
                that.$router.push({
                    path: "/registered"
                });
            },
            mobileLogin: function() {
                var that = this;
                //请求方法
                var methodName = "/Account/Login";
                //请求数据
                var reqData = {
                    Mobile: that.phone,
                    Code: that.code
                };


                that.postData(methodName, reqData).then(function(res) {
                    if (res.data.Code == "100") {
                        localStorage.setItem("token", res.data.Data.Token);
                        var beforeLoginUrl = localStorage.beforeLoginUrl;
                        if (beforeLoginUrl) {
                            location.href = beforeLoginUrl;
                            localStorage.removeItem("beforeLoginUrl");
                        } else {
                            that.Common.showMsg(res.data.Message, function() {
                                location.href = beforeLoginUrl;
                                that.$router.push("/");
                            });
                        }
                    } else {
                        that.Common.showMsg(res.data.Message, function() {
                            that.$router.push("/giftExchange");
                        });
                    }
                });
            },
            sendCode: function() {
                var that = this;
                var myreg = 11 && /^((13|14|15|17|18)[0-9]{1}\d{8})$/;
                if (that.phone == "") {
                    that.Common.showMsg("手机号不能为空！");
                    return false;
                }
                if (!myreg.test(that.phone)) {
                    that.Common.showMsg("手机号格式错误，请重新输入！");
                    return false;
                }
                //请求方法
                var methodName = "/Account/SendMobileVerifyCode";
                //请求数据
                var reqData = {
                    Mobile: that.phone,
                    Type: 1
                };

                that.postData(methodName, reqData).then(function(res) {

                    if (res.data.Code == "100") {
                        that.issendcode = true;
                        that.getCode();
                    } else {
                        that.Common.showMsg(res.data.Message, function() {
                            that.$router.push("/registered");
                        });
                    }
                });

            },
            //验证码倒计时
            getCode() {
                if (this.isGrey === false) {
                    this.isGrey = true;
                    //验证码倒计时60s
                    var TIME_COUNT = 60;
                    if (!this.timer) {
                        this.count = TIME_COUNT;
                        this.timer = setInterval(() => {
                            if (this.count > 0 && this.count <= TIME_COUNT) {
                                this.count--;
                                this.countShow = this.count + "s";
                            } else {
                                clearInterval(this.timer);
                                this.timer = null;
                                this.count = "";
                                this.countShow = "获取验证码";
                                this.isGrey = false;
                            }
                        }, 1000);
                    }
                }
            },

        }
    };
</script>
<style scoped src="../../static/css/login.css"></style>