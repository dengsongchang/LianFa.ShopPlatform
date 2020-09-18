$(function () {
    productConsulting.init();
});
var productConsulting={
    init:function () {
        productConsulting.listInit();//初始化表格
        productConsulting.getProductCategory();//商品分类接口
        //点击未回复的查询按钮
        $('#search_consult_noreply').click(function () {

            var goods_name_noreply=$('#goods_name_noreply').val();
            var goods_number_noreply=$('#goods_number_noreply').val();
            var goods_cate_noreply=$('#goods_cate_noreply').find("option:selected").val();
            var data={

                "ReplyState": 0,
                "Name": goods_name_noreply,
                "CateId": goods_cate_noreply,
                "PSn": goods_number_noreply,
            }

            productConsulting.projectQuery(data);

        });
    },
    cateTpl:`
        {{each CategoryList as value i}}
            <option value="{{CategoryList[i].CateId}}">{{CategoryList[i].Name}}</option>
        {{/each}}
    `,
    listInit: function () {
        $('#tb_Noreply').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/productconsult/AdminProductConsultsList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 5,
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],
            idField: "ShipCoId", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: productConsulting.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: productConsulting.responseHandler,
            columns: [

                {
                    field: 'ConsultNickName',
                    title: '咨询用户',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

                        return e;
                    }
                },
                {
                    field: 'PName',
                    title: '咨询商品',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e='<span>'+value+'</span>';
                        return e;
                    }
                },
                {
                    field: 'ConsultTime',
                    title: '咨询时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        value=value.replace('T',' ');
                        var e='<span>'+value+'</span>';
                        return e;
                    }
                },
                {
                    field: 'ConsultId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        //var e='<a class="delete_shanchu" data-id='+value+'>删除</a>';
                        var e='<span data-id='+value+'></span>';
                        return e;
                    }
                }



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
                //$('.caret').remove()

            },
            onLoadError: function (data) {
                //$('#dishes_list_table').bootstrapTable('removeAll');
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
    queryParams: function (params) {//请求参数
        console.log(params)
        var state=0;
        var goods_name="";
        var cateId=0;
        var PSn="";
        // if(params.goods_name_noreply){
        //     goods_name=params.goods_name_noreply;
        // }
        // if(params.goods_cate_noreply){
        //     cateId=params.goods_cate_noreply;
        // }
        // if(params.goods_number_noreply){
        //     PSn=params.goods_number_noreply;
        // }

        //配置参数
        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            pageSize: params.limit, //页面大小
            pageNumber: (params.offset / params.limit) + 1, //页码
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "ReplyState": 0,
            "Name": goods_name,
            "CateId": cateId,
            "PSn": PSn,

        };
        console.log(temp)
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        console.log('分割线')
        console.log(res)
        if (res.Data != null) {

            return {
                "rows": res.Data.NotReturnProductConsultsList,
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
        $('#tb_Noreply').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/productconsult/AdminProductConsultsList',
                query: obj
            }
        );
    },

    // 获取商品分类下拉分类接口
    getProductCategory:function(){
        var methodName = "/category/AdminCategoryList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(productConsulting.cateTpl);
                var html = render(data.Data);
                $("#goods_cate_noreply").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};