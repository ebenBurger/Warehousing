<template>
    <div>
        <b-navbar class="navbar">
            <b-navbar-brand class="text-white">
                Atrax Logistics - Warehousing - Admin Portal
            </b-navbar-brand>
            <div>
                <b-navbar-nav>
                    <router-link exact-active-class="active" to="/client">
                        <font-awesome-icon icon="fa-user-plus" />
                        Client
                    </router-link>
                    <div @click="openLogoutModal" class="logout">
                        <font-awesome-icon icon="fa-power-off" />
                        Log Out
                    </div>
                </b-navbar-nav>
            </div>
        </b-navbar>

        <b-modal id="logoutModal" hide-footer>
            <b-row>
                <b-col cols="12" class="text-center"><p>Are you sure you want to sign out?</p></b-col>
            </b-row>
            <b-row>
                <b-col cols="12" class="text-center">
                    <b-button @click="closeLogoutModal" class="mr-2 ml-2" variant="outline-red" squared>No</b-button>
                    <b-button variant="outline-primary" @click="logout" class="mr-2 ml-2" squared>Yes</b-button>
                </b-col>
            </b-row>
            <div class="d-block"></div>
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
            this.$router.push({path: '/'})
        },
        openLogoutModal() {
            this.$bvModal.show('logoutModal')
        },
        closeLogoutModal() {
            this.$bvModal.hide('logoutModal')
        },
    },
    computed: {},
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
    background: white;
    padding: 0.1rem 0.4rem;
    border-radius: 12px;
}

ul a:hover {
    color: white;
    text-decoration: none;
    background: blue;
    padding: 0.1rem 0.4rem;
    border-radius: 12px;
}

.active {
    color: white;
    text-decoration: none;
    background: blue;
    padding: 0.1rem 0.4rem;
    border-radius: 12px;
}

.logout {
    cursor: pointer;
}
</style>