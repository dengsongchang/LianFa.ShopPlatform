var PicLibrary = {
    PageIndex:1,
    PageSize:"10",
    Pic_box_Template: `
                        {{each List as value i}}
                        <div class="file_item" data-type="0" data-id="{{List[i].CategoryId}}">
                            <a href="/homePage/picLibraryDetail?id={{List[i].CategoryId}}">
                                <img class="img_icon" src="../public/images/file_pic.png" alt="">
                            </a>
                            <div class="edit_Filename_box">
                                <span class="edit_ipt isspan" style="border-width: 0px 0px 0px;">{{List[i].Name}}</span>
                            </div>
                            <div class="edit_dv">
                                <span class="edite_file" data-id={{List[i].CategoryId}}>编辑</span>
                                <span class="delete_file" data-id={{List[i].CategoryId}}>删除</span>
                            </div>
                        </div>
                        {{/each}}
`,
    cateBoxTemplate: `
        <option value="0">默认分类</option>
        {{each List as value i}}
            <option value="{{List[i].CategoryId}}">{{List[i].Name}}</option>
        {{/each}}
    `,
    init: function () {
        PicLibrary.adminGetCategoryList()
        PicLibrary.initHandle()
    },
    initHandle: function () {
        //上传图片
       // uploadPic_toLibrary('#uploader_pic_trigger', '');
        //上传图片(没有写service,只是预览了)
        uploadPic_toLibrary('#wrapper_pick_img_box');
        //创建文件夹
        $('.create_file_pic').click(function () {
            var parents_dom = $('#PicItem_Box');
            var append_dom = '<div class="file_item">' +
                '<a><img src="../public/images/file_pic.png" alt=""></a>' +
                '<div class="edit_Filename_box">' +
                '<input type="text" class="edit_ipt" value="未分组">' +

                '</div>' +
                '<div class="edit_dv">' +
                '<span class="create_file">创建</span>' +
                '<span class="cancel_file">取消</span>' +
                '</div>' +
                '</div>';
            parents_dom.append(append_dom);
            parents_dom.find('input[type="text"]').focus();
            parents_dom.find('input[type="text"]').select();
        });
        //点击创建
        $(document).on('click', '.create_file', function () {
            var index_v = $(this).index('.create_file');

            var parents_dom = $(this).parents('.file_item');

            if ($(this).parents('.file_item').find(".edit_Filename_box").find('span').hasClass('isspan')) {
                var zhi_is = parents_dom.find('.edit_ipt').text();
            } else {
                var zhi_is = parents_dom.find('.edit_ipt').val();
            }
            parents_dom.find('input[type="text"]').replaceWith('<span class="edit_ipt isspan">' + zhi_is + '</span>');
            var edit_file_span = '<span class="edite_file">编辑</span>';
            parents_dom.find('.edit_dv').prepend(edit_file_span);
            $(this).remove();
            parents_dom.find('.cancel_file').remove();
            var delete_file_span = '<span class="delete_file">删除</span>';
            parents_dom.find('.edit_dv').append(delete_file_span);
            //调用创建接口
            PicLibrary.adminAddCategory(zhi_is)
        });
        //点击编辑
        $(document).on('click', '.edite_file', function () {
            var parents_dom = $(this).parents('.file_item');
            if ($(this).parents('.file_item').find(".edit_Filename_box").find('span').hasClass('isspan')) {
                var zhi_txt = parents_dom.find('.edit_ipt').text();
            } else {
                var zhi_txt = parents_dom.find('.edit_ipt').val();
            }
            $(this).parents('.file_item').find('.edit_ipt').replaceWith('<input type="text" class="edit_ipt" value="' + zhi_txt + '">');

            parents_dom.find('input[type="text"]').focus();
            parents_dom.find('input[type="text"]').select();
            $(this).removeClass('edite_file').addClass('edite_file_Createspan');
            $('.edite_file_Createspan').html("确认");
            localStorage.setItem('cateId',$(this).attr('data-id'));
        });
        //点击确认
        $(document).on('click', '.edite_file_Createspan', function () {
            var parents_dom = $(this).parents('.file_item');
            var zhi_is = parents_dom.find('input[type="text"]').val();
            $(this).parents('.file_item').find('.edit_ipt').replaceWith('<span class="edit_ipt isspan">' + zhi_is + '</span>');
            $(this).removeClass('edite_file_Createspan').addClass('edite_file');
            $('.edite_file').html("编辑");
            //调用编辑接口
            PicLibrary.adminUpdateCategoryName(zhi_is)
        });
        //点击删除
        $(document).on('click', '.delete_file', function () {
            var parents_dom = $(this).parents('.file_item');
            var id = $(this).attr('data-id');
            PicLibrary.adminDeleteCategory(id,parents_dom)
        });
        //点击预览图片的删除
        $(document).on('click', '.maydelete_uploadPic', function () {
           $(this).parents('.thumbnail').remove();
        });
        //图片上传模态框出现
        $('#uploaderPicModal').on('hide.bs.modal', function () {
            $('.uploader_img_list_box').find('.file-item').remove();
            $('#PicCateBox').val('0')
        });
        //确认上传图片
        $('body').on('click', '#confirm_upload_pic', function () {
            var cateId = $('#PicCateBox option:selected').val()
            var list = [];
            $('.picItem').each(function (index, item) {
                list.push($(item).attr('data-src'))
            })
            console.log(list)
            PicLibrary.adminSaveMaterial(list, cateId)
        });

        //点击取消
        $(document).on('click', '.cancel_file', function () {
            var parents_dom = $(this).parents('.file_item');
            parents_dom.remove();
        });
        //切换显示数量
        $(document).on('change','#numOrder_box',function(){
            PicLibrary.PageSize = $('#numOrder_box option:selected').val();
            PicLibrary.PageIndex = 1;
            PicLibrary.adminGetCategoryList();
        })
        //点击加载更多
        $(document).on('click','#load_more_pic',function(){
            PicLibrary.PageIndex +=1;
            PicLibrary.adminGetCategoryList(true)
        })
    },

    //创建文件夹接口
    adminAddCategory: function (name) {
        var methodName = "/Material/AdminAddCategory";
        var data = {
            "Name": name
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('添加成功', function () {
                    location.reload()
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //编辑文件夹接口
    adminUpdateCategoryName: function (name) {
        var methodName = "/Material/AdminUpdateCategoryName";
        var data = {
            "Id": localStorage.getItem('cateId'),
            "Name": name
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.showSuccessMsg('编辑成功', function () {

                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //删除文件夹接口
    adminDeleteCategory: function (id,target) {
        var methodName = "/Material/AdminDeleteCategory";
        var data = {
            "Id": id,
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                Common.confirmDialog('是否要删除?', function () {
                    Common.showSuccessMsg('删除成功!',function(){
                        target.remove()
                    })
                })

            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    //查询文件夹列表
    adminGetCategoryList: function (isLoadermore) {
        var methodName = "/Material/AdminGetCategoryList";
        var data = {
            "Page": {
                "PageSize":$('#numOrder_box').val() ,
                "PageIndex": PicLibrary.PageIndex
            }
        };
        SignRequest.set(methodName, data, function (data) {
            console.log(data)
            if (data.Code == "100") {
                var render = template.compile(PicLibrary.Pic_box_Template);
                var html = render(data.Data);
                var render1 = template.compile(PicLibrary.cateBoxTemplate);
                var html1 = render1(data.Data);
                $('.cateBox').html(html1);
                //小于条数加载更多隐藏
                if(data.Data.List.length < PicLibrary.PageSize){
                    $('#load_more_pic').hide()
                }else {
                    $('#load_more_pic').show()
                }
                //区别加载更多
                if(isLoadermore){
                    $('#PicItem_Box').append(html);
                }else{
                    $('#PicItem_Box').html(html);
                }

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

};

$(function () {

    PicLibrary.init()


});