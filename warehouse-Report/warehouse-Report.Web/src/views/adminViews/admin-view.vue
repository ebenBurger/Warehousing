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
                            <b-col>
                                <label>Supplier</label>
                                <b-form-select v-model="cargo.supplier">
                                    
                                </b-form-select>
                            </b-col>
                            <b-col>
                                <label>Surname</label>
                                <b-form-input ></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Mobile Number</label>
                                <b-form-input ></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="4">
                                <label>Email</label>
                                <b-form-input ></b-form-input>
                            </b-col>
                            <b-col cols="4"></b-col>
                            <b-col cols="4">
                                <label>Landline</label>
                                <b-form-input ></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>

                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <label class="text-primary font-weight-bold mb-4">Company Details</label>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="6">
                                <label>Company Name</label>
                                <b-form-input ></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Building</label>
                                <b-form-input></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Office Park</label>
                                <b-form-input ></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Address</label>
                                <b-form-input ></b-form-input>
                            </b-col>
                        </b-row>

                        <b-row>
                            <b-col>
                                <label>Address</label>
                                <b-form-input></b-form-input>
                            </b-col>
                            <b-col>
                                <label>City</label>
                                <b-form-input></b-form-input>
                            </b-col>
                            <b-col>
                                <!--                                        TODO update typo-->
                                <label>Province</label>
                                <b-form-input></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="col-2">
                                <label>Postal Code</label>
                                <b-form-input ></b-form-input>
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
            dateOfCollection: null,
            freeStorageDate: null,
            cargoReadyPlace: null,
            orderNumber: null,
            invoiceNumber: null,
            description: null,
            quantity: null,
            weights: null,
            cbmConversion: null,
            length: null,
            width: null,
            height: null,
            volume: null,
            chargeableWeight: null,
            numberOfStorageDays: null,
            storageCost: null,
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
        supplier: [],
    }),
    beforeCreate() {
    },
    created() {
    },
    beforeMount() {
    },
    mounted() {
        this.getSupplier()
    },
    beforeUpdate() {
    },
    updated() {
    },
    methods: {
        ...mapActions(["requestSupplier"]),
        openCargo() {},
        openCargoModal() {
            this.$bvModal.show('cargoAddModal')
        },
        hideCargoModal() {
            this.$bvModal.hide('cargoAddModal')
        },
        getSupplier() {
            const request = []
            this.$store.commit('setSupplierRequest', request)
            this.requestSupplier()
                .then(response => {
                    this.supplier = response.data
                })
        },
        goBack() {},
        save() {},
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