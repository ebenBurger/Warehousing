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
        selectedCargo: null,
        selectedCompleteCargo: null,
        selectedPackage: null,
        selectedTransporter: null,
        selectedContainer: null,
        
        
        //request objects
        loginRequest: null,
        
        createSupplierRequest: null,
        supplierRequest: null,
        
        createCargoRequest: null,
        cargoRequest: null,
        cargoCompleteRequest: null,
        
        containerRequest: null,
        containerCompleterRequest: null,
        createContainerRequest: null,
        
        createPackageRequest: null,
        
        createTransporterRequest: null,
    },
    mutations: {
        setApiUrl: (state, payload) => {
            state.baseUrl = payload
            localStorage.setItem('apiUrl', payload)
        },

        setLoginRequest: (state, payload) => {state.loginRequest = payload},
        
        setSupplierRequest: (state, payload) => {state.supplierRequest = payload},
        setCreateSupplierRequest: (state, payload) => {state.createSupplierRequest = payload},
        setSelectedSupplier: (state, payload) => {state.selectedSupplier = payload},

        setCreateCargoRequest: (state, payload) => {state.createCargoRequest = payload},
        setCargoRequest: (state, payload) => {state.cargoRequest = payload},
        setCargoCompleteRequest: (state, payload) => {state.cargoCompleteRequest = payload},
        setSelectedCargo: (state, payload) => {state.selectedCargo = payload},
        setSelectedCompleteCargo: (state, payload) => {state.selectedCompleteCargo = payload},

        setCreateContainerRequest: (state, payload) => {state.createContainerRequest = payload},
        setContainerRequest: (state, payload) => {state.containerRequest = payload},
        setContainerCompleteRequest: (state, payload) => {state.containerCompleterRequest = payload},
        setSelectedContainer: (state, payload) => {state.selectedContainer = payload},
        
        setCreatePackageRequest: (state, payload) => {state.createPackageRequest = payload},
        setSelectedPackage: (state, payload) => {state.selectedPackage = payload},

        setCreateTransporterRequest: (state, payload) => {state.createTransporterRequest = payload},
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
                        Vue.$toast("Welcome to the Portal");
                    })
                    .catch((err) => {
                        reject()
                        console.log("ERROR", err)
                        Vue.$toast.error("Oops!!! try that again, seems like your username or password was incorrect there");
                    })
            })
        },
        
        //Add Users - Admin
        
        //Add User - User
        
        //Supplier
        requestSupplier: ({state}) => {
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
                      Vue.$toast.success("Lets welcome to the new guy");
                    })
                    .catch(err => {
                        console.log('CLIENT SAVING ERROR', err)
                        reject(err)
                        Vue.$toast.error("There was an error");
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
                        Vue.$toast.success("Well, that was smooth, all good from this side");
                    })
                    .catch(err => {
                        reject(err)
                        Vue.$toast.error("There was an error");
                    })
            })
        },
        
        //cargo
        createCargo: ({state}) => {
            const payload = state.createCargoRequest

            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'post',
                    url: state.baseUrl + '/Cargo/AddCargo',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                    data: payload,
                }

                axios(callConfig)
                    .then((response) => {
                        resolve(response)
                        Vue.$toast.success("Cargo captured and ready for packaging");
                    })
                    .catch(err => {
                        reject(err)
                        Vue.$toast.error("There was an error");
                    })
            })
        },
        requestCargo: ({state}) => {
            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'get',
                    url: state.baseUrl + '/Cargo/GetAllAvailCargo',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                }
                axios(callConfig)
                    .then(response => {
                        state.cargoRequest = response.data
                        resolve(response)
                        console.log("CARGO LIST", response.data)
                    })
                    .catch(err => {
                        reject(err)
                        console.log("CLIENT REQUEST ERROR", err)
                    })
            })
        },
        requestCompleteCargo: ({state}) => {
            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'get',
                    url: state.baseUrl + '/Cargo/GetAllCompleteCargo',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                }
                axios(callConfig)
                    .then(response => {
                        state.cargoCompleteRequest = response.data
                        resolve(response)
                        console.log("COMPLETE CARGO LIST", response.data)
                    })
                    .catch(err => {
                        reject(err)
                        console.log("CLIENT REQUEST ERROR", err)
                    })
            })
        },
        updateCargo: ({state}) => {
            const payload = state.selectedCargo

            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'post',
                    url: state.baseUrl + '/Cargo/UpdateCargo',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                    data: payload,
                }

                axios(callConfig)
                    .then(response => {
                        resolve(response)
                        Vue.$toast.success("Cargo Updated");
                    })
                    .catch(err => {
                        reject(err)
                        Vue.$toast.error("There was an error");
                    })
            })
        },
        restoreCargo: ({state}) => {
            const payload = state.selectedCompleteCargo

            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'post',
                    url: state.baseUrl + '/Cargo/UpdateCargo',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                    data: payload,
                }

                axios(callConfig)
                  .then(response => {
                      resolve(response)
                      Vue.$toast.success("Cargo restored");
                  })
                  .catch(err => {
                      reject(err)
                      Vue.$toast.error("There was an error with restoring");
                  })
            })
        },
        
        //Package
        createPackage: ({state}) => {
            const payload = state.createPackageRequest

            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'post',
                    url: state.baseUrl + '/Package/CreatePackage',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                    data: payload,
                }

                axios(callConfig)
                    .then((response) => {
                        resolve(response)
                        Vue.$toast.success("Package was saved");
                    })
                    .catch(err => {
                        reject(err)
                        Vue.$toast.error("There was an error");
                    })
            })
        },
        updatePackage: ({state}) => {
            const payload = state.selectedPackage

            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'post',
                    url: state.baseUrl + '/Package/UpdatePackage',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                    data: payload,
                }

                axios(callConfig)
                    .then(response => {
                        resolve(response)
                        // Vue.$toast.success("Package Updated");
                    })
                    .catch(err => {
                        reject(err)
                        Vue.$toast.error("There was an error");
                    })
            })
        },
        
        //Transporter
        createTransport: ({state}) => {
            const payload = state.createTransporterRequest

            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'post',
                    url: state.baseUrl + '/Transporter/CreateTransporter',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                    data: payload,
                }

                axios(callConfig)
                  .then((response) => {
                      resolve(response)
                      Vue.$toast.success("Transport Captured");
                  })
                  .catch(err => {
                      reject(err)
                      Vue.$toast.error("There was an error");
                  })
            })
        },
        
        //containers
        createContainer: ({state}) => {
            const payload = state.createContainerRequest

            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'post',
                    url: state.baseUrl + '/Container/CreateContainer',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                    data: payload,
                }

                axios(callConfig)
                    .then((response) => {
                        resolve(response)
                        Vue.$toast.success("Container Created");
                    })
                    .catch(err => {
                        reject(err)
                        Vue.$toast.error("There was an error");
                    })
            })
        },
        requestContainer: ({state}) => {
            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'get',
                    url: state.baseUrl + '/Container/GetAllAvailContainers',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                }
                axios(callConfig)
                    .then(response => {
                        state.containerRequest = response.data
                        resolve(response)
                    })
                    .catch(err => {
                        reject(err)
                        console.log("CONTAINER REQUEST ERROR", err)
                    })
            })
        },
        requestCompleteContainer: ({state}) => {
            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'get',
                    url: state.baseUrl + '/Container/GetAllCompleteContainers',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                }
                axios(callConfig)
                    .then(response => {
                        state.containerCompleterRequest = response.data
                        resolve(response)
                        console.log("CONTAINER Complete REQUEST ERROR", response.data)
                    })
                    .catch(err => {
                        reject(err)
                        console.log("CONTAINER Complete REQUEST ERROR", err)
                    })
            })
        },
        updateContainer: ({state}) => {
            const payload = state.selectedContainer

            return new Promise((resolve, reject) => {
                const callConfig = {
                    method: 'post',
                    url: state.baseUrl + '/Container/UpdateContainer',
                    headers: {
                        'Authorization': 'Bearer '+ localStorage.getItem('jwt'),
                        'Content-Type': 'application/json'
                    },
                    data: payload,
                }

                axios(callConfig)
                  .then(response => {
                      resolve(response)
                      Vue.$toast.success("Container Updated");
                  })
                  .catch(err => {
                      reject(err)
                      Vue.$toast.error("There was an error");
                  })
            })
        },
    },
    modules: {},
})