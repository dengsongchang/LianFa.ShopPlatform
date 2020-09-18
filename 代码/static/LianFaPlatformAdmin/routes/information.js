var express = require('express');
var router = express.Router();

/* 渲染通知页 */
router.get('/', function (req, res, next) {
    res.render('information/information.html',{
        currentUrl : "/information/information"
    });
});

module.exports = router;
