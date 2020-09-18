$(function () {
    MemberIntegral.init();
})

var MemberIntegral = {
    userRankTpl: `
        {{each UserRankList as value i}}
        <option value="{{UserRankList[i].UserRId}}">{{UserRankList[i].Title}}</option>
        {{/each}}
    `,
    init:function () {
        MemberIntegral.getCreditRules();
        MemberIntegral.initBootstrapTable()
        MemberIntegral.getUserRankList();
        //单个修改积分
        $("#cashTable").on("click",".editor",function () {
            var uId = $(this).attr("data-id");
            var uIdArr = [];
            uIdArr.push(uId);
            $(".mask").show();
            MemberIntegral.showEditor(uIdArr);
        });
        // 关闭弹窗
        $(".mask").on("click",".close",function () {
            $(".mask").hide();
        });

        // 初始化switch开关控件
        Common.initSwitch();
        //查询按钮点击
        $('body').on('click','#searchBtn',function(){
            var data = {
                "UserName": $('#memberName').val(),
                "UserRId": $("#grade-select").val()
            };
            MemberIntegral.projectQuery(data);
        });
        //点击导出数据按钮
        $(".tab-content").on("click","#exportBtn",function () {
            // var UserName= $('#memberName').val();
            // var UserRId= $("#grade-select").val()
            MemberIntegral.exportCreditList(MemberIntegral.getSelectedData());
        });
        //全选
        $(".select-all").on("change",function(){
            if($(this).is(':checked')){
                $(".checkbox").prop("checked",true);
            }
            else{
                $(".checkbox").prop("checked",false);
            }
        });

        //多个编辑
        $("#editorMore").on("click",function(){
            Common.confirmDialog("确认对选中的数据进行编辑吗？",function(){
                $(".mask").show();
                MemberIntegral.showEditor(MemberIntegral.getSelectedData());
            });
        });
        //点击完成按钮设置积分规则
        $('body').on('click','#inteSubmit',function(){
            var register = $('#register').val();
            var signDaily = $('#signDaily').val();
            var continuSign = $('#continuSign').val();
            var signDay = $('#signDay').val();
            var consumeNum = $('#consumeNum').val();
            var commentGoods = $('#commentGoods').val();
            var deductNum = $('#deductNum').val();
            var deductRatio = $('#deductRatio').val();
            var PayCreditPrice = $('#PayCreditPrice').val()
            if($('#togetherCoupon').hasClass('switch-on')){
                var togetherCoupon = 1;
            }else{
                var togetherCoupon = 0;
            }
            // //注册会员奖励积分验证
            // if (!Validate.emptyValidateAndFocus("#register", "请输入注册会员奖励积分数", "")) {
            //     return false;
            // }
            //每日签到奖励积分验证
            if (!Validate.emptyValidateAndFocus("#signDaily", "请输入每日签到奖励积分数", "")) {
                return false;
            }
            //连续签到奖励积分完成
            if (!Validate.emptyValidateAndFocus("#PayCreditPrice", "请输入100熊豆换算金额", "")) {
                return false;
            }
            // //额外奖励的天数验证
            // if (!Validate.emptyValidateAndFocus("#signDay", "请输入额外奖励的天数", "")) {
            //     return false;
            // }
            // //购物消费钱数验证
            // if (!Validate.emptyValidateAndFocus("#consumeNum", "请输入购物消费的钱数", "")) {
            //     return false;
            // }
            // //评论商品奖励的天数验证
            // if (!Validate.emptyValidateAndFocus("#commentGoods", "请输入评论商品奖励的天数", "")) {
            //     return false;
            // }
            // //抵扣订单金额的分数验证
            // if (!Validate.emptyValidateAndFocus("#deductNum", "请输入抵扣订单金额的分数", "")) {
            //     return false;
            // }
            // //最高可抵扣的比例验证
            // if (!Validate.emptyValidateAndFocus("#deductRatio", "请输入最高可抵扣的比例", "")) {
            //     return false;
            // }
            MemberIntegral.setCreditRules(register,signDaily,continuSign,signDay,consumeNum,commentGoods,deductNum,deductRatio,togetherCoupon,PayCreditPrice)
        });
    },
    //点击修改保存按钮
    showEditor:function(id){
        $(".mask").on("click","#editorBtn",function () {
            var changeKind = parseInt($('input:radio[name="editIntegral"]:checked').val());
            var remark = $('#remark').val();
            if($('input:radio[name="editIntegral"]:checked').val()=="0"){
                var changeNum = parseInt($('#addNum').val());
            }
            if($('input:radio[name="editIntegral"]:checked').val()=="1"){
                var changeNum = parseInt($('#reduceNum').val());
            }
            MemberIntegral.editCredit(id,changeKind,changeNum,remark);
        })
    },

    // 获取选中的数据
    getSelectedData:function(){
        var list = $("#cashTable .checkbox");
        var PId = [];
        for(var i=0;i<list.length;i++){
            if(list.eq(i).is(':checked')){
                PId.push(parseInt(list.eq(i).attr("data-uid")));
            }
        }
        return PId;
    },
    //修改积分数
    editCredit:function (uid,addnum,reducenum,remark) {
        //请求方法
        var methodName = "/credit/AdminEditCredit";
        var data = {
            "UId": uid,
            "AddOrReduce": addnum,
            "PayCredits": reducenum,
            "Remark": remark
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('修改成功');
                $(".mask").hide();
                $(".select-all").prop("checked",false);
                $(".checkbox").prop("checked",false);
                MemberIntegral.projectQuery();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取会员等级列表
    getUserRankList:function(){
        var methodName = "/user/AdminUserRankList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(MemberIntegral.userRankTpl);
                var html = render(data.Data);
                $("#grade-select").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //导出积分列表
    exportCreditList:function (uid) {
        //请求方法
        var methodName = "/credit/ExportAdminUserCreditList";
        var data = {
            "UId":uid
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('导出成功');
                window.location.href=data.Data;
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //设置积分规则
    setCreditRules:function (register,signdaily,continusign,signday,consumenum,commentgoods,deductnum,deductratio,togethercoupon,PayCreditPrice) {
        //请求方法
        var methodName = "/credit/AdminSetCreditRules";
        var data = {
            "RegisterCredit": register,
            "SignInCredit": signdaily,
            "ContinueSignInDayCredit":continusign,
            "ContinueSignInAwardCredit": signday,
            "PurchaseCredit": consumenum,
            "ReviewProductCredit": commentgoods,
            "OrderAmountCredit": deductnum,
            "HighestRateCredit": deductratio,
            "TogetherCouponCredit": togethercoupon,
            "PayCreditPrice": PayCreditPrice,
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('设置成功');
                MemberIntegral.getCreditRules();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //    获取积分规则设置
    getCreditRules:function () {
        //请求方法
        var methodName = "/credit/AdminGetCreditRules";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $('#register').val(data.Data.RegisterCredit);
                $('#signDaily').val(data.Data.SignInCredit);
                $('#continuSign').val(data.Data.ContinueSignInDayCredit);
                $('#signDay').val(data.Data.ContinueSignInAwardCredit);
                $('#consumeNum').val(data.Data.PurchaseCredit);
                $('#commentGoods').val(data.Data.ReviewProductCredit);
                $('#deductNum').val(data.Data.OrderAmountCredit);
                $('#deductRatio').val(data.Data.HighestRateCredit);
                $('#PayCreditPrice').val(data.Data.PayCreditPrice);
                if(data.Data.TogetherCouponCredit==1){
                    $('#togetherCoupon').addClass('switch-on')
                }else{
                    $('#togetherCoupon').removeClass('switch-on');
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#cashTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/credit/AdminCreditList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 10,
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
            queryParams: MemberIntegral.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: MemberIntegral.responseHandler,
            columns: [
                {
                    field: 'UId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-uid="'+ value +'">'
                        return html;
                    }
                },
                {
                    field: 'UserName',
                    title: '会员名',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

                        return e;
                    }
                },
                {
                    field: 'PayCredits',
                    title: '可用积分',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

                        return e;
                    }
                },
                {
                    field: 'HistoryCredits',
                    title: '历史积分',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';;

                        return e;
                    }
                },
                {
                    field: 'Title',
                    title: '会员等级',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';;

                        return e;
                    }
                },
                {
                    field: 'UId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var html = '<span class="editor" data-id="' + value + '">修改</span>' +
                            '<a class="view" href="/member/memberDetail?id='+value+'">查看明细</span>';

                        return html;
                    }
                },


            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function () {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function () { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function (data) {
                // console.log(data);

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


            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {


            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/credit/AdminCreditList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "UserName": "",
            "UserRId": 0

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            // console.log(res);
            return {
                "rows": res.Data.CreditList,
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
    projectQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {};
        } else {
            var obj = parame;
        }
        //方法名
        var methodName = "/credit/AdminCreditList";

        $('#cashTable').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/credit/AdminCreditList',
                query: obj
            }
        );
    },
}