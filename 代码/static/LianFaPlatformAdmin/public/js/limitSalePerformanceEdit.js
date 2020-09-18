var LimitSalePerformanceEdit = {
    //多选
    skuTemplate: `
                        {{each PrdouctSkuList as value i}}
                        <div class="panel panel-default" data-id="{{PrdouctSkuList[i].PId}}">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a class="skuTitle" data-toggle="collapse" data-parent="#accordion"
                                       href="#collapse{{i}}">
                                        {{PrdouctSkuList[i].PName}}
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse{{i}}" class="panel-collapse collapse">
                                <div class="panel-body">
                                   <table class="specification">
                                       <thead>
                                            <tr  style="border-bottom: 2px solid #ccc;">
                                                <th>规格</th>
                                                <th>库存</th>
                                                <th>活动库存</th>
                                                <th>一口价</th>
                                                <th>抢购价</th>
                                            </tr>
                                        </thead>
                                        <tbody  class="skuListBox">
                                            {{each PrdouctSkuList[i].ActivityProductSkuList as value j}}
                                                <tr style="border-bottom: 1px solid #ccc;" data-pid="{{PrdouctSkuList[i].ActivityProductSkuList[j].PId}}" data-num="{{PrdouctSkuList[i].ActivityProductSkuList[j].Number}}" data-shopPrice="{{PrdouctSkuList[i].ActivityProductSkuList[j].ShopPrice}}" data-skuid="{{PrdouctSkuList[i].ActivityProductSkuList[j].SkuId}}" data-dec="{{PrdouctSkuList[i].ActivityProductSkuList[j].Description}}">
                                                    <td id="pro_name">{{PrdouctSkuList[i].ActivityProductSkuList[j].Description}}</td>
                                                    <td id="inventory">{{PrdouctSkuList[i].ActivityProductSkuList[j].Number}}</td>
                                                    <td>
                                                        <input type="number" class="ActiveInventory" class="form-control"  value="1" style="width:120px;text-align: center;margin:auto">
                                                    </td>
                                                    <td id="fixedPrice">{{PrdouctSkuList[i].ActivityProductSkuList[j].ShopPrice}}</td>
                                                    <td>
                                                        <input type="number" class="PurchasePrice" class="form-control" value="1" style="width:120px;text-align: center;margin:auto">
                                                    </td>
                                                </tr>
                                             {{/each}}
                                        </tbody>
                                   </table>
                                   <div class="btnBox" style="margin-top: 15px;text-align: right"><button class="btn btn-default PdelectBtn" style="margin-right: 10px">删除</button><button data-index="{{i}}" class="btn btn-primary PeditBtn">重新选择</button></div>
                                   
                                </div>
                            </div>
                        </div>
                        {{/each}}
                        <div class="addBox"><button class="btn btn-default addPbtn" style="margin-top: 15px">添加商品</button></div>
    `,
    //单选
    skuTemplateSingle: `

                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a class="skuTitle" data-toggle="collapse" data-parent="#accordion"
                                       href="#collapse">
                                        {{PrdouctSkuInfo.PName}}
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse" class="panel-collapse collapse in">
                                <div class="panel-body">
                                   <table class="specification">
                                       <thead>
                                            <tr  style="border-bottom: 2px solid #ccc;">
                                                <th>规格</th>
                                                <th>库存</th>
                                                <th>活动库存</th>
                                                <th>一口价</th>
                                                <th>抢购价</th>
                                            </tr>
                                        </thead>
                                        <tbody  class="skuListBox">
                                            {{each PrdouctSkuInfo.ActivityProductSkuList as value j}}
                                                <tr style="border-bottom: 1px solid #ccc;" data-pid="{{PrdouctSkuInfo.ActivityProductSkuList[j].PId}}" data-num="{{PrdouctSkuInfo.ActivityProductSkuList[j].Number}}" data-shopPrice="{{PrdouctSkuInfo.ActivityProductSkuList[j].ShopPrice}}" data-skuid="{{PrdouctSkuInfo.ActivityProductSkuList[j].SkuId}}" data-dec="{{PrdouctSkuInfo.ActivityProductSkuList[j].Description}}">
                                                    <td id="pro_name">{{PrdouctSkuInfo.ActivityProductSkuList[j].Description}}</td>
                                                    <td id="inventory">{{PrdouctSkuInfo.ActivityProductSkuList[j].Number}}</td>
                                                    <td>
                                                        <input type="number" class="ActiveInventory" class="form-control"  value="1" style="width:120px;text-align: center;margin:auto">
                                                    </td>
                                                    <td id="fixedPrice">{{PrdouctSkuInfo.ActivityProductSkuList[j].ShopPrice}}</td>
                                                    <td>
                                                        <input type="number" class="PurchasePrice" class="form-control" value="1" style="width:120px;text-align: center;margin:auto">
                                                    </td>
                                                </tr>
                                             {{/each}}
                                        </tbody>
                                   </table>
                                   <div class="btnBox" style="margin-top: 15px;text-align: right"><button class="btn btn-default PdelectBtn" style="margin-right: 10px">删除</button><button data-index="0" class="btn btn-primary PeditBtn">重新选择</button></div>
                                   
                                </div>
                            </div>
    `,
    //获取信息的模板
    skuTemplateEdit: `
                        {{each TimeActivityProductInfoList as value i}}
                        <div class="panel panel-default" data-id="{{TimeActivityProductInfoList[i].PId}}">
                            <div class="panel-heading">
                                <h4 class="panel-title">
                                    <a class="skuTitle" data-toggle="collapse" data-parent="#accordion"
                                       href="#collapse{{i}}">
                                        {{TimeActivityProductInfoList[i].Name}}
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse{{i}}" class="panel-collapse collapse">
                                <div class="panel-body">
                                   <table class="specification">
                                       <thead>
                                            <tr  style="border-bottom: 2px solid #ccc;">
                                                <th>规格</th>
                                                <th>库存</th>
                                                <th>活动库存</th>
                                                <th>一口价</th>
                                                <th>抢购价</th>
                                            </tr>
                                        </thead>
                                        <tbody  class="skuListBox">
                                            {{each TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList as value j}}
                                                <tr style="border-bottom: 1px solid #ccc;" data-recordId="{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].RecordId}}" data-pid="{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].PId}}" data-num="{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].Number}}" data-shopPrice="{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].ShopPrice}}" data-skuid="{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].SkuId}}" data-dec="{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].Description}}">
                                                    <td id="pro_name">{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].Description}}</td>
                                                    <td id="inventory">{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].Number}}</td>
                                                    <td>
                                                        <input type="number" class="ActiveInventory" class="form-control"  value="{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].ANumber}}" style="width:120px;text-align: center;margin:auto">
                                                    </td>
                                                    <td id="fixedPrice">{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].ShopPrice}}</td>
                                                    <td>
                                                        <input type="number" class="PurchasePrice" class="form-control" value="{{TimeActivityProductInfoList[i].TimeActivityProductSkuInfoList[j].ToSnapUpPrice}}" style="width:120px;text-align: center;margin:auto">
                                                    </td>
                                                </tr>
                                             {{/each}}
                                        </tbody>
                                   </table>
                                   <div class="btnBox" style="margin-top: 15px;text-align: right"><button class="btn btn-default PdelectBtn" style="margin-right: 10px">删除</button><button data-index="{{i}}" class="btn btn-primary PeditBtn">重新选择</button></div>
                                   
                                </div>
                            </div>
                        </div>
                        {{/each}}
                        <div class="addBox"><button class="btn btn-default addPbtn" style="margin-top: 15px">添加商品</button></div>
    `,
    imgTpl: `
        {{each imglistData as value i}}
            <div class="img-container">
                <img class="select-img" src="{{imglistData[i].SlideshowFull}}" data-src="{{imglistData[i].Slideshow}}">
                <input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">
                <img class="close-img" src="/public/images/close.png" style="width: 100%;height: 100%;">
            </div>
        {{/each}}
        <div class="img-container">
            <img class="select-img" src="/public/images/addImg.png">
            <input class="img-input" type="file" accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">
            <img class="close-img" src="/public/images/close.png">
        </div>
    `,
    cateTpl: `
    {{each CategoryList as value i}}
        <option value="{{CategoryList[i].CateId}}">{{CategoryList[i].Name}}</option>
    {{/each}}
    `,
    //add为添加，edit为重新选择
    type: "",
    index: 0,

    init: function () {
        $('.addPbtn').show();

        LimitSalePerformanceEdit.adminTimeProductActivityInfo();

        //选择商品按钮点击
        $('body').on('click', '#choiceProduct', function () {
            ProductModal.destroy();
            ProductModal.isRefresh = true;
            ProductModal.isMultiple = true;
            ProductModal.init();

            //绑定点击选择触发函数
            ProductModal.onClickConfirm = function (obj) {
                var list = [];
                $('.checkbox').each(function (index, item) {
                    if (this.checked) {
                        list.push($(item).attr('data-id'))
                    }
                })
                if (list.length > 0) {
                    // if(list.length > 4){
                    //     Common.showInfoMsg('最多只能选择4个商品')
                    //     return false
                    // }
                    LimitSalePerformanceEdit.activityProductSkuByPId(list);
                } else {
                    Common.showInfoMsg('请选择商品');
                    return false
                }
            };

            $('#choiceProductModal').modal('show');
        });

        //重新选择按钮点击
        $('body').on('click', '.PeditBtn', function () {
            LimitSalePerformanceEdit.type = 'edit';
            //获取下标
            LimitSalePerformanceEdit.index = $(this).attr('data-index');
            //初始化选择商品模态框
            ProductModal.destroy();
            ProductModal.isRefresh = true;
            ProductModal.isMultiple = false;
            ProductModal.init();

            //绑定点击选择触发函数
            ProductModal.onClickChoice = function (obj) {
                var id = $(obj).attr('data-id');
                LimitSalePerformanceEdit.activityProductSkuByPIds(id);
                $('#choiceProductModal').modal('hide');
            }
            $('#choiceProductModal').modal('show');
        });

        //添加商品按钮点击
        $('body').on('click', '.addPbtn', function () {
            LimitSalePerformanceEdit.type = 'add';

            //初始化选择商品模态框
            ProductModal.destroy();
            ProductModal.isRefresh = true;
            ProductModal.isMultiple = false;
            ProductModal.init();

            //绑定点击选择触发函数
            ProductModal.onClickChoice = function (obj) {
                var id = $(obj).attr('data-id');
                LimitSalePerformanceEdit.activityProductSkuByPIds(id);
                $('#choiceProductModal').modal('hide');
            }
            $('#choiceProductModal').modal('show');
        });

        //分享图片
        uploadSharePic('#pick_share_icon', '#pic_share_img');

        //删除按钮点击
        $('body').on('click', '.PdelectBtn', function () {
            $(this).parents('.panel-default').remove();
            //重置排序
            $('.panel-default').each(function (index, item) {
                var id = 'collapse' + index;
                var href = '#collapse' + index;
                $(item).find('.panel-collapse').attr('id', id);
                $(item).find('.skuTitle').attr('href', href)
                $(item).find('.PeditBtn').attr('data-index', index)
            })
            $('.addPbtn').show();
        });

        //点击编辑按钮
        $('body').on('click', '#editBtn', function () {

            //开始时间
            if (!Validate.emptyValidateAndFocus("#start_time", "请输入开始时间", "")) {
                return false;
            }
            //结束时间
            if (!Validate.emptyValidateAndFocus("#end_time", "请输入结束时间", "")) {
                return false;
            }
            //每人限购数量
            if (!Validate.emptyValidateAndFocus("#LimitPurchase", "请输入每人限购数量", "")) {
                return false;
            }
            //活动标题
            if (!Validate.emptyValidateAndFocus("#ShareTitle", "请输入活动标题", "")) {
                return false;
            }

            var TimeActivityProductRequestInfoList = [];
            var flag = false;
            //规格属性
            $('.panel-default').each(function (index, item) {
                var productItemList = {};
                productItemList.PId = $(item).attr('data-id');
                productItemList.TimeActivityProductSkuInfoList = [];

                $(item).find('.skuListBox').find('tr').each(function (index1, item1) {
                    if ($(item1).find('.ActiveInventory').val() == "" || $(item1).find('.PurchasePrice').val() == "") {
                        flag = true;
                    }
                    var data = {};
                    data.PId = $(item1).attr('data-pid');
                    data.SkuId = $(item1).attr('data-skuid');
                    data.ANumber = $(item1).find('.ActiveInventory').val();
                    data.Description = $(item1).attr('data-dec');
                    data.ToSnapUpPrice = $(item1).find('.PurchasePrice').val();
                    data.Number = $(item1).attr('data-num');
                    data.ShopPrice = $(item1).attr('data-shopPrice');
                    data.RecordId = $(item1).attr('data-recordId') ? $(item1).attr('data-recordId') : 0;
                    productItemList.TimeActivityProductSkuInfoList.push(data);

                })
                TimeActivityProductRequestInfoList.push(productItemList)
            })
            if (flag) {
                Common.showInfoMsg('规格值不能为空')
                return false;
            }

            LimitSalePerformanceEdit.adminEditTimeProductActivity(TimeActivityProductRequestInfoList)
        });
    },
    //后台限时商品活动信息
    adminTimeProductActivityInfo: function () {
        //请求方法
        var methodName = "/timeproductactivity/AdminTimeProductActivitySpecialInfo";
        var data = {
            "ActivityId": Common.getUrlParam('ActivityId'),
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
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

                var render = template.compile(LimitSalePerformanceEdit.skuTemplateEdit);
                var html = render(data.Data);
                $("#accordion").show();
                $("#accordion").html(html);
                // if($('.panel-default').length > 3){
                //     $('.addPbtn').hide()
                // }

                var imgList = {
                    imglistData: data.Data.SlideshowList,
                };
                var render = template.compile(LimitSalePerformanceEdit.imgTpl);
                var html = render(imgList);
                $("#productImg").html(html);


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台编辑限时商品活动
    adminEditTimeProductActivity: function (PrdouctSkuList) {
        var Img = [];
        var imgList = $(".productImg .img-container");
        for (var i = 0; i < imgList.length; i++) {
            var imgSrc = imgList.eq(i).find(".select-img").attr("src");
            if (imgSrc != "/public/images/addImg.png") {
                var dataSrc = imgList.eq(i).find(".select-img").attr("data-src");
                Img.push(dataSrc);
            }
        }
        //请求方法
        var methodName = "/timeproductactivity/AdminEditTimeProductActivitySpecial";
        var data = {
            "ActivityId": Common.getUrlParam('ActivityId'),
            "ANumber": $('#ActiveInventory').val(),
            "ToSnapUpPrice": $('#PurchasePrice').val(),
            "StartTime": $('#start_time').val(),
            "EndTime": $('#end_time').val(),
            "LimitCount": $('#LimitPurchase').val(),
            "AInstructions": $('#ActivityDescription').val(),
            "Title": $('#ShareTitle').val(),
            "Details": $('#ShareContent').val(),
            "Icon": $('#pic_share_img').attr('data-src') ? $('#pic_share_img').attr('data-src') : "",
            'TimeActivityProductRequestInfoList': PrdouctSkuList,
            "SlideshowList": Img,
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功!', function () {
                    location.href = "/marketMode/limitSalePerformanceIng"
                })


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //根据商品id获得限时活动商品sku多选
    activityProductSkuByPId: function (list) {
        //请求方法
        var methodName = "/timeproductactivity/ActivityProductSkuByPId";
        var data = {
            "PIdList": list
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#choiceProductModal').modal('hide');
                var render = template.compile(LimitSalePerformanceEdit.skuTemplate);
                var html = render(data.Data);
                $("#accordion").html(html);
                $('#accordion').show();
                //如果数量等于4把添加按钮隐藏
                // if(data.Data.PrdouctSkuList.length == 4){
                //     $('.addPbtn').hide();
                // }else{
                //     $('.addPbtn').show();
                // }
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //根据商品id获得限时活动商品sku单选
    activityProductSkuByPIds: function (id) {
        //请求方法
        var methodName = "/timeproductactivity/ActivityProductSkuByPIds";
        var data = {
            "PId": id
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#choicePresentModalSingle').modal('hide');
                //代表重新编辑
                if (LimitSalePerformanceEdit.type == 'edit') {
                    var render = template.compile(LimitSalePerformanceEdit.skuTemplateSingle);
                    var html = render(data.Data);
                    $('.panel-default').eq(LimitSalePerformanceEdit.index).html(html)
                    $('.panel-default').eq(LimitSalePerformanceEdit.index).attr('data-id', data.Data.PrdouctSkuInfo.PId);
                    var href = '#collapse' + LimitSalePerformanceEdit.index;
                    var id = 'collapse' + LimitSalePerformanceEdit.index;
                    $('.panel-default').eq(LimitSalePerformanceEdit.index).find('.skuTitle').attr('href', href);
                    $('.panel-default').eq(LimitSalePerformanceEdit.index).find('.panel-collapse').attr('id', id);
                    $('.panel-default').eq(LimitSalePerformanceEdit.index).find('.PeditBtn').attr('data-index', LimitSalePerformanceEdit.index)
                } else {
                    //代表添加商品
                    //获取下标
                    var index = $('.panel-default').length ? $('.panel-default').length : 0;
                    var div = document.createElement("div");
                    $(div).addClass('panel')
                    $(div).addClass('panel-default')
                    $(div).attr('data-id', data.Data.PrdouctSkuInfo.PId);
                    $('.addBox').before(div);
                    var render = template.compile(LimitSalePerformanceEdit.skuTemplateSingle);
                    var html = render(data.Data);
                    $(div).html(html)
                    var href = '#collapse' + index;
                    var id = 'collapse' + index;
                    $(div).find('.skuTitle').attr('href', href)
                    $(div).find('.panel-collapse').attr('id', id)
                    $(div).find('.PeditBtn').attr('data-index', index)
                    // if($('.panel-default').length >= 4){
                    //     $('.addPbtn').hide()
                    // }

                }



            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //商品列表bootstrapTable多选
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
            queryParams: LimitSalePerformanceEdit.queryProParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: LimitSalePerformanceEdit.responseProHandler,
            columns: [{
                    field: 'PId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-id="' + value + '" style="display: inline-block;">'
                        return html;
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
                // {
                //     field: 'PId',
                //     title: '操作',
                //     align: 'center',
                //     valign: 'middle',
                //     formatter: function (value, row, index) {
                //         var html = "<span class='sure_choice' data-num='" + row.number + "' data-price='" + row.ShopPrice + "'  data-id='" + value + "'>选择</span>";
                //         return html;
                //     }
                // }
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
            obj = {
                "Name": $('#choice_presentName').val(),
                "Bname": "",
                "Cname": "",
            };
        } else {
            obj = parame;
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
        LimitSalePerformanceEdit.initProBootstrapTable();
    },
    //商品列表bootstrapTable单选
    initProBootstrapTableSingle: function () {
        $('#choice_goods_tbSingle').bootstrapTable({
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
            queryParams: LimitSalePerformanceEdit.queryProParamsSingle, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: LimitSalePerformanceEdit.responseProHandlerSingle,
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
    queryProParamsSingle: function (params) {
        //配置参数
        //方法名
        var methodName = "/product/ActivityProductList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            "Name": $('#choice_presentNameSingle').val(),
            Page: {
                PageSize: params.limit, //页面大小,
                PageIndex: (params.offset / params.limit) + 1, //页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseProHandlerSingle: function (res) {
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
    projectProQuerySingle: function (parame) {
        //方法名
        var methodName = "/product/ActivityProductList";

        if (parame == "" || parame == undefined) {
            obj = {
                "Name": $('#choice_presentNameSingle').val(),
                "Bname": "",
                "Cname": "",
            };
        } else {
            obj = parame;
        }

        $('#choice_goods_tbSingle').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新
    projectProDestoryQuerySingle: function (parame) {
        //方法名
        var methodName = "/product/ActivityProductList";

        if (parame == "" || parame == undefined) {
            obj = {
                "Name": $('#choice_presentNameSingle').val(),
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

        $('#choice_goods_tbSingle').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
        LimitSalePerformanceEdit.initProBootstrapTableSingle();
    },
    // 获取商品分类
    getProductCategory: function () {
        var methodName = "/category/AdminCategoryList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(LimitSalePerformanceEdit.cateTpl);
                var html = render(data.Data);
                $("#CateId").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
}

$(function () {
    LimitSalePerformanceEdit.init()
});