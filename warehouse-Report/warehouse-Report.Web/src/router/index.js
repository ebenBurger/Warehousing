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
import completeCargo from "@/views/admin/adminViews/completeCargo";
import completedContainers from "@/views/admin/containers/completedContainers";

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
                path: '/home',
                name: 'adminView',
                component: adminView,
                meta: {
                    title: "Home",
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/cargo',
                name: 'completedCargo',
                component: completeCargo,
                meta: {
                    title: "Complete Shipment",
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/Add',
                name: 'createClient',
                component: createClient,
                meta: {
                    title: "Add Supplier",
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/client',
                name: 'clientView',
                component: viewClient,
                meta: {
                    title: "Client",
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/add',
                name: 'createUser',
                component: addUser,
                meta: {
                    title: "Add User",
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/adminUser',
                name: 'createAdminUser',
                component: addAdminUser,
                meta: {
                    title: "Add Admin",
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/container',
                name: 'containerView',
                component: viewContainer,
                meta: {
                    title: "Containers",
                    requiresAuth: true,
                    adminAuth: true,
                    userAuth: false,
                },
            },
            {
                path: '/container',
                name: 'completeContainer',
                component: completedContainers,
                meta: {
                    title: "Complete Containers",
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
            document.title = `${to.meta.title} | Atrax`
            router.push({path: '/'})
        } else {
            if (to.meta.adminAuth) {
                if (role === "Admin") {
                    document.title = `${to.meta.title} | Atrax`
                    return next()
                } else {
                    router.push({path: '/'})
                    document.title = `${to.meta.title} | Atrax`
                    localStorage.removeItem('jwt')
                    localStorage.removeItem('role')
                }
            } else if (to.meta.userAuth) {
                if (role === "User") {
                    document.title = `${to.meta.title} | Atrax`
                    return next()
                } else {
                    document.title = `${to.meta.title} | Atrax`
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