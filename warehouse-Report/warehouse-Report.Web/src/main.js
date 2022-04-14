import Vue from 'vue';
import App from './App.vue';
import VueRouter from "vue-router";
import ToggleButton from 'vue-js-toggle-button'
import router from './router'
import store from "@/store";
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import {
    faBiohazard,
    faCogs, faFileInvoiceDollar,
    faHouseUser, faListOl,
    faPlus,
    faPowerOff, faReceipt, faUserCircle,
    faUserCog,
    faUserEdit,
    faUserPlus, faUserTie
} from '@fortawesome/free-solid-svg-icons'

import { BootstrapVue, IconsPlugin } from "bootstrap-vue";

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import './style.scss'


Vue.config.productionTip = true;

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(VueRouter)
Vue.use(ToggleButton)

Vue.filter('dateTimeFilter', function (value) {
    if (!value) return ''
    let localeValue = new Date(value)
    let response = localeValue.toLocaleDateString() + ' ' + localeValue.getHours().toString().padStart(2, '0') + ':' + localeValue.getMinutes().toString().padStart(2, '0')
    return response
})
Vue.filter('dateFilter', function (value) {
    if (!value) return ''
    let localeValue = new Date(value)
    let response = localeValue.toLocaleDateString()
    return response
})

//FA icons
library.add( faUserPlus, faPowerOff, faPlus, faCogs, faUserEdit, faHouseUser, faUserCog, faUserTie, faUserCircle, faBiohazard, faFileInvoiceDollar, faListOl, faReceipt )
Vue.component('font-awesome-icon', FontAwesomeIcon)


fetch('/config.json')
    .then(res => res.json())
    .then(config => {
        new Vue({
            data: () => ({
                urlConfig : config
            }),
            router,
            store,
            render: h => h(App)
        }).$mount('#app')
    })
