$(function () {
    ProductRelease.init();
})
var ProductRelease = {

    labelTpl: `
        {{each ProductLabelList as value i}}
            <label class="checkbox-inline">
                <input type="checkbox" data-id="{{ProductLabelList[i].PLId}}" value="{{ProductLabelList[i].PLId}}"> {{ProductLabelList[i].Name}}
            </label>
        {{/each}}
    `,
    couponTypeTpl: `
        <option value="0">请选择礼品卡类型</option>
        {{each CouponTypeList as value i}}
            <option value="{{CouponTypeList[i].CouponTypeId}}">{{CouponTypeList[i].Name}}</option>
        {{/each}}
    `,
    init: function () {
        ProductRelease.miniCouponTypeList();
        //订单筛选时间
        laydate.render({
            elem: '#start_time', //指定元素
            type: 'datetime'
        });
        laydate.render({
            elem: '#end_time', //指定元素
            type: 'datetime'
        });
        // 返回上一页
        $('body').on('click', '.backBtn', function () {
            window.history.go(-1);
        })

        //上传小图标
        uploadIconPic('#small_upload_pick', '#small_icon', '/Coupon/AdminUploadCouponImg');
        uploadIconPic('#product_upload_pick', '#product_icon', '/Coupon/AdminUploadCouponImg');

        //input失去焦点颜色变回原来
        $('input[type="text"],input[type="number"]').on('blur', function () {
            $(this).css('border', '1px solid #ccc')
        })
        $('input[type="text"],input[type="number"]').on('focus', function () {
            $(this).css('border', '1px solid #3c8dbc')
        })


        //完成
        $("#finish").on("click", function () {

            // if($('#couponType').val() == 0){
            //     Common.showInfoMsg('请选择礼品卡类型')
            //     return false
            // }
            //礼品卡序列号
            if (!Validate.emptyValidateAndFocusAndColor("#CouponSn", "请输入礼品卡序列号", "")) {
                return false;
            }
            //密码
            if (!Validate.emptyValidateAndFocusAndColor("#PassWord", "请输入密码", "")) {
                return false;
            }
            //用户手机号码
            if (!Validate.emptyValidateAndFocusAndColor("#Mobile", "请输入用户手机号码", "")) {
                return false;
            }


            var PId = Common.getUrlParam("PId");
            if ($('#small_icon').attr('data-src') != "") {
                if (PId != undefined && PId != "") {
                    //编辑
                    ProductRelease.editProduct();
                } else {
                    //添加
                    ProductRelease.addProduct();
                }
            }

        });
    },
    // 礼品卡类型
    miniCouponTypeList: function () {
        var methodName = "/Coupon/MiniCouponTypeList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(ProductRelease.couponTypeTpl);
                var html = render(data.Data);
                $("#couponType").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 新增
    addProduct: function () {
        var methodName = "/Coupon/AdminAddCoupon";
        var data = {
            "CouponSn": $('#CouponSn').val(),
            "PassWord": $('#PassWord').val(),
            "Mobile": $('#Mobile').val()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("新增成功", function () {
                    location.href = '/giftCard/giftCardList'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 编辑
    editProduct: function () {
        var methodName = "/product/AdminEditProduct";
        var data = {
            "CouponSn": $('#CouponSn').val(),
            "PassWord": $('#PassWord').val(),
            "Mobile": $('#Mobile').val()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("编辑成功", function () {
                    location.href = '/giftCard/giftCardList'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取商品信息
    getProductInfo: function (PId) {
        var methodName = "/product/AdminProductInfo";
        var data = {
            PId: PId
        };
        SignRequest.setAsync(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
};