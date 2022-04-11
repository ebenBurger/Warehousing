import Vue from 'vue';
import App from './App.vue';
import VueRouter from "vue-router";
import router from './router'
import store from "@/store";

import { BootstrapVue, IconsPlugin } from "bootstrap-vue";

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import './style.scss'


Vue.config.productionTip = true;

Vue.use(BootstrapVue)
Vue.use(IconsPlugin)
Vue.use(VueRouter)


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
