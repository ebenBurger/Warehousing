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
                    <b-form>
                        <b-row>
                            <b-col>
                                <label class="text-primary font-weight-bold mb-4">Company Details</label>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col cols="6">
                                <label>Company Name <span class="text-danger">*</span> </label>
                                <b-form-input v-model="$v.supplierDetails.companyName.$model" 
                                              @blur="$v.supplierDetails.companyName.$touch()"></b-form-input>
                                <div v-if="$v.supplierDetails.companyName.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.supplierDetails.companyName.required">This is a required field</p>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Building</label>
                                <b-form-input v-model="supplierDetails.building" ></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Office Park</label>
                                <b-form-input v-model="supplierDetails.officePark"></b-form-input >
                            </b-col>
                            <b-col>
                                <label>Address <span class="text-danger">*</span></label>
                                <b-form-input v-model="$v.supplierDetails.addressLine1.$model"
                                              @blur="$v.supplierDetails.addressLine1.$touch()"></b-form-input>
                                <div v-if="$v.supplierDetails.addressLine1.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.supplierDetails.addressLine1.required">This is a required field</p>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Address</label>
                                <b-form-input v-model="supplierDetails.addressLine2"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>City <span class="text-danger">*</span></label>
                                <b-form-input v-model="$v.supplierDetails.city.$model"
                                              @blur="$v.supplierDetails.city.$touch()"></b-form-input>
                                <div v-if="$v.supplierDetails.city.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.supplierDetails.city.required">This is a required field</p>
                                </div>
                            </b-col>
                            <b-col>
                                <label>Province <span class="text-danger">*</span></label>
                                <b-form-input v-model="$v.supplierDetails.provence.$model"
                                              @blur="$v.supplierDetails.city.$touch()"></b-form-input>
                                <div v-if="$v.supplierDetails.city.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.supplierDetails.city.required">This is a required field</p>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col class="col-2">
                                <label>Post Code <span class="text-danger">*</span></label>
                                <b-form-input v-model="$v.supplierDetails.postalCode.$model"
                                              @blur="$v.supplierDetails.postalCode.$touch()"></b-form-input>
                                <div v-if="$v.supplierDetails.postalCode.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.supplierDetails.postalCode.required">This is a required field</p>
                                    <p v-if="!$v.supplierDetails.postalCode.minLength || 
                                                !$v.supplierDetails.postalCode.maxLength">Please check the length (4-6 character)</p>
                                    <p v-if="!$v.supplierDetails.postalCode.numeric">This doesn't seem to be a postal code</p>
                                </div>
                            </b-col>
                        </b-row>
                        <hr class="mx-3">
                        <b-row>
                            <b-col>
                                <label class="text-primary font-weight-bold mb-4">Contact Person Details</label>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Name <span class="text-danger">*</span></label>
                                <b-form-input v-model="$v.supplierDetails.name.$model"
                                              @blur="$v.supplierDetails.name.$touch()"></b-form-input>
                                <div v-if="$v.supplierDetails.name.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.supplierDetails.name.required">This is a required field</p>
                                </div>
                            </b-col>
                            <b-col>
                                <label>Surname <span class="text-danger">*</span></label>
                                <b-form-input v-model="$v.supplierDetails.surname.$model"
                                              @blur="$v.supplierDetails.surname.$touch()"></b-form-input>
                                <div v-if="$v.supplierDetails.surname.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.supplierDetails.surname.required">This is a required field</p>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Mobile Number <span class="text-danger">*</span></label>
                                <b-form-input v-model="$v.supplierDetails.mobile.$model"
                                              @blur="$v.supplierDetails.mobile.$touch()"></b-form-input>
                                <div v-if="$v.supplierDetails.mobile.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.supplierDetails.mobile.required">This is a required field</p>
                                    <p v-if="!$v.supplierDetails.mobile.minLength || 
                                                !$v.supplierDetails.mobile.maxLength || 
                                                !$v.supplierDetails.mobile.numeric">There seems to be an error- enter a proper mobile number (no spacing)</p>
                                </div>
                            </b-col>
                            <b-col>
                                <label>Landline <span class="text-danger">*</span></label>
                                <b-form-input v-model="$v.supplierDetails.landline.$model"
                                              @blur="$v.supplierDetails.landline.$touch()"></b-form-input>
                                <div v-if="$v.supplierDetails.landline.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.supplierDetails.landline.required">This is a required field</p>
                                    <p v-if="!$v.supplierDetails.landline.minLength || 
                                                !$v.supplierDetails.landline.maxLength || 
                                                !$v.supplierDetails.landline.numeric">There seems to be an error- enter a proper landline number (no spacing)</p>
                                </div>
                            </b-col>
                            <b-col>
                                <label>Email <span class="text-danger">*</span></label>
                                <b-form-input v-model="supplierDetails.email"
                                              @blur="$v.supplierDetails.email.$touch()"></b-form-input>
                                <div v-if="$v.supplierDetails.email.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.supplierDetails.email.required">This is a required field</p>
                                    <p v-if="!$v.supplierDetails.email.email">Please check the email address</p>
                                </div>
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
                                        <b-button variant="primary" squared :disabled="$v.supplierDetails.$invalid" @click="save" class="ml-2">Save</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </b-form>
                </b-card>
            </b-col>
        </b-row>
    </div>
</template>

<script>
import {mapActions} from "vuex";
import {email, maxLength, minLength, numeric, required} from "vuelidate/lib/validators";

export default {
    name: "createClient",
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
                this.$router.push({name: 'clientView'})
            })
            .catch(() => {
            })
        },
    },
    computed: {},
    validations: {
        supplierDetails: {
            name: {required},
            surname: {required},
            mobile: {required,minLength: minLength(10), maxlength: maxLength(10), numeric},
            landline: {required,minLength: minLength(10), maxlength: maxLength(10), numeric},
            email: {required, email},
            
            companyName: {required},
            addressLine1: {required},
            city: {required},
            postalCode: {required, minLength: minLength(4), maxlength: maxLength(6), numeric},
            provence: {required}
        },
    },
}
</script>

<style scoped>

</style>