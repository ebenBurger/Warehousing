<template>
    <div>
        <b-row class="align-items-center vh-100 m-0 ">
            <b-col class="d-flex justify-content-center vw-100">
                <b-card class="w-50">
                    <b-row>
                        <B-col>
                            <h4>Edit Client</h4>
                        </B-col>
                    </b-row>
                    <B-form>
                        <b-row>
                            <b-col>
                                <label class="text-primary font-weight-bold mb-4">Personal Details</label>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Name</label>
                                <b-form-input v-model="selectedClient.name"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Surname</label>
                                <b-form-input v-model="selectedClient.surname"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Mobile Number</label>
                                <b-form-input v-model="selectedClient.mobile"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Landline</label>
                                <b-form-input v-model="selectedClient.landline"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="6">
                                <label>Email</label>
                                <b-form-input v-model="selectedClient.email"></b-form-input>
                            </b-col>
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
                                <b-form-input v-model="selectedClient.companyName"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Building</label>
                                <b-form-input v-model="selectedClient.building"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Office Park</label>
                                <b-form-input v-model="selectedClient.officePark"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Address Line 1</label>
                                <b-form-input v-model="selectedClient.addressLine1"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Address Line 2</label>
                                <b-form-input v-model="selectedClient.addressLine2"></b-form-input>
                            </b-col>
                        </b-row>

                        <b-row>
                            <b-col>
                                <label>City</label>
                                <b-form-input v-model="selectedClient.city"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Provence</label>
                                <b-form-input v-model="selectedClient.provence"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="col-2">
                                <label>Post Code</label>
                                <b-form-input v-model="selectedClient.postalCode"></b-form-input>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <label>Special Instructions</label>
                                <b-textarea v-model="selectedClient.specialInstructions"/>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <div class="d-flex justify-content-end">
                                    <div>
                                        <b-button variant="outline-red" squared @click="goBack" class="ml-2">Cancel</b-button>
                                    </div>
                                    <div>
                                        <b-button variant="primary" squared @click="update" class="ml-2">Save</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </B-form>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
import {mapState} from "vuex";

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
        goBack() {
            this.$router.back()
        },
        
        update() {},
    },
    computed: {
        ...mapState([
            "selectedClient"
        ])
    },
}
</script>

<style scoped>

</style>