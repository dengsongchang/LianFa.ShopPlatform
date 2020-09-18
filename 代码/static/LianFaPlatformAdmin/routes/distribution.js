var express = require('express');
var router = express.Router();

/* 分销统计 */
router.get('/statisticsDistribution', function (req, res, next) {
    res.render('distribution/statistics_distribution.html',{
        currentUrl :"/distribution/statisticsDistribution"
    });
});
/* 分销员列表 */
router.get('/UserList', function (req, res, next) {
    res.render('distribution/distribution_userList.html',{
        currentUrl :"/distribution/UserList"
    });
});
/* 分组列表 */
router.get('/GroupedList', function (req, res, next) {
    res.render('distribution/distribution_groupedList.html',{
        currentUrl :"/distribution/GroupedList"
    });
});
/* 分销员列表 */
router.get('/UserApply', function (req, res, next) {
    res.render('distribution/distribution_userApply.html',{
        currentUrl :"/distribution/UserApply"
    });
});
/* 添加熊王 */
router.get('/addKing', function (req, res, next) {
    res.render('distribution/distribution_addKing.html',{
        currentUrl :"/distribution/addKing"
    });
});
//分销明细
router.get('/distributionSubsidiary', function (req, res, next) {
    res.render('distribution/distributionSubsidiary.html',{
        currentUrl :"/distribution/distributionSubsidiary"
    });
});
//佣金明细
router.get('/commissionSubsidiary', function (req, res, next) {
    res.render('distribution/commissionSubsidiary.html',{
        currentUrl :"/distribution/commissionSubsidiary"
    });
});
//分销等级列表
router.get('/distributorGrade', function (req, res, next) {
    res.render('distribution/distributorGrade.html',{
        currentUrl :"/distribution/distributorGrade"
    });
});
//分销等级添加页面
router.get('/distributorGradeAdd', function (req, res, next) {
    res.render('distribution/distributorGradeAdd.html',{
        currentUrl :"/distribution/distributorGradeAdd"
    });
});
//分销等级编辑页面
router.get('/distributorGradeEdit', function (req, res, next) {
    res.render('distribution/distributorGradeEdit.html',{
        currentUrl :"/distribution/distributorGradeEdit"
    });
});

//分销邀请码页面（已用）
router.get('/distributorInviteCode', function (req, res, next) {
    res.render('distribution/distribution_inviteCode.html',{
        currentUrl :"/distribution/distributorInviteCode"
    });
});

//分销邀请码页面（未使用）
router.get('/distributorInviteCodeHasUse', function (req, res, next) {
    res.render('distribution/distribution_inviteCodeHasUse.html',{
        currentUrl :"/distribution/distributorInviteCodeHasUse"
    });
});

/* 提现列表 */
router.get('/withdrawalList', function (req, res, next) {
    res.render('distribution/withdrawalList.html',{
        currentUrl :"/distribution/withdrawalList"
    });
});
/* 历史提现列表 */
router.get('/historicalWithdrawal', function (req, res, next) {
    res.render('distribution/historicalWithdrawal.html',{
        currentUrl :"/distribution/historicalWithdrawal"
    });
});

/* 渲染银行卡管理页面 */
router.get('/bankList', function (req, res, next) {
    res.render('distribution/bankList.html',{
        currentUrl :"/distribution/bankList"
    });
});
/* 渲染创建银行卡页面 */
router.get('/bankAdd', function (req, res, next) {
    res.render('distribution/bankAdd.html',{
        currentUrl :"/distribution/bankAdd"
    });
});
/* 渲染编辑银行卡页面 */
router.get('/bankEdit', function (req, res, next) {
    res.render('distribution/bankEdit.html',{
        currentUrl :"/distribution/bankEdit"
    });
});

module.exports = router;