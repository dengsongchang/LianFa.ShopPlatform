var express = require('express');
var router = express.Router();

/* 渲染营销页 */
router.get('/', function (req, res, next) {
    res.render('marketMode/market_common.html',{
        currentUrl :"/marketMode/market",
    });
});

/* 渲染满减优惠列表页 */
router.get('/marketQuota', function (req, res, next) {
    res.render('marketMode/market_quota.html',{
        currentUrl :"/marketMode/marketQuota",
    });
});

/* 渲染满减添加页 */
router.get('/marketQuotaNew', function (req, res, next) {
    res.render('marketMode/market_quotaNew.html',{
        currentUrl :"/marketMode/marketQuotaNew",
    });
});

/* 渲染满减编辑页 */
router.get('/marketQuotaEdit', function (req, res, next) {
    res.render('marketMode/market_quotaEdit.html',{
        currentUrl :"/marketMode/marketQuotaEdit",
    });
});

/* 渲染满减优惠页 */
router.get('/marketGrouppurchase', function (req, res, next) {
    res.render('marketMode/market_grouppurchase.html',{
        currentUrl :"/marketMode/market",
    });
});
/* 渲染团购编辑页 */
router.get('/AddmarketGrouppurchase', function (req, res, next) {
    res.render('marketMode/market_Addgrouppurchase.html',{
        currentUrl :"/marketMode/market",
    });
});
/* 渲染团购编辑页 */
router.get('/marketEditGrouppurchase', function (req, res, next) {
    res.render('marketMode/market_Editgrouppurchase.html',{
        currentUrl :"/marketMode/market",
    });
});
/* 限时抢购 正在进行 */
router.get('/limitSaleIng', function (req, res, next) {
    res.render('marketMode/limitSaleIng.html',{
        currentUrl :"/marketMode/limitSaleIng"
    });
});
/* 限时抢购 即将开始 */
router.get('/limitSaleWillStar', function (req, res, next) {
    res.render('marketMode/limitSaleWillStar.html',{
        currentUrl :"/marketMode/limitSaleWillStar"
    });
});
/* 限时抢购 历史抢购 */
router.get('/limitSaleHistory', function (req, res, next) {
    res.render('marketMode/limitSaleHistory.html',{
        currentUrl :"/marketMode/limitSaleHistory"
    });
});
/* 限时抢购 添加*/
router.get('/limitSaleAdd', function (req, res, next) {
    res.render('marketMode/limitSaleAdd.html',{
        currentUrl :"/marketMode/limitSaleAdd"
    });
});
/* 限时抢购 编辑*/
router.get('/limitSaleEdit', function (req, res, next) {
    res.render('marketMode/limitSaleEdit.html',{
        currentUrl :"/marketMode/limitSaleEdit"
    });
});
/* 限时抢购 详情页 */
router.get('/limitSaleDetail', function (req, res, next) {
    res.render('marketMode/limitSaleDetail.html',{
        currentUrl :"/marketMode/limitSaleDetail"
    });
});
/* 优惠券管理与添加 页 */
router.get('/couponManage', function (req, res, next) {
    res.render('marketMode/couponManage.html',{
        currentUrl :"/marketMode/couponManage"
    });
});
/* 渲染优惠券页 */
router.get('/marketCoupon', function (req, res, next) {
    res.render('marketMode/marketing_way.html',{
        currentUrl :"/marketMode/marketCoupon",
    });
});
//活动规则
router.get('/activityRules', function (req, res, next) {
    res.render('marketMode/activityRules.html',{
        currentUrl :"/marketMode/activityRules",
    });
});

/* 渲染活动详情页 */
router.get('/marketing_detail_list', function (req, res, next) {
    res.render('marketMode/marketing_detail_list.html',{
        currentUrl : "/marketMode/marketing_detail_list"
    });
});

/* 单品促销列表*/
router.get('/promotionList', function (req, res, next) {
    res.render('marketMode/promotionList.html',{
        currentUrl :"/marketMode/promotionList"
    });
});
/* 单品促销添加*/
router.get('/promotionAdd', function (req, res, next) {
    res.render('marketMode/promotionAdd.html',{
        currentUrl :"/marketMode/promotionAdd"
    });
});
/* 单品促销编辑 */
router.get('/promotionEdit', function (req, res, next) {
    res.render('marketMode/promotionEdit.html',{
        currentUrl :"/marketMode/promotionEdit"
    });
});
//分销员管理
router.get('/application', function (req, res, next) {
    res.render('marketMode/application.html',{
        currentUrl :"/marketMode/application"
    });
});
//分销海报设置
router.get('/posters', function (req, res, next) {
    res.render('marketMode/posters.html',{
        currentUrl :"/marketMode/posters"
    });
});

//秒杀专场
/* 秒杀专场 正在进行 */
router.get('/limitSalePerformanceIng', function (req, res, next) {
    res.render('marketMode/limitSalePerformanceIng.html',{
        currentUrl :"/marketMode/limitSalePerformanceIng"
    });
});
/* 秒杀专场 即将开始 */
router.get('/limitSalePerformanceWillStar', function (req, res, next) {
    res.render('marketMode/limitSalePerformanceWillStar.html',{
        currentUrl :"/marketMode/limitSalePerformanceWillStar"
    });
});
/* 秒杀专场 历史抢购 */
router.get('/limitSalePerformanceHistory', function (req, res, next) {
    res.render('marketMode/limitSalePerformanceHistory.html',{
        currentUrl :"/marketMode/limitSalePerformanceHistory"
    });
});
/* 秒杀专场 添加*/
router.get('/limitSalePerformanceAdd', function (req, res, next) {
    res.render('marketMode/limitSalePerformanceAdd.html',{
        currentUrl :"/marketMode/limitSalePerformanceAdd"
    });
});
/* 秒杀专场 编辑*/
router.get('/limitSalePerformanceEdit', function (req, res, next) {
    res.render('marketMode/limitSalePerformanceEdit.html',{
        currentUrl :"/marketMode/limitSalePerformanceEdit"
    });
});
/* 秒杀专场 详情页 */
router.get('/limitSalePerformanceDetail', function (req, res, next) {
    res.render('marketMode/limitSalePerformanceDetail.html',{
        currentUrl :"/marketMode/limitSalePerformanceDetail"
    });
});

//品牌专场
/* 品牌专场列表 */
router.get('/specialBrandList', function (req, res, next) {
    res.render('marketMode/specialBrandList.html',{
        currentUrl :"/marketMode/specialBrandList"
    });
});
/* 品牌专场添加*/
router.get('/specialBrandAdd', function (req, res, next) {
    res.render('marketMode/specialBrandAdd.html',{
        currentUrl :"/marketMode/specialBrandAdd"
    });
});
/* 品牌专场编辑*/
router.get('/specialBrandEdit', function (req, res, next) {
    res.render('marketMode/specialBrandEdit.html',{
        currentUrl :"/marketMode/specialBrandEdit"
    });
});

//明星专场
/* 明星专场列表 */
router.get('/starVenueList', function (req, res, next) {
    res.render('marketMode/starVenueList.html',{
        currentUrl :"/marketMode/starVenueList"
    });
});
/* 明星专场添加*/
router.get('/starVenueAdd', function (req, res, next) {
    res.render('marketMode/starVenueAdd.html',{
        currentUrl :"/marketMode/starVenueAdd"
    });
});
/* 明星专场编辑*/
router.get('/starVenueEdit', function (req, res, next) {
    res.render('marketMode/starVenueEdit.html',{
        currentUrl :"/marketMode/starVenueEdit"
    });
});

module.exports = router;