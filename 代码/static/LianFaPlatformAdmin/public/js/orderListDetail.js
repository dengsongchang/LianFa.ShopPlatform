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
                {{if OrderLogisticsList}}
                <div class="logistics_box">
                    <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">物流信息</div>
                    
                    <div class="allBox">
                    <!-- 包裹循环体 -->
                    {{each OrderLogisticsList.OrderLogisticsProductList as value i}}
                      <div class="box-header">
                        <div style="font-size:14px;margin-bottom:10px">
                                快递公司名称:{{OrderLogisticsList.OrderLogisticsProductList[i].ShipFriendName}}               
                        </div>
                        <h3 class="box-title" style="font-size:14px">
                                快递单号:{{OrderLogisticsList.OrderLogisticsProductList[i].ShipSn}}               
                        </h3>
                        <div class="pull-right img_toggle">
                           <img src="/public/images/icon_fold.png" class="icon_fold" style="display: inline;">
                           <img src="/public/images/icon_down.png" class="icon_down" style="display: none;">
                        </div>
                      </div>
                    <!-- 循环体 -->
                    <!-- 表格内容 -->
                    <div class="Box">
                        <div class="box-body no-padding " style="border-bottom: 2px solid #ccc;margin-bottom:20px;">
                            <table class="table" id="table_order">
                                <tbody>
                                <tr style="background-color: #f8f8f8">
                                    <th style="">商品名称</th>                                                                
                                    <th>数量</th>
                                </tr>
                                {{each OrderLogisticsList.OrderLogisticsProductList[i].OrderProductList as value j}}
                                    <tr>
                                        <td>
                                            <div class="media">
                                                <div class="media-left">
                                                    <a href="#">
                                                        <img src="{{OrderLogisticsList.OrderLogisticsProductList[i].OrderProductList[j].ShowImg}}" class="media-object" style="width:80px;border-radius: 6px" alt="..."></a>
                                                </div>
                                                <div class="media-body" style="width:auto;vertical-align: middle;">
                                                    <h4 class="media-heading" style="color:#1792e7;font-size: 14px;">{{OrderLogisticsList.OrderLogisticsProductList[i].OrderProductList[j].ProdutcsName}}</h4>
                                                    <div class="detail_desc_pic" style="font-size: 14px;color:#666">
                                                       {{OrderLogisticsList.OrderLogisticsProductList[i].OrderProductList[j].Summary}}
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                        <td>{{OrderLogisticsList.OrderLogisticsProductList[i].OrderProductList[j].Number}}</td>
    
                                    </tr>
                                {{/each}}
                                </tbody>
                            </table>
                        </div>
                        <!-- 表格内容end -->
                        <!-- 循环体end -->
                        <div class="logisticsBox">
                           <div class="logisticsBtn" data-oid="{{OrderLogisticsList.OId}}" data-olid="{{OrderLogisticsList.OrderLogisticsProductList[i].OLId}}">
                               查看物流
                           </div>
                        </div>
                    </div>
                    {{/each}}
                    <!-- 包裹循环体end -->
                    </div>
                    
                    
                    
                </div>
                {{/if}}
                <!--物流跟踪end-->
                <!--门店信息-->
                {{if OrderStoresInfo != "" && OrderStoresInfo != null && OrderStoresInfo != undefined}}
                <div class="buyer_info_big_box">
                        <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">自提门店信息</div>
                       {{if TakeOutOrderInfo.length > 0  }}
                        <div class="buyer_info_box">
                            <ul class="send_time">
                                <li>门店名称：{{OrderStoresInfo.StoreName}}</li>
                            </ul>
                            <ul class="receive_info">
                                <li>
                                    <span style="">店铺编码：</span>
                                    <span style="">
                                        {{OrderStoresInfo.SN}}
                                    </span>
                                </li>
                            </ul>
                            <ul class="send_time">
                                <li>联系方式：{{OrderStoresInfo.ContactsPhone}}</li>
                            </ul>
                            <ul class="mark_info">
                                <li>所在位置：{{OrderStoresInfo.Address}}</li>
                            </ul>
                        </div>
                        {{/if}}
                                           
                </div>
                {{/if}}
                <!-- 门店信息end -->
                <!-- 买家信息盒子 -->
                <div class="buyer_info_big_box">
                        <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">买家信息</div>
                       {{if TakeOutOrderInfo.length > 0  }}
                        <div class="buyer_info_box">
                            <ul class="buyer_info_one">
                                <li>会员名：{{TakeOutOrderInfo[0].UseName}}</li>
                                <li>收货人电话：{{TakeOutOrderInfo[0].Mobile}}</li>
                                <li>收货人：{{TakeOutOrderInfo[0].NickName}}</li>
                            </ul>
                            <ul class="receive_info">
                                <li>
                                    <span style="">收货信息：</span>
                                    <span style="">
                                        {{TakeOutOrderInfo[0].Address}}
                                    </span>
                                </li>
                            </ul>
                            <ul class="send_time">
                                <li>送货时间：时间不限</li>
                            </ul>
                            <ul class="mark_info">
                                <li>买家备注：{{TakeOutOrderInfo[0].BuyerRemark}}</li>
                            </ul>
                     
                        </div>
                        {{/if}}
                   
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
                                    <th>小计(元)</th>
                                    <!--<th>操作</th>-->
                                   
                                </tr>
                                {{each OrderProductList as value i}}
                                <tr>
                                    <td>
                                        <div class="media">
                                            <div class="media-left">
                                                <a href="#">
                                                    <img src="{{OrderProductList[i].ShowImg}}" class="media-object" style="width:80px;border-radius: 6px" alt="..."></a>
                                            </div>
                                            <div class="media-body" style="width:auto;vertical-align: middle;">
                                                <h4 class="media-heading" style="color:#1792e7;font-size: 14px;">{{OrderProductList[i].ProductsName}}</h4>
                                                <div class="detail_desc_pic" style="font-size: 14px;color:#666">
                                                  
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td>¥{{OrderProductList[i].ShopPrice}}</td>
                                    <td>{{OrderProductList[i].RealCount}}</td>
                                     <td>{{OrderProductList[i].ShipFee}}</td>
                                    <td class="editor_food_box">
                                        ¥{{OrderProductList[i].ProductsTogether}}
                                    </td>
                                    <!--<td class="editor_food_box">-->
                                        <!--<div class="status_num" data-id="{{OrderProductList[i].PId}}">预览</div>-->
                                    <!--</td>-->
                                </tr>
                                {{/each}}
                                </tbody>
                            </table>
                        </div>
                        <!-- 表格内容end -->
                        <div class="order_number_box" style="overflow: hidden;margin-top: 20px;">
                            {{if OrderActionInfo.length >0}}
                            <ul class="col-md-6 number_ul dingdan_xinxi" style="padding:0px">
                            <li>门店名称：{{TakeOutOrderInfo[0].StoreName}}</li>
                                <li>订单编号：{{TakeOutOrderInfo[0].OSn}}</li>
                                {{each OrderActionInfo as value i}}
                                {{if OrderActionInfo[i].ActionTypes == "提交"}}
                                <li>订单时间：{{OrderActionInfo[i].ActionTime}}</li>
                                {{else if OrderActionInfo[i].ActionTypes == "确认"}}
                                <li>付款时间：{{OrderActionInfo[i].ActionTime}}</li>
                                {{else if OrderActionInfo[i].ActionTypes == "备货"}}
                                <li>发货时间：{{OrderActionInfo[i].ActionTime}}</li>
                                {{else if OrderActionInfo[i].ActionTypes == "发货"}}
                                <li>完成时间：{{OrderActionInfo[i].ActionTime}}</li>
                                {{/if}}
                                {{/each}}
                            </ul>
                            {{/if}}
                            {{if TakeOutOrderInfo.length >0}}
                            <ul class="col-md-6 total_ul" style="padding:0px">
                                <li>购物车小计：¥{{TakeOutOrderInfo[0].ProductAmount}}</li>
                                <li>运费：¥{{TakeOutOrderInfo[0].ShipFeeAmount}}</li>
                                    <li>购物积分：{{TakeOutOrderInfo[0].AccountMoney}}</li>
                                    <li>积分：{{TakeOutOrderInfo[0].PayCreditCount}}</li>
                                  <li> 支付金额：¥{{TakeOutOrderInfo[0].SurplusMoney}}</li>
                                <li style="max-width:200px;">
                                    <span style="display: inline-block;margin-bottom: 10px">支付方式：{{TakeOutOrderInfo[0].PayFriendName}}</span>
                                    <hr style="border-bottom:1px solid #ccc;margin:0px -20px 0px;">
                                    <div class="has_receive" style="margin-top: 10px">订单实收款：{{TakeOutOrderInfo[0].OrderAmount}}元</div>
                                </li>
                            </ul>
                            {{/if}}
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
            console.log(123)
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
        var methodName = "/order/AdminOrderInfo";
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