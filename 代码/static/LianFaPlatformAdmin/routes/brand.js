var express = require('express');
var router = express.Router();

/* 渲染商品品牌页 */
router.get('/', function (req, res, next) {
    res.render('brand/productBrand.html',{
        currentUrl :"/brand/Brand",
    });
});
/* 渲染商品品牌编辑页 */
router.get('/editBrand', function (req, res, next) {
    res.render('brand/productEditBrand.html',{
        currentUrl :"/brand/editBrand"
    });
});
/* 渲染商品标签页 */
router.get('/productLabel', function (req, res, next) {
    res.render('brand/productLabel.html',{
        currentUrl :"/brand/productLabel"
    });
});

module.exports = router;