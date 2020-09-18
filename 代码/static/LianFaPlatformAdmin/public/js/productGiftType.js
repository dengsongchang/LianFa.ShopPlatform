$(function () {
    ProductList.init();
})

var ProductList = {
    cateTpl: `
        {{each FirstCategoryList as value i}}
            <option value="{{FirstCategoryList[i].CateId}}">{{FirstCategoryList[i].Name}}</option>
        {{/each}}
    `,
    brandTpl: `
        {{each BrandList as value i}}
            <option value="{{BrandList[i].BrandId}}">{{BrandList[i].Name}}</option>
        {{/each}}
    `,
    deliveryTpl: `
        {{each DeliveryAreaList as value i}}
            <option value="{{DeliveryAreaList[i].DeliveryAreaId}}">{{DeliveryAreaList[i].Name}}</option>
        {{/each}}
    `,
    flag: true,
    init: function () {
        if (localStorage.getItem('PageIndex')) {
            if (localStorage.getItem('PageSize')) {
                $('#pagesize_dropdown').val(localStorage.getItem('PageSize'))
            }
            ProductList.initBootstrapTable();
            setTimeout(function () {
                $('.pagination').find('.page-number').each(function (index, item) {
                    $(item).removeClass('active');
                    if ($(item).find('a').text() == localStorage.getItem('PageIndex')) {
                        $(item).addClass('active');
                    }
                })
            }, 1000)
        } else {
            ProductList.initBootstrapTable();
        }
        //配送区域列表
        ProductList.deliveryAreaList();

        //导出商品列表按钮点击
        $('body').on('click', '.all_output', function () {

            ProductList.exportAdminProductList()

        })

        // 查詢
        $("#search").on("click", function () {
            localStorage.setItem('PageIndex', 1)
            ProductList.projectQuery();
        });
        var type = Common.getUrlParam("type");
        if (type) {
            $('#State').find('li').eq(type).addClass('active').siblings().removeClass('active');
        } else {
            $('#State').find('li').eq(0).addClass('active').siblings().removeClass('active');
        }
        // 切换状态
        // $("#State").on("click", "li", function () {
        //     localStorage.setItem('PageIndex', "1")
        //     localStorage.setItem('PageSize', "10")
        //     var tabNum = $(this).find("a").attr("data-type");
        //     console.log(tabNum);
        //     ProductList.dataType = tabNum;
        //     location.href = '/product/productList?type=' + tabNum + ''
        //     if (!$(this).hasClass("active")) {
        //         setTimeout(ProductList.projectQuery, 300);
        //     }
        // });

        //全选
        $("#selAll").on("change", function () {
            if ($(this).is(':checked')) {
                $(".checkbox").prop("checked", true);
            }
            else {
                $(".checkbox").prop("checked", false);
            }
        });

        //编辑
        $("#productTable").on("click", ".status_edit", function () {
            var PId = $(this).attr("data-id");
            var state = $(this).attr("data-type");
            var page = 1;
            $('.pagination').find('.page-number').each(function (index, item) {
                if ($(item).hasClass('active')) {
                    page = $(item).find('a').text()
                }
            })
            var size = $('#pagesize_dropdown').val();
            localStorage.setItem('PageIndex', page);
            localStorage.setItem('PageSize', size);
            var type = 0;
            $('#State').find('li').each(function (index, item) {
                if ($(item).hasClass('active')) {
                    type = $(item).find('a').attr('data-type');
                }
            })

            location.href = "/giftCard/giftTypeRelease?CouponTypeId=" + PId + "&type=" + type;


        });

        //单个删除
        $("#productTable").on("click", ".status_delete", function () {
            var PId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(PId);
            Common.confirmDialog("确认进行删除吗？", function () {
                ProductList.deleteProduct(PIdArr);
            });
        });

        //多个删除
        $("#delete").on("click", function () {
            Common.confirmDialog("确认对选中的数据进行删除吗？", function () {
                ProductList.deleteProduct(ProductList.getSelectedData());
            });
        });
        //下架
        $("#productTable").on("click", ".status_close", function () {
            var PId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(PId);
            Common.confirmDialog("确认进行下架操作吗？", function () {
                ProductList.outSaleProduct(PId);
            });
        });
        //上架
        $("#productTable").on("click", ".status_add", function () {
            var PId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(PId);
            Common.confirmDialog("确认进行上架操作吗？", function () {
                ProductList.onSaleProduct(PId);
            });
        });

        //表格分页每页显示数据
        $("#pagesize_dropdown").on("change", function () {
            ProductList.projectQuery();
        });
        ProductList.initBootstrapTable();
    },
    //后台导出商品列表列表
    exportAdminProductList: function (list) {
        var methodName = "/product/ExportAdminProductList";
        var data = {
            State: Number(Common.getUrlParam('type')) ? Common.getUrlParam('type') : 0,
            Name: $("#Name").val(),
            PSn: $("#PSn").val(),
            CateId: $("#CateId").val(),
            BrandId: $("#BrandId").val(),
            PlId: $("#Label").val(),
            TemplateId: $("#TemplateId").val(),
            Type: $("#Type").val(),
            ModuleType: $('#ModuleType').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                location.href = data.Data
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取选中的数据
    getSelectedData: function () {
        var list = $("#productTable .checkbox");
        var PId = [];
        for (var i = 0; i < list.length; i++) {
            if (list.eq(i).is(':checked')) {
                PId.push(list.eq(i).attr("data-pid"));
            }
        }

        return PId;
    },
    // 下架
    outSaleProduct: function (PId, isAll) {
        var methodName = "/Coupon/AdminEditCouponTypeState";
        var data = {
            "CouponTypeId": PId,
            "Type": 0
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                if (isAll) {
                    ProductList.projectQuery();
                } else {
                    ProductList.refreshQuery();
                }
                Common.showSuccessMsg("下架成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //上架
    onSaleProduct: function (PId, isAll) {
        var methodName = "/Coupon/AdminEditCouponTypeState";
        var data = {
            "CouponTypeId": PId,
            "Type": 1
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                if (isAll) {
                    ProductList.projectQuery();
                } else {
                    ProductList.refreshQuery();
                }
                Common.showSuccessMsg("上架成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 删除
    deleteProduct: function (PId) {
        var methodName = "/product/AdminDelProduct";
        var data = {
            PId: PId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                ProductList.projectQuery();
                Common.showSuccessMsg("删除成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取配送区域列表
    deliveryAreaList: function () {
        var methodName = "/Coupon/DeliveryAreaList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(ProductList.deliveryTpl);
                var html = render(data.Data);
                $("#DeliveryAreaId").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function (isEdit) {
        $('#productTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/Coupon/CouponTypeList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: ProductList.flag ? localStorage.getItem('PageIndex') : 1,
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
            queryParams: ProductList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: ProductList.responseHandler,
            columns: [
                {
                    field: 'CouponTypeId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-pid="' + value + '">'
                        return html;
                    }
                },
                {
                    field: 'Name',
                    title: '礼品卡名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'CouponTypeSn',
                    title: '礼品卡类型序列号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Money',
                    title: '礼品卡价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'DeliveryArea',
                    title: '配送范围',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'StateDec',
                    title: '状态',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'CouponTypeId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = "";
                        if (row.State == 1) {
                            html += "<span class='status_edit' style='margin-right: 15px' data-id='" + value + "' data-type='" + row.ModuleType + "'>编辑</span>";
                            // html += "<span class='status_delete' data-id='" + value + "'>删除</span>";
                            html += "<span class='status_close' data-id='" + value + "' style='margin-left: 10px;'>下架</span>";
                        } else if (row.State == 0) {
                            html += "<span class='status_edit' style='margin-right: 15px' data-id='" + value + "' data-type='" + row.ModuleType + "'>编辑</span>";
                            // html += "<span class='status_delete' data-id='" + value + "'>删除</span>";
                            html += "<span class='status_add' data-id='" + value + "' style='margin-left: 10px;cursor: pointer;color: #039cf8'>上架</span>";
                        }
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
                console.log(data);
                if (ProductList.flag) {
                    ProductList.flag = false
                }
                $('.caret').remove()
            },
            onLoadError: function (data) {
                $('#productTable').bootstrapTable('removeAll');
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
        //方法名
        var methodName = "/Coupon/CouponTypeList";
        if (ProductList.flag) {
            var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                minSize: $("#leftLabel").val(),
                maxSize: $("#rightLabel").val(),
                minPrice: $("#priceleftLabel").val(),
                maxPrice: $("#pricerightLabel").val(),
                "CouponName": $('#CouponName').val(),
                "DeliveryAreaId": $('#DeliveryAreaId').val(),
                Page: {
                    PageSize: params.limit,//页面大小,
                    PageIndex: localStorage.getItem('PageIndex'),//页码
                }
            };
        } else {
            var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                minSize: $("#leftLabel").val(),
                maxSize: $("#rightLabel").val(),
                minPrice: $("#priceleftLabel").val(),
                maxPrice: $("#pricerightLabel").val(),
                "CouponName": $('#CouponName').val(),
                "DeliveryAreaId": $('#DeliveryAreaId').val(),
                Page: {
                    PageSize: params.limit,//页面大小,
                    PageIndex: (params.offset / params.limit) + 1,//页码
                }
            };
        }

        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
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
    //表格刷新(直接刷新)
    refreshQuery: function (parame) {
        //方法名
        var methodName = "/Coupon/CouponTypeList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "CouponName": $('#CouponName').val(),
                "DeliveryAreaId": $('#DeliveryAreaId').val(),
            };
        } else {
            var obj = parame;
        }

        $('#productTable').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新(先销毁后初始化)
    projectQuery: function (parame) {
        //方法名
        var methodName = "/Coupon/CouponTypeList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "CouponName": $('#CouponName').val(),
                "DeliveryAreaId": $('#DeliveryAreaId').val(),
                Page: {
                    PageSize: $("#pagesize_dropdown").val(),//页面大小,
                    PageIndex: 1,//页码
                }
            };
        } else {
            var obj = parame;
        }

        $('#productTable').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );

        ProductList.initBootstrapTable();

    }
}