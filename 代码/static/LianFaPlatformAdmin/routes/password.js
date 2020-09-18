var express = require('express');
var router = express.Router();

/* 渲染修改账号密码页 */
router.get('/', function (req, res, next) {
    res.render('password/amend_password.html',{
        currentUrl : "/password"
    });
});

module.exports = router;