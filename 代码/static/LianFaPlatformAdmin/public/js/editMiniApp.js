var EditMiniApp = {
    // 轮播图模板
    swiperTpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="1">
        <div class="diy-conitem sweiper">
            <ul style="padding:2px;">
                <li>
                    <img src="/public/images/waitupload.png" alt="">
                </li>
            </ul>
            <ul class="pageList">
                <li style="display:none"></li>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up" style="display:none">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    //导航栏模板
    navigationTpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="2">
        <div class="diy-conitem">
            <ul style="background:#fff;height:100px;padding:2px;" id="listMa">
                <li class="lisw4">
                    <span>
                        <a href="../countdown/countdown">
                            <img src="/public/images/waitupload.png">
                        </a>
                    </span>
                    <a class="members_nav1_name" href="../countdown/countdown">导航名称</a>
                </li>
                <li class="lisw4">
                    <span>
                        <a href="../searchresult/searchresult?cid=17">
                            <img src="/public/images/waitupload.png">
                        </a>
                    </span>
                    <a class="members_nav1_name" href="../searchresult/searchresult?cid=17">导航名称</a>
                </li>
                <li class="lisw4">
                    <span>
                        <a href="../couponlist/couponlist">
                            <img src="/public/images/waitupload.png">
                        </a>
                    </span>
                    <a class="members_nav1_name" href="../couponlist/couponlist">导航名称</a>
                </li>
                <li class="lisw4">
                    <span>
                        <a href="4006-089-731">
                            <img src="/public/images/waitupload.png">
                        </a>
                    </span>
                    <a class="members_nav1_name" href="4006-089-731">导航名称</a>
                </li>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    // app1模板
    app1Tpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="3">
        <div class="diy-conitem">
            <ul class="clearfix appUl" style="padding:2px">
                <li class="board5 title_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/bar01.png">
                        </a>
                    </span>
                </li>
                <li class="board5 big_img" style="float:left">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_1_2.png">
                        </a>
                    </span>
                </li>
                <li class="board5 mid_img" style="float:left">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_1_3.png">
                        </a>
                    </span>
                </li>
                <li class="board5 small_img" style="float:left">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_1_4.png">
                        </a>
                    </span>
                </li>
                <li class="board5 small_img" style="float:left">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_1_5.png">
                        </a>
                    </span>
                </li>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    // app2模板
    app2Tpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="4">
        <div class="diy-conitem">
            <ul class="clearfix" style="padding:2px">
                <li class="board7 title_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/bar01.png">
                        </a>
                    </span>
                </li>
                <li class="board7 mid1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_2_2.png">
                        </a>
                    </span>
                </li>
                <li class="board7 small1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_2_3.png">
                        </a>
                    </span>
                </li>
                <li class="board7 small1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_2_4.png">
                        </a>
                    </span>
                </li>
                <li class="board7 mid1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_2_5.png">
                        </a>
                    </span>
                </li>
                <li class="board7 small1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_2_6.png">
                        </a>
                    </span>
                </li>
                <li class="board7 small1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_2_7.png">
                        </a>
                    </span>
                </li>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    // app3模板
    app3Tpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="5">
        <div class="diy-conitem">
            <ul class="clearfix" style="padding:2px">
                <li class="board7 title_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/bar01.png">
                        </a>
                    </span>
                </li>
                <li class="board7 big1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_3_2.png">
                        </a>
                    </span>
                </li>
                <li class="board7 big1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_3_3.png">
                        </a>
                    </span>
                </li>
                <li class="board7 big1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_3_4.png">
                        </a>
                    </span>
                </li>
                <li class="board7 big1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_3_5.png">
                        </a>
                    </span>
                </li>
                <li class="board7 big1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_3_6.png">
                        </a>
                    </span>
                </li>
                <li class="board7 big1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_3_7.png">
                        </a>
                    </span>
                </li>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    // app4模板
    app4Tpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="6">
        <div class="diy-conitem">
            <ul class="clearfix" style="padding:2px">
                <li class="board9 title_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/bar01.png">
                        </a>
                    </span>
                </li>
                <li class="board9 big1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_4_2.png">
                        </a>
                    </span>
                </li>
                <li class="board9 big1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_4_3.png">
                        </a>
                    </span>
                </li>
                <li class="board9 big1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_4_4.png">
                        </a>
                    </span>
                </li>
                <li class="board9 big1_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_4_5.png">
                        </a>
                    </span>
                </li>
                <li class="board9 small2_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_4_6.png">
                        </a>
                    </span>
                </li>
                <li class="board9 small2_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_4_7.png">
                        </a>
                    </span>
                </li>
                <li class="board9 small2_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_4_8.png">
                        </a>
                    </span>
                </li>
                <li class="board9 small2_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_4_9.png">
                        </a>
                    </span>
                </li>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    // 广告位banner模板
    bannerTpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="7">
        <div class="diy-conitem">
            <ul class="clearfix" style="padding:2px">
                <li class="board ad_img">
                    <span>
                        <a href="#">
                            <img src="/public/images/waitupload.png">
                        </a>
                    </span>
                </li>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    //辅助空白模板
    blankTpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="8" style="background: #fff;">
        <div class="diy-conitem">
            <ul style="padding:2px;">
                <li class="members_con">
                    <section class="custom-space" style="height: 16px;"></section>
                </li>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    //限时抢购模板
    flashSaleTpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="9">
        <div class="diy-conitem" style="border: 2px dashed rgb(255, 170, 0);">
            <div class="time_title">
                <div style="float:left"><span class="title_main">限时秒杀</span><span class="title_sub">开抢预约 ，不容错过</span></div>
            </div>
            <ul style="background:#fff;height:108px;padding:2px;" id="listMa">
                <li class="lisw4" style="padding:0 3px">
                    <span>
                        <a href="#">
                            <img src="/public/images/time_kill.png">
                        </a>
                    </span>
                    <a style="padding:5px 0" class="members_nav1_name" href="#"><span class="ShaJia">秒杀价</span><span class="money">￥299</span></a>
                </li>
                <li class="lisw4" style="padding:0 3px">
                    <span>
                        <a href="#">
                            <img src="/public/images/time_kill.png">
                        </a>
                    </span>
                    <a style="padding:5px 0" class="members_nav1_name" href="#"><span class="ShaJia">秒杀价</span><span class="money">￥299</span></a>
                </li>
                <li class="lisw4" style="padding:0 3px">
                    <span>
                        <a href="#">
                            <img src="/public/images/time_kill.png">
                        </a>
                    </span>
                    <a style="padding:5px 0" class="members_nav1_name" href="#"><span class="ShaJia">秒杀价</span><span class="money">￥299</span></a>
                </li>
                <li class="lisw4" style="padding:0 3px">
                    <span>
                        <a href="#">
                            <img src="/public/images/time_kill.png">
                        </a>
                    </span>
                    <a style="padding:5px 0" class="members_nav1_name" href="#"><span class="ShaJia">秒杀价</span><span class="money">￥299</span></a>
                </li>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns" style="display: block;">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    // 团购专区
    groupBuyingTpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="10">
        <div class="diy-conitem" style="border: 2px dashed rgb(255, 170, 0);">
            <ul class="clearfix appUl" style="padding:2px">
                <li class="title10">
                    <span>
                        <a href="javascript:void(0)" class="">
                            团 / 购 / 专 / 区
                        </a>
                    </span>
                </li>
                <li class="banner10">
                    <span>
                        <a href="#">
                            <img src="/public/images/group_banner.png">
                        </a>
                    </span>
                </li>
                <li class="">
                    <div class="_msg">
                        <h1 class="hd">花朵印花</h1>
                        <div class="bd">全身衣装 · 舒适亲肌</div>
                        <div class="ft">
                            <div style="float:left"><span class="money">￥29</span><span style="color:#aaa;"><span>123</span>人已成团</span></div>
                            <div class="clouds">我要成团</div>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns" style="display: block;">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    // 团购专区
    groupBuyingTpl2: ` <div class="diy-conitem" style="border: 2px dashed rgb(255, 170, 0);">
            <ul class="clearfix appUl" style="padding:2px">
                <li class="title10">
                    <span>
                        <a href="javascript:void(0)" class="">
                            团 / 购 / 专 / 区
                        </a>
                    </span>
                </li>
                {{each dataset as v i}}
                <li class="banner10">
                    <span>
                        <a href="#">
                            <img src="{{dataset[i].ProductImg}}">
                        </a>
                    </span>
                </li>
                <li class="">
                    <div class="_msg">
                        <h1 class="hd">{{dataset[i].Name}}</h1>
                        <div class="bd">{{dataset[i].Summary}}</div>
                        <div class="ft">
                            <div style="float:left"><span class="money">￥{{dataset[i].GroupPrice}}</span><span style="color:#aaa;"><span>{{dataset[i].HaveMakeNumber}}</span>人已成团</span></div>
                            <div class="clouds">我要成团</div>
                        </div>
                    </div>
                </li>
                {{/each}}
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns" style="display: block;">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>`,
    // 新品推荐
    newBuyingTpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="11">
        <div class="diy-conitem" style="border: 2px dashed rgb(255, 170, 0);">
            <ul class="clearfix appUl" style="padding:2px">
                <li class="new_title">
                    <span>
                        <a href="javascript:void(0)" class="title_hd">
                            <img src="/public/images/new_title.png"> 新品推荐
                        </a>
                    </span>
                </li>
                <ul class="ul_content">
                    <li class="banner10">
                        <span>
                            <a href="#">
                                <img src="/public/images/newShop.png">
                            </a>
                        </span>
                    </li>
                    <li class="">
                        <div class="_msg2">
                            <div class="hd text-overflow" style="font-size: 18px;">花朵印花</div>
                            <div class="ft">
                                <div style="float:left">
                                    <span class="money">￥29</span>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
                <ul class="ul_content">
                    <li class="banner10">
                        <span>
                            <a href="#">
                                <img src="/public/images/newShop.png">
                            </a>
                        </span>
                    </li>
                    <li class="">
                        <div class="_msg">
                            <div class="hd text-overflow" style="font-size: 18px;">花朵印花</div>
                            <div class="ft">
                                <div style="float:left">
                                    <span class="money">￥29</span>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns" style="display: block;">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    // 新品推荐 有数据时
    newBuyingTpl2: `<div class="diy-conitem" style="border: 2px dashed rgb(255, 170, 0);">
            <ul class="clearfix appUl" style="padding:2px">
                <li class="new_title">
                    <span>
                        <a href="javascript:void(0)" class="title_hd">
                            <img src="/public/images/new_title.png"> 新品推荐
                        </a>
                    </span>
                </li>
                {{each dataset as v i}}
                <ul class="ul_content">
                    <li class="banner10">
                        <span>
                            <a href="#">
                                <img src="{{dataset[i].ShowImgFull}}">
                            </a>
                        </span>
                    </li>
                    <li class="">
                        <div class="_msg2">
                            <div class="hd text-overflow" style="font-size: 18px;">{{dataset[i].Name}}</div>
                            <div class="ft">
                                <div style="float:left">
                                    <span class="money">￥{{dataset[i].ShopPrice}}</span>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
                {{/each}}
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns" style="display: block;">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>`,
    //热销
    hotBuyingTpl: `<li class="ui-state-default ui-sortable-handle selected" data-type="12">
        <div class="diy-conitem" style="border: 2px dashed rgb(255, 170, 0);">
            <ul class="clearfix appUl" style="padding:2px">
                <li class="new_title">
                    <span>
                        <a href="javascript:void(0)" class="title_hd">
                            <img src="/public/images/hotSell.png"> 热销
                        </a>
                    </span>
                </li>
                <ul class="ul_content">
                    <li class="banner10">
                        <span>
                            <a href="#">
                                <img src="/public/images/newShop.png">
                            </a>
                        </span>
                    </li>
                    <li class="">
                        <div class="_msg2">
                            <div class="hd text-overflow" style="font-size: 18px;">花朵印花</div>
                            <div class="ft">
                                <div style="float:left">
                                    <span class="money">￥29</span>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
                <ul class="ul_content">
                    <li class="banner10">
                        <span>
                            <a href="#">
                                <img src="/public/images/newShop.png">
                            </a>
                        </span>
                    </li>
                    <li class="">
                        <div class="_msg">
                            <div class="hd text-overflow" style="font-size: 18px;">花朵印花</div>
                            <div class="ft">
                                <div style="float:left">
                                    <span class="money">￥29</span>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns" style="display: block;">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>
    </li>`,
    //热销 有数据时
    hotBuyingTpl2: ` <div class="diy-conitem" style="border: 2px dashed rgb(255, 170, 0);">
            <ul class="clearfix appUl" style="padding:2px">
                <li class="new_title">
                    <span>
                        <a href="javascript:void(0)" class="title_hd">
                            <img src="/public/images/hotSell.png"> 热销
                        </a>
                    </span>
                </li>
                {{each dataset as v i}}
                <ul class="ul_content">
                    <li class="banner10">
                        <span>
                            <a href="#">
                                <img src="{{dataset[i].ShowImgFull}}">
                            </a>
                        </span>
                    </li>
                    <li class="">
                        <div class="_msg2">
                            <div class="hd text-overflow" style="font-size: 18px;">{{dataset[i].Name}}</div>
                            <div class="ft">
                                <div style="float:left">
                                    <span class="money">￥{{dataset[i].ShopPrice}}</span>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
                {{/each}}
            </ul>
        </div>
        <div class="diy-conitem-action">
            <div class="diy-conitem-action-btns" style="display: block;">
                <a href="javascript:;" class="diy-conitem-btn diy-Up j-Up">上移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-Down j-Down">下移</a>
                <a href="javascript:;" class="diy-conitem-btn diy-edit j-edit">编辑</a>
                <a href="javascript:;" class="diy-conitem-btn diy-del j-del">删除</a>
            </div>
        </div>`,
    //轮播图模板详情
    swiper_detail_tpl: `<ul class="ctrl-item-list">
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/waitupload.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;" data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                      
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
                <div class="formitems" style="line-height: 30px">
                    <label class="fi-name">标题：</label>
                    <div class="form-controls">
                        <input type="text" style="height: 30px" name="title" class="input xlarge" value="">
                        <span class="fi-help-text"></span>
                    </div>
                </div>
                <div class="formitems" style="line-height:28px;">
                    <label class="fi-name">图片尺寸：</label>
                    <div class="form-controls" style="line-height:26px;">
                        750*290
                    </div>
                </div>
            </div>
            <div class="ctrl-item-list-actions">
                <a href="javascript:;" title="上移" class="j-moveup">
                    <img src="/public/images/turn_top.png" alt="">
                </a>
                <a href="javascript:;" title="下移" class="j-movedown">
                    <img src="/public/images/turn_buttom.png" alt="">
                </a>
                <a href="javascript:;" title="删除" class="j-del">
                    <img src="/public/images/del_modal.png" alt="">
                </a>
            </div>
        </li>
        <li class="ctrl-item-list-add" title="添加">+</li>
    </ul>`,
    //轮播图模板详情 有数据时
    swiper_detail_tpl2: `<ul class="ctrl-item-list">
        {{each dataset as v i }}
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="{{dataset[i].pic}}">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
                <div class="fl imgnav-info">
                    <div class="formitems">
                        <label class="fi-name">链接到：</label>
                        <div class="form-controls">
                            <div class="droplist droplist-title">
                                <span>{{#dataset[i] | replaceToLink}}</span>
                                    <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                                </a>
                                <ul class="droplist-menu">
                                    <li data-val="1">
                                        <a href="javascript:;" data-toggle="modal" data-target="#selectShop">选择商品</a>
                                    </li>
                                    <li data-val="2">
                                        <a href="javascript:;"></a>
                                    </li>
                         
                                    <li data-val="4">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="24">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="7">
                                        <a href="javascript:;">会员主页</a>
                                    </li>
                                    <li data-val="8">
                                        <a href="javascript:;">购物车</a>
                                    </li>
                                    <li data-val="10">
                                        <a href="javascript:;">自定义链接</a>
                                    </li>
                                    <li data-val="12">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="13">
                                        <a href="javascript:;">限时抢购</a>
                                    </li>
                                    <li data-val="14">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="5">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="15">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="16">
                                        <a href="javascript:;">优惠券列表</a>
                                    </li>
                                    <li data-val="17">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="18">
                                        <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                    </li>
                                    <li data-val="19">
                                        <a href="javascript:;"></a>
                                    </li>

                                    <li data-val="20" t="0">
                                        <a href="javascript:;"></a>
                                    </li>

                                    <li data-val="21">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="22">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="23">
                                        <a href="javascript:;">电话</a>
                                    </li>
                                    <li data-val="26">
                                        <a href="javascript:;">小程序</a>
                                    </li>
                                </ul>
                            </div>
                            <input type="hidden" class="j-verify" name="item_id" value="{{dataset[i].Title}}">
                            <span class="fi-help-text j-verify-linkType"></span>
                        </div>
                    </div>
                    <div class="formitems" style="line-height: 30px">
                        <label class="fi-name">标题：</label>
                        <div class="form-controls">
                            <input type="text" style="height: 30px" name="title" class="input xlarge" value="{{dataset[i].Title}}">
                            <span class="fi-help-text"></span>
                        </div>
                    </div>
                    <div class="formitems" style="line-height:28px;">
                        <label class="fi-name">图片尺寸：</label>
                        <div class="form-controls" style="line-height:26px;">
                            750*290
                        </div>
                    </div>
                </div>
            <div class="ctrl-item-list-actions">
                <a href="javascript:;" title="上移" class="j-moveup">
                    <img src="/public/images/turn_top.png" alt="">
                </a>
                <a href="javascript:;" title="下移" class="j-movedown">
                    <img src="/public/images/turn_buttom.png" alt="">
                </a>
                <a href="javascript:;" title="删除" class="j-del">
                    <img src="/public/images/del_modal.png" alt="">
                </a>
            </div>
        </li>
        {{/each}}
        <li class="ctrl-item-list-add" title="添加">+</li>
    </ul>`,
    //导航栏模板详情
    navigation_detail_tpl: `<ul class="ctrl-item-list">
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/waitupload.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                   
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
                <div class="formitems" style="line-height: 30px">
                    <label class="fi-name">标题：</label>
                    <div class="form-controls">
                        <input type="text" style="height: 30px" name="title" class="input xlarge" value="">
                        <span class="fi-help-text"></span>
                    </div>
                </div>
                <div class="formitems" style="line-height:28px;">
                    <label class="fi-name">图片尺寸：</label>
                    <div class="form-controls" style="line-height:26px;">
                        160*120
                    </div>
                </div>
            </div>
            <div class="ctrl-item-list-actions">
                <a href="javascript:;" title="上移" class="j-moveup">
                    <img src="/public/images/turn_top.png" alt="">
                </a>
                <a href="javascript:;" title="下移" class="j-movedown">
                    <img src="/public/images/turn_buttom.png" alt="">
                </a>
                <a href="javascript:;" title="删除" class="j-del">
                    <img src="/public/images/del_modal.png" alt="">
                </a>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
                    <div class="fl">
                        <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                            <img src="/public/images/waitupload.png">
                            <span class="imgnav-reselect">选择图片</span>
                        </div>
                        <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
                    </div>
                    <div class="fl imgnav-info">
                        <div class="formitems">
                            <label class="fi-name">链接到：</label>
                            <div class="form-controls">
                                <div class="droplist">
                                    <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                        <span>请选择</span>
                                        <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                                    </a>
                                    <ul class="droplist-menu">
                                        <li data-val="1">
                                            <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                        </li>
                                        <li data-val="2">
                                            <a href="javascript:;"></a>
                                        </li>
                               
                                        <li data-val="4">
                                            <a href="javascript:;"></a>
                                        </li>
                                        <li data-val="24">
                                            <a href="javascript:;"></a>
                                        </li>
                                        <li data-val="7">
                                            <a href="javascript:;">会员主页</a>
                                        </li>
                                        <li data-val="8">
                                            <a href="javascript:;">购物车</a>
                                        </li>
                                        <li data-val="10">
                                            <a href="javascript:;">自定义链接</a>
                                        </li>
                                        <li data-val="12">
                                            <a href="javascript:;"></a>
                                        </li>
                                        <li data-val="13">
                                            <a href="javascript:;">限时抢购</a>
                                        </li>
                                        <li data-val="14">
                                            <a href="javascript:;"></a>
                                        </li>
                                        <li data-val="5">
                                            <a href="javascript:;"></a>
                                        </li>
                                        <li data-val="15">
                                            <a href="javascript:;"></a>
                                        </li>
                                        <li data-val="16">
                                            <a href="javascript:;">优惠券列表</a>
                                        </li>
                                        <li data-val="17">
                                            <a href="javascript:;"></a>
                                        </li>
                                        <li data-val="18">
                                            <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                        </li>
                                        <li data-val="19">
                                            <a href="javascript:;"></a>
                                        </li>

                                        <li data-val="20" t="0">
                                            <a href="javascript:;"></a>
                                        </li>

                                        <li data-val="21">
                                            <a href="javascript:;"></a>
                                        </li>
                                        <li data-val="22">
                                            <a href="javascript:;"></a>
                                        </li>
                                        <li data-val="23">
                                            <a href="javascript:;">电话</a>
                                        </li>
                                        <li data-val="26">
                                            <a href="javascript:;">小程序</a>
                                        </li>
                                    </ul>
                                </div>
                                <input type="hidden" class="j-verify" name="item_id" value="">
                                <span class="fi-help-text j-verify-linkType"></span>
                            </div>
                        </div>
                        <div class="formitems" style="line-height: 30px">
                            <label class="fi-name">标题：</label>
                            <div class="form-controls">
                                <input type="text" style="height: 30px" name="title" class="input xlarge" value="">
                                <span class="fi-help-text"></span>
                            </div>
                        </div>
                        <div class="formitems" style="line-height:28px;">
                            <label class="fi-name">图片尺寸：</label>
                            <div class="form-controls" style="line-height:26px;">
                                160*120
                            </div>
                        </div>
                    </div>
                    <div class="ctrl-item-list-actions">
                        <a href="javascript:;" title="上移" class="j-moveup">
                            <img src="/public/images/turn_top.png" alt="">
                        </a>
                        <a href="javascript:;" title="下移" class="j-movedown">
                            <img src="/public/images/turn_buttom.png" alt="">
                        </a>
                        <a href="javascript:;" title="删除" class="j-del">
                            <img src="/public/images/del_modal.png" alt="">
                        </a>
                    </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/waitupload.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                       
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
                <div class="formitems" style="line-height: 30px">
                    <label class="fi-name">标题：</label>
                    <div class="form-controls">
                        <input type="text" style="height: 30px" name="title" class="input xlarge" value="">
                        <span class="fi-help-text"></span>
                    </div>
                </div>
                <div class="formitems" style="line-height:28px;">
                    <label class="fi-name">图片尺寸：</label>
                    <div class="form-controls" style="line-height:26px;">
                        160*120
                    </div>
                </div>
            </div>
            <div class="ctrl-item-list-actions">
                <a href="javascript:;" title="上移" class="j-moveup">
                    <img src="/public/images/turn_top.png" alt="">
                </a>
                <a href="javascript:;" title="下移" class="j-movedown">
                    <img src="/public/images/turn_buttom.png" alt="">
                </a>
                <a href="javascript:;" title="删除" class="j-del">
                    <img src="/public/images/del_modal.png" alt="">
                </a>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/waitupload.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
             
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
                <div class="formitems" style="line-height: 30px">
                    <label class="fi-name">标题：</label>
                    <div class="form-controls">
                        <input type="text" style="height: 30px" name="title" class="input xlarge" value="">
                        <span class="fi-help-text"></span>
                    </div>
                </div>
                <div class="formitems" style="line-height:28px;">
                    <label class="fi-name">图片尺寸：</label>
                    <div class="form-controls" style="line-height:26px;">
                        160*120
                    </div>
                </div>
            </div>
            <div class="ctrl-item-list-actions">
                <a href="javascript:;" title="上移" class="j-moveup">
                    <img src="/public/images/turn_top.png" alt="">
                </a>
                <a href="javascript:;" title="下移" class="j-movedown">
                    <img src="/public/images/turn_buttom.png" alt="">
                </a>
                <a href="javascript:;" title="删除" class="j-del">
                    <img src="/public/images/del_modal.png" alt="">
                </a>
            </div>
        </li>
    </ul>`,
    //导航栏模板详情 有数据时
    navigation_detail_tpl2: `<ul class="ctrl-item-list">
        {{each dataset as v i }}
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="{{dataset[i].pic}}">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
                <div class="fl imgnav-info">
                    <div class="formitems">
                        <label class="fi-name">链接到：</label>
                        <div class="form-controls">
                            <div class="droplist droplist-title">
                                <span>{{#dataset[i] | replaceToLink}}</span>
                                    <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                                </a>
                                <ul class="droplist-menu">
                                    <li data-val="1">
                                        <a href="javascript:;" data-toggle="modal" data-target="#selectShop">选择商品</a>
                                    </li>
                                    <li data-val="2">
                                        <a href="javascript:;"></a>
                                    </li>
               
                                    <li data-val="4">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="24">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="7">
                                        <a href="javascript:;">会员主页</a>
                                    </li>
                                    <li data-val="8">
                                        <a href="javascript:;">购物车</a>
                                    </li>
                                    <li data-val="10">
                                        <a href="javascript:;">自定义链接</a>
                                    </li>
                                    <li data-val="12">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="13">
                                        <a href="javascript:;">限时抢购</a>
                                    </li>
                                    <li data-val="14">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="5">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="15">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="16">
                                        <a href="javascript:;">优惠券列表</a>
                                    </li>
                                    <li data-val="17">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="18">
                                        <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                    </li>
                                    <li data-val="19">
                                        <a href="javascript:;"></a>
                                    </li>

                                    <li data-val="20" t="0">
                                        <a href="javascript:;"></a>
                                    </li>

                                    <li data-val="21">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="22">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="23">
                                        <a href="javascript:;">电话</a>
                                    </li>
                                    <li data-val="26">
                                        <a href="javascript:;">小程序</a>
                                    </li>
                                </ul>
                            </div>
                            <input type="hidden" class="j-verify" name="item_id" value="{{dataset[i].Title}}">
                            <span class="fi-help-text j-verify-linkType"></span>
                        </div>
                    </div>
                    <div class="formitems" style="line-height: 30px">
                        <label class="fi-name">标题：</label>
                        <div class="form-controls">
                            <input type="text" style="height: 30px" name="title" class="input xlarge" value="{{dataset[i].Title}}">
                            <span class="fi-help-text"></span>
                        </div>
                    </div>
                    <div class="formitems" style="line-height:28px;">
                        <label class="fi-name">图片尺寸：</label>
                        <div class="form-controls" style="line-height:26px;">
                            160*120
                        </div>
                    </div>
                </div>
            <div class="ctrl-item-list-actions">
                <a href="javascript:;" title="上移" class="j-moveup">
                    <img src="/public/images/turn_top.png" alt="">
                </a>
                <a href="javascript:;" title="下移" class="j-movedown">
                    <img src="/public/images/turn_buttom.png" alt="">
                </a>
                <a href="javascript:;" title="删除" class="j-del">
                    <img src="/public/images/del_modal.png" alt="">
                </a>
            </div>
        </li>
        {{/each}}
        <li class="ctrl-item-list-add" title="添加">+</li>
    </ul>`,
    //app1模板详情
    app1_modal_tpl: `<ul class="ctrl-item-list">
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/bar01.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_1_2.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                         
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_1_3.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                        
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_1_4.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                         
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_1_5.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                        
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
    </ul>`,
    //app1模板详情 有数据时
    app1_modal_tpl2: `<ul class="ctrl-item-list">
        {{each dataset as v i }}
            <li class="ctrl-item-list-li clearfix">
                <div class="fl">
                    <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                        <img src="{{dataset[i].pic}}">
                        <span class="imgnav-reselect">选择图片</span>
                    </div>
                    <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
                </div>
                <div class="fl imgnav-info">
                    <div class="formitems">
                        <label class="fi-name">链接到：</label>
                        <div class="form-controls">
                            <div class="droplist">
                                <span>{{#dataset[i] | replaceToLink}}</span>
                                    <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                                </a>
                                <ul class="droplist-menu">
                                    <li data-val="1">
                                        <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                    </li>
                                    <li data-val="2">
                                        <a href="javascript:;"></a>
                                    </li>
                            
                                    <li data-val="4">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="24">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="7">
                                        <a href="javascript:;">会员主页</a>
                                    </li>
                                    <li data-val="8">
                                        <a href="javascript:;">购物车</a>
                                    </li>
                                    <li data-val="10">
                                        <a href="javascript:;">自定义链接</a>
                                    </li>
                                    <li data-val="12">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="13">
                                        <a href="javascript:;">限时抢购</a>
                                    </li>
                                    <li data-val="14">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="5">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="15">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="16">
                                        <a href="javascript:;">优惠券列表</a>
                                    </li>
                                    <li data-val="17">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="18">
                                        <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                    </li>
                                    <li data-val="19">
                                        <a href="javascript:;"></a>
                                    </li>

                                    <li data-val="20" t="0">
                                        <a href="javascript:;"></a>
                                    </li>

                                    <li data-val="21">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="22">
                                        <a href="javascript:;"></a>
                                    </li>
                                    <li data-val="23">
                                        <a href="javascript:;">电话</a>
                                    </li>
                                    <li data-val="26">
                                        <a href="javascript:;">小程序</a>
                                    </li>
                                </ul>
                            </div>
                            <input type="hidden" class="j-verify" name="item_id" value="">
                            <span class="fi-help-text j-verify-linkType"></span>
                        </div>
                    </div>
                </div>
            </li>
        {{/each}}
    </ul>`,
    //app2模板详情
    app2_modal_tpl: `<ul class="ctrl-item-list">
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/bar01.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                           
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_2_2.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                         
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_2_3.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                         
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_2_4.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                          
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_2_5.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                       
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_2_6.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                         
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_2_7.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                        
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
    </ul>`,
    //app3模板详情
    app3_modal_tpl: `<ul class="ctrl-item-list">
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/bar01.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                        
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_3_2.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                           
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_3_3.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                          
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_3_4.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                         
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_3_5.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                        
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_3_6.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                         
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_3_7.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                        
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
    </ul>`,
    //app4模板详情
    app4_modal_tpl: `<ul class="ctrl-item-list">
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/bar01.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                           
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_4_2.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                           
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_4_3.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                            
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_4_4.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                           
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_4_5.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                       
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_4_6.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                        
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_4_7.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                           
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_4_8.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                          
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/group_4_9.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                            
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
    </ul>`,
    // banner广告图详情
    banner_modal_tpl: `<ul class="ctrl-item-list">
        <li class="ctrl-item-list-li clearfix">
            <div class="fl">
                <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                    <img src="/public/images/waitupload.png">
                    <span class="imgnav-reselect">选择图片</span>
                </div>
                <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
            </div>
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>请选择</span>
                                <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                            </a>
                            <ul class="droplist-menu">
                                <li data-val="1">
                                    <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                                </li>
                                <li data-val="2">
                                    <a href="javascript:;"></a>
                                </li>
                           
                                <li data-val="4">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="24">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="7">
                                    <a href="javascript:;">会员主页</a>
                                </li>
                                <li data-val="8">
                                    <a href="javascript:;">购物车</a>
                                </li>
                                <li data-val="10">
                                    <a href="javascript:;">自定义链接</a>
                                </li>
                                <li data-val="12">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="13">
                                    <a href="javascript:;">限时抢购</a>
                                </li>
                                <li data-val="14">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="5">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="15">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="16">
                                    <a href="javascript:;">优惠券列表</a>
                                </li>
                                <li data-val="17">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="18">
                                    <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                                </li>
                                <li data-val="19">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="20" t="0">
                                    <a href="javascript:;"></a>
                                </li>

                                <li data-val="21">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="22">
                                    <a href="javascript:;"></a>
                                </li>
                                <li data-val="23">
                                    <a href="javascript:;">电话</a>
                                </li>
                                <li data-val="26">
                                    <a href="javascript:;">小程序</a>
                                </li>
                            </ul>
                        </div>
                        <input type="hidden" class="j-verify" name="item_id" value="">
                        <span class="fi-help-text j-verify-linkType"></span>
                    </div>
                </div>
            </div>
        </li>
    </ul>`,
    //辅助空白详情
    blank_modal_tpl: `<ul class="ctrl-item-list">
        <li class="ctrl-item-list-li clearfix">
            <div class="formitems inline">
                <label class="fi-name">高度：</label>
                <div class="form-controls">
                    <input  id="ex1" data-slider-id="ex1Slider" style="width:300px" type="text"  
                    data-slider-min="0" data-slider-max="100" data-slider-step="1"  
                    data-slider-value="15"/>  
                    <span style="float:right;position:relative;top:0px;left:-43px;"><span class="blank_num">0</span></span>
                </div>
                
            </div>
        </li>
    </ul>`,
    //限时抢购模板详情
    flash_detail_tpl: `<ul class="ctrl-item-list">
        <li class="ctrl-item-list-li clearfix">
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a data-toggle="modal" data-target="#flashShop" href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>选择商品</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a data-toggle="modal" data-target="#flashShop" href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>选择商品</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a data-toggle="modal" data-target="#flashShop" href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>选择商品</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </li>
        <li class="ctrl-item-list-li clearfix">
            <div class="fl imgnav-info">
                <div class="formitems">
                    <label class="fi-name">链接到：</label>
                    <div class="form-controls">
                        <div class="droplist">
                            <a data-toggle="modal" data-target="#flashShop" href="javascript:;" class="droplist-title j-droplist-toggle">
                                <span>选择商品</span>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </li>
    </ul>`,
    //限时抢购模板详情
    new_sell_detail_tpl: `<ul class="ctrl-item-list">
     <li class="ctrl-item-list-li clearfix">
         <div class="fl imgnav-info">
             <div class="formitems">
                 <label class="fi-name">链接到：</label>
                 <div class="form-controls">
                     <div class="droplist">
                         <a data-toggle="modal" data-target="#flashShop" href="javascript:;" class="droplist-title j-droplist-toggle">
                             <span>选择抢购</span>
                         </a>
                     </div>
                 </div>
             </div>
         </div>
     </li>
    </ul>`,
    //团购模板详情
    new_bulk_detail_tpl: `<ul class="ctrl-item-list">
     <li class="ctrl-item-list-li clearfix">
         <div class="fl imgnav-info">
             <div class="formitems">
                 <label class="fi-name">链接到：</label>
                 <div class="form-controls">
                     <div class="droplist">
                         <a data-toggle="modal" data-target="#bulkShop" href="javascript:;" class="droplist-title j-droplist-toggle">
                             <span>选择团购</span>
                         </a>
                     </div>
                 </div>
             </div>
         </div>
     </li>
    </ul>`,
    //新品专区 热销模板详情
    new_hot_detail_tpl: `<ul class="ctrl-item-list">
     <li class="ctrl-item-list-li clearfix">
         <div class="fl imgnav-info">
             <div class="formitems">
                 <label class="fi-name">链接到：</label>
                 <div class="form-controls">
                     <div class="droplist">
                         <a data-toggle="modal" data-target="#newHotShop" href="javascript:;" class="droplist-title j-droplist-toggle">
                             <span>选择商品</span>
                         </a>
                     </div>
                 </div>
             </div>
         </div>
     </li>
    </ul>`,
    //轮播图添加图片模板
    swiper_addPIc_tpl: `<li class="ctrl-item-list-li clearfix">
        <div class="fl">
            <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                <img src="/public/images/waitupload.png">
                <span class="imgnav-reselect">选择图片</span>
            </div>
            <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
        </div>
        <div class="fl imgnav-info">
            <div class="formitems">
                <label class="fi-name">链接到：</label>
                <div class="form-controls">
                    <div class="droplist">
                        <a href="javascript:;" class="droplist-title j-droplist-toggle">
                            <span>请选择</span>
                            <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                        </a>
                        <ul class="droplist-menu">
                            <li data-val="1">
                                <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                            </li>
                            <li data-val="2">
                                <a href="javascript:;"></a>
                            </li>
                      
                            <li data-val="4">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="24">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="7">
                                <a href="javascript:;">会员主页</a>
                            </li>
                            <li data-val="8">
                                <a href="javascript:;">购物车</a>
                            </li>
                            <li data-val="10">
                                <a href="javascript:;">自定义链接</a>
                            </li>
                            <li data-val="12">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="13">
                                <a href="javascript:;">限时抢购</a>
                            </li>
                            <li data-val="14">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="5">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="15">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="16">
                                <a href="javascript:;">优惠券列表</a>
                            </li>
                            <li data-val="17">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="18">
                                <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                            </li>
                            <li data-val="19">
                                <a href="javascript:;"></a>
                            </li>

                            <li data-val="20" t="0">
                                <a href="javascript:;"></a>
                            </li>

                            <li data-val="21">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="22">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="23">
                                <a href="javascript:;">电话</a>
                            </li>
                            <li data-val="26">
                                <a href="javascript:;">小程序</a>
                            </li>
                        </ul>
                    </div>
                    <input type="hidden" class="j-verify" name="item_id" value="">
                    <span class="fi-help-text j-verify-linkType"></span>
                </div>
            </div>
            <div class="formitems" style="line-height: 30px">
                <label class="fi-name">标题：</label>
                <div class="form-controls">
                    <input type="text" style="height: 30px" name="title" class="input xlarge" value="">
                    <span class="fi-help-text"></span>
                </div>
            </div>
            <div class="formitems" style="line-height:28px;">
                <label class="fi-name">图片尺寸：</label>
                <div class="form-controls" style="line-height:26px;">
                    160*120
                </div>
            </div>
        </div>
        <div class="ctrl-item-list-actions">
            <a href="javascript:;" title="上移" class="j-moveup">
                <img src="/public/images/turn_top.png" alt="">
            </a>
            <a href="javascript:;" title="下移" class="j-movedown">
                <img src="/public/images/turn_buttom.png" alt="">
            </a>
            <a href="javascript:;" title="删除" class="j-del">
                <img src="/public/images/del_modal.png" alt="">
            </a>
        </div>
        </li><li class="ctrl-item-list-add" title="添加">+
    </li>`,
    //轮播图添加图片模板2
    swiper_addPIc2_tpl: `<li class="ctrl-item-list-li clearfix">
        <div class="fl">
            <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                <img src="/public/images/waitupload.png">
                <span class="imgnav-reselect">选择图片</span>
            </div>
            <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
        </div>
        <div class="fl imgnav-info">
            <div class="formitems">
                <label class="fi-name">链接到：</label>
                <div class="form-controls">
                    <div class="droplist">
                        <a href="javascript:;" class="droplist-title j-droplist-toggle">
                            <span>请选择</span>
                            <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                        </a>
                        <ul class="droplist-menu">
                            <li data-val="1">
                                <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                            </li>
                            <li data-val="2">
                                <a href="javascript:;"></a>
                            </li>
                     
                            <li data-val="4">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="24">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="7">
                                <a href="javascript:;">会员主页</a>
                            </li>
                            <li data-val="8">
                                <a href="javascript:;">购物车</a>
                            </li>
                            <li data-val="10">
                                <a href="javascript:;">自定义链接</a>
                            </li>
                            <li data-val="12">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="13">
                                <a href="javascript:;">限时抢购</a>
                            </li>
                            <li data-val="14">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="5">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="15">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="16">
                                <a href="javascript:;">优惠券列表</a>
                            </li>
                            <li data-val="17">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="18">
                                <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                            </li>
                            <li data-val="19">
                                <a href="javascript:;"></a>
                            </li>

                            <li data-val="20" t="0">
                                <a href="javascript:;"></a>
                            </li>

                            <li data-val="21">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="22">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="23">
                                <a href="javascript:;">电话</a>
                            </li>
                            <li data-val="26">
                                <a href="javascript:;">小程序</a>
                            </li>
                        </ul>
                    </div>
                    <input type="hidden" class="j-verify" name="item_id" value="">
                    <span class="fi-help-text j-verify-linkType"></span>
                </div>
            </div>
            <div class="formitems" style="line-height: 30px">
                <label class="fi-name">标题：</label>
                <div class="form-controls">
                    <input type="text" style="height: 30px" name="title" class="input xlarge" value="">
                    <span class="fi-help-text"></span>
                </div>
            </div>
            <div class="formitems" style="line-height:28px;">
                <label class="fi-name">图片尺寸：</label>
                <div class="form-controls" style="line-height:26px;">
                    160*120
                </div>
            </div>
        </div>
        <div class="ctrl-item-list-actions">
            <a href="javascript:;" title="上移" class="j-moveup">
                <img src="/public/images/turn_top.png" alt="">
            </a>
            <a href="javascript:;" title="下移" class="j-movedown">
                <img src="/public/images/turn_buttom.png" alt="">
            </a>
            <a href="javascript:;" title="删除" class="j-del">
                <img src="/public/images/del_modal.png" alt="">
            </a>
    </div>`,
    //导航栏添加图片模板
    navigation_addPIc_tpl: `<li class="ctrl-item-list-li clearfix">
        <div class="fl">
            <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                <img src="/public/images/waitupload.png">
                <span class="imgnav-reselect">选择图片</span>
            </div>
            <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
        </div>
        <div class="fl imgnav-info">
            <div class="formitems">
                <label class="fi-name">链接到：</label>
                <div class="form-controls">
                    <div class="droplist">
                        <a href="javascript:;" class="droplist-title j-droplist-toggle">
                            <span>请选择</span>
                            <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                        </a>
                        <ul class="droplist-menu">
                            <li data-val="1">
                                <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                            </li>
                            <li data-val="2">
                                <a href="javascript:;"></a>
                            </li>
                       
                            <li data-val="4">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="24">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="7">
                                <a href="javascript:;">会员主页</a>
                            </li>
                            <li data-val="8">
                                <a href="javascript:;">购物车</a>
                            </li>
                            <li data-val="10">
                                <a href="javascript:;">自定义链接</a>
                            </li>
                            <li data-val="12">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="13">
                                <a href="javascript:;">限时抢购</a>
                            </li>
                            <li data-val="14">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="5">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="15">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="16">
                                <a href="javascript:;">优惠券列表</a>
                            </li>
                            <li data-val="17">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="18">
                                <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                            </li>
                            <li data-val="19">
                                <a href="javascript:;"></a>
                            </li>

                            <li data-val="20" t="0">
                                <a href="javascript:;"></a>
                            </li>

                            <li data-val="21">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="22">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="23">
                                <a href="javascript:;">电话</a>
                            </li>
                            <li data-val="26">
                                <a href="javascript:;">小程序</a>
                            </li>
                        </ul>
                    </div>
                    <input type="hidden" class="j-verify" name="item_id" value="">
                    <span class="fi-help-text j-verify-linkType"></span>
                </div>
            </div>
            <div class="formitems" style="line-height: 30px">
                <label class="fi-name">标题：</label>
                <div class="form-controls">
                    <input type="text" style="height: 30px" name="title" class="input xlarge" value="">
                    <span class="fi-help-text"></span>
                </div>
            </div>
            <div class="formitems" style="line-height:28px;">
                <label class="fi-name">图片尺寸：</label>
                <div class="form-controls" style="line-height:26px;">
                    120*120
                </div>
            </div>
        </div>
        <div class="ctrl-item-list-actions">
            <a href="javascript:;" title="上移" class="j-moveup">
                <img src="/public/images/turn_top.png" alt="">
            </a>
            <a href="javascript:;" title="下移" class="j-movedown">
                <img src="/public/images/turn_buttom.png" alt="">
            </a>
            <a href="javascript:;" title="删除" class="j-del">
                <img src="/public/images/del_modal.png" alt="">
            </a>
        </div>
        </li><li class="ctrl-item-list-add" title="添加">+
    </li>`,
    //导航栏添加图片模板2
    navigation_addPIc2_tpl: `<li class="ctrl-item-list-li clearfix">
        <div class="fl">
            <div class="imgnav j-selectimg" data-target="#insertcustom_img" data-toggle="modal">
                <img src="/public/images/waitupload.png">
                <span class="imgnav-reselect">选择图片</span>
            </div>
            <span class="fi-help-text txtCenter mgt5 j-verify-pic"></span>
        </div>
        <div class="fl imgnav-info">
            <div class="formitems">
                <label class="fi-name">链接到：</label>
                <div class="form-controls">
                    <div class="droplist">
                        <a href="javascript:;" class="droplist-title j-droplist-toggle">
                            <span>请选择</span>
                            <img src="/public/images/bottom.png" alt="" style="padding-bottom: 3px;padding-left: 3px;">
                        </a>
                        <ul class="droplist-menu">
                            <li data-val="1">
                                <a href="javascript:;"data-toggle="modal" data-target="#selectShop">选择商品</a>
                            </li>
                            <li data-val="2">
                                <a href="javascript:;"></a>
                            </li>
                     
                            <li data-val="4">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="24">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="7">
                                <a href="javascript:;">会员主页</a>
                            </li>
                            <li data-val="8">
                                <a href="javascript:;">购物车</a>
                            </li>
                            <li data-val="10">
                                <a href="javascript:;">自定义链接</a>
                            </li>
                            <li data-val="12">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="13">
                                <a href="javascript:;">限时抢购</a>
                            </li>
                            <li data-val="14">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="5">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="15">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="16">
                                <a href="javascript:;">优惠券列表</a>
                            </li>
                            <li data-val="17">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="18">
                                <a href="javascript:;" data-toggle="modal" data-target="#couponModal">领取优惠劵</a>
                            </li>
                            <li data-val="19">
                                <a href="javascript:;"></a>
                            </li>

                            <li data-val="20" t="0">
                                <a href="javascript:;"></a>
                            </li>

                            <li data-val="21">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="22">
                                <a href="javascript:;"></a>
                            </li>
                            <li data-val="23">
                                <a href="javascript:;">电话</a>
                            </li>
                            <li data-val="26">
                                <a href="javascript:;">小程序</a>
                            </li>
                        </ul>
                    </div>
                    <input type="hidden" class="j-verify" name="item_id" value="">
                    <span class="fi-help-text j-verify-linkType"></span>
                </div>
            </div>
            <div class="formitems" style="line-height: 30px">
                <label class="fi-name">标题：</label>
                <div class="form-controls">
                    <input type="text" style="height: 30px" name="title" class="input xlarge" value="">
                    <span class="fi-help-text"></span>
                </div>
            </div>
            <div class="formitems" style="line-height:28px;">
                <label class="fi-name">图片尺寸：</label>
                <div class="form-controls" style="line-height:26px;">
                    120*120
                </div>
            </div>
        </div>
        <div class="ctrl-item-list-actions">
            <a href="javascript:;" title="上移" class="j-moveup">
                <img src="/public/images/turn_top.png" alt="">
            </a>
            <a href="javascript:;" title="下移" class="j-movedown">
                <img src="/public/images/turn_buttom.png" alt="">
            </a>
            <a href="javascript:;" title="删除" class="j-del">
                <img src="/public/images/del_modal.png" alt="">
            </a>
    </div>`,
    //导航栏添加图片并添加到页面
    addNavTpl: `<li class="lisw4">
        <span>
            <a href="../countdown/countdown">
                <img src="/public/images/waitupload.png">
            </a>
        </span>
        <a class="members_nav1_name" href="../countdown/countdown">导航名称</a>
    </li>`,
    //添加模块为0时
    addNavTpl2: `<li class="lisw4">
        <span>
            <a href="../countdown/countdown">
                <img src="/public/images/waitupload.png">
            </a>
        </span>
        <a class="members_nav1_name" href="../countdown/countdown">导航名称</a>
        </li>
        <li class="lisw4">
            <span>
                <a href="../countdown/countdown">
                    <img src="/public/images/waitupload.png">
                </a>
            </span>
            <a class="members_nav1_name" href="../countdown/countdown">导航名称</a>
        </li>
        <li class="lisw4">
            <span>
                <a href="../countdown/countdown">
                    <img src="/public/images/waitupload.png">
                </a>
            </span>
            <a class="members_nav1_name" href="../countdown/countdown">导航名称</a>
        </li>
        <li class="lisw4">
            <span>
                <a href="../countdown/countdown">
                    <img src="/public/images/waitupload.png">
                </a>
            </span>
            <a class="members_nav1_name" href="../countdown/countdown">导航名称</a>
    </li>`,
    //模板
    GoodsListTemplate: `<ul class="layer_ul_1">
        {{each CategoryList as value i}}
            {{if CategoryList[i].Layer == "1"}}
            <li class="layer-1">
                <div class="contentBody">
                    <div class="col-xs-1">{{CategoryList[i].CateId}}</div>
                    <div class="col-xs-7">
                        <div class="classify_one">
                            <i class="icon-jia jia_collapse_one">
                            {{if CategoryList[i].Haschild == "1"}}
                            <img src="../public/images/suoqi.png" alt="">
                            {{else if CategoryList[i].Haschild == "0"}}
                            <img src="../public/images/zhankai.png" alt="">
                            {{/if}}
                            </i>
                            <i class="iconfont icon-wenjianjia"></i>
                            {{CategoryList[i].Name}}
                        </div>
                    </div>
                    <div class="col-xs-4">
                    {{if CategoryList[i].Haschild == "0"}}
                        <span class="pick" data-id={{CategoryList[i].CateId}} data-name="{{CategoryList[i].Name}}">选择</span>
                    {{/if}} 
                    </div>
                </div>
                {{each CategoryList as value j}}
                {{if CategoryList[j].ParentId == CategoryList[i].CateId}}
                <ul class="layer_ul_2" style="display: none">
                    <li class="layer-2">
                        <div class="contentBody">
                            <div class="col-xs-1">{{CategoryList[j].CateId}}</div>
                            <div class="col-xs-7">
                                <div class="classify_two">
                                    <i class="icon-jia jia_collapse_two">
                                    {{if CategoryList[j].Haschild == "1"}}
                                    <img src="../public/images/suoqi.png" alt="">
                                    {{else if CategoryList[j].Haschild == "0" }}
                                    <img src="../public/images/zhankai.png" alt="">
                                    {{/if}}
                                    </i>
                                    <i class="iconfont icon-wenjianjia"></i>
                                    {{CategoryList[j].Name}}
                                </div>
                            </div>
                            <div class="col-xs-4">
                            {{if CategoryList[j].Haschild == "0"}}
                                <span class="pick" data-id={{CategoryList[j].CateId}} data-name="{{CategoryList[j].Name}}">选择</span>
                            {{/if}}
                            </div>
                        </div>
                        {{each CategoryList as value k}}
                        {{if CategoryList[k].ParentId ==CategoryList[j].CateId}}
                        <ul class="layer_ul_3" style="display: none">
                            <li class="layer-3">
                                <div class="contentBody">
                                    <div class="col-xs-1">{{CategoryList[k].CateId}}</div>
                                    <div class="col-xs-7">
                                        <div class="classify_three">
                                            <i class="iconfont icon-wenjianjia"></i>
                                            {{CategoryList[k].Name}}
                                        </div>
                                    </div>
                                    <div class="col-xs-4">
                                    {{if CategoryList[k].Haschild == "0"}}
                                        <span class="pick" data-id={{CategoryList[k].CateId}} data-name="{{CategoryList[k].Name}}">选择</span>
                                    {{/if}}
                                    </div>
                                </div>
                            </li>
                        </ul>
                        {{/if}}
                        {{/each}}

                    </li>
                </ul>
                {{/if}}
                {{/each}}
            </li>
            {{/if}}
        {{/each}}
    </ul>`,
    brandTpl: `
        {{each BrandList as value i}}
            <option value="{{BrandList[i].BrandId}}">{{BrandList[i].Name}}</option>
        {{/each}}
    `,
    labelTpl: `
        {{each ProductLabelList as value i}}
            <label class="checkbox-inline">
                <input type="checkbox" value="{{ProductLabelList[i].PLId}}"> {{ProductLabelList[i].Name}}
            </label>
        {{/each}}
    `,
    imgTpl: `
        {{each imglistData as value i}}
            <div class="img-container">
                <img class="select-img" src="{{imglistData[i].ImgFull}}" data-src="{{imglistData[i].Img}}">
                <input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">
                <img class="close-img" src="/public/images/close.png" style="width: 100%;height: 100%;">
            </div>
        {{/each}}
        <div class="img-container">
            <img class="select-img" src="/public/images/addImg.png">
            <input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">
            <img class="close-img" src="/public/images/close.png">
        </div>
    `,
    cateTpl: `
        <option value="0">请选择</option>
        {{each CategoryList as value i}}
            <option value="{{CategoryList[i].CateId}}">{{CategoryList[i].Name}}</option>
        {{/each}}
    `,
    cateBoxTemplate: `<li class="newT" data-toggle="modal" data-target="#addGrouping">
        <span class="glyphicon glyphicon-plus"></span>新建分组</li>
        <li class="ImgT a_b" data-id="0">未分组</li>
        {{each List as v i}}
        <li class="ImgT" data-id="{{List[i].CategoryId}}">{{List[i].Name}}</li>
        {{/each}}
    `,
    picBoxTemplate: `{{each List as value i}}
        <div class="pin col-md-2" data-id="{{List[i].MaterialId}}">
            <div class="file-item thumbnail box">
                <img style="height:120px"src="{{List[i].FileUrl}}" alt="">
            </div>
            <div></div>
        </div>
        {{/each}}
    `,
    moveBoxTemplate: `<li class="file_li" data-id="0">
                <i class="glyphicon glyphicon-folder-open" style="color:#bde0e9;margin-right: 10px"></i>未分组
     </li>
     {{each List as value i}}
         <li class="file_li" data-id="{{List[i].CategoryId}}">
                    <i class="glyphicon glyphicon-folder-open" style="color:#bde0e9;margin-right: 10px"></i>{{List[i].Name}}
         </li>
     {{/each}}
    `,
    Type: "",
    PicPageIndex: 1,
    init: function () {
        //使用辅助函数替换链接
        template.defaults.imports.replaceToLink = EditMiniApp.replaceToLink;
        EditMiniApp.AdminGetHomeConfig();
        //初始化布局拖动事件
        EditMiniApp.sortableInit();
        //添加模块点击事件
        $("#addModal").on('click', '.j-diy-addModule', function () {
            var modal_type = $(this).attr("data-type");
            //根据不同类型生成不同的模板
            EditMiniApp.generateTpl(modal_type, true);
        });

        $('body').on('click', '.droplist .typeV', function () {

            return false;
        })


        //点击某一模块进行编辑
        $("body").on("click", ".ui-state-default", function () {
            // console.log($(this).attr("data-Type"));
            $(this).addClass("selected").siblings(".ui-state-default").removeClass("selected").find("ul");
            if ($("#sortable .selected").attr("data-dataset") == "" || $("#sortable .selected").attr("data-dataset") == undefined || JSON.parse($("#sortable .selected").attr("data-dataset")).length == 0) {
                EditMiniApp.detailTpl($(this).attr("data-Type"));
            } else {
                console.log($("#sortable .selected").attr("data-dataset"))
                EditMiniApp.isSaveData($(this).attr("data-Type"));
            }

            EditMiniApp.Type = $(this).attr("data-Type");
            $(".diy-conitem").css("border", "none");
            $("#sortable .selected").find(".diy-conitem").css("border", "2px dashed #fa0");
            $(".diy-conitem-action-btns").css("display", "none");
            $("#sortable .selected").find(".diy-conitem-action-btns").css("display", "block");
        });
        $("body").on("mouseover", ".ui-state-default", function () {
            EditMiniApp.Type = $(this).attr("data-Type");
        });

        //删除模块
        $("#sortable").on("click", ".j-del", function (e) {
            e.stopPropagation();
            if ($(this).parents(".ui-state-default").hasClass("selected")) {
                var Type = false;
                EditMiniApp.detailTpl(Type);
            }
            var _this = $(this)
            Common.confirmDialog("删除后将不可恢复，是否继续？", function () {
                _this.parents(".ui-state-default").remove();
            });
        });

        //模板上移事件绑定
        $("#sortable").on("click", ".j-Up", function () {
            $(this).parent().parent().parent().insertBefore($(this).parent().parent().parent().prev());
            EditMiniApp.UpOrDownShow();
            if ($("#sortable .selected").attr("data-type") == 1) {
                $(".selected .sweiper").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            } else if ($("#sortable .selected").attr("data-type") == 2) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 3) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .appUl .board5:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 4 || $("#sortable .selected").attr("data-type") == 5) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .board7:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 6) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .board9:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 7) {
                $(".selected .ad_img").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            }
        });

        //模板下移事件绑定
        $("#sortable").on("click", ".j-Down", function () {
            $(this).parent().parent().parent().insertAfter($(this).parent().parent().parent().next());
            EditMiniApp.UpOrDownShow();
            if ($("#sortable .selected").attr("data-type") == 1) {
                $(".selected .sweiper").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            } else if ($("#sortable .selected").attr("data-type") == 2) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 3) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .appUl .board5:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 4 || $("#sortable .selected").attr("data-type") == 5) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .board7:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 6) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .board9:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 7) {
                $(".selected .ad_img").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            }
        });

        //删除图片添加选项
        $("#diy-ctrl").on("click", ".j-del", function () {
            if ($("#sortable .selected").attr("data-type") == 1) {
                $(".selected .pageList li:eq(" + $(this).parents(".ctrl-item-list-li").index() + ")").remove();
                if ($(".ctrl-item-list-li").length < 7) {
                    if ($(".ctrl-item-list-add").length == 0) {
                        console.log($(this).parents(".ctrl-item-list"));
                        $('<li class="ctrl-item-list-add" title="添加">+</li>').appendTo($(this).parents(".ctrl-item-list"));
                    }
                }
                $(this).parents(".ctrl-item-list-li").remove();
                if ($(".ctrl-item-list-li").length == 1 || $(".ctrl-item-list-li").length == 0) {
                    $(".selected .pageList li").hide();
                }
                $(".selected .sweiper").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            } else if ($("#sortable .selected").attr("data-type") == 2) {
                $(".selected #listMa li:eq(" + $(this).parents(".ctrl-item-list-li").index() + ")").remove();
                if ($(".ctrl-item-list-li").length < 5) {
                    if ($(".ctrl-item-list-add").length == 0) {
                        $('<li class="ctrl-item-list-add" title="添加">+</li>').appendTo($(this).parents(".ctrl-item-list"));
                    }
                }
                $(this).parents(".ctrl-item-list-li").remove();
                if ($(".ctrl-item-list-li").length == 0) {
                    $(EditMiniApp.addNavTpl2).appendTo(".selected #listMa");
                    $(".selected .lisw4").css("width", "25%");
                } else if ($(".ctrl-item-list-li").length == 1) {
                    $(".selected .lisw4").css("width", "99%");
                } else if ($(".ctrl-item-list-li").length == 2) {
                    $(".selected .lisw4").css("width", "49.8%");
                } else if ($(".ctrl-item-list-li").length == 3) {
                    $(".selected .lisw4").css("width", "33.2%");
                } else if ($(".ctrl-item-list-li").length == 4) {
                    $(".selected .lisw4").css("width", "25%");
                }
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            }
            EditMiniApp.storageOffside();
        });
        // 上移图片添加选项
        $("#diy-ctrl").on("click", ".j-moveup", function () {
            $(this).parent().parent().insertBefore($(this).parent().parent().prev('.ctrl-item-list-li'));
            EditMiniApp.storageOffside();
            if ($("#sortable .selected").attr("data-type") == 1) {
                $(".selected .sweiper").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            } else if ($("#sortable .selected").attr("data-type") == 2) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 3) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .appUl .board5:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 4 || $("#sortable .selected").attr("data-type") == 5) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .board7:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 6) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .board9:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 7) {
                $(".selected .ad_img").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            }
        });
        // 下移图片添加选项
        $("#diy-ctrl").on("click", ".j-movedown", function () {
            $(this).parent().parent().insertAfter($(this).parent().parent().next('.ctrl-item-list-li'));
            EditMiniApp.storageOffside();
            if ($("#sortable .selected").attr("data-type") == 1) {
                $(".selected .sweiper").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            } else if ($("#sortable .selected").attr("data-type") == 2) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 3) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .appUl .board5:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 4 || $("#sortable .selected").attr("data-type") == 5) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .board7:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 6) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .board9:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 7) {
                $(".selected .ad_img").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            }
        });

        //选中标签替换链接
        $("#diy-ctrl").on("click", ".droplist-menu li", function () {
            EditMiniApp.dataVal = $(this).attr("data-val");
            EditMiniApp.replaceLink($(this));
            $(this).parents(".droplist-menu").hide();
        });
        //选中标签替换链接
        $("#diy-ctrl").on("mouseover", ".droplist", function () {
            $(this).find(".droplist-menu").show();
        });

        //点击添加图片
        $("#diy-ctrl").on("click", ".ctrl-item-list-add", function () {
            $(".selected .pageList li").show();
            var ulTpl = $(this).parents(".ctrl-item-list");
            var _this = $(this);
            // 选中轮播图
            if ($("#sortable .selected").attr("data-type") == 1) {
                if ($(".ctrl-item-list-li").length < 5) {
                    $(EditMiniApp.swiper_addPIc_tpl).appendTo(ulTpl);
                    _this.remove();
                } else if ($(".ctrl-item-list-li").length == 5) {
                    // 少于4个时添加<li>+</li>
                    $(EditMiniApp.swiper_addPIc2_tpl).appendTo(ulTpl);
                    $(".ctrl-item-list-add").remove();
                }
                $(".selected .pageList").append("<li></li>");
                //当轮播图图片选择少于2个时
                if ($(".ctrl-item-list-li").length == 1 || $(".ctrl-item-list-li").length == 0) {
                    $(".selected .pageList li").hide();
                }
                $(".selected .sweiper").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            } else if ($("#sortable .selected").attr("data-type") == 2) {
                // 选中导航栏时
                if ($(".ctrl-item-list-li").length == 0) {
                    $(".diy-ctrl-item").html(EditMiniApp.navigation_detail_tpl);
                    return;
                } else if ($(".ctrl-item-list-li").length < 3) {
                    $(EditMiniApp.navigation_addPIc_tpl).appendTo(ulTpl);
                    _this.remove();
                } else if ($(".ctrl-item-list-li").length == 3) {
                    $(EditMiniApp.navigation_addPIc2_tpl).appendTo(ulTpl);
                    $(".ctrl-item-list-add").remove();
                }
                // 添加图片选项左侧选中导航栏变化
                if ($(".ctrl-item-list-li").length == 1) {
                    $(EditMiniApp.addNavTpl).appendTo(".selected #listMa");
                    $(".selected .lisw4").css("width", "99%");
                } else if ($(".ctrl-item-list-li").length == 2) {
                    $(EditMiniApp.addNavTpl).appendTo(".selected #listMa");
                    $(".selected .lisw4").css("width", "49.8%");
                } else if ($(".ctrl-item-list-li").length == 3) {
                    $(EditMiniApp.addNavTpl).appendTo(".selected #listMa");
                    $(".selected .lisw4").css("width", "33.2%");
                } else if ($(".ctrl-item-list-li").length == 4) {
                    $(EditMiniApp.addNavTpl).appendTo(".selected #listMa");
                    $(".selected .lisw4").css("width", "25%");
                }
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            }
            EditMiniApp.storageOffside();
            return false;
        });
        //点击自定义弹窗中的图片，选中与否样式
        $('.wrapper_imgs_box').on('click', '.pin', function () {
            $(this).find('.box').siblings('div').toggleClass('choice_true_dv');
            $(this).toggleClass('rm');
        });

        //选择图片模态框显示
        $('#insertcustom_img').on('show.bs.modal', function (e) {
            EditMiniApp.adminGetCategoryList();
            EditMiniApp.adminGetMaterialList();
            setTimeout(() => {
                if (EditMiniApp.PicTotal > 0) {
                    //初始化分页
                    $.jqPaginator('#pagination2', {
                        totalCounts: EditMiniApp.PicTotal,
                        pageSize: 18,
                        visiblePages: 10,
                        currentPage: 1,
                        prev: '<li class="prev"><a href="javascript:;">&lt;</a></li>',
                        next: '<li class="next"><a href="javascript:;">&gt;</a></li>',
                        page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
                        onPageChange: function (num, type) {
                            EditMiniApp.PicPageIndex = num;
                            EditMiniApp.adminGetMaterialList();
                            $('#p2').text(type + '：' + num);
                        }
                    });
                }
            }, 200);

        });

        //选择商品模态框显示
        $('#selectShop').on('show.bs.modal', function (e) {
            EditMiniApp.getProductBrand(); //初始化商品品牌
            EditMiniApp.getProductCategory();
            EditMiniApp.selectBootstrapTable();
        });
        //选择分类模态框显示
        $('#goodsCategory').on('show.bs.modal', function (e) {
            EditMiniApp.adminCategoryList(); //初始化商品分类
        });
        //选择优惠劵模态框显示
        $('#couponModal').on('show.bs.modal', function (e) {
            EditMiniApp.initCouponBootstrapTable();
        });

        //选择商品分类模态框显示
        $('#removeFileModal').on('show.bs.modal', function (e) {
            EditMiniApp.adminGetCategoryList();
        });
        //选择限时抢购模态框显示
        $('#flashShop').on('show.bs.modal', function (e) {
            EditMiniApp.initBootstrapTable();
        });
        //选择限时抢购模态框显示
        $('#bulkShop').on('show.bs.modal', function (e) {
            EditMiniApp.initGroupTable();
        });
        //选择限时抢购模态框显示
        $('#newHotShop').on('show.bs.modal', function (e) {
            EditMiniApp.newHotShopGroupTable();
        });
        //选择商品确定
        $("#addProduct").click(function () {

            var selectContent = $('#productTabel').bootstrapTable('getSelections')[0];
            if (typeof (selectContent) == 'undefined') {
                Common.showErrorMsg("请选择一列数据!");
                return false;
            }
            EditMiniApp.selectLink(EditMiniApp.productName, selectContent);
            $("#selectShop").modal("hide");
        });

        //选择优惠劵确定
        $("#coupConfirm").click(function () {
            var selectContent = $('#couponTabel').bootstrapTable('getSelections')[0];
            if (typeof (selectContent) == 'undefined') {
                Common.showErrorMsg("请选择一列数据!");
                return false;
            }
            EditMiniApp.selectLink(EditMiniApp.couponName);
            $('#couponModal').modal("hide");
        });

        //选择限时抢购确定
        $("#addFlash").click(function () {
            console.log($('#tb_limit_content').bootstrapTable('getSelections'))
            var selectContent = $('#tb_limit_content').bootstrapTable('getSelections')[0];
            if (typeof (selectContent) == 'undefined') {
                Common.showErrorMsg("请选择一列数据!");
                return false;
            }
            if ($("#sortable .selected").attr("data-type") == "9") {
                if ($('#tb_limit_content').bootstrapTable('getSelections').length > 4) {
                    Common.showErrorMsg("不可以选择超过4条商品");
                    return false;
                }
            }
            EditMiniApp.datasetArray = $('#tb_limit_content').bootstrapTable('getSelections');
            console.log(EditMiniApp.datasetArray)
            EditMiniApp.selectLink("", selectContent);
            var content = {
                dataset: EditMiniApp.datasetArray
            }
            if ($("#sortable .selected").attr("data-type") == 9) {
                $.each(content.dataset, function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", item.Img);
                    $(".selected #listMa li:eq(" + index + ")").find(".money").text("￥" + item.ToSnapUpPrice);
                });
            } else if ($("#sortable .selected").attr("data-type") == 10) {
                var render = template.compile(EditMiniApp.groupBuyingTpl2);
                var html = render(content);
                $("#sortable .selected").html(html);
            } else if ($("#sortable .selected").attr("data-type") == 11) {
                var render = template.compile(EditMiniApp.newBuyingTpl2);
                var html = render(content);
                $("#sortable .selected").html(html);
            } else if ($("#sortable .selected").attr("data-type") == 12) {
                var render = template.compile(EditMiniApp.hotBuyingTpl2);
                var html = render(content);
                $("#sortable .selected").html(html);
            }
            $("#flashShop").modal("hide");
        });

        //选择团购确定
        $("#addbulk").click(function () {
            console.log($('#GroupBox').bootstrapTable('getSelections'))
            var selectContent = $('#GroupBox').bootstrapTable('getSelections')[0];
            if (typeof (selectContent) == 'undefined') {
                Common.showErrorMsg("请选择一列数据!");
                return false;
            }
            EditMiniApp.datasetArray = $('#GroupBox').bootstrapTable('getSelections');
            EditMiniApp.selectLink("",selectContent);
            var content = {
                dataset: EditMiniApp.datasetArray
            }
            if ($("#sortable .selected").attr("data-type") == 9) {
                $.each(content.dataset, function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", item.Img);
                    $(".selected #listMa li:eq(" + index + ")").find(".money").text("￥" + item.ToSnapUpPrice);
                });
            } else if ($("#sortable .selected").attr("data-type") == 10) {
                var render = template.compile(EditMiniApp.groupBuyingTpl2);
                var html = render(content);
                $("#sortable .selected").html(html);
            } else if ($("#sortable .selected").attr("data-type") == 11) {
                var render = template.compile(EditMiniApp.newBuyingTpl2);
                var html = render(content);
                $("#sortable .selected").html(html);
            } else if ($("#sortable .selected").attr("data-type") == 12) {
                var render = template.compile(EditMiniApp.hotBuyingTpl2);
                var html = render(content);
                $("#sortable .selected").html(html);
            }
            $("#bulkShop").modal("hide");
        });

        //选择限时抢购确定
        $("#newHotbulk").click(function () {
            console.log($('#newHotTable').bootstrapTable('getSelections'))
            var selectContent = $('#newHotTable').bootstrapTable('getSelections')[0];
            if (typeof (selectContent) == 'undefined') {
                Common.showErrorMsg("请选择一列数据!");
                return false;
            }
            EditMiniApp.datasetArray = $('#newHotTable').bootstrapTable('getSelections');
            EditMiniApp.selectLink();
            var content = {
                dataset: EditMiniApp.datasetArray
            }
            if ($("#sortable .selected").attr("data-type") == 9) {
                $.each(content.dataset, function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", item.Img);
                    $(".selected #listMa li:eq(" + index + ")").find(".money").text("￥" + item.ToSnapUpPrice);
                });
            } else if ($("#sortable .selected").attr("data-type") == 10) {
                var render = template.compile(EditMiniApp.groupBuyingTpl2);
                var html = render(content);
                $("#sortable .selected").html(html);
            } else if ($("#sortable .selected").attr("data-type") == 11) {
                var render = template.compile(EditMiniApp.newBuyingTpl2);
                var html = render(content);
                $("#sortable .selected").html(html);
            } else if ($("#sortable .selected").attr("data-type") == 12) {
                var render = template.compile(EditMiniApp.hotBuyingTpl2);
                var html = render(content);
                $("#sortable .selected").html(html);
            }
            $("#newHotShop").modal("hide");
        });
        //选择分类点击选择
        $("#goodsCategory").on("click", ".pick", function () {
            EditMiniApp.linkName = $(this).attr("data-name");
            EditMiniApp.pickId = $(this).attr("data-id");
            EditMiniApp.selectLink(EditMiniApp.linkName);
            $('#goodsCategory').modal("hide");
        });

        //选择分类移动模态框点击选择
        $(".cebian_ul2").on("click", " .file_li", function () {
            $(this).addClass('file_li_active').siblings('.file_li').removeClass('file_li_active');
            EditMiniApp.fileId = $(this).attr("data-id");
        });

        //移动确认按钮点击
        $('body').on('click', '#remove_file_confirm', function () {
            var list = [];
            $('#ImgBox .pin').each(function (index, item) {
                if ($(item).hasClass("rm")) {
                    list.push(Number($(item).attr('data-id')));
                }
            });
            EditMiniApp.adminMoveMaterial(list, EditMiniApp.fileId);
            $('#removeFileModal').modal("hide");
        });

        //查询
        $("#productSearch").on("click", function () {
            var data = {
                "Name": $('#UserName').val(),
                "CateId": $('#CateId').val(),
                "BrandId": $('#BrandId').val(),
            }
            EditMiniApp.projectSelectQuery(data);
        });

        //点击图片侧边栏的li时候
        $("#picGroup").on("click", '.cebian_ul .ImgT', function () {
            var index = $(this).index();
            var _thisId = $(this).attr("data-id");
            $(this).addClass('a_b').siblings('.ImgT').removeClass('a_b');
            EditMiniApp.adminGetMaterialList();
            setTimeout(() => {
                //初始化分页
                if (EditMiniApp.PicTotal > 0) {
                    $.jqPaginator('#pagination2', {
                        totalCounts: EditMiniApp.PicTotal,
                        pageSize: 18,
                        visiblePages: 10,
                        currentPage: 1,
                        prev: '<li class="prev"><a href="javascript:;">&lt;</a></li>',
                        next: '<li class="next"><a href="javascript:;">&gt;</a></li>',
                        page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
                        onPageChange: function (num, type) {
                            EditMiniApp.PicPageIndex = num;
                            EditMiniApp.adminGetMaterialList();
                            $('#p2').text(type + '：' + num);
                        }
                    });
                }
            }, 200);
            // EditMiniApp.adminGetCategoryList();
            // UeditorCreate.QueryMediaList();
        });

        //点击创建分组
        $("#creatTabNameBtn").click(function () {
            if ($("#TabName").val() == "") {
                Common.showErrorMsg("分组名字不能为空");
                return;
            }
            EditMiniApp.adminAddCategory($("#TabName").val());
        });

        //排序方式改变
        $('body').on('change', '#timeOrder_box', function () {
            EditMiniApp.adminGetMaterialList();
            setTimeout(() => {
                if (EditMiniApp.PicTotal > 0) {
                    //初始化分页
                    $.jqPaginator('#pagination2', {
                        totalCounts: EditMiniApp.PicTotal,
                        pageSize: 18,
                        visiblePages: 10,
                        currentPage: 1,
                        prev: '<li class="prev"><a href="javascript:;">&lt;</a></li>',
                        next: '<li class="next"><a href="javascript:;">&gt;</a></li>',
                        page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
                        onPageChange: function (num, type) {
                            EditMiniApp.PicPageIndex = num;
                            EditMiniApp.adminGetMaterialList();
                            $('#p2').text(type + '：' + num);
                        }
                    });
                }
            }, 200);
        });

        //批量删除按钮点击
        $('body').on('click', '.delectAll', function () {
            var list = [];
            $('#ImgBox .pin').each(function (index, item) {
                if ($(item).hasClass("rm")) {
                    list.push(Number($(item).attr('data-id')));
                }
            })
            Common.confirmDialog('确认要删除吗?', function () {
                EditMiniApp.adminBatchDeleteMaterial(list)
            })
        });

        //选择商品表格分页每页显示数据
        $("#pagesize_dropdown").on("change", function () {
            EditMiniApp.projectDectoryQuery();
        });

        //点击地下保存按钮
        $("#j-savePage-app").click(function () {

            var LModules = new Array();
            $("#sortable .ui-state-default").each(function (index, item) {
                console.log($(item).attr("data-dataset"))
                var obj = {};
                obj.type = $(item).attr("data-type");
                obj.sort = index;
                if ($(item).attr("data-dataset") == undefined || $(item).attr("data-dataset") == "") {
                    obj.content = {
                        dataset: ""
                    }
                } else {
                    obj.content = {
                        dataset: JSON.parse($(item).attr("data-dataset"))
                    }
                }
                if (obj.type == 8) {
                    obj.content = {
                        height: $(item).attr("data-height")
                    }
                } else {

                }
                LModules.push(obj);
            });
            console.log(LModules);
            EditMiniApp.AdminSaveHomeConfig(JSON.stringify(LModules));
        });

        $("#diy-ctrl").on("click", ".imgnav", function () {
            EditMiniApp.SelectShopIndex = $(this).parents(".ctrl-item-list-li").index();
        });

        //选择图片确认按钮点击事件
        $("#insert_img_btn").click(function () {

            var _src = $(".rm").eq(0).find("img").attr("src");
            $(".ctrl-item-list-li:eq(" + EditMiniApp.SelectShopIndex + ")").find(".imgnav img").attr("src", _src);
            $('#insertcustom_img').modal("hide");
            if ($("#sortable .selected").attr("data-type") == 1) {
                $(".selected .sweiper").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            } else if ($("#sortable .selected").attr("data-type") == 2) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 3) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .appUl .board5:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 4 || $("#sortable .selected").attr("data-type") == 5) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .board7:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 6) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected .board9:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            } else if ($("#sortable .selected").attr("data-type") == 7) {
                $(".selected .ad_img").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li").eq(0).find(".imgnav img").attr("src"));
            } else if ($("#sortable .selected").attr("data-type") == 9) {
                $(".ctrl-item-list-li").each(function (index, item) {
                    $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", $("#diy-ctrl .ctrl-item-list-li:eq(" + index + ")").find(".imgnav img").attr("src"));
                });
            }

            EditMiniApp.storageOffside();
        });

        //详情input失去焦点
        $("#diy-ctrl").on("blur", ".xlarge,.typeV", function () {
            var thisindex = $(this).parents(".ctrl-item-list-li").index();
            var thisval = $(this).val();
            $(".selected #listMa li:eq(" + thisindex + ")").find(".members_nav1_name").text(thisval);
        });
        $("#diy-ctrl").bind("input propertychange", ".xlarge,.typeV", function () {
            EditMiniApp.storageOffside();
        });

        // 重置
        $("#j-resetToInit").click(function () {
            Common.confirmDialog('还原后您编辑的模板将不能保存，确认还原吗?', function () {
                EditMiniApp.AdminGetDefaultConfig();
            })
        });
    },
    // 初始化布局拖动事件
    sortableInit: function () {
        $("#diy-phone .drag").sortable({
            revert: true,
            placeholder: "drag-highlight",
            stop: function (event, ui) {
                console.log(ui.item)
                ui.item.addClass("selected").siblings(".ui-state-default").removeClass("selected").find("ul");
                $(".diy-conitem").css("border", "none");
                $("#sortable .selected").find(".diy-conitem").css("border", "2px dashed #fa0");
                $(".diy-conitem-action-btns").css("display", "none");
                $("#sortable .selected").find(".diy-conitem-action-btns").css("display", "block");
                var Type = EditMiniApp.Type;
                if ($("#sortable .selected").attr("data-dataset") == "" || $("#sortable .selected").attr("data-dataset") == undefined || JSON.parse($("#sortable .selected").attr("data-dataset")).length == 0) {
                    EditMiniApp.detailTpl(Type);
                } else {
                    EditMiniApp.isSaveData(Type);
                }
            }
        }).disableSelection();
    },
    //初始化辅助空白slider
    sliderInit: function () {
        // With JQuery 使用JQuery 方式调用

        var myslider = $('#ex1').slider({
            formatter: function (value) {
                return 'Current value: ' + value;
            }
        }).on('slide', function (slideEvt) {
            //当滚动时触发
            //console.info(slideEvt);
            //获取当前滚动的值，可能有重复
            // console.info(slideEvt.value);
            $(".selected .custom-space").css("height", slideEvt.value + "px");
            $(".blank_num").html(slideEvt.value + "px");
            $("#sortable .selected").attr("data-height", slideEvt.value);
        }).on('change', function (e) {
            //当值发生改变的时候触发
            //console.info(e);
            //获取旧值和新值
            // console.info(e.value.oldValue + '--' + e.value.newValue);
        });
        var str = $(".selected .custom-space").css("height");
        var reg = new RegExp("px", "g");
        var theValue = Number(str.replace(reg, ""));
        myslider.slider('setValue', theValue);
        $(".blank_num").html(theValue + "px");
        $(".slider-handle").addClass("ui-slider-handle ui-state-default ui-corner-all").css({
            "position": "relative",
            "top": "3px"
        });
    },
    //根据不同类型生成不同的模板
    generateTpl: function (type, flag) {
        $(".diy-ctrl-item").show();
        $(".ui-state-default").removeClass("selected");
        $(".diy-conitem").css("border", "2px dashed transparent");
        if (type == "1") {
            $(EditMiniApp.swiperTpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.swiper_detail_tpl);
        } else if (type == "2") {
            $(EditMiniApp.navigationTpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.navigation_detail_tpl);
        } else if (type == "3") {
            $(EditMiniApp.app1Tpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.app1_modal_tpl);
        } else if (type == "4") {
            $(EditMiniApp.app2Tpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.app2_modal_tpl);
        } else if (type == "5") {
            $(EditMiniApp.app3Tpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.app3_modal_tpl);
        } else if (type == "6") {
            $(EditMiniApp.app4Tpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.app4_modal_tpl);
        } else if (type == "7") {
            $(EditMiniApp.bannerTpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.banner_modal_tpl);
        } else if (type == "8") {
            $(EditMiniApp.blankTpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.blank_modal_tpl);
        } else if (type == "9") {
            $(EditMiniApp.flashSaleTpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.new_sell_detail_tpl);
        } else if (type == "10") {
            $(EditMiniApp.groupBuyingTpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.new_bulk_detail_tpl);
        } else if (type == "11") {
            $(EditMiniApp.newBuyingTpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.new_hot_detail_tpl);
        } else if (type == "12") {
            $(EditMiniApp.hotBuyingTpl).appendTo("#sortable");
            $(".diy-ctrl-item").html(EditMiniApp.new_hot_detail_tpl);
        }
        if (flag == false) {
            return;
        }
        EditMiniApp.detailTpl(type);
    },
    //根据不同类型显示不同的详情
    detailTpl: function (type) {
        $(".diy-ctrl-item").show();
        if (type == "1") {
            $(".diy-ctrl-item").html(EditMiniApp.swiper_detail_tpl);
        } else if (type == "2") {
            $(".diy-ctrl-item").html(EditMiniApp.navigation_detail_tpl);
        } else if (type == "3") {
            $(".diy-ctrl-item").html(EditMiniApp.app1_modal_tpl);
        } else if (type == "4") {
            $(".diy-ctrl-item").html(EditMiniApp.app2_modal_tpl);
        } else if (type == "5") {
            $(".diy-ctrl-item").html(EditMiniApp.app3_modal_tpl);
        } else if (type == "6") {
            $(".diy-ctrl-item").html(EditMiniApp.app4_modal_tpl);
        } else if (type == "7") {
            $(".diy-ctrl-item").html(EditMiniApp.banner_modal_tpl);
        } else if (type == "8") {
            $(".diy-ctrl-item").html(EditMiniApp.blank_modal_tpl);
            EditMiniApp.sliderInit();
        } else if (type == "9") {
            $(".diy-ctrl-item").html(EditMiniApp.new_sell_detail_tpl);
        } else if (type == "10") {
            $(".diy-ctrl-item").html(EditMiniApp.new_bulk_detail_tpl);
        } else if (type == "11") {
            $(".diy-ctrl-item").html(EditMiniApp.new_hot_detail_tpl);
        } else if (type == "12") {
            $(".diy-ctrl-item").html(EditMiniApp.new_hot_detail_tpl);
        } else if (type == false) {
            $(".diy-ctrl-item").hide();
            return;
        }
        EditMiniApp.repositionCtrl();
    },
    // 重置ctrl位置
    repositionCtrl: function (conitem, ctrl) {
        var top_conitem = $("#sortable .selected").offset().top;
        var curPosTop = top_conitem - 170;
        $(".diy-ctrl-item").css("marginTop", curPosTop); //设置位置
        $("html,body").animate({
            scrollTop: curPosTop
        }, 300); //滚动页面
    },
    //判断是否显示上移下移
    UpOrDownShow: function () {
        if ($(".drag .diy-conitem-action-btns").length < 2) {
            $(".drag .j-Down").hide();
            $(".drag .j-Up").hide();
        } else if ($(".drag .diy-conitem-action-btns").length == 2) {
            $(".drag .j-Down").eq(0).show();
            $(".drag .j-Up").eq(0).hide();
            $(".drag .j-Down").eq(1).hide();
            $(".drag .j-Up").eq(1).show();
        } else {
            $(".drag .j-Down").show();
            $(".drag .j-Up").show();
            $(".drag .j-Down").eq(0).show();
            $(".drag .j-Up").eq(0).hide();
            $(".drag .j-Down").eq($(".drag .diy-conitem-action-btns").length - 1).hide();
            $(".drag .j-Up").eq($(".drag .diy-conitem-action-btns").length - 1).show();
        }
    },
    // 替换链接
    replaceLink: function (_this) {
        console.log(_this.parents(".ctrl-item-list-li").index());
        EditMiniApp.SelectShopIndex = _this.parents(".ctrl-item-list-li").index();
        if (EditMiniApp.dataVal == 1) {
            return;
        } else if (EditMiniApp.dataVal == 3) {
            return;
        } else if (EditMiniApp.dataVal == 18) {
            return;
        }
        EditMiniApp.selectLink();
    },
    // 根据dataVal替换不同链接
    selectLink: function (str, selectContent) {

        if (EditMiniApp.dataVal == 1) {
            var Str = '<a data-Val="1" href="/pages/productCenter/goodDetail/goodDetail?PId=' + EditMiniApp.PId + '" class="typeV badge badge-success" title="' + str + '" data-ShowImg="' + EditMiniApp.productShowImgFull + '"><span class="select_title">选择商品</span><em class="badge-link ovfEps">' + str + '</em></a>';
        } else if (EditMiniApp.dataVal == 3) {
            var Str = '<a data-Val="3" href="/pages/productCenter/classify/classify?CateId=' + EditMiniApp.pickId + '" class="typeV badge badge-success" title="' + str + '"><span class="select_title">选择分类</span><em class="badge-link ovfEps">' + str + '</em></a>';
        } else if (EditMiniApp.dataVal == 7) {
            var Str = '<a data-Val="7" href="/pages/memberCenter/member/member" class="typeV badge badge-success" title="会员主页"><span class="select_title">会员主页</span><em class="badge-link ovfEps">会员主页</em></a>';
        } else if (EditMiniApp.dataVal == 8) {
            var Str = '<a data-Val="8" href="/pages/cartCenter/cart/cart" class="typeV badge badge-success" title="购物车"><span class="select_title">购物车</span><em class="badge-link ovfEps">购物车</em></a>';
        } else if (EditMiniApp.dataVal == 10) {
            var Str = '<input data-Val="10" type="text" name="customlink" class="typeV input" placeholder="输入完整的URL(以http://或者https://开头)" value="">';
        } else if (EditMiniApp.dataVal == 13) {
            var Str = '<a data-Val="13" href="/pages/limitCenter/flashsale/flashsale" class="typeV badge badge-success" title="限时抢购"><span class="select_title">限时抢购</span><em class="badge-link ovfEps">限时抢购</em></a>';
        } else if (EditMiniApp.dataVal == 16) {
            var Str = '<a data-Val="16" href="/pages/memberCenter/coupon/coupon" class="typeV badge badge-success" title="优惠劵列表"><span class="select_title">优惠劵列表</span><em class="badge-link ovfEps">优惠劵列表</em></a>';
        } else if (EditMiniApp.dataVal == 18) {
            var Str = '<a data-Val="18" href="/pages/memberCenter/getCoupon/getCoupon" class="typeV badge badge-success" title="' + str + '"><span class="select_title">领取优惠劵</span><em class="badge-link ovfEps">' + str + '</em></a>';
        } else if (EditMiniApp.dataVal == 23) {
            var Str = '<input data-Val="23" type="text" name="customlink" class="typeV input" placeholder="输入格式为：18888888888" value="">';
        } else if (EditMiniApp.dataVal == 26) {
            var Str = '<input data-Val="26" type="text" name="customlink" class="typeV input" placeholder="请输入小程序AppID" value="">';
        }
        $(".ctrl-item-list-li:eq(" + EditMiniApp.SelectShopIndex + ")").find(".droplist span").html(Str);


        EditMiniApp.storageOffside();
    },
    //商品选择bootstrapTable
    selectBootstrapTable: function () {
        $('#productTabel').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/product/AdminProductList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],
            idField: "Id", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: true, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: EditMiniApp.selectQueryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: EditMiniApp.selectResponseHandler,
            columns: [{
                checkbox: true
            },
                {
                    field: 'ShowImgFull',
                    title: '商品图片',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '<image style="width:50px" src="' + value + '">'
                        return html;
                    }
                },
                {
                    field: 'Name',
                    title: '商品名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';
                        return e;
                    }
                },
                {
                    field: 'ShopPrice',
                    title: '商品价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                }
            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function () {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function () { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function (data) {
                console.log(data);

                $('.caret').remove()

            },
            onLoadError: function (data) {
                $('#dishes_list_table').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法
                // console.log(row);
                // console.log(tr);
                // console.log(flied);
            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {
                // for (var i = 0; i < rows.length; i++) {
                //     DishesList.UserIdsList.push(rows[i].User.Id);
                //     DishesList.UserOpenIds.push(rows[i].User.OpenId);
                // }
            },
            onUncheckAll: function (rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function (row) {
                console.log(row)
                EditMiniApp.productName = row.Name;
                EditMiniApp.productShowImgFull = row.ShowImgFull;
                EditMiniApp.PId = row.PId;
            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {


            }
        });
    },
    //bootstrap table post 参数 queryParams
    selectQueryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/product/AdminProductList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "Name": "",
            "CateId": $('#CateId').val(),
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    selectResponseHandler: function (res) {
        if (res.Data != null) {
            // console.log(res.Data);
            return {
                "rows": res.Data.ProductList,
                "total": res.Data.Total
            };
        } else {
            return {
                "rows": [],
                "total": 0
            };
        }
    },
    //表格刷新
    projectSelectQuery: function (parame) {
        if (parame == "" ||
            parame == undefined) {
            var obj = {

                page: {
                    PageSize: $("#pagesize_dropdown").val(),
                    PageIndex: 1
                },
            };
        } else {
            var obj = parame;
        }

        $('#productTabel').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/product/AdminProductList',
                query: obj
            }
        );

    },
    projectDectoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {};
        } else {
            var obj = parame;
        }
        //方法名
        var methodName = "/product/AdminProductList";


        $('#productTabel').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/product/AdminProductList',
                query: obj
            }
        );
        EditMiniApp.selectBootstrapTable()
    },
    //优惠券列表bootstrapTable
    initCouponBootstrapTable: function () {
        $('#couponTabel').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/CouponType/AdminCouponTypeList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 3,
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],
            idField: "Id", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: true, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: EditMiniApp.queryCouponParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbarCoupon", //设置工具栏的Id或者class
            responseHandler: EditMiniApp.responseCouponHandler,
            columns: [{
                checkbox: true
            },
                {
                    field: 'Name',
                    title: '优惠券名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Money',
                    title: '面额',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'RemainCount',
                    title: '剩余张数',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'UseMode',
                    title: '使用条件',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'UseStartTime',
                    title: '有效期',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = value.substring(0, 10) + "至" + row.UseEndTime.substring(0, 10);
                        return html;
                    }
                }
            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function () {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function () { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function (data) {
                console.log(data);

                $('.caret').remove()

            },
            onLoadError: function (data) {
                $('#memberTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法
            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {
                // for (var i = 0; i < rows.length; i++) {
                //     dishes_list.UserIdsList.push(rows[i].User.Id);
                //     dishes_list.UserOpenIds.push(rows[i].User.OpenId);
                // }

            },
            onUncheckAll: function (rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function (row) {
                console.log(row)
                EditMiniApp.couponName = row.Name;
                EditMiniApp.CouponTypeId = row.CouponTypeId;
            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {
                Array.prototype.remove = function (val) {
                    var index = this.indexOf(val);
                    if (index > -1) {
                        this.splice(index, 1);
                    }
                };

            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryCouponParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/CouponType/AdminCouponTypeList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            CouponTypeName: $("#Name").val(),
            State: 0,
            SendMode: 1,
            Page: {
                PageSize: params.limit, //页面大小,
                PageIndex: (params.offset / params.limit) + 1, //页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseCouponHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.CouponTypeList,
                "total": res.Data.Total
            };
        } else {
            return {
                "rows": [],
                "total": 0
            };
        }
    },
    //表格刷新(直接刷新)
    refreshCouponQuery: function (parame) {
        //方法名
        var methodName = "/CouponType/AdminCouponTypeList";

        if (parame == "" || parame == undefined) {
            var obj = {
                CouponTypeName: $("#Name").val(),
                State: 0,
                SendMode: 1,
                Page: {
                    PageSize: 3, //页面大小,
                    PageIndex: 1, //页码
                }
            };
        } else {
            var obj = parame;
        }

        $('#couponTabel').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#tb_limit_content').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/timeproductactivity/AdminTimeProductActivityList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],
            idField: "Id", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: EditMiniApp.limitqueryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: EditMiniApp.limitresponseHandler,
            columns: [{
                field: 'checked',
                checkbox: true,
                formatter: EditMiniApp.stateFormatter
            },
                {
                    field: 'Name',
                    title: '商品名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e = `<span style="display:block;position: relative"><span>${value}</span></span>`

                        return e;
                    }
                },
                {
                    field: 'StartTime',
                    title: '开始时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var startTime = moment(value).format('YYYY-MM-D')

                        var e = `<span>${startTime}</span>`


                        return e;
                    }
                },
                {
                    field: 'EndTime',
                    title: '结束时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var endTime = moment(value).format('YYYY-MM-D')
                        var e = `<span>${endTime}</span>`
                        return e;
                    }
                },
                {
                    field: 'ToSnapUpPrice',
                    title: '抢购价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e = `<span>${value}</span>`
                        return e;
                    }
                }
            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function () {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function () { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function (data) {
                console.log(data);
                $('.caret').remove();
            },
            onLoadError: function (data) {
                $('#dishes_list_table').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法
                // console.log(row);
                // console.log(tr);
                // console.log(flied);
            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {
                console.log(rows)
                // for (var i = 0; i < rows.length; i++) {
                //     DishesList.UserIdsList.push(rows[i].User.Id);
                //     DishesList.UserOpenIds.push(rows[i].User.OpenId);
                // }

            },
            onUncheckAll: function (rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function (row) {
                console.log(row);
            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {
                console.log(row)
            },
        });
    },
    limitqueryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/timeproductactivity/AdminTimeProductActivityList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "State": 0,
            Name: EditMiniApp.name,

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    limitresponseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.TimeProductActivityList,
                "total": res.Data.Total
            };
        } else {
            return {
                "rows": [],
                "total": 0
            };
        }
    },
    initGroupTable: function () {
        $('#GroupBox').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/groupbuying/AdminGroupBuyActivitiList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],
            idField: "Id", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: EditMiniApp.GroupqueryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: EditMiniApp.GroupresponseHandler,
            columns: [{
                field: 'checked',
                checkbox: true,
                formatter: EditMiniApp.stateFormatter
            }, {
                field: 'Name',
                title: '商品',
                align: 'center',
                valign: 'middle',
                formatter: function (value, row, index) {
                    console.log(value, row, index)
                    var e = `<span style="display:block;position: relative"><span>${row.Name}</span></span>`
                    return e;
                }
            },
                {
                    field: 'StartTimeStr',
                    title: '活动时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = `<p>开始时间:${row.StartTimeStr}</p>
                                <p>结束时间:${row.EndTimeStr}</p>`
                        return html;
                    }
                },
                {
                    field: 'Number',
                    title: '成团人数',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return row.Number;
                    }
                },
                {
                    field: 'Stock',
                    title: '库存',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return row.Stock;
                    }
                },
                {
                    field: 'Price',
                    title: '当前价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return row.Price;
                    }
                },
                {
                    field: 'GroupPrice',
                    title: '团购格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return row.GroupPrice;
                    }
                }
            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function () {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function () { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function (data) {
                console.log(data);
                $('.caret').remove();
            },
            onLoadError: function (data) {
                $('#dishes_list_table').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法
                // console.log(row);
                // console.log(tr);
                // console.log(flied);
            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {
                console.log(rows)
                // for (var i = 0; i < rows.length; i++) {
                //     DishesList.UserIdsList.push(rows[i].User.Id);
                //     DishesList.UserOpenIds.push(rows[i].User.OpenId);
                // }

            },
            onUncheckAll: function (rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function (row) {
                console.log(row);
            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {
                console.log(row)
            },
        });
    },
    GroupqueryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/groupbuying/AdminGroupBuyActivitiList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            "productsname": $('#GroupName').val(),
            Page: {
                PageSize: params.limit, //页面大小,
                PageIndex: (params.offset / params.limit) + 1, //页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    GroupresponseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.List,
                "total": res.Data.Total
            };
        } else {
            return {
                "rows": [],
                "total": 0
            };
        }
    },
    newHotShopGroupTable: function () {
        $('#newHotTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/product/AdminProductList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],
            idField: "Id", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: EditMiniApp.newHotqueryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: EditMiniApp.newHotresponseHandler,
            columns: [{
                field: 'checked',
                checkbox: true,
                formatter: EditMiniApp.stateFormatter
            },
                // {
                //     field: 'ShowImg',
                //     title: '',
                //     align: 'center',
                //     valign: 'middle',
                //     formatter: function (value, row, index) {
                //         var html = '<image class="checkbox" src="' + value + '">'
                //         return html;
                //     }
                // },
                {
                    field: 'ShowImgFull',
                    title: '商品图片',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '<image style="width:50px" src="' + value + '">'
                        return html;
                    }
                },
                {
                    field: 'Name',
                    title: '商品名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';
                        return e;
                    }
                },
                {
                    field: 'ShopPrice',
                    title: '商品价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                }
            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function () {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function () { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function (data) {
                console.log(data);
                $('.caret').remove();
            },
            onLoadError: function (data) {
                $('#dishes_list_table').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法
                // console.log(row);
                // console.log(tr);
                // console.log(flied);
            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {
                console.log(rows)
                // for (var i = 0; i < rows.length; i++) {
                //     DishesList.UserIdsList.push(rows[i].User.Id);
                //     DishesList.UserOpenIds.push(rows[i].User.OpenId);
                // }

            },
            onUncheckAll: function (rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function (row) {
                console.log(row);
            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {
                console.log(row)
            },
        });
    },
    newHotqueryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/product/AdminProductList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "Name": "",
            "CateId": 0,
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    newHotresponseHandler: function (res) {
        if (res.Data != null) {
            // console.log(res.Data);
            return {
                "rows": res.Data.ProductList,
                "total": res.Data.Total
            };
        } else {
            return {
                "rows": [],
                "total": 0
            };
        }
    },
    //后台分类列表
    adminCategoryList: function () {
        //请求方法
        var methodName = "/category/AdminIndexFirstCategoryList";
        var data = {};
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(EditMiniApp.GoodsListTemplate);
                var html = render(data.Data);
                $('#table_content_classify').html(html);
                EditMiniApp.initHandle();
                EditMiniApp.getProductBrand();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //初始化事件
    initHandle: function () {
        //文件收缩  第一层级
        $('.layer_ul_1 .layer-1 .jia_collapse_one').click(function () {
            //$(this).parents('.contentBody').siblings('.layer_ul_2').slideToggle();
            var display_state = $(this).parents('.contentBody').siblings('.layer_ul_2').css('display'); //所以要用行内样式控制
            if (display_state == 'none') {
                $(this).parents('.contentBody').siblings('.layer_ul_2').slideDown();
                $(this).find('img').attr('src', '../public/images/zhankai.png');
            } else {
                $(this).parents('.contentBody').siblings('.layer_ul_2').slideUp();
                $(this).find('img').attr('src', '../public/images/suoqi.png');
            }
        });

        //文件收缩 第二层级
        $('.layer_ul_2 .layer-2 .jia_collapse_two').click(function () {
            var display_state = $(this).parents('.contentBody').siblings('.layer_ul_3').css('display'); //所以要用行内样式控制
            if (display_state == 'none') {
                $(this).parents('.contentBody').siblings('.layer_ul_3').slideDown();
                $(this).find('img').attr('src', '../public/images/zhankai.png');
            } else {
                $(this).parents('.contentBody').siblings('.layer_ul_3').slideUp();
                $(this).find('img').attr('src', '../public/images/suoqi.png');
            }
        });


    },
    // 获取商品品牌
    getProductBrand: function () {
        var methodName = "/brand/AdminBrandList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(EditMiniApp.brandTpl);
                var html = render(data.Data);
                $("#BrandId").append(html);

                EditMiniApp.getProductLabel();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取商品标签
    getProductLabel: function () {
        var methodName = "/productLabel/AdminProductLabelList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(EditMiniApp.labelTpl);
                var html = render(data.Data);
                $("#PlIdList").html(html);

                var PId = Common.getUrlParam("PId");
                if (PId != undefined && PId != "") {
                    EditMiniApp.getProductInfo(PId);
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取商品信息
    getProductInfo: function (PId) {
        var methodName = "/product/AdminProductInfo";
        var data = {
            PId: PId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var result = data.Data.ProductInfo;
                $("#classifyName").attr("data-id", result.CateId);
                $("#classifyName").text(result.CateName);
                $("#BrandId").val(result.BrandId);
                $("#Name").val(result.Name);
                $("#Summary").val(result.Summary);
                $("#TemplateId").val(result.TemplateId);
                $("#MarketPrice").val(result.MarketPrice);
                $("#ShopPrice").val(result.ShopPrice);
                $("#CostPrice").val(result.CostPrice);
                $("#PSn").val(result.PSn);
                $("#Number").val(result.Number);
                $("#Limit").val(result.Limit);

                // 状态
                var stateList = $("#State label");
                for (var i = 0; i < stateList.length; i++) {
                    if (stateList.eq(i).find("input").val() == result.State) {
                        stateList.eq(i).find("input").prop("checked", true);
                    }
                }

                // 标签
                var labelList = $("#PlIdList label");
                for (var i = 0; i < result.PlIdList.length; i++) {
                    for (var j = i; j < labelList.length; j++) {
                        if (labelList.eq(j).find("input").val() == result.PlIdList[i]) {
                            labelList.eq(j).find("input").prop("checked", true);
                        }
                    }
                }

                var imgArr = [];
                for (var i = 0; i < result.Img.length; i++) {
                    var imgData = {
                        Img: result.Img[i],
                        ImgFull: result.ImgFull[i]
                    };
                    imgArr.push(imgData);
                }
                var imgList = {
                    imglistData: imgArr
                };
                var render = template.compile(EditMiniApp.imgTpl);
                var html = render(imgList);
                $("#productImg").html(html);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取商品分类
    getProductCategory: function () {
        var methodName = "/category/AdminIndexFirstCategoryList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(EditMiniApp.cateTpl);
                var html = render(data.Data);
                $("#CateId").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //查询文件夹列表
    adminGetCategoryList: function () {
        var methodName = "/Material/AdminGetCategoryList";
        var data = {
            "Page": {
                "PageSize": 100,
                "PageIndex": 1
            }
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render1 = template.compile(EditMiniApp.cateBoxTemplate);
                var html1 = render1(data.Data);
                $('.cebian_ul').html(html1);
                var render2 = template.compile(EditMiniApp.moveBoxTemplate);
                var html2 = render2(data.Data);
                $('.cebian_ul2').html(html2);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //创建文件夹接口
    adminAddCategory: function (name) {
        var methodName = "/Material/AdminAddCategory";
        var data = {
            "Name": name
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功', function () {
                    $("#addGrouping").modal("hide");
                    EditMiniApp.adminGetCategoryList();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //确认上传素材
    adminSaveMaterial: function (list) {
        var methodName = "/Material/AdminSaveMaterial";
        var data = {
            "TempImageList": list,
            "CategoryId": $(".a_b").attr("data-id")
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("上传成功!", function () {
                    // $('#uploaderPicModal').modal('hide')
                    // location.reload()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取素材库列表
    adminGetMaterialList: function () {
        var methodName = "/Material/AdminGetMaterialList";
        var data = {
            "FileName": $(".ipt_search_message").val(),
            "CategoryId": $(".a_b").attr("data-id"),
            "DisplayOrder": $("#timeOrder_box option:selected").attr("data-value"),
            "IsAsc": $("#timeOrder_box option:selected").attr("data-type"),
            "Page": {
                "PageSize": 18,
                "PageIndex": EditMiniApp.PicPageIndex
            }
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                EditMiniApp.PicTotal = data.Data.Total;
                var render = template.compile(EditMiniApp.picBoxTemplate);
                var html = render(data.Data);
                $('#ImgBox').html(html);
                uploadPic_toLibrary2('#FilePicker');
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //批量删除素材接口
    adminBatchDeleteMaterial: function (list) {
        var methodName = "/Material/AdminBatchDeleteMaterial";
        var data = {
            "Ids": list
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("删除成功!", function () {
                    EditMiniApp.adminGetMaterialList();
                    setTimeout(() => {
                        if (EditMiniApp.PicTotal > 0) {
                            //初始化分页
                            $.jqPaginator('#pagination2', {
                                totalCounts: EditMiniApp.PicTotal,
                                pageSize: 18,
                                visiblePages: 10,
                                currentPage: 1,
                                prev: '<li class="prev"><a href="javascript:;">&lt;</a></li>',
                                next: '<li class="next"><a href="javascript:;">&gt;</a></li>',
                                page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
                                onPageChange: function (num, type) {
                                    EditMiniApp.PicPageIndex = num;
                                    EditMiniApp.adminGetMaterialList();
                                    $('#p2').text(type + '：' + num);
                                }
                            });
                        }
                    }, 200);
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //移动素材
    adminMoveMaterial: function (cateList, cateid) {
        var methodName = "/Material/AdminMoveMaterial";
        var data = {
            "Ids": cateList,
            "CategoryId": cateid,
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("移动成功!", function () {
                    EditMiniApp.adminGetMaterialList();
                    setTimeout(() => {
                        if (EditMiniApp.PicTotal > 0) {
                            //初始化分页
                            $.jqPaginator('#pagination2', {
                                totalCounts: EditMiniApp.PicTotal,
                                pageSize: 18,
                                visiblePages: 10,
                                currentPage: 1,
                                prev: '<li class="prev"><a href="javascript:;">&lt;</a></li>',
                                next: '<li class="next"><a href="javascript:;">&gt;</a></li>',
                                page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
                                onPageChange: function (num, type) {
                                    EditMiniApp.PicPageIndex = num;
                                    EditMiniApp.adminGetMaterialList();
                                    $('#p2').text(type + '：' + num);
                                }
                            });
                        }
                    }, 200);
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 储存每个对象的方法
    storageOffside: function () {

        var dataset = new Array();
        $("#diy-ctrl .ctrl-item-list-li").each(function (index, item) {
            var obj = {};
            obj.Type = $(item).find(".droplist .typeV").attr("data-val");
            if (obj.Type == undefined) {
                obj.Type = "";
                obj.Title = $(item).find(".xlarge").val();
            } else if (obj.Type == 10 || obj.Type == 23 || obj.Type == 26) {
                obj.link = $(item).find(".droplist .typeV").val();
                obj.Title = $(item).find(".xlarge").val();
            } else {
                obj.link = $(item).find(".droplist .typeV").attr("href");
                obj.Title = $(item).find(".xlarge").val();
                obj.showtitle = $(item).find(".droplist .typeV").find(".badge-link").text();
            }
            obj.pic = $(item).find(".imgnav img").attr("src");
            dataset.push(obj);
        });
        console.log(dataset);
        if ($("#sortable .selected").attr("data-type") == "9" || $("#sortable .selected").attr("data-type") == "10" || $("#sortable .selected").attr("data-type") == "11" || $("#sortable .selected").attr("data-type") == "12") {
            dataset = EditMiniApp.datasetArray;
        }
        // console.log(JSON.stringify(dataset));
        $("#sortable .selected").attr("data-dataset", JSON.stringify(dataset));
    },
    // 保存
    AdminSaveHomeConfig: function (obj) {
        var methodName = "/homeconfig/AdminSaveHomeConfig";
        var data = {
            Title: "",
            Type: 2,
            Desc: "",
            ShareIcon: "",
            ModulesData: obj
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("保存成功!", function () {
                    location.reload();
                });

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取信息
    AdminGetHomeConfig: function () {
        var methodName = "/homeconfig/AdminGetHomeConfig";
        var data = {
            Type: 2
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            console.log(data);
            if (data.Code == "100") {
                var obj = JSON.parse(data.Data.ModulesData);
                console.log(obj);
                if (obj.length == 0) {
                } else {
                    $("#sortable").html("");
                }
                $.each(obj, function (index, item) {
                    EditMiniApp.generateTpl(item.type, false);
                    if (item.content.dataset == "") {

                    } else {
                        if (item.type == 1) {
                            $(".selected .sweiper").find("img").attr("src", item.content.dataset[0].pic);
                        } else if (item.type == 2) {
                            $.each(item.content.dataset, function (index, item) {
                                EditMiniApp.datasetIndex = index;
                                $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", item.pic);
                                $(".selected #listMa li:eq(" + index + ")").find(".members_nav1_name").text(item.Title);
                            });
                            console.log(EditMiniApp.datasetIndex);
                            if (EditMiniApp.datasetIndex == 0) {
                                $(".selected .lisw4").css("width", "99%");
                                $(".selected .lisw4:eq(1)").remove();
                                $(".selected .lisw4:eq(2)").remove();
                                $(".selected .lisw4:eq(3)").remove();
                            } else if (EditMiniApp.datasetIndex == 1) {
                                $(".selected .lisw4").css("width", "49.8%");
                                $(".selected .lisw4:eq(2)").remove();
                                $(".selected .lisw4:eq(3)").remove();
                            } else if (EditMiniApp.datasetIndex == 2) {
                                $(".selected .lisw4").css("width", "33.2%");
                                $(".selected .lisw4:eq(3)").remove();
                            } else if (EditMiniApp.datasetIndex == 3) {
                                $(".selected .lisw4").css("width", "25%");
                            }
                        } else if (item.type == 3) {
                            $.each(item.content.dataset, function (index, item) {
                                $(".selected .appUl .board5:eq(" + index + ")").find("img").attr("src", item.pic);
                            });
                        } else if (item.type == 4 || item.type == 5) {
                            $.each(item.content.dataset, function (index, item) {
                                $(".selected .board7:eq(" + index + ")").find("img").attr("src", item.pic);
                            });
                        } else if (item.type == 6) {
                            $.each(item.content.dataset, function (index, item) {
                                $(".selected .board9:eq(" + index + ")").find("img").attr("src", item.pic);
                            });
                        } else if (item.type == 7) {
                            $(".selected .ad_img").find("img").attr("src", item.content.dataset[0].pic);
                        } else if (item.type == 8) {
                            $(".selected .custom-space").css("height", item.content.height + "px");
                        } else if (item.type == 9) {
                            $.each(item.content.dataset, function (index, item) {
                                $(".selected #listMa li:eq(" + index + ")").find("img").attr("src", item.Img);
                                $(".selected #listMa li:eq(" + index + ")").find(".money").text("￥" + item.ToSnapUpPrice);
                            });
                        } else if (item.type == 10) {
                            var render = template.compile(EditMiniApp.groupBuyingTpl2);
                            var html = render(item.content);
                            $("#sortable .selected").html(html);
                            $(".diy-conitem-action-btns").css("display", "none");
                            $(".diy-conitem").css("border", "2px dashed transparent");
                        } else if (item.type == 11) {
                            var render = template.compile(EditMiniApp.newBuyingTpl2);
                            var html = render(item.content);
                            $("#sortable .selected").html(html);
                            $(".diy-conitem-action-btns").css("display", "none");
                            $(".diy-conitem").css("border", "2px dashed transparent");
                        } else if (item.type == 12) {
                            var render = template.compile(EditMiniApp.hotBuyingTpl2);
                            var html = render(item.content);
                            $("#sortable .selected").html(html);
                            $(".diy-conitem-action-btns").css("display", "none");
                            $(".diy-conitem").css("border", "2px dashed transparent");
                        }
                    }

                    $(".diy-ctrl-item").css("display", "none");
                });
                $("#sortable .ui-state-default").each(function (index, item) {
                    console.log(item);
                    $(item).attr("data-dataset", JSON.stringify(obj[index].content.dataset));
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //还原初始模板
    AdminGetDefaultConfig: function () {
        var methodName = "/homeconfig/AdminGetDefaultConfig";
        var data = {
            Type: 2
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            console.log(data);
            if (data.Code == "100") {
                EditMiniApp.AdminSaveHomeConfig(data.Data.ModulesData);
                window.location.reload();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //判断是否已经存储数据来生成详情
    isSaveData: function (type) {
        $(".diy-ctrl-item").show();
        var obj = {
            dataset: JSON.parse($("#sortable .selected").attr("data-dataset"))
        }
        console.log(obj);
        if (type == "1") {
            var render = template.compile(EditMiniApp.swiper_detail_tpl2);
            var html = render(obj);
            $(".diy-ctrl-item").html(html);
            if (obj.dataset.length > 5) {
                $(".ctrl-item-list-add").hide();
            } else {
                $(".ctrl-item-list-add").show();
            }
        } else if (type == "2") {
            var render = template.compile(EditMiniApp.navigation_detail_tpl2);
            var html = render(obj);
            $(".diy-ctrl-item").html(html);
            console.log(obj.dataset.length)
            if (obj.dataset.length > 3) {
                $(".ctrl-item-list-add").hide();
            } else {
                $(".ctrl-item-list-add").show();
            }
        } else if (type == "3") {
            var render = template.compile(EditMiniApp.app1_modal_tpl2);
            var html = render(obj);
            $(".diy-ctrl-item").html(html);
        } else if (type == "4") {
            var render = template.compile(EditMiniApp.app1_modal_tpl2);
            var html = render(obj);
            $(".diy-ctrl-item").html(html);
        } else if (type == "5") {
            var render = template.compile(EditMiniApp.app1_modal_tpl2);
            var html = render(obj);
            $(".diy-ctrl-item").html(html);
        } else if (type == "6") {
            var render = template.compile(EditMiniApp.app1_modal_tpl2);
            var html = render(obj);
            $(".diy-ctrl-item").html(html);
        } else if (type == "7") {
            var render = template.compile(EditMiniApp.app1_modal_tpl2);
            var html = render(obj);
            $(".diy-ctrl-item").html(html);
        } else if (type == "8") {
            $(".diy-ctrl-item").html(EditMiniApp.blank_modal_tpl);
            EditMiniApp.sliderInit();
        } else if (type == "9") {
            $(".diy-ctrl-item").html(EditMiniApp.new_sell_detail_tpl);
        } else if (type == "10") {
            $(".diy-ctrl-item").html(EditMiniApp.new_bulk_detail_tpl);
        } else if (type == "11") {
            $(".diy-ctrl-item").html(EditMiniApp.new_hot_detail_tpl);
        } else if (type == "12") {
            $(".diy-ctrl-item").html(EditMiniApp.new_hot_detail_tpl);
        } else if (type == false) {
            $(".diy-ctrl-item").hide();
            return;
        }
        EditMiniApp.repositionCtrl();
    },
    // 替换链接辅助函数
    replaceToLink: function (str) {
        console.log(str)
        if (str.Type == 1) {
            var Str = '<a data-val="' + str.Type + '" href="' + str.link + '" class="typeV badge badge-success" title="' + str.Title + '"><span class="select_title">选择商品</span><em class="badge-link ovfEps">' + str.showtitle + '</em></a>';
        } else if (str.Type == 3) {
            var Str = '<a data-val="' + str.Type + '" href="' + str.link + '" class="typeV badge badge-success" title="' + str.Title + '"><span class="select_title">选择分类</span><em class="badge-link ovfEps">' + str.showtitle + '</em></a>';
        } else if (str.Type == 7) {
            var Str = '<a data-val="' + str.Type + '" href="' + str.link + '" class="typeV badge badge-success" title="' + str.Title + '"><span class="select_title">会员主页</span><em class="badge-link ovfEps">会员主页</em></a>';
        } else if (str.Type == 8) {
            var Str = '<a data-val="' + str.Type + '" href="' + str.link + '" class="typeV badge badge-success" title="' + str.Title + '"><span class="select_title">购物车</span><em class="badge-link ovfEps">购物车</em></a>';
        } else if (str.Type == 10) {
            var Str = '<input data-Val="' + str.Type + '" type="text" name="customlink" class="typeV input" placeholder="输入完整的URL(以http://或者https://开头)" value="' + str.link + '">';
        } else if (str.Type == 13) {
            var Str = '<a data-val="' + str.Type + '" href="' + str.link + '" class="typeV badge badge-success" title="' + str.Title + '"><span class="select_title">限时抢购</span><em class="badge-link ovfEps">限时抢购</em></a>';
        } else if (str.Type == 16) {
            var Str = '<a data-val="' + str.Type + '" href="' + str.link + '" class="typeV badge badge-success" title="' + str.Title + '"><span class="select_title">优惠劵列表</span><em class="badge-link ovfEps">优惠劵列表</em></a>';
        } else if (str.Type == 18) {
            var Str = '<a data-val="' + str.Type + '" href="' + str.link + '" class="typeV badge badge-success" title="' + str.Title + '"><span class="select_title">领取优惠劵</span><em class="badge-link ovfEps">' + str.showtitle + '</em></a>';
        } else if (str.Type == 23) {
            var Str = '<input data-Val="' + str.Type + '" type="text" name="customlink" class="typeV input" placeholder="输入格式为：18888888888" value="' + str.link + '">';
        } else if (str.Type == 26) {
            var Str = '<input data-Val="' + str.Type + '" type="text" name="customlink" class="typeV input" placeholder="请输入小程序AppID" value="' + str.link + '">';
        }
        if (Str == undefined) {
            return "请选择"
        }
        return Str;
    },
    stateFormatter: function (value, row, index) {
        if (row.state == true)
            return {
                disabled: true, //设置是否可用
                checked: true //设置选中
            };
        return value;
    }
}
$(function () {
    EditMiniApp.init();
});