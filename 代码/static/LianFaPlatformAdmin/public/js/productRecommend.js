var productRecommend = {
    cateTpl:`
        {{each CategoryList as value i}}
            <option value="{{CategoryList[i].CateId}}">{{CategoryList[i].Name}}</option>
        {{/each}}
    `,
    brandTpl:`
        {{each BrandList as value i}}
            <option value="{{BrandList[i].BrandId}}">{{BrandList[i].Name}}</option>
        {{/each}}
    `,
    shareTpl:`
        {{each ShareTypeList as value i}}
            {{if i == 0}}
                <option value="{{ShareTypeList[i].Key}}" selected>{{ShareTypeList[i].Value}}</option>
            {{else}}
                <option value="{{ShareTypeList[i].Key}}">{{ShareTypeList[i].Value}}</option>
            {{/if}}
        {{/each}}
    `,
    init:function(){

        productRecommend.getProductCategory();//初始化商品分类
        productRecommend.shareTypeList();//获取类型

        //类型改变
        $('body').on('change','#Type',function(){
            productRecommend.projectQuery();
            productRecommend.productDestoryQuery();
        })
        //关闭弹窗
        $(".mask").on("click",".close",function () {
            $(".mask").hide();
        });
        //查询
        $("#productSearch").on("click",function(){
            var data={
                "Name": $('#UserName').val(),
                "CateId": $('#CateId').val(),
                "BrandId":$('#BrandId').val(),
            }
            productRecommend.productDestoryQuery(data);
        });
        //点击选择商品
        $(".content").on("click","#selectProduct",function () {
            $(".mask").show();
            productRecommend.selectBootstrapTable();
        })
        //排序
        $('body').on('change','.order-disp',function(){
            var id = $(this).attr('data-id');
            var num = $(this).val();
            productRecommend.editConfigure(id,num);
        })
        //选择商品表格分页每页显示数据
        $("#pagesize_dropdown").on("change",function () {
            productRecommend.productDestoryQuery();
        });
        // 配置全选
        $(".content").on("change","#stuCheckBox",function(){
            if($(this).is(':checked')){
                $(".checkbox").prop("checked",true);
            }
            else{
                $(".checkbox").prop("checked",false);
            }
        });
        // 选择商品全选
        $(".content").on("change","#allCheckBox",function(){
            if($(this).is(':checked')){
                $(".selectCheckbox").prop("checked",true);
            }
            else{
                $(".selectCheckbox").prop("checked",false);
            }
        });
        //点击table单个删除按钮
        $(".content").on("click",".status_delete",function () {
            var id = $(this).attr('data-id');
            Common.confirmDialog("确认要进行删除吗？",function(){
                productRecommend.delProductConfig(id);
            });

        })
        //多个删除
        $("#delete").on("click",function(){
            if(productRecommend.getSelectedData().length > 0){
                Common.confirmDialog("确认对选中的数据进行删除吗？",function(){
                    productRecommend.batchDelProductConfig(productRecommend.getSelectedData());
                });
            }
            else {
                Common.showInfoMsg("请选择需要删除的商品");
            }
        });
    //    点击添加按钮
        $("#addProduct").on("click",function(){
            if(productRecommend.getSelectedProductData().length > 0){
                productRecommend.AddChooseProduct(productRecommend.getSelectedProductData());
            }
            else {
                Common.showInfoMsg("请选择需要添加的商品");
            }
        });
    //    点击一键添加按钮
        $("#bathAdd").on("click",function(){
             var list = $("#productTabel .selectCheckbox");
             var name= $('#UserName').val();
             var cateId= $('#CateId').val();
             var brandId=$('#BrandId').val();
            if(list.length != 0){
                productRecommend.allAddChooseProduct(name,cateId,brandId);
            } else {
                Common.showInfoMsg("暂无符合的商品");
                $(".mask").hide();
            }
        });
    },
    // 获取商品配置列表选中的数据
    getSelectedData:function(){
        var list = $("#configureTable .checkbox");
        var UId = [];
        for(var i=0;i<list.length;i++){
            if(list.eq(i).is(':checked')){
                UId.push(list.eq(i).attr("data-id"));
            }
        }
        return UId;
    },
    // 获取选择商品列表选中的数据
    getSelectedProductData:function(){
        var list = $("#productTabel .selectCheckbox");
        var UId = [];
        for(var i=0;i<list.length;i++){
            if(list.eq(i).is(':checked')){
                UId.push(list.eq(i).attr("data-id"));
            }
        }
        return UId;
    },
    //编辑商品排序
    editConfigure:function (pid,order) {
        //请求方法
        var methodName = "/productConfig/EditProductConfigDisplayOrder";
        var data = {
            "PId": pid,
            "DisplayOrder": order,
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('排序成功');
                productRecommend.projectQuery();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //单个删除商品
    delProductConfig:function (pid) {
        //请求方法
        var methodName = "/productConfig/AdminDelProductConfig";
        var data = {
            "PId": pid,
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功',function () {
                    productRecommend.projectQuery();
                    productRecommend.projectSelectQuery();
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //批量删除商品
    batchDelProductConfig:function (pidlist) {
        //请求方法
        var methodName = "/productConfig/AdminBatchDelProductConfig";
        var data = {
            "PIdList": pidlist,
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功',function () {
                    productRecommend.projectQuery();
                    productRecommend.projectSelectQuery();
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取商品分类
    getProductCategory:function(){
        var methodName = "/category/AdminCategoryList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(productRecommend.cateTpl);
                var html = render(data.Data);
                $("#CateId").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 后台共享设备类型列表
    shareTypeList:function(){
        var methodName = "/productConfig/ShareTypeList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(productRecommend.shareTpl);
                var html = render(data.Data);
                $("#Type").append(html);
                //初始化表格
                productRecommend.initBootstrapTable();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //添加商品
    AddChooseProduct:function (choosepids) {
        var methodName = "/productConfig/AdminBatchAddChooseProduct";
        var data = {
            "ChoosePIds": choosepids,
            Type:$('#Type').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功',function () {
                    productRecommend.projectQuery();
                    productRecommend.projectSelectQuery();
                    $(".mask").hide();
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //一键添加商品
    allAddChooseProduct:function (name,cateid,brandid) {
        var methodName = "/productConfig/AdminAllAddChooseProduct";
        var data = {
            "Name": name,
            "CateId": cateid,
            "BrandId": brandid,
            Type:$('#Type').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功',function () {
                    productRecommend.projectQuery();
                    productRecommend.projectSelectQuery(data);
                    $(".mask").hide();
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //商品配置bootstrapTable
    initBootstrapTable: function () {
        $('#configureTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/productConfig/AdminProductConfigList',
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
            queryParams: productRecommend.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: productRecommend.responseHandler,
            columns: [
                {
                    field: 'PId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-id="'+ value +'">'
                        return html;
                    }
                },
                {
                    field: 'ShowImg',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = '<image class="checkbox" style="width: 75px;height: 75px" src="'+ value +'">'
                        return html;
                    }
                },
                {
                    field: 'Name',
                    title: '商品',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';
                        return e;
                    }
                },
                // {
                //     field: 'PNumber',
                //     title: '库存',
                //     align: 'center',
                //     valign: 'middle',
                //     formatter: function (value, row, index) {
                //
                //         var e = '<span>'+value+'</span>';
                //
                //         return e;
                //     }
                // },
                // {
                //     field: 'MarketPrice',
                //     title: '市场价',
                //     align: 'center',
                //     valign: 'middle',
                //     formatter: function (value, row, index) {
                //
                //         var e = '<span>'+value+'</span>';
                //
                //         return e;
                //     }
                // },
                {
                    field: 'ShopPrice',
                    title: '一口价',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

                        return e;
                    }
                },
                {
                    field: 'DisplayOrder',
                    title: '排序',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<input type="number" class="order-disp" data-id="'+row.PId+'" value='+value+'>';

                        return e;
                    }
                },
                {
                    field: 'PId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = "";
                        html += "<span class='status_delete' data-id='" + value + "'>删除</span>";
                        return html;
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
                $('.caret').remove()
            },
            onLoadError: function (data) {
                $('#dishes_list_table').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
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
            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/productConfig/AdminProductConfigList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Type:$('#Type').val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res.Data);
            return {
                "rows": res.Data.ProductChooseList,
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
        var methodName = "/productConfig/AdminProductConfigList";

        if (parame == "" || parame == undefined) {
            var obj = {
                Type:$('#Type').val(),
            };
        } else {
            var obj = parame;
        }

        $('#configureTable').bootstrapTable(
            "refresh", {
                url:SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新
    projectQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                Type:$('#Type').val(),
                page: {
                    PageSize: $("#pagesizeDropdown").val(),
                    PageIndex: 1
                },
            };
        } else {
            var obj = parame;
        }

        $('#configureTable').bootstrapTable(
            "destroy", {
                url:SignRequest.urlPrefix + '/productConfig/AdminProductConfigList',
                query: obj
            }
        );
        productRecommend.initBootstrapTable();
    },

    //商品选择bootstrapTable
    selectBootstrapTable: function () {
        $('#productTabel').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/productConfig/AdminProductChooseList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 4,
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
            queryParams: productRecommend.selectQueryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: productRecommend.selectResponseHandler,
            columns: [
                {
                    field: 'PId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = '<input type="checkbox" class="selectCheckbox" data-id="'+ value +'">'
                        return html;
                    }
                },
                {
                    field: 'ShowImg',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = '<image class="checkbox" style="width: 45px;height: 45px" src="'+ value +'">'
                        return html;
                    }
                },
                {
                    field: 'Name',
                    title: '商品名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';
                        return e;
                    }
                },
                {
                    field: 'CostPrice',
                    title: '成本价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

                        return e;
                    }
                },
                {
                    field: 'ShopPrice',
                    title: '商品价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>'+value+'</span>';

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

            },
            onLoadError: function (data) {
                $('#dishes_list_table').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {

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
            }
        });
    },
    //bootstrap table post 参数 queryParams
    selectQueryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/productConfig/AdminProductChooseList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            BrandId:0,
            "Name": $('#UserName').val(),
            "CateId": $('#CateId').val(),
            Type:$('#Type').val(),
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    selectResponseHandler: function (res) {
        if (res.Data != null) {
            // console.log(res.Data);
            return {
                "rows": res.Data.ProductChooseList,
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
    projectSelectQuery: function (parame) {
        if (parame == "" ||
            parame == undefined) {
            var obj = {
                BrandId:0,
                "Name": $('#UserName').val(),
                "CateId": $('#CateId').val(),
                Type:$('#Type').val()
            };
        } else {
            var obj = parame;
        }

        $('#productTabel').bootstrapTable(
            "refresh", {
                url:SignRequest.urlPrefix + '/productConfig/AdminProductChooseList',
                query: obj
            }
        );
    },
    productDestoryQuery:function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                BrandId:0,
                "Name": $('#UserName').val(),
                "CateId": $('#CateId').val(),
                Type:$('#Type').val(),
                page: {
                    PageSize: $("#pagesize_dropdown").val(),
                    PageIndex: 1
                },
            };
        } else {
            var obj = parame;
        }
        $('#productTabel').bootstrapTable(
            "destroy", {
                url:SignRequest.urlPrefix + '/productConfig/AdminProductChooseList',
                query: obj
            }
        );
        productRecommend.selectBootstrapTable();
    }


}


$(function () {

    productRecommend.init();

})