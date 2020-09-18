$(function(){
    ProductRecovery.init();
})

var ProductRecovery = {
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
    init:function(){

        // 分页条数设置
        $("#pagesize_dropdown").on("change",function(){
            ProductRecovery.projectQuery();
        });

        // 删除
        $("#recoveryTable").on("click",".status_delete",function(){
            var PId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(PId);
            Common.confirmDialog("确认进行删除吗？",function(){
                ProductRecovery.deleteProduct(PIdArr);
            });
        });

        // 查询
        $("#search").on("click",function(){
            ProductRecovery.projectQuery();
        });

        // 全选
        $("#stuCheckBox").on("change",function(){
            if($(this).is(':checked')){
                $(".checkbox").prop("checked",true);
            }
            else{
                $(".checkbox").prop("checked",false);
            }
        });

        //多个删除
        $("#delete").on("click",function(){
            if(ProductRecovery.getSelectedData().length > 0){
                Common.confirmDialog("确认对选中的数据进行删除吗？",function(){
                    ProductRecovery.deleteProduct(ProductRecovery.getSelectedData());
                });
            }
            else {
                Common.showInfoMsg("请选择需要删除的数据");
            }
        });

        //多个还原上架
        $("#onSale").on("click",function(){
            if(ProductRecovery.getSelectedData().length > 0){
                Common.confirmDialog("确认对选中的数据进行还原上架操作吗？",function(){
                    ProductRecovery.onSaleProduct(ProductRecovery.getSelectedData());
                });
            }
            else {
                Common.showInfoMsg("请选择需要还原上架的数据");
            }
        });

        //多个还原下架
        $("#outSale").on("click",function(){
            if(ProductRecovery.getSelectedData().length > 0){
                Common.confirmDialog("确认对选中的数据进行还原下架操作吗？",function(){
                    ProductRecovery.outSaleProduct(ProductRecovery.getSelectedData());
                });
            }
            else {
                Common.showInfoMsg("请选择需要还原下架的数据");
            }
        });

        Common.initLaydateWithTime();

        ProductRecovery.getProductCategory();
        ProductRecovery.getProductBrand();

        ProductRecovery.initBootstrapTable();
    },
    // 获取选中的数据
    getSelectedData:function(){
        var list = $("#recoveryTable .checkbox");
        var PId = [];
        for(var i=0;i<list.length;i++){
            if(list.eq(i).is(':checked')){
                PId.push(list.eq(i).attr("data-pid"));
            }
        }
        return PId;
    },
    // 还原到上架
    onSaleProduct:function(PId){
        var methodName = "/product/AdminOnSaleProduct";
        var data = {
            PId: PId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                ProductRecovery.refreshQuery();
                Common.showSuccessMsg("还原成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 还原到下架
    outSaleProduct:function(PId){
        var methodName = "/product/AdminOutSaleProduct";
        var data = {
            PId: PId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                ProductRecovery.refreshQuery();
                Common.showSuccessMsg("还原成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 删除
    deleteProduct:function(PId){
        var methodName = "/product/AdminDelProduct";
        var data = {
            PId: PId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                ProductRecovery.refreshQuery();
                Common.showSuccessMsg("删除成功");
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
                var render = template.compile(ProductRecovery.cateTpl);
                var html = render(data.Data);
                $("#CateId").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取商品品牌
    getProductBrand:function(){
        var methodName = "/brand/AdminBrandList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(ProductRecovery.brandTpl);
                var html = render(data.Data);
                $("#BrandId").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable:function(){
        $('#recoveryTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/product/AdminRecycleBinProductList',
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
            queryParams: ProductRecovery.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: ProductRecovery.responseHandler,
            columns: [
                {
                    field: 'PId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-pid="'+ value +'">'
                        return html;
                    }
                },
                {
                    field: 'PId',
                    title: '排序',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Name',
                    title: '商品',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'PSn',
                    title: '商家编码',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'PNumber',
                    title: '库存',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'MarketPrice',
                    title: '市场价',
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
                    title: '一口价',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'PId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = "<span class='status_delete' data-id='" + value + "'>删除</span>";
                        return html;
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
                $('#recoveryTable').bootstrapTable('removeAll');
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
        var methodName = "/product/AdminRecycleBinProductList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Name: $("#Name").val(),
            PSn: $("#PSn").val(),
            StartTime: $("#start").val(),
            EndTime: $("#end").val(),
            CateId: $("#CateId").val(),
            BrandId: $("#BrandId").val(),
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
                "rows": res.Data.RecycleBinProductList,
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
        var methodName = "/product/AdminRecycleBinProductList";

        if (parame == "" || parame == undefined) {
            var obj = {
                Name: $("#Name").val(),
                PSn: $("#PSn").val(),
                CateId: $("#CateId").val(),
                BrandId: $("#BrandId").val(),
                StartTime: $("#start").val(),
                EndTime: $("#end").val(),
            };
        } else {
            var obj = parame;
        }

        $('#recoveryTable').bootstrapTable(
            "refresh", {
                url:SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新(先销毁后初始化)
    projectQuery: function(parame) {
        //方法名
        var methodName = "/product/AdminRecycleBinProductList";

        if (parame == "" || parame == undefined) {
            var obj = {
                Name: $("#Name").val(),
                PSn: $("#PSn").val(),
                CateId: $("#CateId").val(),
                BrandId: $("#BrandId").val(),
                StartTime: $("#start").val(),
                EndTime: $("#end").val(),
                Page: {
                    PageSize: $("#pagesize_dropdown").val(),//页面大小,
                    PageIndex: 1,//页码
                }
            };
        } else {
            var obj = parame;
        }

        $('#recoveryTable').bootstrapTable(
            "destroy", {
                url:SignRequest.urlPrefix + methodName,
                query: obj
            }
        );

        ProductRecovery.initBootstrapTable();
    }
}