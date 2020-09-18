//引入核心文件
var express = require('express'); //加载express模块
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser'); //加载cookies模块
var bodyParser = require('body-parser'); //加载bodyParse 模块
var session = require("express-session");//引入session模块

// 创建app
var app = express();

// 视图模板引擎设置
// 视图模版存放的目录
app.set('views', __dirname + '/views');
app.set('view engine', 'art-template'); //定义当前的模版引擎  参1：必须是--> view engine  参2：是app.engine这个方法中定义的模板引擎名称 这里是art-template
app.engine('html', require('express-art-template')); //定义当前使用的模板引擎   第一个参数:模板引擎的名称(html/ejs) 第二个参数: 解析处理模板内容的方法 这里是express-art-template

// 视图配置
app.set('view options', {
    debug: process.env.NODE_ENV !== 'production'
});

//监听18106端口
app.listen(19059);


// 设置 favicon 图标 在 /public 目录
//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(session({
    resave: false, // don't save session if unmodified
    saveUninitialized: true, // don't create session until something stored
    secret: 'xiongbeishop456789!@#$%^&*',
    cookie:{
        maxAge: 24000 * 60 *60 // 过期时间设置为一天
    },
}));

// use this middleware to reset cookie expiration time
// when user hit page every time
app.use(function(req, res, next){
    req.session._garbage = Date();
    req.session.touch();
    next();
});

// 静态路由的设置
app.use('/public', express.static(path.join(__dirname, 'public'))); //设置静态路由 当用户访问的url是以/public开始  那么直接返回对应的__dirname+'/public'下的文件

// 后台路由的设置
var account = require('./routes/account');
app.all('*', account.requireAuthentication);//登录认证
app.use('/', require('./routes/index')); //设置后台首页模块路由
app.use('/account', require('./routes/account')); //设置后台账户页-账户模块路由
app.use('/product', require('./routes/product')); //设置商品设置模块路由

app.use('/classify', require('./routes/classify')); //设置商品分类模块路由

app.use('/brand', require('./routes/brand')); //设置商品设置模块路由

app.use('/member', require('./routes/member')); //设置会员管理模块路由

app.use('/mail', require('./routes/mail')); //设置站内信模块路由

app.use('/order', require('./routes/order')); //订单管理模块路由

app.use('/mallSetting', require('./routes/mallSetting')); //商城设置模块路由

app.use('/mall', require('./routes/mall')); //商城设置模块路由

app.use('/jurisdiction', require('./routes/jurisdiction')); //权限安全模块路由

app.use('/payAbout', require('./routes/payAbout')); //支付配送模块路由

app.use('/marketMode', require('./routes/marketMode')); //营销方式模块路由

app.use('/information', require('./routes/information')); //通知模块路由

app.use('/analysis', require('./routes/analysis')); //统计分析路由

app.use('/distribution', require('./routes/distribution')); //分销统计设置路由
app.use('/commission', require('./routes/commission')); //分销统计设置路由
app.use('/store', require('./routes/store')); //门店设置路由
app.use('/password', require('./routes/password'));//管理员账号模块路由
app.use('/homePage', require('./routes/home')); //首页内容设置路由
app.use('/application', require('./routes/application')); //营销方式模块路由
app.use('/bank', require('./routes/bank')); //银行管理路由
app.use('/business', require('./routes/business')); //商家管理路由
app.use('/giftCard', require('./routes/giftCard')); //礼品卡管理路由
app.use('/adminExchange', require('./routes/adminExchange')); //管理员兑换管理路由
// 404错误处理函数
app.use(function (req, res, next) {
    var err = new Error('Not Found');
    err.status = 404;
    next(err);
});

// 异常错误处理函数
app.use(function (err, req, res, next) {
    // set locals, only providing error in development
    res.locals.message = err.message;
    res.locals.error = req.app.get('env') === 'development' ? err : {};

    // render the error page
    res.status(err.status || 500);
    res.render('error');
});

module.exports = app;