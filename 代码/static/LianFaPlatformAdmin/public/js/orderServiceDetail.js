var orderServiceDetail = {
    DetailTemplate: `<div class="main_list_content type_box">
    <div style="margin-bottom:15px"><a class="backBtn"><img src="/public/images/back.png" alt=""><span>返回</span></a></div>
        <div class="list_title_header" style="float:left;width:100%">
            <div class="">
                <div class="head-type">
                    <ul class="nav nav-tabs">
                        <li style="">
                            <a data-type="1" href="/order/orderService">退换货列表</a>
                        </li>
                        <li class="active">
                            <a data-type="1" href="javascript:void(0);">售后详情</a>
                        </li>
                    </ul>
                </div>
                
                <div class="ReceiveLogisticsBox" style="overflow:hidden"></div>
                <div class="SendLogisticsBox" style="overflow:hidden"></div>
                
                
                <!-- 买家信息盒子 -->
                <div class="buyer_info_big_box">
                        <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">买家信息</div>
                                          
                        <div class="buyer_info_box">
                            <ul class="buyer_info_one">
                                <li>订单编号：{{OSn}}</li>
                                <li>联系电话：{{Mobile}}</li>
                                <li>联系人：{{Consignee}}</li>
                            </ul>
                            <ul class="receive_info">
                                <li>
                                    <span style="">收货信息：</span>
                                    <span style="">
                                        {{Address}}
                                    </span>
                                </li>
                            </ul>
                            <ul class="mark_info">
                                <li>退货原因：{{ApplyReason}}</li>
                            </ul>
                            <ul class="mark_info">
                                <li>申请退款金额：{{Money}}</li>
                            </ul>
                            <ul class="mark_info">
                                <li>退款途径：原路返回</li>
                            </ul>
                            <ul class="mark_info">
                                <li>退货数量：{{Count}}</li>
                            </ul>
                            {{if ImgList.length>0}}
                                <ul class="mark_info">
                                    <li>图片：
                                           {{each ImgList as value j}}
                                               <img src="{{ImgList[j]}}" style="width:50px;height:50px"> 
                                           {{/each}}
                                    </li>
                                </ul>
                            {{/if}}
                            
                     
                        </div>
                   
                        <!-- 订单信息列表 -->
                        <!-- 表格内容 -->
                        <div class="box-body no-padding" style="border-bottom: 2px solid #ccc;">
                            <table class="table" id="table_order">
                                <tbody>
                                <tr style="background-color: #f8f8f8">
                                    <th style="">商品名称</th>   
                                    <th>购买数量</th>   
                                    <th>小计(元)</th>

                                </tr>
                                <tr>
                                    <td>
                                        <div class="media">
                                            <div class="media-left">
                                                <a href="#">
                                                    <img src="{{PShowImg}}" class="media-object" style="width:80px;border-radius: 6px" alt="..."></a>
                                            </div>
                                            <div class="media-body" style="width:auto;vertical-align: middle;">
                                                <h4 class="media-heading" style="color:#1792e7;font-size: 14px;">{{PName}}</h4>
                                            </div>
                                        </div>
                                    </td>
                                    <td>{{Count}}</td>
                                    <td class="editor_food_box">
                                        ¥{{Money}}
                                    </td>

                                </tr>
                                </tbody>
                            </table>
                        </div>
                </div>
                <!-- 买家信息盒子 end-->



            </div>
        </div>
    </div>`,
    LogisticsTemplate: `
        <div class="list_title_header" style="float:left;width:100%">
            <div class="">
                <!--物流跟踪-->
                <div class="logistics_box">
                    <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">商城收货物流跟踪</div>
                    <div style="margin-bottom: 15px;color:#666" id="ShipSn1"></div>
                    <div class="logistics_dv_detail">
                        <ul class="listBox">
                            {{each data as value i}}
                                 {{if i == "0"}}
                                    <li class="first_info">
                                        <span class="zhouji">{{data[i].ftime}}</span>
                                        
                                        <span class="state_logistic">{{data[i].context}}</span>
                                    </li>
                                {{else}}
                                    <li>
                                        <span class="zhouji">{{data[i].ftime}}</span>
                                        <span class="time_point">{{data[i].time}}</span>
                                        <span class="state_logistic">{{data[i].context}}</span>
                                    </li>
                                {{/if}}
                            {{/each}}
                        </ul>
                    </div>
                </div>
                <!--物流跟踪end-->
            </div>
        </div>
   `,
    LogisticsTemplate1: `
        <div class="list_title_header" style="float:left;width:100%">
            <div class="">
                <!--物流跟踪-->
                <div class="logistics_box">
                    <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">商城发货物流跟踪</div>
                    <div style="margin-bottom: 15px;color:#666" id="ShipSn2"></div>
                    <div class="logistics_dv_detail">
                        <ul class="listBox">
                            {{each data as value i}}
                                 {{if i == "0"}}
                                    <li class="first_info">
                                        <span class="zhouji">{{data[i].ftime}}</span>
                                        
                                        <span class="state_logistic">{{data[i].context}}</span>
                                    </li>
                                {{else}}
                                    <li>
                                        <span class="zhouji">{{data[i].ftime}}</span>
                                        <span class="time_point">{{data[i].time}}</span>
                                        <span class="state_logistic">{{data[i].context}}</span>
                                    </li>
                                {{/if}}
                            {{/each}}
                        </ul>
                    </div>
                </div>
                <!--物流跟踪end-->
            </div>
        </div>
    `,
    init: function () {
        // 返回上一页
        $('body').on('click', '.backBtn', function () {

            window.history.go(-1);

        })
        orderServiceDetail.adminOrderInfo();
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
    },
    //订单信息
    adminOrderInfo: function () {
        var methodName = "/aftersalesservice/AdminGetAfterSalesServiceDetail";
        var data = {
            "AsId": Common.getUrlParam('id'),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(orderServiceDetail.DetailTemplate);
                var html = render(data.Data);
                $("#all_box").append(html);
                if (data.Data.ReceiveLogistics != null && data.Data.ReceiveLogistics != undefined) {
                    if (data.Data.ReceiveLogistics.data != null && data.Data.ReceiveLogistics.data != undefined) {
                        var render = template.compile(orderServiceDetail.LogisticsTemplate);
                        var html = render(data.Data.ReceiveLogistics);
                        $(".ReceiveLogisticsBox").append(html);
                        $('#ShipSn1').text(data.Data.ShipCoName1 + data.Data.ShipSn1)
                    } else {
                        var html = `<div class="">
                <!--物流跟踪-->
                <div class="logistics_box">
                    <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">商城收货物流跟踪</div>
                    <div class="logistics_dv_detail">
                        <div style="margin-bottom: 15px">${data.Data.ShipCoName1} ${data.Data.ShipSn1}</div>
                        <div>${data.Data.ReceiveLogistics.message}</div>
                    </div>
                </div>
                <!--物流跟踪end-->
            </div>`
                        $(".ReceiveLogisticsBox").html(html);
                    }

                } else {
                    var html = `<div class="">
                <!--物流跟踪-->
                <div class="logistics_box">
                    <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">商城收货物流跟踪</div>
                    <div class="logistics_dv_detail">
                        <div>暂无物流信息</div>
                    </div>
                </div>
                <!--物流跟踪end-->
            </div>`
                    $(".ReceiveLogisticsBox").html(html);
                }
                if (data.Data.SendLogistics != null && data.Data.SendLogistics != undefined) {
                    if (data.Data.SendLogistics.data != null && data.Data.SendLogistics.data != undefined) {
                        var render = template.compile(orderServiceDetail.LogisticsTemplate1);
                        var html = render(data.Data.SendLogistics);
                        $(".SendLogisticsBox").append(html);
                        $('#ShipSn2').text(data.Data.ShipCoName2 + data.Data.ShipSn2)
                    } else {
                        var html = `<div class="">
                <!--物流跟踪-->
                <div class="logistics_box">
                    <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">商城发货物流跟踪</div>
                    <div class="logistics_dv_detail">
                        <ul class="listBox">
                            <div style="margin-bottom: 15px">${data.Data.ShipCoName2} ${data.Data.ShipSn2}</div>
                            <div>${data.Data.ReceiveLogistics.message}</div>
                        </ul>
                    </div>
                </div>
                <!--物流跟踪end-->
            </div>`
                        $(".SendLogisticsBox").html(html);
                    }

                } else {
                    var html = `<div class="">
                <!--物流跟踪-->
                <div class="logistics_box">
                    <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">商城发货物流跟踪</div>
                    <div class="logistics_dv_detail">
                        <div>暂无物流信息</div>
                    </div>
                </div>
                <!--物流跟踪end-->
            </div>`
                    $(".SendLogisticsBox").html(html);
                }
                //0代表退货
                if (data.Data.Type == 0) {
                    $('.SendLogisticsBox').hide()
                } else {
                    $('.SendLogisticsBox').show()
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};

$(function () {

    orderServiceDetail.init()

})