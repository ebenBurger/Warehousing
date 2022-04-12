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
        selectedSupplier: null,
        
        
        //request objects
        loginRequest: null,
        
        createSupplierRequest: null,
        supplierRequest: null,
    },
    mutations: {
        setApiUrl: (state, payload) => {
            state.baseUrl = payload
            localStorage.setItem('apiUrl', payload)
        },

        setLoginRequest: (state, payload) => {state.loginRequest = payload},
        
        setSupplierRequest: (state, payload) => {state.supplierRequest = payload},
        setSupplierClientRequest: (state, payload) => {state.createSupplierRequest = payload},
        setSelectedSupplier: (state, payload) => {state.selectedSupplier = payload},
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
        
        //client
        requestSupplier: ({state}) => {
            console.log("STATE", state)
          return new Promise((resolve, reject) => {
              const callConfig = {
                  method: 'get',
                  url: state.baseUrl + '/Client/GetAllClient',
                  headers: {
                      'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                      'Content-Type': 'application/json'
                  },
              }
              axios(callConfig)
                  .then(response => {
                      state.supplierRequest = response.data
                      resolve(response)
                  })
                  .catch(err => {
                      reject(err)
                      console.log("CLIENT REQUEST ERROR", err)
                  })
          })  
        },
        createSupplier: ({state}) => {
            const payload = state.createSupplierRequest
            
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
        updateSupplier: ({state}) => {
            const payload = state.selectedSupplier
            
            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'post',
                    url: state.baseUrl + '/Client/UpdateClient',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                    data: payload,
                }

                axios(callConfig)
                    .then(response => {
                        resolve(response)
                    })
                    .catch(err => {
                        reject(err)
                    })
            })
        }
        
        //stock
    },
    modules: {},
})