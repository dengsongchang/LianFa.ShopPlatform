var express = require('express');
var router = express.Router();

/* 渲染会员收件箱页 */
router.get('/', function (req, res, next) {
    res.render('mail/inbox.html',{
        currentUrl :"/mail/inbox"
    });
});
/* 渲染会员收件箱已回復页 */
router.get('/hasreply', function (req, res, next) {
    res.render('mail/inbox_hasreply.html',{
        currentUrl :"/mail/inbox"
    });
});
/* 渲染会员收件箱未回復页 */
router.get('/notreply', function (req, res, next) {
    res.render('mail/inbox_notreply.html',{
        currentUrl :"/mail/inbox"
    });
});
/* 渲染会员收件箱回復页 */
router.get('/reply', function (req, res, next) {
    res.render('mail/inbox_reply.html',{
        currentUrl :"/mail/inbox"
    });
});

/* 渲染会员发件箱页 */
router.get('/outbox', function (req, res, next) {
    res.render('mail/outbox.html',{
        currentUrl :"/mail/outbox"
    });
});

module.exports = router;