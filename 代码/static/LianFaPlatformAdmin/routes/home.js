var express = require('express');
var router = express.Router();

/* 渲染轮播图 */
router.get('/', function(req, res, next) {
    res.render('homepage/banner.html', {
        currentUrl: '/homePage'
    });
});
/* 渲染轮播图 */
router.get('/editBanner', function(req, res, next) {
    res.render('homepage/editBanner.html', {
        currentUrl: '/homePage/editBanner'
    });
});


/* 渲染公告列表 */
router.get('/noticeList', function(req, res, next) {
    res.render('homepage/listoNotice.html', {
        currentUrl: "/homePage/noticeList",
    });
});
/* 渲染添加公告列表 */
router.get('/AddNoticeList', function(req, res, next) {
    res.render('homepage/listoNoticeAdd.html', {
        currentUrl: "/homePage/AddNoticeList",
    });
});
/* 渲染编辑公告列表 */
router.get('/EditNoticeList', function(req, res, next) {
    res.render('homepage/listoNoticeEdit.html', {
        currentUrl: "/homePage/EditNoticeList",
    });
});
/* 渲染关于我们 */
router.get('/aboutUs', function(req, res, next) {
    res.render('homepage/aboutUs.html', {
        currentUrl: "/homePage/aboutUs",
    });
});

/* 渲染常见问题列表 */
router.get('/problem_list', function(req, res, next) {
    res.render('homepage/problem_list.html', {
        currentUrl: "/homePage/problem_list",
    });
});

/* 渲染编辑常见问题 */
router.get('/problemEditor', function(req, res, next) {
    res.render('homepage/problem_editor.html', {
        currentUrl: "/homePage/problemEditor",
    });
});

/* 客服列表 */
router.get('/customer', function(req, res, next) {
    res.render('homepage/customer.html', {
        currentUrl: '/homePage/customer'
    });
});
/* 编辑客服 */
router.get('/editCustomer', function(req, res, next) {
    res.render('homepage/editCustomer.html', {
        currentUrl: '/homePage/editCustomer'
    });
});
/* 导航栏设置设置 */
router.get('/navSetting', function(req, res, next) {
    res.render('homepage/nav_setting.html', {
        currentUrl: '/homepage/navSetting'
    });
});
/* 导航栏编辑 */
router.get('/navEditor', function(req, res, next) {
    res.render('homepage/nav_editor.html', {
        currentUrl: '/homepage/navEditor'
    });
});
/* 导航栏添加 */
router.get('/navAdd', function(req, res, next) {
    res.render('homepage/nav_add.html', {
        currentUrl: '/homepage/navAdd'
    });
});

/* 渲染引导图 */
router.get('/guideImg', function(req, res, next) {
    res.render('homePage/guide_img_list.html', {
        currentUrl: '/homePage/guideImg'
    });
});
/* 广告设置 */
router.get('/advertisingSet', function(req, res, next) {
    res.render('homePage/advertisingSet.html', {
        currentUrl: '/homePage/advertisingSet'
    });
});
/* 弹窗广告列表 */
router.get('/popList', function(req, res, next) {
    res.render('homePage/popList.html', {
        currentUrl: '/homePage/popList'
    });
});
/* 添加弹窗广告 */
router.get('/popAdd', function(req, res, next) {
    res.render('homePage/popAdd.html', {
        currentUrl: '/homePage/popAdd'
    });
});
/* 编辑弹窗广告 */
router.get('/popEdit', function(req, res, next) {
    res.render('homePage/popEdit.html', {
        currentUrl: '/homePage/popEdit'
    });
});

/* 首页二级分类设置 */
router.get('/homeClassifySet', function(req, res, next) {
    res.render('homePage/homeClassifySet.html', {
        currentUrl: "/homePage/homeClassifySet"
    });
});
/* 首页二级分类列表 */
router.get('/homeClassifyList', function(req, res, next) {
    res.render('homePage/homeClassifyList.html', {
        currentUrl: "/homePage/homeClassifyList"
    });
});
/* 首页二级分类轮播图列表 */
router.get('/classifyBanner', function(req, res, next) {
    res.render('homePage/classifyBanner.html', {
        currentUrl: "/homePage/classifyBanner"
    });
});
/* 首页二级分类轮播图列表编辑 */
router.get('/editClassifyBanner', function(req, res, next) {
    res.render('homePage/editClassifyBanner.html', {
        currentUrl: "/homePage/editClassifyBanner"
    });
});

/* 渲染商城设置页 */
router.get('/mallBasicsetup', function(req, res, next) {
    res.render('homePage/mall_basic_setup.html', {
        currentUrl: "/homePage/mallBasicsetup"
    });
});

/* 图片库首页 */
router.get('/picLibrary', function(req, res, next) {
    res.render('homePage/picLibrary.html', {
        currentUrl: "/homePage/picLibrary"
    });
});
/* 图片库浏览图片详情页*/
router.get('/picLibraryDetail', function(req, res, next) {
    res.render('homePage/picLibraryDetail.html', {
        currentUrl: "/homePage/picLibraryDetail"
    });
});
/* 礼品卡banner图设置 */
router.get('/ExchangeBanner', function(req, res, next) {
    res.render('homePage/ExchangeBanner.html', {
        currentUrl: '/homePage/ExchangeBanner'
    });
});
/* 广告设置图设置 */
router.get('/dialogPoster', function(req, res, next) {
    res.render('homePage/dialogPoster.html', {
        currentUrl: '/homePage/dialogPoster'
    });
});
module.exports = router;