var express = require('express');
var router = express.Router();

/* 渲染管理员操作列表页 */
router.get('/jurisdictionOperation', function (req, res, next) {
    res.render('jurisdiction/jurisdiction_operation.html',{
        currentUrl :"/jurisdiction/jurisdictionOperation"
    });
});
/* 渲染编辑页 */
router.get('/jurisdictionEdit', function (req, res, next) {
    res.render('jurisdiction/jurisdiction_edit.html',{
        currentUrl :"/jurisdiction/jurisdictionEdit"
    });
});
/* 渲操作日志页 */
router.get('/jurisdictionJournal', function (req, res, next) {
    res.render('jurisdiction/jurisdiction_journal.html',{
        currentUrl :"/jurisdiction/jurisdictionJournal"
    });
});
/* 部门管理页 */
router.get('/department_management', function (req, res, next) {
    res.render('jurisdiction/department_management.html',{
        currentUrl :"/jurisdiction/department_management"
    });
});
/* 部门权限页 */
router.get('/department_power', function (req, res, next) {
    res.render('jurisdiction/department_power.html',{
        currentUrl :"/jurisdiction/department_power"
    });
});



module.exports = router;