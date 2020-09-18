$(function () {
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
                         <td align="center">成本价</td>
                         <td align="center">零售价</td>
                         <!--<td align="center">会员价</td>-->
                         <td align="center">时代编码</td>
                         <!--<td align="center">商品编号</td>-->
                         <!--<td align="center" id="storeField"><em>*</em>库存</td>-->
                         <td align="center">图片</td>
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
                         <input type="text" class="skuItem_CostPrice form-control costPrice" value="{{SKu[i].CostPrice}}" style="width:100px;">
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
                        <!--<td align="center">-->
                         <!--<input type="text" class="skuItem_CostPrice form-control productId" value="{{SKu[i].PSn}}" style="width:100px;">-->
                       <!--</td>-->
                       <!--<td align="center">-->
                         <!--<input type="text" class="skuItem_CostPrice form-control inventory" value="{{SKu[i].Number}}" style="width:100px;">-->
                       <!--</td>-->
                       <td align="center">
                            <div class="img-containerSku">
                               <img class="select-img" src="{{SKu[i].FullShowImg}}" data-src="{{SKu[i].ShowImg}}">
                               <!--<input class="img-input" type="file"accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">-->
                               <!--<img class="close-img" src="/public/images/close.png">-->
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
    labelTpl: `
        {{each ProductLabelList as value i}}
            <label class="checkbox-inline">
                <input type="checkbox" value="{{ProductLabelList[i].PLId}}"> {{ProductLabelList[i].Name}}
            </label>
        {{/each}}
    `,
    imgTpl: `
         <div id="drapBox" style="float: left">
             {{each imglistData as value i}}
             <div class="imgOut" style="position: relative">
                 <div class="img-container ">
                        <img class="select-img" src="{{imglistData[i].ImgFull}}" data-src="{{imglistData[i].Img}}">
            
                        <img class="close-img" src="/public/images/close.png" style="width: 100%;height: 100%;">
                       
                  </div>
                  <div class="sortBox">
                      <div class="leftBtn"> <img src="/public/images/left.png" alt="" style="width: 20px;height: 20px"></div>
                      <div class="rightBtn"> <img src="/public/images/right.png" alt="" style="width: 20px;height: 20px"> </div>
                  </div>
             </div>
                
            {{/each}}
        </div>
        <div class="img-container" id="morePicUploadBtn">
            <img class="select-img" src="/public/images/addImg.png">
            
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
                  <div class="sortBox">
                     <div class="leftBtn"> <img src="/public/images/left.png" alt="" style="width: 20px;height: 20px"></div>
                      <div class="rightBtn"> <img src="/public/images/right.png" alt="" style="width: 20px;height: 20px"> </div>
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
                            {{if CategoryList[i].Layer == "1"}}
                            <li class="layer-1">
                                <div class="contentBody">
                                    <div class="col-xs-1">{{CategoryList[i].CateId}}</div>
                                    <div class="col-xs-7">
                                        <div class="classify_one">
                                            <i class="icon-jia jia_collapse_one">
                                            {{if CategoryList[i].Haschild == "1"}}
                                            <img src="../public/images/suoqi.png" alt="">
                                            {{else if CategoryList[i].Haschild == "0"}}
                                            <img src="../public/images/zhankai.png" alt="">
                                            {{/if}}
                                            </i>
                                            <i class="iconfont icon-wenjianjia"></i>
                                            {{CategoryList[i].Name}}
                                        </div>
                                    </div>
                                    <div class="col-xs-4">
                                  
                                    </div>
                                </div>
                                {{each CategoryList as value j}}
                                {{if CategoryList[j].ParentId == CategoryList[i].CateId}}
                                <ul class="layer_ul_2" style="display: none">
                                    <li class="layer-2">
                                        <div class="contentBody">
                                            <div class="col-xs-1">{{CategoryList[j].CateId}}</div>
                                            <div class="col-xs-7">
                                                <div class="classify_two">
                                                    <i class="icon-jia jia_collapse_two">
                                                    {{if CategoryList[j].Haschild == "1"}}
                                                    <img src="../public/images/suoqi.png" alt="">
                                                    {{else if CategoryList[j].Haschild == "0" }}
                                                    <img src="../public/images/zhankai.png" alt="">
                                                    {{/if}}
                                                    </i>
                                                    <i class="iconfont icon-wenjianjia"></i>
                                                    {{CategoryList[j].Name}}
                                                </div>
                                            </div>
                                            <div class="col-xs-4">
                                        
                                            </div>
                                        </div>
                                        {{each CategoryList as value k}}
                                        {{if CategoryList[k].ParentId ==CategoryList[j].CateId}}
                                        <ul class="layer_ul_3" style="display: none">
                                            <li class="layer-3">
                                                <div class="contentBody">
                                                    <div class="col-xs-1">{{CategoryList[k].CateId}}</div>
                                                    <div class="col-xs-7">
                                                        <div class="classify_three">
                                                            <i class="iconfont icon-wenjianjia"></i>
                                                            {{CategoryList[k].Name}}
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-4">
                                                    {{if CategoryList[k].Haschild == "0"}}
                                                        <span class="pick" data-id={{CategoryList[k].CateId}} data-name="{{CategoryList[k].Name}}">选择</span>
                                                    {{/if}}
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                        {{/if}}
                                        {{/each}}

                                    </li>
                                </ul>
                                {{/if}}
                                {{/each}}
                            </li>
                            {{/if}}
                           {{/each}}
                        </ul>`,
    companyTpl: `
        {{each StoresList as value i}}
            <option  data-id="{{StoresList[i].SId}}" value="{{StoresList[i].SId}}">{{StoresList[i].Name}}</option>
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
    //商品分类模板
    cateTpl: `
    {{each FirstCategoryList as value i}}
        <option value="{{FirstCategoryList[i].CateId}}">{{FirstCategoryList[i].Name}}</option>
    {{/each}}
    `,
    //1代表单个图片，2代表多图上传
    Type: "",
    PicPageIndex: 1,
    fileId: "",
    skuImgItem: "",
    //多选礼包的商品id
    pIdList: [],

    init: function () {


        //选择商品按钮点击
        $('body').on('click', '#choiceProduct', function () {
            $('#choiceProductModal').modal('show');

        });

        //当选择商品模态框出现获取数据
        $('#choiceProductModal').on('shown.bs.modal', function () {
            ProductRelease.getProductCategory(); //初始化商品分类
            ProductRelease.initBootstrapTable(); //初始化商品列表
        });

        //查询
        $("#productSearch").on("click", function () {
            var data = {
                "Name": $('#choiceProductName').val(),
                "CateId": $('#choiceCateId').val(),
            }
            ProductRelease.projectDestoryQuery(data);
        });

        // 分页条数设置
        $("#pageSizeDropDown").on("change", function () {
            var data = {
                "Name": $('#choiceProductName').val(),
                "CateId": $('#choiceCateId').val(),
            }
            ProductRelease.projectDestoryQuery(data);
        });

        //点击确认
        $('#choiceProductModal').on('click', '#choiceProductConfirmBtn', function () {
            var list = [];
            var name = ""
            $('.checkbox').each(function (index, item) {
                if (this.checked) {
                    list.push($(item).attr('data-id'))
                    name += $(this).parents('tr').find('.goods_name_modal').text() + '</br>'
                }
            })
            if(list <= 0){
                Common.showErrorMsg('请选择商品')
                return false
            }
            name = name.substring(0, name.length - 1);
            $('.goods-name_final').html(name)
            ProductRelease.pIdList = list;
            $('#hasChooseBox').show()
            $('#choiceProductModal').modal('hide');

        })

        // 返回上一页
        $('body').on('click', '.backBtn', function () {
            var type = Common.getUrlParam('type');
            window.location.href = "/product/applicationForGiftSetting"
        })
        // //如果是编辑，是否推送隐藏
        if (Common.getUrlParam('PId')) {
            $('#pushBox').hide();
        }

        $('#ProductCommodity').chosen();

        //主图排序
        $('body').on('click', '.leftBtn', function () {
            //获取当前下标
            var index = $(this).parents('.imgOut').index()
            console.log(index)
            //获取当前的数量
            var length = $('.imgOut').length;
            var that = $(this).parents('.imgOut');
            //如果是最左边
            if (index == 0) {
                console.log("最左边,放到最底")
                $('.imgOut').each(function (index1, item) {
                    //找到它左边的那一个元素
                    if (index1 == length - 1) {
                        $(item).after(that)
                    }
                })
            } else {
                $('.imgOut').each(function (index1, item) {
                    //找到它左边的那一个元素
                    if (index1 == index - 1) {
                        $(item).before(that)
                    }
                })
            }
        })
        //向右
        $('body').on('click', '.rightBtn', function () {
            //获取当前下标
            var index = $(this).parents('.imgOut').index()
            console.log(index)
            //获取当前的数量
            var length = $('.imgOut').length;
            var that = $(this).parents('.imgOut');
            //如果是最右边
            if (index == length - 1) {
                console.log("最右边,放到最左")
                $('.imgOut').each(function (index1, item) {
                    //找到它左边的那一个元素
                    if (index1 == 0) {
                        $(item).before(that)
                    }
                })
            } else {
                $('.imgOut').each(function (index1, item) {
                    //找到它左边的那一个元素
                    if (index1 == index + 1) {
                        $(item).after(that)
                    }
                })
            }
        })

        //搜索按钮点击
        $('body').on('click', '#search_icon_mess_btn', function () {
            ProductRelease.PicPageIndex = 1;

            ProductRelease.adminGetMaterialList();
        })
        $('body').on('change', '#ProductCommodity', function () {
            if ($(this).val() == '0') {
                $("#storeOutBox").hide()
            } else {
                $("#storeOutBox").show()
            }
        })

        //删除图片
        $(".productImg").on("click", ".close-img", function () {
            $(this).parents('.imgOut').remove();
        });

        //选择商品分类模态框显示
        $('#removeFileModal').on('show.bs.modal', function (e) {
            ProductRelease.adminGetCategoryList();
        });
        //选择图片模态框显示
        $('#insertcustom_img').on('show.bs.modal', function (e) {
            console.log("图片库出现")
            ProductRelease.adminGetCategoryList();
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
                        onPageChange: function (num, type) {
                            ProductRelease.PicPageIndex = num;
                            ProductRelease.adminGetMaterialList();
                            $('#p2').text(type + '：' + num);
                        }
                    });
                }
            }, 500);

        });
        //sku规格图片点击
        $('body').on('click', '#skuBody .select-img', function () {
            ProductRelease.Type = 3;
            //下标设置为0
            ProductRelease.moreIndex = 1;
            //将点击的项存起来
            ProductRelease.skuImgItem = $(this);
            $('#insertcustom_img').modal('show');
        })

        $('body').on('click', '.small_con', function () {
            ProductRelease.Type = 1;
            //下标设置为0
            ProductRelease.moreIndex = 1;
            $('#insertcustom_img').modal('show');
        })
        $('body').on('click', '#productImg .select-img', function () {
            ProductRelease.Type = 2;
            //下标设置为0
            ProductRelease.moreIndex = 1;
            console.log("触发了")
            $('#insertcustom_img').modal('show');
        })
        //移动确认按钮点击
        $('body').on('click', '#remove_file_confirm', function () {
            var list = [];
            $('#ImgBox .pin').each(function (index, item) {
                if ($(item).hasClass("rm")) {
                    list.push(Number($(item).attr('data-id')));
                }
            });
            ProductRelease.adminMoveMaterial(list, ProductRelease.fileId);
            $('#removeFileModal').modal("hide");
        });

        //选择分类移动模态框点击选择
        $(".cebian_ul2").on("click", " .file_li", function () {
            $(this).addClass('file_li_active').siblings('.file_li').removeClass('file_li_active');
            ProductRelease.fileId = $(this).attr("data-id");
        });

        //点击图片侧边栏的li时候
        $("#picGroup").on("click", '.cebian_ul .ImgT', function () {
            var index = $(this).index();
            var _thisId = $(this).attr("data-id");
            ProductRelease.moreIndex = 1;
            $(this).addClass('a_b').siblings('.ImgT').removeClass('a_b');
            ProductRelease.adminGetMaterialList();
            setTimeout(() => {
                //初始化分页
                if (ProductRelease.PicTotal > 0) {
                    $.jqPaginator('#pagination2', {
                        totalCounts: ProductRelease.PicTotal,
                        pageSize: 18,
                        visiblePages: 10,
                        currentPage: 1,
                        prev: '<li class="prev"><a href="javascript:;">&lt;</a></li>',
                        next: '<li class="next"><a href="javascript:;">&gt;</a></li>',
                        page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
                        onPageChange: function (num, type) {
                            ProductRelease.PicPageIndex = num;
                            ProductRelease.adminGetMaterialList();
                            $('#p2').text(type + '：' + num);
                        }
                    });
                }
            }, 500);
        });

        //点击自定义弹窗中的图片，选中与否样式
        $('.wrapper_imgs_box').on('click', '.pin', function () {
            //如果是单图片上传
            if (ProductRelease.Type == 1) {
                $(this).find('.box').siblings('div').addClass('choice_true_dv');
                $(this).addClass('rm').siblings('.pin').removeClass('rm').find('.box').siblings('div').removeClass('choice_true_dv');
            } else if (ProductRelease.Type == 3) {
                //sku图片选择
                $(this).find('.box').siblings('div').addClass('choice_true_dv');
                $(this).addClass('rm').siblings('.pin').removeClass('rm').find('.box').siblings('div').removeClass('choice_true_dv');
            } else {
                //多选
                //如果是取消选中
                if ($(this).hasClass('rm')) {
                    //获取当前点击的下标
                    var Cindex = $(this).attr('data-index');
                    //重新计算排序
                    $('.pin').each(function (index, item) {
                        if ($(this).hasClass('rm')) {
                            //如果比当前下标大的就减1
                            if ($(item).attr('data-index') > Cindex) {
                                var currentIndex = $(item).attr('data-index');
                                var changeIndex = currentIndex - 1;
                                $(item).attr('data-index', changeIndex)
                            }
                        }
                    })
                    $(this).removeClass('rm');
                    //获取当前选中的数量
                    var Slength = $('#ImgBox').find('.rm').length;
                    if (Slength) {
                        //重设下标
                        ProductRelease.moreIndex = Slength + 1;
                    } else {
                        ProductRelease.moreIndex = 1;
                    }


                } else {
                    //添加下标
                    $(this).attr('data-index', ProductRelease.moreIndex)
                    $(this).addClass('rm')
                    ProductRelease.moreIndex += 1;

                }

                $(this).find('.box').siblings('div').toggleClass('choice_true_dv');
                //显示下标
                $('.pin').each(function (index, item) {
                    if ($(this).hasClass('rm')) {
                        $(this).find('.picIndex').text($(this).attr('data-index'));
                        $(this).find('.picIndex').show();
                    } else {
                        $(this).find('.picIndex').hide();
                    }
                })


            }

        });

        //点击创建分组
        $("#creatTabNameBtn").click(function () {
            if ($("#TabName").val() == "") {
                Common.showErrorMsg("分组名字不能为空");
                return;
            }
            ProductRelease.adminAddCategory($("#TabName").val());
        });

        //排序方式改变
        $('body').on('change', '#timeOrder_box', function () {
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
                        onPageChange: function (num, type) {
                            ProductRelease.PicPageIndex = num;
                            ProductRelease.adminGetMaterialList();
                            $('#p2').text(type + '：' + num);
                        }
                    });
                }
            }, 500);
        });

        //批量删除按钮点击
        $('body').on('click', '.delectAll', function () {
            var list = [];
            $('#ImgBox .pin').each(function (index, item) {
                if ($(item).hasClass("rm")) {
                    list.push(Number($(item).attr('data-id')));
                }
            })
            Common.confirmDialog('确认要删除吗?', function () {
                ProductRelease.adminBatchDeleteMaterial(list)
            })
        });

        //图片库确认按钮点击
        $('body').on('click', '#insert_img_btn', function () {
            //判断是单图上传还是多图上传
            if (ProductRelease.Type == 1) {
                //单图
                //判断是否有选中图片
                var hasSelect = false;
                $('.pin').each(function (index, item) {
                    if ($(this).hasClass('rm')) {
                        hasSelect = true;
                        $('#small_icon').attr('src', $(this).attr('data-src'))
                        $('#small_icon').attr('data-src', $(this).attr('data-usrc'))
                    }
                })
                //没选中
                if (!hasSelect) {
                    Common.showInfoMsg('请选择图片')
                    return false;
                } else {
                    $('#insertcustom_img').modal('hide');
                }

            } else if (ProductRelease.Type == 3) {
                //sku图片上传
                //判断是否有选中图片
                var hasSelect = false;
                $('.pin').each(function (index, item) {
                    if ($(this).hasClass('rm')) {
                        hasSelect = true;
                        ProductRelease.skuImgItem.attr('src', $(this).attr('data-src'))
                        ProductRelease.skuImgItem.attr('data-src', $(this).attr('data-usrc'))
                    }
                })
                //没选中
                if (!hasSelect) {
                    Common.showInfoMsg('请选择图片')
                    return false;
                } else {
                    $('#insertcustom_img').modal('hide');
                }
            } else {
                //多图上传
                //判断是否有选中图片
                var imgArr = [];
                //相对路径
                var Img = [];
                //完整路径
                var ImgFull = [];
                var hasSelect = false;
                //获取当前选中的数量
                var Slength = $('#ImgBox').find('.rm').length;
                for (var i = 1; i <= Slength; i++) {
                    $('.pin').each(function (index, item) {
                        //选中样式并且下标一样的情况下
                        if ($(this).hasClass('rm') && $(item).attr('data-index') == i) {
                            hasSelect = true;
                            var src = $(this).attr('data-src');
                            var uSrc = $(this).attr('data-usrc')
                            ImgFull.push(src);
                            Img.push(uSrc)
                        }
                    })
                }


                for (var i = 0; i < Img.length; i++) {
                    var imgData = {
                        Img: Img[i],
                        ImgFull: ImgFull[i]
                    };
                    imgArr.push(imgData);
                }
                var imgList = {
                    imglistData: imgArr
                };
                //如果数量超过5个提示
                if (Img.length + ($('#productImg').find('.select-img').length - 1) > 5) {
                    Common.showInfoMsg('商品主图最多只能上传5张')
                    return false;
                }
                var render = template.compile(ProductRelease.imgTplEdit);
                var html = render(imgList);
                $("#drapBox").append(html);
                $('#insertcustom_img').modal('hide');
                // var drapBox = document.getElementById("drapBox");
                //
                // new Sortable(drapBox, { group: "omega" });

            }
        })

        //初始化富文本编辑器
        var ue = UE.getEditor('hcEditor');
        var ue2 = UE.getEditor('container');
        ue2.ready(function () {
            ue2.setHeight(500);
            ue.ready(function () {
                ue.setHeight(500);
                ProductRelease.adminBrandList();

            });
        });
        //上传小图标
        // uploadIconPic('#small_upload_pick', '#small_icon', '/product/AdminUploadProductImg');


        //图片上传
        $('#uploadfile_btn').on('change', function () {
            //获取文件
            var file = $("#editorContent").find("input")[0].files[0];
            var formData = new FormData();
            formData.append("upfile", file);
            var xhr = new XMLHttpRequest();
            xhr.open('post', SignRequest.urlPrefix + '/product/AdminUploadProductImg');
            xhr.send(formData);
            xhr.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    console.log(JSON.parse(this.response))
                    if (JSON.parse(this.response).Code == "100") {
                        UE.getEditor('hcEditor').setContent('<img src="' + SignRequest.urlPrefixNoApi + JSON.parse(this.response).Data + '">', true)
                    }
                }
            };
        });

        //input失去焦点颜色变回原来
        $('input[type="text"],input[type="number"]').on('blur', function () {
            $(this).css('border', '1px solid #ccc')
        })
        $('input[type="text"],input[type="number"]').on('focus', function () {
            $(this).css('border', '1px solid #3c8dbc')
        })

        // 下一步
        $("#nextStep,#secondBtn").on("click", function () {
            //礼包名称
            if (!Validate.emptyValidateAndFocusAndColor("#Name", "请输入礼包名称", "")) {
                return false;
            }
            //零售价
            if (!Validate.emptyValidateAndFocusAndColor("#ShopPrice", "请输入零售价", "")) {
                return false;
            }

            //成本价
            if (!Validate.emptyValidateAndFocusAndColor("#costPrice", "请输入成本价", "")) {
                return false;
            }
            if(ProductRelease.pIdList.length <=0){
                Common.showErrorMsg('请选择商品')
                return false
            }


            //sku处理
            var skuList = [];
            var spliter = '\u2299';

            if ($('#skuBody').find('.specdefault').length > 0) {
                Common.showInfoMsg('有某一项未选择')
                return false;
            }
            var flag = false;
            $('.SpecificationTr').each(function (index, item) {
                var data = {};
                if ($(item).find('.CostPrice').val() == "" || $(item).find('.retailPrice').val() == "" || $(item).find('.productId').val() == "" || $(item).find('.select-img').attr('data-src') == "" || $(item).find('.select-img').attr('data-src') == undefined) {
                    flag = true;
                }
                data.ShowImg = $(item).find('.select-img').attr('data-src');
                data.KMSDSn = $(item).find('.merchantCode').val();
                data.Number = $(item).find('.inventory').val() ? $(item).find('.inventory').val() : 0;
                data.CostPrice = $(item).find('.CostPrice').val();
                data.ShopPrice = $(item).find('.retailPrice').val();
                data.MemberPrice = $(item).find('.memberPrice').val() ? $(item).find('.memberPrice').val() : 0.01;
                data.StoreMPrice = "0.01";
                data.CorNum = "";
                data.PSn = 0;
                data.CostPrice = $(item).find('.costPrice').val();
                data.AttrValueId = $(item).find('.specdiv').eq(0).attr('data-idslist').split(spliter);
                skuList.push(data);
            })
            if (flag) {
                Common.showInfoMsg('规格值不能为空')
                return false;
            }
            ProductRelease.skuList = skuList;

            $("#first").removeClass("active");
            $("#second").addClass("active");
            $("#typeNav li").eq(0).removeClass("active");
            $("#typeNav li").eq(1).addClass("active");
        });

        // 上一步
        $("#lastStep").on("click", function () {
            $("#second").removeClass("active");
            $("#first").addClass("active");
            $("#typeNav li").eq(1).removeClass("active");
            $("#typeNav li").eq(0).addClass("active");
        });

        //完成
        $("#finish").on("click", function () {
            // if (ProductRelease.checkNeedInput()) {
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

            // }
        });

        // 添加标签
        $("#addBtn").on("click", function () {
            $("#addBtn").hide();
            $("#addDiv").show();
        });

        // 取消添加
        $("#addDiv").on("click", ".cancel-add", function () {
            $("#addDiv").hide();
            $("#addBtn").show();
            $("#addInput").val("");
        });

        //保存添加
        $("#save").on("click", function () {
            if ($("#addInput").val() != "") {
                ProductRelease.addProductLabel();
            } else {
                Common.showInfoMsg("请先输入标签名称")
            }
        });

        // 选择商品分类
        $("#selectClassify").on("click", function () {
            $(".mask").show();
        });

        // 关闭商品分类弹窗
        $(".mask").on("click", ".close", function () {
            $(".mask").hide();
        });

        // 选择分类
        $(".mask").on("click", ".pick", function () {
            var id = $(this).attr("data-id");
            var name = $(this).attr("data-name");
            $("#classifyName").attr("data-id", id);
            $("#classifyName").text(name);
            $(".mask").hide();
        });

        //商品属性改变的时候
        $("#ProductAttribute").on('change', function () {
            if (ProductRelease.isOpen) {
                //标识是不是选择商品属性
                clearData(true);
            } else {
                ProductRelease.attributesList();
            }
        });
        //规格弹窗里面的添加属性按钮
        $('body').on('click', '.addSkuValueBtn', function () {
            var id = $(this).parents('.skuItemList').siblings('.formitemtitle').attr('data-id');
            var name = $(this).siblings('input').val();
            var that = this;
            if (name == "") {
                Common.showInfoMsg('请输入属性名')
                return false
            }
            ProductRelease.addAttributes(id, name, that)
        });
        //批量填充按钮点击
        $('body').on('click', '#btnBatchOk', function () {
            //成本价
            var txtBatchCostPrice = $('#txtBatchCostPrice').val();
            //零售价
            var txtBatchRetailPrice = $('#txtBatchRetailPrice').val();
            //会员价
            var txtmemberPrice = $('#txtmemberPrice').val();
            //商家编码
            var txtmerchantCode = $('#txtmerchantCode').val();
            //商品编号
            var txtproductId = $('#txtproductId').val();
            //库存
            var txtBatchStore = $('#txtBatchStore').val();
            if (txtBatchCostPrice != "") {
                $('.SpecificationTr').find('.costPrice').each(function (index, item) {
                    $(item).val(txtBatchCostPrice)
                })
            }
            if (txtBatchRetailPrice != "") {
                $('.SpecificationTr').find('.retailPrice').each(function (index, item) {
                    $(item).val(txtBatchRetailPrice)
                })
            }
            if (txtmemberPrice != "") {
                $('.SpecificationTr').find('.memberPrice').each(function (index, item) {
                    $(item).val(txtmemberPrice)
                })
            }
            if (txtmerchantCode != "") {
                $('.SpecificationTr').find('.merchantCode').each(function (index, item) {
                    $(item).val(txtmerchantCode)
                })
            }
            if (txtproductId != "") {
                $('.SpecificationTr').find('.productId').each(function (index, item) {
                    $(item).val(txtproductId)
                })
            }
            if (txtBatchStore != "") {
                $('.SpecificationTr').find('.inventory').each(function (index, item) {
                    $(item).val(txtBatchStore)
                })
            }
        })

        //sku
        // 开启规格
        $('body').on('click', '#openSkuBox', function () {
            //打开模态框
            $('#mySkuBox').modal('show');


        });
        $('body').on('click', '#btnshowSkuValue', function () {
            //打开模态框
            $('#mySkuBox').modal('show');

        });
        //添加规格
        $('body').on('click', '.skuItemList a', function () {
            $(this).hide();
            $(this).siblings('span').show();
        });
        //生成的表中属性的点击
        $('body').on('click', '.specdiv', function () {

            var that = $(this);
            var spliter = '\u2299'
            var id = $(this).attr('valueid');
            var skuid = $(this).attr('skuid');
            var arr = $(this).attr('data-valuelist').split(spliter);
            var idsArr = $(this).attr('data-idslist').split(spliter);
            var top = $(this).offset().top;
            var left = $(this).offset().left;
            var rowindex = $(this).parents('.SpecificationTr').attr('rowindex');
            var rowid = $(this).attr('rowid');
            $('.target_box').each(function (index, item) {
                if ($(item).attr('id') == skuid) {
                    $(item).css("left", left).css('top', top + 50);
                    $(item).attr("rowindex", rowindex);
                    $(item).attr("rowid", rowid);
                    $(item).attr('data-valuelist', that.attr('data-valuelist'));
                    $(item).attr('data-idslist', that.attr('data-idslist'));
                    updateBox($(item), arr, that, false, idsArr);
                    $(item).show()
                } else {
                    $(item).hide()
                }
            })


        });
        //添加的行的请选择点击
        $('body').on('click', '.specdefault', function () {
            var that = $(this);
            var spliter = '\u2299'
            var id = $(this).attr('valueid');
            var skuid = $(this).attr('skuid');
            var arr = $(this).attr('data-valuelist').split(spliter)
            var top = $(this).offset().top;
            var left = $(this).offset().left;
            var rowindex = $(this).parents('.SpecificationTr').attr('rowindex');
            var rowid = $(this).attr('rowid');
            $('.target_box').each(function (index, item) {
                if ($(item).attr('id') == skuid) {
                    $(item).css("left", left).css('top', top + 22);
                    $(item).attr("rowindex", rowindex);
                    $(item).attr("rowid", rowid);
                    $(item).attr('data-valuelist', that.attr('data-valuelist'))
                    $(item).attr('data-idslist', that.attr('data-idslist'))
                    updateBox($(item), arr, that, true);
                    $(item).show()
                } else {
                    $(item).hide()
                }
            })
        });
        //skuBox确认点击，生成组合
        $('body').on('click', '#sureBtn', function () {
            //判断是否有选中，没选中提示

            if ($('#skuItems').find('input[type=checkbox]').length > 0) {
                //并且要有选中
                var flag = false;
                $('#skuItems').find('input[type=checkbox]').each(function (index1, item1) {
                    if (this.checked) {
                        flag = true;
                    }
                })
                if (flag) {
                    //先清除
                    $('.target_box').each(function (index, item) {
                        $(item).remove();
                    })
                    var data = combineAttr();
                    ProductRelease.result = data.values;
                    ProductRelease.ids = data.ids;
                    ProductRelease.skuid = data.skuid;
                    var result = data.values;
                    var ids = data.ids;
                    var skuid = data.skuid;
                    var allcombination = data.allcombination;
                    var allIdcombination = data.allIdcombination;
                    console.log("所有的组合数组", allcombination)
                    console.log("所有的组合数组", allIdcombination)
                    render(result, ids, skuid, allcombination, allIdcombination)
                    //关闭模态框
                    $('#mySkuBox').modal('hide');
                    $('#skuRow').show();
                    $('#specificationsBox').hide();
                    //标识是否开启规格
                    ProductRelease.isOpen = true;
                } else {

                    Common.showInfoMsg('请选择规则属性值')
                }

            } else {
                Common.showInfoMsg('请选择规则属性值')
            }


        });
        //targetBox里面的属性点击
        $('body').on('click', '.specspan', function () {
            //id值
            var valueid = $(this).attr('valueid');
            //属性名
            var text = $(this).text();
            //第几列
            var rowindex = $(this).parent('.target_box').attr('rowindex');
            //第几个
            var rowid = $(this).parent('.target_box').attr('rowid');
            //组合的值 如："红⊙大⊙A"；
            var valueList = $(this).parent('.target_box').attr('data-valuelist');
            var idsList = $(this).parent('.target_box').attr('data-idslist');
            //判断是不是请选择点击的
            var isSelectBox = $(this).attr('isSelectBox');
            $('.SpecificationTr').each(function (index, item) {
                if (index == rowindex) {
                    $(item).find('td').eq(rowid).find('div').attr("valueid", valueid);
                    $(item).find('td').eq(rowid).find('div').text(text);
                    // $(item).find('td').eq(rowid).find('div').attr('class','specdiv');
                    $(item).find('td').eq(rowid).find('div').addClass('notspecspan');
                    var that = $(item).find('td');
                    setallKeys(text, rowid, valueList, rowindex, that, isSelectBox, valueid, idsList)
                }
            });


            $(this).parent('.target_box').hide();

        });
        //添加按钮点击的时候
        $('body').on('click', '#btnAddItem', function () {
            //获取总共有多少行
            var lineLength = $('.SpecificationTr').length;
            renderLine(lineLength)
            ProductRelease.isOpen = true;
        });
        //点击删除按钮
        $('body').on('click', ".DelectBtn", function () {
            //获取删除行的下标
            var index = $(this).parents('.SpecificationTr').attr('rowindex');
            var item = $(this).parents('.SpecificationTr');
            //判断当前行是否是已经选择好的
            var isComplete = !item.hasClass('specdefault');
            //选择好的就可以删除
            if (isComplete) {
                ProductRelease.allKeys.splice(index, 1);
                ProductRelease.allIds.splice(index, 1);

                item.remove();
                $('.SpecificationTr').each(function (index, item) {
                    $(this).attr('rowindex', index);
                })
                //重新计算(未选中的组合)
                screening();
                //说明已经全部删除
                if (ProductRelease.allIds.length == 0) {
                    ProductRelease.isOpen = false;
                }


            } else {
                Common.showInfoMsg("请选择规格")
                return false;
            }


        });
        //点击清空按钮
        $('body').on('click', '#clearData', function () {
            $('#mySkuBox').modal('hide');
            ProductRelease.isOpen = false;
            clearData();

        });
        //点其他地方消失
        $(document).bind('click', function (e) {
            var e = e || window.event; //浏览器兼容性
            var elem = e.target || e.srcElement;
            console.log("元素为", $(elem).attr('class'))
            while (elem) { //循环判断至跟节点，防止点击的是div子元素
                if ($(elem).attr('class') && $(elem).attr('class') == 'specdiv' || $(elem).attr('class') == 'specdiv notspecspan' || $(elem).attr('class') == 'specdefault' || $(elem).attr('class') == 'specdefault notspecspan') {
                    console.log("进来了")
                    return;
                }
                elem = elem.parentNode;
            }
            $('.target_box').each(function (index, item) {
                $(item).hide();
            })
        });

        //对选中的集合重新赋值
        function setallKeys(text, rowid, valueList, rowindex, that, isSelectBox, valueid, idsList) {

            var spliter = '\u2299';
            if (isSelectBox) {

                var array = valueList.split(spliter);
                array[rowid] = text;
                array = array.join(spliter);
                that.siblings('td').find('div').attr('data-valuelist', array);
                var array1 = idsList.split(spliter);
                array1[rowid] = valueid;
                array1 = array1.join(spliter);
                that.siblings('td').find('div').attr('data-idslist', array1);
                //看看是否还有项是未选的,全部都选了的话就向已选中列表添加当前项
                if (array.indexOf("空") > -1) {

                } else {
                    //为已选中列表添加当前项
                    ProductRelease.allKeys.push(array);
                    //为已选中列表添加当前项
                    ProductRelease.allIds.push(array1);
                    $('.SpecificationTr').eq(rowindex).find('.specdefault').attr('class', 'specdiv');
                }

            } else {
                //为生成的项data-valuelist赋值
                ProductRelease.allKeys.forEach(function (item, index) {
                    if (index == rowindex) {
                        console.log(item)
                        var arr = item.split(spliter);
                        arr[rowid] = text;
                        arr = arr.join(spliter);
                        that.siblings('td').find('div').attr('data-valuelist', arr);
                        ProductRelease.allKeys[index] = arr;

                    }
                })
                //为生成的项data-idslist赋值
                ProductRelease.allIds.forEach(function (item, index) {
                    if (index == rowindex) {
                        console.log(item)
                        var arr = item.split(spliter);
                        arr[rowid] = valueid;
                        arr = arr.join(spliter);
                        that.siblings('td').find('div').attr('data-idslist', arr);
                        ProductRelease.allIds[index] = arr;

                    }
                })
            }

            //重新计算未选中的项
            screening()
            console.log(ProductRelease.allKeys)
            console.log(ProductRelease.allIds)

        };

        //计算组合数据
        function combineAttr() {
            var result = {};
            var allcombination = {};
            var allIdcombination = {};
            var selectList = {};
            var selectidList = {};
            var keysLength = $('#skuItems').find('.formitemtitle').length;
            var ids = {};
            var data = {};
            var skuid = [];
            //编辑的时候用到
            var skuSelectList = [];
            //循环规格属性
            for (var i = 0; i < keysLength; i++) {

                //获取当前行
                var $current = $('#skuItems').find('.skuItemListUl').eq(i);
                var key = $('#skuItems').find('.formitemtitle').eq(i).attr('data-type');
                var Id = $('#skuItems').find('.formitemtitle').eq(i).attr('data-id');

                if ($('#skuItems').find('.skuItemListUl').eq(i).find('input[type="checkbox"]:checked').length > 0) {
                    $('#skuItems').find('.skuItemListUl').eq(i).find('input[type="checkbox"]').each(function (index, item) {
                        var key = $(item).parents('.skuItem').find('.formitemtitle').attr('data-type');
                        var Id = $(item).parents('.skuItem').find('.formitemtitle').attr('data-id');
                        // //获取所有的文字组合方式（包括不选中）
                        if (!allcombination[key]) allcombination[key] = [];
                        allcombination[key].push($(item).siblings('span').text());
                        // //获取所有的id组合方式（包括不选中）
                        if (!allIdcombination[key]) allIdcombination[key] = [];
                        allIdcombination[key].push($(item).attr('data-id'));
                        //如：颜色的id是111 =》 111 ：[红,白,蓝]
                        if (!selectList[Id]) selectList[Id] = [];
                        selectList[Id].push($(item).siblings('span').text());
                        //如：颜色的id是111 =》 111 ：[1,2,3]
                        if (!selectidList[Id]) selectidList[Id] = [];
                        selectidList[Id].push($(item).attr('data-id'));
                    })
                }


                $('#skuItems').find('.skuItemListUl').eq(i).find('input[type="checkbox"]').each(function (index, item) {

                    if (this.checked) {

                        if (!result[key]) result[key] = [];
                        result[key].push($(item).siblings('span').text());
                        if (!ids[key]) ids[key] = [];
                        ids[key].push($(item).attr('data-id'));
                        if (skuid.indexOf($('#skuItems').find('.formitemtitle').eq(i).attr('data-id')) > -1) {

                        } else {
                            skuid.push($('#skuItems').find('.formitemtitle').eq(i).attr('data-id'))
                        }

                    }
                })
            }
            ;
            data.values = result;
            data.ids = ids;
            data.skuid = skuid;
            data.allcombination = allcombination;
            data.allIdcombination = allIdcombination;
            ProductRelease.selectList = selectList;
            ProductRelease.selectidList = selectidList;
            console.log("选中的维度", allcombination)
            console.log("选中的维度", allIdcombination)
            return data;
        };

        //渲染的全部组合
        function render(result, ids, skuid, allcombination, allIdcombination) {
            var spliter = '\u2299';
            var keys = [];
            for (var attr_key in result) {
                keys.push(attr_key)
            }
            var allkey = [];
            for (var all_key in allcombination) {
                allkey.push(all_key)
            }

            var array = arrayNested(result, keys);
            var idsList = arrayNested(ids, keys);
            var allcombinationList = arrayNested(allcombination, allkey);
            var allIdcombination = arrayNested(allIdcombination, allkey)
            console.log(allcombinationList)
            console.log("id返回", allIdcombination)
            var html = "";
            //渲染表头
            html += `<tr class="SpecificationTh">`;
            for (var j = 0; j < keys.length; j++) {
                html += `<td align="center" class="fieldCell" style="width:50px !important" skuid="${skuid[j]}"><span>${keys[j]}</span></td>`;
                //创建targetBox
                var box = `<div style="position: absolute; display: none;" id="${skuid[j]}" class="target_box">
                                <span valueid="47" class="sku19values specspan" style="padding:3px;">1.2m</span>
                            </div>`
                $('body').append(box);
            }
            html += `
                         <td align="center">成本价</td>
                         <td align="center">零售价</td>
                         <!--<td align="center">会员价</td>-->
                         <td align="center">时代编码</td>
                         <!--<td align="center">商品编号</td>-->
                         <!--<td align="center" id="storeField"><em>*</em>库存</td>-->
                         <td align="center">图片</td>
                         <td align="center">操作</td>`;
            html += `</tr>`;
            //获取所有组合
            ProductRelease.allKeys = doExchange(array);
            ProductRelease.allIds = doExchange(idsList);
            ProductRelease.allcombination = doExchange(allcombinationList);
            ProductRelease.allIdcombination = doExchange(allIdcombination);
            console.log("所有的id组合", ProductRelease.allIdcombination)
            //渲染(循环所有组合)
            for (var i = 0; i < ProductRelease.allKeys.length; i++) {
                //内容
                var value = ProductRelease.allKeys[i].split(spliter);
                //id
                var ids = ProductRelease.allIds[i].split(spliter);

                html += `<tr id="sku_${i + 1}" rowindex="${i}" class="SpecificationTr">`;
                for (var k = 0; k < value.length; k++) {
                    html += `<td align="center">
                                <div id="skuDisplay_1_20" rowid="${k}" skuid="${skuid[k]}" valueid="${ids[k]}" data-valueList="${ProductRelease.allKeys[i]}" data-idsList="${ProductRelease.allIds[i]}"class="specdiv">${value[k]}</div>
                             </td>`;
                }
                html += `
                       <td align="center">
                         <input type="text" class="skuItem_CostPrice form-control costPrice" style="width:100px;">
                       </td>
                       <td align="center">
                         <input type="text" class="skuItem_CostPrice form-control retailPrice" style="width:100px;">
                       </td>
                       <!--<td align="center">-->
                         <!--<input type="text" class="skuItem_CostPrice form-control memberPrice" style="width:100px;">-->
                       <!--</td>-->
                       <td align="center">
                         <input type="text" class="skuItem_CostPrice form-control merchantCode"  style="width:100px;">
                       </td>
                        <!--<td align="center">-->
                         <!--<input type="text" class="skuItem_CostPrice form-control productId"  style="width:100px;">-->
                       <!--</td>-->
                       <!--<td align="center">-->
                         <!--<input type="text" class="skuItem_CostPrice form-control inventory" style="width:100px;">-->
                       <!--</td>-->
                       <td align="center">
                         <div class="img-containerSku">
                               <img class="select-img" src="/public/images/addImg.png">
                               <!--<input class="img-input" type="file"accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">-->
                               <!--<img class="close-img" src="/public/images/close.png">-->
                         </div>
                       </td>
                       <td align="center">
                         <a style="float:left;width:100%;text-algin:center" href="javascript:;" id="deleSku_1">
                          <span style="float:none;color:red" class="glyphicon glyphicon-trash DelectBtn"></span>
                         </a>
                       </td>`;
                html += `</tr>`;
                $('#skuBody').html(html)

            }

        };

        //计算出所有组合
        function doExchange(arr) {
            var spliter = '\u2299'
            var len = arr.length;
            // 当数组大于等于2个的时候
            if (len >= 2) {
                // 第一个数组的长度
                var len1 = arr[0].length;
                // 第二个数组的长度
                var len2 = arr[1].length;
                // 2个数组产生的组合数
                var lenBoth = len1 * len2;
                //  申明一个新数组,做数据暂存
                var items = new Array(lenBoth);
                // 申明新数组的索引
                var index = 0;
                // 2层嵌套循环,将组合放到新数组中
                for (var i = 0; i < len1; i++) {
                    for (var j = 0; j < len2; j++) {
                        items[index] = arr[0][i] + spliter + arr[1][j];
                        index++;
                    }
                }
                // 将新组合的数组并到原数组中
                var newArr = new Array(len - 1);
                for (var i = 2; i < arr.length; i++) {
                    newArr[i - 1] = arr[i];
                }
                newArr[0] = items;
                // 执行回调
                return doExchange(newArr);
            } else {
                return arr[0];
            }
        };

        //数组嵌套
        function arrayNested(result, keys) {
            var arr = Object.keys(result);
            console.log(arr.length)
            if (arr.length > 1) {
                var length = keys.length;
                console.log(length);
                var Inarray = new Array();
                for (var i = 0; i < length; i++) {
                    Inarray[i] = new Array();
                    var len2 = result[keys[i]].length;
                    for (var j = 0; j < len2; j++) {
                        if (result[keys[i]][j] != undefined)
                            Inarray[i][j] = result[keys[i]][j];
                    }
                }
                return Inarray
            } else {
                var oneArray = new Array();
                oneArray[0] = new Array();
                for (var k = 0; k < result[keys[0]].length; k++) {
                    if (result[keys[0]][k] != undefined)
                        oneArray[0][k] = result[keys[0]][k];
                }
                return oneArray
            }


        };

        //为targetBox更新数据
        function updateBox(item, arr, that, isSelectBox, idsArr) {
            var spliter = '\u2299';
            var index = that.attr('rowid');
            var chooseList = that.attr('data-valuelist').split(spliter);
            var chooseIDList = that.attr('data-idslist').split(spliter);
            var skuid = item.attr('id');
            var copy2 = chooseList.slice();
            var copy4 = chooseIDList.slice();
            screening();
            var html = "";
            if (isSelectBox) {
                //获取所有可能存在的文字组合的「幂集」
                var allPowerset = [];
                for (var i = 0; i < ProductRelease.uncheckList.length; i++) {
                    var arr1 = ProductRelease.uncheckList[i].split(spliter);
                    allPowerset = allPowerset.concat(powerset(arr1))
                }
                ProductRelease.allPowerset = unique(allPowerset)
                //获取所有可能存在的id组合的「幂集」
                var allIDPowerset = [];
                for (var i = 0; i < ProductRelease.uncheckIDList.length; i++) {
                    var arr2 = ProductRelease.uncheckIDList[i].split(spliter);
                    allIDPowerset = allIDPowerset.concat(powerset(arr2))
                }
                ProductRelease.allIDPowerset = unique(allIDPowerset)

                var list = copy4.join(spliter);
                var array = list.split(spliter);
                console.log("当前选择的项的数组", array);
                for (var i = 0; i < ProductRelease.selectidList[skuid].length; i++) {
                    var id = ProductRelease.selectidList[skuid][i];
                    var goods = ProductRelease.selectidList[skuid][i];
                    var FLAG1 = false;
                    var length = array.length;
                    var copy3 = array.slice();
                    copy3[index] = goods;
                    var str = copy3.join();
                    var reger = new RegExp("空", "g");
                    var reger1 = new RegExp(",", "g");
                    str = str.replace(reger, "");
                    str = str.replace(reger1, "");
                    console.log("处理后的字符串", str);
                    ProductRelease.allIDPowerset.forEach(function (item1, index1) {
                        var Item1 = item1.join();
                        var reger1 = new RegExp(",", "g");
                        Item1 = Item1.replace(reger1, "");
                        // console.log('所有的id组合的字符串',Item1)
                        if (str == Item1) {
                            console.log('相同的有', Item1)
                            FLAG1 = true;
                        }
                    })

                    if (FLAG1) {
                        html += `<span valueid="${id}" class="sku${skuid}values specspan" isSelectBox="true" style="padding:3px;">${ProductRelease.selectList[skuid][i]}</span>`
                    } else {
                        html += `<span valueid="${id}" class="sku${skuid}values specsna " style="padding:3px;">${ProductRelease.selectList[skuid][i]}</span>`
                    }

                }


            } else {
                for (var i = 0; i < ProductRelease.selectidList[skuid].length; i++) {
                    var flag = false;
                    var copy = chooseIDList.slice();
                    copy[index] = ProductRelease.selectidList[skuid][i];
                    var list = copy.join(spliter);
                    console.log("当前选择的项", list);


                    for (var attr_key in ProductRelease.uncheckIDList) {
                        var id = ProductRelease.selectidList[skuid][i];
                        if (list == ProductRelease.uncheckIDList[attr_key]) {
                            flag = true
                        }
                    }
                    if (flag) {
                        html += `<span valueid="${id}" class="sku${skuid}values specspan" style="padding:3px;">${ProductRelease.selectList[skuid][i]}</span>`
                    } else {
                        html += `<span valueid="${id}" class="sku${skuid}values specsna " style="padding:3px;">${ProductRelease.selectList[skuid][i]}</span>`
                    }

                }
            }

            item.html(html);


        };

        //筛选出未选中的项
        function screening() {
            var spliter = '\u2299';
            //未选中的组合
            ProductRelease.uncheckList = [];
            //未选中的id组合
            ProductRelease.uncheckIDList = [];
            var Copy = ProductRelease.allcombination.slice();
            var Copy1 = ProductRelease.allIdcombination.slice();
            var length = ProductRelease.allcombination.length;
            var length1 = ProductRelease.allIdcombination.length;
            console.log("所有的文字组合", ProductRelease.allcombination);
            console.log("所有的ID组合", ProductRelease.allIdcombination);
            console.log("已选中的组合", ProductRelease.allKeys)
            //所有文字组合处理
            for (var i = 0; i < ProductRelease.allcombination.length; i++) {
                var currentItem = ProductRelease.allcombination[i];
                var Flag = false;
                for (var j = 0; j < ProductRelease.allKeys.length; j++) {
                    var currentSelectItem = ProductRelease.allKeys[j];
                    if (currentItem != currentSelectItem) {

                    } else {
                        Flag = true;
                        break;
                    }
                }
                if (Flag) {
                    var copyLength = Copy.length;
                    var cha = length - copyLength;

                    Copy.splice(i - cha, 1);
                }

            }
            //所有id组合处理
            for (var i = 0; i < ProductRelease.allIdcombination.length; i++) {
                var currentItem1 = ProductRelease.allIdcombination[i];
                var Flag1 = false;
                for (var j = 0; j < ProductRelease.allIds.length; j++) {
                    var currentSelectItem1 = ProductRelease.allIds[j];
                    if (currentItem1 != currentSelectItem1) {

                    } else {
                        Flag1 = true;
                        break;
                    }
                }
                if (Flag1) {
                    var copyLength1 = Copy1.length;
                    var cha1 = length1 - copyLength1;

                    Copy1.splice(i - cha1, 1);
                }

            }
            ProductRelease.uncheckList = Copy;
            console.log("未选中文字的组合", ProductRelease.uncheckList);
            ProductRelease.uncheckIDList = Copy1;
            console.log("未选中ID的组合", ProductRelease.uncheckIDList);
            //获取所有可能存在的文字组合的「幂集」
            var allPowerset = [];
            for (var i = 0; i < ProductRelease.uncheckList.length; i++) {
                var arr1 = ProductRelease.uncheckList[i].split(spliter);
                allPowerset = allPowerset.concat(powerset(arr1))
            }
            ProductRelease.allPowerset = unique(allPowerset)
            //获取所有可能存在的id组合的幂集
            var allIDPowerset = [];
            for (var i = 0; i < ProductRelease.uncheckIDList.length; i++) {
                var arr1 = ProductRelease.uncheckIDList[i].split(spliter);
                allIDPowerset = allIDPowerset.concat(powerset(arr1))
            }
            ProductRelease.allIDPowerset = unique(allIDPowerset)
            console.log("所有可能存在的文字组合的幂集", ProductRelease.allPowerset)
            console.log("所有可能存在的id组合的幂集", ProductRelease.allIDPowerset)


        };

        //渲染增加的行
        function renderLine(length) {
            var spliter = '\u2299'
            var index = length;
            var html = "";
            console.log(ProductRelease.result);
            console.log(ProductRelease.skuid)
            var arr = Object.keys(ProductRelease.result);
            var valuesList = [];
            for (var i = 0; i < arr.length; i++) {
                valuesList.push('空');
            }
            var valuesList = valuesList.join(spliter)
            html += `<tr id="sku_${index}" rowindex="${index}" class="SpecificationTr">`
            for (var i = 0; i < arr.length; i++) {
                html += `<td align="center"><div id="skuDisplay_${index}_${ProductRelease.skuid[i]}" rowid="${i}" skuid="${ProductRelease.skuid[i]}" valueid="" data-valuelist = "${valuesList}" data-idslist="${valuesList}"  class="specdefault">请选择</div></td>`
            }
            html += `
                       <td align="center">
                         <input type="text" class="skuItem_CostPrice form-control costPrice" style="width:100px;">
                       </td>
                       <td align="center">
                         <input type="text" class="skuItem_CostPrice form-control retailPrice" style="width:100px;">
                       </td>
                       <!--<td align="center">-->
                         <!--<input type="text" class="skuItem_CostPrice form-control memberPrice" style="width:100px;">-->
                       <!--</td>-->
                       <td align="center">
                         <input type="text" class="skuItem_CostPrice form-control merchantCode"  style="width:100px;">
                       </td>
                        <!--<td align="center">-->
                         <!--<input type="text" class="skuItem_CostPrice form-control productId"  style="width:100px;">-->
                       <!--</td>-->
                       <!--<td align="center">-->
                         <!--<input type="text" class="skuItem_CostPrice form-control inventory" style="width:100px;">-->
                       <!--</td>-->
                        <td align="center">
                         <div class="img-containerSku">
                               <img class="select-img" src="/public/images/addImg.png">
                               <!--<input class="img-input" type="file"accept="image/gif,image/jpeg,image/jpg,image/png,image/svg">-->
                               <!--<img class="close-img" src="/public/images/close.png">-->
                         </div>
                       </td>
                       <td align="center">
                         <a style="float:left;width:100%;text-algin:center" href="javascript:;" id="deleSku_1">
                          <span style="float:none;color:red" class="glyphicon glyphicon-trash DelectBtn"></span>
                         </a>
                       </td>`;
            html += `</tr>`
            $('#skuBody').append(html)

        };

        //取得集合的所有子集「幂集」
        function powerset(arr) {
            // console.log("传进来的数组",arr)
            var ps = [
                []
            ];
            for (var i = 0; i < arr.length; i++) {
                for (var j = 0, len = ps.length; j < len; j++) {
                    ps.push(ps[j].concat(arr[i]));
                }
            }
            return ps;
        };

        //数组去重
        function unique(arr) {
            var result = [],
                hash = {};
            for (var i = 0, elem;
                 (elem = arr[i]) != null; i++) {
                if (!hash[elem]) {
                    result.push(elem);
                    hash[elem] = true;
                }
            }
            return result;
        };

        //数据清空
        function clearData(isChange) {
            $('#skuBody').html("");
            $('#skuItems').find('input[type="checkbox"]').each(function (index, item) {
                if (this.checked) {
                    this.checked = false;
                }
            })
            $('#specificationsBox').show();
            $('#skuRow').hide();
            $('.target_box').each(function (index, item) {
                $(item).remove();
            })
            $('#openSkuBox').show();
            if (isChange) {
                ProductRelease.attributesList();
            }

        };


    },
    //初始化BootstrapTable
    initBootstrapTable: function () {
        $('#choiceProductTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + "/product/GiftProductList",
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pageSizeDropDown").val(),
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],
            idField: "Id", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: ProductRelease.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: ProductRelease.responseHandler,
            columns: [
                {
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
            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function () {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function () { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function (data) {
            },
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
                url: SignRequest.urlPrefix + "/product/GiftProductList",
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
                url: SignRequest.urlPrefix + "/product/GiftProductList",
                query: obj
            }
        );
        ProductRelease.initBootstrapTable();
    },
    // 获取商品分类
    getProductCategory: function (ele) {
        var methodName = "/category/AdminFirstCategoryList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(ProductRelease.cateTpl);
                var html = render(data.Data);
                $("#choiceCateId").append(html);
                //初始化搜索下拉框
                $("#choiceCateId").chosen();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取后台品牌列表
    adminBrandList: function () {
        var methodName = "/brand/AdminBrandList";
        var data = {
            "Name": "",
            "Page": {
                "PageSize": 1000,
                "PageIndex": 1
            }
        };
        SignRequest.set(methodName, data, function (data) {
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
    //获取后台属性列表
    attributesList: function (isInit, AttrGroupId, result) {
        var methodName = "/Distribution/GiftBagAttributesList";
        var data = {
            "AttrGroupId": isInit ? AttrGroupId : $('#ProductAttribute').val(),
            "GId": Common.getUrlParam('PId') ? Common.getUrlParam('PId') : '0',
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                console.log(data)
                //如果长度大于0的话显示开启按钮
                if (data.Data.AttributesList.length > 0) {
                    $('#openSkuBox').show();
                } else {
                    $('#openSkuBox').hide();
                    ProductRelease.isOpen = false;
                }
                var render = template.compile(ProductRelease.skuAttrTpl);
                var html = render(data.Data);
                $("#skuItemsUl").html(html);
                if (isInit) {
                    $('#sureBtn').click();
                    console.log('结果是', result);
                    var render = template.compile(ProductRelease.editSkuTpl);
                    var html = render(result);
                    $("#skuBody").html(html);
                    var allKeys = [];
                    var allIds = [];
                    result.SKu.forEach(function (item, index) {
                        allKeys.push(item.AttrValueStringList);
                        allIds.push(item.AttrValueIdList);
                    })
                    console.log('之前选择的组合', ProductRelease.allKeys);
                    console.log('之前选择的id组合', ProductRelease.allIds);
                    ProductRelease.allKeys = allKeys;
                    ProductRelease.allIds = allIds;
                    console.log('当前选择的组合', ProductRelease.allKeys);
                    console.log('当前选择的id组合', ProductRelease.allIds);
                }

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台添加属性
    addAttributes: function (id, name, that) {
        var methodName = "/productSku/AddAttrValue";
        var data = {
            "AttrId": id,
            "AttrValueList": [name],
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                console.log(data)
                var value = $(that).siblings('input[type="text"]').val();
                if (value) {
                    var html = `<li style="padding:5px 0;margin-bottom:0;float:left;" class="contentLi">
                                                    <div class="checkBoxList">
                                                        <input type="checkbox" data-id="${data.Data}">
                                                        <span style="float:left;display: block">${value}</span>
                                                    </div>
                                                </li>`

                    $(that).parents('.skuItemListUl').find('.addBox').before(html);

                } else {
                    Common.showErrorMsg(data.Message);
                }
            }
        });
    },
    //获取运费模板列表
    adminTemplatesList: function () {
        var methodName = "/templates/AdminTemplatesList";
        var data = {
            "Page": {
                "PageSize": 1000,
                "PageIndex": 1
            }
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {

                var render = template.compile(ProductRelease.freightTpl);
                var html = render(data.Data);
                $("#TemplateId").append(html);
                $("#TemplateId").chosen();
                var PId = Common.getUrlParam("PId");
                if (PId != undefined && PId != "") {
                    ProductRelease.getProductInfo(PId);
                }
                //初始化搜索下拉框

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 添加商品标签
    addProductLabel: function () {
        var methodName = "/productLabel/AdminAddProductLabel";
        var data = {
            Name: $("#addInput").val()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var result = {};
                var ProductLabelList = [];
                ProductLabelList.push(data.Data.ProductLabel);
                result.ProductLabelList = ProductLabelList;

                var render = template.compile(ProductRelease.labelTpl);
                var html = render(result);
                $("#PlIdList").append(html);

                $("#addDiv").hide();
                $("#addBtn").show();
                $("#addInput").val("");

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 新增
    addProduct: function () {

        var methodName = "/Distribution/AdminAddGift";

        var ue = UE.getEditor('hcEditor');
        var ue2 = UE.getEditor('container');
        var Description = ue.getContent();
        var AfterSaleProtection = ue2.getContent();
        var PlIdList = [];
        var pList = $("#PlIdList label");
        for (var i = 0; i < pList.length; i++) {
            if (pList.eq(i).find("input").is(':checked')) {
                PlIdList.push(pList.eq(i).find("input").val());
            }
        }

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
        //AttrIdList
        var AttrIdList = [];
        $('.SpecificationTh').find('.fieldCell').each(function (index, item) {
            AttrIdList.push($(item).attr('skuid'))
        })

        var sku = [

            {
                "KMSDSn": $('#KMSDsn').val(),
                "CorNum": "",
                "MemberPrice": $("#MemberPrice").val() ? $("#MemberPrice").val() : 0.01,
                "ShopPrice": $("#ShopPrice").val(),
                "StoreMPrice": "0.01",
                "Number": $('#inventory').val() ? $('#inventory').val() : 0,
                "CostPrice": $('#costPrice').val(),
                "AttrValueId": [],
                "PSn": "",
            }
        ];
        var data = {
            CateId: $("#classifyName").attr("data-id"),
            BrandId: $('#ProductBrand').val(),
            Name: $("#Name").val(),
            Summary: $("#Summary").val() ? $("#Summary").val() : "",
            PlIdList: PlIdList,
            State: State,
            IsAppNotify: IsAppNotify,
            TemplateId: $("#TemplateId").val(),
            ImgList: Img,
            Description: Description,
            "SKu": !ProductRelease.isOpen ? sku : ProductRelease.skuList,
            AfterSaleProtection: $("#AfterSaleProtections").val(),
            "ShowImg": $('#small_icon').attr('data-src'),
            "MId": $('#StoreName1').attr('data-id'),
            "AttrGroupId": ProductRelease.isOpen ? $('#ProductAttribute').val() : "0",
            "AttrIdList": ProductRelease.isOpen ? AttrIdList : [],
            "SId": $('#ProductCommodity').val() == "1" ? $('#storeList').val() : 0,
            "Type": $('#ProductCommodity').val(),
            "VirtualSales": $("#virtualSales").val() ? $("#virtualSales").val() : 0,
            "VPromoteCount": $("#vPromoteCount").val() ? $("#vPromoteCount").val() : 0,
            "Amount": 0,
            "ShopPrice": $("#ShopPrice").val(),
            "CostPrice": $('#costPrice').val(),
            "PId":ProductRelease.pIdList
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("新增成功", function () {
                    location.href = '/product/applicationForGiftSetting'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 编辑
    editProduct: function () {
        var methodName = "/Distribution/AdminEditGiftBag";

        var ue = UE.getEditor('hcEditor');
        var ue2 = UE.getEditor('container');
        var Description = ue.getContent();
        var AfterSaleProtection = ue2.getContent();

        var PlIdList = [];
        var pList = $("#PlIdList label");
        for (var i = 0; i < pList.length; i++) {
            if (pList.eq(i).find("input").is(':checked')) {
                PlIdList.push(pList.eq(i).find("input").val());
            }
        }

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
        //AttrIdList
        var AttrIdList = [];
        $('.SpecificationTh').find('.fieldCell').each(function (index, item) {
            AttrIdList.push($(item).attr('skuid'))
        })

        var sku = [{
            "KMSDSn": $('#KMSDsn').val(),
            "CorNum": "",
            "Number": $('#inventory').val() ? $('#inventory').val() : 0,
            "MemberPrice": $("#MemberPrice").val() ? $("#MemberPrice").val() : 0.01,
            "ShopPrice": $("#ShopPrice").val(),
            "StoreMPrice": "0.01",
            "CostPrice": $('#costPrice').val(),
            "AttrValueId": [],
            "PSn": "",
        }];

        var data = {
            GId: Common.getUrlParam("PId"),
            CateId: $("#classifyName").attr("data-id"),
            BrandId: $('#ProductBrand').val(),
            Name: $("#Name").val(),
            Summary: $("#Summary").val() ? $("#Summary").val() : "",
            PlIdList: PlIdList,
            State: State,
            TemplateId: $("#TemplateId").val(),
            ImgList: Img,
            Description: Description,
            "SKu": !ProductRelease.isOpen ? sku : ProductRelease.skuList,
            AfterSaleProtection: $("#AfterSaleProtections").val(),
            "ShowImg": $('#small_icon').attr('data-src'),
            "MId": $('#StoreName1').attr('data-id'),
            "AttrGroupId": ProductRelease.isOpen ? $('#ProductAttribute').val() : "0",
            "AttrIdList": ProductRelease.isOpen ? AttrIdList : [],
            "SId": $('#ProductCommodity').val() == "1" ? $('#storeList').val() : 0,
            "Type": $('#ProductCommodity').val(),
            "VirtualSales": $("#virtualSales").val() ? $("#virtualSales").val() : 0,
            "VPromoteCount": $("#vPromoteCount").val() ? $("#vPromoteCount").val() : 0,
            "Amount": $('#bearNumber').val(),
            "ShopPrice": $("#ShopPrice").val(),
            "CostPrice": $('#costPrice').val(),
            "PId":ProductRelease.pIdList
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("编辑成功", function () {
                    location.href = '/product/applicationForGiftSetting'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 校验必填项
    checkNeedInput: function () {
        if ($("#Name").val() == "") {
            Common.showErrorMsg("商品名称必填");
            return false;
        } else if ($("#TemplateId").val() == "") {
            Common.showErrorMsg("运费模板必选");
            return false;
        } else if ($("#ShopPrice").val() == "") {
            Common.showErrorMsg("一口价必填");
            return false;
        } else if ($("#Number").val() == "") {
            Common.showErrorMsg("商品库存必填");
            return false;
        } else if ($("#Limit").val() == "") {
            Common.showErrorMsg("警戒库存必填");
            return false;
        } else {
            return true;
        }
    },
    // 获取商品信息
    getProductInfo: function (PId) {
        var methodName = "/Distribution/AdminGetGiftBagInfo";
        var data = {
            GId: PId
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var result = data.Data;
                $("#Name").val(result.Name);
                $("#virtualSales").val(result.VirtualSales);
                $("#vPromoteCount").val(result.VPromoteCount);
                $('#small_icon').attr('data-src', result.ShowImgs.Url);
                $('#small_icon').attr('src', result.ShowImgs.FullUrl);
                $('#bearNumber').val(result.Amount)
                $('#ShopPrice').val(result.ShopPrice)
                $('#costPrice').val(result.CostPrice)

                $('#hasChooseBox').show();
                var name = ""

                var pidList = [];

                result.Products.forEach(function(item,index){
                    pidList.push(item.PId)
                    name += item.Name + '</br>'
                })

                name = name.substring(0, name.length - 1);

                ProductRelease.pIdList = pidList;

                $('.goods-name_final').html(name)

                if (result.Type == "0") {
                    $("#storeOutBox").hide()
                } else {
                    $("#storeOutBox").show()
                }
                $('#ProductCommodity').val(result.Type)
                $('#ProductCommodity').trigger('chosen:updated'); //更新选项


                // 状态
                var stateList = $("#State label");
                for (var i = 0; i < stateList.length; i++) {
                    if (stateList.eq(i).find("input").val() == result.State) {
                        stateList.eq(i).find("input").prop("checked", true);
                    }
                }


                // 标签
                // var labelList = $("#PlIdList label");
                // for (var i = 0; i < result.PlIdList.length; i++) {
                //     for (var j = 0; j < labelList.length; j++) {
                //         if (labelList.eq(j).find("input").val() == result.PlIdList[i]) {
                //             labelList.eq(j).find("input").prop("checked", true);
                //         }
                //     }
                // }

                var imgArr = [];
                console.log(result.Img)
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
                ue2.setContent(result.AfterSaleProtections);
                $('#AfterSaleProtections').val(result.AfterSaleProtections)
                //    sku规格表渲染 代表开启了规格

                $('#ProductBrand').trigger('chosen:updated'); //更新选项

                $('#TemplateId').trigger('chosen:updated'); //更新选项

                if (result.IsSku) {

                    ProductRelease.isOpen = true;

                    $('#specificationsBox').hide();
                    $('#skuRow').show();
                    //生成targetBox
                    result.AttributeList.forEach(function (item, indx) {
                        //创建targetBox
                        var box = `<div style="position: absolute; display: none;" id="${item.AttrId}" class="target_box">
                                <span valueid="47" class="sku19values specspan" style="padding:3px;">1.2m</span>
                            </div>`
                        $('body').append(box);
                    })
                    //代表初始化
                    ProductRelease.attributesList(true, result.AttrGroupId, result);


                }
                $('#ProductAttribute').val(result.AttrGroupId);
                $('#ProductAttribute').trigger('chosen:updated'); //更新选项
                // var drapBox = document.getElementById("drapBox");
                //
                // new Sortable(drapBox, { group: "omega" });

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

    //后台分类列表
    adminCategoryList: function () {
        //请求方法
        var methodName = "/category/AdminCategoryList";
        var data = {};
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
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
    initHandle: function () {
        //文件收缩  第一层级
        $('.layer_ul_1 .layer-1 .jia_collapse_one').click(function () {
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
        $('.layer_ul_2 .layer-2 .jia_collapse_two').click(function () {

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
    adminGetCategoryList: function () {
        var methodName = "/Material/AdminGetCategoryList";
        var data = {
            "Page": {
                "PageSize": 100,
                "PageIndex": 1
            }
        };
        SignRequest.set(methodName, data, function (data) {
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
    adminAddCategory: function (name) {
        var methodName = "/Material/AdminAddCategory";
        var data = {
            "Name": name
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功', function () {
                    $("#addGrouping").modal("hide");
                    ProductRelease.adminGetCategoryList();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //确认上传素材
    adminSaveMaterial: function (list) {
        var methodName = "/Material/AdminSaveMaterial";
        var data = {
            "TempImageList": list,
            "CategoryId": $(".a_b").attr("data-id")
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("上传成功!", function () {
                    // $('#uploaderPicModal').modal('hide')
                    // location.reload()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取素材库列表
    adminGetMaterialList: function () {
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
        SignRequest.set(methodName, data, function (data) {
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
    adminBatchDeleteMaterial: function (list) {
        var methodName = "/Material/AdminBatchDeleteMaterial";
        var data = {
            "Ids": list
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("删除成功!", function () {
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
                                onPageChange: function (num, type) {
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
    adminMoveMaterial: function (cateList, cateid) {
        var methodName = "/Material/AdminMoveMaterial";
        var data = {
            "Ids": cateList,
            "CategoryId": cateid,
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("移动成功!", function () {
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
                                onPageChange: function (num, type) {
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