var OrderRefundApplication = {
  init:function(){
      OrderRefundApplication.initBootstrapTable();
      //全选按钮
      $('body').on('click','.allCheck',function(){

          if(this.checked){
              $('#RefundBox input[type="checkbox"]').each(function (index, val) {
                  this.checked = true;
              });
          }else{
              $('#RefundBox input[type="checkbox"]').each(function (index, val) {
                  this.checked = false;
              });
          }

      });
      // 分页条数设置
      $("#pagesize_dropdown").on("change",function(){
          OrderRefundApplication.projectDectoryQuery();
      });
      //查询按钮点击
      $('body').on('click','#searchBtn',function(){
          OrderRefundApplication.projectDectoryQuery()
      });
      //删除按钮点击
      $('body').on('click','.deleteBtn',function(){
          var list = [];
          $('.checkbox').each(function(index,item){
              if(this.checked){
                  list.push($(item).attr('data-id'))
              }
          })
          OrderRefundApplication.adminDelOrderRefund(list)
      });
      //导出按钮点击
      $('body').on('click','.exportBtn',function(){
          var list = [];
          $('.checkbox').each(function(index,item){
              if(this.checked){
                  list.push($(item).attr('data-id'))
              }
          })
          OrderRefundApplication.exportAdminRefundList(list)
      });
      //通过按钮点击
      $('body').on('click','.actionBtn',function(){
          var id = $(this).attr('data-id');
          Common.confirmDialog('确认要通过?',function(){
              OrderRefundApplication.adminOrderRefund(id)
          })
      })
      //拒绝按钮点击
      $('body').on('click','.closeOrder',function(){
          var id = $(this).attr('data-id');
          Common.confirmDialog('确认要拒绝?',function(){
              OrderRefundApplication.adminReFuseOrderRefund(id)
          })
      })
  },
    //通过接口
    adminOrderRefund:function(RefundId){
        var methodName = "/orderRefund/AdminOrderRefund";
        var data = {
            "RefundId":RefundId
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('通过成功!',function(){
                    OrderRefundApplication.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //拒接接口
    adminReFuseOrderRefund:function(RefundId){
        var methodName = "/orderRefund/AdminReFuseOrderRefund";
        var data = {
            "RefundId":RefundId
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('拒绝成功!',function(){
                    OrderRefundApplication.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台删除退款订单信息
    adminDelOrderRefund:function(RefundIdList){
        var methodName = "/orderRefund/AdminDelOrderRefund";
        var data = {
            "RefundIdList":RefundIdList
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功!',function(){
                    OrderRefundApplication.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台导出退款订单列表
    exportAdminRefundList:function(RefundIdList){
        var methodName = "/orderRefund/ExportAdminRefundList";
        var data = {
            "RefundIdList":RefundIdList
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('导出成功!',function(){
                    location.href = data.Data;
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#RefundBox').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/orderRefund/AdminOrderRefundList',
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
            queryParams: OrderRefundApplication.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: OrderRefundApplication.responseHandler,
            columns: [
                {
                    field: 'RefundId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-id="'+ value +'">'
                        return html;
                    }
                },
                {
                    field: 'OSn',
                    title: '订单编号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<a style="color:#44b8fd" href="javascript:void(0);">' + value + '</a>';
                        return e;
                    }
                },
                {
                    field: 'UserName',
                    title: '会员名',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e =
                            '<span class="sorting_1"style="display:block;height:20px;">' + value + '</span>';

                        return e;
                    }
                },

                {
                    field: 'RefundMoney',
                    title: '退款金额(元)',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e =
                            '<span class="sorting_1"style="display:block;height:20px;">' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'ApplyTime',
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
                    field: 'State',
                    title: '处理状态',
                    align: 'center',
                    valign: 'middle',

                },
                {
                    field: 'RefundTime',
                    title: '退款时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        if(value == '1970-01-01T00:00:00'){
                            if(row.State == "失败"){
                                var e = '<span style="display:block;"></span>';
                            }else{
                                var e = '<span style="display:block;">还未处理</span>';
                            }

                        }else if(value){
                            var value = moment(value).format('YYYY-MM-DD HH:mm:ss')
                            var e = '<span style="display:block;">' + value + '</span>';
                        }
                        else{
                            var e = '<span style="display:block;"> </span>';
                        }
                        return e;
                    }

                },

                {
                    field: 'State',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        if(value == "失败"){
                            var html =
                                '<a class="" href="/order/appicationDetail?id='+row.RefundId +'" style="color:#44b8fd;cursor: pointer;margin-right: 5px"  data-id="' + value + '" >详情</a>' ;
                        }else if(value == "退款中"){
                            var html =
                                '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer"  class="actionBtn" data-id="' + row.RefundId + '">通过</span>'+
                                '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer"  class="closeOrder" data-id="' + row.RefundId + '">拒绝</span>'+
                                '<a class="" href="/order/appicationDetail?id='+row.RefundId +'" style="color:#44b8fd;cursor: pointer"  data-id="' + value + '" >详情</a>' ;

                        }else if(value == "运营审核通过"){
                            var html =
                                '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer"  class="actionBtn" data-id="' + row.RefundId + '">通过</span>'+
                                '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer"  class="closeOrder" data-id="' + row.RefundId + '">拒绝</span>'+
                                '<a class="" href="/order/appicationDetail?id='+row.RefundId +'" style="color:#44b8fd;cursor: pointer"  data-id="' + value + '" >详情</a>' ;

                        }else if(value == "退款完成"){
                            var html =
                                '<a class="" href="/order/appicationDetail?id='+row.RefundId +'" style="color:#44b8fd;cursor: pointer"  data-id="' + value + '" >详情</a>' ;
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
        var methodName = "/orderRefund/AdminOrderRefundList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "State": 0,
            "OSn": $('#order_name').val(),

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.OrderRefundList,
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
                "State": 0,
                "OSn": $('#order_name').val(),
            };
        } else {
            var obj = parame;
        }

        $('#RefundBox').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/orderRefund/AdminOrderRefundList',
                query: obj
            }
        );

    },
    //表格刷新
    projectDectoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
           var  obj = {
                "State": 0,
                "OSn": $('#order_name').val(),
               Page: {
                   PageSize: $("#pagesize_dropdown").val(),
                   PageIndex: 1
               }
            };
        } else {
            var obj = parame;
        }

        $('#RefundBox').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/orderRefund/AdminOrderRefundList',
                query: obj
            }
        );
        OrderRefundApplication.initBootstrapTable()

    },
};

$(function(){

    OrderRefundApplication.init()

})