<template>

    <div class="allBox">
        <div class="productBox">
            <img :src="Image" class="productImg" alt="">
            <div class="productRight">
                <div class="productName">{{StoreName}}</div>
                <div class="logisticsNum">{{ShiPsyStemName}}：{{ShipSn}}</div>
            </div>
        </div>
        <!--物流信息-->
        <div class="logisticsBox">
            <div class="item_infoTop" v-if="logisticInfo.State== null">暂时没有物流信息</div>

            <div v-for="item,index in list" :key="index" v-if="logisticInfo.Logistics.status!= 0" class="item_info_list">
                <div class="item_info" v-if="index == 0">
                    <div class="desc_content">
                        <span class="title_desc" style="color:#999;">
                            {{item.context}}
                        </span>
                        <span class='time_date'>{{item.time}}</span>
                        <span class='time_dateNext'>{{item.ftime}}</span>
                        <div class='icon'></div>
                    </div>
                </div>
                <div class="item_info" v-if="index!=0 && index!=(logisticInfo.Logistics.data.length-1)">
                    <div class="desc_content">
                        <span class="title_desc" style="color:#999;">
                            {{item.context}}
                        </span>
                        <span class='time_date'>{{item.time}}</span>
                        <span class='time_dateNext'>{{item.ftime}}</span>
                        <div class="icon_sign_dot"></div>
                    </div>
                </div>
                <div class="item_info"
                    v-if="index==(logisticInfo.Logistics.data.length-1) && logisticInfo.Logistics.data.length > 1">
                    <div class="desc_content">
                        <span class="title_desc" style="color:#999;">
                            {{item.context}}
                        </span>
                        <span class='time_date'>{{item.time}}</span>
                        <span class='time_dateNext'>{{item.ftime}}</span>
                        <div class='icon'></div>
                    </div>
                </div>
            </div>


        </div>

    </div>


</template>

<script>
    export default {
        name: 'logistics',
        data() {
            return {
                logisticInfo: {},
                Image: "",
                ShiPsyStemName: "",
                ShipSn: "",
                StoreName: "",
                list: [],
            };
        },
        computed: {},
        components: {

        },
        methods: {
            OrderLogisticsList: function() {
                var that = this;
                var data = {
                    oid: that.$route.query.OId,
                    OLId: 0
                };
                that.postData("/UCenter/OrderLogistics", data).then(function(data) {
                    if (data.data.Code == "100") {
                        that.logisticInfo = data.data.Data.OrdersLogisticsInfo;
                        that.list = data.data.Data.OrdersLogisticsInfo.Logistics.data;
                        that.ShipSn = data.data.Data.OrdersLogisticsInfo.OrderAddressInfo.ShipSn;
                        that.Image = data.data.Data.OrdersLogisticsInfo.OrderAddressInfo.Image;
                        that.ShiPsyStemName = data.data.Data.OrdersLogisticsInfo.OrderAddressInfo.ShiPsyStemName;
                        that.StoreName = data.data.Data.OrdersLogisticsInfo.OrderAddressInfo.StoreName;

                    }
                });
            }
        },
        mounted() {
            var that = this;
            that.OrderLogisticsList();
        },
        created() {
            var that = this;

        },
    }
</script>
<style scoped src="../../static/css/logistics.css">

</style>