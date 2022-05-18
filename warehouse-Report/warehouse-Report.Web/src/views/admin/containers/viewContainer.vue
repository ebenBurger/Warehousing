<template>
    <div>
        <Loader v-if="loading" />
        
        <div v-if="!loading">
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
                                    <b-button class="ml-3" variant="outline-primary" size="sm" squared @click="openCompleteContainers">
                                        <font-awesome-icon icon="fa-truck-loading" class="mr-1" />
                                        Completed Containers
                                    </b-button>
                                </b-col>
                            </b-col>
                        </b-row>
                        <div v-if="this.availableContainers.length >= 0">
                            <b-row>
                                <b-col>
                                    <h4 class="m-0">Containers Loading</h4>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col class="d-flex cursor-pointer">
                                    <b-card class="containerItem cursor-pointer" v-for="(container) in availableContainers.filter(a => a.status === 'Loading')" :key="container.containerId" @click="openContainerContentModal(container)">
                                        <div>
                                            <label class="w-100">
                                                {{container.fileReference}}
                                            </label>
                                            <label class="w-100">
                                                {{container.containerNumber}}
                                            </label>
                                            <label class="w-100">
                                                ({{container.containerType}})
                                            </label>
                                        </div>
                                    </b-card>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <h4 class="m-0">Available Containers</h4>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col class="d-flex cursor-pointer">
                                    <div class="d-flex flex-wrap" v-if="availableContainers">
                                        <b-card class="containerItem cursor-pointer"
                                                v-for="(container) in availableContainers.filter(a => a.status === 'Available')"
                                                :class="container.status"
                                                :key="container.containerId"
                                                @click="openContainerContentModal(container)">
                                            <div>
                                                <label class="w-100">
                                                    {{container.fileReference}}
                                                </label>
                                                <label class="w-100">
                                                    {{container.containerNumber}}
                                                </label>
                                                <label class="w-100">
                                                    ({{container.containerType}})
                                                </label>
                                            </div>
                                        </b-card>
                                    </div>
                                </b-col>
                            </b-row>
                        </div>
                        <div v-if="availableContainers.length === 0">
                            <h1 class="d-flex justify-content-center">Nothing To See Here</h1>
                        </div>
                    </b-card>
                </b-col>
            </b-row>
            <b-modal id="containerAddModal" hide-footer hide-header-close class="text-center" title="Create Container" size="xl" :no-close-on-backdrop="true">
                <b-row>
                    <b-col>
                        <label>File Reference</label>
                        <b-form-input v-model="containerData.fileReference" />
                    </b-col>
                    <b-col>
                        <label>Container Number</label>
                        <b-form-input v-model="containerData.containerNumber" />
                    </b-col>
                    <b-col>
                        <label>Container Type</label>
                        <b-form-select v-model="containerData.containerType" :options="options"></b-form-select>
                    </b-col>
                </b-row>
                <b-row>
                    <b-col>
                        <label>Vessel</label>
                        <b-form-input v-model="containerData.vesel" />
                    </b-col>
                    <b-col>
                        <label>Voyage</label>
                        <b-form-input v-model="containerData.voyage" />
                    </b-col>
                    <b-col>
                        <label>Bill of Lading</label>
                        <b-form-input v-model="containerData.billOfLading" />
                    </b-col>
                </b-row>
                <b-row>
                    <b-col cols="4">
                        <label>Loading Port</label>
                        <b-form-input v-model="containerData.loadingPort" />
                    </b-col>
                    <b-col cols="4">
                        <label>Destination Port</label>
                        <b-form-input v-model="containerData.destinationPort" />
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

            <b-modal v-if="selectedContainer" id="containerContentModal" size="xl" hide-footer hide-header-close class="text-center" title="Update/ View Container" :no-close-on-backdrop="true">
                <b-row class="mb-3">
                    <b-col>
                        <label class="font-weight-bold">Container Number</label>
                        {{selectedContainer.containerNumber}}
                    </b-col>
                    <b-col>
                        <label>
                            <font-awesome-icon icon="fa-ship" />
                            <small class="ml-2 text-info">Vessel</small>
                        </label>
                        {{selectedContainer.vesel}}
                    </b-col>
                    <b-col>
                        <label>
                            <font-awesome-icon icon="fa-signs-post" />
                            <small class="ml-2 text-info">Voyage</small>
                        </label>
                        {{selectedContainer.voyage}}
                    </b-col>
                    <b-col>
                        <label>
                            <font-awesome-icon icon="fa-plane-departure" />
                            <small class="ml-2 text-info">Loading</small>
                        </label>
                        {{selectedContainer.loadingPort}}
                    </b-col>
                    <b-col>
                        <label>
                            <font-awesome-icon icon="fa-plane-arrival" />
                            <small class="ml-2 text-info">Arrival</small>
                        </label>
                        {{selectedContainer.destinationPort}}
                    </b-col>
                    <b-col>
                        <label>
                            <font-awesome-icon icon="fa-arrows-alt-v" />
                            <font-awesome-icon icon="fa-arrows-alt-h" />
                            <small class="ml-2 text-info">Size</small>
                        </label>
                        {{selectedContainer.containerType}}
                    </b-col>
                </b-row>
                <b-tabs fill>
                    <b-tab title="Update Container">
                        <b-form>
                            <b-row>
                                <b-col>
                                    <label>Seal One</label>
                                    <b-form-input v-model="selectedContainer.sealOne"/>
                                </b-col>
                                <b-col>
                                    <label>Seal Two</label>
                                    <b-form-input v-model="selectedContainer.sealTwo"/>
                                </b-col>
                            </b-row>
                            <b-row>
                                <b-col cols="6">
                                    <label>Packing Loacation</label>
                                    <b-form-input v-model="selectedContainer.packingLocation"/>
                                </b-col>
                                <b-col cols="6">
                                    <label>Packing Date</label>
                                    <b-form-datepicker
                                        reset-button
                                        v-model="selectedContainer.packingDate"
                                    />
                                </b-col>
                            </b-row>
                            <b-row>

                            </b-row>
                            <b-row>
                                <b-col cols="1">
                                    <label>Complete</label>
                                    <toggle-button :value="false"
                                                   v-model="selectedContainer.isComplete"/>
                                </b-col>
                            </b-row>
                        </b-form>
                    </b-tab>
                    <b-tab title="View Cargo Loaded">
                        <div v-show="selectedContainer.cargo.length === 0">
                            <h4 class="my-5 text-center">
                                This container is empty
                            </h4>
                        </div>
                        <hr class="mx-3">
                        <div v-show="selectedContainer.cargo.length > 0">
                            <div class="mt-3" v-for="(item, index) in selectedContainer.cargo" :key="index">
                                <b-row  v-if="item.isActive">
                                    <b-col>
                                        <label>Supplier</label>
                                        {{item.supplier}}
                                    </b-col>
                                    <b-col>
                                        <label>Description</label>
                                        {{item.description}}
                                    </b-col>
                                    <b-col>
                                        <label>BPO Number</label>
                                        {{item.bpoNumber}}
                                    </b-col>
                                    <b-col>
                                        <label>Date Complete</label>
                                        {{item.dateComplete | dateFilter}}
                                    </b-col>
                                    <b-col>
                                        <label>Number of storage days</label>
                                        {{item.numberOfStorageDays}}
                                    </b-col>
                                    <b-col>
                                        <label>Status</label>
                                        {{item.isComplete ? "Complete" : 'Incomplete'}}
                                    </b-col>
                                </b-row>
                                <hr class="mx-3" />
                            </div>
                        </div>
                    </b-tab>
                </b-tabs>

                <b-row>
                    <b-col>
                        <div class="d-flex justify-content-end">
                            <div>
                                <b-button variant="outline-red" squared @click="hideContainerContentModal" class="ml-2">Cancel</b-button>
                            </div>
                            <div>
                                <b-button variant="primary" squared @click="containerUpdate" class="ml-2">Update</b-button>
                            </div>
                        </div>
                    </b-col>
                </b-row>
            </b-modal>
        </div>

    </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import Loader from "@/components/loader";

export default {
    components: {Loader},
    data: () => ({
        name: "containerView",
        containerData: {
            containerNumber: null,
            containerType: null,
            billOfLading: null,
            fileReference: null,
            vesel: null,
            voyage: null,
            loadingPort: null,
            destinationPort: null,
            isActive: null,
            
        },
        availableContainers: [],
        options: [
            {value: null, text: "Please select a container type"},
            {value: '20ft GP', text: '20ft GP'},
            {value: '20ft FR IG', text: '20ft FR IG'},
            {value: '20ft FR OOG', text: '20ft FR OOG'},
            {value: '20ft OT IG', text: '20ft OT IG'},
            {value: '20ft OT OOG', text: '20ft OT OOG'},
            {value: '40ft GP', text: '40ft GP'},
            {value: '40ft HQ', text: '40ft HQ'},
            {value: '40ft FR IG', text: '40ft FR IG'},
            {value: '40ft FR OOG', text: '40ft FR OOG'},
            {value: '40ft OT IG', text: '40ft OT IG'},
            {value: '40ft OT OOG', text: '40ft OT OOG'},
        ],
        loading: true,
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
        ...mapActions(["createContainer", "requestContainer", "updateContainer"]),

        openCompleteContainers() {
            this.$router.push({name: 'completeContainer'})
        },
        
        openContainerModal() {
            this.$bvModal.show('containerAddModal')
        },
        hideContainerModal() {
            this.$bvModal.hide('containerAddModal')
        },
        save() {
            this.containerData.status = "Available"
            this.containerData.isActive = true
            this.$store.commit('setCreateContainerRequest', this.containerData)
            this.createContainer()
            .then(() => {
                this.hideContainerModal()
                this.getContainers()
                this.containerData.containerNumber = null
                this.containerData.containerType = null
                this.containerData.billOfLading = null
                this.containerData.fileReference = null
                this.containerData.vesel = null
                this.containerData.voyage = null
                this.containerData.port = null
                this.containerData.isActive = null
            })
            
        },
        getContainers() {
            this.requestContainer()
            .then((response) => {
                this.availableContainers = response.data
                this.loading = false
                console.log("CONTAINER", response.data)
                console.log("CONTAINER STATUS", response.data.status)
            })
            .catch((err) => {
                this.$router.push({path: '/'})
                localStorage.removeItem('jwt')
                localStorage.removeItem('user')
                console.log('ERROR', err)
            })
        },
        openContainerContentModal(item) {
            if (item) {
                this.$store.commit('setSelectedContainer', item)
                this.$bvModal.show('containerContentModal')
                console.log("SELECTED CONTAINER", item)
            }
        },
        hideContainerContentModal() {
            this.$bvModal.hide('containerContentModal')
        },
        containerUpdate() {
            this.$store.commit('setSelectedContainer', this.selectedContainer)
            this.updateContainer()
                .then(() => {
                    this.hideContainerContentModal()
                    console.log("UPDATED CONTAINER", this.selectedContainer)
                    this.getContainers()
                })
        },
    },
    computed: {
        ...mapState([
            "selectedContainer"
        ]),
    },
}
</script>

<style scoped>
.containerItem {
    background: lightgray;
    margin: 1rem;
    width: 10rem;
    height: 10rem;
    cursor: pointer;
    position: relative;
    z-index: 100;
}

.containerItem div {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    text-align: center;
    width: 100%;
    
}

.containerItem div > label {
    color: white;
}

.contentCount {
    position: relative;
    height: 10rem;
    width: 10rem;
    cursor: pointer;
    
}

.contentCount .content {
    position: absolute;
    top: 2rem;
    left: 4rem;
    text-align: right;
    cursor: pointer;
}

.containerItem.Loading {
    background: yellow;
}
</style>