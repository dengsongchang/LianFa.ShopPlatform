var ProductBrand = {

    init: function () {
        //初始化表格
        ProductBrand.initBootstrapTable()
        //图片上传按钮
        uploadFoodPic('#brandbox', '#uploader_food_btn', '/brand/AdminUploadBrandLogo');
        //查询按钮点击
        $('body').on('click', '#searchBtn', function () {
            var data = {
                "Name": $('#brand_name').val(),
                Page: {
                    PageSize: 10,//页面大小,
                    PageIndex: 1,//页码
                },
            };
            ProductBrand.projectQuery(data)
        });
        //添加里面的完成按钮点击
        $('body').on('click', '#submit', function () {
            var name = $('#add_brand_name').val();
            var src = $('#brandbox').attr('data-src');
            //品牌名验证
            if (!Validate.emptyValidateAndFocus("#add_brand_name", "请输入标签名", "")) {
                return false;
            }
            //图片验证
            // if($('#brandbox').attr('data-src') == null || $('#brandbox').attr('data-src') == ""){
            //     Common.showErrorMsg("请上传图片!")
            //     return false;
            // }
            ProductBrand.adminAddBrand(name, src)


        });
        //点击删除
        $('body').on('click', '.status_delete', function () {
            var id = $(this).attr('data-id');
            Common.confirmDialog("确认要删除嘛", function () {
                ProductBrand.adminDelBrand(id)
            })
        });
        //Tab栏添加按钮点击
        $('body').on('click', '#addBrand', function () {
            $('#add_brand_name').val("");
            $('#brandbox').attr('src', '../../public/images/addImg.png');
            $('#brandbox').attr('data-src', '');
        });
        $('body').on('change', '.order-disp', function () {
            var id = $(this).attr('data-id');
            var num = $(this).val();
            console.log(num)
            ProductBrand.adminBrandInfo(id, num);

        })
        //品牌数量的改变
        $(".inputNum").change(function () {
            var reg = /^[1-9]\d*$/;
            if (!reg.test($(this).val())) {
                swal({title: '提示!', text: '请输入正确数量', timer: 3000, type: 'success'})
            } else {
                swal({title: '提示!', text: '批量更新排序成功', timer: 3000, type: 'success'})
            }
        });
    },
    //后台删除品牌
    adminDelBrand: function (id) {
        //请求方法
        var methodName = "/brand/AdminBatchDelBrand";
        var data = {
            "BrandIdList": [id]
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功', function () {
                    //删除成功之后刷新表格
                    ProductBrand.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台添加品牌
    adminAddBrand: function (name, logo) {
        //请求方法
        var methodName = "/brand/AdminAddBrand";
        var data = {
            "Name": name,
            "Logo": logo
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功', function () {
                    $('.nav-tabs a[data-type=1]').click();
                    ProductBrand.projectQuery();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台编辑品牌
    adminEditBrand: function (id, num) {
        //请求方法
        var methodName = "/brand/AdminEditBrand";
        var data = {
            "BrandId": id,
            "Name": localStorage.getItem('brandName'),
            "Logo": localStorage.getItem('brandbox_src'),
            "DisplayOrder": num
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功', function () {
                    ProductBrand.projectQuery();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台品牌信息
    adminBrandInfo: function (id, num) {
        //请求方法
        var methodName = "/brand/AdminBrandInfo";
        var data = {
            "BrandId": id
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                localStorage.setItem('brandName', data.Data.BrandInfo.Name);
                localStorage.setItem('brandbox_src', data.Data.BrandInfo.Logo);
                ProductBrand.adminEditBrand(id, num)

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#brand_box').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/brand/AdminBrandList',
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
            queryParams: ProductBrand.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: ProductBrand.responseHandler,
            columns: [
                {
                    field: 'Name',
                    title: '品牌名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'DisplayOrder',
                    title: '显示顺序',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<input type="number" class="order-disp" data-id="' + row.BrandId + '" value=' + value + '>';

                        return e;
                    }
                },
                {
                    field: 'BrandId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var html = '<a class="editor" href="/brand/editBrand?id=' + value + '">编辑</a>' +
                            '<span style="padding: 0 6px" class="status_delete" data-id="' + value + '">删除</span>';

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
        var methodName = "/brand/AdminBrandList";

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
            Name: $('#brand_name').val(),

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.BrandList,
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
            var obj = {};
        } else {
            var obj = parame;
        }
        //方法名
        var methodName = "/brand/AdminBrandList";


        $('#brand_box').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/brand/AdminBrandList',
                query: obj
            }
        );
    },
    //表格先销毁刷新
    projectDesQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {};
        } else {
            var obj = parame;
        }
        //方法名
        var methodName = "/brand/AdminBrandList";


        $('#brand_box').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/brand/AdminBrandList',
                query: obj
            }
        );
        ProductBrand.initBootstrapTable()
    },


}


$(function () {

    ProductBrand.init()

})