<template>
  <div class="form">
    <div class="inputBox">
      <img src="../../static/images/login/phone.png">
      <input v-model="phone" clearable type="number" placeholder="请输入手机号">
    </div>
    <div class="inputBox">
      <img src="../../static/images/login/pwd.png">
      <input v-model="pwd" clearable type="password" placeholder="请输入密码">
    </div>
    <div class="inputBox">
      <img src="../../static/images/login/pwd.png">
      <input v-model="cpwd" clearable type="password" placeholder="请输入确认密码">
    </div>
    <div class="inputBox">
      <img src="../../static/images/login/code.png">
      <span v-show="sendAuthCode" class="codetxt" @click="getAuthCode">获取验证码</span>
      <span v-show="!sendAuthCode" class="codetxt">{{time}}s后获取</span>
      <input v-model="code" type="number" placeholder="请输入验证码">
    </div>
    <van-button size="large" class="loginBtn" type="submit" @click="formSubmit">注册</van-button>
  </div>
</template>
<script>
import { Button, Toast } from "vant";
export default {
  name: "login",
  components: {
    "van-button": Button
  },
  data() {
    return {
      phone: "",
      pwd: "",
      cpwd: "",
      code: "",
      sendAuthCode: true,
      time: 0
    };
  },
  methods: {
    formSubmit() {
      var that = this;
      var myreg = 11 && /^((13|14|15|17|18)[0-9]{1}\d{8})$/;
      if (that.phone == "") {
        that.Common.showMsg("手机号不能为空！");
      } else if (!myreg.test(that.phone)) {
        that.Common.showMsg("手机号格式错误，请重新输入！");
      }
    },
    getAuthCode() {
      var that = this;
      var myreg = 11 && /^((13|14|15|17|18)[0-9]{1}\d{8})$/;
      if (that.phone == "") {
        that.Common.showMsg("手机号不能为空！");
      } else if (!myreg.test(that.phone)) {
        that.Common.showMsg("手机号格式错误，请重新输入！");
      } else {
        that.sendAuthCode = false;
        that.time = 60;
        var auth_time = setInterval(() => {
          that.time--;
          if (that.time <= 0) {
            that.sendAuthCode = true;
            clearInterval(auth_time);
          }
        }, 1000);
      }
    }
    // sendCode() {
    //   var myreg = 11 && /^((13|14|15|17|18)[0-9]{1}\d{8})$/;
    //   if (this.phone == "") {
    //     Toast("手机号不能为空！");
    //   } else if (!myreg.test(this.phone)) {
    //     Toast("手机号格式错误，请重新输入！");
    //   } else {
    //     this.disabled = true;
    //     //验证码的时间设置 可有可无
    //     setTimeout(this.timer, 1000);
    //手机号争取后调取接口
    // this.$axios
    //   .post("/sms/editpass", {})
    //   .then(res => {
    //     if (res.status != 200) {
    //       this.$toast("网络错误");
    //     } else if (res.status == 200) {
    //       if (res.data.error == null && res.data.result == true) {
    //         this.$toast("请注意接收验证码");
    //       } else if (res.data.error != null && res.data.result == null) {
    //         this.$toast(res.data.error.msg);
    //       }
    //     }
    //   })
    //   .catch(function(err) {
    //     console.log(err);
    //   });
  }
};
</script>
<style scoped src="../../static/css/login.css"></style>

