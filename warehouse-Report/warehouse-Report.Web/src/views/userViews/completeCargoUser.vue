<template>
    <div>
        <b-row >
            <b-col>
                <b-card>
                    <b-row>
                        <b-col>
                            <b-row>
                                <b-col>
                                    <h4 class="m-0">Complete Shipments</h4>
                                </b-col>
                                <b-col>
                                    <b-form-input v-model="search" placeholder="Search Supplier || BPO Number"/>
                                </b-col>
                                <b-col>
                                    <b-col class="text-right">
                                        <vue-excel-xlsx
                                            class="button-style ml-3"
                                            :data="filterSearch"
                                            :sheet-name="'Complete Containers'"
                                            :file-name='this.fileName'
                                            :file-type="'xlsx'"
                                            :columns="[
                                                        {
                                                            label: 'Supplier',
                                                            field: 'supplier'
                                                        },
                                                        {
                                                            label: 'BPO Number',
                                                            field: 'bpoNumber'
                                                        },
                                                        {
                                                            label: 'Total Chargeable Weight',
                                                            field: 'totalChargeableWeight'
                                                        },
                                                        {
                                                            label: 'Number Of Storage Days',
                                                            field: 'numberOfStorageDays'
                                                        },
                                                        {
                                                            label: 'Container Number',
                                                            field: 'container.containerNumber'
                                                        },
                                                    ]"
                                        >
                                            <font-awesome-icon class="mr-1" icon="fa-file-export" />
                                            Export Excel

                                        </vue-excel-xlsx>
                                        <b-button class="ml-3" variant="outline-primary" size="sm" squared @click="goToBack">
                                            <font-awesome-icon icon="fa-people-carry" class="mr-1" />
                                            Current Shipments
                                        </b-button>
                                    </b-col>
                                </b-col>
                            </b-row>


                        </b-col>
                    </b-row>
                    <b-row class="mt-3">
                        <b-col>
                            <b-table
                                sort-icon-left
                                striped hover
                                :items="filterSearch"
                                :fields="cargoTable.tableColumns"
                                :busy="cargoTable.isLoading"
                                :per-page="cargoTable.resultsPerPage"
                                :total-rows="cargoRows"
                                @row-clicked="openCargoModal"
                                :tbody-tr-class="rowClass"
                                :current-page="cargoTable.currentPage"
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

<!--                                <template #cell(icons)="data">-->
<!--                                    <b-row align-v="center">-->
<!--                                        <span v-if="data.item.hazardous" class="mr-1">-->
<!--                                            <font-awesome-icon class="text-danger" icon="fa-biohazard" />-->
<!--                                        </span>-->
<!--                                        <span v-if="data.item.packingListReceived" class="mr-1">-->
<!--                                            <font-awesome-icon icon="fa-box" />-->
<!--                                        </span>-->
<!--                                        <span v-if="data.item.commercialInvoiceReceived" class="mr-1">-->
<!--                                            <font-awesome-icon icon="fa-file-invoice-dollar" />-->
<!--                                        </span>-->
<!--                                        <span v-if="data.item.billedToJkn" class="mr-1">-->
<!--                                            <font-awesome-icon icon="fa-dollar-sign" />-->
<!--                                        </span>-->
<!--                                    </b-row>-->
<!--                                </template>-->

                                <template #cell(weightSum)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{data.item.packageModels.filter(active => active.isActive === true).reduce((acc, weight) => acc + weight.weight, 0)}} Kg</span>
                                    </b-row>
                                </template>

                                <template #cell(kgCBMConversionSum)="data">
                                    <b-row align-v="center" >
                                        <span class="mr-auto">
                                        </span>
                                        <span class="mr-auto">
                                            {{(data.item.packageModels.filter(active => active.isActive === true).reduce((acc, cbm) => acc + cbm.kgCBMConversion, 0)).toFixed(3)}}
                                        </span>
                                    </b-row>
                                </template>

                                <template #cell(volumeSum)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{ (data.item.packageModels.filter(active => active.isActive === true).reduce((acc, vol) => acc + vol.volumeCbm, 0)).toFixed(3)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(chargeableWeightSum)="data">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{(data.item.packageModels.filter(active => active.isActive === true).reduce((acc, Charge) => acc + Charge.chargeableWeight, 0)).toFixed(3)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(storageDaysCalc)="data">
                                    <b-row align-v="center">
                                        {{data.item.numberOfStorageDays}}
                                    </b-row>
                                </template>

                                <template #cell(storageCostsCalc)="data">
                                    <b-row align-v="center">
                                        USD
                                        {{Math.round((data.item.storageCost + Number.EPSILON) * 100) / 100}}
                                    </b-row>
                                </template>

                                <template #cell(actions)="data">
                                    <b-row align-v="center" align-h="end">
                                        <b-button @click="openCargoModal(data.item, data.index)" size="sm" class="btn-icon">
                                            <b-icon-chevron-right></b-icon-chevron-right>
                                        </b-button>
                                    </b-row>
                                </template>

                            </b-table>

                            <b-row align-h="center" >
                                <b-pagination
                                    v-model="cargoTable.currentPage"
                                    :total-rows="cargoRows"
                                    :per-page="cargoTable.resultsPerPage"
                                    aria-controls="cargoTable"
                                ></b-pagination>
                            </b-row>

                        </b-col>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
        <div v-if="selectedCargo">
            <b-modal id="cargo" size="lg" hide-footer hide-header class="text-center" title="Edit Cargo Entry" >
                <b-row>
                    <b-col>
                        <b-tabs fill>
                            <b-tab title="Cargo Overview" title-item-class="tabItem">
                                <b-row>
                                    <b-col cols="4">
                                        <label><span class="font-weight-bold">Description:</span> </label>
                                        <label>{{selectedCargo.description}} </label>
                                    </b-col>
                                    <b-col>
                                        <label><span class="font-weight-bold">Special Requirements:</span> </label>
                                        <label>{{selectedCargo.specialRequirements}}</label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col>
                                        <label><span class="font-weight-bold">Supplier:</span> </label>
                                        <label>{{selectedCargo.supplier}}</label>
                                    </b-col>

                                    <b-col>
                                        <label><span class="font-weight-bold">BPO Number:</span> </label>
                                        <label>{{selectedCargo.bpoNumber}}</label>
                                    </b-col>

                                    <b-col>
                                        <label><span class="font-weight-bold">Commercial Invoice #:</span> </label>
                                        <label>{{selectedCargo.commercialInvoiceNumber}}</label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col>
                                        <label><span class="font-weight-bold">Date Collected:</span> </label>
                                        <label>{{selectedCargo.dateCollected | dateFilter}}</label>
                                    </b-col>
                                    <b-col>
                                        <label><span class="font-weight-bold">Date Received:</span> </label>
                                        <label>{{selectedCargo.dateReceived | dateFilter}}</label>
                                    </b-col>
                                    <b-col>
                                        <label><span class="font-weight-bold">Date of charge</span></label>
                                        <label>{{selectedCargo.endDateOfFreeStorage | dateFilter}}</label>
                                    </b-col>
                                </b-row>
                                <b-row>

                                    <b-col cols="4">
                                        <label><span class="font-weight-bold">Number of storage days</span></label>
                                        <label v-if="selectedCargo.numberOfStorageDays === 0">
                                            {{this.storageDays >= 0 ? this.storageDays : "No storage days"}}
                                            <font-awesome-icon icon="fa-tower-broadcast"  class="ml-2 blob"/>
                                        </label>
                                        <label v-if="selectedCargo.numberOfStorageDays > 0">
                                            {{selectedCargo.numberOfStorageDays}}
                                            <font-awesome-icon icon="fa-lock" class="ml-2 text-success" />
                                        </label>
                                    </b-col>
                                    <b-col cols="4">
                                        <label><span class="font-weight-bold">Storage cost</span> </label>
                                        <label v-if="selectedCargo.storageCost === 0">
                                            <span v-if="storageCost > 0"> USD</span>
                                            {{this.storageCost > 0 ? this.storageCost : "No Cost"}}
                                            <font-awesome-icon icon="fa-tower-broadcast"  class="ml-2 blob"/>
                                        </label>
                                        <label v-if="selectedCargo.storageCost > 0">
                                            <span v-if="storageCost >= 0"> USD</span>
                                            {{selectedCargo.storageCost ? selectedCargo.storageCost.toFixed(2) : ''}}
                                            <font-awesome-icon icon="fa-lock" class="ml-2 text-success" />
                                        </label>
                                    </b-col>
                                </b-row>
                                <b-row>

                                    <b-col>
                                        <label class="font-weight-bold">Total Quantity</label>
                                        <label>{{this.quantity}}</label>
                                    </b-col>
                                    <b-col>
                                        <label class="font-weight-bold">Total Weight</label>
                                        <label>{{this.totalWeight}} Kg</label>
                                    </b-col>
                                    <b-col>
                                        <label class="font-weight-bold">Total Chargeable Weight</label>
                                        <label>{{this.chargeWeight ? this.chargeWeight.toFixed(3) : ''}}</label>
                                    </b-col>
                                </b-row>
                                <hr class="mx-3">
                                <b-row>
                                    <b-col class="text-center">
                                        <label>
                                            Hazardous?
                                            <span>
                                                ( <font-awesome-icon class="text-danger" icon="fa-biohazard" /> )
                                            </span>
                                        </label>
                                        <label class="label-good" v-if="selectedCargo.hazardous"><font-awesome-icon icon="fa-check" /></label>
                                        <label class="label-bad" v-if="!selectedCargo.hazardous"><font-awesome-icon icon="fa-times" /></label>
                                    </b-col>
                                    <b-col class="text-center">
                                        <label>
                                            Commercial Invoice Received?
                                            <span>
                                                ( <font-awesome-icon icon="fa-file-invoice-dollar" /> )
                                            </span>
                                        </label>
                                        <label class="label-good" v-if="selectedCargo.commercialInvoiceReceived">
                                            <font-awesome-icon icon="fa-check" />
                                        </label>
                                        <label class="label-bad" v-if="!selectedCargo.commercialInvoiceReceived">
                                            <font-awesome-icon icon="fa-times" />
                                        </label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col class="text-center">
                                        <label>
                                            Packing List Received?
                                            <span>
                                                ( <font-awesome-icon icon="fa-box" /> )
                                            </span>
                                        </label>
                                        <label class="label-good" v-if="selectedCargo.packingListReceived">
                                            <font-awesome-icon icon="fa-check" />
                                        </label>
                                        <label class="label-bad" v-if="!selectedCargo.packingListReceived">
                                            <font-awesome-icon icon="fa-times" />
                                        </label>
                                    </b-col>
                                    <b-col class="text-center">
                                        <label>
                                            Billed To JKN?
                                            <span>
                                                ( <font-awesome-icon icon="fa-dollar-sign" /> )
                                            </span></label>
                                        <label class="label-good" v-if="selectedCargo.billedToJkn">
                                            <font-awesome-icon icon="fa-check" />
                                        </label>
                                        <label class="label-bad" v-if="!selectedCargo.billedToJkn">
                                            <font-awesome-icon icon="fa-times" />
                                        </label>
                                    </b-col>
                                </b-row>
                                <b-row>
                                    <b-col>
                                        <label>Atrax Invoice Number</label>
                                        <label>{{selectedCargo.atraxInvoiceNumber}}</label>
                                    </b-col>
                                    <b-col>
                                        <label>Atrax Invoice Date</label>
                                        <label>{{selectedCargo.atraxInvoiceDate | dateFilter}}</label>
                                    </b-col>
                                </b-row>
                            </b-tab >
                            <b-tab title="Package Details" title-item-class="tabItem">
                                <div>
                                    <div class="mt-2">
                                        <div v-for="item in selectedCargo.packageModels" :key="item.index" class="w-100 my-2">
                                            <div v-if="item.isActive" >
                                                <b-row >
                                                    <b-col>
                                                        <div class="d-flex justify-content-between align-items-center">
                                                            <label class="text-primary font-weight-bold mb-4 text-uppercase">{{item.description}}</label>

                                                        </div>
                                                    </b-col>
                                                </b-row>
                                                <b-row class="m-0 colStyle">
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Quantity:</label>
                                                        <h3 class="text-center">{{item.quantity}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Weight Total (kg):</label>
                                                        <h3 class="text-center">{{item.weight}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Volume CBM:</label>
                                                        <h3 class="text-center">{{item.volumeCbm ? item.volumeCbm.toFixed(3) : ''}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Chargeable Weight:</label>
                                                        <h3 class="text-center">{{item.chargeableWeight ? item.chargeableWeight.toFixed(3) : ''}}</h3>
                                                    </b-col>

                                                </b-row>
                                                <b-row class="m-0 colStyle">
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Length: (cm)</label>
                                                        <h3 class="text-center">{{item.length}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Width: (cm)</label>
                                                        <h3 class="text-center">{{item.width}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">Height: (cm)</label>
                                                        <h3 class="text-center">{{item.height}}</h3>
                                                    </b-col>
                                                    <b-col>
                                                        <label class="text-center font-weight-bold">CBM Conv.:</label>
                                                        <h3 class="text-center">{{item.kgCBMConversion ? item.kgCBMConversion.toFixed(3) : ''}}</h3>
                                                    </b-col>
                                                </b-row>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </b-tab>
                        </b-tabs>
                    </b-col>
                </b-row>
                <hr class="mx-3">
                <b-row>
                    <b-col>
                        <div class="d-flex justify-content-end">
                            <div>
                                <b-button variant="outline-red" squared class="ml-2" @click="closeCargoModal">Cancel</b-button>
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

export default {
    name: 'completeCargoUser',
    data: () => ({
        cargoTable: {
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
                    label: 'Atrax Invoice',
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
                    label: 'Volume (CBM)',
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
        packageData: {
            description: null,
            quantity: null,
            weight: null,
            length: null,
            width: null,
            height: null,
            cargoModelCargoId: null,
        },
        storageDays: null,
        storageCost: null,
        chargeWeight: null,
        quantity: null,
        totalWeight: null,
        search: '',
        fileName: `Complete Cargo ${new Date().toLocaleDateString('en-ZA')}`,
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
        ...mapActions(["requestCompleteCargo"]),

        openCargoModal(rowItem) {
            this.$bvModal.show('cargo')
            this.$store.commit('setSelectedCargo', rowItem)
        },
        closeCargoModal() {
            this.$bvModal.hide('cargo')
        },
        rowClass(item, type) {
            if (!item || type !== 'row') return
            if (item.hazardous === true) return 'table-danger'
        },
        goToBack() {
            this.$router.push({name: 'userView'})
        },
        completeCargo() {
            this.cargoTable.isLoading = true
            this.requestCompleteCargo()
                .then((response) => {
                    this.cargoTable.dataSource = response.data
                    this.cargoTable.isLoading = false
                    console.log('COMPLETE', response.data)
                })
        },
    },
    computed: {
        ...mapState([
            "selectedCargo"
        ]),
        cargoRows() {
            return this.cargoTable.dataSource.length
        },
        filterSearch() {
            return this.cargoTable.dataSource.filter((item) => {
                return item.supplier.toLowerCase().match(this.search.toLowerCase())
                    || item.bpoNumber.toLowerCase().match(this.search.toLowerCase())
            })
        },
    },
}
</script>

<style scoped lang="scss">
.label-good {
    font-size: 1.5rem;
    color: green;
}
.label-bad {
    font-size: 1.5rem;
    color: red;
}
.button-style {
    padding: 0.25rem 0.5rem;
    font-size: 0.875rem;
    line-height: 1.5;
    display: inline-block;
    font-weight: 400;
    background-color: transparent;
    border: 1px solid black;

&:hover {
     color: #ffffff;
     background-color: #18224C;
     border-color: #18224C;
 }
}
</style>