<template>
    <div>
        <div class="mark" v-show="isShow">

        </div>
        <div class="showModelBox" v-show="isShow">
            <div class="showModelTitle">
                卡密
            </div>
            <div class="contentBox">
                <div class="contentItem">
                    <div class="contentTitle">
                        卡号 :
                    </div>
                    <div class="contentInput">
                        <input type="text" class="inputBox">
                    </div>
                </div>
                <div class="contentItem">
                    <div class="contentTitle">
                        输入号码 :
                    </div>
                    <div class="contentInput">
                        <input type="text" class="inputBox">
                    </div>
                </div>
            </div>
            <!--按钮-->
            <div class="cancelBtn" @click="clickHandle('cancel')">取消</div>
            <div class="comfirmBtn" @click="clickHandle('comfirm')">确定购买</div>
        </div>
    </div>

</template>

<script>

    export default {
        name: 'showModel',
        props:{
            isShow:Boolean,
        },
        computed: {

        },
        methods: {
            clickHandle:function(type){
                var that = this;
                that.$emit('responHandle',type)
            },
        },

    }
</script>
<style scoped src="../../static/css/showModel.css">

</style>
