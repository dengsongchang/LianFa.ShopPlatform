$(function() {
    LayoutWithNav.init();
})

var LayoutWithNav = {
    layoutTpl: `{{each NoticeList as value i}}
        <li class="clearfix">
            <p class="info-text">{{NoticeList[i].Body}}</p>
            <p class="info-time">{{NoticeList[i].AddTime | convertNotificationTime}}</p>
            <img class="info-read" src="/public/images/true.png" data-nid={{NoticeList[i].NId}}>
        </li>
    {{/each}}`,
    sidebarTpl: "{{each PermissionsList as value i}}" +
        "<li class='{{PermissionsList[i].Action | selectFirstSide}}' mid='tab{{i+1}}' funurl='{{PermissionsList[i].Action | selectFirstUrl}}'>" +
        "<a tabindex='-1' href='javascript:void(0);'>" +
        "<i class='iconfont {{PermissionsList[i].IconClass == '' ? 'icon-yingyong' : PermissionsList[i].IconClass }}'></i>" +
        "<span>{{PermissionsList[i].Title}}</span>" +
        "{{if PermissionsList[i].ChildList.length>0}}" +
        "<span class='pull-right-container'>" +
        "<i class='fa fa-angle-left pull-right'></i>" +
        "</span>" +
        "{{/if}}" +
        "</a>" +
        "{{if PermissionsList[i].ChildList.length>0}}" +
        "<ul class='treeview-menu'>" +
        "{{each PermissionsList[i].ChildList as value j}}" +
        "<li class='{{PermissionsList[i].ChildList[j].Action | selectSecondSide}}' mid='tab{{i+1}}_{{j+1}}' funurl='{{PermissionsList[i].ChildList[j].Action}}'>" +
        "<a tabindex='-1' href='javascript:void(0);'>" +
        "<i class='fa fa-circle-o'></i>" +
        "<span>{{PermissionsList[i].ChildList[j].Title}}</span>" +
        "</a>" +
        "</li>" +
        "{{/each}}" +
        "</ul>" +
        "{{/if}}" +
        "</li>" +
        "{{/each}}",
    storeSideBarTpl: `
        <li class=" treeview {{'/' | selectFirstSide}}">
        <a href="{{'/' | selectFirstUrl}}">
            <i class="iconfont icon-shouye"></i>
            <span>首页</span>
        </a>
        </li>
        <li class="treeview {{'/order/' | selectFirstSide}}">
        <a href="#">
            <i class="iconfont icon-dingdan"></i>
            <span>订单管理</span>
            <span class="pull-right-container">
            <i class="fa fa-angle-left pull-right"></i>
            </span>
        </a>
        <ul class="treeview-menu">
            <li class="{{'/order/orderDepartmentList' | selectSecondSide}}">
                <a href="/order/orderDepartmentList">
                    <i class="fa fa-circle-o"></i>
                    <span>百货订单列表</span>
                </a>
            </li>
            <li class="{{'/order/orderServiceList' | selectSecondSide}}">
                <a href="/order/orderServiceList">
                    <i class="fa fa-circle-o"></i>
                    <span>服务订单列表</span>
                </a>
            </li>
            <li class="{{'/order/orderService' | selectSecondSide}}">
                <a href="/order/orderService">
                    <i class="fa fa-circle-o"></i>
                    <span>售后服务</span>
                </a>
            </li>
            <li class="{{'/order/refundApplication' | selectSecondSide}}">
                <a href="/order/refundApplication">
                    <i class="fa fa-circle-o"></i>
                    <span>退款申请单</span>
                </a>
            </li>
        </ul>
    </li>                                            
    `,
    init: function() {
        //tab的左右按钮初始化
        $(".wrapper-nav-tabs").css("margin-left", "0");
        LayoutWithNav.resizeHandle();

        // $('body').on('click', '#seeAll', function (e) {
        //     e.stopPropagation();
        //     var li = $(this);
        //     var menuId = 'tab20_1';
        //     var url = $(li).attr('funurl');
        //     var title = '消息中心';
        //     $('.footerA').bTabsAdd(menuId, title, url);
        // })
        //
        // $('.treeview-menu').find('li').on('click', function (e) {
        //
        //     e.stopPropagation();
        //     var li = $(this);
        //     var menuId = $(li).attr('mid');
        //     var url = $(li).attr('funurl');
        //     var title = $(this).find('span').text();
        //     $('.treeview-menu').bTabsAdd(menuId, title, url);
        // });

        //辅助函数（侧边栏选中）
        template.defaults.imports.selectFirstUrl = Common.selectFirstUrl;
        template.defaults.imports.selectSecondSide = Common.selectSecondSide;
        template.defaults.imports.selectFirstSide = Common.selectFirstSide;

        //加载左侧菜单列表
        LayoutWithNav.adminPermissionsList();

        $('body').on("click", '#logout', function() {
            LayoutWithNav.logout();
        });

        //标记已读
        $("#infoList").on("click", ".info-read", function() {
            var nid = $(this).attr("data-nid");
            LayoutWithNav.updateIsRead(nid);
        });

        //标记全部已读
        $("#markAll").on("click", function() {
            LayoutWithNav.updateAllIsRead();
        });

        //给页面图片添加error
        $("#userImg-mid").on("error", function() {
            $(this).attr("src", "/public/images/logo_login.png");
        });

        //给页面图片添加error
        $("#userImg-small,#userImg-big").on("error", function() {
            $(this).attr("src", "/public/images/logo.png");
        });

        //使用辅助函数
        template.defaults.imports.convertNotificationTime = Common.convertNotificationTime;

        LayoutWithNav.initUserInfo();

        if (localStorage.getItem('token') != null && localStorage.getItem('token') != '') {
            LayoutWithNav.getIsNewOrder();
            window.setInterval(function() {
                LayoutWithNav.timeActionWithOrder()
            }, 10000);
        }
    },
    //btab插件
    initTab: function() {
        $('a', $('#menuSideBar')).on('click', function(e) {
            // e.stopPropagation();
            var li = $(this).closest('li');
            var menuId = $(li).attr('mid');
            var url = $(li).attr('funurl');
            var title = $(this).text();

            if (url != '/' && url != '' && url != '#') {
                //二级导航栏加入选中样式
                $(this).parents('li').addClass('active');
                $(this).parents('li').siblings().removeClass('active');
                localStorage.setItem('PageIndex', "1")
                localStorage.setItem('PageSize', "10")

                //如果已经存在的话就不移动
                var flag = true;
                $('.listOutBox').find('a').each(function(index, item) {
                    if ($(item).attr('href').indexOf(menuId) > 0) {
                        flag = false
                    }
                })


                $('#mainFrameTabs').bTabsAdd(menuId, title, url);

            } else if (url == "/") {
                // //切换到首页的tab上
                $('ul.wrapper-nav-tabs a[href="#bTabs_navTabsMainPage"]', $("#mainFrameTabs")).tab('show');
            }
        });
        $('body').on('click', '.dropdown-menu #seeAll', function(e) {
            var li = $(this);
            var menuId = 'tab20_1';
            var url = $(li).attr('funurl');
            var title = '消息中心';
            if (url != '/' && url != '' && url != '#') {

                $('#mainFrameTabs').bTabsAdd(menuId, title, url);
            }
        })
        $('#mainFrameTabs').bTabs({
            //登录界面URL，用于登录超时后的跳转
            'loginUrl': '/account/login',
            //用于初始化主框架的宽度高度等，另外会在窗口尺寸发生改变的时候，也自动进行调整
            'resize': function() {
                $('#mainFrameTabs').height(window.height);

            }
        });
    },
    calOuterWidth: function(l) {
        var k = 0;
        $(l).each(function() {
            k += $(this).outerWidth(true)
        });
        return k;
    },
    tabsMove: function(n) {

        var li = $('.wrapper-nav-tabs .active');

        //获取当前tab的宽度
        var tabContentWidth = LayoutWithNav.calOuterWidth('.wrapper-nav-tabs>li');

        //可见内容区域宽度
        var showWidth = $('.containerHeadBox').width() - $('.J_tabLeft').width() - $('.J_tabRight').width();


        //当前左边距
        var curMarginLeft = parseInt($(".listOutBox").css('marginLeft'));
        var changeMarginLeft = 0;

        //获取当前li的宽度
        var activeWidth = li.outerWidth(true);

        if (tabContentWidth < showWidth) {
            changeMarginLeft = 0
        } else {
            var changeMarginLeft = curMarginLeft <= 0 ? curMarginLeft - activeWidth : 0;
        }

        $(".listOutBox").animate({
                marginLeft: changeMarginLeft + 40 + "px"
            },
            "fast")
    },
    //定时获取是否存在新的订单、刷新通知列表
    timeActionWithOrder: function() {
        LayoutWithNav.getIsNewOrder();
    },
    //是否有新的订单通知
    getIsNewOrder: function() {
        var methodName = "/notice/AdminNotSignNoticeList";
        var data = {};
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                if (data.Data.Total > 0) {
                    //获取订单提醒音频对象
                    var audio = document.getElementById("audio");
                    // audio.play();
                    LayoutWithNav.getInformationList();
                }
            } else {
                // Common.showErrorMsg(data.Message);
            }
        }, null, true);

    },
    //加载用户头像昵称信息
    initUserInfo: function() {
        $("#userImg-small").attr("src", localStorage.Avatar);
        $("#userImg-big").attr("src", localStorage.Avatar);
        $("#userImg-mid").attr("src", localStorage.Avatar);
        $("#username").text("熊呗");
        $("#username-mid").text(localStorage.UserName);
    },
    //标记全部已读
    updateAllIsRead: function() {
        var methodName = "/notice/AdminSetAllNoticeIsRead";
        var data = {};
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {

                $("#infoList").html("");
                $("#markAll").hide();
                $("#count").text(0);

            } else {
                Common.showErrorMsg(data.Message);
            }
        }, null, true);
    },
    //标记已读
    updateIsRead: function(NId) {
        var methodName = "/notice/AdminSetNoticeIsRead";
        var data = {
            NId: NId
        };
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {

                LayoutWithNav.getInformationList();

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //通知列表
    getInformationList: function() {
        var pageSize = 3;
        var methodName = "/notice/AdminNoticeList";
        var data = {
            IsRead: 0,
            Page: {
                PageSize: pageSize,
                PageIndex: 1
            }
        };
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                //渲染html
                var render = template.compile(LayoutWithNav.layoutTpl);
                var html = render(data.Data);
                $("#infoList").html(html);
                $("#count").text(data.Data.Total);
                if (data.Data.Total == 0) {
                    $("#markAll").hide();
                } else if (data.Data.Total > 0) {
                    $("#markAll").show();
                }
            } else {
                // Common.showErrorMsg(data.Message);
            }
        }, null, true);
    },
    //退出接口
    logout: function(isNoShowMsg) {
        $.ajax({
            url: "/account/loginout",
            type: "post",
            dataType: 'json',
            contentType: "application/json ; charset=utf-8",
        });

        localStorage.clear();

        if (isNoShowMsg) {
            window.location.href = "/account/login";
        } else {
            Common.showSuccessMsg("退出成功!", function() {
                if (window != top) {
                    top.location.href = "/account/login";
                } else {
                    window.location.href = "/account/login";
                }
            });
        }
    },
    //获取管理员权限列表
    adminPermissionsList: function() {
        var methodName = "/admin/GetAdminPermissionsList";
        var data = {};
        SignRequest.setAsync(methodName, data, function(data) {
            if (data.Code == "100") {
                if (localStorage.getItem('IsStoreManager') == "true") {
                    //渲染html
                    var render = template.compile(LayoutWithNav.storeSideBarTpl);
                } else {
                    //渲染html
                    var render = template.compile(LayoutWithNav.sidebarTpl);
                }
                console.log(data)

                var html = render(data.Data);
                $(".sidebar-menu").html(html);
                //判断是否有首页的权限
                data.Data.PermissionsList.forEach(function(item, index) {
                    if (item.Action == "/") {
                        localStorage.setItem('hasIndex', true)
                    }
                })

                //初始化标签页
                LayoutWithNav.initTab();

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //设置左右点击按钮
    resizeHandle: function() {
        //获取滚动区域的宽度
        var outBoxWidth = $('.listOutBox').width() - 80;
        // $('.listOutBox').width(outBoxWidth)
        console.log("滚动区域的宽度", outBoxWidth)
            //往左按钮点击
        $('body').off('click', '.J_tabLeft').on('click', '.J_tabLeft', function() {
                //获取当前tab的宽度
                var tabContentWidth = LayoutWithNav.calOuterWidth('.wrapper-nav-tabs>li');

                //可见内容区域宽度
                var showWidth = $('.containerHeadBox').width() - $('.J_tabLeft').width() - $('.J_tabRight').width();

                //获取当前的页数
                var currentIndex = 1;

                //当前左边距
                var curMarginLeft = parseInt($(".listOutBox").css('marginLeft'));

                if (curMarginLeft == 0) {
                    //如果左边距为 0则是第一页
                    currentIndex = 1;

                } else {
                    //计算偏移量和可视区域的倍数关系
                    //向上取整
                    var number = Math.ceil(Math.abs(curMarginLeft) / showWidth)
                        //当前页数
                    currentIndex = number + 1;
                }

                //如果当前页数大于1的话
                if (currentIndex > 1) {
                    if (currentIndex - 1 == 1) {
                        //回到首页
                        $('.listOutBox').animate({
                            'marginLeft': 40 + 'px'
                        });
                    } else {
                        //回到上一页
                        $('.listOutBox').animate({
                            'marginLeft': -(showWidth * (currentIndex - 2)) + 40 + 'px'
                        });
                    }


                } else {
                    console.log("到第一页了")
                }

            })
            //往右按钮点击
        $('body').off('click', '.J_tabRight').on('click', '.J_tabRight', function() {

            //获取当前tab的宽度
            var tabContentWidth = LayoutWithNav.calOuterWidth('.wrapper-nav-tabs>li');

            //可见内容区域宽度
            var showWidth = $('.containerHeadBox').width() - $('.J_tabLeft').width() - $('.J_tabRight').width();


            //总页数(已有项的总长度除以滚动区域的长度)
            var totalIndex = Math.ceil(tabContentWidth / showWidth);

            //获取当前的页数
            var currentIndex = 1;

            //当前左边距
            var curMarginLeft = parseInt($(".listOutBox").css('marginLeft'));


            //如果当前左边距为0的话，说明是第一页
            if (curMarginLeft == 0) {
                //说明是第一页
                currentIndex = 1
            } else {
                currentIndex = Math.round((Math.abs(curMarginLeft)) / showWidth) + 1;
            }
            //如果总页数大于当前页面的话就右移
            if (totalIndex > currentIndex) {
                $('.listOutBox').animate({
                    'marginLeft': -(showWidth * currentIndex) + 40 + 'px'
                });

            } else {
                console.log("到最后一页了")
            }

        })
    },
}
$(window).resize(function() {
    //当窗口大小发生变化的时候
    LayoutWithNav.resizeHandle();

});