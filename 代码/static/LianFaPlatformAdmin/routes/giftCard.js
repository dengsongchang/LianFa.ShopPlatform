var express = require('express');
var router = express.Router();

/* 礼品卡发布页 */
router.get('/giftCardRelease', function (req, res, next) {
    res.render('giftCard/giftCardRelease.html',{
        currentUrl :"/giftCard/giftCardRelease"
    });
});

/* 礼品卡列表页 */
router.get('/giftCardList', function (req, res, next) {
    res.render('giftCard/giftCardList.html',{
        currentUrl :"/giftCard/giftCardList"
    });
});

/* 礼品卡详情页 */
router.get('/giftCardDetail', function (req, res, next) {
    res.render('giftCard/giftCardDetail.html',{
        currentUrl :"/giftCard/giftCardDetail"
    });
});

/* 礼包类型发布 */
router.get('/giftTypeRelease', function (req, res, next) {
    res.render('giftCard/giftTypeRelease.html',{
        currentUrl :"/giftCard/giftTypeRelease"
    });
});

/* 渲染礼品卡类型列表页 */
router.get('/productGiftType', function (req, res, next) {
    res.render('giftCard/productGiftType.html',{
        currentUrl :"/giftCard/productGiftType"
    });
});


module.exports = router;