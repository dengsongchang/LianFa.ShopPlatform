var express = require('express');
var router = express.Router();

/* 支付设置 */
router.get('/payAboutSetting', function (req, res, next) {
    res.render('payAndSend/paySetting.html',{
        currentUrl :"/payAbout/payAboutSetting"
    });
});

/* 物流公司 */
router.get('/logisticCompany', function (req, res, next) {
    res.render('payAndSend/logisticCompany.html',{
        currentUrl :"/payAbout/logisticCompany"
    });
});

/* 区域管理 */
router.get('/areaManage', function (req, res, next) {
    res.render('payAndSend/areaManage.html',{
        currentUrl :"/payAbout/areaManage"
    });
});

module.exports = router;