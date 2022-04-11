import Vue from 'vue';
import App from './App.vue';
import VueRouter from "vue-router";
import Routes from './router/index'
import VueMq from "vue-mq";
import store from "@/store";

Vue.config.productionTip = true;

new Vue({
    render: h => h(App)
}).$mount('#app');
