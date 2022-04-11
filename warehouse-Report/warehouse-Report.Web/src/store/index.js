import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        baseUrl: '',
        userIdentifier: null,
        user: null,
        
        //selected Item
        
        
        //request objects
        loginRequest: null,
    },
    mutations: {
        setApiUrl: (state, payload) => {
            state.baseUrl = payload
            localStorage.setItem('apiUrl', payload)
        },

        setLoginRequest: (state, payload) => {state.loginRequest = payload},
    },
    actions: {
        login: ({state, dispatch}) => {
            let payload = state.loginRequest
            return new Promise((resolve, reject) =>{
                axios.post(state.baseUrl + 'Token/auth', payload)
                    .then(response => {
                        localStorage.setItem('jwt', response.data.securityToken)
                        // localStorage.setItem('user', response.data.user.name + " " + response.data.user.surname)
                        // localStorage.setItem('userEmail', response.data.user.email)
                        state.user = response.data.user
                        resolve(response)
                    })
                    .catch((err) => {
                        reject()
                        console.log("ERROR", err)
                    })
            })
        },
    },
    modules: {},
})