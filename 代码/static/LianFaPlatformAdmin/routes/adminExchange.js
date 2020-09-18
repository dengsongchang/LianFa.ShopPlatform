var express = require('express');
var router = express.Router();

/* 管理员兑换管理列表 */
router.get('/adminExchangeList', function (req, res, next) {
    res.render('adminExchange/adminExchangeList.html',{
        currentUrl :"/adminExchange/adminExchangeList"
    });
});

/* 礼品卡详情页 */
router.get('/giftCardDetail', function (req, res, next) {
    res.render('adminExchange/giftCardDetail.html',{
        currentUrl :"/adminExchange/giftCardDetail"
    });
});


module.exports = router;