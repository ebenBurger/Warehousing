<template>
    <div>
        <b-navbar class="navbar">
            <b-navbar-brand class="text-white">
                <img src="../assets/Logo.png" alt="Atrax Logo">
            </b-navbar-brand>
            <div class="text-white" v-if="user !== null">
                <h3>Welcome {{user}}</h3>
            </div>
            <div>
                <b-navbar-nav class="align-items-center">
                    <router-link v-if="role === 'Admin'" exact-active-class="active" :to="{name: 'adminView'}">
                        <font-awesome-icon icon="fa-house-user" />
                        Home
                    </router-link>
                    <router-link v-if="role === 'Admin'" exact-active-class="active" :to="{name: 'containerView'}">
                        <font-awesome-icon icon="fa-shipping-fast" />
                        Containers
                    </router-link>
                    <router-link v-if="role === 'Admin'" exact-active-class="active" :to="{name: 'clientView'}">
                        <font-awesome-icon icon="fa-user-tie" />
                        Client
                    </router-link>
                    <div v-if="role === 'Admin'" v-b-toggle.sidebar-backdrop class=" admin navItemSpacing">
                        <font-awesome-icon icon="fa-cogs" />
                        Admin
                    </div>
                    <div @click="openLogoutModal" class="logout navItemSpacing">
                        <font-awesome-icon icon="fa-power-off" />
                        Log Out
                    </div>
                </b-navbar-nav>
            </div>
        </b-navbar>

        <b-modal id="logoutModal" hide-footer>
            <b-row>
                <b-col cols="12" class="text-center">
                    <p>
                        <span>{{user}}</span>
                        are you sure you want to sign out?
                    </p>
                </b-col>
            </b-row>
            <b-row>
                <b-col cols="12" class="text-center">
                    <b-button @click="closeLogoutModal" class="mr-2 ml-2" variant="outline-red" squared>No</b-button>
                    <b-button variant="outline-primary" @click="logout" class="mr-2 ml-2" squared>Yes</b-button>
                </b-col>
            </b-row>
            <div class="d-block"></div>
        </b-modal>

        <b-sidebar id="sidebar-backdrop"
                   title="Edit Pages"
                   backdrop
                   shadow>
            <template #default="{ hide }">
                <div class="p-3">
                    <h4 id="sidebar-no-header-title">Admin Selections</h4>
                    <nav class="mb-3">
                        <b-nav vertical>
                            <b-nav-item :to="{name: 'createClient'}">
                                <font-awesome-icon icon="fa-user-plus" />
                                Add Supplier
                            </b-nav-item>
                            <b-nav-item :to="{name: 'createUser'}">
                                <font-awesome-icon icon="fa-user-cog" />
                                Add User
                            </b-nav-item>
                            <b-nav-item :to="{name: 'createAdminUser'}">
                                <font-awesome-icon icon="fa-user-cog" />
                                Add Admin User
                            </b-nav-item>
                        </b-nav>
                    </nav>
                    <b-button variant="primary" @click="hide">Close</b-button>
                </div>
            </template>
        </b-sidebar>
    </div>
</template>

<script>
export default {
    name: "admin-nav",
    data: () => ({}),
    beforeCreate() {
    },
    created() {
    },
    beforeMount() {
    },
    mounted() {
    },
    beforeUpdate() {
    },
    updated() {
    },
    methods: {
        logout() {
            this.$router.push({name: 'login'})
            localStorage.removeItem('jwt')
            localStorage.removeItem('user')
            window.location.reload()
        },
        openLogoutModal() {
            this.$bvModal.show('logoutModal')
        },
        closeLogoutModal() {
            this.$bvModal.hide('logoutModal')
        },
    },
    computed: {
        user() {
            return localStorage.getItem('user') 
        },
        role() {
            return localStorage.getItem('role')
        },
    },
}
</script>

<style scoped lang="scss">

.navbar {
    display: flex;
    justify-content: space-between;
    background-image: linear-gradient(to left, #176cb0, lightgray);
    //background: #176cb0;
    
    .navbar-brand {
        img {
            width: 14rem;
        }
    }
}

a {
    color: white;
    margin: 0 0.3rem;
    padding: 0.1rem 0.4rem;
}

.collapsed {
    color: black;
}
.not-collapsed {
    color: white;
}

ul a:hover {
    color: #e66b0c;
    text-decoration: none;
    border-radius: 12px;
}

.active {
    color: #e66b0c;
    text-decoration: none;
    border-radius: 12px;
}

.logout {
    cursor: pointer;
    color: red;
}

.logout:hover {
    color: #e66b0c;
}
.admin {
    color: white;
    
    &:hover {
        color: #e66b0c;
    }
}

.navItemSpacing {
    margin: 0 0.3rem;
    padding: 0.1rem 0.4rem;
}
</style>