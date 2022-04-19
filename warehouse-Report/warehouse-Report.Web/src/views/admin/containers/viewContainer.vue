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
                    <b-row>
                        <b-col>
                            <h4 class="m-0">Available Containers</h4>
                        </b-col>
                    </b-row>
                </b-card>
            </b-col>
        </b-row>
        <b-modal id="containerAddModal" hide-footer hide-header-close class="text-center" title="Add Cargo">
            <b-row>
                <b-col>
                    <label>Container Name</label>
                    <b-form-input v-model="containerData.containerName" />
                </b-col>
            </b-row>
            <hr class="mx-3">
            <b-row>
                <b-col>
                    <div class="d-flex justify-content-end">
                        <div>
                            <b-button variant="outline-red" squared @click="hideContainerModal" class="ml-2">Cancel</b-button>
                        </div>
                        <div>
                            <b-button variant="primary" squared @click="save" class="ml-2">Save</b-button>
                        </div>
                    </div>
                </b-col>
            </b-row>
        </b-modal>
    </div>
</template>

<script>
import {mapActions} from "vuex";

export default {
    data: () => ({
        containerData: {
            containerName: null,
            isActive: null,
        },
        availableContainers: [],
    }),
    beforeCreate() {
    },
    created() {
        this.getContainers()
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
        ...mapActions(["createContainer", "requestContainer"]),
        openContainerModal() {
            this.$bvModal.show('containerAddModal')
        },
        hideContainerModal() {
            this.$bvModal.hide('containerAddModal')
        },
        save() {
            this.containerData.isActive = true
            this.$store.commit('setCreateContainerRequest', this.containerData)
            this.createContainer()
            .then(() => {
                this.toast('Container successfully created','Success','success')
                this.hideContainerModal()
            })
            .catch(() => {
                this.toast('There was an error- creating your container, Please try again','Bad News','danger')
            })
            
        },
        getContainers() {
            this.requestContainer()
            .then((response) => {
                this.availableContainers = response.data 
                console.log("CONTAINERS", this.response.data)
                console.log("CONTAINERS AVAIL", this.availableContainers)
            })
            .catch(() => {
                // this.$router.push({path: '/'})
                // localStorage.removeItem('jwt')
                // localStorage.removeItem('user')
            })
        },
        toast(message, title, variant) {
            this.$bvToast.toast(message, {
                title: title,
                variant: variant,
                toaster: 'b-toaster-top-center',
                solid: true,
                autoHideDelay: 5000,
            })
        },
    },
    computed: {},
}
</script>

<style scoped>

</style>