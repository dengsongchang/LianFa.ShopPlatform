var LimitSaleDetail = {
    //状态（默认 0-全部 1-待付款 2-确认中 3-已确认 4-等待配送 5-配送中 6-已完成 7-锁定 8-已关闭） ,
    state:0,
    //快递公司模板
    expressCompanyTemplate:`
            <option selected="selected" value="0">请选择快递公司</option>
            {{each ShipList as value i}}
                <option value="{{ShipList[i]}}">{{ShipList[i]}}</option>
            {{/each}}
`,
    //商品模板
    productTemplate:`
                        <li class="clearfix">
                            <div class="p-name">
                                <img src="{{PShowImg}}">
                                <div>
                                    <p class="pn-name">{{PName}}</p>
                                </div>
                            </div>
                            <p>{{Count}}</p>
                            <p>{{Money}}</p>
                        </li>
`,
    init: function () {

        LimitSaleDetail.initBootstrapTable();
        //点击发货按钮
        $('body').on('click','.sendProduct',function(){
            $('.mask').show();
            $('.checkBox').hide();
            $('.deliverBox').show();
            var id = $(this).attr('data-id')
            // localStorage.setItem('AsId',$(this).attr('data-id'));
            // LimitSaleDetail.adminGetDeliveryList()
            LimitSaleDetail.adminTimeProductActivityOrderSend(id)

        });
        //点击确认按钮
        $('body').on('click','.comfirmBtn',function(){
            var id = $(this).attr('data-id');
            LimitSaleDetail.confirmAdminTimeProductActivityOrder(id);
        })
        //点击关闭按钮
        $('body').on('click','.closeBtn',function(){
            var id = $(this).attr('data-id');
            LimitSaleDetail.cancellAdminTimeProductActivityOrder(id);
        })
        //查询按钮点击
        $('body').on('click','#searchBtn',function(){
            LimitSaleDetail.projectQuery()
        })
        // 分页条数设置
        $("#pagesize_dropdown").on("change", function () {
            LimitSaleDetail.projectDectoryQuery();
        });
        //发货里面的弹窗去确认点击
        $('body').on('click','#deliver',function(){
            //快递单号验证
            if (!Validate.emptyValidateAndFocus("#expressNumber", "请输入快递单号", "")) {
                return false;
            }
            //快递公司
            if($('#logisticsCompany').val() == "0"){
                Common.showErrorMsg("请选择快递公司!")
                return false;
            }
            //调用确认发货接口
            LimitSaleDetail.adminSendGoods()

        })
    },
    //获取发货订单基本信息
    adminGetDeliveryList:function(){
        var methodName = "/aftersalesservice/AdminGetAfterSalesServiceDetail";
        var data = {
            "AsId": localStorage.getItem('AsId'),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#orderAddress').text(data.Data.Address);
                $('#buymarker').text(data.Data.BuyerNote);
                var render = template.compile(LimitSaleDetail.expressCompanyTemplate);
                var html = render(data.Data);
                $('#logisticsCompany').html(html);
                var render1 = template.compile(LimitSaleDetail.productTemplate);
                var html1 = render1(data.Data);
                $('.pl-table').html(html1);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //发货
    adminSendGoods:function(){
        var methodName = "/aftersalesservice/AdminSendGoods";
        var data = {
            "AsSid": localStorage.getItem('AsId'),
            "ShipCoName": $('#logisticsCompany').val(),
            "ShipSn": $('#expressNumber').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('发货成功',function(){
                    LimitSaleDetail.projectQuery();
                    $('.mask').hide()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //确认后台限时商品活动订单
    confirmAdminTimeProductActivityOrder:function(id){
        var methodName = "/timeproductactivity/ConfirmAdminTimeProductActivityOrder";
        var data = {
            "Oid": id
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('确认成功',function(){
                    LimitSaleDetail.projectQuery();
                    $('.mask').hide()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台限时商品活动订单发货
    adminTimeProductActivityOrderSend:function(id){
        var methodName = "/timeproductactivity/AdminTimeProductActivityOrderSend";
        var data = {
            "Oid": id
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('发货成功',function(){
                    LimitSaleDetail.projectQuery();
                    $('.mask').hide()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台限时商品活动订单发货
    lockAdminTimeProductActivityOrder:function(id){
        var methodName = "/timeproductactivity/LockAdminTimeProductActivityOrder";
        var data = {
            "Oid": id
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('锁定成功',function(){
                    LimitSaleDetail.projectQuery();
                    $('.mask').hide()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //关闭后台限时商品活动订单
    cancellAdminTimeProductActivityOrder:function(id){
        var methodName = "/timeproductactivity/CancellAdminTimeProductActivityOrder";
        var data = {
            "Oid": id
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('关闭成功',function(){
                    LimitSaleDetail.projectQuery();
                    $('.mask').hide()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

    //bootstrapTable
    initBootstrapTable: function () {
        $('#tb_limit_content').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/timeproductactivity/AdminTimeProductActivityOrderList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
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
            queryParams: LimitSaleDetail.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: LimitSaleDetail.responseHandler,
            columns: [

                {
                    field: 'UNmae',
                    title: '会员名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e = `<span>${value}</span>`
                        return e;
                    }
                },
                {
                    field: 'OSn',
                    title: '订单编号',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'AddTime',
                    title: '下单时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var endTime = moment(value).format('YYYY-MM-D')
                        var e =`<span>${endTime}</span>`
                        return e;
                    }
                },
                {
                    field: 'ProductAmount',
                    title: '订单实收款(元)',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e =`<span>${value}</span>`
                        return e;
                    }
                },
                {
                    field: 'Count',
                    title: '抢购数量',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e =`<span>${value}</span>`
                        return e;
                    }
                },
                {
                    field: 'OrderStateDescription',
                    title: '订单状态',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'OId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = "";
                        if(row.OrderState == "40"){
                            html += `<span style="padding: 0 6px;color:#44b8fd;cursor: pointer"  class="comfirmBtn"  data-id="${value}">确认</span>`
                        }
                        else if(row.OrderState == "80"){
                            html += `<span style="padding: 0 6px;color:#44b8fd;cursor: pointer"  class="sendProduct"  data-id="${value}">发货</span>`
                        }
                        html += `<span style="padding: 0 6px;color:#44b8fd;cursor: pointer"  class="closeBtn"  data-id="${value}">关闭</span>`

                        if(row.OrderState == "160"){
                            html = "";
                        }
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
                console.log(data);

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
        var methodName = "/timeproductactivity/AdminTimeProductActivityOrderList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "ActivityId": Common.getUrlParam('ActivityId'),
            "State": $('#stateBox').val(),

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.OrderList,
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
            obj = {
                "ActivityId": Common.getUrlParam('ActivityId'),
                "State": $('#stateBox').val(),
            };
        } else {
            obj = parame;
        }

        $('#tb_limit_content').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/timeproductactivity/AdminTimeProductActivityOrderList',
                query: obj
            }
        );

    },
    //表格刷新
    projectDectoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            obj = {
                "ActivityId": Common.getUrlParam('ActivityId'),
                "State": $('#stateBox').val(),
                page: {
                    PageSize: LimitSaleDetail.pageSize,
                    PageIndex: 1
                }
            };
        } else {
            obj = parame;
        }

        $('#tb_limit_content').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/timeproductactivity/AdminTimeProductActivityOrderList',
                query: obj
            }
        );
        LimitSaleDetail.initBootstrapTable()
    },
};

$(function () {

    LimitSaleDetail.init()

});