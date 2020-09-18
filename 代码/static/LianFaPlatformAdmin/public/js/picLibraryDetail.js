var PicLibraryDetail = {
    //排序方式(1上传时间2图片名3修改时间)
    displayOrder: "1",
    //是否排序
    IsAsc: '1',
    //每页显示多少
    PageSize: "10",
    //页码
    PageIndex: 1,
    cateId: "",
    //文件名
    fileName:"",

    cateBoxTemplate: `
        <option value="0">默认分类</option>
        {{each List as value i}}
            <option value="{{List[i].CategoryId}}">{{List[i].Name}}</option>
        {{/each}}
    `,
    picBoxTemplate: `    {{each List as value i}}
                            <li class="pic_li_detail" data-id="{{List[i].MaterialId}}">
                                    <div class="goods_img">
                                        <img src="{{List[i].FileUrlFull}}" alt="">
                                    </div>
                                    <div class="edit_pic_box">
                                        <div class="box-pic_name">
                                            <input type="checkbox" data-id="{{List[i].MaterialId}}" class="materialCheckbox">
                                            <span class="txtName">{{List[i].ShowName}}</span>
                                        </div>
                                        <div class="edit_operation_box">
                                            <a data-toggle="modal" data-id="{{List[i].MaterialId}}" href="#reviseNameModal" class="revise_name">改名</a>
                                            <span class="exp u-btn u-btn-primary u-btn-small" style="margin: 0 5px"
                                                    data-clipboard-text="{{List[i].FileUrlFull}}">复制图片路径
                                            </span>
                                            <span class="delete_it" data-id="{{List[i].MaterialId}}">删除</span>
                                        </div>
                                    </div>
                            </li>
                            {{/each}}
`,
    moveBoxTemplate: `
                                        <li class="file_li" data-id="0">
                                                    <i class="glyphicon glyphicon-folder-open" style="color:#bde0e9;margin-right: 10px"></i>未分组
                                         </li>
                                         {{each List as value i}}
                                             <li class="file_li" data-id="{{List[i].CategoryId}}">
                                                        <i class="glyphicon glyphicon-folder-open" style="color:#bde0e9;margin-right: 10px"></i>{{List[i].Name}}
                                             </li>
                                         {{/each}}
    `,

    init: function () {
        //初始化
        PicLibraryDetail.adminGetCategoryList();
        PicLibraryDetail.adminGetMaterialList();
        //初始化复制插件
        var clipboard = new Clipboard('.exp');
        clipboard.on('success', function (e) {
            Common.showSuccessMsg('已复制')
            e.clearSelection();
        });
        clipboard.on('error', function (e) {
            console.log(e);
        });

        //图片上传模态框出现
        $('#uploaderPicModal').on('hide.bs.modal', function () {
            $('.uploader_img_list_box').find('.file-item').remove();
            $('#PicCateBox').val('0')
        });
        //改名相关
        //当修改名字模态框弹起时候记录它点击的是哪个元素
        $("#reviseNameModal").on("show.bs.modal", function (e) {
            //获取是哪个元素点击
            var invoker_dom = $(e.relatedTarget);
            //获取触发的该元素父元素所属的data-id
            var dataId = invoker_dom.parents('.pic_li_detail').attr('data-id');
            //此时给该弹窗赋予data-id，与触发元素的父元素data-id相对应
            $("#reviseNameModal").attr('data-id', dataId);
            var txt_previous = invoker_dom.parents('.pic_li_detail').find('.txtName').text();
            $('#revise_input_name').val(txt_previous);

        });
        //当点击确认按钮，修改名字时候
        $('#revise_confirm_name').click(function () {
            var dataId = $("#reviseNameModal").attr('data-id');
            var array_val = $('#revise_input_name').val();
            //var array_val=propertyVal_into_Array(addVal);//这里不用处理数据格式
            $('.pic_li_detail[data-id=' + dataId + ']').find('.txtName').text(array_val);
            $("#reviseNameModal").modal('hide');
            //调用修改素材名字接口
            PicLibraryDetail.adminUpdateMaterialName(dataId, array_val)
        });
        //排序方式改变
        $('body').on('change', '#timeOrder_box', function () {
            PicLibraryDetail.displayOrder = $('#timeOrder_box option:selected').val();
        });
        //查询按钮点击
        $('body').on('click', '#searchBtn', function () {
            //页码变为1
            PicLibraryDetail.PageIndex = 1;
            PicLibraryDetail.fileName = $('#fileName').val();
            PicLibraryDetail.adminGetMaterialList()
        });
        //批量删除按钮点击
        $('body').on('click', '.delectAll', function () {
            var list = [];
            $('.materialCheckbox').each(function (index, item) {
                if (this.checked) {
                    list.push(Number($(item).attr('data-id')))
                }
            })
            Common.confirmDialog('确认要删除吗?',function(){
                PicLibraryDetail.adminBatchDeleteMaterial(list)
            })
        });
        //删除按钮点击
        $('body').on('click', '.delete_it', function () {
            var target = $(this);
            var id = $(this).attr('data-id');
            Common.confirmDialog('确定要删除吗?',function(){
                PicLibraryDetail.adminDeleteMaterial(id, target)
            })

        });
        //确认上传图片
        $('body').on('click', '#confirm_upload_pic', function () {
            var cateId = $('#PicCateBox option:selected').val()
            var list = [];
            $('.picItem').each(function (index, item) {
                list.push($(item).attr('data-src'))
            })
            console.log(list)
            PicLibraryDetail.adminSaveMaterial(list, cateId)
        });
        //移动确认按钮点击
        $('body').on('click', '#remove_file_confirm', function () {
            $('.file_li').each(function (index, item) {
                if ($(item).hasClass('file_li_active')) {
                    PicLibraryDetail.cateId = $(item).attr('data-id');
                }
            })
            var cateList = [];
            $('.materialCheckbox').each(function (index, item) {
                if (this.checked) {
                    cateList.push($(item).attr('data-id'));
                }
            })
            PicLibraryDetail.adminMoveMaterial(cateList, PicLibraryDetail.cateId)
        });
        //点击加载更多
        $('body').on('click','#load_more_pic',function(){
            PicLibraryDetail.PageIndex += 1;
            PicLibraryDetail.adminGetMaterialList(true);
        })
        //点击预览图片的删除
        $(document).on('click', '.maydelete_uploadPic', function () {
            $(this).parents('.thumbnail').remove();
        });


    },
    initHandle: function () {


        //上传图片(没有写service,只是预览了)
        uploadPic_toLibrary('#wrapper_pick_img_box');

        //点击全选效果
        $('#all_check_box').click(function () {

            if (this.checked) {
                $('.pic_box_library .box-pic_name input').each(function (ind, val) {
                    this.checked = true;
                })
            }
            else {
                $('.pic_box_library .box-pic_name input').each(function (ind, val) {
                    this.checked = false;
                })
            }
        });
        //点击其中一个input子元素，看是否取消全选
        $('.pic_box_library').on('click', '.box-pic_name input', function () {
            if (!this.checked) {
                $('#all_check_box')[0].checked = false;//取消全选
            }
        });
        //筛选选中移动到哪个文件夹file_li_active
        $('.files_dv_box').on('click', '.file_li', function () {
            $('.file_li').removeClass('file_li_active');
            $(this).addClass('file_li_active');
        });
        //点击弹出来的移动框 中的 移动按钮
        // $('#remove_file_confirm').click(function () {
        //     var which_fileId = $("#file_classify_box").find("option:selected").attr('data-id');//注意这里是通过dataid判断的
        //
        //     var choice_fileId = $('.file_li_active').attr('data-id');
        //     if (which_fileId == choice_fileId) {//如果图片移入的文件夹和当前图片所在文件夹相同的话就不移除该图片
        //
        //     }
        //     else {
        //         $('.pic_box_library .box-pic_name input').each(function (ind, val) {
        //             if (this.checked) {
        //                 $(this).parents('.pic_li_detail').remove();
        //                 swal("移动成功", "", "success");
        //             }
        //             else {
        //
        //             }
        //         })
        //     }
        //
        //     $('#removeFileModal').modal('hide');
        //
        // });


    },
    //查询分类
    adminGetCategoryList: function () {
        var methodName = "/Material/AdminGetCategoryList";
        var data = {
            "Page": {
                "PageSize": 100000,
                "PageIndex": 1
            }
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(PicLibraryDetail.cateBoxTemplate);
                var html = render(data.Data);
                $('.cateBox').html(html);
                var render1 = template.compile(PicLibraryDetail.moveBoxTemplate);
                var html1 = render1(data.Data);
                $('#file_box').html(html1);
                PicLibraryDetail.initHandle();
                data.Data.List.forEach(function (item, index) {
                    if (Common.getUrlParam('id') == "0") {
                        $('#targetName').html('未分组')
                    }
                    if ($(item).CategoryId == Common.getUrlParam('id')) {
                        $('#targetName').html($(item).Name)
                    }
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //获取素材库列表
    adminGetMaterialList: function (isLoadermore) {
        var methodName = "/Material/AdminGetMaterialList";
        var data = {
            "FileName": PicLibraryDetail.fileName,
            "CategoryId": Common.getUrlParam('id'),
            "DisplayOrder": PicLibraryDetail.displayOrder,
            "IsAsc": PicLibraryDetail.IsAsc,
            "Page": {
                "PageSize": PicLibraryDetail.PageSize,
                "PageIndex": PicLibraryDetail.PageIndex
            }
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(PicLibraryDetail.picBoxTemplate);
                var html = render(data.Data);
                //小于10条加载更多隐藏
                if(data.Data.List.length < 10){
                    $('#load_more_pic').hide()
                }else {
                    $('#load_more_pic').show()
                }
                //区别加载更多
                if(isLoadermore){
                    $('#PicBox').append(html);
                    PicLibraryDetail.initHandle()
                }else{
                    $('#PicBox').html(html);
                    PicLibraryDetail.initHandle()
                }


            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //删除素材接口
    adminDeleteMaterial: function (id, target) {
        var methodName = "/Material/AdminDeleteMaterial";
        var data = {
            "Id": id,
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("删除成功!", function () {
                    target.parents('.pic_li_detail').remove();
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //批量删除素材接口
    adminBatchDeleteMaterial: function (list) {
        var methodName = "/Material/AdminBatchDeleteMaterial";
        var data = {
            "Ids": list
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("删除成功!", function () {
                    $('.pic_li_detail').each(function (index, item) {
                        list.forEach(function (item2, index2) {
                            if ($(item).attr('data-id') == item2) {
                                $(item).remove();
                            }
                        })
                    })
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //素材改名
    adminUpdateMaterialName: function (id, name) {
        var methodName = "/Material/AdminUpdateMaterialName";
        var data = {
            "Id": id,
            "Name": name
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("修改成功!", function () {

                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //确认上传素材
    adminSaveMaterial: function (list, id) {
        var methodName = "/Material/AdminSaveMaterial";
        var data = {
            "TempImageList": list,
            "CategoryId": id
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("上传成功!", function () {
                    $('#uploaderPicModal').modal('hide')
                    location.reload()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //移动素材
    adminMoveMaterial: function (cateList, cateid) {
        var methodName = "/Material/AdminMoveMaterial";
        var data = {
            "Ids": cateList,
            "CategoryId": cateid,
        };
        console.log(data)
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg("移动成功!", function () {
                    location.reload()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },



};


$(function () {

    PicLibraryDetail.init()

});