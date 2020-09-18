var orderListDetail = {
    DetailTemplate: `<div class="main_list_content type_box">
        <div style="margin-bottom:15px"><a class="backBtn"><img src="/public/images/back.png" alt=""><span>返回</span></a></div>

        <div class="list_title_header" style="float:left;width:100%">

            <div class="">
                <div class="head-type">
                    <ul class="nav nav-tabs">
                        <li class="active">
                            <a data-type="1" href="javascript:void(0);">订单详情</a>
                        </li>
                    </ul>
                </div>
             <!--物流跟踪-->
                <div class="logistics_box">
                    <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">物流跟踪</div>
                    <div class="logistics_dv_detail">
                        <div style="margin-bottom:10px;">
                            <span style="display:inline-block;margin-right:20px;">快递公司：{{OrderInfo.ShipSystemName}}</span>
                            <span>快递单号：{{OrderInfo.ShipSn}}</span>
                        </div>
                        {{if OrdersLogisticsInfo.Logistics.data}}
                            <ul class="listBox">
                                {{each OrdersLogisticsInfo.Logistics.data as value i}}
                                    {{if i == "0"}}
                                        <li class="first_info">
                                            <span class="zhouji">{{OrdersLogisticsInfo.Logistics.data[i].ftime}}</span>
                                            <span class="state_logistic">{{OrdersLogisticsInfo.Logistics.data[i].context}}</span>
                                        </li>
                                    {{else}}
                                        <li>
                                            <span class="zhouji">{{OrdersLogisticsInfo.Logistics.data[i].ftime}}</span>
                                            <span class="time_point">{{OrdersLogisticsInfo.Logistics.data[i].time}}</span>
                                            <span class="state_logistic">{{OrdersLogisticsInfo.Logistics.data[i].context}}</span>
                                        </li>
                                    {{/if}}
                                {{/each}}
                            </ul>
                        {{else}}
                            <div style="margin-bottom:20px;">{{OrdersLogisticsInfo.Logistics.message}}</div>
                        {{/if}}
                        
                    </div>
                </div>
                <!--物流跟踪end-->
                <!-- 买家信息盒子 -->
                <div class="buyer_info_big_box">
                        <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">买家信息</div>
                        <div class="buyer_info_box">
                            <ul class="buyer_info_one">
                                <li>会员名：{{OrderInfo.Consignee}}</li>
                                <li>收货人电话：{{OrderAddressInfo.Mobile}}</li>
                                <li>收货人：{{OrderAddressInfo.Consignee}}</li>
                            </ul>
                            <ul class="receive_info">
                                <li>
                                    <span style="">收货信息：</span>
                                    <span style="">
                                       {{OrderAddressInfo.Address}}
                                    </span>
                                </li>
                            </ul>
                            <ul class="send_time">
                                <li>送货时间：时间不限</li>
                            </ul>
                            <ul class="mark_info">
                                <li>买家备注：{{OrderInfo.BuyerRemark}}</li>
                            </ul>
                     
                        </div>
        
                   
                        <!-- 订单信息列表 -->
                        <!-- 表格内容 -->
                        <div class="box-body no-padding" style="border-bottom: 2px solid #ccc;">
                            <table class="table" id="table_order">
                                <tbody>
                                <tr style="background-color: #f8f8f8">
                                    <th style="">商品名称</th>
                                    <th>商品单价(元)</th>
                                    <th>购买数量</th>
                                    <th>运费</th>
                                    <!--<th>操作</th>-->
                                   
                                </tr>
                                <tr>
                                    <td>
                                        <div class="media">
                                            <div class="media-left">
                                                <a href="#">
                                                    <img src="{{CouponInfo.CouponImg}}" class="media-object" style="width:80px;border-radius: 6px" alt="..."></a>
                                            </div>
                                            <div class="media-body" style="width:auto;vertical-align: middle;">
                                                <h4 class="media-heading" style="color:#1792e7;font-size: 14px;">{{CouponInfo.Name}}</h4>
                                                {{each CouponInfo.ContentList as value j }}
                                                    <div class="detail_desc_pic" style="font-size: 14px;color:#666">
                                                       {{CouponInfo.ContentList[j].CouponContent}}
                                                    </div>
                                                {{/each}}
                                            </div>
                                        </div>
                                    </td>
                                    <td>¥{{CouponInfo.Money}}</td>
                                    <td>1</td>
                                     <td>{{OrderInfo.ShipFee}}</td>
                                </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- 表格内容end -->
                        <div class="order_number_box" style="overflow: hidden;margin-top: 20px;">
                            <ul class="col-md-6 number_ul dingdan_xinxi" style="padding:0px">
                                <li>订单编号：{{OrderInfo.OSn}}</li>
                            </ul>
                            <ul class="col-md-6 total_ul" style="padding:0px">
                                <li>运费：¥{{OrderInfo.ShipFee}}</li>
                                <li> 支付金额：¥{{OrderInfo.SurplusMoney}}</li>
                                <li style="max-width:200px;">
                                    <hr style="border-bottom:1px solid #ccc;margin:0px -20px 0px;">
                                    <div class="has_receive" style="margin-top: 10px">订单实收款：{{OrderInfo.OrderAmount}}元</div>
                                </li>
                            </ul>
                        </div>

                </div>
                <!-- 买家信息盒子 end-->



            </div>
        </div>
    </div>`,
    init: function () {
        orderListDetail.adminOrderInfo();
        // 返回上一页
        $('body').on('click', '.backBtn', function () {
            if (Common.getUrlParam('isSee')) {
                window.history.go(-3);
            } else {
                window.history.go(-1);
            }
        })
        //切换效果
        $('body').on('click', '.img_toggle', function () {
            $(this).parents().next(".Box").slideToggle();
            $(this).children().toggle();
        })
        //点击查看物流进入详情
        $('body').on('click', '.logisticsBtn', function () {
            var oid = $(this).attr('data-oid');
            var olid = $(this).attr('data-olid');
            location.href = '/order/logistics?oid=' + oid + '&olid=' + olid + ''
        })
        //预览按钮点击
        $('body').on('click', '.status_num', function () {
            var id = $(this).attr('data-id');
            $('.pmask').show();
            $('#productPreBox').show();
            $('#preIframe').attr('src', '' + SignRequest.urlPrefixMobile + '/productdetail?PId=' + id + '')
        })
        //点击遮罩
        $('body').on('click', '#productPreBox', function () {
            $('.pmask').hide();
            $('#productPreBox').hide();
        })
    },
    //订单信息
    adminOrderInfo: function () {
        var methodName = "/order/AdminCouponsOrderInfo";
        var data = {
            "oid": Common.getUrlParam('id'),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(orderListDetail.DetailTemplate);
                var html = render(data.Data);
                $("#all_box").append(html);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};

$(function () {

    orderListDetail.init()

})