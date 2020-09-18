
var express = require('express');
var router = express.Router();


/* 渲染注册设置页 */
router.get('/mallrRegister', function (req, res, next) {
    res.render('mall/mali_register.html',{
        currentUrl :"/mall/mallrRegister"
    });
});







module.exports = router;