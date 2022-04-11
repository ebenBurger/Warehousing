<template>
    <div>
        <b-row class="align-items-center vh-100 m-0 ">
            <b-col class="d-flex justify-content-center vw-100">
                <b-card class="w-50">
                    <b-row>
                        <B-col>
                            <h4>Add Contact</h4>
                        </B-col>
                    </b-row>
                    <B-form>
                        <label class="text-primary font-weight-bold mb-4">Personal Details</label>
                        <b-row>
                            <b-col>
                                <label>Name</label>
                                <b-form-input v-model="contactData.name"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Surname</label>
                                <b-form-input v-model="contactData.surname"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Mobile Number</label>
                                <b-form-input v-model="contactData.mobile"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Landline</label>
                                <b-form-input v-model="contactData.landline"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="6">
                                <label>Email</label>
                                <b-form-input v-model="contactData.email"></b-form-input>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <label class="text-primary font-weight-bold mb-4">Company Details</label>
                        <b-row>
                            <b-col>
                                <label>Company Name</label>
                                <b-form-input v-model="contactData.companyName"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Building</label>
                                <b-form-input v-model="contactData.building"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Office Park</label>
                                <b-form-input v-model="contactData.officePark"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Address Line 1</label>
                                <b-form-input v-model="contactData.addressLine1"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Address Line 2</label>
                                <b-form-input v-model="contactData.addressLine2"></b-form-input>
                            </b-col>
                        </b-row>

                        <b-row>
                            <b-col>
                                <label>City</label>
                                <b-form-input v-model="contactData.city"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Provence</label>
                                <b-form-input v-model="contactData.provence"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="col-2">
                                <label>Post Code</label>
                                <b-form-input v-model="contactData.postalCode"></b-form-input>
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
export default {
    data: () => ({
        contactData: {
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
        goBack() {
            this.$router.back()
        },
        save() {},
    },
    computed: {},
}
</script>

<style scoped>

</style>