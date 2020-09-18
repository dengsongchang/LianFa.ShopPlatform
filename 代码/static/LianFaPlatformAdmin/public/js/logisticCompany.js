$(function () {
    //点击开启状态的图片
    $('.main_list_content').on('click', '#logistic_info_tb td img', function () {
        var dataId = $(this).parents('tr').find('.bianji').attr('data-id');
        logisticObj.openOrclose(dataId);
    });
    logisticObj.init();

});
var logisticObj = {
    init: function () {
        logisticObj.listInit();
        //点击查询按钮
        $('#search_company_btn').click(function () {
            var companyNameTxt = $('#companyNameTxt').val();
            console.log(companyNameTxt);
            logisticObj.projectQuery(companyNameTxt);
        });
        //点击编辑按钮
        $('body').on('click', '.bianji', function () {
            var name = $(this).parents('tr').find('td').eq(0).text();
            var id = $(this).attr('data-id')
            $("#editLogisticModal").attr('data-id',id)
            $('#edit_Logistic_input').val(name)
            $("#editLogisticModal").modal('show');
        })
        //点击编辑属性值,记录点击的是谁
        $("#editLogisticModal").on("show.bs.modal", function (e) {
            //获取是哪个元素点击
            var invoker_dom = $(e.relatedTarget);
            console.log(e)
            //获取触发的该元素父元素所属的data-id
            var dataId = invoker_dom.parents('tr').find('.bianji').attr('data-id');
            //此时给该弹窗赋予data-id，与触发元素的父元素data-id相对应
            $("#editLogisticModal").attr('data-id', dataId);

        });
        //点击删除
        $('body').on('click', '.delectBtn', function () {
            var id = $(this).attr('data-id');
            Common.confirmDialog("是否要删除此公司?",function(){
                logisticObj.adminDelShipCompanie(id);
            })

        })
        //点击确认编辑按钮
        $('#edit_Logistic_confirm').click(function () {
            logisticObj.editCompany();

        });

        //点击 确认添加按钮
        $('#add_Logistic_confirm').click(function () {
            logisticObj.addCompany();
        });


    },
    listInit: function () {
        $('#logistic_info_tb').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/shipcompanie/AdminShipCompanieList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: 2,
            pageNumber: 1,
            pageList: [10, 20, 50, 100, 200, 500, 1000, 2000, 5000, 10000],
            idField: "ShipCoId", //标识哪个字段为id主键
            showToggle: false, //名片格式
            cardView: false, //设置为True时显示名片（card）布局
            // showColumns: true, //显示隐藏列
            // showRefresh: true, //显示刷新按钮
            singleSelect: false, //复选框只能选择一条记录
            search: false, //是否显示右上角的搜索框
            clickToSelect: true, //点击行即可选中单选/复选框
            sidePagination: "server", //表格分页的位置
            queryParams: logisticObj.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: logisticObj.responseHandler,
            columns: [
                // {
                //     field: 'UId',
                //     title: '',
                //     align: 'center',
                //     valign: 'middle',
                //     formatter: function(value, row, index) {
                //         var html = '<input type="checkbox" class="checkbox" data-uid="'+ value +'">'
                //         return html;
                //     }
                // },
                {
                    field: 'Name',
                    title: '物流公司',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'State',
                    title: '开启状态',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {


                        if (value == 1) {
                            var e = '<img src="../public/images/dui.png" alt="">';
                        }
                        else {
                            var e = '<img src="../public/images/cuo.png" alt="">';
                        }
                        // <img src="../public/images/cuo.png" alt="">

                        return e;
                    }
                },
                {
                    field: 'ShipCoId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        //var e = '<span>'+value+'</span>';;

                        var e = '<a class="bianji" style="margin-right: 5px;cursor: pointer" data-id=' + value + '>编辑</a>';
                        e += '<a class="delectBtn" style="cursor: pointer" data-id=' + value + '>删除</a>'
                        return e;
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
                //$('.caret').remove()

            },
            onLoadError: function (data) {
                //$('#dishes_list_table').bootstrapTable('removeAll');
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
    queryParams: function (params) {//请求参数
        //配置参数
        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            Name: $('#companyNameTxt').val(),
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        console.log('分割线')
        console.log(res)
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.ShipCompanieList,
                // "total": res.Data.ShipCompanieList.length
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
        var obj = {};       
        if (parame == "" || parame == undefined) {
            obj = {};
        } else {
            obj = parame;
        }
        //方法名
        var methodName = "/credit/AdminCreditList";

        $('#logistic_info_tb').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/shipcompanie/AdminShipCompanieList',
                query: obj
            }
        );
    },
    //添加公司
    addCompany: function () {

        var companName = $('#add_Logistic_input').val();
        if (companName != '') {
            //请求方法
            var methodName = "/shipcompanie/AdminAddShipCompanie";
            var data = {
                "Name": companName
            };
            //请求接口
            SignRequest.set(methodName, data, function (data) {
                if (data.Code == "100") {

                    logisticObj.projectQuery(companName);
                    $('#addLogisticModal').modal('hide');
                    swal("添加成功", "", "success");
                } else {
                    Common.showErrorMsg(data.Message);
                }
            });
        }
        else {
            swal("请输入完整信息", "", "error");
        }

    },
    //编辑公司
    editCompany: function () {
        var edit_Logistic_input = $('#edit_Logistic_input').val();
        var dataId = $("#editLogisticModal").attr('data-id');
        var companName = $('#edit_Logistic_input').val();
        if (companName != '') {
            //请求方法
            var methodName = "/shipcompanie/AdminEditShipCompanie";
            var data = {
                "ShipCoId": dataId,
                "Name": companName
            };
            //请求接口
            SignRequest.set(methodName, data, function (data) {
                if (data.Code == "100") {
                    logisticObj.projectQuery(companName);
                    $('#editLogisticModal').modal('hide');
                    swal("编辑成功", "", "success");
                } else {
                    Common.showErrorMsg(data.Message);

                }
            });
        }
        else {
            swal("请输入完整信息", "", "error");
        }

    },
    //删除公司
    //后台删除配送公司
    adminDelShipCompanie: function (id) {

        //请求方法
        var methodName = "/shipcompanie/AdminDelShipCompanie";
        var data = {
            "ShipCoId": id,
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {

                Common.showSuccessMsg("删除成功", function () {
                    logisticObj.projectQuery();
                })

            } else {
                Common.showErrorMsg(data.Message);

            }
        });

    },
    //开启或关闭
    openOrclose: function (ShipCoId) {
        var methodName = "/shipcompanie/AdminChangeShipCompanieState";
        var data = {
            "ShipCoId": ShipCoId
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                logisticObj.projectQuery(data);
                swal("开启状态已改变", "", "success");
            } else {

            }
        });
    },
}