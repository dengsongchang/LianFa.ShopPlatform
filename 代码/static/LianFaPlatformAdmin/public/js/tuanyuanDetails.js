var tuanyuanDetails={
    DetailTemplate:'<div class="tuanzhangInfo">\n' +
    '                    <h3 class="tuanzhang">团员信息</h3>\n' +
    '                    <div class="Info_details">\n' +
    '                        <div class="info col-lg-4 col-md-12">\n' +
    '                            <p>会员名：{{SpellGroupInfo.UserName}}</p>\n' +
    '                        </div>\n' +
    '                        <div class="info col-lg-4 col-md-12">\n' +
    '                            <p >联系电话：{{SpellGroupInfo.Mobile}}</p>\n' +
    '                        </div>\n' +
    '                        <div class="info col-lg-4 col-md-12">\n' +
    '                            <p >真实姓名：{{SpellGroupInfo.NickName}}</p>\n' +
    '                        </div>\n' +
    '                    </div>\n' +
    '                    <div class="customerinfo ">\n' +
    '                        <p>买家备注：{{SpellGroupInfo.BuyerRemark}}</p>\n' +
    '                    </div>\n' +
    '                </div>\n' +
    '\n' +
    '                <div class="productInfo col-lg-12 col-md-12">\n' +
    '                    <h3 class="shangpin">商品信息</h3>\n' +
    '                </div>\n' +
    '\n' +
    '                <!--表格内容-->\n' +
    '                <div class="box-body no-padding" style="border-bottom: 2px solid #ccc;">\n' +
    '                    <table class="table" id="table_order">\n' +
    '                        <tbody>\n' +
    '                        <tr style="background-color: #f8f8f8">\n' +
    '                            <th style="">商品名称</th>\n' +
    '                            <th>产品价格</th>\n' +
    '                            <th>购买数量</th>\n' +
    '                            <th>规格</th>\n' +
    '\n' +
    '                        </tr>\n' +
    '                        <tr>\n' +
    '                            <td style="vertical-align: middle">\n' +
    '                                <div class="media">\n' +
    '                                    <div class="media-left">\n' +
    '                                        <a href="#">\n' +
    '                                            <img src="{{SpellGroupProductInfo.ShowImg}}" class="media-object" style="width:80px;border-radius: 6px" alt="..."></a>\n' +
    '                                    </div>\n' +
    '                                    <div class="media-body" style="width:auto;vertical-align: middle;">\n' +
    '                                        <h4 class="media-heading" style="color:#1792e7;font-size: 14px;">{{SpellGroupProductInfo.Name}}</h4>\n' +
    '                                        <div class="detail_desc_pic" style="font-size: 14px;color:#666">\n' +
    '                                        </div>\n' +
    '                                    </div>\n' +
    '                                </div>\n' +
    '                            </td>\n' +
    '                            <td style="vertical-align: middle">¥{{SpellGroupProductInfo.ShopPrice}}</td>\n' +
    '                            <td style="vertical-align: middle">{{SpellGroupProductInfo.Count}}</td>\n' +
    '                            <td style="vertical-align: middle">{{SpellGroupProductInfo.Spec}}</td>\n' +
    '                        </tr>\n' +
    '                        </tbody>\n' +
    '                    </table>\n' +
    '                </div>\n' +
    '\n' +
    '\n' +
    '                <div class="order_number_box" style="overflow: hidden;margin-top: 20px;">\n' +
    '                    <ul class="col-md-6 number_ul dingdan_xinxi" style="padding-top:10px;padding-bottom: 20px;">\n' +
    '                        <li>订单编号：{{SpellGroupInfo.Osn}}</li>\n' +
    '                        <li style="margin-top:10px">订单时间：{{SpellGroupInfo.AddTime}}</li>\n' +
    '                    </ul>\n' +
    '\n' +
    '                </div>',


    init:function () {
        tuanyuanDetails.AdminOrderInfo()

        // 使用辅助函数
        template.defaults.imports.interceptTime = Common.interceptTime;
    },


    //订单信息
    AdminOrderInfo:function(){
        var methodName = "/groupbuying/AdminSpellGroupMembersInfo";
        var data = {
            "OId": Common.getUrlParam('id'),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(tuanyuanDetails.DetailTemplate);
                var html = render(data.Data);
                $("#all_box").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};

$(function(){

    tuanyuanDetails.init()

})