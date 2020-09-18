$(function(){
    information.init();
})

var information = {
    informationTpl : `
    {{each NoticeList as value i}}
        <div class="box box-default">
            <div class="box-header with-border">
                <h3 class="box-title">{{NoticeList[i].Body}}</h3>
                <div class="box-tools pull-right">
                    <span class="time_txt">{{NoticeList[i].AddTime | convertInformationTime}}创建</span>
                    <button type="button" class="btn btn-box-tool deleteBtn" data-nid="{{NoticeList[i].NId}}">
                        <i class="fa fa-remove"></i>
                    </button>
                </div>
            </div>
        </div>
    {{/each}}`,

    init:function(){

        $("#informationList").on("click",".deleteBtn",function(){
            var nid = $(this).attr("data-nid");
            information.deleteInformation(nid);
        });

        //使用辅助函数
        template.defaults.imports.convertInformationTime = Common.convertInformationTime;

        information.getInformationList(0);
    },
    //删除通知
    deleteInformation:function(NId){
        var methodName = "/notice/AdminDelNotice";
        var data = {
            NId: NId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showInfoMsg("删除成功");
                // alert(Common.getCurrentPageIndex());
                var pageIndex = Common.getCurrentPageIndex() - 1;
                information.getInformationList(pageIndex);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取通知列表
    getInformationList:function(page_index){
        var pageSize = 10;
        var methodName = "/notice/AdminNoticeList";
        var data = {
            IsRead: 2,
            Page: {
                PageSize: pageSize,
                PageIndex: page_index+1
            }
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                //渲染html
                var render = template.compile(information.informationTpl);
                var html = render(data.Data);
                $("#informationList").html(html);

                var count = data.Data.Total;
                $("#Pagination").pagination(count, {
                    callback: information.getInformationList,
                    items_per_page: pageSize,
                    current_page: page_index
                });

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}
