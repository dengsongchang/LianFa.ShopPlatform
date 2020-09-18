var LimitSaleEdit = {
    //有规格
    skuTemplate: `
                            <thead>
                                <tr  style="border-bottom: 2px solid #ccc;">
                                    <th>规格</th>
                                    <th>库存</th>
                                    <th>活动库存</th>
                                    <th>一口价</th>
                                    <th>抢购价</th>
                                </tr>
                            </thead>
                            <tbody  id="skuListBox">
                            {{each PrdouctSkuList as value i}}
                                <tr style="border-bottom: 1px solid #ccc;" data-pid="{{PrdouctSkuList[i].PId}}" data-num="{{PrdouctSkuList[i].Number}}" data-shopPrice="{{PrdouctSkuList[i].ShopPrice}}" data-skuid="{{PrdouctSkuList[i].SkuId}}" data-dec="{{PrdouctSkuList[i].Description}}">
                                    <td id="pro_name">{{PrdouctSkuList[i].Description}}</td>
                                    <td id="inventory">{{PrdouctSkuList[i].Number}}</td>
                                    <td>
                                        <input type="number" class="ActiveInventory" class="form-control"  value="1" style="width:120px;text-align: center;margin:auto">
                                    </td>
                                    <td id="fixedPrice">{{PrdouctSkuList[i].ShopPrice}}</td>
                                    <td>
                                        <input type="number" class="PurchasePrice" class="form-control" value="1" style="width:120px;text-align: center;margin:auto">
                                    </td>
                                </tr>
                             {{/each}}
                            </tbody>
    `,
    //编辑获取信息用的
    skuTemplate1: `
                            <thead>
                                <tr  style="border-bottom: 2px solid #ccc;">
                                    <th>规格</th>
                                    <th>库存</th>
                                    <th>活动库存</th>
                                    <th>一口价</th>
                                    <th>抢购价</th>
                                </tr>
                            </thead>
                            <tbody  id="skuListBox">
                            {{each TimeProductActivitySKuList as value i}}
                                <tr style="border-bottom: 1px solid #ccc;" data-recordId="{{TimeProductActivitySKuList[i].RecordId}}" data-pid="{{TimeProductActivitySKuList[i].PId}}" data-num="{{TimeProductActivitySKuList[i].Number}}" data-shopPrice="{{TimeProductActivitySKuList[i].ShopPrice}}" data-skuid="{{TimeProductActivitySKuList[i].SkuId}}" data-dec="{{TimeProductActivitySKuList[i].Description}}">
                                    <td id="pro_name">{{TimeProductActivitySKuList[i].Description}}</td>
                                    <td id="inventory">{{TimeProductActivitySKuList[i].Number}}</td>
                                    <td>
                                        <input type="number" class="ActiveInventory" class="form-control"  value="{{TimeProductActivitySKuList[i].ANumber}}" style="width:120px;text-align: center;margin:auto">
                                    </td>
                                    <td id="fixedPrice">{{TimeProductActivitySKuList[i].ShopPrice}}</td>
                                    <td>
                                        <input type="number" class="PurchasePrice" class="form-control" value="{{TimeProductActivitySKuList[i].ToSnapUpPrice}}" style="width:120px;text-align: center;margin:auto">
                                    </td>
                                </tr>
                             {{/each}}
                            </tbody>
    `,
    //没规格

    skuTemplate2: `
                            <thead>
                                <tr  style="border-bottom: 2px solid #ccc;">
                                    <th>规格</th>
                                    <th>库存</th>
                                    <th>活动库存</th>
                                    <th>一口价</th>
                                    <th>抢购价</th>
                                </tr>
                            </thead>
                            <tbody  id="skuListBox">
                                <tr style="border-bottom: 1px solid #ccc;"  data-pid="{{PrdouctSkuList[0].PId}}" data-num="{{PrdouctSkuList[0].Number}}" data-shopPrice="{{PrdouctSkuList[0].ShopPrice}}" data-skuid="{{PrdouctSkuList[0].SkuId}}" data-dec="{{PrdouctSkuList[0].Description}}">
                                    <td id="pro_name">{{PrdouctSkuList[0].Description}}</td>
                                    <td id="inventory">{{PrdouctSkuList[0].Number}}</td>
                                    <td>
                                        <input type="number" class="ActiveInventory" class="form-control"  value="1" style="width:120px;text-align: center;margin:auto">
                                    </td>
                                    <td id="fixedPrice">{{PrdouctSkuList[0].ShopPrice}}</td>
                                    <td>
                                        <input type="number" class="PurchasePrice" class="form-control" value="1" style="width:120px;text-align: center;margin:auto">
                                    </td>
                                </tr>
                            </tbody>
    `,
    pid: "",
    init: function () {
        //获取限时抢购商品详情
        LimitSaleEdit.adminTimeProductActivityInfo();

        //初始化选择商品模态框
        ProductModal.init();

        //绑定点击选择触发函数
        ProductModal.onClickChoice = function (obj) {
            //商品名称
            var id = obj.attr('data-id');
            var txt_name = $(obj).parents('tr').find('.goods_name_modal').text();
            $('.goods-name_final').text(txt_name);
            $('.goods-name_final').show();
            $('.goods-name_final').attr('data-id', $(obj).attr('data-id'))
            LimitSaleEdit.activityProductSkuByPId(id);
            $('#choiceProductModal').modal('hide');
        }

        //分享图片
        uploadSharePic('#pick_share_icon', '#pic_share_img');

        //点击编辑按钮
        $('body').on('click', '#editBtn', function () {
            //商品名称
            if ($('.goods-name_final').is(':visible')) {

            } else {
                Common.showInfoMsg('请输入商品名称')
                return false;
            }

            //开始时间
            if (!Validate.emptyValidateAndFocus("#start_time", "请输入开始时间", "")) {
                return false;
            }
            //结束时间
            if (!Validate.emptyValidateAndFocus("#end_time", "请输入结束时间", "")) {
                return false;
            }
            //每人限购数量
            // if (!Validate.emptyValidateAndFocus("#LimitPurchase", "请输入每人限购数量", "")) {
            //     return false;
            // }
            var PrdouctSkuList = [];
            var flag = false;
            //规格属性
            $('#skuListBox').find('tr').each(function (index, item) {
                if ($(item).find('.ActiveInventory').val() == "" || $(item).find('.PurchasePrice').val() == "") {
                    flag = true;
                }
                var data = {};
                data.PId = $(item).attr('data-pid');
                data.SkuId = $(item).attr('data-skuid');
                data.ANumber = $(item).find('.ActiveInventory').val();
                data.Description = $(item).attr('data-dec');
                data.ToSnapUpPrice = $(item).find('.PurchasePrice').val();
                data.Number = $(item).attr('data-num');
                data.ShopPrice = $(item).attr('data-shopPrice');
                data.RecordId = $(item).attr('data-recordId') ? $(item).attr('data-recordId') : 0;
                PrdouctSkuList.push(data)
            })
            if (flag) {
                Common.showInfoMsg('规格值不能为空')
                return false;
            }
            LimitSaleEdit.adminEditTimeProductActivity(PrdouctSkuList)

        })
    },
    //后台限时商品活动信息
    adminTimeProductActivityInfo: function () {
        //请求方法
        var methodName = "/timeproductactivity/AdminTimeProductActivityInfo";
        var data = {
            "ActivityId": Common.getUrlParam('ActivityId'),
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                LimitSaleEdit.pid = data.Data.TimeProductActivityInfo.PId;
                $('.goods-name_final').text(data.Data.TimeProductActivityInfo.Name);
                $('#start_time').val(data.Data.TimeProductActivityInfo.StartTime.replace('T', " "));
                $('#end_time').val(data.Data.TimeProductActivityInfo.EndTime.replace('T', " "));
                $('#LimitPurchase').val(data.Data.TimeProductActivityInfo.LimitCount);
                $('#ActivityDescription').val(data.Data.TimeProductActivityInfo.AInstructions);
                $('#ShareTitle').val(data.Data.TimeProductActivityInfo.Title);
                $('#ShareContent').val(data.Data.TimeProductActivityInfo.Details);
                if (data.Data.TimeProductActivityInfo.IconFull) {
                    $('#pic_share_img').attr('src', data.Data.TimeProductActivityInfo.IconFull)
                    $('#pic_share_img').attr('data-src', data.Data.TimeProductActivityInfo.Icon)
                }
                $('.goods-name_final').attr('data-id', data.Data.TimeProductActivityInfo.PId);

                var render = template.compile(LimitSaleEdit.skuTemplate1);
                var html = render(data.Data);
                $("#specification").html(html);


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //根据商品id获得限时活动商品sku
    activityProductSkuByPId: function (id) {
        //请求方法
        var methodName = "/timeproductactivity/AdminActivityProductSkuByPId";
        var data = {
            "PId": id
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                if (data.Data.IsSign) {
                    var render = template.compile(LimitSaleEdit.skuTemplate);
                    var html = render(data.Data);
                    $("#specification").html(html);
                    $('#specification').show();
                } else {
                    var render = template.compile(LimitSaleEdit.skuTemplate2);
                    var html = render(data.Data);
                    $("#specification").html(html);
                    $('#specification').show();
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台编辑限时商品活动
    adminEditTimeProductActivity: function (PrdouctSkuList) {
        //请求方法
        var methodName = "/timeproductactivity/AdminEditTimeProductActivity";
        var data = {
            "ActivityId": Common.getUrlParam('ActivityId'),
            "PId": [
                $('.goods-name_final').attr('data-id')
            ],
            "ANumber": $('#ActiveInventory').val(),
            "ToSnapUpPrice": $('#PurchasePrice').val(),
            "StartTime": $('#start_time').val(),
            "EndTime": $('#end_time').val(),
            "LimitCount": $('#LimitPurchase').val(),
            "AInstructions": $('#ActivityDescription').val(),
            "Title": $('#ShareTitle').val(),
            "Details": $('#ShareContent').val(),
            "Icon": $('#pic_share_img').attr('data-src') ? $('#pic_share_img').attr('data-src') : "",
            'EditPrdouctSkuList': PrdouctSkuList,
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功!', function () {
                    location.href = "/marketMode/limitSaleIng"
                })


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取商品分类
    getProductCategory: function () {
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
    //商品列表bootstrapTable
    initProBootstrapTable: function () {
        $('#choice_goods_tb').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/product/ActivityProductList',
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
            queryParams: LimitSaleEdit.queryProParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: LimitSaleEdit.responseProHandler,
            columns: [{
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
                        var html = "<span class='sure_choice' data-num='" + row.number + "' data-price='" + row.ShopPrice + "'  data-id='" + value + "'>选择</span>";
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

                $('.caret').remove()

            },
            onLoadError: function (data) {
                $('#recoveryTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法

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
    queryProParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/product/ActivityProductList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            "Name": $('#choice_presentName').val(),
            "Bname": "",
            "Cname": "",
            Page: {
                PageSize: params.limit, //页面大小,
                PageIndex: (params.offset / params.limit) + 1, //页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseProHandler: function (res) {
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
    projectProQuery: function (parame) {
        //方法名
        var methodName = "/product/ActivityProductList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "Name": $('#choice_presentName').val(),
                "Bname": "",
                "Cname": "",
            };
        } else {
            var obj = parame;
        }

        $('#choice_goods_tb').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新
    projectProDestoryQuery: function (parame) {
        //方法名
        var methodName = "/product/ActivityProductList";

        if (parame == "" || parame == undefined) {
            obj = {
                "Name": $('#choice_presentName').val(),
                "Bname": "",
                "Cname": "",
                Page: {
                    PageSize: 10, //页面大小,
                    PageIndex: 1, //页码
                }
            };
        } else {
            obj = parame;
        }

        $('#choice_goods_tb').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
        LimitSaleEdit.initProBootstrapTable();
    }
}

$(function () {
    LimitSaleEdit.init()
});