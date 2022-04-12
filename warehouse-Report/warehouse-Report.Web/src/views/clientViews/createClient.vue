<template>
    <div>
        <b-row class="align-items-center m-0 ">
            <b-col class="d-flex justify-content-center vw-100">
                <b-card class="w-50">
                    <b-row>
                        <B-col>
                            <h4>Add Client</h4>
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
                                <b-form-input v-model="clientDetails.name"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Surname</label>
                                <b-form-input v-model="clientDetails.surname"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Mobile Number</label>
                                <b-form-input v-model="clientDetails.mobile"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Landline</label>
                                <b-form-input v-model="clientDetails.landline"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Email</label>
                                <b-form-input v-model="clientDetails.email"></b-form-input>
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
                                <b-form-input v-model="clientDetails.companyName"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Building</label>
                                <b-form-input v-model="clientDetails.building"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Office Park</label>
                                <b-form-input v-model="clientDetails.officePark"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Address</label>
                                <b-form-input v-model="clientDetails.addressLine1"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Address</label>
                                <b-form-input v-model="clientDetails.addressLine2"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>City</label>
                                <b-form-input v-model="clientDetails.city"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Province</label>
                                <b-form-input v-model="clientDetails.provence"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="col-2">
                                <label>Post Code</label>
                                <b-form-input v-model="clientDetails.postalCode"></b-form-input>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <label>Special Instructions</label>
                                <b-textarea v-model="clientDetails.specialInstructions"/>
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
        clientDetails: {
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
        ...mapActions(["createClient"]),
        
        goBack() {
            this.$router.back()
        },
        save() {
            this.$store.commit('setCreateClientRequest', this.clientDetails)
            this.state = 'loading'
            this.createClient()
            .then(() => {
                this.$router.push({path: '/admin-home'})
            })
        },
    },
    computed: {},
}
</script>

<style scoped>

</style>