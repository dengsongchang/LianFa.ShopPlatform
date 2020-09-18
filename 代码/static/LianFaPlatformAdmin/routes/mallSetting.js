var express = require('express');
var router = express.Router();

/* 图片库首页 */
router.get('/picLibrary', function (req, res, next) {
    res.render('mallSetting/picLibrary.html',{
        currentUrl :"/mallSetting/picLibrary"
    });
});
/* 图片库浏览图片首页 */
router.get('/picLibraryDetail', function (req, res, next) {
    res.render('mallSetting/picLibraryDetail.html',{
        currentUrl :"/mallSetting/picLibraryDetail"
    });
});


module.exports = router;