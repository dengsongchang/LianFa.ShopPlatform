$(function () {
    StoreAdd.init();
})

var StoreAdd = {
    //区域id
    regionid: "",
    //lat
    lat: '',
    //lng
    lng: '',
    //店长名称
    NickName: "",
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
    time_template: `
                            {{each data as value i}}
                                
                                <div class="time-item">
                                         {{data[i].StartTime}}~{{data[i].EndTime}}
                                    <img class="delectBtn" src="/public/images/delect.png" alt="">
                                </div>
                                
                            {{/each}}
    `,
    chair_template: `
                                    <option selected="selected" value="0" data-id="0">请选择按摩椅</option>
                                    {{each MassageChairList as value i}}
                                    <option data-id="{{MassageChairList[i].MCId}}">{{MassageChairList[i].MCSn}}</option>
                                    {{/each}}
    `,
    shopkeeper_template: `
                                <option selected="selected" value="0" data-id="0">请选择店长</option>
                                    {{each StoreManagerList as value i}}
                                    <option data-id="{{StoreManagerList[i].UId}}">{{StoreManagerList[i].NickName}}</option>
                                    {{/each}}
    `,


    init: function () {
//初始化预览插件
        var viewer1 = new Viewer(document.getElementById('dowebok'),{
            url: 'data-original'
        });
        var viewer2 = new Viewer(document.getElementById('dowebok1'),{
            url: 'data-original1'
        });
        var viewer3 = new Viewer(document.getElementById('dowebok2'),{
            url: 'data-original2'
        });
        //上传商家资质牌照
        uploadIconPic('#merchantQualification_pick', '#merchantQualification', '/stores/AdminUploadStoresImg');
        //上传商家Logo
        uploadIconPic('#merchantLogo_pick', '#merchantLogo', '/stores/AdminUploadStoresImg');
        //上传身份证
        uploadIconPic('#merchantIdCardOne_pick', '#merchantIdCardOne', '/stores/AdminUploadStoresImg');
        uploadIconPic('#merchantIdCardTwo_pick', '#merchantIdCardTwo', '/stores/AdminUploadStoresImg');

        $('.preBtn1').on('click',function(){
            //获取url
            var url = $('#merchantQualification').attr('data-src');
            if(url){
                $('#merchantQualification').attr('data-original',SignRequest.urlPrefixNoApi+url)
                viewer1.show();
            }else{
                Common.showInfoMsg('请先上传图片')
            }
        })
        $('.preBtn2').on('click',function(){
            //获取url
            var url = $('#merchantIdCardOne').attr('data-src');
            var url2 = $('#merchantIdCardTwo').attr('data-src');
            if(url || url2){
                $('#merchantIdCardOne').attr('data-original1',SignRequest.urlPrefixNoApi+url)
                $('#merchantIdCardTwo').attr('data-original1',SignRequest.urlPrefixNoApi+url2)
                viewer2.show();
            }else{
                Common.showInfoMsg('请先上传图片')
            }
        })

        $('.preBtn3').on('click',function(){
            //获取url
            var url = $('#merchantLogo').attr('data-src');
            if(url){
                $('#merchantLogo').attr('data-original2',SignRequest.urlPrefixNoApi+url)
                viewer3.show();
            }else{
                Common.showInfoMsg('请先上传图片')
            }
        })
        //获取省
        StoreAdd.adminProvinceList();

        StoreAdd.initBootstrapTable();
        //时间范围
        laydate.render({
            elem: '#time'
            , type: 'time'
            , range: true
            , format: 'HH:mm'

        });
        var geocoder, map, marker = null;
        var init = function () {
            var center = new qq.maps.LatLng(34.754504, 113.706663);
            map = new qq.maps.Map(document.getElementById('Map'), {
                center: center,
                zoom: 18
            });
            geocoder = new qq.maps.Geocoder({
                complete: function (result) {
                    console.log(result)
                    map.setCenter(result.detail.location);
                    var marker = new qq.maps.Marker({
                        map: map,
                        position: result.detail.location
                    });
                    StoreAdd.lat = result.detail.location.lat.toFixed(6);
                    StoreAdd.lng = result.detail.location.lng.toFixed(6);
                }
            });
            var label;

            function setLabelPoi(lab, latlng) {
                lab.setMap(map);
                //根据地理坐标获取相对地图容器的像素坐标。
                var point = map.fromLatLngToContainerPixel(latlng);
                var pointN = new qq.maps.Point(
                    point.getX() + 15,
                    point.getY() + 30
                );
                //根据相对地图容器的像素坐标获取地理坐标。
                var gl = map.fromContainerPixelToLatLng(pointN);
                lab.setPosition(gl);
                lab.setContent(
                    latlng.getLat().toFixed(6) + "," + latlng.getLng().toFixed(6)
                );
            };
        };
        init();
        //上传小图标
        uploadIconPic('#small_upload_pick', '#small_icon', '/stores/AdminUploadStoresImg');
        //上传门店二维码
        uploadIconPic('#code_upload_pick', '#code_icon', '/stores/AdminUploadStoresImg');
        //省改变时
        $('body').on('change', '#province_box', function () {
            var id = $('#province_box option:selected').attr('data-id');
            StoreAdd.regionid = id;
            StoreAdd.cityHandle(id);
            $('#city_box').val(0)
            $('#area_box').html('<option selected="selected" value="0">请选择区/县</option>')
            getAddres()
        })
        //市改变时
        $('body').on('change', '#city_box', function () {
            var id = $('#city_box option:selected').attr('data-id');
            StoreAdd.regionid = id;
            StoreAdd.areaHandle(id);
            $('#area_box').val(0)
            getAddres()
        })
        //区改变时
        $('body').on('change', '#area_box', function () {
            var id = $('#area_box option:selected').attr('data-id');
            StoreAdd.regionid = id;
            $('#street_box').val(0)
            getAddres()
        })
        //详细地址改变
        $('body').on('blur', '#detailAddress', function () {
            getAddres()
        });
        //间距时间触发
        $('body').on('change', '#ChairTime', function () {
            var start = $('#time').val().split('-')[0];
            var end = $('#time').val().split('-')[1];
            var start = $.trim(start);
            var end = $.trim(end);
            var setMin = Number($('#ChairTime').val());
            var result = divideAppointTime(start, end, setMin);
            var Data = {};
            Data.data = result;
            var render = template.compile(StoreAdd.time_template);
            var html = render(Data);
            $(".time-box").html(html);

        });
        //时间段删除
        $('body').on('click', '.delectBtn', function () {
            $(this).parents('.time-item').remove();
        });
        //按摩椅选择
        $('body').on('change', '#Chair', function () {
            if ($('#Chair').val() == "0") {
                $('#chairNum').val(0)
                $('.time-box').html("")
            } else {
                $('#chairNum').val(3)
            }

        });
        //判断门店名
        $('body').on('blur', '#storeName', function () {
            var name = $('#storeName').val()
            StoreAdd.adminIsStoreNameRepeat(name)
        });
        //查询按钮点击
        $('body').on('click', '#searchBtn', function () {
            StoreAdd.NickName = $('#Name').val();
            StoreAdd.projectQuery();
        })

        // 选择店长
        $("#selectClassify").on("click", function () {
            $(".mask").show();
        });

        // 关闭选择店长弹窗
        $(".mask").on("click", ".close", function () {
            $(".mask").hide();
        });

        // 选择店长
        $(".mask").on("click", ".status_choose", function () {
            var id = $(this).attr("data-id");
            var name = $(this).attr("data-name");
            $("#managerName").attr("data-id", id);
            $("#managerName").text(name);
            $(".mask").hide();
        });

        //保存
        $('body').on('click', '#nextStep', function () {
            //门店编号
            // if (!Validate.emptyValidateAndFocus("#storeNum", "请输入门店编号", "")) {
            //     return false;
            // }
            //门店名称
            if (!Validate.emptyValidateAndFocus("#storeName", "请输入门店名称", "")) {
                return false;
            }
            //门店关键词
            if (!Validate.emptyValidateAndFocus("#key", "请输入门店关键词", "")) {
                return false;
            }
            //门店简介
            if (!Validate.emptyValidateAndFocus("#introduction", "请输入门店简介", "")) {
                return false;
            }
            //门店图片
            if ($('#small_icon').attr('data-src') == null || $('#small_icon').attr('data-src') == "") {
                Common.showInfoMsg("请上传门店主图!")
                return false;
            }
            //门店二维码
            if ($('#code_icon').attr('data-src') == null || $('#code_icon').attr('data-src') == "") {
                Common.showInfoMsg("请上传门店二维码!")
                return false;
            }

            //门店图片
            if ($('#merchantLogo').attr('data-src') == null || $('#merchantLogo').attr('data-src') == "") {
                Common.showInfoMsg("请上传门店logo!")
                return false;
            }
            //门店资质牌照
            if ($('#merchantQualification').attr('data-src') == null || $('#merchantQualification').attr('data-src') == "") {
                Common.showInfoMsg("请上传门店资质牌照!")
                return false;
            }
            //门店身份证证件照
            if ($('#merchantIdCardOne').attr('data-src') == null || $('#merchantIdCardOne').attr('data-src') == "" || $('#merchantIdCardTwo').attr('data-src') == null || $('#merchantIdCardTwo').attr('data-src') == "") {
                Common.showInfoMsg("请上传身份证证件照!")
                return false;
            }
            //门店联系人
            if (!Validate.emptyValidateAndFocus("#userName", "请输入门店联系人", "")) {
                return false;
            }
            //门店联系电话
            if (!Validate.emptyValidateAndFocus("#phone", "请输入门店联系电话", "")) {
                return false;
            }
            if(!(/^0\d{2,3}-?\d{7,8}$/.test($('#phone').val())) && !(/^1[3|7|5|8]\d{9}$/.test($('#phone').val()))){

                Common.showInfoMsg('请输入正确的联系电话');
                return false;
            }

            if ($('#province_box').val() == "0" || $('#city_box').val() == "0" || $('#area_box').val() == "0") {
                Common.showErrorMsg("请选择省市区")
                return false;
            }
            //详细地址
            if (!Validate.emptyValidateAndFocus("#detailAddress", "请输入详细地址", "")) {
                return false;
            }

            //轮播图
            var picList = [];
            $('.select-img').each(function (index, item) {
                if ($(item).attr('data-src') != "" && $(item).attr('data-src') != undefined) {
                    picList.push($(item).attr('data-src'))
                }
            })
            if (picList.length < 1) {
                Common.showInfoMsg('请上传轮播图图片');
                return false
            }

                StoreAdd.adminAddStore(picList)
        });

        //获取地址
        function getAddres() {
            var province = $('#province_box option:selected').val() == 0 ? '' : $('#province_box option:selected').val();
            var city = $('#city_box option:selected').val() == 0 ? '' : $('#city_box option:selected').val();
            var area = $('#area_box option:selected').val() == 0 ? '' : $('#area_box option:selected').val();
            var detail = $('#detailAddress').val();
            var address = province + city + area + detail;
            geocoder.getLocation(address);
        }

        //根据间距分割时间
        function divideAppointTime(startTime, endTime, setMin) {

            var startArr = startTime.split(":");
            var endArr = endTime.split(":");
            var startMinute = parseInt(startArr[0]) * 60 + parseInt(startArr[1]);
            var endMinute = parseInt(endArr[0]) * 60 + parseInt(endArr[1]);
            var count = parseInt((endMinute - startMinute) / setMin);
            var result = [];
            for (var i = 0; i < count; i++) {
                var tMinute = setMin * i + startMinute;
                var nextMinute = tMinute + setMin;
                var times = {
                    StartTime: exchangeMinutesToTimeString(tMinute),
                    EndTime: exchangeMinutesToTimeString(nextMinute)
                }
                result.push(times);
            }
            return result;
        };

        // 根据分钟数换算成小时分钟字符串
        function exchangeMinutesToTimeString(tMinute) {
            console.log(tMinute)
            var newHour = parseInt(tMinute / 60);
            var newMinute = tMinute % 60;
            newHour = newHour >= 10 ? newHour : ("0" + newHour);
            newMinute = newMinute >= 10 ? newMinute : ("0" + newMinute);
            var newTime = newHour + ":" + newMinute;
            return newTime;
        };


    },

    //初始化事件
    initHandle: function () {

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
    },
    //获取省列表接口
    adminProvinceList: function () {
        //请求方法
        var methodName = "/Tool/ProvinceList";
        var data = {
            "Type": 1,
            "ParentId": 0
        };
        //请求接口
        SignRequest.setNoAdmin(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(StoreAdd.province_template);
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
                var render = template.compile(StoreAdd.city_template);
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
                var render = template.compile(StoreAdd.area_template);
                var html = render(data.Data);
                $("#area_box").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },

    //拼接地址
    joinAddress: function () {
        var province = $('#province_box option:selected').val() == 0 ? '' : $('#province_box option:selected').val();
        var city = $('#city_box option:selected').val() == 0 ? '' : $('#city_box option:selected').val();
        var area = $('#area_box option:selected').val() == 0 ? '' : $('#area_box option:selected').val();
        var detail = $('#detailAddress').val();
        var address = province + city + area + detail;
        return address;
    },
    //获取按摩椅列表
    adminMassageChairsList: function () {
        //请求方法
        var methodName = "/stores/AdminMassageChairsList";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(StoreAdd.chair_template);
                var html = render(data.Data);
                $("#Chair").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //新增门店接口
    adminAddStore: function (StoresImgList) {
        //请求方法
        var methodName = "/stores/AdminAddStore";
        var data = {
            "Name": $('#storeName').val(),
            "Contacts": $('#userName').val(),
            "ContactsPhone": $('#phone').val(),
            "Address": $('#detailAddress').val(),
            "Longitude": StoreAdd.lng,
            "Latitude": StoreAdd.lat,
            "UId": $("#managerName").attr("data-id"),
            "RegionId": StoreAdd.regionid,
            "Logo": $('#merchantLogo').attr('data-src'),
            "FrontIdCard": $('#merchantIdCardOne').attr('data-src'),
            "BackIdCard":  $('#merchantIdCardTwo').attr('data-src'),
            "License": $('#merchantQualification').attr('data-src'),
            "KeyWords": $('#key').val(),
            "Summary": $('#introduction').val(),
            "ShowImg": $('#small_icon').attr('data-src'),
            "StoresImgList": StoresImgList,
            "Identity":$('input[name="Identity"]:checked').val(),
            "IsMarketing": true,
            "CodeImage":$('#code_icon').attr('data-src'),

        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('新增门店成功', function () {

                    location.href = '/store/storeList'
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //判断门店名是否重复
    adminIsStoreNameRepeat: function (Name) {
        //请求方法
        var methodName = "/stores/AdminIsStoreNameRepeat";
        var data = {
            "Name": Name
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Data) {
                Common.showErrorMsg("门店名已存在");
                StoreAdd.storeFlag = true;
            } else {
                StoreAdd.storeFlag = false;
            }
        });
    },
    //获取店长列表
    adminStoreManagerListAddChoose: function () {
        //请求方法
        var methodName = "/stores/AdminStoreManagerListAddChoose";
        var data = {

            "Page": {
                "PageSize": 100,
                "PageIndex": 1
            }
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(StoreAdd.shopkeeper_template);
                var html = render(data.Data);
                $("#shopkeeper").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#table_content_classify').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/stores/AdminStoreManagerListAddChoose',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 10,
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
            queryParams: StoreAdd.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: StoreAdd.responseHandler,
            columns: [
                {
                    field: 'UId',
                    title: '店长编号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var html = `${value}`

                        return html;
                    }
                },
                {
                    field: 'NickName',
                    title: '店长名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'UId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var html = '<span style="padding: 0 6px;cursor: pointer;color: #1792e7;" class="status_choose" data-id="' + value + '" data-name="'+ row.NickName +'">选择</span>';


                        return html;
                    }
                },


            ],
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
                $('#dishes_list_table').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法
                // console.log(row);
                // console.log(tr);
                // console.log(flied);
            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {

                // for (var i = 0; i < rows.length; i++) {
                //     DishesList.UserIdsList.push(rows[i].User.Id);
                //     DishesList.UserOpenIds.push(rows[i].User.OpenId);
                // }

            },
            onUncheckAll: function (rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function (row) {


            },
            //取消每一个单选框时对应的操作；
            onUncheck: function (row) {


            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/stores/AdminStoreManagerListAddChoose";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            pageSize: params.limit, //页面大小
            pageNumber: (params.offset / params.limit) + 1, //页码
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "NickName": StoreAdd.NickName,

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.StoreManagerList,
                "total": res.Data.Total
            };
        } else {
            return {
                "rows": [],
                "total": 0
            };
        }
    },
    //表格刷新
    projectQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                "NickName": StoreAdd.NickName,
            };
        } else {
            var obj = parame;
        }
        //方法名
        var methodName = "/stores/AdminStoreManagerListAddChoose";


        $('#table_content_classify').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/stores/AdminStoreManagerListAddChoose',
                query: obj
            }
        );
    },


}