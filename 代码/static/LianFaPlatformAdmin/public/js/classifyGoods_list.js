var ClassifyGoodsList = {
    //模板
    GoodsListTemplate:`<ul class="layer_ul_1">
                        {{each CategoryList as value i}}
                            <li class="layer-1">
                                <div class="contentBody">
                                    <div class="col-xs-1">
                                        <input data-id={{CategoryList[i].CateId}}  type="checkbox" name="check_classify_delete">
                                    </div>
                                    <div class="col-xs-1">{{CategoryList[i].CateId}}</div>
                                    <div class="col-xs-4">
                                        <div class="classify_one">
                                            <i class="icon-jia jia_collapse_one">
                                            <img src="../public/images/zhankai.png" alt="">  
                                            </i>
                                            <i class="iconfont icon-wenjianjia"></i>
                                            {{CategoryList[i].Name}}
                                        </div>
                                    </div>
                                    <div class="col-xs-2">
                                        <input type="number" class="order-disp" data-id={{CategoryList[i].CateId}} value="{{CategoryList[i].DisplayOrder}}">
                                    </div>
                                    <div class="col-xs-4">
                                        <a class="edit_classify" href="/classify/classifyEdit?id={{CategoryList[i].CateId}}">编辑</a>
                                        <span class="edit_dele_one" data-id={{CategoryList[i].CateId}}>删除</span>
                                    </div>
                                </div>
                            </li>
                           {{/each}}
                        </ul>`,

    init: function () {
        //获取后台分类列表
        ClassifyGoodsList.adminCategoryList();
        //点击删除按钮(第一层)
        $('body').on('click','.edit_dele_one',function(){
            var id = $(this).attr('data-id');
            var target = $(this);
            Common.confirmDialog("是否要删除此分类?",function(){
                ClassifyGoodsList.adminDelCategory(id,target,1)
            })

        })
        //点击删除按钮(第二层)
        $('body').on('click','.edit_dele_two',function(){
            var id = $(this).attr('data-id');
            var target = $(this);
            Common.confirmDialog("是否要删除此分类?",function(){
                ClassifyGoodsList.adminDelCategory(id,target,2)
            })
        })
        //点击删除按钮(第三层)
        $('body').on('click','.edit_dele_three',function(){
            var id = $(this).attr('data-id');
            var target = $(this);
            Common.confirmDialog("是否要删除此分类?",function(){
                ClassifyGoodsList.adminDelCategory(id,target,3)
            })
        });

        //排序修改
        $('body').on('change','.order-disp',function(){
            var id = $(this).attr('data-id');
            var num = $(this).val();
            console.log(num)
            ClassifyGoodsList.adminCategoryInfo(id,num);

        })
        //批量删除
        $('body').on('click','#all_delte_box',function(){
            var list = [];
            $('input[name=check_classify_delete]').each(function(index,item){
                if(this.checked){
                    // console.log($(item).attr('data-id'))
                    list.push($(item).attr('data-id'))
                }
            })
            ClassifyGoodsList.adminBatchDelCategory(list)
        })

    },
    //后台分类列表
    adminCategoryList:function(){
        //请求方法
        var methodName = "/category/AdminCategoryList";
        var data = {

        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(ClassifyGoodsList.GoodsListTemplate);
                var html = render(data.Data);
                $('#table_content_classify').html(html);
                ClassifyGoodsList.initHandle();
                ClassifyGoodsList.selectHandle(data);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //选中事件
    selectHandle:function(data){
        //如果存在id
        if(Common.getUrlParam('id')){
            var id = Common.getUrlParam('id');
            data.Data.CategoryList.forEach(function(item,index){

                //获取选中的项
                if(item.CateId == id){
                    var layer = item.Layer;
                    //如果是第一级的不做处理
                    if(layer == 1){

                    }else if(layer == 2){
                        //如果是二级的话，一级展开
                        $('input[type="checkbox"]').each(function(index,item1){
                            console.log(item1)
                            //找到修改过的项
                            if($(item1).attr('data-id') == id ){
                                $(this).parents('.layer_ul_2').show().siblings('.layer_ul_2').show();
                                $(this).parents('.layer-1').find('.jia_collapse_one').find('img').attr('src', '../public/images/zhankai.png');
                            }
                        })

                    }else if(layer == 3){
                        //如果是三级的话，一级展开，二级展开
                        $('input[type="checkbox"]').each(function(index,item1){
                            console.log(item1)
                            //找到修改过的项
                            if($(item1).attr('data-id') == id ){
                                $(this).parents('.layer_ul_2').show().siblings('.layer_ul_2').show();
                                $(this).parents('.layer_ul_3').show().siblings('.layer_ul_3').show();
                                $(this).parents('.layer_ul_2').find('.jia_collapse_two').find('img').attr('src', '../public/images/zhankai.png');
                                $(this).parents('.layer-1').find('.jia_collapse_one').find('img').attr('src', '../public/images/zhankai.png');
                            }
                        })
                    }
                }
            })
        }
    },
    //初始化事件
    initHandle:function(){
        //文件收缩  第一层级
        $('.layer_ul_1 .layer-1 .jia_collapse_one').click(function () {
            //$(this).parents('.contentBody').siblings('.layer_ul_2').slideToggle();
            var display_state = $(this).parents('.contentBody').siblings('.layer_ul_2').css('display');//所以要用行内样式控制

            if (display_state == 'none') {
                $(this).parents('.contentBody').siblings('.layer_ul_2').slideDown();
                $(this).find('img').attr('src', '../public/images/zhankai.png');
            }
            else {
                $(this).parents('.contentBody').siblings('.layer_ul_2').slideUp();
                $(this).find('img').attr('src', '../public/images/suoqi.png');
            }
        });

        //文件收缩 第二层级
        $('.layer_ul_2 .layer-2 .jia_collapse_two').click(function () {

            var display_state = $(this).parents('.contentBody').siblings('.layer_ul_3').css('display');//所以要用行内样式控制
            if (display_state == 'none') {
                $(this).parents('.contentBody').siblings('.layer_ul_3').slideDown();
                $(this).find('img').attr('src', '../public/images/zhankai.png');
            }
            else {
                $(this).parents('.contentBody').siblings('.layer_ul_3').slideUp();
                $(this).find('img').attr('src', '../public/images/suoqi.png');
            }
        });
        //全选效果
        $('#check_classify_delete_all').click(function () {
            var check_is = this.checked;
            if (this.checked) {
                $('.layer_ul_1 input[type="checkbox"]').each(function (index, val) {
                    this.checked = true;
                });
            }
            else {
                $('.layer_ul_1 input[type="checkbox"]').each(function (index, val) {
                    this.checked = false;
                });
            }
        });
        //全选删除
        $('.all_delte_box').click(function () {
            if ($('#check_classify_delete_all')[0].checked) {

                Common.confirmDialog('本操作会把所有数据删除', function () {
                    $('#table_content_classify').empty();
                    swal("删除成功", "", "success");
                }, '提示');
            }
        });
        //单个删除(第一层级li)
        $('#table_content_classify').on('click', '.edit_dele_one', function () {

            var parObj = $(this).parents('.layer-1');//第一层级父辈元素li
            var input_checkbox = parObj.children('.contentBody').find('input[type="checkbox"]');

            if (input_checkbox[0].checked) {
                Common.confirmDialog('本操作会删除本类别及下属子类别', function () {
                    parObj.remove();
                    swal("删除成功", "", "success");
                }, '提示');
            }

        });
        //单个删除(第二层级li)
        $('#table_content_classify').on('click', '.edit_dele_two', function () {
            var parObj = $(this).parents('.layer-2');//第二层级父辈元素li
            var input_checkbox = parObj.children('.contentBody').find('input[type="checkbox"]');
            if (input_checkbox[0].checked) {
                Common.confirmDialog('本操作会删除本类别及下属子类别', function () {
                    parObj.remove();
                    swal("删除成功", "", "success");
                }, '提示');
            }
        });
        //单个删除(第三层级li)
        $('#table_content_classify').on('click', '.edit_dele_three', function () {
            var parObj = $(this).parents('.layer-3');//第三层级父辈元素li
            var input_checkbox = parObj.children('.contentBody').find('input[type="checkbox"]');
            if (input_checkbox[0].checked) {
                Common.confirmDialog('本操作会删除本类别及下属子类别', function () {
                    parObj.remove();
                    swal("删除成功", "", "success");
                }, '提示');
            }
        });

        //点击其中一个input，当判断它为空时候，取消全选效果
        $('.main_list_content').on('click', '.layer_ul_1 input[type="checkbox"]', function () {
            if (!this.checked) {
                $('#check_classify_delete_all')[0].checked = false;
            }
        });
    },
    //后台删除分类
    adminDelCategory:function(id,target,num){
        //请求方法
        var methodName = "/category/AdminBatchDelCategory";
        var data = {
            "CateIdList": [id]
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {

                Common.showSuccessMsg("删除成功!",function(){
                    if(num == '1'){
                        console.log("第一层")
                        target.parents('.layer-1').remove();
                    }else if(num == '2'){
                        console.log("第二层")
                        target.parents('.layer_ul_2').remove();
                    }else if(num == '3'){
                        console.log("第三层")
                        target.parents('.layer_ul_3').remove();
                    }
                })


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台分类信息
    adminCategoryInfo:function(id,num){
        //请求方法
        var methodName = "/category/AdminCategoryInfo";
        var data = {
            "CateId": id
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                localStorage.setItem('classify_name',data.Data.CategoryInfo.Name);
                localStorage.setItem('BigIcon',data.Data.CategoryInfo.BigIcon);
                localStorage.setItem('SmallIcon',data.Data.CategoryInfo.SmallIcon);
                localStorage.setItem('Cate',data.Data.CategoryInfo.ParentId);
                ClassifyGoodsList.adminEditCategory(id,num)
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台编辑分类
    adminEditCategory:function(id,num){
        //请求方法
        var methodName = "/category/AdminEditCategory";
        var data = {
            "CateId":id ,
            "Name": localStorage.getItem('classify_name'),
            "ParentId": localStorage.getItem('Cate'),
            "SmallIcon": localStorage.getItem('SmallIcon'),
            "BigIcon": localStorage.getItem('BigIcon'),
            "DisplayOrder":num,
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功',function(){

                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台批量删除接口
    adminBatchDelCategory:function(list){
        //请求方法
        var methodName = "/category/AdminBatchDelCategory";
        var data = {
            "CateIdList":list
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功!',function(){
                    location.reload()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
};


$(function () {

    ClassifyGoodsList.init();






});