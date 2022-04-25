<template>
    <div>
        <b-row>
            <b-col>
                <b-card>
                    <b-row>
                        <b-col>
                            <div class="d-flex justify-content-between align-items-center">
                                <h4 class="m-0">Warehouse Report</h4>
                            </div>
                            <b-col class="text-right">
                                <b-button variant="outline-primary" size="sm" squared @click="openContainerModal">
                                    <font-awesome-icon icon="fa-plus" class="mr-1" />
                                    Add Container
                                </b-button>
                            </b-col>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <h4 class="m-0">Available Containers</h4>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col class="d-flex w-100 h-100">
                            <b-card class="containerItem" v-for="(container) in availableContainers" :key="container.containerId">
                                <p>
                                    {{container.containerName}}
                                </p>
                                
                            </b-card>
                        </b-col>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
        <b-modal id="containerAddModal" hide-footer hide-header-close class="text-center" title="Add Cargo" size="xl">
            <b-row>
                <b-col>
                    <label>Container Name</label>
                    <b-form-input v-model="containerData.containerName" />
                </b-col>
            </b-row>
            <b-row class="mt-3">
                <b-col class="d-flex justify-content-center">
                    <toggle-switch
                        :options="option20ft"
                        @change="toggleContainers($event.value)" 
                    />
                </b-col>
            </b-row>
            <b-row class="mt-3">
                <b-col class="d-flex justify-content-center">
                    <toggle-switch
                        :options="option40ft"
                        @change="toggleContainers($event.value)"
                    />
                </b-col>
            </b-row>
            <hr class="mx-3">
            <b-row>
                <b-col>
                    <div class="d-flex justify-content-end">
                        <div>
                            <b-button variant="outline-red" squared @click="hideContainerModal" class="ml-2">Cancel</b-button>
                        </div>
                        <div>
                            <b-button variant="primary" squared @click="save" class="ml-2">Save</b-button>
                        </div>
                    </div>
                </b-col>
            </b-row>
        </b-modal>
    </div>
</template>

<script>
import {mapActions} from "vuex";

export default {
    data: () => ({
        containerData: {
            containerName: null,
            gp20ft: false,
            frig20ft: false,
            froog20ft: false,
            toig20ft: false,
            otooog20ft: false,
            
            gp40ft: false,
            hq40ft: false,
            frig40ft: false,
            feoog40ft: false,
            otig40ft: false,
            otoog40ft: false,
            
            isActive: null,
        },
        availableContainers: [],
        option20ft: {
            layout: {
                color: 'black',
                backgroundColor: 'white',
                selectedColor: 'white',
                selectedBackgroundColor: 'blue',
                borderColor: 'black',
                fontFamily: 'Arial',
                fontWeight: 'normal',
                fontWeightSelected: 'bold',
                squareCorners: false,
                noBorder: false
            },
            size: {
                fontSize: 0.9,
                height: 2,
                padding: 0.3,
                width: 35,
            },
            items: {
                delay: .4,
                preSelected: 'unknown',
                disabled: false,
                labels: [
                    {name: '20ft GP'},
                    {name: '20ft FR IG'},
                    {name: '20ft FR OOG'},
                    {name: '20ft TO OOG'},
                    {name: '20ft OTO OOG'},
                ]
            }
        },
        option40ft: {
            layout: {
                color: 'black',
                backgroundColor: 'white',
                selectedColor: 'white',
                selectedBackgroundColor: 'blue',
                borderColor: 'black',
                fontFamily: 'Arial',
                fontWeight: 'normal',
                fontWeightSelected: 'bold',
                squareCorners: false,
                noBorder: false
            },
            size: {
                fontSize: 0.9,
                height: 2,
                padding: 0.3,
                width: 35,
            },
            items: {
                delay: .4,
                preSelected: 'unknown',
                disabled: false,
                labels: [
                    {name: '40ft GP'},
                    {name: '40ft HQ'},
                    {name: '40ft FR IG'},
                    {name: '40ft TO IG'},
                    {name: '40ft OT OOG'},
                ]
            }
        },
    }),
    beforeCreate() {
    },
    created() {
        this.getContainers()
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
        ...mapActions(["createContainer", "requestContainer"]),
        
        openContainerModal() {
            this.$bvModal.show('containerAddModal')
        },
        hideContainerModal() {
            this.$bvModal.hide('containerAddModal')
        },
        save() {
            this.containerData.isActive = true
            this.$store.commit('setCreateContainerRequest', this.containerData)
            this.createContainer()
            .then(() => {
                this.hideContainerModal()
                this.getContainers()
            })
            
        },
        getContainers() {
            this.requestContainer()
            .then((response) => {
                this.availableContainers = response.data
            })
            .catch((err) => {
                this.$router.push({path: '/'})
                localStorage.removeItem('jwt')
                localStorage.removeItem('user')
                console.log('ERROR', err)
            })
        },
        toggleContainers(event) {
            switch (event){
                case '20ft GP':
                    this.containerData.gp20ft = true
                    this.containerData.frig20ft = false
                    this.containerData.froog20ft = false
                    this.containerData.toig20ft = false
                    this.containerData.otooog20ft = false
                    console.log('EVENT', event)
                    console.log('GP20ft', this.containerData.gp20ft)
                    console.log('FRIG20ft', this.containerData.frig20ft)
                    break
                case '20ft FR IG':
                    this.containerData.gp20ft = false
                    this.containerData.frig20ft = true
                    console.log('EVENT', event)
                    console.log('GP20ft', this.containerData.gp20ft)
                    console.log('FRIG20ft', this.containerData.frig20ft)
                    break
            }
        },
    },
    computed: {},
}
</script>

<style scoped>
.containerItem {
    background: red;
    margin: 1rem;
    width: 10rem;
    height: 10rem;
    cursor: pointer;
    position: relative;
}

.containerItem p {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    text-align: center;
}
</style>