$(function () {
    addFreightTemplate.init();
})


var addFreightTemplate = {

    IsDefault:"",
    allselect:"",
    allselectthird:"",
    //把点击编辑的那个元素存起来
    target:"",
    //右边已选中的一级菜单
    selectOneList:[],
    //判断一级里面是否存在该二级菜单
    hasSecond:"",
    //判断右边是否存在该一级菜单
    hasOne:'',
    //判断点击二级菜单的时候，一级已选中
    secondFlag:"",
    //判断点击三级菜单的时候，二级已选中
    thirdFlag:"",
    province_template: `        
                                   <option value="0">所有省</option>
                                   {{each RegionsList as value i}}
                                   <option data-id="{{RegionsList[i].RegionId}}">{{RegionsList[i].Name}}</option>
                                   {{/each}}

`,
    city_template: `

                                    <option value="0">所有市</option>
                                     {{each RegionsList as value i}}
                                    <option data-id="{{RegionsList[i].RegionId}}">{{RegionsList[i].Name}}</option>
                                       {{/each}}
`,
    area_template: `
                                    
                                    <option value="0">所有区/县</option>
                                    {{each RegionsList as value i}}
                                    <option data-id="{{RegionsList[i].RegionId}}">{{RegionsList[i].Name}}</option>
                                    {{/each}}
`,
    street_template: `
                                    <option selected="selected" value="0">请选择街道</option>
                                    {{each RegionsList as value i}}
                                    <option data-id="{{RegionsList[i].RegionId}}">{{RegionsList[i].Name}}</option>
                                    {{/each}}
`,


    init: function () {
        // 初始化switch开关控件
        addFreightTemplate.initSwitch();

        //根据是否发送信息来显隐信息文本框
        $("input:radio[name='send']").on("ifChecked",function () {
            if($('input:radio[name="send"]:checked').val()=="false"){
                $("#message").attr("disabled",true);
            }
            if($('input:radio[name="send"]:checked').val()=="true"){
                $("#message").attr("disabled",false);
            }
        });//省改变时
        $('body').on('change', '#province_box', function () {
            var id = $('#province_box option:selected').attr('data-id');
            addFreightTemplate.provinceId = id;
            addFreightTemplate.cityId = 0;
            addFreightTemplate.regionid = 0;
            addFreightTemplate.cityHandle(id,0);
            $('#city_box').val(0)
            $('#area_box').html('<option selected="selected" value="0">所有区/县</option>')
            if ($('#province_box option:selected').val() == "0") {
                addFreightTemplate.provinceId = 0;
            } else {

            }
        })
        //市改变时
        $('body').on('change', '#city_box', function () {
            var id = $('#city_box option:selected').attr('data-id');
            addFreightTemplate.cityId = id;
            addFreightTemplate.regionid = 0;
            addFreightTemplate.areaHandle(id);
            $('#area_box').val(0)
            $('#street_box').html('<option selected="selected" value="0">请选择街道</option>')
        })
        //区改变时
        $('body').on('change', '#area_box', function () {
            var id = $('#area_box option:selected').attr('data-id');
            // addFreightTemplate.streetHandle(id);
            addFreightTemplate.regionid = id;
            $('#street_box').val(0)
        })
        //判断是以哪种运送方式运送
        $("input:radio[name='Valuation']").on("ifChecked",function (event) {
            var type = $(this).val();
            if(type=="1"){
                $('.changeText').html('件')
                $('.nofloat[name="ValuationUnit"]').html('件')
                $('.nofloat[name="ValuationUnitDesc"]').html('件')
                $('#PercentageBox,#freightBox').hide();
                $("#regionFreight,#addCityFreight,#formatBox").show();
            }else if(type=="2"){
                $('.changeText').html('kg')
                $('.nofloat[name="ValuationUnit"]').html('kg')
                $('.nofloat[name="ValuationUnitDesc"]').html('重')
                $('#PercentageBox,#freightBox').hide();
                $("#regionFreight,#addCityFreight,#formatBox").show();
            }else if(type=="3"){
                $('.changeText').html('m<sup>3</sup>')
                $('.nofloat[name="ValuationUnit"]').html('m<sup>3</sup>')
                $('.nofloat[name="ValuationUnitDesc"]').html('体积');
                $('#PercentageBox,#freightBox').hide();
                $("#regionFreight,#addCityFreight,#formatBox").show();
            }else if(type=="4"){
                $('#PercentageBox').show();
                $("#regionFreight,#addCityFreight,#formatBox,#freightBox").hide();
            }else if(type=="5"){
                $("#regionFreight,#addCityFreight,#formatBox,#PercentageBox").hide();
                $('#freightBox').show();
            }
        });
        //点击新增指定城市运费
        $('body').on('click','#addCityFreight',function(){
            var one = $("#onNumberone").val()||0;
            var two = $("#onNumbertwo").val()||0;
            var three = $("#onNumberthree").val()||0;
            var four = $("#onNumberfour").val()||0;
            var five = $("#onNumberfive").val()||0;
            var html = `<tr><td><a href="javascript:void(0);" class="exit-area">编辑</a><div class="area-group"><p data-pid="0" data-cid="0" data-aid="0">未添加地区</p></div></td><td><input type="text" class="form-control input-xs" name="one" value="`+one+`"></td><td><input type="text" name="two" class="form-control input-xs" value="`+two+`"></td><td><input type="text "name="three" class="form-control input-xs" value="`+three+`"></td><td><input type="text" name="four" class="form-control input-xs" value="`+four+`"></td><td><span class="btn-a"><a href="javascript:void(0);" name="delContent">删除</a></span></td></tr>`
            $('#regionFreight').append(html)
        });
        //点击新增指定城市包邮
        $('body').on('click','#addFree',function(){
            var one = $("#onNumberone").val()||0;
            var two = $("#onNumbertwo").val()||0;
            var three = $("#onNumberthree").val()||0;
            var four = $("#onNumberfour").val()||0;
            var five = $("#onNumberfive").val()||0;
            var html = `<tr><td><a href="javascript:void(0);" class="exit-area">编辑</a><div class="area-group"><p data-pid="0" data-cid="0" data-aid="0">未添加地区</p></div></td><td><input type="text" class="form-control input-xs" name="one" value="`+one+`"></td><td><input type="text" name="two" class="form-control input-xs" value="`+two+`"></td><td><input type="text "name="three" class="form-control input-xs" value="`+three+`"></td><td><input type="text" name="four" class="form-control input-xs" value="`+four+`"></td><td><span class="btn-a"><a href="javascript:void(0);" name="delContent">删除</a></span></td></tr>`
            $('#regionFree').append(html)
        });
        //点击是否包邮单选框
        $("input:radio[name='packageMail']").on("ifChecked",function (event) {
            var type = $(this).val();
            if(type=="1"){
                $('.isshow').show();
            }else if(type=="2"){
                $('.isshow').hide();
            }
        });
        //指定城市包邮里面的select改变时
        $('body').on('change','.setfreeshipping',function(){
            if($(this).val() == '1'){
                var html = `满<input type="text" value="" class="form-control mlr">件包邮`;
                $(this).siblings('.free-contion').html(html)
            }else if($(this).val() == '2'){
                var html = `满<input type="text" value="" class="form-control mlr">元包邮`;
                $(this).siblings('.free-contion').html(html)
            }else if($(this).val() == '3'){
                var html = `满<input type="text" value="" class="form-control mlr">件，<input type="text" value="" class="form-control"> 元包邮`;
                $(this).siblings('.free-contion').html(html)
            }
        });
        //点击编辑出现选择地址弹窗
        $('body').on('click','.exit-area',function(){
            console.log('点击了编辑');
            var target = $(this).siblings().find("p");
            addFreightTemplate.target = target
            var pid = target.attr("data-pid");
            var cid = target.attr("data-cid");
            var aid = target.attr("data-aid");
            //获取省市区
            addFreightTemplate.provinceHandle(pid);
            addFreightTemplate.cityHandle(pid,cid);
            addFreightTemplate.areaHandle(cid,aid);
            $('.area-modal-wrap').show();
        });
        //关闭弹窗
        $('body').on('click','.js-modal-close',function(){
            $('.area-modal-wrap').hide();
        });
        //弹窗里面城市加号点击
        $('body').on('click','.area-editor-ladder-toggle',function(ev){
            var oEvent = ev || event;
            oEvent.stopPropagation();
            $(this).parents('.area-editor-list-title').next('.area-editor-list').slideToggle();
            $(this).parents('.area-editor-list-title').children().toggle();
        });
        //选中地址
        $('body').on('click','.js-area-editor-notused .js-ladder-select',function(){
            var targetLi = $(this).parent('.area-editor-list-title').parent('li');
            //原本是选中状态的时候
            if(targetLi.hasClass('selectBackground')){
                //判断点的是一级菜单还是二级菜单
                if(targetLi.parent('.area-editor-list').hasClass('area-editor-depth0')){
                    //如果是一级菜单的话,当前项移除选中样式并且儿子的li一并移除
                    targetLi.removeClass('selectBackground');
                    targetLi.children('.area-editor-depth1').find('li').removeClass('selectBackground')
                }else if(targetLi.parent('.area-editor-list').hasClass('area-editor-depth1')){
                    //如果点击的是二级菜单当前项移除选中样式
                    targetLi.removeClass('selectBackground');
                    targetLi.children('.area-editor-depth2').find('li').removeClass('selectBackground');
                    targetLi.parent('.area-editor-depth1').parent('li').removeClass('selectBackground');
                }else if(targetLi.parent('.area-editor-list').hasClass('area-editor-depth2')){
                    //点击三级菜单的时候
                    targetLi.removeClass('selectBackground');
                    targetLi.parent('.area-editor-depth2').parent('li').removeClass('selectBackground');
                    targetLi.parent('.area-editor-depth2').parent('li').parent('.area-editor-depth1').parent('li').removeClass('selectBackground');
                }

                //原本是未选中状态的时候
            }else{
                if(targetLi.parent('.area-editor-list').hasClass('area-editor-depth0')){
                    //点击一级菜单的时候给旗下所有的li添加选中状态
                    targetLi.addClass('selectBackground');
                    targetLi.find('li').addClass('selectBackground')
                }else if(targetLi.parent('.area-editor-list').hasClass('area-editor-depth1')){
                    //点击二级菜单的时候
                    targetLi.addClass('selectBackground');
                    targetLi.children('.area-editor-depth2').find('li').addClass('selectBackground')
                    addFreightTemplate.allselect = true;
                    targetLi.parent('.area-editor-depth1').find('li').each(function(index,item){
                        if(!$(item).hasClass('selectBackground')){
                            addFreightTemplate.allselect = false;
                        }
                    })
                    console.log(addFreightTemplate.allselect)
                    if(addFreightTemplate.allselect){
                        targetLi.parent('.area-editor-depth1').parent('li').addClass('selectBackground')
                    }

                }else if(targetLi.parent('.area-editor-list').hasClass('area-editor-depth2')){
                    //点击三级菜单的时候
                    targetLi.addClass('selectBackground');
                    //如果全选就给第二级添加选中样式
                    addFreightTemplate.allselectthird = true;
                    targetLi.parent('.area-editor-depth2').find('li').each(function(index,item){
                        if(!$(item).hasClass('selectBackground')){
                            addFreightTemplate.allselectthird = false;
                        }
                    })
                    if(addFreightTemplate.allselectthird){
                        targetLi.parent('.area-editor-depth2').parent('li').addClass('selectBackground')
                    }
                    //如果第三级全选并且第二级也全选的情况下，给第一级增加选中样式
                    addFreightTemplate.allselect = true;
                    targetLi.parents('.area-editor-depth1').find('li').each(function(index,item){
                        if(!$(item).hasClass('selectBackground')){
                            addFreightTemplate.allselect = false;
                        }
                    })
                    if(addFreightTemplate.allselect){
                        targetLi.parents('.area-editor-depth1').parent('li').addClass('selectBackground')
                    }
                }

            }

        });
        //点击弹窗里面的添加按钮
        $('body').on('click','#addAddressBtn',function(){
            //首先获取右边有哪几个一级菜单
            addFreightTemplate.selectOneList = [];
            $('.js-area-editor-used').find('.area-editor-depth0').children('li').each(function(index,item){
                addFreightTemplate.selectOneList.push($(item).attr('data-id'))
            })

            $('.js-area-editor-notused').find('li').each(function(index,item){
                //区别出是哪一级的li
                if($(item).parent('.area-editor-list').hasClass('area-editor-depth0')){
                    //第一级
                    //如果有选中的话
                    if($(item).hasClass('selectBackground')){
                        //右边的个数不小于1时
                        if(addFreightTemplate.selectOneList.length>0){
                            addFreightTemplate.hasOne = false;
                            addFreightTemplate.selectOneList.forEach(function(item1,index1){
                                //如果重复的话
                                if(item1 == $(item).attr('data-id')){
                                    console.log('此一级菜单已存在,不能重复添加')
                                    addFreightTemplate.hasOne = true;
                                }
                            })
                            if(!addFreightTemplate.hasOne){
                                //克隆一个新的li，然后添加删除按钮
                                var html = $(item).clone(true);
                                html.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                $('.js-area-editor-used').find('.area-editor-depth0').append(html);
                            }else{
                                console.log('该一级菜单没有重复，可以添加进来')
                            }

                        }else{
                            //克隆一个新的li，然后添加删除按钮
                            var html = $(item).clone(true);
                            html.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                            $('.js-area-editor-used').find('.area-editor-depth0').append(html);
                        }



                    }
                }
                if($(item).parent('.area-editor-list').hasClass('area-editor-depth1')){
                    //如果当前项的一级菜单是选中状态的话,代表全部选中,就不需要以下操作了
                    //获取它一级菜单的id
                    var parentId =$(item).attr('data-id').split('-')[0];
                    var secondId = $(item).attr('data-id');
                    if($('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').hasClass('selectBackground')){

                        //并且其下面的li都选中的情况下
                        addFreightTemplate.secondFlag = false;
                        $('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('li').each(function(index1,item1){
                            if(!$(item1).hasClass('selectBackground')){
                                addFreightTemplate.secondFlag = true;
                            }
                        })
                        if(addFreightTemplate.secondFlag){
                            console.log('当前项的一级菜单已选中，以下操作可省略');
                        }else {
                            if($('.js-area-editor-used').find('.area-editor-depth0').find('li[data-id="'+secondId+'"]').length>0){
                                console.log('当前子集已存在')
                            }else{
                                var htmltwo = $('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').children('li[data-id="'+secondId+'"]').clone(true);
                                htmltwo.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                $('.js-area-editor-used').find('.area-editor-depth0').find('li[data-id="'+parentId +'"]').find('.area-editor-depth1').append(htmltwo);
                            }

                        }

                    }else{
                        //第二级
                        //如果有选中的话
                        if($(item).hasClass('selectBackground')){
                            //有两个情况
                            //获取它一级菜单的id
                            var parentId =$(item).attr('data-id').split('-')[0];
                            //获取当前选中li的id值
                            var id = $(item).attr('data-id');
                            if(addFreightTemplate.selectOneList.length>0){
                                addFreightTemplate.selectOneList.forEach(function(item1,index1){
                                    //第一种是右边没有其父级
                                    if(item1 != parentId){
                                        console.log('右边没有父级');
                                        console.log(id);
                                        console.log('父id'+parentId);
                                        //就要从左边克隆过去
                                        var html = $('.js-area-editor-notused').find('.area-editor-depth0').find('li[data-id="'+parentId +'"]').clone(true);
                                        //把内容清空
                                        html.html("");
                                        //然后把当前点击的二级菜单append进去
                                        html.append($(item).html());
                                    }else{
                                        //如果右边已经存在该父级的情况下，先循环遍历看里面是否存在该子集
                                        console.log('右边已经存在该父级');
                                        console.log(id)
                                        console.log('父id'+parentId);
                                        //判断该一级菜单是否存在该二级菜单,默认不存在
                                        addFreightTemplate.hasSecond = false;
                                        $('.js-area-editor-used').find('.area-editor-depth0').find('li[data-id="'+parentId +'"]').find('li').each(function(index2,item2){
                                            if($(item2).attr('data-id') == id){
                                                console.log($(item2))
                                                console.log('右边已经存在该子集')
                                                //存在就改变为true
                                                addFreightTemplate.hasSecond = true;
                                            }
                                        })
                                        if(!addFreightTemplate.hasSecond){
                                            //不存在就添加进去
                                            //克隆一个新的li，然后添加删除按钮
                                            var html = $(item).clone(true);
                                            html.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                            $('.js-area-editor-used').find('.area-editor-depth0').find('li[data-id="'+parentId +'"]').find('.area-editor-depth1').append(html)
                                        }else{
                                            console.log('该一级菜单不存在该二级菜单，可以添加进来')
                                        }


                                    }

                                })
                            }else{
                                console.log('右边没有父级');
                                console.log(id);
                                console.log('父id'+parentId);
                                //就要从左边克隆过去
                                var html = $('.js-area-editor-notused').find('.area-editor-depth0').find('li[data-id="'+parentId +'"]').clone(true);
                                console.log(html.attr('data-id'));
                                //添加删除按钮
                                html.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                //把内容清空
                                html.find('.area-editor-depth1').html('');
                                //为二级菜单添加删除按钮
                                var secondHtml = $(item).clone(true);
                                secondHtml.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                //然后把当前点击的二级菜单append进一级菜单那里
                                html.find('.area-editor-depth1').append(secondHtml);
                                //然后把整个菜单添加进去
                                $('.js-area-editor-used').find('.area-editor-depth0').append(html);


                            }



                        }
                    }


                }
                if($(item).parent('.area-editor-list').hasClass('area-editor-depth2')){
                    //第三级
                    //如果二级菜单是选中的话,代表全部选中,就不需要以下操作了
                    //一级id
                    var parentId = $(item).attr('data-id').split('-')[0];
                    //二级id
                    var secondId = parentId + '-' + $(item).attr('data-id').split('-')[1];
                    //当前的三级id
                    var third = $(item).attr('data-id');

                    if($('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').children('li[data-id="'+secondId+'"]').hasClass('selectBackground')){
                        addFreightTemplate.thirdFlag = false;
                        $('.js-area-editor-used').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').children('li[data-id="'+secondId+'"]').find('.area-editor-depth1').children('li').each(function(index2,item2){
                            if(!$(item2).hasClass('selectBackground')){
                                addFreightTemplate.thirdFlag = true;
                            }
                        })
                        if(addFreightTemplate.thirdFlag){
                            console.log('当前项的二级菜单已选中，以下操作可省略')
                        }else {
                            if($('.js-area-editor-used').find('.area-editor-depth1').find('li[data-id="'+secondId+'"]').find('.area-editor-depth2').find('li[data-id="'+third+'"]').length>0){
                                console.log('当前三级子集已存在')
                            }else{
                                //把三级菜单克隆
                                var htmlthird = $('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').children('li[data-id="'+secondId+'"]').find('.area-editor-depth2').children('li[data-id="'+third+'"]').clone(true);
                                htmlthird.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                $('.js-area-editor-used').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').children('li[data-id="'+secondId+'"]').find('.area-editor-depth2').append(htmlthird);
                            }

                        }



                    }else{
                        //第三级
                        //如果有选中的话
                        if($(item).hasClass('selectBackground')){
                            //如果右边存在该三级菜单的话

                                if($('.js-area-editor-used').find('.area-editor-depth0').find('li[data-id="'+parentId+'"]').length>0 && $('.js-area-editor-used').find('.area-editor-depth0').find('li[data-id="'+secondId+'"]').length>0 ){
                                    //存在右边有此一级id跟二级，但是三级的不存在的情况
                                    if($('.js-area-editor-used').find('.area-editor-depth0').find('li[data-id="'+third+'"]').length>0){
                                        console.log("已存在该三级吃菜单")
                                    }else{
                                        //把三级菜单克隆
                                        var htmlthird = $('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').children('li[data-id="'+secondId+'"]').find('.area-editor-depth2').children('li[data-id="'+third+'"]').clone(true);
                                        htmlthird.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                        $('.js-area-editor-used').find('.area-editor-depth1').find('li[data-id="'+secondId+'"]').find('.area-editor-depth2').append(htmlthird);

                                    }
                                }else{
                                    //存在一级但二级不一样
                                    if($('.js-area-editor-used').find('.area-editor-depth0').find('li[data-id="'+parentId+'"]').length>0){
                                        //把二级菜单克隆
                                        var htmltwo = $('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').children('li[data-id="'+secondId+'"]').clone(true);
                                        htmltwo.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                        //把三级菜单克隆
                                        var htmlthird = $('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').children('li[data-id="'+secondId+'"]').find('.area-editor-depth2').children('li[data-id="'+third+'"]').clone(true);
                                        htmlthird.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                        htmltwo.find('.area-editor-depth2').html("");
                                        htmltwo.find('.area-editor-depth2').append(htmlthird);
                                        $('.js-area-editor-used').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').append(htmltwo)
                                    }else{
                                        //把第一级菜单克隆
                                        var htmlone = $('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').clone(true);
                                        htmlone.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                        //把二级菜单克隆
                                        var htmltwo = $('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').children('li[data-id="'+secondId+'"]').clone(true);
                                        htmltwo.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                        //把三级菜单克隆
                                        var htmlthird = $('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+parentId+'"]').find('.area-editor-depth1').children('li[data-id="'+secondId+'"]').find('.area-editor-depth2').children('li[data-id="'+third+'"]').clone(true);
                                        htmlthird.find('.js-ladder-select').append(`<div class="area-editor-remove-btn js-ladder-remove">×</div>`);
                                        htmlone.find('.area-editor-depth1').html("");
                                        htmltwo.find('.area-editor-depth2').html("");
                                        htmltwo.find('.area-editor-depth2').append(htmlthird);
                                        htmlone.find('.area-editor-depth1').append(htmltwo);
                                        $('.js-area-editor-used').find('.area-editor-depth0').append(htmlone)
                                    }



                                }

                                



                        }
                    }




                }


            })
            //为所有的li清除选中样式
            $('.js-area-editor-used').find('li').removeClass('selectBackground');
        });
        //点击弹窗里面的确认
        $('body').on('click','#sureBtn',function(){
            //获取一级菜单总个数的id数组
            var provinceText =  $("#province_box").val() ==0?"所有省":$("#province_box").val();
            var provinceId = $("#province_box option:selected").attr("data-id");
            var cityText =  $("#city_box").val()==0?"所有市":$("#city_box").val();
            var cityId = !$("#city_box option:selected").attr("data-id")?provinceId:$("#city_box option:selected").attr("data-id");
            var areaText =  $("#area_box").val()==0?"所有区/县":$("#area_box").val();
            var areaId = !$("#area_box option:selected").attr("data-id")?cityId:$("#area_box option:selected").attr("data-id");
            var toP = addFreightTemplate.target;
            $(toP).html(provinceText+" , "+cityText+" , "+areaText);
            $(toP).attr({"data-Pid":provinceId,"data-Cid":cityId,"data-Aid":areaId});
            
            $('.area-modal-wrap').hide();
            // var oneList = [];
            // $('.js-area-editor-used').find('.area-editor-depth0').children('li').each(function(index,item){
            //     oneList.push($(item).attr('data-id'))
            // })
            // //检查其下面的子集是否齐全
            // oneList.forEach(function(item,index){
            //     $('.js-area-editor-notused').find('.area-editor-depth0').children('li[data-id="'+item+'"]').find('.area-editor-depth1').children('li').each(function(index1,item1){

            //     })
            // })
        })
        //点击地址里面的删除
        $('body').on('click','.area-editor-remove-btn',function(){
            //删除当前项
            $(this).parents('.area-editor-list-title').parent('li').remove();

        })
        //点击删除
        $('body').on('click','.btn-a',function(){
            $(this).parents('tr').remove();
        })
        $('input').iCheck({
            checkboxClass: 'icheckbox_flat-blue',
            radioClass: 'iradio_flat-blue',
            increaseArea: '20%' // optional
        });
        $('.stuCheckBox').on('ifChecked', function (event) { //ifCreated 事件应该在插件初始化之前绑定
            // MemberGrade.IsDefault = 1;
        });
        $('.stuCheckBox2').on('ifChecked', function (event) { //ifCreated 事件应该在插件初始化之前绑定
            // MemberGrade.IsDefault = 0;
        });
        //点击添加
        $('body').on('click','#submitBtn',function(){
            var list =[];
             $("#regionFreight tbody").find("tr").each(function(){
                 var data = {}
                var aid = $(this).find("p").attr("data-aid")||0;
                var one = $(this).find("input[name='one']").val()||0;
                var two = $(this).find("input[name='two']").val()||0;
                var three = $(this).find("input[name='three']").val()||0;
                var four = $(this).find("input[name='four']").val()||0;
                var five = $(this).find("input[name='five']").val()||0;
                // console.log(aid,one,two,three,four,five)
                
                var data = {
                        "DefaultNumber": one,
                        "AddNumber": three,
                        "Price": two,
                        "AddPrice": four,
                        "MinPrice":five,
                        "RegionIdList": [
                            aid
                        ]
                    }
                    list.push(data)
            });
            addFreightTemplate.adminAddTemplates(list)
        })


    },
    //调用获取省列表
    provinceHandle: function (pid) {
        var methodName = "/admin/regions/AdminProvinceList";
        var data = {};
        SignRequest.setNoAdmin(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(addFreightTemplate.province_template);
                var html = render(data.Data);
                $("#province_box").html(html);
                $("#province_box").find("option").each(function(e){
                    if($(this).attr("data-id")==pid){
                        $(this).attr("selected","selected")
                    }
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //调用获取市列表
    cityHandle: function (id,cid) {
        var methodName = "/admin/regions/AdminCityList";
        var data = {
            "Type": 1,
            "ParentId": id
        };
        SignRequest.setNoAdmin(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(addFreightTemplate.city_template);
                var html = render(data.Data);
                $("#city_box").html(html);
                $("#city_box").find("option").each(function(e){
                    if($(this).attr("data-id")==cid){
                        $(this).attr("selected","selected")
                    }
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //调用获取区列表
    areaHandle: function (id,aid) {
        var methodName = "/Tool/MunicipalDistrictList";
        var data = {
            "Type": 1,
            "ParentId": id
        };
        SignRequest.setNoAdmin(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(addFreightTemplate.area_template);
                var html = render(data.Data);
                $("#area_box").html(html);
                $("#area_box").find("option").each(function(e){
                    if($(this).attr("data-id")==aid){
                        $(this).attr("selected","selected")
                    }
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //添加运费模板接口
    adminAddTemplates:function(list){
        var methodName = "/templates/AdminAddTemplates";
        var one = $("#onNumberone").val()||0;
        var two = $("#onNumbertwo").val()||0;
        var three = $("#onNumberthree").val()||0;
        var four = $("#onNumberfour").val()||0;
        var five = $("#onNumberfive").val()||0;
        var data = {
            "TemplateName": $('#templateName').val(),
            "IsfreeShipping": true,
            "ValuationMethod": $('input:radio[name="Valuation"]:checked').val(),
            "DefaultNumber": one,
            "Price": two,
            "AddNumber": three,
            "AddPrice": four,
            "ShippingRegionsGroupsList": list,
            "ShippingAppointRegionsList": [],
            "StartPrice":$('input:radio[name="Valuation"]:checked').val() == "4" ? $('#one_num3').val() : "0",
            "EndPrice": $('input:radio[name="Valuation"]:checked').val() == "4" ? $('#one_num4').val() : "0",
            "Freight": $('input:radio[name="Valuation"]:checked').val() == "4" ? $('#one_num5').val() : "0",
            "WithFree":$('input:radio[name="Valuation"]:checked').val() == "4" ? $('#one_num6').val() : $('#two_num3').val()||0,
            "SPrice": $('input:radio[name="Valuation"]:checked').val() == "4" ? $('#one_num1').val() : $('#two_num1').val()||0 ,
            "SFreight": $('input:radio[name="Valuation"]:checked').val() == "4" ? $('#one_num2').val() :$('#two_num2').val()||0,
            "IsAppoint": true,
            "MinPrice": five
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功',function(){
                    location.href = '/product/freightTemplate'
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 初始化switch控件
    initSwitch:function () {
        $(".switch").on("click",function () {
            if($(this).hasClass("switch-on")){
                $(this).removeClass("switch-on");
                $('#regionFree').hide();
                $('#addFreeBox').hide()
            }
            else {
                $(this).addClass("switch-on");
                $('#regionFree').show();
                $('#addFreeBox').show()
            }
        })
    }

}
