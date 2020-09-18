$(function () {
    marketQuotaNew.init();
})

var marketQuotaNew = {
    userRankTpl:`
        {{each UserRankList as value i}}
            <label class="checkbox-inline">
                <input type="checkbox" name="memberRank"  value="{{UserRankList[i].UserRId}}">
                {{UserRankList[i].Title}}
            </label>
        {{/each}}
    `,
    mianProductTpl:`
        <div class="mainGoodList row" data-id="{{PId}}">
            <div class="col-md-5 name">{{Name}}</div>
            <div class="col-md-3">{{ShopPrice}}</div>
            <div class="col-md-4 operate" data-id="{{PId}}">删除</div>
        </div>
    `,
    sendProductTpl:`
        <div class="mainGoodList row" data-id="{{PId}}">
            <div class="col-md-5 name">{{Name}}</div>
            <div class="col-md-3">{{CostPrice}}</div>
            <div class="col-md-4 operate" data-id="{{PId}}">删除</div>
        </div>
    `,
    init:function () {
        //初始化富文本编辑器
        var ue = UE.getEditor('hcEditor');

        //图片上传
        $('#uploadfile_btn').on('change', function () {
            //获取文件
            var file = $("#editorContent").find("input")[0].files[0];
            var formData = new FormData();
            formData.append("upfile", file);
            formData.append("operation", "uploadproducteditorimage");
            var xhr = new XMLHttpRequest();
            xhr.open('post', "http://localhost:42090/api/admin" + '/tool/Upload');
            xhr.send(formData);
            xhr.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    console.log(JSON.parse(this.response))
                    if (JSON.parse(this.response).Code == "100") {
                        UE.getEditor('hcEditor').setContent('<img src="' + JSON.parse(this.response).Data + '">', true)
                    }
                }
            };
        });

        //关闭弹窗
        $(".mask").on("click",".close",function () {
            $(".mask").hide();
        });

        // 切换类型
        $("#typeBox").on("change","input",function () {
            if($(this).is(':checked')){
                marketQuotaNew.changePageElementWithType($(this).val());
            }
        });

        // 全选
        $("#productTabel").on("change","#selAll",function(){
            if($(this).is(':checked')){
                $(".checkbox").prop("checked",true);
            }
            else{
                $(".checkbox").prop("checked",false);
            }
        });

        // 查询商品
        $("#productSearch").on("click",function () {
            marketQuotaNew.projectQuery();
        });

        // 满送选择主商品
        $("#mainProductSelect").on("click",function () {
            if($("#productTabel").find("tr").length > 0){
                marketQuotaNew.projectQuery();
            }
            else {
                marketQuotaNew.initBootstrapTable();
            }
            $("#confirm").attr("data-type",1);
            $(".mask").show();
        });

        // 满送选择赠送商品
        $("#sendProductSelect").on("click",function () {
            if($("#productTabel").find("tr").length > 0){
                marketQuotaNew.projectQuery();
            }
            else {
                marketQuotaNew.initBootstrapTable();
            }
            $("#confirm").attr("data-type",2);
            $(".mask").show();
        });

        // 满减选择商品
        $("#productSelect").on("click",function () {
            if($("#productTabel").find("tr").length > 0){
                marketQuotaNew.projectQuery();
            }
            else {
                marketQuotaNew.initBootstrapTable();
            }
            $("#confirm").attr("data-type",3);
            $(".mask").show();
        });

        // 选完商品确定
        $("#confirm").on("click",function () {
            var type = $(this).attr("data-type");
            marketQuotaNew.confirmAfterSelectProduct(type);
        });

        // 确认提交添加
        $("#editConfirm").on("click",function () {
            if(marketQuotaNew.checkInputData()){
                Common.confirmDialog("确认新增活动吗？",function () {
                    if($("input[name='quotaOptions']:checked").val() == 1){
                        // 满减
                        marketQuotaNew.confirmSubmitCutAdd();
                    }
                    else {
                        // 满送
                        marketQuotaNew.confirmSubmitSendAdd();
                    }
                });
            }
        });

        // 删除选中商品
        $("#add").on("click",".operate",function () {
            $(this).parent().remove();
        });

        // 初始化日期控件
        marketQuotaNew.initLaydateWithTime();

        marketQuotaNew.getUserRankList();

        marketQuotaNew.changePageElementWithType(2);
    },
    // 校验数据
    checkInputData:function () {
        if($("#Name").val() == ""){
            Common.showInfoMsg("促销活动名称未填写");
            return false;
        }
        else {
            if($("#start").val() == ""){
                Common.showInfoMsg("开始日期必选");
                return false;
            }
            else {
                if($("#end").val() == ""){
                    Common.showInfoMsg("结束日期必选");
                    return false;
                }
                else {
                    if(marketQuotaNew.getRankSelectIdArr().length == 0){
                        Common.showInfoMsg("请选择适合的客户");
                        return false;
                    }
                    else {
                        var ue = UE.getEditor('hcEditor');
                        var Describe = ue.getContent();
                        if(Describe == ""){
                            Common.showInfoMsg("促销详细信息未填写");
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                }
            }
        }
    },
    // 确认提交满减添加
    confirmSubmitCutAdd:function () {
        var methodName = "/promotion/AddFullCutPromotion";

        var ue = UE.getEditor('hcEditor');
        var Describe = ue.getContent();

        var data = {
            PType: 1,
            PromotionName: $("#Name").val(),
            Type: $("input[name='activityKind']:checked").val(),
            StartTime: $("#start").val(),
            EndTime: $("#end").val(),
            UserRankLower: marketQuotaNew.getRankSelectIdArr(),
            State: $("input[name='stateOptions']:checked").val(),
            LimitMoney1: $("#LimitMoney").val(),
            CutMoney1: $("#CutMoney").val(),
            Describe: Describe,
            Pids: marketQuotaNew.getSelectIdArr()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("新增成功",function () {
                    location.href = "/marketMode/marketQuota";
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 确认提交满送编辑
    confirmSubmitSendAdd:function () {
        var methodName = "/promotion/AddFullSendPromotion";

        var ue = UE.getEditor('hcEditor');
        var Describe = ue.getContent();

        var data = {
            PType: 2,
            PromotionName: $("#Name").val(),
            StartTime: $("#start").val(),
            EndTime: $("#end").val(),
            UserRankLower: marketQuotaNew.getRankSelectIdArr(),
            State: $("input[name='stateOptions']:checked").val(),
            LimitMoney: $("#LimitMoney").val(),
            AddMoney: $("#AddMoney").val(),
            Describe: Describe,
            Pids: marketQuotaNew.getMainSelectIdArr(),
            Gids: marketQuotaNew.getSendSelectIdArr()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("新增成功",function () {
                    location.href = "/marketMode/marketQuota";
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取适合的客户选中id数组
    getRankSelectIdArr:function () {
        var result = [];
        var list = $("#rankList input");
        for(var i=0;i<list.length;i++){
            if(list.eq(i).is(':checked')){
                result.push(list.eq(i).val());
            }
        }
        return result;
    },
    // 获取满减商品选中的商品id数组
    getSelectIdArr:function () {
        var result = [];
        var list = $("#product .mainGoodList");
        for(var i=0;i<list.length;i++){
            result.push(list.eq(i).attr("data-id"));
        }
        return result;
    },
    // 获取满送主商品选中的商品id数组
    getMainSelectIdArr:function () {
        var result = [];
        var list = $("#mainProduct .mainGoodList");
        for(var i=0;i<list.length;i++){
            result.push(list.eq(i).attr("data-id"));
        }
        return result;
    },
    // 获取满送赠送商品选中的商品id数组
    getSendSelectIdArr:function () {
        var result = [];
        var list = $("#sendProduct .mainGoodList");
        for(var i=0;i<list.length;i++){
            result.push(list.eq(i).attr("data-id"));
        }
        return result;
    },
    // 选中商品确定
    confirmAfterSelectProduct:function (type) {
        var selectData = marketQuotaNew.getSelectedProductData();
        if(selectData.length > 0){
            if(type == 1){
                // 满送主商品
                for(var i=0;i<selectData.length;i++){
                    var render = template.compile(marketQuotaNew.mianProductTpl);
                    var html = render(selectData[i]);
                    $("#mainProduct").append(html);
                }

                $(".mask").hide();
            }
            else if(type == 2){
                // 满送赠送商品
                for(var i=0;i<selectData.length;i++){
                    if($("#sendProduct .mainGoodList").length < 5){
                        // 控制数量不超过5个
                        var render = template.compile(marketQuotaNew.sendProductTpl);
                        var html = render(selectData[i]);
                        $("#sendProduct").append(html);
                    }
                }

                $(".mask").hide();
            }
            else if(type == 3){
                // 满送主商品
                for(var i=0;i<selectData.length;i++){
                    var render = template.compile(marketQuotaNew.mianProductTpl);
                    var html = render(selectData[i]);
                    $("#product").append(html);
                }

                $(".mask").hide();
            }
        }
        else {
            $(".mask").hide();
            Common.showInfoMsg("未选择商品");
        }
    },
    // 获取选中的商品
    getSelectedProductData:function () {
        var result = [];
        var list = $("#productTabel .checkbox");
        for(var i=0;i<list.length;i++){
            if(list.eq(i).is(':checked')){
                var productData = {
                    PId: list.eq(i).attr("data-id"),
                    Name: list.eq(i).attr("data-name"),
                    ShopPrice: list.eq(i).attr("data-shopprice"),
                    CostPrice: list.eq(i).attr("data-costprice")
                };
                result.push(productData);
            }
        }
        return result;
    },
    //初始化日期控件：日期时间
    initLaydateWithTime:function () {
        //日期范围限制
        var start = laydate.render({
            elem: '#start',
            type: 'datetime',
            istoday: false,
            done: function (value,date) {
                if(value != ""){
                    date.month = date.month - 1;
                    end.config.min = date; //开始日选好后，重置结束日的最小日期
                }
            }
        })

        var end = laydate.render({
            elem: '#end',
            type: 'datetime',
            istoday: false,
            done: function (value,date) {
                if(value != ""){
                    date.month = date.month - 1;
                    start.config.max = date; //结束日选好后，重置开始日的最大日期
                }
            }
        })
    },
    // 获取会员等级（适合的客户人选）
    getUserRankList:function(){
        var methodName = "/user/AdminUserRankList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(marketQuotaNew.userRankTpl);
                var html = render(data.Data);
                $("#rankList").html(html);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //根据操作类型控制页面元素的显示隐藏
    changePageElementWithType:function (type) {
        if(type == 1){
            $("#cutProductBox").show();
            $("#sendProductBox").hide();
            $("#pType").show();
            $("#CutMoneyBox").show();
            $("#AddMoneyBox").hide();
        }
        else if(type == 2){
            $("#sendProductBox").show();
            $("#cutProductBox").hide();
            $("#pType").hide();
            $("#CutMoneyBox").hide();
            $("#AddMoneyBox").show();
        }
    },

    //会员列表bootstrapTable
    initBootstrapTable:function(){
        $('#productTabel').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/promotion/AdminGetProductList',
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
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: marketQuotaNew.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: marketQuotaNew.responseHandler,
            columns: [
                {
                    field: 'PId',
                    title: '<input type="checkbox" id="selAll">',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-id="'+ value +'" data-name="'+ row.Name +'" data-costprice="'+ row.CostPrice +'" data-shopprice="'+ row.ShopPrice +'" style="margin: 0 auto;">'
                        return html;
                    }
                },
                {
                    field: 'Name',
                    title: '礼品',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'CostPrice',
                    title: '成本价',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'ShopPrice',
                    title: '商品价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                }
            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function() {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function() { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function(data) {
                console.log(data);

                $('.caret').remove()

            },
            onLoadError: function(data) {
                $('#productTabel').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function(row, tr, flied) {
                // 书写自己的方法

            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function(rows) {

                // for (var i = 0; i < rows.length; i++) {
                //     dishes_list.UserIdsList.push(rows[i].User.Id);
                //     dishes_list.UserOpenIds.push(rows[i].User.OpenId);
                // }

            },
            onUncheckAll: function(rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function(row) {


            },
            //取消每一个单选框时对应的操作；
            onUncheck: function(row) {
                Array.prototype.remove = function(val) {
                    var index = this.indexOf(val);
                    if (index > -1) {
                        this.splice(index, 1);
                    }
                };

            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryParams: function(params) {
        //配置参数
        //方法名
        var methodName = "/promotion/AdminGetProductList";

        var pType = $("input[name='quotaOptions']:checked").val();
        if(pType == 1) {
            var PIds = marketQuotaNew.getSelectIdArr();
        }
        else if(pType == 2){
            var PIds = marketQuotaNew.getMainSelectIdArr();
            PIds.push.apply(PIds,marketQuotaNew.getSendSelectIdArr());
        }

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            PIds: PIds,
            Name: $("#productName").val(),
            Page: {
                PageSize: params.limit,//页面大小,
                PageIndex: (params.offset / params.limit) + 1,//页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function(res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.PromotionProductInfoList,
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
    refreshQuery: function(parame) {
        //方法名
        var methodName = "/promotion/AdminGetProductList";

        var pType = $("input[name='quotaOptions']:checked").val();
        if(pType == 1) {
            var PIds = marketQuotaNew.getSelectIdArr();
        }
        else if(pType == 2){
            var PIds = marketQuotaNew.getMainSelectIdArr();
            PIds.push.apply(PIds,marketQuotaNew.getSendSelectIdArr());
        }

        if (parame == "" || parame == undefined) {
            var obj = {
                PIds: PIds,
                Name: $("#productName").val()
            };
        } else {
            var obj = parame;
        }

        $('#productTabel').bootstrapTable(
            "refresh", {
                url:SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新（先销毁后初始化）
    projectQuery: function(parame) {
        //方法名
        var methodName = "/promotion/AdminGetProductList";

        var pType = $("input[name='quotaOptions']:checked").val();
        if(pType == 1) {
            var PIds = marketQuotaNew.getSelectIdArr();
        }
        else if(pType == 2){
            var PIds = marketQuotaNew.getMainSelectIdArr();
            PIds.push.apply(PIds,marketQuotaNew.getSendSelectIdArr());
        }

        if (parame == "" || parame == undefined) {
            var obj = {
                PIds: PIds,
                Name: $("#productName").val(),
                Page: {
                    PageSize: 3,//页面大小,
                    PageIndex: 1,//页码
                }
            };
        } else {
            var obj = parame;
        }


        $('#productTabel').bootstrapTable(
            "destroy", {
                url:SignRequest.urlPrefix + methodName,
                query: obj
            }
        );

        marketQuotaNew.initBootstrapTable();
    }
}
