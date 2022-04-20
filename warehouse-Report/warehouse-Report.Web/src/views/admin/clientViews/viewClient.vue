<template>
    <div>
        <b-row>
            <b-col>
                <b-card>
                    <b-row>
                        <b-col>
                            <h4 class="m-0">Client View</h4>
                        </b-col>
                    </b-row>
                    <b-row class="mt-3">
                        <b-col>
                            <b-table
                                striped hover
                                :items="supplierTable.dataSource"
                                :fields="supplierTable.tableColumns"
                                :busy="supplierTable.isLoading"
                                @row-clicked="openContact"
                                id="vehicleTable"
                                :current-page="supplierTable.currentPage"
                            >
                                <template #table-busy>
                                    <div class="text-center my-2">
                                        <b-spinner style="width: 3rem; height: 3rem;"></b-spinner>
                                    </div>
                                </template>

                                <template #cell(actions)="row">
                                    <b-row align-v="center" align-h="end">
                                        <b-button @click="openContact(row.item)" size="sm" class="btn-icon">
                                            <b-icon-chevron-right></b-icon-chevron-right>
                                        </b-button>
                                    </b-row>
                                </template>

                            </b-table>

                            <b-row align-h="center" >
                                <b-pagination
                                    v-model="supplierTable.currentPage"
                                    :total-rows="rows"
                                    :per-page="supplierTable.resultsPerPage"
                                    aria-controls="contactTable"
                                ></b-pagination>
                            </b-row>

                        </b-col>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>

        <div v-if="selectedSupplier">
            <b-modal id="supplierEditModal" size="xl" hide-footer hide-header-close class="text-center" title="Edit Client">
                <div>
                    <div v-if="!isDeleteSelected">
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
                                        <b-col>
                                            <label>Name</label>
                                            <b-form-input v-model="selectedSupplier.name"></b-form-input>
                                        </b-col>
                                        <b-col>
                                            <label>Surname</label>
                                            <b-form-input v-model="selectedSupplier.surname"></b-form-input>
                                        </b-col>
                                        <b-col>
                                            <label>Mobile Number</label>
                                            <b-form-input v-model="selectedSupplier.mobile"></b-form-input>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col cols="4">
                                            <label>Email</label>
                                            <b-form-input v-model="selectedSupplier.email"></b-form-input>
                                        </b-col>
                                        <b-col cols="4"></b-col>
                                        <b-col cols="4">
                                            <label>Landline</label>
                                            <b-form-input v-model="selectedSupplier.landline"></b-form-input>
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
                                            <b-form-input v-model="selectedSupplier.companyName"></b-form-input>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col>
                                            <label>Building</label>
                                            <b-form-input v-model="selectedSupplier.building"></b-form-input>
                                        </b-col>
                                        <b-col>
                                            <label>Office Park</label>
                                            <b-form-input v-model="selectedSupplier.officePark"></b-form-input>
                                        </b-col>
                                        <b-col>
                                            <label>Address</label>
                                            <b-form-input v-model="selectedSupplier.addressLine1"></b-form-input>
                                        </b-col>
                                    </b-row>

                                    <b-row>
                                        <b-col>
                                            <label>Address</label>
                                            <b-form-input v-model="selectedSupplier.addressLine2"></b-form-input>
                                        </b-col>
                                        <b-col>
                                            <label>City</label>
                                            <b-form-input v-model="selectedSupplier.city"></b-form-input>
                                        </b-col>
                                        <b-col>
                                            <!--                                        TODO update typo-->
                                            <label>Province</label>
                                            <b-form-input v-model="selectedSupplier.provence"></b-form-input>
                                        </b-col>
                                    </b-row>
                                    <b-row>
                                        <b-col class="col-2">
                                            <label>Postal Code</label>
                                            <b-form-input v-model="selectedSupplier.postalCode"></b-form-input>
                                        </b-col>
                                    </b-row>
                                    <hr class="mx-3">
                                    <b-row>
                                        <b-col>
                                            <label>Special Instructions</label>
                                            <b-textarea v-model="selectedSupplier.specialInstructions"/>
                                        </b-col>
                                    </b-row>
                                    <hr class="mx-3">
                                    <b-row>
                                        <b-col>
                                            <div class="d-flex justify-content-end">
                                                <div>
                                                    <b-button variant="outline-red" squared @click="closeClient" class="ml-2">Cancel</b-button>
                                                </div>
                                                <div>
                                                    <b-button variant="primary" squared @click="update" class="ml-2">Save</b-button>
                                                </div>
                                            </div>
                                        </b-col>
                                    </b-row>
                                </B-form>
                            </b-col>
                        </b-row>
                    </div>
                    <div v-if="isDeleteSelected">
                        <div class="d-flex justify-content-center">
                            <p class="h4">
                                Are you sure you want to delete the supplier <span class="text-danger font-weight-bold">{{selectedSupplier.name}} {{selectedSupplier.surname}}</span>
                            </p>
                        </div>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-end">
                                    <div>
                                        <b-button variant="outline-red" squared @click="toggleDelete" class="ml-2">Cancel</b-button>
                                    </div>
                                    <div>
                                        <b-button variant="danger" squared @click="deleteClient" class="ml-2">Delete</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </div>
                </div>
            </b-modal>
        </div>

    </div>
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
    data: () => ({
        supplier: {
            name: null,
            surname: null,
            mobile: null,
            landline: null,
            email: null,
            companyName: null,
            building: null,
            officePark: null,
            addressLine1: null,
            addressLine2: null,
            postalCode: null,
            city: null,
            provence: null,
            isActive: true,
            location: null,
            officeId: null,
            specialInstructions: null,
        },
        supplierTable: {
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
        isDeleteSelected: false,
    }),
    beforeCreate() {
    },
    created() {
        this.getSupplier()
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
        ...mapActions(["requestSupplier", "updateSupplier"]),
        
        openContact(rowItem) {
            this.$store.commit('setSelectedSupplier', rowItem)
            this.$bvModal.show('supplierEditModal')
        },
        closeClient() {
            this.$bvModal.hide('supplierEditModal')
        },
        
        getSupplier() {
            this.supplierTable.isLoading = true
            
            this.requestSupplier()
            .then(response => {
                console.log("CLIENT DATA", response.data)
                this.supplierTable.dataSource = response.data
                this.supplierTable.isLoading = false
            })
            .catch(() => {
                this.$router.push({path: '/'})
                localStorage.removeItem('jwt')
                localStorage.removeItem('user')
            })
        },
        
        update() {
            this.$store.commit('setSelectedSupplier', this.selectedSupplier)
            this.updateSupplier()
            .then(() => {
                this.$bvModal.hide('supplierEditModal')
            })
        },
        deleteClient() {
            this.selectedSupplier.isActive = false
            this.$store.commit('setSelectedSupplier', this.selectedSupplier)
            this.updateSupplier()
                .then(() => {
                    this.closeClient()
                    this.getSupplier()
                })
        },
        
        toggleDelete() {
            this.isDeleteSelected = !this.isDeleteSelected
        },
    },
    computed: {
        //TODO- fix the -- (Computed property "selectedCargo" was assigned to but it has no setter.)
        ...mapState([
            "selectedSupplier"
        ]),
        rows() {
            return this.supplierTable.dataSource.length
        },
    },
}
</script>

<style scoped>

</style>