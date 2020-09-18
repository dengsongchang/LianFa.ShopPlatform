var freightTemplate={
    init:function () {
        //初始化按摩椅列表
        freightTemplate.initBootstrapTable();
        //全选
        $("#selAll").on("change",function(){
            if($(this).is(':checked')){
                $(".checkbox").prop("checked",true);
            }
            else{
                $(".checkbox").prop("checked",false);
            }
        });
        //单个删除
        $("#armchairTable").on("click",".status_delete",function(){
            var mcId = $(this).attr("data-id");
            var mcIdArr = [];
            mcIdArr.push(mcId);
            Common.confirmDialog("确认进行删除吗？",function(){
                freightTemplate.deleteChair(mcIdArr);
            });
        });
        //多个删除
        $("#delete").on("click",function(){
            Common.confirmDialog("确认对选中的数据进行删除吗？",function(){
                freightTemplate.deleteChair(freightTemplate.getSelectedData());
            });
        });
    },
    // 获取选中的数据
    getSelectedData:function(){
        var list = $("#armchairTable .checkbox");
        var mcId = [];
        for(var i=0;i<list.length;i++){
            if(list.eq(i).is(':checked')){
                mcId.push(list.eq(i).attr("data-pid"));
            }
        }
        return mcId;
    },
    // 删除
    deleteChair:function(ids){
        var methodName = "/templates/AdminDelTemplates";
        var data = {
            "TemplatesIdList":ids
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {

                Common.showSuccessMsg("删除成功",function(){
                    freightTemplate.refreshQuery();
                });
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable:function(){
        $('#armchairTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/templates/AdminTemplatesList',
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
            queryParams: freightTemplate.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: freightTemplate.responseHandler,
            columns: [
                {
                    field: 'TemplateId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-pid="'+ value +'">'
                        return html;
                    }
                },
                {
                    field: 'TemplateName',
                    title: '模板名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'ValuationMethodDesc',
                    title: '计价方式',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'TemplateId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function(value, row, index) {
                        if(localStorage.getItem('IsStoreManager') == 'true'){
                            var html = "";
                            html += '<a class="status_edit" href="/product/editFreightTemplate?id='+value+'" style="margin-right:10px;">编辑</a>';
                        }else{
                            var html = "";
                            html += '<a class="status_edit" href="/product/editFreightTemplate?id='+value+'" style="margin-right:10px;">编辑</a>';
                            html += "<span class='status_delete' data-id='" + value + "'>删除</span>";
                        }

                        return html;
                    }
                }
            ], //列
            silent: true, //刷新事件必须设置
            formatLoadingMessage: function() {
                return "请稍等，正在加载中...";
            },
            formatNoMatches: function() { //没有匹配的结果
                return '无符合条件的记录';
            },
            onLoadSuccess: function(data) {
                // console.log(data);

                $('.caret').remove()

            },
            onLoadError: function(data) {
                $('#armchairTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function(row, tr, flied) {
                // 书写自己的方法

            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function(rows) {

                // for (var i = 0; i < rows.length; i++) {
                //     dishes_list.UserIdsList.push(rows[i].User.Id);
                //     dishes_list.UserOpenIds.push(rows[i].User.OpenId);
                // }

            },
            onUncheckAll: function(rows) {

            },
            //点击每一个单选框时触发的操作
            onCheck: function(row) {


            },
            //取消每一个单选框时对应的操作；
            onUncheck: function(row) {
                Array.prototype.remove = function(val) {
                    var index = this.indexOf(val);
                    if (index > -1) {
                        this.splice(index, 1);
                    }
                };

            }
        });
    },
    //bootstrap table post 参数 queryParams
    queryParams: function(params) {
        //配置参数
        //方法名
        var methodName = "/templates/AdminTemplatesList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,//页面大小,
                PageIndex: (params.offset / params.limit) + 1,//页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function(res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.TemplatesList,
                "total": res.Data.Total
            };
        } else {
            return {
                "rows": [],
                "total": 0
            };
        }
    },
    //表格刷新(直接刷新)
    refreshQuery: function(parame) {
        //方法名
        var methodName = "/templates/AdminTemplatesList";

        if (parame == "" || parame == undefined) {
           var obj = {
                Page: {
                    PageSize: 10,//页面大小,
                    PageIndex: 1,//页码
                }
            };
        } else {
            obj = parame;
        }

        $('#armchairTable').bootstrapTable(
            "refresh", {
                url:SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新(先销毁后初始化)
    projectQuery: function(parame) {
        //方法名
        var methodName = "/templates/AdminTemplatesList";

        if (parame == "" || parame == undefined) {
            var obj = {
                Page: {
                    PageSize: 10,//页面大小,
                    PageIndex: 1,//页码
                }
            };
        } else {
            obj = parame;
        }

        $('#armchairTable').bootstrapTable(
            "destroy", {
                url:SignRequest.urlPrefix + methodName,
                query: obj
            }
        );

        freightTemplate.initBootstrapTable();

    }
}

$(function () {
    freightTemplate.init();
})