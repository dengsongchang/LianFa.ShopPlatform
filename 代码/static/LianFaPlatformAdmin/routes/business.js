var express = require('express');
var router = express.Router();

/* 渲染会员列表页 */
router.get('/', function (req, res, next) {
    res.render('business/businessList.html', {
        currentUrl: "/business/businessList"
    });
});

module.exports = router;