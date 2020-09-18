$(function(){
    OrderService.init();
})

var OrderService = {
    //批量审核的列表
    allList:"",
    //单个审核的id
    singleId:"",
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
                            <p>{{OrderAmount}}</p>
                        </li>
`,
    flag:true,
    init:function(){
        
        $('#State').find('a').click(function(){
            localStorage.setItem('PageIndex', 1)
            localStorage.setItem('PageSize', "10")
        })

        if (localStorage.getItem('PageIndex')) {
            if (localStorage.getItem('PageSize')) {
                $('#pagesize_dropdown').val(localStorage.getItem('PageSize'))
            }
            OrderService.initBootstrapTable()
            setTimeout(function () {
                $('.pagination').find('.page-number').each(function (index, item) {
                    $(item).removeClass('active');
                    if ($(item).find('a').text() == localStorage.getItem('PageIndex')) {
                        $(item).addClass('active');
                    }
                })
            }, 1000)

            console.log("进来了")
        } else {
            //初始化表格
            OrderService.initBootstrapTable()
        }

        //查看按钮点击
        $('body').on('click', '.seeBtn', function () {
            var id = $(this).attr('data-id');
            var page = 1;
            $('.pagination').find('.page-number').each(function (index, item) {
                if ($(item).hasClass('active')) {
                    page = $(item).find('a').text()
                }
            })
            var size = $('#pagesize_dropdown').val();
            localStorage.setItem('PageIndex', page);
            localStorage.setItem('PageSize', size);
            location.href = '/order/orderServiceDetail?id=' + id + ''
        })


        // 分页条数设置
        $("#pagesize_dropdown").on("change",function(){
            OrderService.projectDectoryQuery();
        });
        //关闭弹窗
        $(".mask").on("click",".close",function(){
            $(".mask").hide();
            OrderService.allList="";
        });
        //关闭弹窗
        $(".mask").on("click","#cancel",function(){
            $(".mask").hide();
            OrderService.allList="";
        });
        //点击审核
        $('body').on('click','.examain',function(){
            $('.mask').show();
            $('.checkBox').show();
            $('.deliverBox').hide();
            OrderService.singleId = [$(this).attr('data-id')];
            OrderService.allList="";
        });
        //批量审核
        $('body').on('click','#check',function(){
            $('.mask').show();
            $('.checkBox').show();
            $('.deliverBox').hide();
            var list = [];
            $('.checkbox').each(function(index,item){
                if(this.checked){
                    if($(item).attr('data-state') == "1"){
                        list.push($(item).attr('data-id'))
                    }
                }
            })
            OrderService.singleId = "";
            OrderService.allList= list ;
        });
        //审核点击确认
        $('.mask').on('click','#confirm',function(){
            var state = $('input[name=saleState]:checked').val();
            var marker = $('.remark').val();
            var address = $('#address').val();
            var mobile = $('#phoneNumber').val();
            var consignee = $('#userName').val();
            //地址
            if (!Validate.emptyValidateAndFocus("#address", "请输入地址", "")) {
                return false;
            }
            //手机
            if(!(/^0\d{2,3}-?\d{7,8}$/.test($('#phoneNumber').val())) && !(/^1[3|7|5|8]\d{9}$/.test($('#phoneNumber').val()))){

                Common.showInfoMsg('请输入正确的联系电话');
                return false;
            }
            //联系人
            if (!Validate.emptyValidateAndFocus("#userName", "请输入联系人", "")) {
                return false;
            }
            if(OrderService.allList){
                var list = OrderService.allList
            }else if(OrderService.singleId){
                var list = OrderService.singleId
            }

            if(list.length>0){
                OrderService.adminAuditAfterSalesService(list,state,marker,address,mobile,consignee)
            }else{
                Common.showInfoMsg("选中的订单并无审核选项")
                $('.mask').hide();
            }
        });
        //点击收货
        $('body').on('click','.getProduct',function(){
            var id = $(this).attr('data-id');
            OrderService.adminBeenShipped(id)
        });
        //点击发货按钮
        $('body').on('click','.sendProduct',function(){
            $('.mask').show();
            $('.checkBox').hide();
            $('.deliverBox').show();
            localStorage.setItem('AsId',$(this).attr('data-id'));
            OrderService.adminGetDeliveryList()

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
            OrderService.adminSendGoods()

        })
        //点击完成
        $('body').on('click','.action',function(){
            var id = $(this).attr('data-id');
            OrderService.adminCompleteAfterSales(id)
        });
        //全选
        $('body').on('click','#stuCheckBox',function(){

            if(this.checked){
                $('.checkbox').each(function(index,item){
                    if($(item).attr('data-state') == "1"){
                        this.checked = true;
                    }
                })
            }else{
                $('.checkbox').each(function(index,item){
                    this.checked = false;
                })
            }

        });
        //点击删除
        $('body').on('click','#delete',function(){
            var list = [];
            $('.order_checkbox').each(function(index,item){
                if(this.checked){
                    list.push($(item).attr('data-id'))
                }
            })
            OrderService.adminDelAfterSalesService(list)
        });
        //点击查询按钮
        $('body').on('click','#search',function(){
            localStorage.setItem('PageIndex', 1)
            OrderService.projectDectoyrQuery()
        });
        //点击导出数据
        $('body').on('click','#export',function(){
            var ids = [];
            $('.order_checkbox').each(function(index,item){
                if(this.checked){
                    ids.push($(item).attr('data-id'))
                }
            });
            OrderService.exAdminReturnApplyListing(ids)

        })
        Common.initLaydateWithTime();
        OrderService.initBootstrapTable()
    },
    //完成操作
    adminCompleteAfterSales:function(asSid){
        var methodName = "/aftersalesservice/AdminCompleteAfterSales";
        var data = {
            "asSid":asSid
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('成功!',function(){
                    OrderService.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //商城收货
    adminBeenShipped:function(asSid){
        var methodName = "/aftersalesservice/AdminBeenShipped";
        var data = {
            "asSid":asSid
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('收货成功!',function(){
                    OrderService.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //导出
    exAdminReturnApplyListing:function(ids){
        var methodName = "/aftersalesservice/ExAdminReturnApplyListing";
        var data = {
            "OSn": $('#osnId').val(),
            "AsState": $('#state_Box').val(),
            "StartTime": $('#start').val(),
            "EndTime": $('#end').val(),
            "AsType": 1,
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                location.href = data.Data;

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //删除
    adminDelAfterSalesService:function(asSid){
        var methodName = "/aftersalesservice/AdminDelAfterSalesService";
        var data = {
            "asSid":asSid
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功!',function(){
                    OrderService.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //审核
    adminAuditAfterSalesService:function(asSid,state,marker,address,mobile,consignee){
        var methodName = "/aftersalesservice/AdminAuditAfterSalesService";
        var data = {
            "AsSid":asSid,
            "AuditState": state,
            "AuditRemark": marker,
            "Address": address,
            "Mobile": mobile,
            "Consignee": consignee
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('审核成功!',function(){
                    OrderService.projectQuery();
                    $('.mask').hide()
                    OrderService.clearData();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //清空数据
    clearData:function(){
        $('#phoneNumber').val("");
        $('#userName').val("");
        $('#address').val("");
        $('.remark').val("");
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
                var render = template.compile(OrderService.expressCompanyTemplate);
                var html = render(data.Data);
                $('#logisticsCompany').html(html);
                var render1 = template.compile(OrderService.productTemplate);
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
                    OrderService.projectQuery();
                    $('.mask').hide()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#productTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/aftersalesservice/AdminReturnApplyListingList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: OrderService.flag ? localStorage.getItem('PageIndex') : 1,
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
            queryParams: OrderService.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: OrderService.responseHandler,
            columns: [
                {
                    field: 'AsSid',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-id="'+ value +'" data-state="'+row.AsState+'">'
                        return html;
                    }
                },
                {
                    field: 'Osn',
                    title: '订单编号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<a style="color:#44b8fd" href="/order/orderServiceDetail?id=' + row.AsSid + '">' + value + '</a>';

                        return e;
                    }
                },
                {
                    field: 'Name',
                    title: '商品',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e =
                            '<span class="sorting_1"style="display:block;height:20px;">' + value + '</span>';

                        return e;
                    }
                },

                {
                    field: 'Money',
                    title: '金额',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e =
                            '<span class="sorting_1"style="display:block;height:20px;">' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'SendCount',
                    title: '数量',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'AsTypeDec',
                    title: '类型',
                    align: 'center',
                    valign: 'middle',

                },
                {
                    field: 'AsStateDec',
                    title: '状态',
                    align: 'center',
                    valign: 'middle',

                },
                {
                    field: 'ApplyTimec',
                    title: '申请时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var value = moment(value).format('YYYY-MM-DD HH:mm:ss')

                        var e = '<span style="display:block;">' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'AsState',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        if(value == "1"){
                            var html =
                                '<a class="seeBtn" href="javascript:void(0);" style="color:#44b8fd;cursor: pointer;"  data-id="' + row.AsSid + '" >详情</a>' +
                                '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer" class="examain" data-id="' + row.AsSid + '">审核</span>';
                        }else if(value == "2"){
                            var html =
                                '<a class="seeBtn" href="javascript:void(0);" style="color:#44b8fd;cursor: pointer;"  data-id="' + row.AsSid + '" >详情</a>';
                        }else if(value == "3"){
                            var html =
                                '<a class="seeBtn" href="javascript:void(0);" style="color:#44b8fd;cursor: pointer;"  data-id="' + row.AsSid + '" >详情</a>';
                        }else if(value == "4"){
                            var html =
                                '<a class="seeBtn" href="javascript:void(0);" style="color:#44b8fd;cursor: pointer;"  data-id="' + row.AsSid + '" >详情</a>' +
                                '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer" data-state="'+row.orderstate+'" class="getProduct"  data-id="' + row.AsSid + '">收货</span>';
                        }else if(value == "5"){
                            var html =
                                '<a class="seeBtn" href="javascript:void(0);" style="color:#44b8fd;cursor: pointer;"  data-id="' + row.AsSid + '" >详情</a>' +
                                '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer" data-state="'+row.orderstate+'" class="sendProduct"  data-id="' + row.AsSid + '">发货</span>';
                        }else if(value == "6"){
                            var html =
                                '<a class="seeBtn" href="javascript:void(0);" style="color:#44b8fd;cursor: pointer;"  data-id="' + row.AsSid + '" >详情</a>' +
                                '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer" data-state="'+row.Orderstate+'" class="action"  data-id="' + row.AsSid + '">完成</span>';
                        }else if(value == "7"){
                            var html =
                                '<a class="seeBtn" href="javascript:void(0);" style="color:#44b8fd;cursor: pointer;"  data-id="' + row.AsSid + '" >详情</a>';

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
                if (OrderService.flag) {
                    OrderService.flag = false
                }
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
        var methodName = "/aftersalesservice/AdminReturnApplyListingList";

        if (OrderService.flag) {
            var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                minSize: $("#leftLabel").val(),
                maxSize: $("#rightLabel").val(),
                minPrice: $("#priceleftLabel").val(),
                maxPrice: $("#pricerightLabel").val(),
                Page: {
                    PageSize: params.limit,
                    PageIndex: localStorage.getItem('PageIndex'),
                },
                "OSn": $('#osnId').val(),
                "AsState": $('#state_Box').val(),
                "StartTime": $('#start').val(),
                "EndTime": $('#end').val(),
                "AsType": 1
            };
        } else {
            var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                minSize: $("#leftLabel").val(),
                maxSize: $("#rightLabel").val(),
                minPrice: $("#priceleftLabel").val(),
                maxPrice: $("#pricerightLabel").val(),
                Page: {
                    PageSize: params.limit,
                    PageIndex: (params.offset / params.limit) + 1,
                },
                "OSn": $('#osnId').val(),
                "AsState": $('#state_Box').val(),
                "StartTime": $('#start').val(),
                "EndTime": $('#end').val(),
                "AsType": 1
            };
        }
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.ReturnApplyListingList,
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
            var obj = {
                "OSn": $('#osnId').val(),
                "AsState": $('#state_Box').val(),
                "StartTime": $('#start').val(),
                "EndTime": $('#end').val(),
                "AsType": 1,
            };
        } else {
            var obj = parame;
        }

        $('#productTable').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/aftersalesservice/AdminReturnApplyListingList',
                query: obj
            }
        );

    },
    //表格刷新
    projectDectoyrQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                "OSn": $('#osnId').val(),
                "AsState": $('#state_Box').val(),
                "StartTime": $('#start').val(),
                "EndTime": $('#end').val(),
                "AsType": 1,
                page: {
                    PageSize: $("#pagesize_dropdown").val(),
                    PageIndex: 1
                }
            };
        } else {
            var obj = parame;
        }

        $('#productTable').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/aftersalesservice/AdminReturnApplyListingList',
                query: obj
            }
        );
        OrderService.initBootstrapTable()

    },
}