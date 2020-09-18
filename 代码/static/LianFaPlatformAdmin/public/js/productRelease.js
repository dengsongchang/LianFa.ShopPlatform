$(function() {
    ProductRelease.init();
})
var ProductRelease = {
    //获取最多种的组合方式（包含没选中）
    allcombination: {},
    //未选中的列表
    uncheckList: [],
    //未选中的id列表
    uncheckIDList: [],
    //allKeys表示当前选择规格生成的所有种类
    allKeys: {},
    //allsets取得集合的所有子集「幂集」
    allSets: [],
    //allIds
    allIds: {},
    //result(选中的结果，带属性下标)
    result: "",
    //ids id集合
    ids: "",
    //skuid skuid集合
    skuid: "",
    //是否开启规格
    isOpen: false,
    skuAttrTpl: `        
                            {{each AttributesList as value i}}
                            <li class="skuItem">
                                <span class="formitemtitle" style="width:100%;float:left;" data-type="{{AttributesList[i].Name}}" data-id="{{AttributesList[i].AttrId}}">{{AttributesList[i].Name}}：</span>
                                <span class="skuItemList">
                                    <ul style="width:100%;float:left;" class="skuItemListUl">
                                        {{each AttributesList[i].AttrValueList as value j}}
                                        <li style="padding:5px 0;margin-bottom:0;float:left;" class="contentLi">
                                            <div class="checkBoxList">
                                                <input type="checkbox" {{AttributesList[i].AttrValueList[j].IsSign ? 'checked' : '' }} data-id="{{AttributesList[i].AttrValueList[j].AttrValueId}}">
                                                <span style="float:left;display: block">{{AttributesList[i].AttrValueList[j].AttrValue}}</span>
                                            </div>
                                        </li>
                                       {{/each}}
                                        <li style="float:left;overflow:hidden;width:auto;" class="addBox">
                                            <a id="btnadd_53" href="javascript:;"
                                               style="line-height: 1.8; float: left;">添加</a>
                                            <span style="margin-top: 10px; display: none;overflow: hidden" id="sp_53">
                                                <input type="text" id="txt_53" class="form-control"
                                                       style="width:160px;float: left" name="txt_53">
                                                <input type="button" id="btnins_53"
                                                       class="btn btn-primary addSkuValueBtn" value="保存"
                                                       style="margin-left:10px;float: left">
                                            </span>
                                        </li>

                                    </ul>
                                </span>
                            </li>
                            {{/each}}
    `,
    //编辑的时候用到的sku模板
    editSkuTpl: `
         <tr class="SpecificationTh">
                           {{each AttributeList as value i}}
                         <td align="center" class="fieldCell" style="width:50px !important" skuid="{{AttributeList[i].AttrId}}"><span>{{AttributeList[i].Name}}</span></td>
                            {{/each}}
                         <td align="center">特价</td>
                         <td align="center">零售价</td>
                         <!--<td align="center">会员价</td>-->
                         <td align="center">时代编码</td>
                         <td align="center" id="storeField"><em>*</em>库存</td>
                         <td align="center">图片(200*200)</td>
                         <td align="center">操作</td>
        </tr>
        {{each SKu as value i}}
        <tr id="sku_1" rowindex="{{i}}" class="SpecificationTr">
                        {{each SKu[i].AttrValueList as value j}}
                           <td align="center">
                              <div id="skuDisplay_1_20" rowid="{{j}}" skuid="{{SKu[i].AttrValueList[j].AttrId}}" valueid="{{SKu[i].AttrValueList[j].AttrValueId}}" data-valuelist="{{SKu[i].AttrValueStringList}}" data-idslist="{{SKu[i].AttrValueIdList}}" class="specdiv">{{SKu[i].AttrValueList[j].AttrValue}}</div>
                           </td>
                       {{/each}}
                     
                       <td align="center">
                         <input type="text" class="skuItem_CostPrice form-control CostPrice" value="{{SKu[i].CostPrice}}" style="width:100px;">
                       </td>
                       <td align="center">
                         <input type="text" class="skuItem_CostPrice form-control retailPrice" value="{{SKu[i].ShopPrice}}" style="width:100px;">
                       </td>
                       <!--<td align="center">-->
                         <!--<input type="text" class="skuItem_CostPrice form-control memberPrice" value="{{SKu[i].MemberPrice}}" style="width:100px;">-->
                       <!--</td>-->
                        <td align="center">
                         <input type="text" class="skuItem_CostPrice form-control merchantCode" value="{{SKu[i].SkuSn}}" style="width:100px;">
                       </td>
                       <td align="center">
                         <input type="text" class="skuItem_CostPrice form-control inventory" value="{{SKu[i].Number}}" style="width:100px;">
                       </td>
                       <td align="center">
                            <div class="img-containerSku">
                               <img class="select-img" src="{{SKu[i].FullShowImg}}" data-src="{{SKu[i].ShowImg}}">
                            </div>
                       </td>
                       <td align="center">
                         <a style="float:left;width:100%;text-algin:center" href="javascript:;" id="deleSku_1">
                           <span style="float:none;color:red" class="glyphicon glyphicon-trash DelectBtn"></span>
                         </a>
                       </td>
         </tr>
         {{/each}}
    `,
    brandTpl: `
        {{each BrandList as value i}}
            <option value="{{BrandList[i].BrandId}}">{{BrandList[i].Name}}</option>
        {{/each}}
    `,
    AttributeTpl: `
         {{each AttributeGroupList as value i}}
            <option value="{{AttributeGroupList[i].AttrGroupId}}">{{AttributeGroupList[i].Name}}</option>
        {{/each}}   
    `,
    imgTpl: `
        {{each imglistData as value i}}
            <div class="img-container">
                <img class="select-img" src="{{imglistData[i].ImgFull}}" data-src="{{imglistData[i].Img}}">
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
    imgTplEdit: `
        {{each imglistData as value i}}
            <div class="imgOut" style="position: relative">
                 <div class="img-container ">
                        <img class="select-img" src="{{imglistData[i].ImgFull}}" data-src="{{imglistData[i].Img}}">
            
                        <img class="close-img" src="/public/images/close.png" style="width: 100%;height: 100%;">
                       
                  </div>
             </div>
        {{/each}}
    `,
    freightTpl: `
        {{each TemplatesList as value i}}
            <option value="{{TemplatesList[i].TemplateId}}">{{TemplatesList[i].TemplateName}}</option>
        {{/each}}
    `,
    //模板
    GoodsListTemplate: `<ul class="layer_ul_1">
                        {{each CategoryList as value i}}
                            <li class="layer-1">
                                <div class="contentBody">
                                    <div class="col-xs-1">{{CategoryList[i].CateId}}</div>
                                    <div class="col-xs-7">
                                        <div class="classify_one">
                                            <i class="icon-jia jia_collapse_one">
                                                <img src="../public/images/zhankai.png" alt="">
                                            </i>
                                            <i class="iconfont icon-wenjianjia"></i>
                                                {{CategoryList[i].Name}}
                                        </div>
                                    </div>
                                    <div class="col-xs-4">
                                       <span class="pick" data-id={{CategoryList[i].CateId}} data-name="{{CategoryList[i].Name}}">选择</span>
                                    </div>
                                </div>
                            </li>
                        {{/each}}
                        </ul>`,
    companyTpl: `
        {{each OrderStoresList as value i}}
            <option  data-id="{{OrderStoresList[i].SId}}" value="{{OrderStoresList[i].SId}}">{{OrderStoresList[i].Name}}</option>
        {{/each}}
    `,
    labelTpl: `
        {{each ProductLabelList as value i}}
            <label class="checkbox-inline">
                <input type="checkbox" data-id="{{ProductLabelList[i].PLId}}" value="{{ProductLabelList[i].PLId}}"> {{ProductLabelList[i].Name}}
            </label>
        {{/each}}
    `,
    //图片库新增
    cateBoxTemplate: `<li class="newT" data-toggle="modal" data-target="#addGrouping">
        <span class="glyphicon glyphicon-plus"></span>新建分组</li>
        <li class="ImgT a_b" data-id="0">未分组</li>
        {{each List as v i}}
        <li class="ImgT" data-id="{{List[i].CategoryId}}">{{List[i].Name}}</li>
        {{/each}}
    `,
    picBoxTemplate: `{{each List as value i}}
        <div class="pin col-md-2" data-id="{{List[i].MaterialId}}" data-src="{{List[i].FileUrlFull}}" data-usrc="{{List[i].FileUrl}}">
            <div class="file-item thumbnail box">
                <img style="height:120px"src="{{List[i].FileUrlFull}}" alt="">
            </div>
            <div></div>
           <div class="picIndex" style="display: none;"></div>
        </div>
        {{/each}}
    `,
    moveBoxTemplate: `<li class="file_li" data-id="0">
                <i class="glyphicon glyphicon-folder-open" style="color:#bde0e9;margin-right: 10px"></i>未分组
     </li>
     {{each List as value i}}
         <li class="file_li" data-id="{{List[i].CategoryId}}">
                    <i class="glyphicon glyphicon-folder-open" style="color:#bde0e9;margin-right: 10px"></i>{{List[i].Name}}
         </li>
     {{/each}}
    `,
    //1代表单个图片，2代表多图上传
    Type: "",
    PicPageIndex: 1,
    fileId: "",
    skuImgItem: "",
    AvalueTabTpl: `
    <span class="label label-default tag" style="margin-right: 5px;margin-bottom: 5px;display: inline-block">
        <span class="text-info">#
            <span class="tabItem" data-id="{{Id}}">{{Name}}</span>#</span>
        <a href="javascript:void(0);" class="delTap">×</a>
    </span>
    `,
    init: function() {
        $(document).keydown(function(event) {
            if (event.keyCode == 13) {
                $('form').each(function() {
                    event.preventDefault();
                });
            }
        });
        $('#moduleType').chosen();
        $('#salesType').chosen();
        $('#ProductCommodity').chosen();
        $('#storeList').chosen();
        // 返回上一页
        $('body').on('click', '.backBtn', function() {
                var type = Common.getUrlParam('type');
                window.location.href = "/product/productList?type=" + type + ""
            })
            // //如果是编辑，是否推送隐藏
        if (Common.getUrlParam('PId')) {
            $('#pushBox').hide();
        }

        //选择商品分类模态框显示
        $('#removeFileModal').on('show.bs.modal', function(e) {
            ProductRelease.adminGetCategoryList();
        });
        //初始化富文本编辑器
        var ue = UE.getEditor('hcEditor');
        var ue2 = UE.getEditor('container');
        ue2.ready(function() {
            ue2.setHeight(500);
            ue.ready(function() {
                ue.setHeight(500);
                ProductRelease.adminBrandList();

            });
        });
        //上传小图标
        uploadIconPic('#small_upload_pick', '#small_icon', '/product/AdminUploadProductImg');

        //input失去焦点颜色变回原来
        $('input[type="text"],input[type="number"]').on('blur', function() {
            $(this).css('border', '1px solid #ccc')
        })
        $('input[type="text"],input[type="number"]').on('focus', function() {
            $(this).css('border', '1px solid #3c8dbc')
        })

        // 下一步
        $("#nextStep,#secondBtn").on("click", function() {
            //分类
            if ($("#classifyName").attr("data-id") == "0") {
                Common.showInfoMsg('请选择分类')
                return false;
            }
            if ($('#ProductBrand').val() == 0) {
                Common.showInfoMsg('请选择品牌')
                return false;
            }
            if ($('#TemplateId').val() == 0) {
                Common.showInfoMsg('请选择运费模板')
                return false;
            }

            //商品名称
            if (!Validate.emptyValidateAndFocusAndColor("#Name", "请输入商品名称", "")) {
                return false;
            }
            if (!ProductRelease.getWhethertoopen()) {
                return false;
            }
            if (!ProductRelease.isOpen) {
                //零售价
                if (!Validate.emptyValidateAndFocusAndColor("#ShopPrice", "请输入零售价", "")) {
                    return false;
                }
                // //成本价
                // if (!Validate.emptyValidateAndFocusAndColor("#CostPrice", "请输入特价", "")) {
                //     return false;
                // }
                //库存
                if (!Validate.emptyValidateAndFocusAndColor("#inventory", "请输入库存", "")) {
                    return false;
                }
                //重量
                if (!Validate.emptyValidateAndFocusAndColor("#Weight", "请输入重量", "")) {
                    return false;
                }
            }
            $("#first").removeClass("active");
            $("#second").addClass("active");
            $("#typeNav li").eq(0).removeClass("active");
            $("#typeNav li").eq(1).addClass("active");
        });

        // 上一步
        $("#lastStep").on("click", function() {
            $("#second").removeClass("active");
            $("#first").addClass("active");
            $("#typeNav li").eq(1).removeClass("active");
            $("#typeNav li").eq(0).addClass("active");
        });

        //完成
        $("#finish").on("click", function() {
            if ($('#small_icon').attr('data-src') == "" || $('#small_icon').attr('data-src') == null) {
                Common.showInfoMsg('请上传封面图')
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
            } else {
                Common.showInfoMsg('请上传封面图')
            }
        });


        //保存添加
        $("#save").on("click", function() {
            if ($("#addInput").val() != "") {
                ProductRelease.addProductLabel();
            } else {
                Common.showInfoMsg("请先输入标签名称")
            }
        });

        // 选择商品分类
        $("#selectClassify").on("click", function() {
            $(".mask").show();
        });

        // 关闭商品分类弹窗
        $(".mask").on("click", ".close", function() {
            $(".mask").hide();
        });

        // 选择分类
        $(".mask").on("click", ".pick", function() {
            var id = $(this).attr("data-id");
            var name = $(this).attr("data-name");
            $("#classifyName").attr("data-id", id);
            $("#classifyName").text(name);
            $(".mask").hide();
        });

        $("#Whethertoopen").on("change", function() {
            if ($("input[name='Whethertoopen']:checked").val() == 1) {
                $(".Specialprice").show();
            } else {
                $(".Specialprice").hide();
            }
        })

    },
    //获取后台品牌列表
    adminBrandList: function() {
        var methodName = "/brand/AdminBrandList";
        var data = {
            "Name": "",
            "Page": {
                "PageSize": 1000,
                "PageIndex": 1
            }
        };
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                console.log(data)
                var render = template.compile(ProductRelease.brandTpl);
                var html = render(data.Data);
                $("#ProductBrand").append(html);
                ProductRelease.adminCategoryList();

                //初始化搜索下拉框
                $('#ProductBrand').chosen();
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

                var render = template.compile(ProductRelease.freightTpl);
                var html = render(data.Data);
                $("#TemplateId").append(html);
                var PId = Common.getUrlParam("PId");
                if (PId != undefined && PId != "") {
                    ProductRelease.getProductInfo(PId);
                }

                //初始化搜索下拉框
                $("#TemplateId").chosen();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 新增
    addProduct: function() {

        var methodName = "/product/AdminAddProduct";

        var ue = UE.getEditor('hcEditor');
        var ue2 = UE.getEditor('container');
        var Description = ue.getContent();
        var AfterSaleProtection = ue2.getContent();

        var State = $("input[name='state']:checked").val();
        var IsAppNotify = $("input[name='push']:checked").val()

        var Img = [];
        var imgList = $(".productImg .img-container");
        for (var i = 0; i < imgList.length; i++) {
            var imgSrc = imgList.eq(i).find(".select-img").attr("src");
            if (imgSrc != "/public/images/addImg.png") {
                var dataSrc = imgList.eq(i).find(".select-img").attr("data-src");
                Img.push(dataSrc);
            }
        }
        var data = {
            PSn: "",
            CateId: $("#classifyName").attr("data-id"),
            Number: $('#inventory').val(),
            IsCostPrice: $("input[name='Whethertoopen']:checked").val() == 1 ? 1 : 0,
            CostPrice: $("#CostPrice").val(),
            ShopPrice: $('#ShopPrice').val(),
            BrandId: $('#ProductBrand').val(),
            Name: $("#Name").val(),
            Summary: AfterSaleProtection,
            State: State,
            TemplateId: $("#TemplateId").val(),
            Img: Img,
            "ShowImg": $('#small_icon').attr('data-src'),
            Description: Description,
            Weight: $('#Weight').val(),
            AfterSaleProtection: $('#AfterSaleProtection').val(),
        };
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("新增成功", function() {
                    location.href = '/product/productList'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 编辑
    editProduct: function() {
        var methodName = "/product/AdminEditProduct";

        var ue = UE.getEditor('hcEditor');
        var ue2 = UE.getEditor('container');
        var Description = ue.getContent();
        var AfterSaleProtection = ue2.getContent();

        var State = $("input[name='state']:checked").val();

        var Img = [];
        var imgList = $(".productImg .img-container");
        for (var i = 0; i < imgList.length; i++) {
            var imgSrc = imgList.eq(i).find(".select-img").attr("src");
            if (imgSrc != "/public/images/addImg.png") {
                var dataSrc = imgList.eq(i).find(".select-img").attr("data-src");
                Img.push(dataSrc);
            }
        }


        var data = {
            PId: Common.getUrlParam("PId"),
            PSn: "",
            CateId: $("#classifyName").attr("data-id"),

            Number: $('#inventory').val(),
            ShopPrice: $('#ShopPrice').val(),
            BrandId: $('#ProductBrand').val(),
            Name: $("#Name").val(),
            Summary: AfterSaleProtection,
            IsCostPrice: $("input[name='Whethertoopen']:checked").val() == 1 ? 1 : 0,
            CostPrice: $("input[name='Whethertoopen']:checked").val() == 1 ? $('#CostPrice').val() : 0.01,
            State: State,
            TemplateId: $("#TemplateId").val(),
            Img: Img,
            "ShowImg": $('#small_icon').attr('data-src'),
            Description: Description,
            Weight: $('#Weight').val(),
            AfterSaleProtection: $('#AfterSaleProtection').val(),
        };
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("编辑成功", function() {
                    location.href = '/product/productList'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取商品信息
    getProductInfo: function(PId) {
        var methodName = "/product/AdminProductInfo";
        var data = {
            PId: PId
        };
        SignRequest.setAsync(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                var result = data.Data.ProductInfo;
                $("#classifyName").attr("data-id", result.CateId);
                $("#classifyName").text(result.CateName);
                $("#BrandId").val(result.BrandId);
                $("#Name").val(result.Name);
                $("#TemplateId").val(result.TemplateId);
                $("#ProductBrand").val(result.BrandId);
                $('#small_icon').attr('data-src', result.ShowImg);
                $('#small_icon').attr('src', result.ShowImgFull);
                $('#Weight').val(result.Weight)
                    // 状态
                var stateList = $("#State label");
                for (var i = 0; i < stateList.length; i++) {
                    if (stateList.eq(i).find("input").val() == result.State) {
                        stateList.eq(i).find("input").prop("checked", true);
                    }
                }
                // 是否开启特价
                if (result.IsCostPrice) {
                    $("#Whethertoopen label").eq(0).find("input").prop("checked", true);
                    $(".Specialprice").show();
                } else {
                    $("#Whethertoopen label").eq(1).find("input").prop("checked", true);
                    $(".Specialprice").hide();

                }

                var imgArr = [];
                for (var i = 0; i < result.Img.length; i++) {
                    var imgData = {
                        Img: result.Img[i],
                        ImgFull: result.ImgFull[i]
                    };
                    imgArr.push(imgData);
                }
                var imgList = {
                    imglistData: imgArr
                };
                var render = template.compile(ProductRelease.imgTpl);
                var html = render(imgList);
                $("#productImg").html(html);

                var ue = UE.getEditor('hcEditor');
                ue.setContent(result.Description);
                var ue2 = UE.getEditor('container');
                ue2.setContent(result.Summary);
                $('#ProductBrand').trigger('chosen:updated'); //更新选项
                $('#TemplateId').trigger('chosen:updated'); //更新选项
                $('#ShopPrice').val(result.ShopPrice)
                $("#CostPrice").val(result.CostPrice);
                $('#inventory').val(result.Number)

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

    //后台分类列表
    adminCategoryList: function() {
        //请求方法
        var methodName = "/category/AdminCategoryList";
        var data = {};
        console.log(data)
            //请求接口
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(ProductRelease.GoodsListTemplate);
                var html = render(data.Data);
                $('#table_content_classify').html(html);
                ProductRelease.initHandle();
                ProductRelease.adminTemplatesList()
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //初始化事件
    initHandle: function() {
        //文件收缩  第一层级
        $('.layer_ul_1 .layer-1 .jia_collapse_one').click(function() {
            //$(this).parents('.contentBody').siblings('.layer_ul_2').slideToggle();
            var display_state = $(this).parents('.contentBody').siblings('.layer_ul_2').css('display'); //所以要用行内样式控制

            if (display_state == 'none') {
                $(this).parents('.contentBody').siblings('.layer_ul_2').slideDown();
                $(this).find('img').attr('src', '../public/images/zhankai.png');
            } else {
                $(this).parents('.contentBody').siblings('.layer_ul_2').slideUp();
                $(this).find('img').attr('src', '../public/images/suoqi.png');
            }
        });

        //文件收缩 第二层级
        $('.layer_ul_2 .layer-2 .jia_collapse_two').click(function() {

            var display_state = $(this).parents('.contentBody').siblings('.layer_ul_3').css('display'); //所以要用行内样式控制
            if (display_state == 'none') {
                $(this).parents('.contentBody').siblings('.layer_ul_3').slideDown();
                $(this).find('img').attr('src', '../public/images/zhankai.png');
            } else {
                $(this).parents('.contentBody').siblings('.layer_ul_3').slideUp();
                $(this).find('img').attr('src', '../public/images/suoqi.png');
            }
        });
    },
    //    图片库增加
    //查询文件夹列表
    adminGetCategoryList: function() {
        var methodName = "/Material/AdminGetCategoryList";
        var data = {
            "Page": {
                "PageSize": 100,
                "PageIndex": 1
            }
        };
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                var render1 = template.compile(ProductRelease.cateBoxTemplate);
                var html1 = render1(data.Data);
                $('.cebian_ul').html(html1);
                var render2 = template.compile(ProductRelease.moveBoxTemplate);
                var html2 = render2(data.Data);
                $('.cebian_ul2').html(html2);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //创建文件夹接口
    adminAddCategory: function(name) {
        var methodName = "/Material/AdminAddCategory";
        var data = {
            "Name": name
        };
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功', function() {
                    $("#addGrouping").modal("hide");
                    ProductRelease.adminGetCategoryList();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //确认上传素材
    adminSaveMaterial: function(list) {
        var methodName = "/Material/AdminSaveMaterial";
        var data = {
            "TempImageList": list,
            "CategoryId": $(".a_b").attr("data-id")
        };
        console.log(data)
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("上传成功!", function() {
                    // $('#uploaderPicModal').modal('hide')
                    // location.reload()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取素材库列表
    adminGetMaterialList: function() {
        var methodName = "/Material/AdminGetMaterialList";
        var data = {
            "FileName": $("#ipt_search_message").val(),
            "CategoryId": $(".a_b").attr("data-id"),
            "DisplayOrder": $("#timeOrder_box option:selected").attr("data-value"),
            "IsAsc": $("#timeOrder_box option:selected").attr("data-type"),
            "Page": {
                "PageSize": 18,
                "PageIndex": ProductRelease.PicPageIndex
            }
        };
        SignRequest.set(methodName, data, function(data) {
            console.log(data)
            if (data.Code == "100") {
                ProductRelease.PicTotal = data.Data.Total;
                var render = template.compile(ProductRelease.picBoxTemplate);
                var html = render(data.Data);
                $('#ImgBox').html(html);
                uploadPic_toLibraryP('#FilePicker');
                ProductRelease.moreIndex = 1;

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //批量删除素材接口
    adminBatchDeleteMaterial: function(list) {
        var methodName = "/Material/AdminBatchDeleteMaterial";
        var data = {
            "Ids": list
        };
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("删除成功!", function() {
                    ProductRelease.adminGetMaterialList();
                    setTimeout(() => {
                        if (ProductRelease.PicTotal > 0) {
                            //初始化分页
                            $.jqPaginator('#pagination2', {
                                totalCounts: ProductRelease.PicTotal,
                                pageSize: 18,
                                visiblePages: 10,
                                currentPage: 1,
                                prev: '<li class="prev"><a href="javascript:;">&lt;</a></li>',
                                next: '<li class="next"><a href="javascript:;">&gt;</a></li>',
                                page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
                                onPageChange: function(num, type) {
                                    ProductRelease.PicPageIndex = num;
                                    ProductRelease.adminGetMaterialList();
                                    $('#p2').text(type + '：' + num);
                                }
                            });
                        }
                    }, 500);
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //移动素材
    adminMoveMaterial: function(cateList, cateid) {
        var methodName = "/Material/AdminMoveMaterial";
        var data = {
            "Ids": cateList,
            "CategoryId": cateid,
        };
        console.log(data)
        SignRequest.set(methodName, data, function(data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("移动成功!", function() {
                    ProductRelease.adminGetMaterialList();
                    setTimeout(() => {
                        if (ProductRelease.PicTotal > 0) {
                            //初始化分页
                            $.jqPaginator('#pagination2', {
                                totalCounts: ProductRelease.PicTotal,
                                pageSize: 18,
                                visiblePages: 10,
                                currentPage: 1,
                                prev: '<li class="prev"><a href="javascript:;">&lt;</a></li>',
                                next: '<li class="next"><a href="javascript:;">&gt;</a></li>',
                                page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
                                onPageChange: function(num, type) {
                                    ProductRelease.PicPageIndex = num;
                                    ProductRelease.adminGetMaterialList();
                                    $('#p2').text(type + '：' + num);
                                }
                            });
                        }
                    }, 500);
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

};