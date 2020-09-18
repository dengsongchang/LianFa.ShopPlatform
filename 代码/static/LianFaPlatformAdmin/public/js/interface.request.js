var InterfaceRequest = {
    //测试地址
    urlPrefixNoApi: "http://192.168.31.186:8077/",
    urlPrefix: "http://192.168.31.186:8077/api/",
    //异步请求
    set: function (method, data, callback, errorCallback, beforeSendCallBack) {
        var jsonData = JSON.stringify(data);
        $.ajax({
            url: this.urlPrefix + method,
            type: "post",
            dataType: 'json',
            contentType: "application/json ; charset=utf-8",
            data: jsonData,
            //必须有这项的配置，不然cookie无法发送至服务端
            xhrFields: {
                withCredentials: true
            },
            beforeSend: function () {
                if ($.isFunction(beforeSendCallBack)) {
                    beforeSendCallBack();
                }
            },
            success: function (data) {
                if ($.isFunction(callback)) {
                    callback(data);
                }
            },
            error: function () {
                if ($.isFunction(errorCallback)) {
                    errorCallback();
                }
                else {
                    // Common.showErrorMsg("网络异常，请稍后重试");
                }
            },
            // 当响应对应的状态码时，执行对应的回调函数
            statusCode: {
                //未登录，跳转到会员登录页
                401: function (data) {
                    window.location.href = "/account/login";
                }
            }
        });
    },
    //同步请求
    SetSync: function (method, data, callback, errorCallback, beforeSendCallBack) {
        var jsonData = JSON.stringify(data);
        $.ajax({
            url: this.urlPrefix + method,
            type: "post",
            dataType: 'json',
            contentType: "application/json ; charset=utf-8",
            data: jsonData,
            //同步
            async: false,
            //必须有这项的配置，不然cookie无法发送至服务端
            xhrFields: {
                withCredentials: true
            },
            beforeSend: function () {
                if ($.isFunction(beforeSendCallBack)) {
                    beforeSendCallBack();
                }
            },
            success: function (data) {
                if ($.isFunction(callback)) {
                    callback(data);
                }
            },
            error: function () {
                if ($.isFunction(errorCallback)) {
                    errorCallback(data);
                } else {
                    Common.showErrorMsg("网络异常，请稍后重试");
                }
            },
            // 当响应对应的状态码时，执行对应的回调函数
            statusCode: {
                //未登录，跳转到会员登录页
                401: function (data) {
                    window.location.href = "/account/login";
                }
            }
        });
    }
}