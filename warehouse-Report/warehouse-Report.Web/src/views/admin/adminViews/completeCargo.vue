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
                    <b-row class="text-center justify-content-around">
                        <div v-if="cargoComplete.length >= 1">
                            <b-col v-for="(item, index) in cargoComplete" :key="index">
                                <b-card>
                                    <div class="cargoItem">
                                        <b-card-text>
                                            <label>Supplier: {{item.supplier}}</label>
                                            <label>Description: {{item.description}}</label>
                                            <label>Date Captured: {{item.dateCreated | dateFilter}}</label>
                                            <label>Order Number: {{item.orderNumber}}</label>
                                            <label>Date Complete: {{item.dateComplete}}</label>
                                        </b-card-text>
                                    </div>
                                </b-card>
                            </b-col>
                        </div>
                        <b-col v-if="cargoComplete.length === 0" class="text-center">
                            <h3>Nothing to see here</h3>
                        </b-col>
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
        cargoComplete : []
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
                    this.cargoComplete = response.data
                    console.log('COMPLETE', response.data)
                })
        },
    },
    computed: {},
}
</script>

<style scoped>
.cargoItem {
    border: 2px solid black;
    border-radius: 12px;
    padding: 2rem 0;
    width: 220px;
}
</style>