var express = require('express');
var router = express.Router();

/* 渲染门店列表 */
router.get('/storeList', function (req, res, next) {
    res.render('store/storeList.html',{
        currentUrl :"/store/storeList",
        IsStoreManager:req.session.isStoreManager

    });
});


router.get('/storeAdd', function (req, res, next) {
    res.render('store/storeAdd.html',{
        currentUrl :"/store/storeAdd",
        IsStoreManager:req.session.isStoreManager
    });
});
/* 渲染门店编辑页面 */
router.get('/storeEdit', function (req, res, next) {
    res.render('store/storeEdit.html',{
        currentUrl :"/store/storeEdit",
        IsStoreManager:req.session.isStoreManager
    });
});


//渲染店长列表
router.get('/managerList', function (req, res, next) {
    res.render('store/managerList.html',{
        currentUrl : "/store/managerList",
    });
});

//渲染编辑店长信息
router.get('/editMemberInfo', function (req, res, next) {
    res.render('store/editMemberInfo.html',{
        currentUrl : "/store/editMemberInfo",
    });
});
//渲染新增店长信息
router.get('/addMemberInfo', function (req, res, next) {
    res.render('store/addMemberInfo.html',{
        currentUrl : "/store/addMemberInfo",
    });
});

/* 渲染推荐画廊配置页 */
router.get('/storeRecommend', function (req, res, next) {
    res.render('store/storeRecommend.html',{
        currentUrl :"/store/storeRecommend"
    });
});

/* 渲染门店审核页 */
router.get('/storeAduitList', function (req, res, next) {
    res.render('store/storeAduitList.html',{
        currentUrl :"/store/storeAduitList"
    });
});

/* 渲染提现列表页面 */
router.get('/withdrawalList', function (req, res, next) {
    res.render('store/orderwithdrawalList.html',{
        currentUrl : "/store/withdrawalList"
    });
});

/* 渲染门店入驻页面 */
router.get('/storeService', function (req, res, next) {
    res.render('store/storeService.html',{
        currentUrl : "/store/storeService"
    });
});
/* 渲染推荐门店收益列表 */
router.get('/storeEarnings', function (req, res, next) {
    res.render('store/storeEarnings.html',{
        currentUrl :"/store/storeEarnings"
    });
});

/* 渲染门店后台入口页面 */
router.get('/storeAgreement', function (req, res, next) {
    res.render('store/storeAgreement.html',{
        currentUrl : "/store/storeAgreement"
    });
});

module.exports = router;