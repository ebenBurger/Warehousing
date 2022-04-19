import Vue from "vue";
import login from "@/views/login";
import VueRouter from 'vue-router'

import layout from "@/views/layouts/layout";
import userView from "@/views/user/userViews/user-view";

import adminLayout from "@/views/layouts/adminLayout";
import adminView from "@/views/admin/adminViews/admin-view";
import createClient from "@/views/admin/clientViews/createClient";
import viewClient from "@/views/admin/clientViews/viewClient";
import addUser from "@/views/admin/user/addUser";
import addAdminUser from "@/views/admin/user/addAdminUser";
import viewContainer from "@/views/admin/containers/viewContainer";

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'login',
        component: login,
        meta: {
            requiresAuth: false,
            adminAuth: false,
            userAuth: false,
        },
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
                    adminAuth: false,
                    userAuth: true,
                },
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
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/clientAdd',
                name: 'createClient',
                component: createClient,
                meta: {
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/clientView',
                name: 'viewClient',
                component: viewClient,
                meta: {
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/addUser',
                name: 'addUser',
                component: addUser,
                meta: {
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/addAdminUser',
                name: 'addAdminUser',
                component: addAdminUser,
                meta: {
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/viewContainer',
                name: 'viewContainer',
                component: viewContainer,
                meta: {
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
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
    let role = localStorage.getItem('role')
    const token = localStorage.getItem('jwt')

    if (to.meta.requiresAuth) {
        if (!role || !token) {
            router.push({path: '/'})
        } else {
            if (to.meta.adminAuth) {
                if (role === "Admin") {
                    return next()
                } else {
                    router.push({path: '/'})
                    localStorage.removeItem('jwt')
                    localStorage.removeItem('role')
                }
            } else if (to.meta.userAuth) {
                if (role === "User") {
                    return next()
                } else {
                    router.push({path: '/'})
                    localStorage.removeItem('jwt')
                    localStorage.removeItem('role')
                }
            }
        }

    } else {
        return next()
    }
})
    
export default router