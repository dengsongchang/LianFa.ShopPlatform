import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        //收货地址列表
        ShipAddressList: [],
        //footer-Nav
        footerActive:'home',
    },
    getters:{
        addressList: state => {
            var list = state.ShipAddressList;
            var addressList = [];
            list.forEach(function(item,index){
                var data = {};
                data.id = item.SAId;
                data.name = item.Consignee;
                data.tel = item.Mobile;
                data.address = item.ProvinceName + item.CityName + item.RName + item.Address;
                addressList.push(data)
            })
            return addressList
        },
    },
    mutations: {
        //设置收货地址列表
        setShipAddressList: function (state, list) {
            state.ShipAddressList = list;
        },
        //设置底部nav栏
        setFooterNav:function(state,type){
            state.footerActive = type;
        },
    },
    actions: {
        //获取收货地址列表
        shipAddressList({commit}, {that: that, requstData: requstData}) {
            return new Promise ( (resolve, reject ) => {
                that.postData("/ShipAddress/ShipAddressList", requstData).then(function (data) {
                    if (data.data.Code == "100") {
                        commit('setShipAddressList', data.data.Data.ShipAddressList);
                        setTimeout(function(){
                            resolve()
                        },5000)
                    } else {
                        that.Common.showMsg(data.data.Message);
                        reject()
                    }
                });
            })


        }
    }
})
