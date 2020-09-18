var express = require('express');
var router = express.Router();


/* 提现设置 */
router.get('/withdrawConfig', function (req, res, next) {
    res.render('commission/withdrawConfig.html',{
        currentUrl :"/commission/withdrawConfig"
    });
});


module.exports = router;