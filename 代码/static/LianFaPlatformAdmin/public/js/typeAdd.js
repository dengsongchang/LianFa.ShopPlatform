var typeAdd = {
    TapBoxTpl:`
            {{each AttrValueList as v i}}
                <span class="label label-default tag" data-id={{AttrValueList[i].AttrValueId}}>
                    <span class="text-info">#
                        <span>{{AttrValueList[i].AttrValue}}</span>#</span>
                    <a href="javascript:void(0);" data-id="{{AttrValueList[i].AttrValueId}}"  class="delTap">×</a>
                </span>
            {{/each}}
    `,
    init:function(){
        typeAdd.initBootstrapTable()
        //添加规格属性模态框的确认点击
        $('body').on('click','#addProperty-confirm',function(){
            //属性名称
            if (!Validate.emptyValidateAndFocus("#addProperymingchen", "请输入属性名称", "")) {
                return false;
            }
            typeAdd.addAttributes()
        })
        //回车添加属性值
        $('#receive_key_ipt').keydown(function(e){
            e = e || event;
            console.log(e)
            //获取id
            var id = $('#addPropertyValueModal').attr('data-id');
            if (e.keyCode == "13") { //keyCode=13是回车键
                var this_val = $(this).val();
                if ($(this).val() == "") {
                    Common.showErrorMsg('属性值不能为空')
                    return false;
                }
                typeAdd.addAttrValue(id,$(this).val());
            }

        });
        //点击添加属性值,记录点击的是谁
        $("#addPropertyValueModal").on("show.bs.modal", function(e) {
            //获取是哪个元素点击
            var invoker_dom = $(e.relatedTarget);
            //获取触发的该元素父元素所属的data-id
            var dataId=invoker_dom.attr('data-id');
            //此时给该弹窗赋予data-id，与触发元素的父元素data-id相对应
            $("#addPropertyValueModal").attr('data-id',dataId);
            typeAdd.attrValueList(dataId);
        });
        //点击删除属性值
        $('body').on('click','.delTap',function(){
            var id = $(this).attr('data-id');
            //调用删除接口
            Common.confirmDialog('确认要删除?',function(){
                typeAdd.delAttrValue(id)
            })

        });
        //点击删除属性值
        $('body').on('click','.delete-txt',function(){
            var id = $(this).attr('data-id');
            //调用删除接口
            Common.confirmDialog('确认要删除?',function(){
                typeAdd.delAttrValue(id)
            })
        });

        //点击删除属性
        $('body').on('click','.DelectBtn',function(){
            var id = $(this).attr('data-id');
            Common.confirmDialog('确认要删除?',function(){
                typeAdd.delAttributes(id);
            })

        });
        //排序修改
        $('body').on('change','.order-disp',function(){
            var id = $(this).attr('data-id');
            var sort = $(this).val();
            typeAdd.ChangeAttributesDisplayOrder(id,sort)
        })
        //规格名称改变
        $('body').on('change','.attr_edit',function(){
            var id = $(this).attr('data-id');
            var name = $(this).val();
            typeAdd.editAttributes(id,name)
        })



    },
    //后台改变规格属性排序
    ChangeAttributesDisplayOrder:function(id,sort){
        //请求方法
        var methodName = "/productSku/ChangeAttributesDisplayOrder";
        var data = {
            "AttrId": id,
            "DisplayOrder": sort
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('修改成功',function(){
                    typeAdd.projectQuery()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台改变属性名称
    editAttributes:function(id,name){
        //请求方法
        var methodName = "/productSku/EditAttributes";
        var data = {
            "AttrId": id,
            "Name": name
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('修改成功',function(){
                    typeAdd.projectQuery()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台删除规格属性
    delAttributes:function(id){
        //请求方法
        var methodName = "/productSku/DelAttributes";
        var data = {
            "AttrId": id
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功',function(){
                    typeAdd.projectQuery()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台删除属性值
    delAttrValue:function(id){
        //请求方法
        var methodName = "/productSku/DelAttrValue";
        var data = {
            "AttrValueId": id
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功',function(){
                    var dataId = $("#addPropertyValueModal").attr('data-id');
                    typeAdd.attrValueList(dataId);
                    typeAdd.projectQuery();
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //后台属性值列表
    attrValueList:function(id){
        //请求方法
        var methodName = "/productSku/AttrValueList";
        var data = {
            "AttrId": id
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(typeAdd.TapBoxTpl);
                var html = render(data.Data);
                $("#TapBox").html(html);

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //添加规格属性
    addAttributes:function(){
        //请求方法
        var methodName = "/productSku/AddAttributes";
        var data = {
            "AttrGrouPId": localStorage.getItem("typeId"),
            "Name": $('#addProperymingchen').val(),
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg("添加成功",function(){
                    typeAdd.projectQuery();
                    $('#addProperymingchen').val("");
                    //关闭模态框
                    $('#addPropertyModal').modal('hide');
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //添加规格属性值
    addAttrValue:function(id,this_val){
        //请求方法
        var methodName = "/productSku/AddAttrValue";
        var data = {
            "AttrId": id,
            "AttrValueList": [this_val],

        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg("增加属性值成功",function(){
                    var dataId = $("#addPropertyValueModal").attr('data-id');
                    typeAdd.attrValueList(dataId);
                    typeAdd.projectQuery()
                });

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#orderBox').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/productSku/AttributesList',
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
            queryParams: typeAdd.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: typeAdd.responseHandler,
            columns: [

                {
                    field: 'Name',
                    title: '属性名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = `<input type="text" class="attr_edit" data-id="${row.AttrId}" value="${value}">`
                        return html;
                    }
                },
                {
                    field: 'AttrValueList',
                    title: '属性值',
                    align: 'center',
                    valign: 'middle',
                    width:280,
                    formatter: function (value, row, index) {
                        var html = "";
                        for(var i=0;i<value.length;i++){
                            html += `<span class="property-value-span">
                                        <a class="shuxingzhi-txt">${value[i].AttrValue}</a>
                                        <a class="delete-txt" data-id="${value[i].AttrValueId}">x</a>
                                    </span>`
                        }
                        return html;
                    }
                },
                {
                    field: 'DisplayOrder',
                    title: '排序',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = `<input type="number" class="order-disp" data-id="${row.AttrId}" value="${value}">`
                        return html;
                    }
                },

                {
                    field: 'AttrId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html =
                            '<a data-toggle="modal" data-id="' + value + '" style="color:#44b8fd;cursor: pointer;margin-right: 5px" data-target="#addPropertyValueModal">添加属性值</a>'+
                            '<a class="" href="/classify/typeEdit?id=' + value + '" style="color:#44b8fd;cursor: pointer;margin-right: 5px"  data-id="' + value + '" >编辑</a>' +
                            '<span style="padding: 0 6px;color:#44b8fd;cursor: pointer" class="DelectBtn" data-id="' + value + '">删除</span>';
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
                $('.amount').html(data.amount);
                $('.number2').html(data.total)

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
        var methodName = "/productSku/AttributesList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "AttrGroupId": localStorage.getItem('typeId')



        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.AttributesList,
                "total": res.Data.Total,
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
                "AttrGroupId": localStorage.getItem('typeId')

            };
        } else {
            var obj = parame;
        }

        $('#orderBox').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/productSku/AttributesList',
                query: obj
            }
        );

    },
    //表格先销毁刷新
    projectDestoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {

                Page: {
                    PageSize: 10,//页面大小,
                    PageIndex: 1,//页码
                },
                "AttrGroupId": localStorage.getItem('typeId')

            };
        } else {
            var obj = parame;
        }

        $('#orderBox').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/productSku/AttributesList',
                query: obj
            }
        );
        typeAdd.initBootstrapTable()
    },
};
$(function () {

    typeAdd.init()

});
//公共函数（处理获取出来的属性值以逗号隔开的，转成数组）
function propertyVal_into_Array(val){
    var array_val=val.split(',');
    for(var i = 0 ;i<array_val.length;i++)
    {//去除空数组元素
        if(array_val[i] == "" || typeof(array_val[i]) == "undefined")
        {
            array_val.splice(i,1);
            i= i-1;
        }
    }
    return array_val;
}
