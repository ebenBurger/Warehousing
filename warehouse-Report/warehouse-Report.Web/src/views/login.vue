<template>
    <div>
        <b-row align-h="center" align-v="center" class="full-height  login-container mb-0">
            <b-col cols="3">
                <b-card class="mb-2">
                    <b-card-header>
<!--                        <img src="@/assets/nextecLogo.png" alt="" class="img-to-fit">-->
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
                            <b-row class="mx-0">
                                <b-button class="mt-3 primary-no-border" size="sm" variant="outline-primary" block squared>
                                    Forgot Password?
                                </b-button>
                            </b-row>
                        </b-form>
                    </b-card-text>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
import {mapActions} from "vuex";
// import md5 from 'js-md5'

export default {
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
                console.log("RESPONSE", response)
                this.isLoading = false
                if (response.data.role[0] === "User") {
                    this.$router.push({path: '/home'})
                }
                if (response.data.role[0] === "Admin") {
                    this.$router.push({path: '/admin-home'})
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

<style scoped>

</style>