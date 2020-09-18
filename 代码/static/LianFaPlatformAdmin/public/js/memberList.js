$(function () {
    MemberList.init();
})

var MemberList = {
    userLabelTpl: `
        {{each UserLabelList as value i}}
        <option value="{{UserLabelList[i].LId}}">{{UserLabelList[i].Title}}</option>
        {{/each}}
    `,
    setLabelTpl: `
        {{each UserLabelList as value i}}
        <label class="checkbox-inline">
            <input class="set-checkbox" type="checkbox" value="{{UserLabelList[i].LId}}"> {{UserLabelList[i].Title}}
        </label>
        {{/each}}
    `,
    userRankTpl: `
        {{each UserRankList as value i}}
        <option value="{{UserRankList[i].UserRId}}">{{UserRankList[i].Title}}</option>
        {{/each}}
    `,
    gradeTpl: `
        {{each AdminDistributionGradeList as value i}}
            <option value="{{AdminDistributionGradeList[i].Level}}">{{AdminDistributionGradeList[i].Title}}</option>
        {{/each}}
    `,
    UId: 0,
    init: function () {

        MemberList.adminDistributionGradeList();


        //点击调整等级按钮
        $('body').on('click', '.adjustGradeBtn', function () {
            var title = $(this).attr('data-title');
            var name = $(this).attr('data-name');
            var id = $(this).attr('data-id');
            $('#adjustBtn').attr('data-id', id)
            $('#memberName').val(name)
            $('#currentGrade').val(title);

        })

        //点击调整分销员等级按钮
        $('body').on('click', '#adjustBtn', function () {
            MemberList.adjustGrade();
        })

        //点击添加熊豆按钮
        $('body').on('click', '.addIntegralBtn', function () {
            var id = $(this).attr('data-id');
            $('#addIntegralBtn').attr('data-id', id)
        })

        //点击添加熊豆按钮
        $('body').on('click', '#addIntegralBtn', function () {
            MemberList.adminEditCredit();
        })


        //关闭弹窗
        $(".mask").on("click", ".close", function () {
            $(".mask").hide();
        });

        //全选
        $("#stuCheckBox").on("change", function () {
            if ($(this).is(':checked')) {
                $(".checkbox").prop("checked", true);
            }
            else {
                $(".checkbox").prop("checked", false);
            }
        });

        // 优惠券全选
        $("#couponTabel").on("change", "#selectAllCoupon", function () {
            if ($(this).is(':checked')) {
                $(".coupon-checkbox").prop("checked", true);
            }
            else {
                $(".coupon-checkbox").prop("checked", false);
            }
        });

        //注册时间排序
        $("#OrderBy").on("change", function () {
            MemberList.projectQuery();
        });

        //每页显示数量
        $("#pagesize_dropdown").on("change", function () {
            MemberList.projectQuery();
        });

        //查询
        $("#search").on("click", function () {
            MemberList.projectQuery();
        });

        //导出
        $("#export").on("click", function () {
            if (MemberList.getCurrentPageIndexWithBootstrap() > 0) {
                Common.confirmDialog("确认导出当前筛选条件下的当前页数据？", function () {
                    MemberList.exportMemberListData();
                });
            }
            else {
                Common.showInfoMsg("当前无数据可导出");
            }
        });

        //多个删除
        $("#delete").on("click", function () {
            if (MemberList.getSelectedData().length > 0) {
                Common.confirmDialog("确认对选中的数据进行删除吗？", function () {
                    MemberList.deleteMember(MemberList.getSelectedData());
                });
            }
            else {
                Common.showInfoMsg("请选择需要删除的会员");
            }
        });

        //单个删除
        $("#memberTable").on("click", ".status_delete", function () {
            var UId = [];
            UId.push($(this).attr("data-id"));
            Common.confirmDialog("确认删除该会员吗？", function () {
                MemberList.deleteMember(UId);
            });
        });

        //查看
        $("#memberTable").on("click", ".status_see", function () {
            var UId = $(this).attr("data-id");
            location.href = "/member/memberDetailInfo?UId=" + UId;
        });

        //编辑
        $("#memberTable").on("click", ".status_edit", function () {
            var UId = $(this).attr("data-id");
            location.href = "/member/memberEdit?UId=" + UId;
        });

        //查看标签
        $("#memberTable").on("click", ".see-label", function () {
            var UId = $(this).attr("data-id");
            MemberList.getUserInfoLabelListWithUId(UId);
        });

        //多个设置标签
        $("#setLabel").on("click", function () {
            if (MemberList.getSelectedData().length > 0) {
                $(".set-checkbox").prop("checked", false);
                $(".mask").show();
                $("#setLabelBox").show();
                $("#seeLabelBox").hide();
                $("#sendCoupBox").hide();
            }
            else {
                Common.showInfoMsg("请选择需要设置标签的会员");
            }
        });

        //多个赠送优惠券
        $("#sendCoup").on("click", function () {
            if (MemberList.getSelectedData().length > 0) {
                MemberList.initCouponBootstrapTable();
                $(".mask").show();
                $("#setLabelBox").hide();
                $("#seeLabelBox").hide();
                $("#sendCoupBox").show();
            }
            else {
                Common.showInfoMsg("请选择需要赠送优惠券的会员");
            }
        });

        //确定赠送优惠券
        $("#coupConfirm").on("click", function () {
            if (MemberList.getSelectedDataWithCoupon().length > 0) {
                MemberList.sendUserCoupon();
            }
            else {
                $(".mask").hide();
                Common.showInfoMsg("未选择优惠券");
            }
        });

        // 优惠券查询
        $("#couponSearch").on("click", function () {
            MemberList.refreshCouponQuery();
        });

        // 批量设置会员标签
        $("#labelConfirm").on("click", function () {
            var LId = [];
            var laelList = $("#setLaelList input");
            for (var i = 0; i < laelList.length; i++) {
                if (laelList.eq(i).is(':checked')) {
                    LId.push(laelList.eq(i).val());
                }
            }
            if (LId.length > 0) {
                MemberList.setUserLabel(LId, MemberList.getSelectedData());
            } else {
                Common.showInfoMsg('请选择标签')
            }
        });

        // 单个查看设置会员标签
        $("#seeConfirm").on("click", function () {
            var LId = [];
            var laelList = $("#seeLaelList input");
            for (var i = 0; i < laelList.length; i++) {
                if (laelList.eq(i).is(':checked')) {
                    LId.push(laelList.eq(i).val());
                }
            }

            var UId = [];
            UId.push($("#seeConfirm").attr("data-id"));
            if (LId.length > 0) {
                MemberList.setUserLabel(LId, UId);
            } else {
                Common.showInfoMsg('请选择标签')
            }


        });

        //会员导入

        $("#import input").on("change", function () {
            var file = $(this)[0].files[0];
            var url = "/user/AdminUploadUserList";
            console.log()
            MemberList.setFile(file, url)
        });
        //会员关系导入
        $("#importRelation input").on("change", function () {
            var file = $(this)[0].files[0];
            var url = "/user/AdminUploadUserRelation";
            MemberList.setFile(file, url, true)
        });
        //下载模板按钮点击
        $('body').on('click', '#downloadBtn', function () {
            MemberList.downloadDiseasesUploadTemplate();
        })
        $('body').on('click','.status_order',function(){
            var id = $(this).attr('data-id');
            location.href = '/order/orderList?UId='+ id + ''
        })
        $('body').on('click','.status_corder',function(){
            var id = $(this).attr('data-id');
            location.href = '/order/exchangeOrderList?UId='+ id + ''
        })

        // 初始化日期控件
        Common.initLaydateWithTime();

        MemberList.getUserRankList();
        MemberList.getUserLabelList();

        MemberList.initBootstrapTable();
    },
    //上传模板
    setFile: function (file, url, isRelative) {
        var formData = new FormData();
        formData.append('file', file);
        Common.showUploading();
        $.ajax({
            url: SignRequest.urlPrefix + url,
            type: "post",
            dataType: "json",
            data: formData,
            cache: false,
            processData: false,
            contentType: false
        }).done(function (data) {
            swal.close()
            if (data.Code == "100") {
                Common.showSuccessMsg("导入成功", function () {
                    //进行刷新操作==>
                    MemberList.projectQuery();
                })
                if (isRelative) {
                    $("#importFileRelation").val("");
                } else {
                    $("#importFile").val("");
                }

            } else {
                if (isRelative) {
                    $("#importFileRelation").val("");
                } else {
                    $("#importFile").val("");
                }
                Common.showErrorMsg(data.Message);
            }
        }).fail(function () {
            swal.close()
            if (isRelative) {
                $("#importFileRelation").val("");
            } else {
                $("#importFile").val("");
            }
            Common.showErrorMsg("上传文件失败");
        })
    },
    //下载模板
    downloadDiseasesUploadTemplate: function () {
        var methodName = "/user/DownloadUserTemplate";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                location.href = data.Data;
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取分销员等级列表
    adminDistributionGradeList: function () {
        var methodName = "/DistributionGrade/AdminDistributionGradeList";
        var data = {};
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(MemberList.gradeTpl);
                var html = render(data.Data);
                $("#setGrade").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //添加熊豆
    adminEditCredit: function () {
        var methodName = "/Credit/AdminEditCredit";
        var data = {
            "UId": $('#addIntegralBtn').attr('data-id'),
            "PayCredits": $('#integralNumber').val(),
            "AddOrReduce": $('input[name=saleState]:checked').val(),
            "Remark": "",
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('修改成功', function () {
                    MemberList.refreshQuery()
                    $('#addModal').modal('hide')
                    $('#integralNumber').val(0)
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //调整等级
    adjustGrade: function () {
        var methodName = "/Distribution/AdjustGrade";
        var data = {
            "UId": $('#adjustBtn').attr('data-id'),
            "DistGradeId": $('#setGrade').val(),
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                Common.showSuccessMsg('调整等级成功', function () {
                    MemberList.refreshQuery()
                    $('#myAdjustModal').modal('hide')
                    $('#setGrade').val(0)
                })
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取单个会员标签
    getUserInfoLabelListWithUId: function (UId) {
        var methodName = "/user/AdminGetUserInfoLabelList";
        var data = {
            UId: UId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var UserLabelList = data.Data.UserLabelList;
                var seeLaelList = $("#seeLaelList input");
                seeLaelList.prop("checked", false)
                for (var i = 0; i < UserLabelList.length; i++) {
                    for (var j = i; j < seeLaelList.length; j++) {
                        if (seeLaelList.eq(j).val() == UserLabelList[i].LId) {
                            seeLaelList.eq(j).prop("checked", true);
                        }
                    }
                }
                $("#seeConfirm").attr("data-id", UId);
                $(".mask").show();
                $("#setLabelBox").hide();
                $("#seeLabelBox").show();
                $("#sendCoupBox").hide();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取会员列表分页当前页数(bootstrap分页插件)
    getCurrentPageIndexWithBootstrap: function () {
        var thisPage = $("#memberTableDiv").find(".fixed-table-pagination").find("ul").find(".active").find("a").text();
        return thisPage;
    },
    // 获取优惠券列表选中的数据
    getSelectedDataWithCoupon: function () {
        var list = $("#couponTabel .coupon-checkbox");
        var CouponTypeId = [];
        for (var i = 0; i < list.length; i++) {
            if (list.eq(i).is(':checked')) {
                CouponTypeId.push(list.eq(i).attr("data-id"));
            }
        }
        return CouponTypeId;
    },
    // 获取会员列表选中的数据
    getSelectedData: function () {
        var list = $("#memberTable .checkbox");
        var UId = [];
        for (var i = 0; i < list.length; i++) {
            if (list.eq(i).is(':checked')) {
                UId.push(list.eq(i).attr("data-id"));
            }
        }
        return UId;
    },
    // 赠送优惠券
    sendUserCoupon: function () {
        var methodName = "/user/AdminSendUserCoupons";
        var data = {
            CouponTypeId: MemberList.getSelectedDataWithCoupon(),
            UId: MemberList.getSelectedData()
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $(".mask").hide();
                Common.showSuccessMsg("优惠券赠送成功");
            } else {
                $(".mask").hide();
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 设置标签
    setUserLabel: function (LId, UId) {
        var methodName = "/user/AdminSetUserLabel";
        var data = {
            LId: LId,
            UId: UId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                $(".mask").hide();
                MemberList.refreshQuery();
                Common.showSuccessMsg("设置成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 导出
    exportMemberListData: function () {
        var methodName = "/user/ExportAdminUserList";

        var PageIndex = MemberList.getCurrentPageIndexWithBootstrap();
        var data = {
            UserName: $("#UserName").val(),
            NickName: $("#NickName").val(),
            RegisterTimeStart: $("#start").val(),
            RegisterTimeEnd: $("#end").val(),
            UserRId: $("#rankList").val(),
            UserLId: $("#labelList").val(),
            OrderBy: $("#OrderBy").val(),
            Mobile: $("#Mobile").val(),
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                location.href = data.Data;
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 删除会员
    deleteMember: function (UId) {
        var methodName = "/user/AdminDelUser";
        var data = {
            UId: UId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                MemberList.refreshQuery();
                Common.showSuccessMsg("删除成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取会员等级列表
    getUserRankList: function () {
        var methodName = "/user/AdminUserRankList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            console.log(data.Data);
            if (data.Code == "100") {
                var render = template.compile(MemberList.userRankTpl);
                var html = render(data.Data);
                $("#rankList").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取会员标签列表
    getUserLabelList: function () {
        var methodName = "/user/AdminGetUserLabelList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(MemberList.userLabelTpl);
                var html = render(data.Data);
                $("#labelList").append(html);

                // 设置标签
                var sRender = template.compile(MemberList.setLabelTpl);
                var shtml = sRender(data.Data);
                $("#setLaelList").html(shtml);
                $("#seeLaelList").html(shtml);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //会员列表bootstrapTable
    initBootstrapTable: function () {
        $('#memberTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/user/AdminUserList',
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
            queryParams: MemberList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: MemberList.responseHandler,
            columns: [
                {
                    field: 'UId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-id="' + value + '">'
                        return html;
                    }
                },
                {
                    field: 'UserName',
                    title: '会员编号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'NickName',
                    title: '姓名昵称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Mobile',
                    title: '手机号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'OrderAmount',
                    title: '消费金额',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'OrderSum',
                    title: '订单数',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'UId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = "";
                        html += "<span class='status_order' style='color: #039cf8;cursor: pointer;margin-right: 10px' data-id='" + value + "'>普通订单列表</span>";
                        html += "<span class='status_corder'  style='color: #039cf8;cursor: pointer;margin-right: 10px' data-id='" + value + "'>兑换订单列表</span>";
                        html += "<span class='status_see' data-id='" + value + "'>查看</span>";
                        // html += "<span class='status_edit' data-id='" + value + "' style='margin-left: 10px;'>编辑</span>";
                        html += "<span class='status_delete' data-id='" + value + "' style='margin-left: 10px;'>删除</span>";
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
                $('#memberTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法

            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {

                // for (var i = 0; i < rows.length; i++) {
                //     dishes_list.UserIdsList.push(rows[i].User.Id);
                //     dishes_list.UserOpenIds.push(rows[i].User.OpenId);
                // }

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
    queryParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/user/AdminGetUserInfo";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            UserName: $("#UserName").val(),
            NickName: $("#NickName").val(),
            RegisterTimeStart: $("#start").val(),
            RegisterTimeEnd: $("#end").val(),
            UserRId: $("#rankList").val(),
            UserLId: $("#labelList").val(),
            OrderBy: $("#OrderBy").val(),
            Mobile: $("#Mobile").val(),
            Page: {
                PageSize: params.limit,//页面大小,
                PageIndex: (params.offset / params.limit) + 1,//页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.UserList,
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
    refreshQuery: function (parame) {
        //方法名
        var methodName = "/user/AdminUserList";

        if (parame == "" || parame == undefined) {
            var obj = {
                UserName: $("#UserName").val(),
                NickName: $("#NickName").val(),
                RegisterTimeStart: $("#start").val(),
                RegisterTimeEnd: $("#end").val(),
                UserRId: $("#rankList").val(),
                UserLId: $("#labelList").val(),
                OrderBy: $("#OrderBy").val(),
            };
        } else {
            var obj = parame;
        }

        $('#memberTable').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新（先销毁后初始化）
    projectQuery: function (parame) {
        //方法名
        var methodName = "/user/AdminUserList";

        if (parame == "" || parame == undefined) {
            var obj = {
                UserName: $("#UserName").val(),
                NickName: $("#NickName").val(),
                RegisterTimeStart: $("#start").val(),
                RegisterTimeEnd: $("#end").val(),
                UserRId: $("#rankList").val(),
                UserLId: $("#labelList").val(),
                OrderBy: $("#OrderBy").val(),
                Mobile: $("#Mobile").val(),
                Page: {
                    PageSize: $("#pagesize_dropdown").val(),//页面大小,
                    PageIndex: 1,//页码
                }
            };
        } else {
            var obj = parame;
        }


        $('#memberTable').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );

        MemberList.initBootstrapTable();
    },

    //优惠券列表bootstrapTable
    initCouponBootstrapTable: function () {
        $('#couponTabel').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/CouponType/AdminCouponTypeList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
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
            queryParams: MemberList.queryCouponParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbarCoupon", //设置工具栏的Id或者class
            responseHandler: MemberList.responseCouponHandler,
            columns: [
                {
                    field: 'CouponTypeId',
                    title: '<input type="checkbox" id="selectAllCoupon">',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '<input type="checkbox" class="coupon-checkbox" data-id="' + value + '" style="margin: 0 auto;">'
                        return html;
                    }
                },
                {
                    field: 'Name',
                    title: '优惠券名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Money',
                    title: '面额',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'RemainCount',
                    title: '剩余张数',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'UseMode',
                    title: '使用条件',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'UseStartTime',
                    title: '有效期',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = value.substring(0, 10) + "至" + row.UseEndTime.substring(0, 10);
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
                $('#memberTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {
                // 书写自己的方法

            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {

                // for (var i = 0; i < rows.length; i++) {
                //     dishes_list.UserIdsList.push(rows[i].User.Id);
                //     dishes_list.UserOpenIds.push(rows[i].User.OpenId);
                // }

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
    queryCouponParams: function (params) {
        //配置参数
        //方法名
        var methodName = "/CouponType/AdminCouponTypeList";

        var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
            minSize: $("#leftLabel").val(),
            maxSize: $("#rightLabel").val(),
            minPrice: $("#priceleftLabel").val(),
            maxPrice: $("#pricerightLabel").val(),
            CouponTypeName: $("#Name").val(),
            State: 0,
            SendMode: 1,
            Page: {
                PageSize: params.limit,//页面大小,
                PageIndex: (params.offset / params.limit) + 1,//页码
            }
        };
        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseCouponHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.CouponTypeList,
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
    refreshCouponQuery: function (parame) {
        //方法名
        var methodName = "/CouponType/AdminCouponTypeList";

        if (parame == "" || parame == undefined) {
            var obj = {
                CouponTypeName: $("#Name").val(),
                State: 0,
                SendMode: 1,
                Page: {
                    PageSize: 3,//页面大小,
                    PageIndex: 1,//页码
                }
            };
        } else {
            var obj = parame;
        }

        $('#couponTabel').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    }

}