import { Dialog } from 'vant';
//公共帮助
var Common = {
    showMsg: function(msg, callback, title) {

        if (title == null || title.length == 0) {
            title = "提示";
        }
        Dialog.alert({
            title: title,
            message: msg
        }).then(() => {
            typeof callback === "function" ? callback() : false;

        });

    },
    confirmDialog: function(msg, comfirmCallback, cancelCallback, title) {
        if (title == null || title.length == 0) {
            title = "提示";
        }
        Dialog.confirm({
            title: title,
            message: msg
        }).then(() => {
            typeof comfirmCallback === "function" ? comfirmCallback() : false;

        }).catch(() => {
            typeof cancelCallback === "function" ? cancelCallback() : false;
        });
    },


}
export default Common