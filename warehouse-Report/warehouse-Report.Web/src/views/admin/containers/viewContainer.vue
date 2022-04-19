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
                                <b-button variant="outline-primary" size="sm" squared @click="openContainerModal">
                                    <font-awesome-icon icon="fa-plus" class="mr-1" />
                                    Add Container
                                </b-button>
                            </b-col>
                        </b-col>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
        <b-modal id="containerAddModal" hide-footer hide-header-close class="text-center" title="Add Cargo">
            
        </b-modal>
    </div>
</template>

<script>
export default {
    data: () => ({}),
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
        openContainerModal() {
            this.$bvModal.show('containerAddModal')
        },
    },
    computed: {},
}
</script>

<style scoped>

</style>