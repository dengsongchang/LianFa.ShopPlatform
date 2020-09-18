var collageDetails={
    DetailTemplate:'<div class="tuanzhangInfo">\n' +
    '                    <h3 class="tuanzhang">团长信息</h3>\n' +
    '                    <div class="Info_details">\n' +
    '                        <div class="info col-lg-4 col-md-12">\n' +
    '                            <p>会员名：{{AdminSpellGroupInfo.UserName}}</p>\n' +
    '                        </div>\n' +
    '                        <div class="info col-lg-4 col-md-12">\n' +
    '                            <p >联系电话：{{AdminSpellGroupInfo.Mobile}}</p>\n' +
    '                        </div>\n' +
    '                        <div class="info col-lg-4 col-md-12">\n' +
    '                            <p >真实姓名：{{AdminSpellGroupInfo.RealName}}</p>\n' +
    '                        </div>\n' +
    '                    </div>\n' +
    '                    <div class="customerinfo ">\n' +
    '                        <p>买家备注：{{AdminSpellGroupInfo.BuyerRemark}}</p>\n' +
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
    '                            <th>拼团人数</th>\n' +
    '\n' +
    '                        </tr>\n' +
    '                        <tr>\n' +
    '                            <td style="vertical-align: middle">\n' +
    '                                <div class="media">\n' +
    '                                    <div class="media-left">\n' +
    '                                        <a href="#">\n' +
    '                                            <img src="{{AdminSpellGroupProduct.ShowImg}}" class="media-object" style="width:80px;border-radius: 6px" alt="..."></a>\n' +
    '                                    </div>\n' +
    '                                    <div class="media-body" style="width:auto;vertical-align: middle;">\n' +
    '                                        <h4 class="media-heading" style="color:#1792e7;font-size: 14px;">{{AdminSpellGroupProduct.ProductName}}</h4>\n' +
    '                                        <div class="detail_desc_pic" style="font-size: 14px;color:#666">\n' +
    '                                        </div>\n' +
    '                                    </div>\n' +
    '                                </div>\n' +
    '                            </td>\n' +
    '                            <td style="vertical-align: middle">{{AdminSpellGroupProduct.Number}}</td>\n' +
    '\n' +
    '                        </tr>\n' +
    '                        </tbody>\n' +
    '                    </table>\n' +
    '                </div>\n' +
    '\n' +
    '\n' +
    '                <div class="order_number_box" style="overflow: hidden;margin-top: 20px;">\n' +
    '                    <ul class="col-md-6 number_ul dingdan_xinxi" style="padding-top:10px;padding-bottom: 20px;">\n' +
    '                        <li>订单编号：{{AdminSpellGroupInfo.OSn}}</li>\n' +
    '                        <li style="margin-top:10px">订单时间：{{AdminSpellGroupInfo.AddTime | interceptTime}}</li>\n' +
    '                    </ul>\n' +
    '\n' +
    '                </div>\n' +
    '\n' +
    '\n' +
    '                <div class="box-body no-padding" style="border-bottom: 2px solid #ccc;">\n' +
    '                    <table class="table" id="table_order2">\n' +
    '                        <tbody>\n' +
    '                        <tr style="background-color: #f8f8f8">\n' +
    '\n' +
    '                            <th>拼团订单编号</th>\n' +
    '                            <th>团员</th>\n' +
    '                            <th>规格</th>\n' +
    '                            <th>团购价</th>\n' +
    '                            <th>团购时间</th>\n' +
    '                            <th>操作</th>\n' +
    '\n' +
    '                        </tr>\n' +
    '                       {{each SpellGroupMembersList as value i}}\n' +
    '                        <tr>\n' +
    '                            <td style="vertical-align: middle">{{SpellGroupMembersList[i].OSn}}</td>\n' +
    '                            <td style="vertical-align: middle"><img src="{{SpellGroupMembersList[i].Avatar}}" alt="" style="width:40px;height:40px;border-radius:50%;overflow: hidden"></img>&nbsp;&nbsp{{SpellGroupMembersList[i].UserNmae}}</td>\n' +
    '                            <td style="vertical-align: middle">{{SpellGroupMembersList[i].Spec}}</td>\n' +
    '                            <td style="vertical-align: middle">{{SpellGroupMembersList[i].GroupPrice}}</td>\n' +
    '                            <td style="vertical-align: middle">{{SpellGroupMembersList[i].AddTime | interceptTime}}</td>\n' +
    '                            <td class="editor_food_box" style="vertical-align: middle">\n' +
    '                                <a class="" href="/order/tuanyuanDetails?id={{SpellGroupMembersList[i].OId}}" style="color:#44b8fd;cursor: pointer" >查看</a>\n' +
    '                            </td>\n' +
    '\n' +
    '                        </tr>\n' +
    '                        {{/each}}\n' +
    '                        </tbody>\n' +
    '                    </table>\n' +
    '                </div>',





    init:function () {
        collageDetails.AdminSpellGroupInfo()
        // 使用辅助函数
        template.defaults.imports.interceptTime = Common.interceptTime;
    },


    //订单信息
    AdminSpellGroupInfo:function(){
        var methodName = "/groupbuying/AdminSpellGroupInfo";
        var data = {
            "OgrId": Common.getUrlParam('id'),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {

                var render = template.compile(collageDetails.DetailTemplate);
                var html = render(data.Data);
                $("#all_box").append(html);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};

$(function(){

    collageDetails.init()

})