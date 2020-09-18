var express = require('express');
var router = express.Router();

/* 渲染会员列表页 */
router.get('/', function (req, res, next) {
    res.render('miniApp/editMiniApp.html',{
        currentUrl : "/miniApp/editMiniApp"
    });
});

/* 渲染消息模板页 */
router.get('/messTempalte', function (req, res, next) {
    res.render('miniApp/message_template.html',{
        currentUrl : "/miniApp/messTempalte"
    });
});

/* 渲染商品配置页 */
router.get('/proConfigure', function (req, res, next) {
    res.render('miniApp/product_configure.html',{
        currentUrl : "/miniApp/proConfigure"
    });
});

module.exports = router;