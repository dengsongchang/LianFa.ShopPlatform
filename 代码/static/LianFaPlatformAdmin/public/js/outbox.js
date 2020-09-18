var OutBox = {
    userRankTplList: `
        {{each UserRanksList as value i}}
        <option value="{{UserRanksList[i].UserRId}}">{{UserRanksList[i].Title}}</option>
        {{/each}}
    `,
    //判断是指定会员等级还是指定会员(1代表指定)
    userRankFlag:"1",
    //标题
    title:"",
    //内容
    content:"",
    init: function () {
        OutBox.initBootstrapTable();
        OutBox.adminUserRanksList();
        $('.nav-tabs').find('a').on('click', function () {
            $(".write").show();  //初始化，第一步显示
            $(".secondWrite").hide();  //初始化，第二步隐藏
            $('#mailName').val('');
            $('#wContent').val("")
        })
        // 分页条数设置
        $("#pagesize_dropdown").on("change", function () {
            OutBox.projectDectoryQuery();
        });
        //全选与取消全选
        $(".allCheck").click(function () {
            if (this.checked == true) {
                $(".isCheck").each(function () {
                    // this.checked==true
                    $(this).attr("checked", "checked");
                })
            } else {
                $(".isCheck").each(function () {
                    // this.checked==false;
                    $(this).removeAttr("checked");
                })
            }
        })
        //    根据发送对象显示下面的内容
        $(".secondWrite").on("click", "#sendObject", function () {
            console.log($('input:radio[name="sendObject"]:checked').val());
            if ($('input:radio[name="sendObject"]:checked').val() == "0") {
                $(".memberName").hide();
                $(".memberGrade").show();
                OutBox.userRankFlag = 2;
            }
            if ($('input:radio[name="sendObject"]:checked').val() == "1") {
                $(".memberName").show();
                $(".memberGrade").hide();
                OutBox.userRankFlag = 1;
            }
        })
        //    表单教验
        $("#submit").click(function () {
            //标题验证
            if (!Validate.emptyValidateAndFocus("#mailName", "请输入标题", "")) {
                return false;
            }
            //内容验证
            if (!Validate.emptyValidateAndFocus("#wContent", "请输入内容", "")) {
                return false;
            }
            $(".write").hide();  //点下一步按钮时，第一步隐藏
            $(".secondWrite").show();  //点下一步按钮时，第二步显示
            //记录标题跟内容
            OutBox.title = $('#mailName').val();
            OutBox.content = $('#wContent').val();
            // OutBox.adminAddStandInsideletter()
        });
        //点击发送
        $('body').on('click','#submitBtn',function(){
            var useList = $('#userList').val().split(",")
            OutBox.adminAddStandInsideletter(useList)
        })
        //点击删除
        $('body').on('click', '.status_delete', function () {
            var id = Number($(this).attr('data-id'));
            var list = [id];
            var target = $(this);
            Common.confirmDialog('确认删除?', function () {
                OutBox.delStandInsideletter(list, target)
            })

        })
        //删除全部按钮
        $('body').on('click', '.deleteBtn', function () {
            var list = [];
            $('.isCheck').each(function (index, item) {
                if (this.checked) {
                    list.push($(item).attr('data-id'))
                }
            })
            Common.confirmDialog('确认删除?', function () {
                OutBox.delStandInsideletter(list)
            })

        })

    },
    //增加站内信
    adminAddStandInsideletter: function (useList) {
        var methodName = "/standinsideletter/AdminAddStandInsideletter";
        var data = {
            "Title": OutBox.title,
            "Content": OutBox.content,
            "ObjectType": OutBox.userRankFlag,
        };
        if(OutBox.userRankFlag == "1"){
            data.UsreName = useList;
        }else if(OutBox.userRankFlag == "2"){
            data.UserRid = $('#userRank option:selected').val();
        }
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('增加站内信成功!', function () {
                    $('#mailName').val("");
                    $('#wContent').val("");
                    $('#userList').val("");
                    OutBox.projectQuery()
                    $('.nav-tabs').find('a')[0].click();
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function () {
        $('#memberList_Box').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/standinsideletter/AdminStandInsideletterOutboxList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
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
            queryParams: OutBox.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: OutBox.responseHandler,
            columns: [
                {
                    field: 'Title',
                    title: '标题',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = `<div style="position: relative"><input type="checkbox" data-id=${row.Silid} style="position: absolute;left: 7px;top: 3px" class="isCheck">
                                        <span>${value}</span></div>`;
                        return e;
                    }
                },
                {
                    field: 'Email',
                    title: '发件人',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
                    }
                },
                {
                    field: 'AddTime',
                    title: '时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>value</span>';

                        return e;
                    }
                },
                {
                    field: 'Content',
                    title: '内容',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {

                        var e = '<span>' + value + '</span>';

                        return e;
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
        var methodName = "/standinsideletter/AdminStandInsideletterOutboxList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            Page: {
                PageSize: params.limit,
                PageIndex: (params.offset / params.limit) + 1,
            },
            "sdstate": 0

        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.StandInsideletterList,
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
                "sdstate": 0
            };
        } else {
            var obj = parame;
        }

        $('#memberList_Box').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + '/standinsideletter/AdminStandInsideletterOutboxList',
                query: obj
            }
        );

    },
    //表格刷新
    projectDectoryQuery: function (parame) {
        if (parame == "" || parame == undefined) {
            var obj = {

                page: {
                    PageSize: $("#pagesize_dropdown").val(),
                    PageIndex: 1
                },
                "sdstate": 0
            };
        } else {
            var obj = parame;
        }

        $('#memberList_Box').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + '/standinsideletter/AdminStandInsideletterOutboxList',
                query: obj
            }
        );
        OutBox.initBootstrapTable()
    },
    //删除站内信
    delStandInsideletter: function (siLid, target) {
        var methodName = "/standinsideletter/AdminDelStandInsideletter";
        var data = {
            "siLid": siLid,
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('删除成功!', function () {
                    OutBox.projectQuery()
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取会员等级列表
    adminUserRanksList: function () {
        var methodName = "/standinsideletter/AdminUserRanksList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(OutBox.userRankTplList);
                var html = render(data.Data);
                $("#userRank").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
};

$(function () {

    OutBox.init()

})