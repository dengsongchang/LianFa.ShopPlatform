import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import 'amfe-flexible'
Vue.config.productionTip = false
import { Dialog } from 'vant';
Vue.use(Dialog);
//引入静态资源
import axios from 'axios'
import Common from '../static/js/common.js'
//全局配置Common方法
Vue.prototype.Common = Common;

// axios.defaults.withCredentials = true;
var urlPrefix = "http://www.szlfh.com/api/admin"; //正式api地址
var urlPrefixNoAdmin = "http://www.szlfh.com/api"; //正式api地址
var urlPrefixNoApi = "http://www.szlfh.com"; //正式api地址


// var urlPrefix = "http://192.168.31.141:8955/api/admin"; //正式api地址
// var urlPrefixNoAdmin = "http://192.168.31.141:8955/api"; //正式api地址
// var urlPrefixNoApi = "http://192.168.31.141:8955"; //正式api地址
import global_ from './components/Global.vue' //引用文件
Vue.prototype.GLOBAL = global_ //挂载到Vue实例上面

import md5 from 'js-md5'

var SignRequest = {
    signkey: "SIGNKEY=lf@vSwcrpoKJ4IxdxtI",
    token: "TOKEN=",
    timestamp: "TIMESTAMP=",
    //获取服务器时间
    severtime: function() {
        var dateTime;
        $.ajax({
            url: urlPrefixNoAdmin + '/Common/GetServerTime',
            type: "post",
            dataType: 'json',
            async: false,
            success: function(data) {
                dateTime = data;
            },
            error: function() {
                Common.showMsg("提示", "无法获取服务器时间!");
            }
        });
        return dateTime;
    },
    //获取方法名
    methodname: function(method) {
        return "METHODNAME=" + method;
    },
    //获取Token
    getToken: function() {
        var gettoken = localStorage.getItem("token");
        return gettoken;
    },
    //设置Token
    setToken: function(token) {
        localStorage.setItem('token', token);
    },
    //MD5加密
    tokenmd5string2: function(method, dateTime) {
        var gettoken = this.getToken();

        //token为空不参与加密
        if (gettoken == "" || gettoken == null) {
            return this.signkey + ";" + this.timestamp + dateTime + ";" + this.methodname(method) + ";";
        }

        return this.signkey + ";" + this.timestamp + dateTime + ";" + this.methodname(method) + ";" + this.token + gettoken + ";";
    },
    //设置json数据
    getSign: function(method, dateTime) {
        var sMd5 = this.tokenmd5string2(method, dateTime);
        var iMd5 = md5(sMd5).toUpperCase();
        return iMd5;
    }
};

Vue.prototype.postData = function(url, data) {
    //获取当前时间戳
    var that = this;
    var timeStamp = Date.parse(new Date()) / 1000;

    //获取签名
    var sign = SignRequest.getSign(url, timeStamp);

    //获取token
    var token = SignRequest.getToken();
    return new Promise((resolve, reject) => {
        axios({
            method: 'post',
            url: urlPrefixNoAdmin + url,
            dataType: 'json',
            data: data,
            headers: {
                "TimeStamp": timeStamp,
                "Sign": sign,
                "MethodName": url,
                "Token": token,
                "Content-Type": "application/json ; charset=utf-8"
            },
        }).then((response) => {

            resolve(response)

        }, (response) => {
            console.log(response.response);
            if (response.response.status == 401) {
                localStorage.removeItem("token");
                localStorage.removeItem("beforeLoginUrl");
                localStorage.beforeLoginUrl = window.location.href;
                that.Common.showMsg('请登录', function() {
                    //跳转登录页面
                    router.push('/login');
                })
            }
            reject(response)
        })
    })
}
router.beforeEach((to, form, next) => {
    window.document.title = to.meta.title == undefined ? 'LianFa' : to.meta.title
    next()
})

Vue.prototype.getData = function(url, data) {
    return new Promise((resolve, reject) => {
        axios({
            method: 'get',
            url: urlPrefixNoAdmin + url,
            data: qs.stringify(data)
        }).then((response) => {
            resolve(response)
        }, (response) => {
            reject(response)
        })
    })
}

// 登录后跳转方法
Vue.prototype.goBeforeLoginUrl = () => {
    let url = localStorage.beforeLoginUrl;
    if (!url || url.indexOf('/author') != -1) {
        url = urlPre;
    }
    console.log("登录前页面：", url);
    // router.push('url')
    localStorage.removeItem("beforeLoginUrl");
    window.location.href = url;
}

//页面缓存设置z
Vue.prototype.changeKeepAlive = (name, keepAlive) => {
    router.options.routes.map((item) => {
        if (item.name === name) {
            item.meta.keepAlive = keepAlive
        }
    })
}

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app')