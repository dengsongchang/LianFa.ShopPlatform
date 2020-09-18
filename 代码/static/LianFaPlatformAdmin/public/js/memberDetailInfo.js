
$(function () {
    MemberDetailInfo.init();
})

var MemberDetailInfo = {
    baseInfoTpl: `
        <div class="col-md-4 l-info">
            <img src="{{AvatarFull}}">
            <p>{{UserName}}<span>{{Title}}</span></p>
        </div>
        <div class="row col-md-8 r-info">
            <div class="col-xs-6 base-list">
                <div class="info-content">
                    <p>姓名：<span>{{UserName}}</span></p>
                </div>
                <div class="info-content">
                    <p>性别：<span>{{Gender}}</span></p>
                </div>
                <div class="info-content">
                    <p>手机：<span>{{Mobile}}</span></p>
                </div>
                <div class="info-content">
                    <p>注册于：<span>{{CreateTime}}</span></p>
                </div>
            </div>
            <div class="col-xs-6 base-list">
                <div class="info-content">
                    <p>昵称：<span>{{UserName}}</span></p>
                </div>
            </div>
        </div>
    `,
    stasticInfoTpl: `
        <li>
            <p class="stastic-num">{{OrderAmount}}</p>
            <p class="stastic-text">消费金额</p>
        </li>
        <li>
            <p class="stastic-num">{{OrderSum}}</p>
            <p class="stastic-text">消费次数</p>
        </li>
        <li>
            <p class="stastic-num">{{CreditsSum}}</p>
            <p class="stastic-text">积分</p>
        </li>
    `,
    buyInfoTpl:`
        {{each BuyRecordInfo as value i}}
            <li class="clearfix">
                <p>{{BuyRecordInfo[i].OSn}}</p>
                <p>{{BuyRecordInfo[i].PayTime}}</p>
                <p>{{BuyRecordInfo[i].FinishTime}}</p>
                <p>{{BuyRecordInfo[i].PayFriendName}}</p>
                <p>{{BuyRecordInfo[i].SurplusMoney}}</p>
                <p>{{BuyRecordInfo[i].OrderAmount}}</p>
            </li>
        {{/each}}
    `,
    init:function () {

        //使用辅助函数
        // template.defaults.imports.convertMemberImgUrl = Common.convertMemberImgUrl;
        // template.defaults.imports.exchangeTimeData = Common.exchangeTimeData;

        MemberDetailInfo.getMemberInfo();
        // 返回上一页
        $('body').on('click','.backBtn',function(){
            window.history.go(-1);
        })
    },
    getMemberInfo:function () {
        var UId = Common.getUrlParam("UId");
        var methodName = "/user/AdminGetUserInfo";
        var data = {
            UId: UId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(MemberDetailInfo.baseInfoTpl);
                var baseHtml = render(data.Data.UserInfo);
                $("#baseInfo").html(baseHtml);

                var sRender = template.compile(MemberDetailInfo.stasticInfoTpl);
                var sHtml = sRender(data.Data.StatisticsInfo);
                $("#stasticInfo").html(sHtml);

                var bRender = template.compile(MemberDetailInfo.buyInfoTpl);
                var bHtml = bRender(data.Data);
                $("#buyInfo").html(bHtml);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}