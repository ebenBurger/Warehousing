import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        baseUrl: 'https://localhost:5001',
        userIdentifier: null,//not used
        user: null,//not used
        
        //selected Item
        selectedClient: null,
        
        
        //request objects
        loginRequest: null,
        createClientRequest: null,
    },
    mutations: {
        setApiUrl: (state, payload) => {
            state.baseUrl = payload
            localStorage.setItem('apiUrl', payload)
        },

        setLoginRequest: (state, payload) => {state.loginRequest = payload},
        
        setCreateClientRequest: (state, payload) => {state.createClientRequest = payload},
        
        setSelectedClient: (state, payload) => {state.selectedClient = payload},
    },
    actions: {
        login: ({state}) => {
            let payload = state.loginRequest
            return new Promise((resolve, reject) =>{
                axios.post(state.baseUrl + '/Authentication/login', payload)
                    .then(response => {
                        localStorage.setItem('jwt', response.data.token)
                        localStorage.setItem('role', response.data.role[0])
                        resolve(response)
                    })
                    .catch((err) => {
                        reject()
                        console.log("ERROR", err)
                    })
            })
        },
        createClient: ({state}) => {
            const payload = state.createClientRequest
            
            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'post',
                    url: state.baseUrl + '/Client/AddClient',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                    data: payload,
                }
                
                axios(callConfig)
                    .then((response) => {
                      resolve(response)  
                    })
                    .catch(err => {
                        console.log('CLIENT SAVING ERROR', err)
                        reject(err)
                    })
            })
        },
    },
    modules: {},
})