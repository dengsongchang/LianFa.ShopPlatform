var express = require('express');
var router = express.Router();

//分销员管理
router.get('/application', function (req, res, next) {
    res.render('application/application.html',{
        currentUrl :"/application/application"
    });
});
//分销海报设置
router.get('/posters', function (req, res, next) {
    res.render('application/posters.html',{
        currentUrl :"/application/posters"
    });
});

//熊粉分销规则设置
router.get('/applicationForFans', function (req, res, next) {
    res.render('application/applicationForFans.html',{
        currentUrl :"/application/applicationForFans"
    });
});
//熊后分销规则设置
router.get('/applicationForQueen', function (req, res, next) {
    res.render('application/applicationForQueen.html',{
        currentUrl :"/application/applicationForQueen"
    });
});
//熊王分销规则设置
router.get('/applicationForKing', function (req, res, next) {
    res.render('application/applicationForKing.html',{
        currentUrl :"/application/applicationForKing"
    });
});

//礼包设置
router.get('/applicationForGiftSetting', function (req, res, next) {
    res.render('application/applicationForGiftSetting.html',{
        currentUrl :"/application/applicationForGiftSetting"
    });
});

//礼包添加
router.get('/applicationForAddGiftSetting', function (req, res, next) {
    res.render('application/applicationForAddGiftSetting.html',{
        currentUrl :"/application/applicationForAddGiftSetting"
    });
});

//礼包编辑
router.get('/applicationForEditGiftSetting', function (req, res, next) {
    res.render('application/applicationForEditGiftSetting.html',{
        currentUrl :"/application/applicationForEditGiftSetting"
    });
});


module.exports = router;