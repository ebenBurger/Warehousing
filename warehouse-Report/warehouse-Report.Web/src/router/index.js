import Vue from "vue";
import login from "@/views/login";
import VueRouter from 'vue-router'

import layout from "@/views/layouts/layout";
import userView from "@/views/user-view";

import adminView from "@/views/admin-view";
import adminLayout from "@/views/layouts/adminLayout";
import createClient from "@/views/clientViews/createClient";

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

    //CLIENT PORTAL
    {
        path: '/',
        name: 'layout',
        component: layout,
        children: [
            
            {
                path: '/home',
                name: 'userMain',
                component: userView,
                meta: {
                    requiresAuth: true,
                }
            },
        ]
    },

    //ADMIN PORTAL
    {
        path: '/',
        name: 'adminLayout',
        component: adminLayout,
        children: [

            {
                path: '/admin-home',
                name: 'adminMain',
                component: adminView,
                meta: {
                    requiresAuth: true,
                }
            },
            {
                path: '/client',
                name: 'client',
                component: createClient,
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

//TODO fix Route Guarding for the whole app.
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