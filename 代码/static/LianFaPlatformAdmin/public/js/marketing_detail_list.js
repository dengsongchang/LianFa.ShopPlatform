var MarketingDetailList = {
    couponTypeId:"",
    //订单编号
    orderNsn:"",
    //状态
    state:"0",
    init:function(){
        //获取优惠券Id
        MarketingDetailList.couponTypeId = Common.getUrlParam("CouponTypeId");
        MarketingDetailList.initBootstrapTable();

        $('body').on('click','#search_coupon',function(){
            var state = $('#stateList').find('option:selected').val();
            MarketingDetailList.state=state;
            MarketingDetailList.orderNsn = $('#coupon_name').val();
            MarketingDetailList.projectQuery();
        })
        //返回上一页
        $('body').on('click','.goBackbtn',function(){
            window.history.go(-1);
        })

    },
    //bootstrapTable
    initBootstrapTable: function () {
        $("#tb_coupon_list").bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/Coupon/AdminCouponList',
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
            queryParams: MarketingDetailList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: MarketingDetailList.responseHandler,
            columns:
                [
                    {
                        field: 'CouponSn',
                        title: '优惠码',
                        align: 'center',
                        valign: 'middle',
                    },
                    {
                        field: 'UserName',
                        title: '领取会员',
                        align: 'center',
                        valign: 'middle',

                    },
                    {
                        field: 'CreateTime ',
                        title: '领取日期',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            var start = moment(row.CreateTime).format('YYYY-MM-DD HH:mm:ss');
                            var html = `<span>${start}</span>
                            `
                            return html
                        }

                    },
                    {
                        field: 'UseTime',
                        title: '使用日期',
                        align: 'center',
                        valign: 'middle',
                        formatter: function (value, row, index) {
                            if(value == "1900-01-01T00:00:00"){
                                var start = "未使用";
                            }else{
                                var start = moment(row.UseTime).format('YYYY-MM-DD HH:mm:ss');
                            }

                            var html = `<span>${start}</span>
                            `
                            return html
                        }

                    },

                    {
                        field: 'State',
                        title: '状态',
                        align: 'center',
                        valign: 'middle',

                    },
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
            OrderNsn :MarketingDetailList.orderNsn,
            State:MarketingDetailList.state,
            CouponTypeId:MarketingDetailList.couponTypeId,
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
                "rows": res.Data.CouponList,
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
    projectQuery: function (parame,id) {
        console.log(id)
        if (parame == "" || parame == undefined) {
            var obj = {
                OrderNsn:MarketingDetailList.orderNsn,
                State:MarketingDetailList.state,
                CouponTypeId:MarketingDetailList.couponTypeId
            };
        } else {
            var obj = parame;
        }


        $("#tb_coupon_list").bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/Coupon/AdminCouponList',
                query: obj
            }
        );
    },
}

$(function() {

    MarketingDetailList.init();

});