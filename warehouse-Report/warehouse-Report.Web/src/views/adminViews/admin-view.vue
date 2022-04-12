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
                                <b-button variant="outline-primary" size="sm" squared >
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
    </div>
</template>

<script>
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
    }),
    beforeCreate() {
    },
    created() {
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
        openCargo() {},
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