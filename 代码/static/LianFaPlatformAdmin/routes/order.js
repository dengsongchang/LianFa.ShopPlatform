
var express = require('express');
var router = express.Router();

/* 所有订单列表页 */
router.get('/orderList', function (req, res, next) {
    res.render('order/orderList.html',{
        currentUrl :"/order/orderList"
    });
});

/* 订单详情页 */
router.get('/orderListDetail', function (req, res, next) {
    res.render('order/orderListDetail.html',{
        currentUrl :"/order/orderListDetail"
    });
});
/* 渲染订单设置页面 */
router.get('/orderSettings', function (req, res, next) {
    res.render('order/order_settings.html',{
        currentUrl : "/order/orderSettings"
    });
});

router.get('/orderAdministration', function (req, res, next) {
    res.render('order/order_administration.html',{
        currentUrl :"/order/orderAdministration"
    });
});

router.get('/orderService', function (req, res, next) {
    res.render('order/order_service.html',{
        currentUrl :"/order/orderService"
    });
});
router.get('/orderServiceChange', function (req, res, next) {
    res.render('order/order_service_change.html',{
        currentUrl :"/order/orderService"
    });
});
router.get('/orderServiceDetail', function (req, res, next) {
    res.render('order/orderServiceDetail.html',{
        currentUrl :"/order/orderServiceDetail"
    });
});


/* 渲染退款申请单页面 */
router.get('/refundApplication', function (req, res, next) {
    res.render('order/refund_application.html',{
        currentUrl : "/order/refundApplication"
    });
});
/* 渲染退款中页面 */
router.get('/refundApplicating', function (req, res, next) {
    res.render('order/refund_applicating.html',{
        currentUrl : "/order/refundApplication"
    });
});
/* 渲染退款失败页面 */
router.get('/refundErrorApplication', function (req, res, next) {
    res.render('order/refund_Errorapplication.html',{
        currentUrl : "/order/refundApplication"
    });
});
/* 渲染已退款页面 */
router.get('/refundHasApplication', function (req, res, next) {
    res.render('order/refund_Hasapplication.html',{
        currentUrl : "/order/refundApplication"
    });
});

/* 渲染退款申请单详情页面 */
router.get('/appicationDetail', function (req, res, next) {
    res.render('order/refundApplication_detail.html',{
        currentUrl : "/order/refundApplication"
    });
});

/* 渲染物流信息页面 */
router.get('/logistics', function (req, res, next) {
    res.render('order/logistics_detail.html',{
        currentUrl : "/order/orderList"
    });
});

/* 渲染退款详情页面 */
router.get('/orderServiceDetail', function (req, res, next) {
    res.render('order/orderServiceDetail.html',{
        currentUrl : "/order/orderServiceDetail"
    });
});
/* 渲染提现列表页面 */
router.get('/withdrawalList', function (req, res, next) {
    res.render('order/withdrawalList.html',{
        currentUrl : "/order/withdrawalList"
    });
});

/*兑换订单列表*/
router.get('/exchangeOrderList', function (req, res, next) {
    res.render('order/exchangeOrderList.html',{
        currentUrl : "/order/exchangeOrderList"
    });
});

/*兑换订单详情*/
router.get('/exchangeOrderDetail', function (req, res, next) {
    res.render('order/exchangeOrderDetail.html',{
        currentUrl : "/order/exchangeOrderDetail"
    });
});

module.exports = router;