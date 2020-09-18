$(function() {
    giftTypeRelease.init();
})
var giftTypeRelease = {
    deliveryTpl: `
        <option selected="selected" value="0">请选择配送区域</option>
        {{each DeliveryAreaList as value i}}
            <option value="{{DeliveryAreaList[i].DeliveryAreaId}}">{{DeliveryAreaList[i].Name}}</option>
        {{/each}}
    `,
    freightTpl: `
        {{each TemplatesList as value i}}
            <option value="{{TemplatesList[i].TemplateId}}">{{TemplatesList[i].TemplateName}}</option>
        {{/each}}
    `,
    init: function() {
        giftTypeRelease.adminTemplatesList();

        //订单筛选时间
        // laydate.render({
        //     elem: '#start_time', //指定元素
        //     type: 'datetime'
        // });
        // laydate.render({
        //     elem: '#end_time', //指定元素
        //     type: 'datetime'
        // });
        // 返回上一页
        $('body').on('click', '.backBtn', function() {
                window.history.go(-1);
            })
            //上传小图标
        uploadIconPic('#small_upload_pick', '#small_icon', '/Coupon/AdminUploadCouponImg');
        uploadIconPic('#product_upload_pick', '#product_icon', '/Coupon/AdminUploadCouponImg');

        //input失去焦点颜色变回原来
        $('input[type="text"],input[type="number"]').on('blur', function() {
            $(this).css('border', '1px solid #ccc')
        })
        $('input[type="text"],input[type="number"]').on('focus', function() {
            $(this).css('border', '1px solid #3c8dbc')
        })

        $("#Whethertoopen").on("change", function() {
            if ($("input[name='Whethertoopen']:checked").val() == 1) {
                $(".Specialprice").show();

            } else {
                $(".Specialprice").hide();

            }
        })

        //完成
        $("#finish").on("click", function() {
            //礼品卡类型名称
            if (!Validate.emptyValidateAndFocusAndColor("#Name", "请输入礼品卡类型名称", "")) {
                return false;
            }

            //礼品卡序列号
            if (!Validate.emptyValidateAndFocusAndColor("#CouponTypeSn", "请输入礼品卡序列号", "")) {
                return false;
            }

            //价格
            if (!Validate.emptyValidateAndFocusAndColor("#Money", "请输入价格", "")) {
                return false;
            }

            if (!giftTypeRelease.getWhethertoopen()) {
                return false;
            }
            //礼品卡内容
            if (!Validate.emptyValidateAndFocusAndColor("#Summary", "请输入礼品卡内容", "")) {
                return false;
            }

            if ($('#DeliveryAreaId').val() == 0) {
                Common.showInfoMsg('请选择配送范围')
                return false
            }
            if ($('#TemplateId').val() == 0) {
                Common.showInfoMsg('请选择运费模板')
                return false;
            }

            //兑换结束日期
            // if (!Validate.emptyValidateAndFocusAndColor("#end_time", "请选择兑换结束日期", "")) {
            //     return false;
            // }

            if ($('#small_icon').attr('data-src') == "" || $('#small_icon').attr('data-src') == null) {
                Common.showInfoMsg('请上传礼品卡图片')
                return false;
            }

            if ($('#product_icon').attr('data-src') == "" || $('#product_icon').attr('data-src') == null) {
                Common.showInfoMsg('请上传商品图片')
                return false;
            }

            var CouponTypeId = Common.getUrlParam("CouponTypeId");

            if ($('#small_icon').attr('data-src') != "") {

                if (CouponTypeId != undefined && CouponTypeId != "") {
                    //编辑
                    giftTypeRelease.editProduct();
                } else {
                    //添加
                    giftTypeRelease.addProduct();
                }
            }

        });
    },
    //获取运费模板列表
    adminTemplatesList: function() {
        var methodName = "/templates/AdminTemplatesList";
        var data = {
            "Page": {
                "PageSize": 1000,
                "PageIndex": 1
            }
        };
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {

                var render = template.compile(giftTypeRelease.freightTpl);
                var html = render(data.Data);
                $("#TemplateId").append(html);
                //初始化搜索下拉框
                $("#TemplateId").chosen();
                //配送列表接口调用
                giftTypeRelease.deliveryAreaList();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    getWhethertoopen: function() {
        var result = false;
        if ($("input[name='Whethertoopen']:checked").val() == 1) {
            if ($("#CostPrice").val() != "") {
                result = true;
            } else {
                Common.showInfoMsg("请输入特价价格");
            }
        } else {
            result = true;
        }

        return result;
    },
    // 获取配送区域列表
    deliveryAreaList: function() {
        var methodName = "/Coupon/DeliveryAreaList";
        var data = {};
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                var render = template.compile(giftTypeRelease.deliveryTpl);
                var html = render(data.Data);
                $("#DeliveryAreaId").html(html);
                var CouponTypeId = Common.getUrlParam("CouponTypeId");
                if (CouponTypeId != undefined && CouponTypeId != "") {
                    giftTypeRelease.getProductInfo(CouponTypeId);
                }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 新增
    addProduct: function() {
        var methodName = "/Coupon/AdminAddCouponType";

        var data = {
            "Name": $('#Name').val(),
            "CouponTypeSn": $('#CouponTypeSn').val(),
            "Money": $('#Money').val(),
            "DeliveryAreaId": $('#DeliveryAreaId').val(),
            "IsCostPrice": $("input[name='Whethertoopen']:checked").val() == 1 ? 1 : 0,
            "CostPrice": $("input[name='Whethertoopen']:checked").val() == 1 ? $('#CostPrice').val() : 0.01,
            TemplateId: $("#TemplateId").val(),
            // "UseEndTime": $('#end_time').val(),
            "CouponImg": $('#small_icon').attr('data-src'),
            "ProductImg": $('#product_icon').attr('data-src'),
            "Content": $('#Summary').val()

        };
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("新增成功", function() {
                    location.href = '/giftCard/productGiftType'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 编辑
    editProduct: function() {
        var methodName = "/Coupon/AdminEditCouponType";

        var data = {
            "CouponTypeId": Common.getUrlParam('CouponTypeId'),
            "Name": $('#Name').val(),
            "CouponTypeSn": $('#CouponTypeSn').val(),
            "Money": $('#Money').val(),
            "IsCostPrice": $("input[name='Whethertoopen']:checked").val() == 1 ? 1 : 0,
            "CostPrice": $("input[name='Whethertoopen']:checked").val() == 1 ? $('#CostPrice').val() : 0.01,
            "DeliveryAreaId": $('#DeliveryAreaId').val(),
            TemplateId: $("#TemplateId").val(),
            // "UseEndTime": $('#end_time').val(),
            "CouponImg": $('#small_icon').attr('data-src'),
            "ProductImg": $('#product_icon').attr('data-src'),
            "Content": $('#Summary').val()

        };
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {

                Common.showSuccessMsg("编辑成功", function() {
                    location.href = '/giftCard/productGiftType'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取商品信息
    getProductInfo: function(CouponTypeId) {
        var methodName = "/Coupon/AdminGetCouponTypeDetail";
        var data = {
            CouponTypeId: CouponTypeId
        };
        SignRequest.setAsync(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                var result = data.Data;
                // 是否开启特价
                if (result.CouponTypeDetail.IsCostPrice) {
                    $("#Whethertoopen label").eq(0).find("input").prop("checked", true);
                    $(".Specialprice").show();
                } else {
                    $("#Whethertoopen label").eq(1).find("input").prop("checked", true);
                    $(".Specialprice").hide();

                }
                $("#CostPrice").val(result.CouponTypeDetail.CostPrice);
                $('#Name').val(result.CouponTypeDetail.Name)
                $('#CouponTypeSn').val(result.CouponTypeDetail.CouponTypeSn)
                $('#Money').val(result.CouponTypeDetail.Money)
                $('#DeliveryAreaId').val(result.CouponTypeDetail.DeliveryAreaId)
                $("#TemplateId").val(result.CouponTypeDetail.TemplateId);
                $('#TemplateId').trigger('chosen:updated'); //更新选项
                    // $('#end_time').val(result.CouponTypeDetail.UseEndTimes)
                $('#Summary').val(result.Content)
                $('#small_icon').attr('data-src', result.CouponTypeDetail.CouponImg)
                $('#small_icon').attr('src', result.CouponTypeDetail.CouponImgFull)
                $('#product_icon').attr('data-src', result.CouponTypeDetail.ProductImg)
                $('#product_icon').attr('src', result.CouponTypeDetail.ProductImgFull)
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};