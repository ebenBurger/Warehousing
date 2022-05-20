import Vue from 'vue';
import App from './App.vue';
import VueRouter from "vue-router";
import ToggleButton from 'vue-js-toggle-button'
import ToggleSwitch from 'vuejs-toggle-switch'
import router from './router'
import store from "@/store";
import Toast from "vue-toastification";
import JsonExcel from "vue-json-excel";
import Vuelidate from 'vuelidate';
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import {
    faArrowsAltH, faArrowsAltV,
    faBiohazard,
    faBox, faCalendarDays, faCheck,
    faCogs,
    faDollar,
    faEdit, faFileExport,
    faFileInvoiceDollar,
    faHouseUser,
    faLock, faPeopleCarry,
    faPlaneArrival,
    faPlaneDeparture,
    faPlus,
    faPowerOff, faServer,
    faShip,
    faShippingFast,
    faSignsPost,
    faStopwatch, faTimes,
    faTowerBroadcast,
    faTrash,
    faTruckLoading,
    faUser,
    faUserCircle,
    faUserCog,
    faUserEdit,
    faUserPlus,
    faUserTie,
    faWindowClose
} from '@fortawesome/free-solid-svg-icons'

import { BootstrapVue, IconsPlugin } from "bootstrap-vue";

import "vue-toastification/dist/index.css";
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import './style.scss'


Vue.config.productionTip = true;

const toastOptions = {
    icon: true,
    position: 'top-center',
    draggable: true,
    pauseOnFocusLoss: true,
    pauseOnHover: false,
    timeout: 3000,
    newestOnTop: true
}

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(VueRouter)
Vue.use(ToggleButton)
Vue.use(Toast, toastOptions)
Vue.use(ToggleSwitch)
Vue.component("downloadExcel", JsonExcel);
Vue.use(Vuelidate)

Vue.filter('dateTimeFilter', function (value) {
    if (!value) return ''
    let localeValue = new Date(value)
    let response = localeValue.toLocaleDateString() + ' ' + localeValue.getHours().toString().padStart(2, '0') + ':' + localeValue.getMinutes().toString().padStart(2, '0')
    return response
})
Vue.filter('dateFilter', function (value) {
    if (!value) return ''
    let localeValue = new Date(value)
    let response = localeValue.toLocaleDateString("en-GB")
    return response
})

//FA icons
library.add( 
    faUserPlus, faPowerOff, faPlus, faCogs, faUserEdit, faHouseUser, faUserCog, 
    faUserTie, faUserCircle, faShippingFast, faTrash, faWindowClose, faEdit, faTruckLoading,
    faBiohazard, faBox, faFileInvoiceDollar, faDollar, faTrash, faUser, faLock, faTowerBroadcast,
    faPlaneArrival, faPlaneDeparture, faShip, faSignsPost, faStopwatch, faArrowsAltH, faArrowsAltV,
    faServer, faCalendarDays, faFileExport, faCheck, faTimes, faPeopleCarry
)
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
