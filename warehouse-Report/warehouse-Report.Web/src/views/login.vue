<template>
    <div>
        <b-row align-h="center" align-v="center" class="full-height  login-container mb-0">
            <b-col cols="3">
                <b-card class="mb-2">
                    <b-card-header class="bg-transparent">
                        <img src="@/assets/Logo.png" alt="" class="img-to-fit">
                    </b-card-header>
                    <b-card-text>
                        <b-form>
                            <b-row class="mb-0 px-0">
                                <b-col cols="12" class="px-0">
                                    <label>Username</label>
                                    <b-form-input v-model="username"></b-form-input>
                                </b-col>
                                <b-col cols="12" class="px-0 mt-2">
                                    <label>Password</label>
                                    <b-form-input v-model="password" type="password"></b-form-input>
                                </b-col>
                            </b-row>
                            <b-row align-h="center" class="mx-0 mb-0">
                                <b-button class="mt-3 primary" @click="loginUser" size="sm"  block squared>
                                    <b-spinner small v-if="isLoading"></b-spinner>
                                    Login
                                </b-button>
                            </b-row>
<!--                            <b-row class="mx-0">-->
<!--                                <b-button class="mt-3 primary-no-border" size="sm" variant="outline-primary" block squared>-->
<!--                                    Forgot Password?-->
<!--                                </b-button>-->
<!--                            </b-row>-->
                        </b-form>
                    </b-card-text>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
import {mapActions} from "vuex";

export default {
    name: "login",
    data: () => ({
        state: 'login',
        showErrors: false,
        errors: [],
        username: null,
        password: null,
        email: null,
        isLoading: false
    }),
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
        ...mapActions(["login"]),
        
        loginUser() {
            this.isLoading = true
            this.showErrors = false
            
            const request = {
                username: this.username,
                password: this.password 
            }
            
            this.$store.commit('setLoginRequest', request)
            
            this.login()
            .then((response) => {
                this.isLoading = false
                if (response.data.role[0] === "User") {
                    this.$router.push({name: 'userView'})
                    localStorage.setItem('jwt', response.data.token)
                    localStorage.setItem('role', response.data.role)
                }
                if (response.data.role[0] === "Admin") {
                    this.$router.push({name: 'adminView'})
                    localStorage.setItem('jwt', response.data.token)
                    localStorage.setItem('role', response.data.role)
                }
            })
            .catch(() => {
                console.log('User not logged in')
                this.isLoading = false
            })
        },
    },
    computed: {},
}
</script>

<style scoped lang="scss">
.login-container {
    .card {
        background: #f0f0f0;
        
        .form-control {
            //background: lightgray;
            border: 1px solid lightgray;
            
            .focus {
                background: green;
            }
        }
    }
}
</style>