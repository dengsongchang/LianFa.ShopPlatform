function uploadPic_toLibraryP(pickId) {

    var uploader = WebUploader.create({

        // 选完文件后，是否自动上传。
        auto: true,

        // swf文件路径
        //swf: BASE_URL + '/js/Uploader.swf',

        // 文件接收服务端。
        server: SignRequest.urlPrefix + '/Material/AdminUploadImage?CategoryId='+$(".a_b").attr("data-id"),

        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: pickId,

        duplicate: true,
        //fileNumLimit: 3,
        // 只允许选择图片文件。
        accept: {
            title: 'Images',
            extensions: 'gif,jpg,jpeg,png',
            mimeTypes: 'image/gif,image/jpg,image/jpeg,image/bmp,image/png' //指定文件格式，解决谷歌卡慢
        }
    });

    // 当有文件添加进来的时候
    uploader.on('fileQueued', function (file) {
        var $li = $(
            '<div id="' + file.id + '" class="file-item thumbnail">' +
            '<img class="picItem">' +
            '<span class="maydelete_uploadPic">×</span>'+
            //'<div class="info">' + file.name + '</div>' +
            '</div>'
        );

        $img = $li.find('img');

        //$list.append( $li );


        // $list为容器jQuery实例
        //$list.append( $li );

        //alert('文件添加进来')

        thumbnailWidth = 100;
        thumbnailHeight = 100;

        // 创建缩略图
        // 如果为非图片文件，可以不用调用此方法。
        // thumbnailWidth x thumbnailHeight 为 100 x 100
        uploader.makeThumb(file, function (error, src) {
            if (error) {
                $img.replaceWith('<span>不能预览</span>');
                return;
            }

            $img.attr( 'src', src );

            $('.uploader_img_list_box').append($li);//追加的预览图片
            Common.showUploading();

        }, thumbnailWidth, thumbnailHeight);
    });


    // 文件上传过程中创建进度条实时显示。
    uploader.on('uploadProgress', function (file, percentage) {
        var $li = $('#' + file.id),
            $percent = $li.find('.progress span');
        console.log(6)
        // 避免重复创建
        if (!$percent.length) {
            $percent = $('<p class="progress"><span></span></p>')
                .appendTo($li)
                .find('span');
        }

        $percent.css('width', percentage * 100 + '%');
    });

    // 文件上传成功，给item添加成功class, 用样式标记上传成功。
    uploader.on('uploadSuccess', function (file, response) {
        console.log(file);
        $('#'+file.id+'').find('img').attr('data-src',response.Data)
        console.log(response);
        swal.close()
        setTimeout(() => {
            if (ProductRelease.PicTotal > 0) {
                //初始化分页
                $.jqPaginator('#pagination2', {
                    totalCounts: ProductRelease.PicTotal,
                    pageSize: 18,
                    visiblePages: 10,
                    currentPage: 1,
                    prev: '<li class="prev"><a href="javascript:;">&lt;</a></li>',
                    next: '<li class="next"><a href="javascript:;">&gt;</a></li>',
                    page: '<li class="page"><a href="javascript:;">{{page}}</a></li>',
                    onPageChange: function (num, type) {
                        ProductRelease.PicPageIndex = num;
                        ProductRelease.adminGetMaterialList();
                        $('#p2').text(type + '：' + num);
                    }
                });
            }
        }, 500);
        // setTimeout(function(){
        //
        //     ProductRelease.adminGetMaterialList();
        //
        // },500)
        // $(picId).attr('data-src',response.Data);

    });

    // 文件上传失败，显示上传出错。
    uploader.on('uploadError', function (file) {
        var $li = $('#' + file.id),
            $error = $li.find('div.error');

        // 避免重复创建
        if (!$error.length) {
            $error = $('<div class="error"></div>').appendTo($li);
        }
        swal.close()
        Common.showErrorMsg("上传失败")
        //$error.text('上传失败');
    });

    // 完成上传完了，成功或者失败，先删除进度条。
    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').remove();
    });

}