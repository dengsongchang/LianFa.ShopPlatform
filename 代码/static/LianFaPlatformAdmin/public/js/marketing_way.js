var MarketingWay = {
    //优惠券类型名称
    couponTypeName: "",
    //优惠券类型状态
    state: "0",
    //发放方式(0代表免费领取,1代表手动发放)
    sendMode: "0",
    //订单金额是否没限制
    limited: true,
    //是否限制商品
    isProduct: "0",
    //领取方式
    sendModel: "0",
    //编辑的时候记录id
    couponId: "",
    //已选中的商品列表
    hasChooseProductList: [],
    PageIndex:1,
    //表格模板
    tableTemplate: `
                                <tbody>
                                    <tr>
                                        <th width="85%">商品名称</th>
                                        <th width="15%">操作</th>
                                    </tr>
                                    {{each CouponProductsShowList as value i }}
                                        <tr name="appendlist" style="display: table-row;">
                                            <td>{{CouponProductsShowList[i].Name}}</td>
                                            <td>
                                                <input type="hidden" value="204" id="hidProduct_204">
                                                <span class="icon_close" data-id="{{CouponProductsShowList[i].PId}}"></span>
                                            </td>
                                        </tr>
                                    {{/each}}
                                </tbody>
    `,
    //表格模板编辑
    tableEditTemplate: `
                                <tbody>
                                    <tr>
                                        <th width="85%">商品名称</th>
                                        <th width="15%">操作</th>
                                    </tr>
                                    {{each CouponProductsShowList as value i }}
                                        <tr name="appendlist" style="display: table-row;">
                                            <td>{{CouponProductsShowList[i].Name}}</td>
                                            <td>
                                                <input type="hidden" value="204" id="hidProduct_204">
                                                <span class="icon_closeEdit" data-id="{{CouponProductsShowList[i].PId}}"></span>
                                            </td>
                                        </tr>
                                    {{/each}}
                                </tbody>
    `,

    init: function () {
        //上传小图标

        MarketingWay.initProBootstrapTable();
        //添加优惠券模态框出现的时候
        $('#coupon_add_modal').on('show.bs.modal', function () {
            uploadIconPic('#small_upload_pick', '#small_icon', '/CouponType/AdminUploadCouponTypeImg');
            MarketingWay.hasChooseProductList = [];
            MarketingWay.PageIndex = 1;
            $('#addlist').html("");
            $('#SpecifyProductBox').hide();
            //把模态框内的内容清空
            MarketingWay.resetData();
        });

        //全选
        $("#selAll").on("change", function () {
            if ($(this).is(':checked')) {
                $(".checkbox").prop("checked", true);
            }
            else {
                $(".checkbox").prop("checked", false);
            }
        });

        // 分页条数设置
        $("#pagesize_dropdown").on("change", function () {
            MarketingWay.projectDestoryQuery();
        });
        //领取方式改变
        $('body').on('change', '#payment', function () {
            MarketingWay.sendModel = $('#payment').find('option:selected').val();
        })
        //状态改变
        $('body').on('change', '#state_box', function () {
            MarketingWay.state = $('#state_box').find('option:selected').val();
        })

        //保存按钮点击
        $('body').on('click', '#saveData', function () {
            if ($('#small_icon').attr('data-src') == "" || $('#small_icon').attr('data-src') == null) {
                Common.showInfoMsg('请上传封面图')
                return false;
            }
            MarketingWay.adminAddCouponType();
        });
        //点击查询按钮
        $('body').on("click", '#search_coupon', function () {
            var data = {
                CouponTypeName: $('#coupon_name').val(),
                SendMode: $('#payment').find('option:selected').val(),
                State: $('#state_box').find('option:selected').val(),
                Page: {
                    PageSize: $('#pagesize_dropdown').val(),//页面大小,
                    PageIndex: 1,//页码
                }

            }
            MarketingWay.projectQuery(data)
        });
        //点击编辑
        $('body').on('click', '.editCoupon', function () {
            var id = $(this).attr("data-id");
            uploadIconPic('#small_upload_pick2', '#small_icon2', '/CouponType/AdminUploadCouponTypeImg');
            MarketingWay.couponId = id;
            MarketingWay.adminEditCouponTypeInfo(id)
        });
        //编辑模态框的确认按钮
        $('body').on("click", '#saveEditData', function () {
            if ($('#small_icon2').attr('data-src') == "" || $('#small_icon2').attr('data-src') == null) {
                Common.showInfoMsg('请上传封面图')
                return false;
            }
            MarketingWay.adminEditCouponType()
        });


        //单选
        $('input[type="radio"].flat-green').iCheck({
            // checkboxClass: 'icheckbox_flat-green',
            radioClass: 'iradio_flat-green'
        });
        //订单金额是否没限制
        $('#unlimited').on('ifChecked', function (e) {
            MarketingWay.limited = true;
            console.log(MarketingWay.limited)
        })
        $('#unlimited').on('ifUnchecked', function (e) {
            MarketingWay.limited = false;
            console.log(MarketingWay.limited)
        });

        //是否限制商品
        $('#fullField').on('ifChecked', function (e) {
            MarketingWay.isProduct = 0;
            console.log(MarketingWay.isProduct)
            $('#chooseProductBtn').hide();
            MarketingWay.hasChooseProductList = [];
        });
        //指定商品点击
        $('#fullField').on('ifUnchecked', function (e) {
            MarketingWay.isProduct = 1;
            $('#chooseProductBtn').show();
            MarketingWay.projectProQuery();
        });
        //选择商品按钮点击
        $('body').on('click', '#chooseProductBtn', function () {
            $('#choicePresentModal').modal('show');
            MarketingWay.projectDestoryQuery();
        })
        //上一页按钮点击
        $('body').on('click','#btnPrePage',function(){
            //获取当前页面
            var PageIndex = MarketingWay.PageIndex;
            if(PageIndex > 1){
                //大于一才减
                MarketingWay.PageIndex = PageIndex - 1;
                //调用获取数据的接口
                MarketingWay.couponProductShowList([]);
            }else{
                return false
            }
        })

        //下一页按钮点击
        $('body').on('click','#btnNextPage',function(){
            //获取当前页面
            var PageIndex = MarketingWay.PageIndex;
            //算一个总页数
            var totalIndex = MarketingWay.totalIndex
            if(PageIndex < totalIndex){
                //不大于总页数才增加
                MarketingWay.PageIndex = PageIndex + 1;
                //调用获取数据的接口
                MarketingWay.couponProductShowList([]);
            }else{
                return false
            }
        });
        //编辑
        //上一页按钮点击
        $('body').on('click','#btnPrePageEdit',function(){
            //获取当前页面
            var PageIndex = MarketingWay.PageIndex;
            if(PageIndex > 1){
                //大于一才减
                MarketingWay.PageIndex = PageIndex - 1;
                //调用获取数据的接口
                MarketingWay.couponProductShowList([],true);
            }else{
                return false
            }
        })

        //下一页按钮点击
        $('body').on('click','#btnNextPageEdit',function(){
            //获取当前页面
            var PageIndex = MarketingWay.PageIndex;
            //算一个总页数
            var totalIndex = MarketingWay.totalIndex
            if(PageIndex < totalIndex){
                //不大于总页数才增加
                MarketingWay.PageIndex = PageIndex + 1;
                //调用获取数据的接口
                MarketingWay.couponProductShowList([],true);
            }else{
                return false
            }
        });

        //查看

        $('body').on('click','#btnPrePagereadonly',function(){
            //获取当前页面
            var PageIndex = MarketingWay.PageIndex;
            if(PageIndex > 1){
                //大于一才减
                MarketingWay.PageIndex = PageIndex - 1;
                //调用获取数据的接口
                MarketingWay.couponProductShowList([],true,true);
            }else{
                return false
            }
        })

        //下一页按钮点击
        $('body').on('click','#btnNextPagereadonly',function(){
            //获取当前页面
            var PageIndex = MarketingWay.PageIndex;
            //算一个总页数
            var totalIndex = MarketingWay.totalIndex
            if(PageIndex < totalIndex){
                //不大于总页数才增加
                MarketingWay.PageIndex = PageIndex + 1;
                //调用获取数据的接口
                MarketingWay.couponProductShowList([],true,true);
            }else{
                return false
            }
        });
        //商品删除按钮点击
        $('body').on('click','.icon_close',function(){
            var id = $(this).attr('data-id');
            var hasChooseProductList = MarketingWay.hasChooseProductList;
            var Index = 0;
            hasChooseProductList.forEach(function(item,index){
                if(item == id){
                    Index = index
                }
            })
            hasChooseProductList.splice(Index,1)
            //获取当前剩余量
            var length = $('#addlist').find('tr[name="appendlist"]').length;
            if(length == 1 && MarketingWay.PageIndex > 1){
                //如果只剩余一条，并且当前页面大于1的话
                MarketingWay.PageIndex -= 1;
            }
            MarketingWay.hasChooseProductList = hasChooseProductList;
            MarketingWay.couponProductShowList([]);

        })

        //确认按钮点击
        $('body').on('click','#confirmBtn',function(){
            var list = [];
            $('.checkbox').each(function(index,item){
                if(this.checked){
                    list.push($(item).attr('data-id'))
                }
            })
            if(list.length > 0 ){

                MarketingWay.couponProductShowList(list)

            }else{
                Common.showInfoMsg('请选择商品')
                return false;
            }
            console.log(list)
        })



        //领取方式
        $('#activeClaim').on('ifChecked', function (e) {
            MarketingWay.sendModel = 0;
            console.log(MarketingWay.sendModel)
        });
        $('#activeClaim').on('ifUnchecked', function (e) {
            MarketingWay.sendModel = 1;
            console.log(MarketingWay.sendModel)
        });

        $('#unlimited_edit').on('ifChecked', function (e) {
            $('#fullnumber_edit').val("");

        })
        //使失效
        $('body').on("click", '.takeEffect', function () {
            var id = $(this).attr("data-id");
            MarketingWay.adminCouponTypeState(id)
        });
        //删除按钮
        $('body').on('click', '.delectCoupon', function () {
            var id = $(this).attr("data-id");
            Common.confirmDialog("是否确认删除？", function () {
                MarketingWay.adminDelCouponType(id)
            })

        })
        //查看按钮点击
        $('body').on("click", '.look', function () {
            var id = $(this).attr("data-id");
            MarketingWay.adminEditCouponTypeInfo(id, true)
        });


        //有效期
        laydate.render({
            elem: '#start_time', //指定元素

        });
        laydate.render({
            elem: '#end_time', //指定元素
        });
        //有效期
        laydate.render({
            elem: '#start_time_edit', //指定元素
        });
        laydate.render({
            elem: '#end_time_edit', //指定元素
        });

        //发放时间
        laydate.render({
            elem: '#sendStart_time_add', //指定元素

        });
        laydate.render({
            elem: '#sendEnd_time_add', //指定元素
        });
        //有效期
        laydate.render({
            elem: '#sendStart_time_edit', //指定元素
        });
        laydate.render({
            elem: '#sendEnd_time_edit', //指定元素
        });

        MarketingWay.initBootstrapTable()
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $("#tb_coupon_list").bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/CouponType/AdminCouponTypeList',
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
            queryParams: MarketingWay.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: MarketingWay.responseHandler,
            columns:
                [
                    {
                        field: 'Name',
                        title: '优惠券名称',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'Money',
                        title: '面值(元)',
                        align: 'center',
                        valign: 'middle',

                    },
                    {
                        field: 'GetMode',
                        title: '使用条件',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (value == "0") {
                                return "无限制"
                            } else if (value == "1") {
                                return "限领一张"
                            } else if (value == "2") {
                                return "每天限领一张"
                            }
                        }
                    },
                    {
                        field: 'SendStartTime ',
                        title: '有效期',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            var start = moment(row.UseStartTime).format('YYYY-MM-DD');
                            var end = moment(row.UseEndTime).format('YYYY-MM-DD');
                            var html = `<span>${start} 至 ${end}</span>
                            `
                            console.log(html)
                            return html
                        }

                    },
                    {
                        field: 'RemainCount',
                        title: '剩余数量',
                        align: 'center',
                        valign: 'middle',

                    },
                    {
                        field: 'SendCount',
                        title: '领取人/张',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            var html = `
                            <span>${value}/${value}<span>
                            `
                            return html
                        }

                    },
                    {
                        field: 'SendCount',
                        title: '已使用',
                        align: 'center',
                        valign: 'middle',

                    },
                    {
                        field: 'State',
                        title: '操作',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if (row.State == 1) {
                                var html = `
                                    <!--<a data-toggle="modal" data-target="#coupon_link_box">链接</a>-->
                                    <a href="/marketMode/marketing_detail_list?CouponTypeId=${row.CouponTypeId}">活动详情</a>
                                    <a data-toggle="modal" class="editCoupon" data-target="#coupon_edit_modal" data-id="${row.CouponTypeId}">编辑</a>
                                    <a class="takeEffect" data-id="${row.CouponTypeId}">使失效</a>
                                `
                            }
                            else {
                                var html = `
                                    <a href="/marketMode/marketing_detail_list?CouponTypeId=${row.CouponTypeId}">活动详情</a>
                                    <a data-id="${row.CouponTypeId}" data-toggle="modal" class="look" data-target="#coupon_readonly_modal">查看</a>
                                    <a class="delectCoupon" data-id="${row.CouponTypeId}">删除</a>
                                `
                            }
                            return html
                        }

                    }
                ],
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
                $('#tb_coupon_list').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法
            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {

            },
            onUncheckAll: function (rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function (row) {


            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {
                Array.prototype.remove = function (val) {
                    var index = this.indexOf(val);
                    if (index > -1) {
                        this.splice(index, 1);
                    }
                };

            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryParams: function (params) {
        //配置参数
        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            CouponTypeName: MarketingWay.couponTypeName,
            State: MarketingWay.state,
            SendMode: MarketingWay.sendMode,
            Page: {
                PageSize: params.limit, //页面大小,
                PageIndex: (params.offset / params.limit) + 1 //页码
            }
        };
        console.log(temp)
        return temp;

    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        console.log(res)
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.CouponTypeList,
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
    projectQuery: function (parame, id) {
        console.log(id)
        if (parame == "" || parame == undefined) {
            var obj = {
                CouponTypeName: $("#coupon_name").val(),
                State: $("#state_box").val(),
                SendMode: $("#payment").val(),
            };
        } else {
            var obj = parame;
        }


        $("#tb_coupon_list").bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/CouponType/AdminCouponTypeList',
                query: obj
            }
        );
    },
    //后台添加优惠劵类型
    adminAddCouponType: function () {
        //请求方法
        var methodName = "/CouponType/AdminAddCouponType";
        var data = {
            Name: $('#coupon_name_add').val(),
            Money: $('#denomination').val(),
            Count: Math.ceil($('#TotalAmount').val()),
            GetMode: $('#GetModel').find("option:selected").val(),
            OrderMoney: MarketingWay.limited ? "0" : $('#fullnumber').val(),
            UseStartTime: $('#start_time').val(),
            UseEndTime: $('#end_time').val(),
            SendStartTime: $('#sendStart_time_add').val(),
            SendEndTime: $('#sendEnd_time_add').val(),
            IsProduct: $('input:radio[name="limitProduct"]:checked').val(),
            SendMode: $('input:radio[name="SendModeRadio"]:checked').val(),
            ShowImg: $('#small_icon').attr('data-src') ? $('#small_icon').attr('data-src') : "",
            LimitPIdList:MarketingWay.hasChooseProductList,
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg("添加优惠券成功!", function () {
                    var data = {
                        CouponTypeName: "",
                        State: "0",
                        SendMode: "0",
                    }
                    MarketingWay.projectQuery(data)
                    $('#coupon_add_modal').modal('hide');
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //添加优惠券模态框消失的时候数据重置
    resetData: function () {
        //全场通用
        $('#fullField').iCheck('check');
        //主动领取
        $('#activeClaim').iCheck('check');
        //订单金额无限制
        $('#unlimited').iCheck('check');
        $('#fullnumber').val("");
        //日期清空
        $('#start_time').val("");
        $('#end_time').val("");
        $('#sendStart_time_add').val("");
        $('#sendEnd_time_add').val("");

        $('#small_icon').attr('data-src', "");
        $('#small_icon').attr('src', "/public/images/addImg.png");
        //每人限领
        $('#GetModel').val('0');
        //发放总量
        $('#TotalAmount').val("");
        //面值
        $('#denomination').val("");
        //优惠券名称
        $('#coupon_name_add').val("");
    },
    //后台优惠劵类型信息
    adminEditCouponTypeInfo: function (id, islook) {
        //请求方法
        var methodName = "/CouponType/AdminCouponTypeInfo";
        var data = {
            CouponTypeId: id
        };
        console.log(data);
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data);
            if (data.Code == "100") {
                if (islook) {
                    $('#coupon_name_readonly').val(data.Data.CouponTypeInfo.Name);
                    $('#denomination_readonly').val(data.Data.CouponTypeInfo.Money);
                    $('#TotalAmount_readonly').val(data.Data.CouponTypeInfo.Count);
                    //每人限领
                    $('#GetModel_readonly').val(data.Data.CouponTypeInfo.GetMode);
                    //订单金额
                    if (data.Data.CouponTypeInfo.OrderAmountLower == "0") {
                        $('#unlimited_readonly').iCheck('enable');
                        $('#unlimited_readonly').iCheck('check');
                        $('#full_readonly').iCheck('disable');
                    } else {
                        $('#full_readonly').iCheck('enable');
                        $('#full_readonly').iCheck('check');
                        $('#unlimited_readonly').iCheck('disable');
                        $('#fullnumber_readonly').val(data.Data.CouponTypeInfo.OrderAmountLower);
                    }
                    //有效期
                    $('#start_time_readonly').val(moment(data.Data.CouponTypeInfo.UseStartTime).format("YYYY-MM-DD"));
                    $('#end_time_readonly').val(moment(data.Data.CouponTypeInfo.UseEndTime).format("YYYY-MM-DD"));

                    //发放时间
                    $('#sendStart_time_readonly').val(moment(data.Data.CouponTypeInfo.SendStartTime).format("YYYY-MM-DD"));
                    $('#sendEnd_time_readonly').val(moment(data.Data.CouponTypeInfo.SendEndTime).format("YYYY-MM-DD"));
                    //可使用商品
                    if (data.Data.CouponTypeInfo.LimitProduct == "0") {
                        $('#fullField_readonly').iCheck('enable');
                        $('#fullField_readonly').iCheck('check');
                        $('#designated_readonly').iCheck('disable');
                        // $('#fullField_readonly').iCheck('disable');
                        MarketingWay.hasChooseProductList = [];
                        MarketingWay.PageIndex  =1;
                        $('#SpecifyProductBoxreadonly').hide()

                    } else {
                        //指定商品
                        $('#designated_readonly').iCheck('enable');
                        $('#designated_readonly').iCheck('check');
                        $('#fullField_readonly').iCheck('disable');
                        // $('#designated_readonly').iCheck('disable');
                        MarketingWay.hasChooseProductList = data.Data.CouponTypeInfo.LimitPIdList;
                        MarketingWay.PageIndex  =1;
                        MarketingWay.couponProductShowList([],true,true)
                        $('#SpecifyProductBoxreadonly').show()


                    }
                    //领取方式
                    if (data.Data.CouponTypeInfo.SendMode == "1") {
                        $('#activeClaim_readonly').iCheck('enable');
                        $('#activeClaim_readonly').iCheck('check');
                        $('#designated_send_readonly').iCheck('disable');
                    } else {
                        $('#designated_send_readonly').iCheck('enable');
                        $('#designated_send_readonly').iCheck('check');
                        $('#activeClaim_readonly').iCheck('disable');
                    }
                    //为图片赋值
                    $('#seeSmall_icon').attr('data-src', data.Data.CouponTypeInfo.ShowImg)
                    $('#seeSmall_icon').attr('src', data.Data.CouponTypeInfo.ShowImgFull)

                } else {
                    $('#coupon_name_edit').val(data.Data.CouponTypeInfo.Name);
                    $('#denomination_edit').val(data.Data.CouponTypeInfo.Money);
                    $('#TotalAmount_edit').val(data.Data.CouponTypeInfo.Count);
                    //每人限领
                    $('#GetModel_edit').val(data.Data.CouponTypeInfo.GetMode);
                    //订单金额
                    if (data.Data.CouponTypeInfo.OrderAmountLower == "0") {
                        $('#unlimited_edit').iCheck('check');
                    } else {
                        $('#full_edit').iCheck('check');
                        $('#fullnumber_edit').val(data.Data.CouponTypeInfo.OrderAmountLower);
                    }
                    //有效期
                    $('#start_time_edit').val(moment(data.Data.CouponTypeInfo.UseStartTime).format("YYYY-MM-DD"));
                    $('#end_time_edit').val(moment(data.Data.CouponTypeInfo.UseEndTime).format("YYYY-MM-DD"));

                    //发放时间
                    $('#sendStart_time_edit').val(moment(data.Data.CouponTypeInfo.SendStartTime).format("YYYY-MM-DD"));
                    $('#sendEnd_time_edit').val(moment(data.Data.CouponTypeInfo.SendEndTime).format("YYYY-MM-DD"));
                    //可使用商品
                    if (data.Data.CouponTypeInfo.LimitProduct == "0") {
                        // $('#fullField_edit').iCheck('disable');
                        $('#fullField_edit').iCheck('enable');
                        $('#fullField_edit').iCheck('check');
                        $('#designated_edit').iCheck('disable');
                        MarketingWay.hasChooseProductList = [];
                        MarketingWay.PageIndex  =1;
                        $('#SpecifyProductBoxEdit').hide()
                    } else {
                        $('#fullField_edit').iCheck('disable');
                        $('#designated_edit').iCheck('enable');
                        $('#designated_edit').iCheck('check');
                        MarketingWay.hasChooseProductList = data.Data.CouponTypeInfo.LimitPIdList;
                        MarketingWay.PageIndex  =1;
                        MarketingWay.couponProductShowList([],true)
                        $('#SpecifyProductBoxEdit').show()
                    }
                    //领取方式
                    if (data.Data.CouponTypeInfo.SendMode == "1") {
                        $('#activeClaim_edit').iCheck('check');
                    } else {
                        $('#designated_send_edit').iCheck('check');
                    }

                    //为图片赋值
                    $('#small_icon2').attr('data-src', data.Data.CouponTypeInfo.ShowImg)
                    $('#small_icon2').attr('src', data.Data.CouponTypeInfo.ShowImgFull)

                }


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台编辑优惠劵类型
    adminEditCouponType: function () {
        //请求方法
        var methodName = "/CouponType/AdminEditCouponType";
        var data = {
            CouponTypeId: MarketingWay.couponId,
            Name: $('#coupon_name_edit').val(),
            Money: $('#denomination_edit').val(),
            Count: Math.ceil($('#TotalAmount_edit').val()),
            GetMode: $('#GetModel_edit').find("option:selected").val(),
            UseStartTime: $('#start_time_edit').val(),
            UseEndTime: $('#end_time_edit').val(),
            SendStartTime: $('#sendStart_time_edit').val(),
            SendEndTime: $('#sendEnd_time_edit').val(),
            SendMode: $('input:radio[name="order_taking_w"]:checked').val(),
            ShowImg: $('#small_icon2').attr('data-src') ? $('#small_icon2').attr('data-src') : "",
            LimitPIdList:MarketingWay.hasChooseProductList,
        };
        //订单金额
        if ($('#unlimited_edit').is(":checked")) {
            data.OrderMoney = "0";
        } else {
            data.OrderMoney = $('#fullnumber_edit').val();
        }
        //是否限制商品
        if ($('#fullField_edit').is(':checked')) {
            data.IsProduct = "0";
        } else {
            data.IsProduct = "1";
        }
        // //发放方式
        // if($('#activeClaim_edit').is(":checked")){
        //     data.SendModel = "0";
        // }else{
        //     data.SendModel = "1";
        // }
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data);
            if (data.Code == "100") {
                Common.showSuccessMsg("编辑成功!", function () {
                    //表格刷新
                    MarketingWay.projectQuery();
                    $('#coupon_edit_modal').modal('hide');
                });


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台更改优惠券状态
    adminCouponTypeState: function (id) {
        //请求方法
        var methodName = "/CouponType/AdminCouponTypeState";
        var data = {
            CouponTypeId: id,
        };
        console.log(data);
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data);
            if (data.Code == "100") {
                Common.showSuccessMsg("更改成功!", function () {
                    //表格刷新
                    MarketingWay.projectQuery();
                });

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台删除优惠劵类型
    adminDelCouponType: function (id) {
        //请求方法
        var methodName = "/CouponType/AdminDelCouponType";
        var data = {
            couponTypeIdList: [
                id
            ],
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg("删除成功!", function () {
                    //表格刷新
                    MarketingWay.projectQuery();
                });


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //优惠券商品展示列表
    couponProductShowList: function (list,isEdit,islook) {
        //请求方法
        var methodName = "/product/CouponProductShowList";
        var data = {
            "OldPIdList":MarketingWay.hasChooseProductList,
            "NewPIdList":list,
            "Page": {
                "PageSize": 5,
                "PageIndex": MarketingWay.PageIndex
            }
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                if(isEdit){
                    //编辑
                    var render = template.compile(MarketingWay.tableEditTemplate);
                    var html = render(data.Data);
                    $("#addlistEdit").html(html);
                    //计算总页数
                    var totalIndex = Math.ceil(data.Data.ProductsCount / 5);
                    MarketingWay.totalIndex = totalIndex;
                    $('.currentIndexEdit').text(MarketingWay.PageIndex)
                    $('#totalIndexEdit').text(totalIndex)
                    if(islook){
                        var render = template.compile(MarketingWay.tableEditTemplate);
                        var html = render(data.Data);
                        $("#addlistreadonly").html(html);
                        //计算总页数
                        var totalIndex = Math.ceil(data.Data.ProductsCount / 5);
                        MarketingWay.totalIndex = totalIndex;
                        $('.currentIndexreadonly').text(MarketingWay.PageIndex)
                        $('#totalIndexreadonly').text(totalIndex)
                    }
                }else{
                    $('#SpecifyProductBox').show()
                    $('#choicePresentModal').modal('hide');
                    var render = template.compile(MarketingWay.tableTemplate);
                    var html = render(data.Data);
                    $("#addlist").html(html);
                    MarketingWay.hasChooseProductList = data.Data.PIdList;
                    //计算总页数
                    var totalIndex = Math.ceil(data.Data.ProductsCount / 5);
                    MarketingWay.totalIndex = totalIndex;
                    $('.currentIndex').text(MarketingWay.PageIndex)
                    $('#totalIndex').text(totalIndex)
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //优惠券商品列表bootstrapTable
    initProBootstrapTable: function () {
        $('#choice_goods_tb').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/product/CouponProductList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $('#pagesize_dropdown').val(),
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
            queryParams: MarketingWay.queryProParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: MarketingWay.responseProHandler,
            columns: [
                {
                    field: 'PId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-id="' + value + '" style="display: inline-block;">'
                        return html;
                    }
                },
                {
                    field: 'ShowImg',
                    title: '图片',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '<img src="' + value + '" style="width: 80px;height: 80px;">'
                        return html;
                    }
                },
                {
                    field: 'Name',
                    title: '商品',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return `<span class="goods_name_modal">${value}</span>`;
                    }
                },
                {
                    field: 'ShopPrice',
                    title: '商品价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
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
                $('#recoveryTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法

            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {


            },
            onUncheckAll: function (rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function (row) {


            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {
                Array.prototype.remove = function (val) {
                    var index = this.indexOf(val);
                    if (index > -1) {
                        this.splice(index, 1);
                    }
                };

            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryProParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/product/CouponProductList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            "Name": $('#choice_presentName').val(),
            "PIdList":MarketingWay.hasChooseProductList,
            Page: {
                PageSize: params.limit,//页面大小,
                PageIndex: (params.offset / params.limit) + 1,//页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseProHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.ProductsList,
                "total": res.Data.ProductsCount
            };
        } else {
            return {
                "rows": [],
                "total": 0
            };
        }
    },
    //表格刷新(直接刷新)
    projectProQuery: function (parame) {
        //方法名
        var methodName = "/product/CouponProductList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "Name": $('#choice_presentName').val(),
                "PIdList":MarketingWay.hasChooseProductList
            };
        } else {
            var obj = parame;
        }

        $('#choice_goods_tb').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格先销毁刷新
    projectDestoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                "Name": $('#choice_presentName').val(),
                "PIdList":MarketingWay.hasChooseProductList,
                Page: {
                    PageSize: $('#pagesize_dropdown').val(),//页面大小,
                    PageIndex: 1,//页码
                }
            };
        } else {
            var obj = parame;
        }

        $('#choice_goods_tb').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/Order/AdminOrderList',
                query: obj
            }
        );
        MarketingWay.initProBootstrapTable()
    },

}

$(function () {

    MarketingWay.init()

});