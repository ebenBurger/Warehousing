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
                                :current-page="cargoCompleteTable.currentPage"
                                id="cargoTable"
                            ></b-table>

                            <b-row align-h="center" >
                                <b-pagination
                                    v-model="cargoCompleteTable.currentPage"
                                    :total-rows="cargoRows"
                                    :per-page="cargoCompleteTable.resultsPerPage"
                                    aria-controls="cargoTable"
                                ></b-pagination>
                            </b-row>

                        </b-col>
<!--                        <b-col v-if="cargoCompleteTable.dataSource.length === 0" class="text-center">-->
<!--                            <h3>Nothing to see here</h3>-->
<!--                        </b-col>-->
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
import {mapActions} from "vuex";

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
        }
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
        
        goBack() {
            this.$router.back()
        },
        
        completeCargo () {
            this.requestCompleteCargo()
                .then((response) => {
                    this.cargoCompleteTable.dataSource = response.data
                    console.log('COMPLETE', response.data)
                    console.log('COMPLETE 2', this.cargoCompleteTable.dataSource)
                })
        },
        rowClass(item, type) {
            if (!item || type !== 'row') return
            if (item.hazardous === true) return 'table-danger'
        }
    },
    computed: {
        cargoRows() {
            return this.cargoCompleteTable.dataSource.length
        },
    },
}
</script>

<style scoped>
</style>