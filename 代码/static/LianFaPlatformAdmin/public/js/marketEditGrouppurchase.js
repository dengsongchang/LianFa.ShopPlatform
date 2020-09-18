$(function () {
    MarketGrouppurchase.init();
})

var MarketGrouppurchase = {
    //有规格
    skuTemplate: `
                            <thead>
                                <tr  style="border-bottom: 2px solid #ccc;">
                                    <th>规格</th>
                                    <th>库存</th>
                                    <th>活动库存</th>
                                    <th>原价</th>
                                    <th>团购价</th>
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
                                    <th>原价</th>
                                    <th>团购价</th>
                                </tr>
                            </thead>
                            <tbody  id="skuListBox">
                            {{each GroupBuyModelProductSkuList as value i}}
                                <tr style="border-bottom: 1px solid #ccc;" data-pid="{{GroupBuyModelProductSkuList[i].PId}}" data-num="{{GroupBuyModelProductSkuList[i].Stock}}" data-shopPrice="{{GroupBuyModelProductSkuList[i].ShopPrice}}" data-skuid="{{GroupBuyModelProductSkuList[i].SkuId}}" data-dec="{{GroupBuyModelProductSkuList[i].Description}}">
                                    <td id="pro_name">{{GroupBuyModelProductSkuList[i].Description}}</td>
                                    <td id="inventory">{{GroupBuyModelProductSkuList[i].Number}}</td>
                                    <td>
                                        <input type="number" class="ActiveInventory" class="form-control"  value="{{GroupBuyModelProductSkuList[i].Stock}}" style="width:120px;text-align: center;margin:auto">
                                    </td>
                                    <td id="fixedPrice">{{GroupBuyModelProductSkuList[i].ShopPrice}}</td>
                                    <td>
                                        <input type="number" class="PurchasePrice" class="form-control" value="{{GroupBuyModelProductSkuList[i].GroupPrice}}" style="width:120px;text-align: center;margin:auto">
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
                                <tr style="border-bottom: 1px solid #ccc;" data-pid="{{PrdouctSkuList[0].PId}}" data-num="{{PrdouctSkuList[0].Number}}" data-shopPrice="{{PrdouctSkuList[0].ShopPrice}}" data-skuid="{{PrdouctSkuList[0].SkuId}}" data-dec="{{PrdouctSkuList[0].Description}}">
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
    //标识是否选中商品
    proid: "",

    init: function () {
        //模态框出现获取数据
        $('#choicePresentModal').on('shown.bs.modal', function () {
            MarketGrouppurchase.initProBootstrapTable()
        });
        laydate.render({
            elem: '#start', //指定元素
            type: 'datetime'
        });
        laydate.render({
            elem: '#end', //指定元素
            type: 'datetime'
        });
        //获取信息
        MarketGrouppurchase.adminSelPorducts()
        //全选按钮点击
        $('body').on('click', '#stuCheckBox', function () {
            if (this.checked) {
                $('.order_checkbox').each(function (index, item) {
                    this.checked = true;
                })
            } else {
                $('.order_checkbox').each(function (index, item) {
                    this.checked = false;
                })
            }
        })
        $('body').on('click', '#addBrand', function () {
            MarketGrouppurchase.proid = "";
            $('#oneprice').hide();
            $('#pro_name').hide();
            $('#oneprice').find('span').text("");
            $('#pro_name').text("");
        })
        //编辑按钮点击
        $('body').on('click', '#editBtn', function () {
            console.log(!MarketGrouppurchase.proid)
            //是否选择了商品
            if (!MarketGrouppurchase.proid) {
                Common.showInfoMsg('请选择商品', function () {

                })
                return false;
            }
            //是否选择了商品
            if (!MarketGrouppurchase.proid) {
                Common.showInfoMsg('请选择商品', function () {

                })
                return false;
            }
            //开始日期
            if (!Validate.emptyValidateAndFocus("#start", "请输入开始日期", "")) {
                return false;
            }
            //结束日期
            if (!Validate.emptyValidateAndFocus("#end", "请输入结束日期", "")) {
                return false;
            }
            //持续时间
            if (!Validate.emptyValidateAndFocus("#Timelimit", "请输入持续时间", "")) {
                return false;
            }
            //限购总数量
            // if (!Validate.emptyValidateAndFocus("#PurchaseLimitationcount", "请输入限购总数量", "")) {
            //     return false;
            // }
            //团购满足数量
            if (!Validate.emptyValidateAndFocus("#GroupBuyingcount", "请输入团购满足数量", "")) {
                return false;
            }
            var PrdouctSkuList = [];
            var flag = false;
            //规格属性
            $('#skuListBox').find('tr').each(function (index, item) {
                if ($(item).find('.ActiveInventory').val() == "" || $(item).find('.PurchasePrice').val() == "") {
                    flag = true;
                }
                var data = {};
                data.SkuId = $(item).attr('data-skuid');
                data.Stock = $(item).find('.ActiveInventory').val();
                data.Description = $(item).attr('data-dec');
                data.GroupPrice = $(item).find('.PurchasePrice').val();
                PrdouctSkuList.push(data)
            })
            if (flag) {
                Common.showInfoMsg('规格值不能为空')
                return false;
            }
            MarketGrouppurchase.addGroupBuying(PrdouctSkuList)
        });
        //商品查询按钮点击
        $('body').on('click', '#searchPro', function () {
            MarketGrouppurchase.projectProQuery()
        });
        //弹窗查询按钮点击
        $('body').on('click', '#prosearchBtn', function () {
            MarketGrouppurchase.projectProQuery()
        });
        //选择按钮点击
        $('body').on('click', '.sure_choice', function () {
            $('#choicePresentModal').modal("hide");
            //表示选中了
            MarketGrouppurchase.proid = $(this).attr('data-id');
            $('#oneprice').find('span').text($(this).attr('data-price'))
            $('#oneprice').show();
            $('#pro_name').text($(this).attr('data-name'));
            $('#pro_name').show();
            console.log(MarketGrouppurchase.proid)
            MarketGrouppurchase.activityProductSkuByPId(MarketGrouppurchase.proid);

        });

    },
    //编辑团购
    addGroupBuying: function (PrdouctSkuList) {
        var methodName = "/groupbuying/AdminEditGroupBuyActiviti";
        var data = {
            "AId": Common.getUrlParam('id'),
            "StartTime": moment($('#start').val()).format('X'),
            "EndTime": moment($('#end').val()).format("X"),
            "Number": $('#GroupBuyingcount').val(),
            "Price": 0.01,
            "Stock": 1,
            "Sort": $('#sort').val(),
            "PId": MarketGrouppurchase.proid,
            "Timelimit": $('#Timelimit').val(),
            "GroupPrice":0.01,
            'GroupBuyProductSkuList': PrdouctSkuList,
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {

                Common.showSuccessMsg("编辑成功", function () {
                    location.href = '/marketMode/marketGrouppurchase'
                    // MarketGrouppurchase.projectQuery();
                });

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取团购信息
    adminSelPorducts: function () {
        var methodName = "/groupbuying/AdminGroupBuyActivitiInfo";
        var data = {
            "AId": Common.getUrlParam('id')
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                MarketGrouppurchase.proid = data.Data.PId;
                $('#proprice').html(data.Data.Price);
                $('#start').val(moment(data.Data.StartTime).format("YYYY-MM-D HH:mm:ss"));
                $('#end').val(moment(data.Data.EndTime).format("YYYY-MM-D HH:mm:ss"));
                // $('#PurchaseLimitationcount').val(data.Data.Stock);
                $('#GroupBuyingcount').val(data.Data.Number);
                $('#pro_name').html(data.Data.Name)
                $('#Timelimit').val(data.Data.Timelimit)
                $('#sort').val(data.Data.Sort)
                var render = template.compile(MarketGrouppurchase.skuTemplate1);
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
        var methodName = "/timeproductactivity/ActivityProductSkuByPId";
        var data = {
            "PId": id
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                if (data.Data.IsSign) {
                    var render = template.compile(MarketGrouppurchase.skuTemplate);
                    var html = render(data.Data);
                    $("#specification").html(html);
                    $('#specification').show();
                } else {
                    var render = template.compile(MarketGrouppurchase.skuTemplate2);
                    var html = render(data.Data);
                    $("#specification").html(html);
                    $('#specification').show();
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //商品列表bootstrapTable
    initProBootstrapTable: function () {
        $('#choice_goods_tb').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/groupbuying/AdminProductsList',
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
            queryParams: MarketGrouppurchase.queryProParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: MarketGrouppurchase.responseProHandler,
            columns: [
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
                        var html = "<span class='sure_choice' data-name='" + row.Name + "' data-price='" + row.ShopPrice + "'  data-id='" + value + "'>选择</span>";
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
        var methodName = "/groupbuying/AdminProductsList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            "Pname": $('#ProductName').val(),
            "Bname": "",
            "Cname": "",
            Page: {
                PageSize: params.limit,//页面大小,
                PageIndex: (params.offset / params.limit) + 1,//页码
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
        var methodName = "/groupbuying/AdminProductsList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "Pname": $('#choice_presentName').val(),
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

}