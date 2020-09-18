$(function () {
    areaManage.init();
})

var areaManage = {
    listTpl:`
        <ul>
        {{each RegionsList as value i}}
            <li data-id="{{RegionsList[i].RegionId}}">
                <input value="{{RegionsList[i].Name}}" readonly>
                <div class="btn-div">
                    <a class="save">保存</a>
                    <a class="delete">删除</a>
                </div>
            </li>
        {{/each}}
        </ul>
        <a class="add-btn">+</a>
    `,
    init:function () {
        var timer;
        // 单击
        $(".list-content").on("click","li",function () {
            var tThis = $(this);
            timer = setTimeout(function () {
                if(!tThis.hasClass("select") && !tThis.hasClass("operat-on")){
                    tThis.siblings(".operat-on").find("input").attr("readonly",true);
                    tThis.siblings("li").removeClass("operat-on");
                    tThis.siblings("li").removeClass("select");
                    tThis.addClass("select");
                    tThis.parent().find("input").attr("readonly",true);

                    var id = tThis.attr("data-id");
                    if(tThis.parent().parent().attr("id") == "provinceList"){
                        console.log("省/自治区/直辖市");
                        $("#cityList").html("");
                        $("#districtList").html("");
                        $("#streetList").html("");
                        if(id != undefined && id != ""){
                            areaManage.getCityList(id);
                        }
                    }
                    else if(tThis.parent().parent().attr("id") == "cityList"){
                        console.log("地级市");
                        $("#districtList").html("");
                        $("#streetList").html("");
                        if(id != undefined && id != ""){
                            areaManage.getDistrictList(id);
                        }
                    }
                    else if(tThis.parent().parent().attr("id") == "districtList"){
                        console.log("市辖区/县(县级市)");
                        $("#streetList").html("");
                        if(id != undefined && id != ""){
                            areaManage.getStreetList(id);
                        }
                    }
                    else if(tThis.parent().parent().attr("id") == "streetList"){
                        console.log("乡/镇/街道");
                    }
                }
            },200);
        });

        // 双击
        $(".list-content").on("dblclick","li",function () {
            clearTimeout(timer);
            if(!$(this).hasClass("select") && !$(this).hasClass("operat-on")){
                var id = $(this).attr("data-id");
                if($(this).parent().parent().attr("id") == "provinceList"){
                    console.log("省/自治区/直辖市");
                    $("#cityList").html("");
                    $("#districtList").html("");
                    $("#streetList").html("");
                    if(id != undefined && id != ""){
                        areaManage.getCityList(id);
                    }
                }
                else if($(this).parent().parent().attr("id") == "cityList"){
                    console.log("地级市");
                    $("#districtList").html("");
                    $("#streetList").html("");
                    if(id != undefined && id != ""){
                        areaManage.getDistrictList(id);
                    }
                }
                else if($(this).parent().parent().attr("id") == "districtList"){
                    console.log("市辖区/县(县级市)");
                    $("#streetList").html("");
                    if(id != undefined && id != ""){
                        areaManage.getStreetList(id);
                    }
                }
                else if($(this).parent().parent().attr("id") == "streetList"){
                    console.log("乡/镇/街道");
                }
            }
            if(!$(this).hasClass("operat-on")){
                $(this).siblings("li").removeClass("operat-on");
                $(this).siblings("li").removeClass("select");
                $(this).removeClass("select");
                $(this).addClass("operat-on");
                $(this).siblings("li").find("input").attr("readonly",true);
                $(this).find("input").attr("readonly",false);
                $(this).find("input").focus();
            }
        });

        // 添加
        $(".list-content").on("click",".add-btn",function () {
            var count = 0;
            var list = $(this).siblings("ul").find("li");
            for(var i=0;i<list.length;i++){
                if(list.eq(i).attr("data-id") == undefined){
                    count ++;
                }
            }
            if(count == 0){
                var html = '<li>' +
                    '<input value="" readonly>' +
                    '<div class="btn-div">' +
                    '<a class="save">保存</a>' +
                    '<a class="delete">取消</a>' +
                    '</div>' +
                    '</li>';
                $(this).siblings("ul").find("li").removeClass("operat-on");
                $(this).siblings("ul").append(html);
                var newItem = $(this).siblings("ul").children("li:last-child");
                newItem.addClass("operat-on");
                newItem.find("input").attr("readonly",false);
                newItem.find("input").focus();
            }
        });

        // 编辑、添加（保存）
        $(".list-content").on("click",".save",function () {
            var tCell = $(this).parent().parent();
            var id = $(this).parent().parent().attr("data-id");
            var name = $(this).parent().siblings("input").val();
            if(id != undefined && id != ""){
                // 编辑已有数据
                areaManage.updateAreaData(id,name,tCell);
            }
            else {
                // 添加新数据
                areaManage.addAreaData(name,tCell);
            }
        });

        // 删除、取消
        $(".list-content").on("click",".delete",function () {
            var tCell = $(this).parent().parent();
            var id = $(this).parent().parent().attr("data-id");
            var name = $(this).parent().siblings("input").val();
            if(id != undefined && id != ""){
                // 删除已有数据
                areaManage.deleteAreaData(id,name,tCell);
            }
            else {
                // 取消添加新数据
                areaManage.cancelAreaData(tCell);
            }
        });

        areaManage.getProvinceList();

    },
    // 添加数据
    addAreaData:function (Name,tCell) {
        var layer = tCell.parent().parent().attr("data-layer");
        var ParentId = 0;
        var ProvinceId = 0;
        var CityId = 0;
        if(layer == 2){
            if($("#provinceList").find(".select").attr("data-id") != undefined){
                ParentId = $("#provinceList").find(".select").attr("data-id");
                ProvinceId = $("#provinceList").find(".select").attr("data-id");
            }
            else {
                ParentId = $("#provinceList").find(".operat-on").attr("data-id");
                ProvinceId = $("#provinceList").find(".operat-on").attr("data-id");
            }
        }
        else if(layer == 3){
            if($("#provinceList").find(".select").attr("data-id") != undefined){
                ProvinceId = $("#provinceList").find(".select").attr("data-id");
            }
            else {
                ProvinceId = $("#provinceList").find(".operat-on").attr("data-id");
            }
            if($("#cityList").find(".select").attr("data-id") != undefined){
                ParentId = $("#cityList").find(".select").attr("data-id");
                CityId = $("#cityList").find(".select").attr("data-id");
            }
            else {
                ParentId = $("#cityList").find(".operat-on").attr("data-id");
                CityId = $("#cityList").find(".operat-on").attr("data-id");
            }
        }
        else if(layer == 4){
            if($("#provinceList").find(".select").attr("data-id") != undefined){
                ProvinceId = $("#provinceList").find(".select").attr("data-id");
            }
            else {
                ProvinceId = $("#provinceList").find(".operat-on").attr("data-id");
            }
            if($("#cityList").find(".select").attr("data-id") != undefined){
                CityId = $("#cityList").find(".select").attr("data-id");
            }
            else {
                CityId = $("#cityList").find(".operat-on").attr("data-id");
            }
            if($("#districtList").find(".select").attr("data-id") != undefined){
                ParentId = $("#districtList").find(".select").attr("data-id");
            }
            else {
                ParentId = $("#districtList").find(".operat-on").attr("data-id");
            }
        }
        var methodName = "/regions/AdminAddRegion";
        var data = {
            Name: Name,
            ParentId: ParentId,
            Layer: layer,
            ProvinceId: ProvinceId,
            CityId: CityId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                tCell.removeClass("operat-on");
                tCell.find("input").attr("readonly",true);
                tCell.attr("data-id",data.Data.RegionId);
                Common.showSuccessMsg("添加成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 编辑已有数据
    updateAreaData:function (RegionId,Name,tCell) {
        var methodName = "/regions/AdminEditRegion";
        var data = {
            RegionId: RegionId,
            Name: Name
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                tCell.removeClass("operat-on");
                tCell.addClass("select");
                tCell.find("input").attr("readonly",true);
                Common.showSuccessMsg("编辑成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 删除已有数据
    deleteAreaData:function (RegionId,Name,tCell) {
        var methodName = "/regions/AdminDeleteRegion";
        var data = {
            RegionId: RegionId,
            Name: Name
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                tCell.remove();
                Common.showSuccessMsg("删除成功");
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 取消添加数据
    cancelAreaData:function (tCell) {
        tCell.remove();
    },
    // 获取省/自治区/直辖市列表
    getProvinceList:function () {
        var methodName = "/regions/AdminProvinceList";
        var data = {};
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(areaManage.listTpl);
                var html = render(data.Data);
                $("#provinceList").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取地级市列表
    getCityList:function (ParentId) {
        var methodName = "/regions/AdminCityList";
        var data = {
            ParentId: ParentId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(areaManage.listTpl);
                var html = render(data.Data);
                $("#cityList").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取区县列表
    getDistrictList:function (ParentId) {
        var methodName = "/regions/AdminMunicipalDistrictList";
        var data = {
            ParentId: ParentId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(areaManage.listTpl);
                var html = render(data.Data);
                $("#districtList").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    },
    // 获取街道列表
    getStreetList:function (ParentId) {
        var methodName = "/regions/AdminVillagesTownsList";
        var data = {
            ParentId: ParentId
        };
        SignRequest.set(methodName, data, function (data) {
            if (data.Code == "100") {
                var render = template.compile(areaManage.listTpl);
                var html = render(data.Data);
                $("#streetList").html(html);
            } else {
                Common.showErrorMsg(data.Message);
            }
        });
    }
}