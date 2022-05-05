<template>
    <div>
        <b-row>
            <b-col>
                <b-card>
                    <b-row>
                        <b-col>
                            <div class="d-flex justify-content-between align-items-center">
                                <h4 class="m-0">Complete Container</h4>
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
                                :items="containerTable.dataSource"
                                :fields="containerTable.tableColumns"
                                :busy="containerTable.isLoading"
                                :per-page="containerTable.resultsPerPage"
                                :total-rows="rows"
                                @row-clicked="openEditModal"
                                :current-page="containerTable.currentPage"
                                id="cargoTable"
                            >

                                <template #cell(actions)="data">
                                    <b-row align-v="center" align-h="end">
                                        <b-button @click="openEditModal(data.item, data.index)" size="sm" class="btn-icon">
                                            <b-icon-chevron-right></b-icon-chevron-right>
                                        </b-button>
                                    </b-row>
                                </template>
                                
                            </b-table>

                            <b-row align-h="center" >
                                <b-pagination
                                    v-model="containerTable.currentPage"
                                    :total-rows="rows"
                                    :per-page="containerTable.resultsPerPage"
                                    aria-controls="cargoTable"
                                ></b-pagination>
                            </b-row>

                        </b-col>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
        
        <div v-if="selectedCompleteContainer">
            <b-modal id="containerEdit" size="lg" hide-footer hide-header class="text-center" title="Edit Container Entry">
                <div v-if="!isRestore">
                    <b-row>
                        <b-col>
                            <label>Container Nuber</label>
                            {{selectedCompleteContainer.containerNumber}}
                        </b-col>
                        <b-col></b-col>
                        <b-col></b-col>
                    </b-row>
                    <hr class="mx-3">
                    <b-row>
                        <b-col>
                            <div class="d-flex justify-content-end">
                                <div>
                                    <b-button variant="outline-red" squared @click="hideEditModal" class="ml-2">Cancel</b-button>
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
                            <h4>Are you sure you want to reactivate this container</h4>
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
</template>

<script>
import {mapActions, mapState} from "vuex";

export default {
    data: () => ({
        containerTable : {
            resultsPerPage: 10,
            currentPage: 1,
            dataSource: [],
            isLoading: true,
            tableColumns: [
                {
                    label: 'Container Number',
                    key: 'containerNumber',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Vessel',
                    key: 'vesel',
                    sortable: true,
                    tdClass:'',
                },
                {
                    label: 'Voyage',
                    key: 'voyage',
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
        isRestore: false,
    }),
    beforeCreate() {
    },
    created() {
    },
    beforeMount() {
    },
    mounted() {
        this.completeContainer()
    },
    beforeUpdate() {
    },
    updated() {
    },
    methods: {
        ...mapActions(['requestCompleteContainer']),
        goBack() {
            this.$router.back()
        },
        completeContainer() {
            this.containerTable.isLoading = true
            this.requestCompleteContainer()
                .then((response) => {
                    this.containerTable.dataSource = response.data
                    this.containerTable.isLoading = false
                    console.log('COMPLETE', response.data)
                    console.log('COMPLETE 2', this.containerTable.dataSource)
                })
        },
        openEditModal(rowItem) {
            this.$bvModal.show('containerEdit')
            this.$store.commit('selectedCompleteContainer', rowItem)
            console.log("ITEM", rowItem)
        },
        hideEditModal() {
            this.$bvModal.hide('containerEdit')
        },
        restore() {
            this.isRestore = !this.isRestore
        },
        reactivate() {},
    },
    computed: {
        ...mapState([
            "selectedCompleteContainer"
        ]),
        rows () {
            return this.containerTable.dataSource.length
        },
    },
}
</script>

<style scoped>

</style>