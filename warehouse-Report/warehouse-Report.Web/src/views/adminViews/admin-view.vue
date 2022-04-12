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
                                @row-clicked="openCargo"
                                id="vehicleTable"
                                :current-page="cargoTable.currentPage"
                            >
                                <template #table-busy>
                                    <div class="text-center my-2">
                                        <b-spinner style="width: 3rem; height: 3rem;"></b-spinner>
                                    </div>
                                </template>

                                <template #cell(actions)="row">
                                    <b-row align-v="center" align-h="end">
                                        <b-button @click="openCargo(row.item)" size="sm" class="btn-icon">
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
<!--                        <b-row>-->
<!--                            <b-col>-->
<!--                                <div class="d-flex justify-content-between align-items-center">-->
<!--                                    <label class="text-primary font-weight-bold mb-4">Personal Details</label>-->
<!--                                    <b-button variant="outline-red" squared @click="toggleDelete" size="sm">Delete</b-button>-->
<!--                                </div>-->
<!--                            </b-col>-->
<!--                        </b-row>-->
                        <b-row>
                            <b-col cols="4">
                                <label>Supplier</label>
                                <b-form-select v-model="cargo.supplier">
                                    <b-form-select-option v-for="(item, index) in suppliers" :key="index" :value="item.supplier">{{item.companyName}}</b-form-select-option>
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
    </div>
</template>

<script>
import {mapActions} from "vuex";

export default {
    data: () => ({
        cargo: {
            supplier: null,
            dateCollected: null,
            // freeStorageDate: null,
            // cargoReadyPlace: null,
            orderNumber: null,
            invoiceNumber: null,
            description: null,
            quantity: null,
            weight: null,
            // cbmConversion: null,
            length: null,
            width: null,
            height: null,
            isActive: true,
            // volume: null,
            // chargeableWeight: null,
            // numberOfStorageDays: null,
            // storageCost: null,
        },
        cargoTable: {
            resultsPerPage: 10,
            currentPage: 1,
            dataSource: [],
            isLoading: false,
            tableColumns: [
                {
                    label: 'Name',
                    key: 'name',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Surname',
                    key: 'surname',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Company Name',
                    key: 'companyName',
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
    }),
    beforeCreate() {
    },
    created() {
    },
    beforeMount() {
    },
    mounted() {
        this.getSupplierList()
    },
    beforeUpdate() {
    },
    updated() {
    },
    methods: {
        ...mapActions(["requestSupplier", "createCargo"]),
        openCargo() {},
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
        goBack() {},
        save() {
            this.$store.commit('setCreateCargoRequest', this.cargo)
            this.state = 'loading'
            this.createCargo()
                .then(() => {
                    this.$bvModal.hide('cargoAddModal')
                })
                .catch(() => {
                })
        },
    },
    computed: {
        rows() {
            return this.cargoTable.dataSource.length
        },
    },
}
</script>

<style scoped>

</style>