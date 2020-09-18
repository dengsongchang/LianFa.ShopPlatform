var logisticsDetail = {
    DetailTemplate: `<div class="main_list_content type_box">
        <div class="list_title_header" style="float:left;width:100%">
            <div class="">
                <div class="head-type">
                    <ul class="nav nav-tabs">
                        <li >
                            <a data-type="1" class="gobackBtn" href="">订单详情</a>
                        </li>
                        <li class="active">
                            <a data-type="1" href="javascript:void(0);">物流信息</a>
                        </li>
                    </ul>
                </div>
                <!--物流跟踪-->
                <div class="logistics_box">
                    <div class="page-header" style="color:#666;border-bottom: none;padding-bottom: 0px;font-size: 16px;">物流跟踪</div>
                    <div class="logistics_dv_detail">
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
                    </div>
                </div>
                <!--物流跟踪end-->
            </div>
        </div>
    </div>`,
    init: function () {
        logisticsDetail.adminOrderInfo();

    },
    //订单信息
    adminOrderInfo: function () {
        var methodName = "/order/AdminOrderLogistics";
        var data = {
            "Oid": Common.getUrlParam('oid'),
            "OLId": Common.getUrlParam('olid'),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(logisticsDetail.DetailTemplate);
                var html = render(data.Data);
                $("#all_box").append(html);
                $('.gobackBtn').attr('href','/order/orderListDetail?id='+Common.getUrlParam('oid')+'&isSee=true')
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};

$(function () {

    logisticsDetail.init()

})