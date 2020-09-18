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
    labelTpl: `
        {{each ProductLabelList as value i}}
            <option value="{{ProductLabelList[i].PLId}}">{{ProductLabelList[i].Name}}</option>
        {{/each}}
    `,
    flag: true,
    province_template: `        
                                   <option selected="selected" value="0">请选择省</option>
                                   {{each RegionsList as value i}}
                                   <option data-id="{{RegionsList[i].RegionId}}">{{RegionsList[i].Name}}</option>
                                   {{/each}}

`,
    city_template: `

                                    <option selected="selected" value="0">请选择市</option>
                                     {{each RegionsList as value i}}
                                    <option data-id="{{RegionsList[i].RegionId}}">{{RegionsList[i].Name}}</option>
                                       {{/each}}
`,
    area_template: `
                                    
                                    <option selected="selected" value="0">请选择区/县</option>
                                    {{each RegionsList as value i}}
                                    <option data-id="{{RegionsList[i].RegionId}}">{{RegionsList[i].Name}}</option>
                                    {{/each}}
`,
    //商品模板
    productTemplate: `
                        {{each productList as value i}}
                            <li class="clearfix">
                                <div class="p-name">
                                    <img src="{{productList[i].ShowImg}}">
                                    <div>
                                        <p class="pn-name">{{productList[i].Name}}</p>
                                    </div>
                                </div>
                                <p>{{productList[i].RealCount}}</p>
                                <p>{{productList[i].Subtotal}}</p>
                            </li>
                        {{/each}}
`,
    //快递公司模板
    expressCompanyTemplate: `
            <option selected="selected" value="0">请选择快递公司</option>
            {{each ShipCompaniesList as value i}}
                <option value="{{ShipCompaniesList[i].Name}}">{{ShipCompaniesList[i].Name}}</option>
            {{/each}}
`,
    init: function () {
        //初始化获取省列表
        ProductList.provinceHandle();
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

        //导出商品列表按钮点击
        $('body').on('click', '.all_output', function () {
            ProductList.exportAdminProductList()
        })

        //省改变时
        $('body').on('change', '#province_box', function () {
            var id = $('#province_box option:selected').attr('data-id');
            ProductList.provinceId = id;
            ProductList.cityId = 0;
            ProductList.regionid = 0;
            ProductList.cityHandle(id);
            $('#city_box').val(0)
            $('#area_box').html('<option selected="selected" value="0">请选择区/县</option>')
            $('#street_box').html('<option selected="selected" value="0">请选择街道</option>')
            if ($('#province_box option:selected').val() == "0") {
                ProductList.provinceId = 0;
            } else {

            }
        })
        //市改变时
        $('body').on('change', '#city_box', function () {
            var id = $('#city_box option:selected').attr('data-id');
            ProductList.cityId = id;
            ProductList.regionid = 0;
            ProductList.areaHandle(id);
            $('#area_box').val(0)
            $('#street_box').html('<option selected="selected" value="0">请选择街道</option>')
        })
        //区改变时
        $('body').on('change', '#area_box', function () {
            var id = $('#area_box option:selected').attr('data-id');
            ProductList.regionid = id;
            $('#street_box').val(0)
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

        //编辑礼品卡
        $("#productTable").on("click", ".status_edit", function () {
            $('#editModal').modal('show');
        });
        //单个兑换模态框
        $('body').on('click', '.status_exchange', function () {
            // ProductList.clearHandle();
            $('#exchangeModal').modal('show');
        })
        //兑换按钮点击
        $('#exChangeBtn').on('click', function () {
            //礼品卡序列号
            if (!Validate.emptyValidateAndFocusAndColor("#sequenceNumber", "请输入礼品卡序列号", "")) {
                return false;
            }
            //密码
            if (!Validate.emptyValidateAndFocusAndColor("#passWord", "请输入密码", "")) {
                return false;
            }
            //收货人
            if (!Validate.emptyValidateAndFocusAndColor("#consignee", "请输入收货人", "")) {
                return false;
            }
            //收货地址详情
            if (!Validate.emptyValidateAndFocusAndColor("#addressDetail", "请输入收货地址详情", "")) {
                return false;
            }
            //手机号码
            if (!Validate.emptyValidateAndFocusAndColor("#phoneNumber", "请输入手机号码", "")) {
                return false;
            }
            //备注
            if (!Validate.emptyValidateAndFocusAndColor("#mark", "请输入备注", "")) {
                return false;
            }
            if (ProductList.regionid == 0) {
                Common.showInfoMsg('请选择省市区')
                return false
            }
            //调用兑换接口
            ProductList.adminSubmitCouponOrder()

        })
        //导入

        $("#import input").on("change", function () {
            var file = $(this)[0].files[0];
            var url = "/Coupon/AdminUploadCouponOrderList";
            ProductList.setFile(file, url)
        });
        //下载模板按钮点击
        $('body').on('click','#downloadBtn',function(){
            ProductList.downloadDiseasesUploadTemplate();
        })
        //礼品卡详情
        $('#productTable').on('click', '.status_detail', function () {
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

            location.href = "/giftCard/giftCardDetail?PId=" + PId + "&type=" + type;
        })

        //单个删除
        $("#productTable").on("click", ".status_delete", function () {
            var PId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(PId);
            Common.confirmDialog("确认进行删除吗？", function () {
                ProductList.deleteProduct(PIdArr);
            });
        });
        //关闭弹窗
        $(".mask").on("click", ".close", function () {
            $(".mask").hide();
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
                ProductList.outSaleProduct(PIdArr);
            });
        });
        //上架
        $("#productTable").on("click", ".status_add", function () {
            var PId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(PId);
            Common.confirmDialog("确认进行上架操作吗？", function () {
                ProductList.onSaleProduct(PIdArr);
            });
        });
        //点击发货按钮
        $('body').on('click', '.sendProduct', function () {
            $('.mask').show();
            $('.deliverBox').show();
            localStorage.setItem('oid', $(this).attr('data-id'));
            $('#expressNumber').val("")
            $('#logisticsCompany').val(0)
            ProductList.adminGetDeliveryList()

        });
        //发货里面的弹窗去确认点击
        $('body').on('click', '#deliver', function () {
            //快递单号验证
            if (!Validate.emptyValidateAndFocus("#expressNumber", "请输入快递单号", "")) {
                return false;
            }
            //快递公司
            if ($('#logisticsCompany').val() == "0") {
                Common.showErrorMsg("请选择快递公司!")
                return false;
            }
            //调用确认发货接口
            ProductList.adminSendGoods()

        })

        //表格分页每页显示数据
        $("#pagesize_dropdown").on("change", function () {
            ProductList.projectQuery();
        });
        ProductList.initBootstrapTable();
    },
    //获取发货订单基本信息
    adminGetDeliveryList: function () {
        var methodName = "/order/AdminGetDeliveryList";
        var data = {
            "OId": localStorage.getItem('oid'),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#orderAddress').text(data.Data.DeliveryList[0].address);
                $('#buymarker').text(data.Data.DeliveryList[0].buyerRemark);
                $('#orderUser').text(data.Data.DeliveryList[0].consignee)
                $('#orderPhone').text(data.Data.DeliveryList[0].phone)
                var render = template.compile(ProductList.expressCompanyTemplate);
                var html = render(data.Data);
                $('#logisticsCompany').html(html);
                var render1 = template.compile(ProductList.productTemplate);
                var html1 = render1(data.Data.DeliveryList[0]);
                $('.pl-table').html(html1);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //下载模板
    downloadDiseasesUploadTemplate: function() {
        var methodName = "/Coupon/DownloadCouponOrderTemplate";
        var data = {
        };
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                location.href = data.Data;
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //发货
    adminSendGoods: function () {
        var methodName = "/order/AdminOrdersGoods";
        var data = {
            "OId": localStorage.getItem('oid'),
            "ShipCode": $('#expressNumber').val(),
            "ShipFriendName": $('#logisticsCompany').val()
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('发货成功', function () {
                    ProductList.projectQuery();
                    $('.mask').hide()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //上传模板
    setFile: function (file, url) {
        var formData = new FormData();
        formData.append('file', file);
        $.ajax({
            url: SignRequest.urlPrefix + url,
            type: "post",
            dataType: "json",
            data: formData,
            cache: false,
            processData: false,
            contentType: false
        }).done(function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("导入成功")
                //进行刷新操作==>
                $("#importFile").val("");
                ProductList.projectQuery();
            } else {
                $("#importFile").val("");
                Common.showErrorMsg(data.Message);
            }
        }).fail(function () {
            $("#importFile").val("");
            Common.showErrorMsg("上传文件失败");
        })
    },
    //清空数据
    clearHandle: function () {
        $('#passWord').val("");
        $('#consignee').val("");
        $('#addressDetail').val("");
        $('#phoneNumber').val("");
        $('#mark').val("");
        ProductList.provinceId = 0;
        ProductList.cityId = 0;
        ProductList.regionid = 0;
        $('#city_box').val(0)
        $('#area_box').html('<option selected="selected" value="0">请选择区/县</option>')
        $('#street_box').html('<option selected="selected" value="0">请选择街道</option>')
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
        var methodName = "/product/AdminOutSaleProduct";
        var data = {
            PId: PId
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
        var methodName = "/product/AdminOnSaleProduct";
        var data = {
            PId: PId
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
    // 兑换接口
    adminSubmitCouponOrder: function () {
        var methodName = "/Coupon/AdminSubmitCouponOrder";
        var data = {
            "CouponSn": $('#sequenceNumber').val(),
            "Password": $('#passWord').val(),
            "RegionId": ProductList.regionid,
            "Consignee": $('#consignee').val(),
            "Address": $('#addressDetail').val(),
            "ShipMobile": $('#phoneNumber').val(),
            "BuyerRemark": $('#mark').val()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('兑换成功', function () {
                    ProductList.refreshQuery();
                    $('#exchangeModal').modal('hide')
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //调用获取省列表
    provinceHandle: function () {
        var methodName = "/Tool/ProvinceList";
        var data = {
            "Type": 1,
            "ParentId": 0
        };
        SignRequest.setNoAdmin(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(ProductList.province_template);
                var html = render(data.Data);
                $("#province_box").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //调用获取市列表
    cityHandle: function (id) {
        var methodName = "/Tool/CityList";
        var data = {
            "Type": 1,
            "ParentId": id
        };
        SignRequest.setNoAdmin(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(ProductList.city_template);
                var html = render(data.Data);
                $("#city_box").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //调用获取区列表
    areaHandle: function (id) {
        var methodName = "/Tool/MunicipalDistrictList";
        var data = {
            "Type": 1,
            "ParentId": id
        };
        SignRequest.setNoAdmin(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(ProductList.area_template);
                var html = render(data.Data);
                $("#area_box").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function (isEdit) {
        $('#productTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/Coupon/AdminCouponList',
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
                // {
                //     field: 'CouponId',
                //     title: '',
                //     align: 'center',
                //     valign: 'middle',
                //     formatter: function (value, row, index) {
                //         var html = '<input type="checkbox" class="checkbox" data-pid="' + value + '">'
                //         return html;
                //     }
                // },
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
                    field: 'CouponSn',
                    title: '礼品卡序列号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'AddTimes',
                    title: '购买时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'UseTimes',
                    title: '兑换时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'AdminUser',
                    title: '兑换管理员名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'CouponId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = "";
                        html += '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer" class="sendProduct"  data-id="' + row.OId + '">订单发货</span>';
                        ;
                        html += "<span class='status_detail' style='color: #039cf8;cursor: pointer' data-id='" + value + "'>礼品卡详情</span>";
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
        var methodName = "/Coupon/AdminCouponList";
        if (ProductList.flag) {
            var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                minSize: $("#leftLabel").val(),
                maxSize: $("#rightLabel").val(),
                minPrice: $("#priceleftLabel").val(),
                maxPrice: $("#pricerightLabel").val(),
                "CouponSn": $('#CouponSn').val(),
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
                "CouponSn": $('#CouponSn').val(),
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
    //表格刷新(直接刷新)
    refreshQuery: function (parame) {
        //方法名
        var methodName = "/Coupon/AdminCouponList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "CouponSn": $('#CouponSn').val(),
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
        var methodName = "/Coupon/AdminCouponList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "CouponSn": $('#CouponSn').val(),
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