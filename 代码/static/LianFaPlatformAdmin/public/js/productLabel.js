var ProductLabel = {
    init: function () {
        ProductLabel.initBootstrapTable();
        //添加的模态框出现
        $('#myModal').on('show.bs.modal',function () {
                //清空内容
                $('#labelName').val("");
        })
        //添加的保存
        $('body').on('click', '#conserve', function () {
            var name = $('#labelName').val();
            //标签名验证
            if (!Validate.emptyValidateAndFocus("#labelName", "请输入标签名", "")) {
                return false;
            }
            ProductLabel.adminAddProductLabel(name);
        });
        //点击删除
        $('body').on('click','.status_delete',function(){
            var id = $(this).attr('data-id');

            Common.confirmDialog('是否删除该标签?',function(){
                ProductLabel.adminDelProductLabel(id);
            })


        })
        //点击编辑
        $('body').on('click','.editor',function(){
            localStorage.setItem('LableId',$(this).attr('data-id'));
            localStorage.setItem('LableName',$(this).parents('tr').find('.label_name').text());
            $('#myModalEdit').modal('show');
        })
        //编辑模态框出现之后
        $('#myModalEdit').on('shown.bs.modal', function (e) {
                $('#labelName_edit').val(localStorage.getItem('LableName'));
        })
        //点击保存
        $('body').on('click','#conserve_edit',function(){
            var name = $('#labelName_edit').val();
            if(!name){
                Common.showInfoMsg('名称不能为空!')
            }else{
                ProductLabel.adminEditProductLabel(name)
            }

        })
    },
    //后台添加商品标签
    adminAddProductLabel: function (name) {
        //请求方法
        var methodName = "/productLabel/AdminAddProductLabel";
        var data = {
            "Name": name
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg("添加标签成功!", function () {
                    ProductLabel.projectQuery()
                    $('#myModal').modal('hide');
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台编辑商品标签
    adminEditProductLabel: function (name) {
        //请求方法
        var methodName = "/productLabel/AdminEditProductLabel";
        var data = {
            "PlId": localStorage.getItem('LableId'),
            "Name": name
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg("编辑标签成功!", function () {

                    ProductLabel.projectQuery()
                    $('#myModalEdit').modal('hide');
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台删除商品标签
    adminDelProductLabel: function (id) {
        //请求方法
        var methodName = "/productLabel/AdminDelProductLabel";
        var data = {
            "PlId": id
        };
        console.log(data)
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                    Common.showSuccessMsg('删除标签成功',function(){
                        //删除成功之后刷新表格
                        ProductLabel.projectQuery()
                    })


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#productLableBox').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/productLabel/AdminProductLabelList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: false, //在表格底部显示分页工具栏
            pageSize: 3,
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
            queryParams: ProductLabel.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: ProductLabel.responseHandler,
            columns: [
                {
                    field: 'Name',
                    title: '标签名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span class="label_name">' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'PLId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var html = '<span style="color: #039cf8;cursor: pointer;" class="editor" data-id="'+value+'" >编辑</span>' +
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
        var methodName = "/productLabel/AdminProductLabelList";

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
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.ProductLabelList,
                // "total": res.Data.PageModel.TotalCount
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
        var methodName = "/productLabel/AdminProductLabelList";


        $('#productLableBox').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/productLabel/AdminProductLabelList',
                query: obj
            }
        );
    },
}


$(function () {

    ProductLabel.init();
})