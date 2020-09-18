//商品弹出框
var ProductModal = {
    productListUrl: SignRequest.urlPrefix + "/product/ActivityProductList",
    isMultiple: false, //是否多选 false-单选 true-多选
    isRefresh: true, //是否重新加载数据
    //商品分类模板
    cateTpl: `
    {{each FirstCategoryList as value i}}
        <option value="{{FirstCategoryList[i].CateId}}">{{FirstCategoryList[i].Name}}</option>
    {{/each}}
    `,
    productModalTpl: `
    <div class="modal fade" id="choiceProductModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document" style="width:1060px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">选择商品</h4>
                </div>
                <div class="modal-body" style="margin:auto">
                    <div class="row form-group search_goods_box">
                        <div class="form-group col-lg-4 col-md-4">
                            <label for="" style="width:80px;text-align: right">商品名称：</label>
                            <input type="text" class="form-control" id="choiceProductName" style="width:200px;display: inline-block;text-align:left;border-radius: 4px ">
                        </div>
                        <div class="form-group col-lg-3 col-md-3">
                            <select class="form-control" id="choiceCateId">
                                <option selected="selected" value="0">请选择商品分类</option>
                            </select>
                        </div>
                        <div class="disp_page_box pull-right" style="margin-right: 20px">
                            <label style="float:left;line-height: 34px;margin-right: 4px;">每页显示数量：</label>
                            <select id="pageSizeDropDown" class="pagesize-dropdown select2 pull-right" tabindex="-1"
                                aria-hidden="true">
                                <option value="10" selected>10</option>
                                <option value="20">20</option>
                                <option value="40">40</option>
                                <option value="200">200</option>
                                <option value="500">500</option>
                                <option value="1000">1000</option>
                                <option value="2000">2000</option>
                            </select>
                        </div>
                        <button class="btn btn-primary" id="productSearch" style="margin-left: 20px">查询</button>
                    </div>
                    <div class="row form-group">
                        <div class="box_choice_goods">
                            <table id="choiceProductTable">

                            </table>
                        </div>
                    </div>
                </div>
                {{if isMultiple}}
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>
                    <button type="button" class="btn btn-primary" id="choiceProductConfirmBtn">确认</button>
                </div>
                {{/if}}
            </div>
        </div>
    </div>`,
    //初始化
    init: function () {
        var render = template.compile(ProductModal.productModalTpl);
        var data = {
            isMultiple: ProductModal.isMultiple
        }
        var html = render(data);
        $('body').append(html);

        //当选择商品模态框出现获取数据
        $('#choiceProductModal').on('shown.bs.modal', function () {
            if (ProductModal.isRefresh) {
                ProductModal.getProductCategory(); //初始化商品分类
                ProductModal.initBootstrapTable(); //初始化商品列表
                ProductModal.isRefresh = false;
            }
        });

        //查询
        $("#productSearch").on("click", function () {
            var data = {
                "Name": $('#choiceProductName').val(),
                "CateId": $('#choiceCateId').val(),
            }
            ProductModal.projectDestoryQuery(data);
        });

        // 分页条数设置
        $("#pageSizeDropDown").on("change", function () {
            var data = {
                "Name": $('#choiceProductName').val(),
                "CateId": $('#choiceCateId').val(),
            }
            ProductModal.projectDestoryQuery(data);
        });

        //点击选择
        $('#choiceProductModal').on('click', '.choice-span', function () {
            if ($.isFunction(ProductModal.onClickChoice)) {
                ProductModal.onClickChoice($(this));
            } else {
                $('#choiceProductModal').modal('hide');
            }
        });

        //点击确认
        $('#choiceProductModal').on('click', '#choiceProductConfirmBtn', function () {
            if ($.isFunction(ProductModal.onClickConfirm)) {
                ProductModal.onClickConfirm($(this));
            } else {
                $('#choiceProductModal').modal('hide');
            }
        })
    },
    //销毁
    destroy: function () {
        $('#choiceProductModal').remove();
    },
    //点击选择触发函数
    onClickChoice: {},
    //点击确认按钮触发函数
    onClickConfirm: {},
    //初始化BootstrapTable
    initBootstrapTable: function () {
        $('#choiceProductTable').bootstrapTable({
            method: 'post',
            url: ProductModal.productListUrl,
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pageSizeDropDown").val(),
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
            queryParams: ProductModal.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: ProductModal.responseHandler,
            columns: [
                {
                    field: 'PId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        //多选时渲染选择
                        if (ProductModal.isMultiple) {
                            var html = '<input type="checkbox" class="checkbox" data-id="' + value + '" style="display: inline-block;">'
                            return html;
                        }
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
                    field: 'CostPrice',
                    title: '成本价',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
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
                {
                    field: 'PId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        //单选框时渲染选择
                        if (!ProductModal.isMultiple) {
                            var html = "<span class='choice-span' data-num='" + row.number + "' data-price='" + row.ShopPrice + "'  data-id='" + value + "'>选择</span>";
                            return html;
                        }
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
            onLoadSuccess: function (data) {},
            onLoadError: function (data) {
                $('#choiceProductTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {

            },
            //2. 点击前面的复选框进行对应的操作,点击全选框时触发的操作
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
    //bootstraptable post 查询参数
    queryParams: function (params) {
        //配置参数
        var temp = {
            "Name": $('#choiceProductName').val(),
            "CateId": $('#choiceCateId').val(),
            Page: {
                PageSize: params.limit, //页面大小,
                PageIndex: (params.offset / params.limit) + 1, //页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
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
    projectQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            obj = {
                "Name": $('#choiceProductName').val(),
                "CateId": $('#choiceCateId').val(),
            };
        } else {
            obj = parame;
        }

        $('#choiceProductTable').bootstrapTable(
            "refresh", {
                url: ProductModal.productListUrl,
                query: obj
            }
        );
    },
    //表格刷新
    projectDestoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            obj = {
                "Name": $('#choiceProductName').val(),
                "CateId": $('#choiceCateId').val(),
                Page: {
                    PageSize: $("#pageSizeDropDown").val(), //页面大小,
                    PageIndex: 1, //页码
                }
            };
        } else {
            obj = parame;
        }

        $('#choiceProductTable').bootstrapTable(
            "destroy", {
                url: ProductModal.productListUrl,
                query: obj
            }
        );
        ProductModal.initBootstrapTable();
    },
    // 获取商品分类
    getProductCategory: function (ele) {
        var methodName = "/category/AdminFirstCategoryList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(ProductModal.cateTpl);
                var html = render(data.Data);
                $("#choiceCateId").append(html);
                //初始化搜索下拉框
                $("#choiceCateId").chosen();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
}