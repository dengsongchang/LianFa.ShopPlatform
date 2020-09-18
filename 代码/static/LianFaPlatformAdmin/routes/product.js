var express = require('express');
var router = express.Router();

/* 自营商品发布页 */
router.get('/', function (req, res, next) {
    res.render('product/product_release.html',{
        currentUrl :"/product/productRelease"
    });
});


/* 渲染商品列表页 */
router.get('/productList', function (req, res, next) {
    res.render('product/product_list.html',{
        currentUrl :"/product/productList"
    });
});




/* 渲染运费模板页*/
router.get('/freightTemplate', function (req, res, next) {
    res.render('product/freightTemplate.html',{
        currentUrl :"/product/freightTemplate"
    });
});
/* 渲染添加运费模板页*/
router.get('/addFreightTemplate', function (req, res, next) {
    res.render('product/addFreightTemplate.html',{
        currentUrl :"/product/freightTemplate"
    });
});
/* 渲染编辑运费模板页*/
router.get('/editFreightTemplate', function (req, res, next) {
    res.render('product/editFreightTemplate.html',{
        currentUrl :"/product/freightTemplate"
    });
});

module.exports = router;