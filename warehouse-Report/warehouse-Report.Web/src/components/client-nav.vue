<template>
    <div>
        <b-navbar class="navbar">
            <b-navbar-brand class="text-white">
                Atrax Logistics - Warehouse Portal
            </b-navbar-brand>
            <div>
                <b-navbar-nav class="align-items-center">
<!--                    <router-link exact-active-class="active" :to="{name: 'userView'}">
                        <font-awesome-icon icon="fa-house-user" />
                        Home
                    </router-link>-->
                    <div class="text-center">
                        <h6>
                            Welcome {{user}}
                        </h6>
                    </div>
                    <div @click="openLogoutModal" class="logout navItemSpacing">
                        <font-awesome-icon icon="fa-power-off" />
                        Log Out
                    </div>
                </b-navbar-nav>
            </div>
        </b-navbar>

        <b-modal id="logoutModal" hide-footer title="Log Out">
            <b-row>
                <b-col cols="12" class="text-center"><p>Are you sure you want to sign out?</p></b-col>
            </b-row>
            <b-row>
                <b-col cols="12" class="text-center">
                    <b-button @click="closeLogoutModal" class="mr-2 ml-2" variant="outline-red" squared>No</b-button>
                    <b-button variant="outline-primary" @click="logout" class="mr-2 ml-2" squared>Yes</b-button>
                </b-col>
            </b-row>
        </b-modal>

    </div>
</template>

<script>
export default {
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
    },
}
</script>

<style scoped>
.navbar {
    display: flex;
    justify-content: space-between;
    background: lightgrey;
}

a {
    color: black;
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
    color: blue;
    text-decoration: none;
    border-radius: 12px;
}

.active {
    color: white;
    text-decoration: none;
    border-radius: 12px;
}

.logout {
    cursor: pointer;
    color: red;
}

.logout:hover {
    color: black;
}

.navItemSpacing {
    margin: 0 0.3rem;
    padding: 0.1rem 0.4rem;
}
</style>