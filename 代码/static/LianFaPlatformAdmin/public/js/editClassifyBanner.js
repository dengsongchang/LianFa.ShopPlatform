$(function () {
    NavigationEditor.init();
})

var NavigationEditor = {
    init: function () {
        //上传小图标
        uploadIconPic('#small_upload_pick', '#small_icon', '/banner/AdminUploadBannerImg');
        //获取Id
        var bId = Common.getUrlParam("id");
        //获取轮播信息 
        NavigationEditor.getNavInfo(bId);

        //返回按钮点击
        $('body').on('click','#backBtn',function(){
            window.history.go(-1);
        })


        //选择明星会场模态框出现获取数据
        $('#choiceStarModal').on('shown.bs.modal', function () {
            NavigationEditor.initStarBootstrapTable()
        });
        //选择商品模态框出现获取数据
        $('#choicePresentModal').on('shown.bs.modal', function () {
            NavigationEditor.initProBootstrapTable()
        });
        //选择秒杀专场模态框出现获取数据
        $('#choicePerformanceModal').on('shown.bs.modal', function () {
            NavigationEditor.initPerformanceBootstrapTable()
        });
        //选择品牌专场模态框出现获取数据
        $('#choiceBrandModal').on('shown.bs.modal', function () {
            NavigationEditor.initBootstrapTable()
        });

        //商品查询
        $('body').on('click', '#prosearchBtn', function () {
            NavigationEditor.projectProQuery();
        })

        //专场查询
        $('body').on('click', '#performanceBtn', function () {
            NavigationEditor.projectPerformanceQuery();
        })
        //品牌查询
        $('body').on('click', '#BrandBtn', function () {
            NavigationEditor.projectQuery();
        })

        //类型切换的时候
        $('body').on('change', '#type', function () {
            var val = $(this).val();
            if (val == 0) {
                $('#productBox').hide();
                $('#performanceBox').hide();
                $('#BrandBox').hide();
                $('#starBox').show();
            } else if (val == 1) {
                $('#productBox').show();
                $('#performanceBox').hide();
                $('#BrandBox').hide();
                $('#starBox').hide();

            } else if (val == 2) {
                $('#productBox').hide();
                $('#performanceBox').show();
                $('#BrandBox').hide();
                $('#starBox').hide();
            } else if (val == 3) {
                $('#productBox').hide();
                $('#performanceBox').hide();
                $('#BrandBox').show();
                $('#starBox').hide();
            }
        })
        //明星会场模态框点击选择
        $('#choiceStarModal').on('click', '.sure_choiceStar', function () {
            //商品名称
            var id = $(this).attr('data-id')
            var txt_name = $(this).parents('tr').find('.star_name_modal').text();
            $('.star-name_final').text(txt_name);
            $('.star-name_final').show();
            $('.star-name_final').attr('data-id', $(this).attr('data-id'))
            $('#choiceStarModal').modal('hide');
        });
        //商品模态框点击选择
        $('#choicePresentModal').on('click', '.sure_choice', function () {
            //商品名称
            var id = $(this).attr('data-id')
            var txt_name = $(this).parents('tr').find('.goods_name_modal').text();
            $('.goods-name_final').text(txt_name);
            $('.goods-name_final').show();
            $('.goods-name_final').attr('data-id', $(this).attr('data-id'))
            $('#choicePresentModal').modal('hide');
        });
        //专场模态框点击选择
        $('#choicePerformanceModal').on('click', '.sure_choicePer', function () {
            //商品名称
            var id = $(this).attr('data-id')
            var txt_name = $(this).parents('tr').find('.goods_performance_modal').text();
            $('.performance-name_final').text(txt_name);
            $('.performance-name_final').show();
            $('.performance-name_final').attr('data-id', $(this).attr('data-id'))
            $('#choicePerformanceModal').modal('hide');
        });

        //品牌模态框点击选择
        $('#choiceBrandModal').on('click', '.sure_choiceBrand', function () {
            //商品名称
            var id = $(this).attr('data-id')
            var txt_name = $(this).parents('tr').find('.goods_brand_modal').text();
            $('.brand-name_final').text(txt_name);
            $('.brand-name_final').show();
            $('.brand-name_final').attr('data-id', $(this).attr('data-id'))
            $('#choiceBrandModal').modal('hide');
        });
        //保存按钮点击
        $('body').on('click', '#nextStep', function () {
            var Name = $('#consultingName').val();
            var Img = $('#small_icon').attr('data-src');
            var DisplayOrder = $('#sort').val();
            var Url = $("#url").val();
            var State = $("input[name='state']:checked").val();
            var IsLink =Number($('input[name="link"]:checked').val());
            //图片验证
            if (Img == null || Img == undefined) {
                Common.showInfoMsg('请先上传图片')
                return false;
            }
            //排序验证
            if (!Validate.emptyValidateAndFocus("#sort", "请输入排序", "")) {
                return false;
            }
            // 调用编辑接口这个方法
            NavigationEditor.adminProvinceList(bId, Name, "", State, Img, DisplayOrder, IsLink);
        })
    },
    //获取编辑接口
    adminProvinceList: function (BId, Name, Url, State, Img, DisplayOrder, IsLink) {
        //请求方法
        var data = {
            "CateId": Common.getUrlParam('id'),
            "BId": BId,
            "State": State,
            "DisplayOrder": DisplayOrder,
            "Title": Name,
            "Image": Img,
            "Url": Url,
            IsLink: 0,
            "Type": $('#type').val(),
        };
        data.Id = 0;
        //请求方法
        var methodName = "/category/AdminEditCategoryBanner";
        if ($('#type').val() == 1 && $('#productBox').is(":visible")) {
            data.Id = $('.goods-name_final').attr('data-id')
        } else if ($('#type').val() == 2 && $('#performanceBox').is(":visible")) {
            data.Id = $('.performance-name_final').attr('data-id')
        } else if ($('#type').val() == 3 && $('#BrandBox').is(":visible")) {
            data.Id = $('.brand-name_final').attr('data-id')
        } else {
            data.Id = $('.star-name_final').attr('data-id') ? $('.star-name_final').attr('data-id') : 0
        }
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功', function () {
                    window.history.go(-1);
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取轮播信息     
    getNavInfo: function (BId) {
        var methodName = "/category/AdminCategoryBannerInfo";
        var data = {
            "BId": BId,

        }
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                $('#small_icon').attr('src', data.Data.AdminCategoryBannerInfo.ImageFull);
                $('#small_icon').attr('data-src', data.Data.AdminCategoryBannerInfo.Image);
                $('#sort').val(data.Data.AdminCategoryBannerInfo.DisplayOrder);
                $('#url').val(data.Data.AdminCategoryBannerInfo.Url);
                // 状态
                var stateList = $("#State label");
                for (var i = 0; i < stateList.length; i++) {    
                    if (stateList.eq(i).find("input").val() == data.Data.AdminCategoryBannerInfo.State) {        
                        stateList.eq(i).find("input").prop("checked", true);    
                    }
                }
                var link = $("#IsLink label");
                for (var i = 0; i < link.length; i++) {    
                    if (link.eq(i).find("input").val() == data.Data.AdminCategoryBannerInfo.IsLink) {        
                        link.eq(i).find("input").prop("checked", true);
                    }
                }
                $('#type').val(data.Data.AdminCategoryBannerInfo.Type)
                if(data.Data.AdminCategoryBannerInfo.Type == 1){
                    //商品
                    $('#starBox').hide();
                    $('#productBox').show();
                    $('#performanceBox').hide();
                    $('#BrandBox').hide();
                    $('.goods-name_final').show();
                    $('.goods-name_final').text(data.Data.AdminCategoryBannerInfo.Name)
                    $('.goods-name_final').attr('data-id',data.Data.AdminCategoryBannerInfo.Id)
                }else if(data.Data.AdminCategoryBannerInfo.Type == 2){
                    $('#starBox').hide();
                    $('#productBox').hide();
                    $('#performanceBox').show();
                    $('#BrandBox').hide();
                    $('.performance-name_final').show();
                    $('.performance-name_final').text(data.Data.AdminCategoryBannerInfo.Name)
                    $('.performance-name_final').attr('data-id',data.Data.AdminCategoryBannerInfo.Id)
                }else if(data.Data.AdminCategoryBannerInfo.Type == 3){
                    $('#starBox').hide();
                    $('#productBox').hide();
                    $('#performanceBox').hide();
                    $('#BrandBox').show();
                    $('.brand-name_final').show();
                    $('.brand-name_final').text(data.Data.AdminCategoryBannerInfo.Name)
                    $('.brand-name_final').attr('data-id',data.Data.AdminCategoryBannerInfo.Id)
                }else{
                    //明星会场
                    $('#starBox').show();
                    $('#productBox').hide();
                    $('#performanceBox').hide();
                    $('#BrandBox').hide();
                    $('.star-name_final').show();
                    $('.star-name_final').text(data.Data.AdminCategoryBannerInfo.Name)
                    $('.star-name_final').attr('data-id',data.Data.AdminCategoryBannerInfo.Id)
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
            url: SignRequest.urlPrefix + '/category/AdminCategoryProductList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 6,
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
            queryParams: NavigationEditor.queryProParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: NavigationEditor.responseProHandler,
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
        var methodName = "/category/AdminCategoryProductList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            "Name": $('#choice_presentName').val(),
            "CateId": Common.getUrlParam('cateId'),
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
                "rows": res.Data.AdminCategoryProductList,
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
        var methodName = "/category/AdminCategoryProductList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "Name": $('#choice_presentName').val(),
                "CateId": Common.getUrlParam('cateId'),
                Page: {
                    PageSize: 6,
                    PageIndex: 1,
                },
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

    //选择专场
    //秒杀专场列表
    initPerformanceBootstrapTable: function () {
        $('#choice_performance_tb').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/timeproductactivity/AdminTimeProductActivitySpecialList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 6,
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
            queryParams: NavigationEditor.queryPerformanceParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: NavigationEditor.responsePerformanceHandler,
            columns: [

                {
                    field: 'Title',
                    title: '专场名称',
                    align: 'center',
                    valign: 'middle',
                    width:300,
                    formatter: function (value, row, index) {
                        var e = `<span class="goods_performance_modal">${value}</span>`
                        return e;
                    }
                },
                {
                    field: 'StartTime',
                    title: '开始时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var startTime = value.replace('T'," ")

                        var e =`<span>${startTime}</span>`


                        return e;
                    }
                },
                {
                    field: 'EndTime',
                    title: '结束时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var endTime = value.replace('T'," ")

                        var e =`<span>${endTime}</span>`

                        return e;
                    }
                },
                {
                    field: 'ToSnapUpPrice',
                    title: '抢购价格',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e =`<span>${value}</span>`


                        return e;
                    }
                },
                {
                    field: 'HasRobNumber',
                    title: '已购总量',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e =`<span>${value}</span>`


                        return e;
                    }
                },
                {
                    field: 'ActivityId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = "<span class='sure_choicePer'  data-id='" + value + "'>选择</span>";
                        return html;
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
    queryPerformanceParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/timeproductactivity/AdminTimeProductActivitySpecialList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "State": 0,
            Title: $('#choicePerformanceName').val()

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responsePerformanceHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.TimeProductActivityList,
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
    projectPerformanceQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                Title: $('#choicePerformanceName').val(),
                "State": 0,
                Page: {
                    PageSize: 6,
                    PageIndex: 1,
                },
            };
        } else {
            obj = parame;
        }

        $('#choice_performance_tb').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/timeproductactivity/AdminTimeProductActivitySpecialList',
                query: obj
            }
        );

    },
    //品牌专场
    //bootstrapTable
    initBootstrapTable: function () {
        $('#tb_limit_content').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/brand/AdminBrandSpecialList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 6,
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
            queryParams: NavigationEditor.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: NavigationEditor.responseHandler,
            columns: [

                {
                    field: 'Title',
                    title: '活动名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e = `<span class="goods_brand_modal">${value}</span>`
                        return e;
                    }
                },
                {
                    field: 'ShowImg',
                    title: '活动主图',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e =`<img src="${value}" style="width: 100px;height: 100px;">`
                        return e;
                    }
                },
                {
                    field: 'RecordId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = "<span class='sure_choiceBrand'  data-id='" + value + "'>选择</span>";
                        return html;
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
        var methodName = "/brand/AdminBrandSpecialList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.AdminBrandSpecialList,
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

            };
        } else {
            var obj = parame;
        }

        $('#tb_limit_content').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/brand/AdminBrandSpecialList',
                query: obj
            }
        );

    },
    //明星专场列表
    //bootstrapTable
    initStarBootstrapTable: function () {
        $('#tb_star_content').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/category/AdminCategoryStarVenueList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 6,
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
            queryParams: NavigationEditor.queryStarParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: NavigationEditor.responseStarHandler,
            columns: [

                {
                    field: 'Name',
                    title: '名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var e = `<span class="star_name_modal">${value}</span>`
                        return e;
                    }
                },
                {
                    field: 'RecordId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = "<span class='sure_choiceStar' style='color: #1792e7;cursor: pointer'  data-id='" + value + "'>选择</span>";
                        return html;
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
    queryStarParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/category/AdminCategoryStarVenueList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "CateId": Common.getUrlParam('cateId'),

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseStarHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.AdminCategoryStarVenueList,
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
    projectStarQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {
                "CateId": Common.getUrlParam('cateId'),
            };
        } else {
            var obj = parame;
        }

        $('#tb_star_content').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/category/AdminCategoryStarVenueList',
                query: obj
            }
        );

    },



}