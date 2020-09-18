import Vue from 'vue'
import Router from 'vue-router'
Vue.use(Router)

export default new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [{
            path: '/',
            name: 'index',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/index.vue'),
            meta: {
                title: '联发行'
            }
        },
        {
            path: '/editAddress',
            name: 'editAddress',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/editAddress.vue'),
            meta: {
                title: '编辑收货地址'
            }
        },
        {
            path: '/orderAddress',
            name: 'orderAddress',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/orderAddress.vue'),
            meta: {
                title: '订单地址'
            }

        },
        {
            path: '/addAddress',
            name: 'addAddress',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/addAddress.vue'),
            meta: {
                title: '新增收货地址'
            }
        },
        {
            path: '/myOrder',
            name: 'myOrder',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/myOrder.vue'),
            meta: {
                title: '我的订单'
            }
        },
        {
            path: '/orderDetail',
            name: 'orderDetail',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/orderDetail.vue'),
            meta: {
                title: '订单详情'
            }
        },
        {
            path: '/productDetail',
            name: 'productDetail',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/productDetail.vue'),
            meta: {
                title: '商品详情'
            }
        },
        {
            path: '/payOrder',
            name: 'payOrder',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/payOrder.vue'),
            meta: {
                title: '订单结算'
            }
        },
        {
            path: '/giftPayOrder',
            name: 'giftPayOrder',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/giftPayOrder.vue'),
            meta: {
                title: '礼品卡结算'
            }
        },
        {
            path: '/cart',
            name: 'cart',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/cart.vue'),
            meta: {
                title: '购物车'
            }
        },
        {
            path: '/login',
            name: 'login',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/login.vue'),
            meta: {
                title: '登录'
            }
        },

        // {
        //     path: '/register',
        //     name: 'register',
        //     component: () =>
        //         import ( /* webpackChunkName: "about" */ './views/register.vue')
        // },
        {
            path: '/user',
            name: 'user',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/user.vue'),
            meta: {
                title: '个人中心'
            }
        },
        {
            path: '/logistics',
            name: 'logistics',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/logistics.vue'),
            meta: {
                title: '查看物流'
            }
        },
        {
            path: '/giftDetail',
            name: 'giftDetail',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/giftDetail.vue'),
            meta: {
                title: '礼品卡详情'
            }
        },
        {
            path: '/buySuccess',
            name: 'buySuccess',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/buySuccess.vue'),
            meta: {
                title: '购买成功'
            }
        },
        {
            path: '/giftExchange',
            name: 'giftExchange',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/giftExchange.vue'),
            meta: {
                title: '礼品兑换'
            }
        },
        {
            path: '/cardPayOrder',
            name: 'cardPayOrder',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/cardPayOrder.vue'),
            meta: {
                title: '兑换礼品'
            }
        },
        {
            path: '/checkSuccess',
            name: 'checkSuccess',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/checkSuccess.vue'),
            meta: {
                title: '兑换成功'
            }
        },
        {
            path: '/giftOrder',
            name: 'giftOrder',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/giftOrder.vue'),
            meta: {
                title: '兑换记录'
            }
        },
        {
            path: '/registered',
            name: 'registered',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/registered.vue'),
            meta: {
                title: '注册'
            }
        },
        {
            path: '/giftcard',
            name: 'giftcard',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/giftcard.vue'),
            meta: {
                title: '礼品卡列表'
            }
        },
        {
            path: '/giftOrderDetail',
            name: 'giftOrderDetail',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/giftOrderDetail.vue'),
            meta: {
                title: '兑换详情'
            }
        },
        {
            path: '/giftdetails',
            name: 'giftdetails',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/giftdetails.vue'),
            meta: {
                title: '礼品卡详情'
            }
        },
        {
            path: '/giftinquiry',
            name: 'giftinquiry',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/giftinquiry.vue'),
            meta: {
                title: '礼品卡查询'
            }
        },
        {
            path: '/personaldetails',
            name: 'personaldetails',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/personaldetails.vue'),
            meta: {
                title: '个人信息'
            }
        },
        //支付中间页
        {
            path: '/middlePage',
            name: 'middlePage',
            component: () =>
                import ( /* webpackChunkName: "about" */ './views/pay/middlePage.vue'),
            meta: {
                title: '订单支付'
            }
        },
    ]
})