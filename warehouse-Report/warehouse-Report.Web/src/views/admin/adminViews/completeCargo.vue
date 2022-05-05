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
                                <b-button variant="outline-primary" size="sm" squared @click="goBack">
                                    Back
                                </b-button>
                            </b-col>
                        </b-col>
                    </b-row>
                    <b-row >
                        <b-col>
                            <b-table
                                sort-icon-left
                                striped hover
                                :items="cargoCompleteTable.dataSource"
                                :fields="cargoCompleteTable.tableColumns"
                                :busy="cargoCompleteTable.isLoading"
                                :per-page="cargoCompleteTable.resultsPerPage"
                                :total-rows="cargoRows"
                                :tbody-tr-class="rowClass"
                                @row-clicked="openCargoEntry"
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
            <b-modal id="completeEdit" size="lg" hide-footer hide-header class="text-center" title="Edit Cargo Entry">
                <div v-if="!active">
                    <b-row>
                        <b-col>
                            <label class="font-weight-bold">Supplier</label>
                            <label>{{selectedCompleteCargo.supplier}}</label>
                        </b-col>
                        <b-col>
                            <label class="font-weight-bold" l>Description</label>
                            <label>{{selectedCompleteCargo.description}}</label>
                        </b-col>
                        <b-col>
                            <label class="font-weight-bold">BPO Number</label>
                            <label>{{selectedCompleteCargo.bpoNumber}}</label>
                        </b-col>
                    </b-row>
                    <hr class="mx-5 mt-5" />
                    <b-row >
                        <b-col>
                            <h4>Shipment</h4>
                        </b-col>
                    </b-row>
                    <b-row >
                        <b-col>
                            <label class="font-weight-bold">Packed Date</label>
                            <label>{{selectedCompleteCargo.packDate | dateFilter}}</label>
                        </b-col>
                        <b-col>
                            <label class="font-weight-bold">Storage Days</label>
                            <label>{{selectedCompleteCargo.numberOfStorageDays}}</label>
                        </b-col>
                        <b-col>
                            <label class="font-weight-bold">Storage Cost</label>
                            <label>USD {{selectedCompleteCargo.storageCost.toFixed(3)}}</label>
                        </b-col>
                    </b-row>
                    <hr class="mx-5 mt-5" />
                    <b-row >
                        <b-col>
                            <h4>Container</h4>
                        </b-col>
                    </b-row>
                    <b-row >
                        <b-col>
                            <label class="font-weight-bold">Container Number</label>
                            <label>{{selectedCompleteCargo.container.containerNumber}}</label>
                        </b-col>
                        <b-col>
                            <label class="font-weight-bold">Vessel</label>
                            <label>{{selectedCompleteCargo.container.vesel}}</label>
                        </b-col>
                        <b-col>
                            <label class="font-weight-bold">Voyage</label>
                            <label>{{selectedCompleteCargo.container.voyage}}</label>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col>
                            <label class="font-weight-bold">Bill of Lading</label>
                            <label>{{selectedCompleteCargo.container.billOfLading}}</label>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col class="text-right">
                            <b-button variant="outline-red" size="sm" squared @click="hideCargoEntry">
                                Cancel
                            </b-button>
                            <b-button class="ml-3" variant="outline-primary" size="sm" squared @click="restore">
                                Restore
                            </b-button>
                        </b-col>
                    </b-row>
                </div>
                <div v-if="active">
                    <b-row>
                        <b-col>
                            <h4>Are you sure you want to reactivate this shipment?</h4>
                        </b-col>
                    </b-row>
                    <b-row>
                        <b-col class="text-right">
                            <b-button variant="outline-red" size="sm" squared @click="restore">
                                No
                            </b-button>
                            <b-button class="ml-3" variant="primary" size="sm" squared @click="restoreShipment">
                                Restore
                            </b-button>
                        </b-col>
                    </b-row>
                </div>
            </b-modal>
        </div>
    </div>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
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
        active: false,
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
            this.$router.back()
        },
        
        completeCargo () {
            this.cargoCompleteTable.isLoading = true
            this.requestCompleteCargo()
                .then((response) => {
                    this.cargoCompleteTable.dataSource = response.data
                    this.cargoCompleteTable.isLoading = false
                })
        },
        rowClass(item, type) {
            if (!item || type !== 'row') return
            if (item.hazardous === true) return 'table-danger'
        },
        openCargoEntry(rowItem) {
            this.$bvModal.show('completeEdit')
            this.$store.commit('setSelectedCompleteCargo', rowItem)
            console.log('SELECTED CARGO', this.selectedCompleteCargo)
        },
        hideCargoEntry() {
            this.$bvModal.hide('completeEdit')
        },
        restore() {
            this.active = !this.active
        },
        restoreShipment() {
            this.selectedCompleteCargo.isComplete = false
            this.selectedCompleteCargo.key = this.selectedCompleteCargo.value
            this.$store.commit('selectedCompleteCargo', this.selectedCompleteCargo)
                this.restoreCargo()
                .then(() => {
                    this.$router.push({path: '/admin-home'})
                    console.log('RESTORED', this.selectedCompleteCargo)
                })
        },
    },
    computed: {
        ...mapState([
            "selectedCompleteCargo"
        ]),
        cargoRows() {
            return this.cargoCompleteTable.dataSource.length
        },
    },
}
</script>

<style scoped>
</style>