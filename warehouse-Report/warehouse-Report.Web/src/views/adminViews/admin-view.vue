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
                                <b-button variant="outline-primary" size="sm" squared @click="openCargoModal">
                                    <font-awesome-icon icon="fa-plus" class="mr-1" />
                                    Add Entry
                                </b-button>
                            </b-col>
                        </b-col>
                    </b-row>
                    <b-row class="mt-3">
                        <b-col>
                            <b-table
                                striped hover
                                :items="cargoTable.dataSource"
                                :fields="cargoTable.tableColumns"
                                :busy="cargoTable.isLoading"
                                @row-clicked="openCargoEntry"
                                id="vehicleTable"
                                :current-page="cargoTable.currentPage"
                            >
                                <template #table-busy>
                                    <div class="text-center my-2">
                                        <b-spinner style="width: 3rem; height: 3rem;"></b-spinner>
                                    </div>
                                </template>
                                
                                <template #cell(dateCollected)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.dateCollected | dateFilter}}</span>
                                    </b-row>
                                </template>

                                <template #cell(endDateOfFreeStorage)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.endDateOfFreeStorage | dateFilter}}</span>
                                    </b-row>
                                </template>

                                <template #cell(kgCBMConvertion)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.kgCBMConvertion.toFixed(3)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(volume)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.volume.toFixed(3)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(chargeableWeight)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.chargeableWeight.toFixed(3)}}</span>
                                    </b-row>
                                </template>
                                
                                <template #cell(storageCost)="row">
                                    <b-row align-v="center">
                                        <span class="mr-auto">{{row.item.storageCost.toFixed(2)}}</span>
                                    </b-row>
                                </template>

                                <template #cell(actions)="row">
                                    <b-row align-v="center" align-h="end">
                                        <b-button @click="openCargoEntry(row.item)" size="sm" class="btn-icon">
                                            <b-icon-chevron-right></b-icon-chevron-right>
                                        </b-button>
                                    </b-row>
                                </template>
                                
                            </b-table>

                            <b-row align-h="center" >
                                <b-pagination
                                    v-model="cargoTable.currentPage"
                                    :total-rows="rows"
                                    :per-page="cargoTable.resultsPerPage"
                                    aria-controls="contactTable"
                                ></b-pagination>
                            </b-row>
                            
                        </b-col>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
        <b-modal  id="cargoAddModal" size="xl" hide-footer hide-header-close class="text-center" title="Add Cargo">
            <b-row class="align-items-center m-0 ">
                <b-col class="d-flex justify-content-center">
                    <B-form class="w-100">
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-between align-items-center">
                                    <label class="text-primary font-weight-bold mb-4">Personal Details</label>
                                    <b-button variant="outline-red" squared @click="toggleDelete" size="sm">Delete</b-button>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="4">
                                <label>Supplier</label>
                                <b-form-select v-model="cargo.supplier">
                                    <b-form-select-option v-for="(item, index) in suppliers" :key="index" :value="item.companyName">{{item.companyName}}</b-form-select-option>
                                </b-form-select>
                            </b-col>
                            <b-col cols="4">
                                <label>Date Collected</label>
                                <b-form-datepicker v-model="cargo.dateCollected"></b-form-datepicker>
                            </b-col>
                            <b-col cols="4">
                                <label>Order Number</label>
                                <b-form-input v-model="cargo.orderNumber"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="4">
                                <label>Description</label>
                                <b-form-input v-model="cargo.description"></b-form-input>
                            </b-col>
                            <b-col cols="4"></b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <label class="text-primary font-weight-bold mb-4">Cargo Details</label>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Quantity</label>
                                <b-form-input type="number" v-model="cargo.quantity"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Weight (kg)</label>
                                <b-form-input type="number" v-model="cargo.weight"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Length</label>
                                <b-form-input type="number" v-model="cargo.length"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Width</label>
                                <b-form-input type="number" v-model="cargo.width"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Height</label>
                                <b-form-input type="number" v-model="cargo.height"></b-form-input>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-end">
                                    <div>
                                        <b-button variant="outline-red" squared @click="hideCargoModal" class="ml-2">Cancel</b-button>
                                    </div>
                                    <div>
                                        <b-button variant="primary" squared @click="save" class="ml-2">Save</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </B-form>
                </b-col>
            </b-row>
        </b-modal>

        <div v-if="this.selectedCargo">
            <b-modal id="cargoEdit" size="xl" hide-footer hide-header class="text-center" title="Edit Cargo Entry">
            <b-row class="align-items-center m-0 ">
                <b-col class="d-flex justify-content-center">
                    <B-form class="w-100">
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-between align-items-center">
                                    <label class="text-primary font-weight-bold mb-4">Cargo Details</label>
                                    <b-button variant="outline-red" squared @click="toggleDelete" size="sm">Delete</b-button>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label><span class="font-weight-bold">Supplier:</span> </label>
                                <label>{{selectedCargo.supplier ? selectedCargo.supplier : "Supplier not selected"}}</label>
                            </b-col>
                            <b-col>
                                <label><span class="font-weight-bold">Date Collected:</span> </label>
                                <label>{{selectedCargo.dateCollected}}</label>
                            </b-col>
                            <b-col>
                                <label><span class="font-weight-bold">Order Number:</span> </label>
                                <label>{{selectedCargo.orderNumber}}</label>
                            </b-col>
                            <b-col>
                                <label>Invoice Number</label>
                                <b-form-input v-model="selectedCargo.invoiceNumber" />
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="6">
                                <label><span class="font-weight-bold">Description:</span> </label>
                                <label>{{selectedCargo.description}}</label>
                            </b-col>
                            <b-col cols="4"></b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <label class="text-primary font-weight-bold mb-4">Cargo Details</label>
                            </b-col>
                        </b-row>
                        <b-row class="m-0 colStyle">
                            <b-col>
                                <label class="text-center font-weight-bold">Quantity:</label>
                                <h3 class="text-center">{{selectedCargo.quantity}}</h3>
                            </b-col>
                            <b-col>
                                <label class="text-center font-weight-bold">Weight (kg):</label>
                                <h3 class="text-center">{{selectedCargo.weight}}</h3>
                            </b-col>
                            <b-col>
                                <label class="text-center font-weight-bold">Length:</label>
                                <h3 class="text-center">{{selectedCargo.length}}</h3>
                            </b-col>
                            <b-col>
                                <label class="text-center font-weight-bold">Width:</label>
                                <h3 class="text-center">{{selectedCargo.width}}</h3>
                            </b-col>
                            <b-col>
                                <label class="text-center font-weight-bold">Height:</label>
                                <h3 class="text-center">{{selectedCargo.height}}</h3>
                            </b-col>
                            
                        </b-row>
                        <b-row class="m-0 colStyle">
                            <b-col>
                                <label class="text-center font-weight-bold">CBM Conv.:</label>
                                <h3 class="text-center">{{selectedCargo.kgCBMConvertion}}</h3>
                            </b-col>
                            <b-col>
                                <label class="text-center font-weight-bold">Volume:</label>
                                <h3 class="text-center">{{selectedCargo.volume}}</h3>
                            </b-col>
                            <b-col>
                                <label class="text-center font-weight-bold">Storage Days:</label>
                                <h3 class="text-center">{{selectedCargo.numberOfStorageDays}}</h3>
                            </b-col>
                            <b-col>
                                <label class="text-center font-weight-bold">Chargable Weight:</label>
                                <h3 class="text-center">{{selectedCargo.chargeableWeight}}</h3>
                            </b-col>
                            <b-col>
                                <label class="text-center font-weight-bold">Storage Cost:</label>
                                <h3 class="text-center">{{selectedCargo.storageCost.toFixed(2)}}</h3>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-end">
                                    <div>
                                        <b-button variant="outline-red" squared @click="hideCargoEditModal" class="ml-2">Cancel</b-button>
                                    </div>
                                    <div>
                                        <b-button variant="primary" squared @click="save" class="ml-2">Save</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </B-form>
                </b-col>
            </b-row>
        </b-modal>
            
        </div>

    </div>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
    data: () => ({
        cargo: {
            supplier: null,
            dateCollected: null,
            orderNumber: null,
            invoiceNumber: null,
            description: null,
            quantity: null,
            weight: null,
            length: null,
            width: null,
            height: null,
            isActive: true,
        },
        cargoTable: {
            resultsPerPage: 10,
            currentPage: 1,
            dataSource: [],
            isLoading: false,
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
                    label: 'Cargo Ready Place',
                    key: 'cargoReadyPlace',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Order Number',
                    key: 'orderNumber',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Invoice Number',
                    key: 'invoiceNumber',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Description',
                    key: 'description',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Quantity',
                    key: 'quantity',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Weight (KG)',
                    key: 'weight',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'KG - CBM Conv.',
                    key: 'kgCBMConvertion',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Length',
                    key: 'length',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Width',
                    key: 'width',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Height',
                    key: 'height',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Volume',
                    key: 'volume',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Chargeable Weight',
                    key: 'chargeableWeight',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Storage Days',
                    key: 'numberOfStorageDays',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'TEST Storage Days',
                    key: 'test',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Storage Cost',
                    key: 'storageCost',
                    sortable: true,
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
        suppliers: [],
        isDeleteSelected: false,
    }),
    beforeCreate() {
    },
    created() {
    },
    beforeMount() {
    },
    mounted() {
        this.getSupplierList()
        this.getCargoList()
    },
    beforeUpdate() {
    },
    updated() {
    },
    methods: {
        ...mapActions(["requestSupplier", "createCargo", "requestCargo"]),
        openCargoEntry(rowItem) {
            this.$bvModal.show('cargoEdit')
            this.$store.commit('setSelectedCargo', rowItem)
            console.log("SELECTED CARGO", this.selectedCargo)
        },
        hideCargoEditModal() {
            this.$bvModal.hide('cargoEdit')
        },
        openCargoModal() {
            this.$bvModal.show('cargoAddModal')
        },
        hideCargoModal() {
            this.$bvModal.hide('cargoAddModal')
        },
        getSupplierList() {
            this.requestSupplier()
                .then(response => {
                    this.suppliers = response.data
                    console.log('SUPPLIER', this.supplier)
                })
        },
        getCargoList() {
            this.requestCargo()
            .then(response => {
                this.cargoTable.dataSource = response.data
                console.log('CARGO', response.data)
                console.log('CARGO TABLE', this.cargoTable.dataSource)
            })
        },
        save() {
            const newCargo = {}
            newCargo.supplier = this.cargo.supplier
            this.$store.commit('setCreateCargoRequest', this.cargo)
            this.state = 'loading'
            this.createCargo()
                .then(() => {
                    this.$bvModal.hide('cargoAddModal')
                    this.getCargoList()
                })
                .catch(() => {
                })
        },
        toggleDelete() {
            this.isDeleteSelected = !this.isDeleteSelected
        }
    },
    computed: {
        rows() {
            return this.cargoTable.dataSource.length
        },
        ...mapState([
            "selectedCargo"
        ])
    },
}
</script>

<style scoped>
    .colStyle > .col {
        border: 1px solid black;
    }
</style>