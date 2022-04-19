<template>
    <div>
        <b-row class="align-items-center m-0 ">
            <b-col class="d-flex justify-content-center vw-100">
                <b-card class="w-50">
                    <b-row>
                        <B-col>
                            <h4>Add Supplier</h4>
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
                                <b-form-input v-model="supplierDetails.name"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Surname</label>
                                <b-form-input v-model="supplierDetails.surname"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Mobile Number</label>
                                <b-form-input v-model="supplierDetails.mobile"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Landline</label>
                                <b-form-input v-model="supplierDetails.landline"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Email</label>
                                <b-form-input v-model="supplierDetails.email"></b-form-input>
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
                                <b-form-input v-model="supplierDetails.companyName"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Building</label>
                                <b-form-input v-model="supplierDetails.building"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Office Park</label>
                                <b-form-input v-model="supplierDetails.officePark"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Address</label>
                                <b-form-input v-model="supplierDetails.addressLine1"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Address</label>
                                <b-form-input v-model="supplierDetails.addressLine2"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>City</label>
                                <b-form-input v-model="supplierDetails.city"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Province</label>
                                <b-form-input v-model="supplierDetails.provence"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="col-2">
                                <label>Post Code</label>
                                <b-form-input v-model="supplierDetails.postalCode"></b-form-input>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <label>Special Instructions</label>
                                <b-textarea v-model="supplierDetails.specialInstructions"/>
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
                                        <b-button variant="primary" squared @click="save" class="ml-2">Save</b-button>
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
import {mapActions} from "vuex";



export default {
    data: () => ({
        supplierDetails: {
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
        ...mapActions(["createSupplier"]),
        
        goBack() {
            this.$router.back()
        },
        save() {
            this.$store.commit('setCreateSupplierRequest', this.supplierDetails)
            this.state = 'loading'
            this.createSupplier()
            .then(() => {
                this.$router.push({path: '/admin-home'})
            })
            .catch(() => {
            })
        },
    },
    computed: {},
}
</script>

<style scoped>

</style>