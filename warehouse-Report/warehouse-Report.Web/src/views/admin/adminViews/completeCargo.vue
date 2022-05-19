<template>
    <div>
        <Loader v-if="loading"/>
        <div v-if="!loading">
            <b-row>
                <b-col>
                    <b-card>
                        <b-row>
                            <b-col>
                                <b-row>
                                    <b-col>
                                        <h4 class="m-0">Complete Shipments</h4>
                                    </b-col>
                                    <b-col>
                                        <b-form-input v-model="search" placeholder="Search Supplier || BPO Number || Container Number"/>
                                    </b-col>
                                    <b-col>
                                        <b-col class="text-right">
                                            <b-button variant="outline-primary" size="sm" squared @click="goBack">
                                                Back
                                            </b-button>
                                            <b-button class="ml-3" variant="outline-primary" size="sm" squared >
                                                <download-excel
                                                    :data="filterSearch"
                                                    worksheet="Complete Containers"
                                                    :name='this.fileName'
                                                    :before-finish= "finishDownload"
                                                    :before-generate="startDownload"
                                                    :export-fields="{
                                                    'Supplier': 'supplier',
                                                    'BPO Number': 'bpoNumber',
                                                    'Total Chargeable Weight': 'totalChargeableWeight',
                                                    'Number Of Storage Days': 'numberOfStorageDays',
                                                    'Container Number' : 'container.containerNumber',
                                                }"
                                                >
                                                    <font-awesome-icon class="mr-1" icon="fa-file-export" />
                                                    Export Excel

                                                </download-excel>
                                            </b-button>
                                        </b-col>
                                    </b-col>
                                </b-row>
                            </b-col>
                        </b-row>

                        <b-row >
                            <b-col>
                                <b-table
                                    sort-icon-left
                                    striped hover
                                    :items="filterSearch"
                                    :fields="cargoCompleteTable.tableColumns"
                                    :busy="cargoCompleteTable.isLoading"
                                    :per-page="cargoCompleteTable.resultsPerPage"
                                    :total-rows="cargoRows"
                                    @row-clicked="openCargoEntry"
                                    :tbody-tr-class="rowClass"
                                    :current-page="cargoCompleteTable.currentPage"
                                    id="cargoTable"
                                >

                                    <template #table-busy>
                                        <div class="text-center my-2">
                                            <b-spinner style="width: 3rem; height: 3rem;"></b-spinner>
                                        </div>
                                    </template>

                                    <template #cell(dateCollected)="data">
                                        <b-row align-v="center">
                                            <span class="mr-auto">{{data.item.dateCollected | dateFilter}}</span>
                                        </b-row>
                                    </template>

                                    <template #cell(endDateOfFreeStorage)="data">
                                        <b-row align-v="center">
                                            <span class="mr-auto">{{data.item.endDateOfFreeStorage | dateFilter}}</span>
                                        </b-row>
                                    </template>

                                    <template #cell(quantitySum)="data">
                                        <b-row align-v="center">
                                            <span class="mr-auto">{{data.item.packageModels.filter(active => active.isActive === true).reduce((acc, qty) => acc + qty.quantity, 0)}}</span>
                                        </b-row>
                                    </template>

                                    <template #cell(icons)="data">
                                        <b-row align-v="center">
                                        <span v-if="data.item.hazardous" class="mr-1">
                                            <font-awesome-icon class="text-danger" icon="fa-biohazard" />
                                        </span>
                                            <span v-if="data.item.packingListReceived" class="mr-1">
                                            <font-awesome-icon icon="fa-box" />
                                        </span>
                                            <span v-if="data.item.commercialInvoiceReceived" class="mr-1">
                                            <font-awesome-icon icon="fa-file-invoice-dollar" />
                                        </span>
                                            <span v-if="data.item.billedToJkn" class="mr-1">
                                            <font-awesome-icon icon="fa-dollar-sign" />
                                        </span>
                                        </b-row>
                                    </template>

                                    <template #cell(weightSum)="data">
                                        <b-row align-v="center">
                                            <span class="mr-auto">{{data.item.packageModels.filter(active => active.isActive === true).reduce((acc, weight) => acc + weight.weight, 0)}} Kg</span>
                                        </b-row>
                                    </template>

                                    <template #cell(kgCBMConversionSum)="data">
                                        <b-row align-v="center" >
                                            <span class="mr-auto">{{(data.item.packageModels.filter(active => active.isActive === true).reduce((acc, cbm) => acc + cbm.kgCBMConversion, 0)).toFixed(3)}}</span>
                                        </b-row>
                                    </template>

                                    <template #cell(volumeSum)="data">
                                        <b-row align-v="center">
                                            <span class="mr-auto">{{ (data.item.packageModels.filter(active => active.isActive === true).reduce((acc, vol) => acc + vol.volumeMetric, 0)).toFixed(3)}}</span>
                                        </b-row>
                                    </template>

                                    <template #cell(chargeableWeightSum)="data">
                                        <b-row align-v="center">
                                            <span class="mr-auto">{{(data.item.packageModels.filter(active => active.isActive === true).reduce((acc, Charge) => acc + Charge.chargeableWeight, 0)).toFixed(3)}}</span>
                                        </b-row>
                                    </template>

                                    <template #cell(storageDaysCalc)="data">
                                        <b-row align-v="center">
                                        <span v-if=" Math.round(((new Date()).getTime() - (new Date(data.item.endDateOfFreeStorage)).getTime()) / (1000 * 60 * 60 * 24)) + 1 >= 0" class="mr-auto">
                                            {{
                                                Math.round(((new Date()).getTime() - (new Date(data.item.endDateOfFreeStorage)).getTime()) / (1000 * 60 * 60 * 24))
                                            }}
                                        </span>
                                            <span v-if=" Math.round(((new Date()).getTime() - (new Date(data.item.endDateOfFreeStorage)).getTime()) / (1000 * 60 * 60 * 24)) + 1 < 0" class="mr-auto">
                                            0
                                        </span>
                                        </b-row>
                                    </template>

                                    <template #cell(storageCostsCalc)>
                                        <b-row align-v="center">
                                        <span class="mr-auto">
                                            
                                        </span>
                                        </b-row>
                                    </template>

                                    <template #cell(actions)="data">
                                        <b-row align-v="center" align-h="end">
                                            <b-button @click="openCargoEntry(data.item, data.index)" size="sm" class="btn-icon">
                                                <b-icon-chevron-right></b-icon-chevron-right>
                                            </b-button>
                                        </b-row>
                                    </template>

                                </b-table>

                                <b-row align-h="center" >
                                    <b-pagination
                                        v-model="cargoCompleteTable.currentPage"
                                        :total-rows="cargoRows"
                                        :per-page="cargoCompleteTable.resultsPerPage"
                                        aria-controls="cargoTable"
                                    ></b-pagination>
                                </b-row>

                            </b-col>
                        </b-row>

                    </b-card>
                </b-col>
            </b-row>
            <div v-if="selectedCompleteCargo">
                <b-modal id="cargoEdit" size="lg" hide-footer hide-header class="text-center" title="Edit Cargo Entry">
                    <div v-if="!isRestore">
                        <b-row>
                            <b-col>
                                <label>Supplier</label>
                                {{selectedCompleteCargo.supplier}}
                            </b-col>
                            <b-col>
                                <label>Description</label>
                                {{selectedCompleteCargo.description}}
                            </b-col>
                            <b-col>
                                <label>BPO Number</label>
                                {{selectedCompleteCargo.bpoNumber}}
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="4">
                                <label>Atrax Invoice Date</label>
                                {{selectedCompleteCargo.atraxInvoiceDate | dateFilter}}
                            </b-col>
                            <b-col cols="4">
                                <label>Atrax Invoice Number</label>
                                {{selectedCompleteCargo.atraxInvoiceNumber}}
                            </b-col>
                        </b-row>
                        <hr class="mx-3 mt-5">
                        <b-row>
                            <b-col>
                                <h4>Container Details</h4>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Bill of Lading</label>
                                {{selectedCompleteCargo.container.billOfLading}}
                            </b-col>
                            <b-col>
                                <label>Container Number</label>
                                {{selectedCompleteCargo.container.containerNumber}}
                            </b-col>
                            <b-col>
                                <label>Container Type</label>
                                {{selectedCompleteCargo.container.containerType}}
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>File Reference</label>
                                {{selectedCompleteCargo.container.fileReference}}
                            </b-col>
                            <b-col>
                                <label>Vessel</label>
                                {{selectedCompleteCargo.container.vesel}}
                            </b-col>
                            <b-col>
                                <label>Voyage</label>
                                {{selectedCompleteCargo.container.voyage}}
                            </b-col>
                        </b-row>
                        <hr class="mx-3 mt-5">
                        <b-row>
                            <b-col>
                                <h4>Transporter Details</h4>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Transporter Invoice Number</label>
                                {{selectedCompleteCargo.transporterInvoiceNumber}}
                            </b-col>
                            <b-col>
                                <label>Transporter Invoice Date</label>
                                {{selectedCompleteCargo.transporterInvoiceDate | dateFilter}}
                            </b-col>
                            <b-col>
                                <label>Transporter Cost</label>
                                ZAR {{selectedCompleteCargo.transporterCost}}
                            </b-col>
                        </b-row>
                        <hr class="mx-3 mt-5">
                        <b-row>
                            <b-col>
                                <h4>Cargo Details</h4>
                            </b-col>
                        </b-row>
                        <div v-for="(item, index) in selectedCompleteCargo.packageModels" :key="index">
                            <b-row>
                                <b-col>
                                    <label>Cargo Description</label>
                                    {{item.description}}
                                </b-col>
                                <b-col></b-col>
                                <b-col></b-col>
                            </b-row>
                            <b-row>
                                <b-col>
                                    <label>Length</label>
                                    {{item.length}}
                                </b-col>
                                <b-col>
                                    <label>Width</label>
                                    {{item.width}}
                                </b-col>
                                <b-col>
                                    <label>Height</label>
                                    {{item.height}}
                                </b-col>
                            </b-row>
                        </div>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-end">
                                    <div>
                                        <b-button variant="outline-red" squared @click="hideCargoEntry" class="ml-2">Cancel</b-button>
                                    </div>
                                    <div>
                                        <b-button variant="primary" squared @click="restore" class="ml-2">Restore</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </div>
                    <div v-if="isRestore">
                        <b-row>
                            <b-col>
                                <h4>Are you sure you want to reactivate this shipment</h4>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-end">
                                    <div>
                                        <b-button variant="outline-red" squared @click="restore" class="ml-2">No</b-button>
                                    </div>
                                    <div>
                                        <b-button variant="primary" squared class="ml-2" @click="reactivate">restore</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </div>
                </b-modal>
            </div>
        </div>
        
    </div>
</template>

<script>
import {mapActions, mapState} from "vuex";
import Loader from "@/components/loader";

export default {
    name: "completedCargo",
    components: {Loader},
    data: () => ({
        cargoCompleteTable : {
            resultsPerPage: 10,
            currentPage: 1,
            dataSource: [],
            isLoading: true,
            tableColumns: [
                {
                    label: 'Supplier',
                    key: 'supplier',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Date Of Collection',
                    key: 'dateCollected',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Date Charge Start',
                    key: 'endDateOfFreeStorage',
                    sortable: true,
                    tdClass:'',
                },
                // {
                //     label: 'Cargo Ready Place',
                //     key: 'cargoReadyPlace',
                //     sortable: false,
                //     tdClass:'',
                // },
                {
                    label: 'BPO Number',
                    key: 'bpoNumber',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Container',
                    key: 'container.containerNumber',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Invoice Number',
                    key: 'atraxInvoiceNumber',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Quantity',
                    key: 'quantitySum',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: '',
                    key: 'icons',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Weight (KG)',
                    key: 'weightSum',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'KG - CBM Conv.',
                    key: 'kgCBMConversionSum',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Volume',
                    key: 'volumeSum',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Chargeable Weight',
                    key: 'chargeableWeightSum',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Storage Days',
                    key: 'storageDaysCalc',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: 'Storage Cost',
                    key: 'storageCostsCalc',
                    sortable: false,
                    tdClass:'',
                },
                {
                    label: '',
                    key: 'actions',
                    sortable: false,
                    tdClass: ''
                },
            ]
        },
        isRestore: false,
        search: '',
        fileName: `Complete Cargo ${new Date().toLocaleDateString('en-ZA')}.xls`,
        loading: false,
    }),
    beforeCreate() {
    },
    created() {
        this.completeCargo()
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
        ...mapActions(["requestCompleteCargo", "restoreCargo"]),
        
        goBack() {
            this.$router.push({name: 'adminView'})
        },
        
        completeCargo () {
            this.cargoCompleteTable.isLoading = true
            this.requestCompleteCargo()
                .then((response) => {
                    this.cargoCompleteTable.dataSource = response.data
                    this.cargoCompleteTable.isLoading = false
                    console.log('COMPLETE ', this.cargoCompleteTable.dataSource)
                })
        },
        rowClass(item, type) {
            if (!item || type !== 'row') return
            if (item.hazardous === true) return 'table-danger'
        },
        openCargoEntry(rowItem) {
            this.$bvModal.show('cargoEdit')
            this.$store.commit('setSelectedCompleteCargo', rowItem)
            console.log("CARGO COMPLETE", rowItem)
        },
        hideCargoEntry() {
            this.$bvModal.hide('cargoEdit')
        },
        restore() {
            this.isRestore = !this.isRestore
        },

        reactivate() {
            this.selectedCompleteCargo.isComplete = false
            this.$store.commit('setSelectedCompleteCargo', this.selectedCompleteCargo)
                this.restoreCargo()
                    .then(() => {
                        this.$router.push({path: '/admin-home'})
                    })
        },

        startDownload() {
            this.loading = true
        },

        finishDownload(){
            this.loading = false
        }
    },
    computed: {
        ...mapState([
            'selectedCompleteCargo'
        ]),
        cargoRows() {
            return this.cargoCompleteTable.dataSource.length
        },
        filterSearch() {
            return this.cargoCompleteTable.dataSource.filter((item) => {
                return item.supplier.toLowerCase().match(this.search.toLowerCase()) || 
                    item.bpoNumber.toLowerCase().match(this.search.toLowerCase()) || 
                    item.container.containerNumber.toLowerCase().match(this.search.toLowerCase())
            })
        },
    },
}
</script>

<style scoped>
label {
    font-weight: bold;
}
</style>