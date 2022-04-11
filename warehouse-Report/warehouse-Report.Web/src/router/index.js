import Vue from "vue";
import login from "@/views/login";
import VueRouter from 'vue-router'

import layout from "@/views/layout";
import adminView from "@/views/admin-view";
import userView from "@/views/user-view";

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'login',
        component: login,
        meta: {
            requiresAuth: false
        }
    },

    {
        path: '/',
        name: 'layout',
        component: layout,
        children: [
            {
                path: '/admin-view',
                name: 'adminMain',
                component: adminView,
                meta: {
                    requiresAuth: true,
                }
            },
            {
                path: '/user-view',
                name: 'userMain',
                component: userView,
                meta: {
                    requiresAuth: true,
                }
            },
        ]
    },
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

router.beforeEach((to, from, next) => {
    if(to.matched.some(record => record.meta.requiresAuth)) {

        if (localStorage.getItem('jwt') !== null && localStorage.getItem('jwt') !== '') {
            next()
        }
        else{
            if (to.name !== 'Login') {
                next('')
            } else {
                next()
            }
        }

    } else {
        next()
    }
})

export default router