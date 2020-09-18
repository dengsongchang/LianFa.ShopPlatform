var ApplicationForGiftSetting = {

    init: function () {
        //初始化表格
        ApplicationForGiftSetting.initBootstrapTable();
        //排序更改
        $('body').on('change', '.order-disp', function () {
            var BId = $(this).attr('data-id');
            var DisplayOrder = $(this).val();
            ApplicationForGiftSetting.adminChangeNbDisplayOrder(BId, DisplayOrder)
        });
        //保存按钮点击
        $('body').on('click', '#nextStep', function () {
            var Name = $('#consultingName').val();
            var Url = $("#url").val();
            var Img = $('#small_icon').attr('data-src');
            var DisplayOrder = $('#sort').val();
            var State = $('input[name="state"]:checked').val();
            var IsLink = Number($('input[name="link"]:checked').val());

            //咨询名称验证
            if (!Validate.emptyValidateAndFocus("#consultingName", "请输入标题名称", "")) {
                return false;
            }
            //图片验证
            if (Img == null || Img == undefined) {
                Common.showInfoMsg('请先上传图片')
                return false;
            }
            if (!Validate.emptyValidateAndFocus("#url", "请输入地址", "")) {
                return false;
            }
            //排序验证
            if (!Validate.emptyValidateAndFocus("#sort", "请输入排序", "")) {
                return false;
            }
            // 调用编辑接口这个方法
            ApplicationForGiftSetting.adminProvinceList(Name, Url, Img, DisplayOrder, State, IsLink);
        });

        //单个删除
        $("body").on("click", ".status_delete", function () {
            var rId = $(this).attr("data-id");
            Common.confirmDialog("确认删除吗？", function () {
                ApplicationForGiftSetting.adminDelGiftBag(rId);
            });
        });
        $('body').on("click",'.status_change',function(){
            var  state = $(this).attr('data-state');
            var id = $(this).attr('data-id');
            ApplicationForGiftSetting.adminChangeGiftBagStatus(id,state)
        })
    },

    //更改状态
    adminChangeGiftBagStatus: function (Id, state) {
        //请求方法
        var methodName = "/Distribution/AdminChangeGiftBagStatus";
        var data = {
            "GId": Id,
            "Staus": state
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('更改成功', function () {
                    //删除成功之后刷新表格
                    ApplicationForGiftSetting.projectQuery();
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    adminDelGiftBag: function (RId) {
        var methodName = "/Distribution/AdminDelGiftBag";
        var data = {
            GId: RId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("删除成功",function(){
                    ApplicationForGiftSetting.projectQuery();
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#brand_box').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/Distribution/AdminGetGiftBagList',
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
            queryParams: ApplicationForGiftSetting.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class\


            responseHandler: ApplicationForGiftSetting.responseHandler,
            columns: [{
                field: 'GId',
                title: '编号',
                align: 'center',
                valign: 'middle',
            },
                {
                    field: 'Title',
                    title: '标题',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'StatusStr',
                    title: '状态',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'ShopPrice',
                    title: '销售价',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'CostPrice',
                    title: '成本价',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'GId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        if (row.Status == 2) {
                            var html = "<span class='status_change' data-state='1' style='margin-right:10px;color: #3c8dbc;cursor: pointer' data-id='" + value + "'> 上架</span>"
                        } else {
                            var html = "<span class='status_change' data-state='2' style='margin-right:10px;color: #3c8dbc;cursor: pointer' data-id='" + value + "'> 下架</span>"
                        }


                        html += '<a class="editor" href="/product/applicationForAddGiftSetting?PId=' + value + '">编辑</a> &nbsp;&nbsp;';
                        html += "<span class='status_delete' data-id='" + value + "'> 删除</span>";
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
        var methodName = "/Distribution/AdminGetGiftBagList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            PageModel: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.List,
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
        var methodName = "/Distribution/AdminGetGiftBagList";
        $('#brand_box').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/Distribution/AdminGetGiftBagList',
                query: obj
            }
        );
    },


}


$(function () {

    ApplicationForGiftSetting.init()

})