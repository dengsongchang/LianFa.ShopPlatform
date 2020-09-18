<!-- 礼品卡列表 -->
<template>
    <div class="content">
        <van-list v-model="loading" :loading-text="loadText" :finished="finished" finished-text="没有更多了"
            :error.sync="error" error-text="请求失败，点击重新加载" @load="giftcardlist">
            <div class="gift_card">
                <div class="card_list">
                    <ul>
                        <li v-for="(item,index) in cardlist" :key="index" @click="gettcarddetails(item.CouponTypeId)">
                            <div class="card_img">
                                <img :src="item.CouponImg" alt="">
                            </div>
                            <span>{{item.Name}}</span>
                        </li>
                    </ul>
                </div>
            </div>



        </van-list>
    </div>



</template>

<script>
    import {
        List
    } from 'vant'
    export default {
        name: 'giftcard',
        data() {
            return {
                loading: false, //加载
                finished: false, //加载完成
                // isLoading: false,
                error: false,
                total: 0, //列表总数
                loadText: '加载中…',

                pageIndex: 1,
                pageSize: 10,
                cardlist: []
            }
        },
        computed: {},
        components: {
            "van-list": List

        },
        methods: {
            gettcarddetails(CouponTypeId) {
                var that = this;
                that.$router.push({
                    path: "/giftdetails",
                    query: {
                        gId: CouponTypeId,
                    }
                });
            },
            giftcardlist() {
                var that = this;
                //请求数据
                var requestData = {
                    "Page": {
                        "PageIndex": that.pageIndex,
                        "PageSize": that.pageSize
                    }
                };
                that.postData("/IndexData/GetCouponList", requestData).then(function(data) {

                    if (data.data.Code == "100") {
                        that.cardlist = that.cardlist.concat(data.data.Data.CouponList);
                        that.pageIndex += 1;
                        // 加载状态结束
                        that.loading = false;
                        // 数据全部加载完成
                        if (that.cardlist.length >= data.data.Data.Total) {
                            that.finished = true;
                        }
                    } else {
                        that.Common.showMsg(data.data.Message)
                    }

                });

            },
        },


        mounted() {
            var that = this;
            // that.getCouponList()
        },
        created() {


        },

    }
</script>
<style scoped src="../../static/css/giftcard.css">

</style>