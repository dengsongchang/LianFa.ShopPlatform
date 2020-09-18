var navSettingList = {
    init: function () {
        //初始化表格
        navSettingList.initBootstrapTable();
    
        //排序更改
        $('body').on('change','.order-disp',function(){
            var NBId = $(this).attr('data-id');
            var DisplayOrder = $(this).val();
            navSettingList.adminChangeNbDisplayOrder(NBId,DisplayOrder)
        });
        //删除按钮点击
        $('body').on('click','.delectBtn',function(){
            var id = $(this).attr('data-id');
            var list = [];
            list.push(id);
            Common.confirmDialog('确认要删除吗?',function(){
                navSettingList.adminBatchDelNavigationBar(list)
            })
        })
    },
    
  //更改产品排序
  adminChangeNbDisplayOrder:function (NBId,DisplayOrder) {
    //请求方法
    var methodName = "/indexDatas/AdminChangeNbDisplayOrder";
    var data = {
        "NBId": NBId,
        "DisplayOrder": DisplayOrder
    };
    
    //请求接口
    SignRequest.set(methodName, data, function (data) {
        if (data.Code == "100") {
            Common.showSuccessMsg('排序成功',function(){
                //删除成功之后刷新表格
                navSettingList.projectQuery();
            })
        } else {
            Common.showErrorMsg(data.Message);
        }
    });
},

    //bootstrapTable
    initBootstrapTable: function () {
        $('#brand_box').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/category/AdminShowSecondLevelCategoryList',
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
            queryParams: navSettingList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class\

            
            responseHandler: navSettingList.responseHandler,
            columns: [{
                    field: 'CateId',
                    title: '编号',
                    align: 'center',
                    valign: 'middle',

                },
                {
                    field: 'Name',
                    title: '分类名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                // {
                //     field: 'DisplayOrder',
                //     title: '排序',
                //     align: 'center',
                //     valign: 'middle',
                //     formatter: function (value, row, index) {
                //         var html = `<input type="number" style="text-align: center" class="order-disp" data-id="${row.NbId}" value="${value}">`
                //         return html;
                //     }
                // },
                {
                    field: 'CateId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '<a class="editor" href="/homePage/classifyBanner?id=' + value + '">设置轮播图</a>';
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
        var methodName = "/category/AdminShowSecondLevelCategoryList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            pageSize: params.limit, //页面大小
            pageNumber: (params.offset / params.limit) + 1, //页码
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            Title: navSettingList.consultingName,
            StartTime: navSettingList.startTime,
            EndTime: navSettingList.endTime,
     
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.AdminShowSecondLevelCategoryList,
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
                Title: navSettingList.consultingName,
                StartTime: navSettingList.startTime,
                EndTime: navSettingList.endTime,
  
            };
        } else {
            obj = parame;
        }
        //方法名
        var methodName = "/category/AdminShowSecondLevelCategoryList";


        $('#brand_box').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/category/AdminShowSecondLevelCategoryList',
                query: obj
            }
        );
    },



}



$(function () {

    navSettingList.init()

})