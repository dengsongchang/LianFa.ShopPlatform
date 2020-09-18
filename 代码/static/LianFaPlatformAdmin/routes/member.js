var express = require('express');
var router = express.Router();

/* 渲染会员列表页 */
router.get('/', function (req, res, next) {
    res.render('member/member_list.html',{
        currentUrl : "/member/memberList"
    });
});

/* 渲染编辑会员信息页 */


/* 渲染会员详情页 */
router.get('/memberDetailInfo', function (req, res, next) {
    res.render('member/member_detailInfo.html',{
        currentUrl : "/member/memberDetailInfo"
    });
});

/* 渲染会员分层页 */
router.get('/memberLayer', function (req, res, next) {
    res.render('member/member_layer.html',{
        currentUrl : "/member/memberLayer"
    });
});

/* 渲染积分管理页 */
router.get('/memberIntegral', function (req, res, next) {
    res.render('member/member_integral.html',{
        currentUrl : "/member/memberIntegral"
    });
});

/* 渲染会员明细页 */
router.get('/memberDetail', function (req, res, next) {
    res.render('member/member_detail.html',{
        currentUrl : "/member/memberDetail"
    });
});
/* 渲染会员等级页 */
router.get('/memberGrade', function (req, res, next) {
    res.render('member/member_grade.html',{
        currentUrl : "/member/memberGrade"
    });
});
/* 渲染会员等级编辑页 */
router.get('/memberEditGrade', function (req, res, next) {
    res.render('member/member_Editgrade.html',{
        currentUrl : "/member/memberGrade"
    });
});
//会员标签页
router.get('/memberLabel', function (req, res, next) {
    res.render('member/member_label.html',{
        currentUrl : "/member/memberLabel"
    });
});
//编辑会员标签页
router.get('/memberEditLabel', function (req, res, next) {
    res.render('member/member_Editlabel.html',{
        currentUrl : "/member/memberLabel"
    });
});

//熊豆管理列表
router.get('/memberIntegralManagement', function (req, res, next) {
    res.render('member/member_IntegralManagement.html',{
        currentUrl : "/member/memberIntegralManagement"
    });
});

//熊豆规则添加管理列表
router.get('/memberIntegralAdd', function (req, res, next) {
    res.render('member/memberIntegralAdd.html',{
        currentUrl : "/member/memberIntegralAdd"
    });
});

module.exports = router;