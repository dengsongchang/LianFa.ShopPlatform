$(function () {
    giftCardList.init();
})

var giftCardList = {
    cateTpl: `
        {{each FirstCategoryList as value i}}
            <option value="{{FirstCategoryList[i].CateId}}">{{FirstCategoryList[i].Name}}</option>
        {{/each}}
    `,
    brandTpl: `
        {{each BrandList as value i}}
            <option value="{{BrandList[i].BrandId}}">{{BrandList[i].Name}}</option>
        {{/each}}
    `,
    couponTypeTpl: `

        {{each CouponTypeList as value i}}
            <option value="{{CouponTypeList[i].CouponTypeId}}">{{CouponTypeList[i].Name}}</option>
        {{/each}}
    `,
    flag: true,
    init: function () {

        if (localStorage.getItem('PageIndex')) {
            if (localStorage.getItem('PageSize')) {
                $('#pagesize_dropdown').val(localStorage.getItem('PageSize'))
            }
            giftCardList.initBootstrapTable();
            setTimeout(function () {
                $('.pagination').find('.page-number').each(function (index, item) {
                    $(item).removeClass('active');
                    if ($(item).find('a').text() == localStorage.getItem('PageIndex')) {
                        $(item).addClass('active');
                    }
                })
            }, 1000)
        } else {
            giftCardList.initBootstrapTable();
        }

        giftCardList.miniCouponTypeList();

        //导出商品列表按钮点击
        $('body').on('click', '.all_output', function () {
            giftCardList.exportAdmingiftCardList()
        })
        //下载模板按钮点击
        $('body').on('click', '#downloadBtn', function () {
            giftCardList.downloadDiseasesUploadTemplate();
        })

        // 查詢
        $("#search").on("click", function () {
            localStorage.setItem('PageIndex', 1)
            giftCardList.projectQuery();
        });

        var type = Common.getUrlParam("type");
        if (type) {
            $('#State').find('li').eq(type).addClass('active').siblings().removeClass('active');
        } else {
            $('#State').find('li').eq(0).addClass('active').siblings().removeClass('active');
        }

        // 切换状态
        // $("#State").on("click", "li", function () {
        //     localStorage.setItem('PageIndex', "1")
        //     localStorage.setItem('PageSize', "10")
        //     var tabNum = $(this).find("a").attr("data-type");
        //     console.log(tabNum);
        //     giftCardList.dataType = tabNum;
        //     location.href = '/product/productList?type=' + tabNum + ''
        //     if (!$(this).hasClass("active")) {
        //         setTimeout(giftCardList.projectQuery, 300);
        //     }
        // });
        laydate.render({
            elem: '#start_time', //指定元素
            type: 'datetime'
        });
        //全选
        $("#radio").on("change", function () {
            if ($(this).is(':checked')) {
                $(".checkbox").prop("checked", true);
            } else {
                $(".checkbox").prop("checked", false);
            }
        });
        //兑换按钮点击
        $('body').on('click', '.status_check', function () {
            var PId = $(this).attr("data-id");
            giftCardList.PId = PId;

        })

        //编辑
        $("#productTable").on("click", ".status_edit", function () {
            var PId = $(this).attr("data-id");
            var state = $(this).attr("data-type");
            var page = 1;
            $('.pagination').find('.page-number').each(function (index, item) {
                if ($(item).hasClass('active')) {
                    page = $(item).find('a').text()
                }
            })
            var size = $('#pagesize_dropdown').val();
            localStorage.setItem('PageIndex', page);
            localStorage.setItem('PageSize', size);
            var type = 0;
            $('#State').find('li').each(function (index, item) {
                if ($(item).hasClass('active')) {
                    type = $(item).find('a').attr('data-type');
                }
            })

            location.href = "/giftCard/giftCardDetail?PId=" + PId + "&type=" + type;


        });
        // 关闭弹窗
        $(".mask").on("click", ".close", function () {
            $(".mask").hide();
        });

        //多个编辑
        $("#editorMore").on("click", function () {
            Common.confirmDialog("确认对选中的数据进行修改吗？", function () {
                $(".mask").show();
                giftCardList.showEditor(giftCardList.getSelectedData());
            });
        });
        // 修改截至日期
        $("#productTable").on("click", ".status_editor", function () {
            var CouponId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(CouponId);
            $(".mask").show();
            giftCardList.showEditor(PIdArr);
        });
        //单个删除
        $("#productTable").on("click", ".status_delete", function () {
            var CouponId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(CouponId);
            Common.confirmDialog("确认进行删除吗？", function () {
                giftCardList.deleteProduct(PIdArr);
            });
        });

        //多个删除
        $("#radio_delete").on("click", function () {
            Common.confirmDialog("确认对选中的数据进行删除吗？", function () {
                giftCardList.deleteProduct(giftCardList.getSelectedData());
            });
        });

        //单个删除
        $("#productTable").on("click", ".status_cancel", function () {
            var CouponId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(CouponId);
            Common.confirmDialog("确认进行作废吗？", function () {
                giftCardList.cancelCoupon(PIdArr);
            });
        });

        //批量作废
        $("#cancelCoupon").on("click", function () {
            Common.confirmDialog("确认对选中的数据进行作废吗？", function () {
                giftCardList.cancelCoupon(giftCardList.getSelectedData());
            });
        });

        //下架
        $("#productTable").on("click", ".status_close", function () {
            var PId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(PId);
            Common.confirmDialog("确认进行下架操作吗？", function () {
                giftCardList.outSaleProduct(PId);
            });
        });
        //上架
        $("#productTable").on("click", ".status_add", function () {
            var PId = $(this).attr("data-id");
            var PIdArr = [];
            PIdArr.push(PId);
            Common.confirmDialog("确认进行上架操作吗？", function () {
                giftCardList.onSaleProduct(PId);
            });
        });
        //导入

        $("#import input").on("change", function () {
            var file = $(this)[0].files[0];
            var url = "/Coupon/AdminUploadCouponList";
            giftCardList.setFile(file, url)
        });

        //表格分页每页显示数据
        $("#pagesize_dropdown").on("change", function () {
            giftCardList.projectQuery();
        });
        giftCardList.initBootstrapTable();
    },
    //下载模板
    downloadDiseasesUploadTemplate: function () {
        var methodName = "/Coupon/DownloadUploadTemplate";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                location.href = data.Data;
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }, //点击修改保存按钮
    showEditor: function (id) {
        $(".mask").on("click", "#editorBtn", function () {
            var UseEndTime = $('#start_time').val()
            giftCardList.editCredit(UseEndTime, id);
        })
    },
    //修改截至
    editCredit: function (UseEndTime, CouponId) {
        //请求方法
        var methodName = "/Coupon/AdminEditCouponTime";
        var data = {
            UseEndTime: UseEndTime,
            CouponId: CouponId
        };
        //请求接口
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('修改成功');
                $(".mask").hide();
                giftCardList.projectQuery();
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //上传模板
    setFile: function (file, url) {
        var formData = new FormData();
        formData.append('file', file);
        $.ajax({
            url: SignRequest.urlPrefix + url,
            type: "post",
            dataType: "json",
            data: formData,
            cache: false,
            processData: false,
            contentType: false
        }).done(function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("导入成功")
                //进行刷新操作==>
                $("#importFile").val("");
                giftCardList.projectQuery();
            } else {
                $("#importFile").val("");
                Common.showErrorMsg(data.Message);
            }
        }).fail(function () {
            $("#importFile").val("");
            Common.showErrorMsg("上传文件失败");
        })
    },
    //后台导出商品列表列表
    exportAdmingiftCardList: function (list) {
        var methodName = "/product/ExportAdmingiftCardList";
        var data = {
            State: Number(Common.getUrlParam('type')) ? Common.getUrlParam('type') : 0,
            Name: $("#Name").val(),
            PSn: $("#PSn").val(),
            CateId: $("#CateId").val(),
            BrandId: $("#BrandId").val(),
            PlId: $("#Label").val(),
            TemplateId: $("#TemplateId").val(),
            Type: $("#Type").val(),
            ModuleType: $('#ModuleType').val(),
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                location.href = data.Data
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取选中的数据
    getSelectedData: function () {
        var list = $("#productTable .checkbox");
        var CouponId = [];
        for (var i = 0; i < list.length; i++) {
            if (list.eq(i).is(':checked')) {
                CouponId.push(list.eq(i).attr("data-id"));
            }
        }

        return CouponId;
    },
    // 下架
    outSaleProduct: function (PId, isAll) {
        var methodName = "/product/AdminOutSaleProduct";
        var data = {
            PId: PId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                if (isAll) {
                    giftCardList.projectQuery();
                } else {
                    giftCardList.refreshQuery();
                }
                Common.showSuccessMsg("下架成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //上架
    onSaleProduct: function (PId, isAll) {
        var methodName = "/product/AdminOnSaleProduct";
        var data = {
            PId: PId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                if (isAll) {
                    giftCardList.projectQuery();
                } else {
                    giftCardList.refreshQuery();
                }
                Common.showSuccessMsg("上架成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 删除
    deleteProduct: function (CouponId) {
        var methodName = "/Coupon/AdminDelCoupon";
        var data = {
            CouponId: CouponId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                giftCardList.projectQuery();
                Common.showSuccessMsg("删除成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 删除
    cancelCoupon: function (CouponId) {
        var methodName = "/Coupon/AdminCancelCoupon";
        var data = {
            CouponId: CouponId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                giftCardList.projectQuery();
                Common.showSuccessMsg("作废成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 礼品卡类型
    miniCouponTypeList: function () {
        var methodName = "/Coupon/MiniCouponTypeList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(giftCardList.couponTypeTpl);
                var html = render(data.Data);
                $("#couponType").append(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //bootstrapTable
    initBootstrapTable: function (isEdit) {
        $('#productTable').bootstrapTable({
            method: 'post',
            url: SignRequest.urlPrefix + '/Coupon/CouponList',
            dataType: "json",
            striped: true, //使表格带有条纹
            pagination: true, //在表格底部显示分页工具栏
            pageSize: $("#pagesize_dropdown").val(),
            pageNumber: giftCardList.flag ? localStorage.getItem('PageIndex') : 1,
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
            queryParams: giftCardList.queryParams, //参数
            queryParamsType: "limit", //参数格式,发送标准的RESTFul类型的参数请求
            toolbar: "#toolbar", //设置工具栏的Id或者class
            responseHandler: giftCardList.responseHandler,
            columns: [{
                    field: 'CouponId',
                    title: '',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = '<input type="checkbox" class="checkbox" data-id="' + value + '">'
                        return html;
                    }
                },
                {
                    field: 'Name',
                    title: '礼品卡名称',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'CouponSn',
                    title: '礼品卡类型序列号',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'Password',
                    title: '密码',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'AddTimes',
                    title: '购买时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'UseEndTimes',
                    title: '截止日期',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'UseTimes',
                    title: '兑换时间',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        return value;
                    }
                },
                {
                    field: 'StateDec',
                    title: '状态',
                    align: 'center',
                    valign: 'middle',
                },
                {
                    field: 'CouponId',
                    title: '操作',
                    align: 'center',
                    valign: 'middle',
                    formatter: function (value, row, index) {
                        var html = "";
                        if (row.State == 1) {
                            html += "<span class='status_edit' style='margin-right: 15px' data-id='" + value + "' data-type='" + row.ModuleType + "'>详情</span>";
                            html += "<span class='status_editor' data-id='" + value + "'>修改</span>";
                            // html += "<span class='status_close' data-id='" + value + "' style='margin-left: 10px;'>下架</span>";
                        } else if (row.State == 0) {
                            html += "<span class='status_edit' style='margin-right: 15px' data-id='" + value + "'>详情</span>";
                            // html += "<span class='status_check' data-id='" + value + "'>兑换</span>";
                            html += "<span class='status_cancel' style='margin-right: 15px'  data-id='" + value + "'>作废</span>";
                            html += "<span class='status_delete' style='margin-right: 15px'  data-id='" + value + "'>删除</span>";
                            html += "<span class='status_editor' data-id='" + value + "'>修改</span>";
                        } else if (row.State == 2) {
                            html += "<span class='status_edit' style='margin-right: 15px' data-id='" + value + "'>详情</span>";
                            html += "<span class='status_cancel' style='margin-right: 15px'  data-id='" + value + "'>作废</span>";
                            html += "<span class='status_delete' style='margin-right: 15px' data-id='" + value + "'>删除</span>";
                            html += "<span class='status_editor' data-id='" + value + "'>修改</span>";

                            // html +="<span class='status_add' data-id='" + value + "' style='margin-left: 10px;cursor: pointer;color: #039cf8'>上架</span>";
                        }
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
                if (giftCardList.flag) {
                    giftCardList.flag = false
                }
                $('.caret').remove()
            },
            onLoadError: function (data) {
                $('#productTable').bootstrapTable('removeAll');
            },
            // 1.点击每行进行函数的触发
            onClickRow: function (row, tr, flied) {

            },
            //2. 点击前面的复选框进行对应的操作
            //点击全选框时触发的操作
            //点击全选框时触发的操作
            onCheckAll: function (rows) {},
            onUncheckAll: function (rows) {},
            //点击每一个单选框时触发的操作
            onCheck: function (row) {},
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
        var methodName = "/Coupon/CouponList";
        if (giftCardList.flag) {
            var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                minSize: $("#leftLabel").val(),
                maxSize: $("#rightLabel").val(),
                minPrice: $("#priceleftLabel").val(),
                maxPrice: $("#pricerightLabel").val(),
                "CouponSn": $('#Number').val(),
                "Name": $('#name').val(),
                "State": $('#stateBox').val(),
                Page: {
                    PageSize: params.limit, //页面大小,
                    PageIndex: localStorage.getItem('PageIndex'), //页码
                }
            };
        } else {
            var temp = { //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                minSize: $("#leftLabel").val(),
                maxSize: $("#rightLabel").val(),
                minPrice: $("#priceleftLabel").val(),
                maxPrice: $("#pricerightLabel").val(),
                "CouponSn": $('#Number').val(),
                "Name": $('#name').val(),
                "State": $('#stateBox').val(),
                Page: {
                    PageSize: params.limit, //页面大小,
                    PageIndex: (params.offset / params.limit) + 1, //页码
                }
            };
        }

        return temp;
    },
    // 用于server 分页，表格数据量太大的话 不想一次查询所有数据，可以使用server分页查询，数据量小的话可以直接把sidePagination: "server"  改为 sidePagination: "client" ，同时去掉responseHandler: responseHandler就可以了，
    responseHandler: function (res) {
        if (res.Data != null) {
            console.log(res);
            return {
                "rows": res.Data.CouponList,
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
        var methodName = "/Coupon/CouponList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "CouponSn": $('#Number').val(),
                "Name": $('#name').val(),
                "State": $('#stateBox').val(),
            };
        } else {
            var obj = parame;
        }

        $('#productTable').bootstrapTable(
            "refresh", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );
    },
    //表格刷新(先销毁后初始化)
    projectQuery: function (parame) {
        //方法名
        var methodName = "/Coupon/CouponList";

        if (parame == "" || parame == undefined) {
            var obj = {
                "CouponSn": $('#Number').val(),
                "Name": $('#name').val(),
                "State": $('#stateBox').val(),
                Page: {
                    PageSize: $("#pagesize_dropdown").val(), //页面大小,
                    PageIndex: 1, //页码
                }
            };
        } else {
            var obj = parame;
        }

        $('#productTable').bootstrapTable(
            "destroy", {
                url: SignRequest.urlPrefix + methodName,
                query: obj
            }
        );

        giftCardList.initBootstrapTable();

    }
}