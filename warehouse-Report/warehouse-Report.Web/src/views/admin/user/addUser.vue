<template>
    <div>
        <b-row class="align-items-center m-0 ">
            <b-col class="d-flex justify-content-center vw-100">
                <b-card class="w-50">
                    <b-row>
                        <B-col>
                            <h4>Add User</h4>
                        </B-col>
                    </b-row>
                    <B-form>
                        <b-row>
                            <b-col>
                                <label class="text-primary font-weight-bold mb-4">Enter details for a normail User account</label>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>First Name</label>
                                <b-form-input v-model="$v.userDetails.firstName.$model" 
                                              @blur="$v.userDetails.firstName.$touch()"></b-form-input>
                                <div v-if="$v.userDetails.firstName.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.userDetails.firstName.minLength">Please check the Spelling</p>
                                    <p v-if="!$v.userDetails.firstName.required">This is a required field</p>
                                </div>
                            </b-col>
                            <b-col>
                                <label>Surname</label>
                                <b-form-input v-model="$v.userDetails.surname.$model" 
                                              @blur="$v.userDetails.surname.$touch()"></b-form-input>
                                <div v-if="$v.userDetails.surname.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.userDetails.surname.minLength">Please check the name</p>
                                    <p v-if="!$v.userDetails.surname.required">This is a required field</p>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>UserName</label>
                                <b-form-input v-model="$v.userDetails.userName.$model" 
                                              @blur="$v.userDetails.userName.$touch()"></b-form-input>
                                <div v-if="$v.userDetails.userName.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.userDetails.userName.required">This is a required field</p>
                                </div>
                            </b-col>
                            <b-col>
                                <label>Email</label>
                                <b-form-input v-model="$v.userDetails.email.$model" 
                                              @blur="$v.userDetails.email.$touch()"></b-form-input>
                                <div v-if="$v.userDetails.email.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.userDetails.email.email">This seems to not be an email.</p>
                                    <p v-if="!$v.userDetails.email.required">This is a required field</p>
                                </div>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Password</label>
                                <b-form-input type="password" v-model="$v.userDetails.password.$model" 
                                              @blur="$v.userDetails.password2.$touch()"></b-form-input>
                                <div v-if="$v.userDetails.password.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.userDetails.password.minLength">The password needs to be minimum length of 6 characters</p>
                                    <p v-if="!$v.userDetails.password.required">This is a required field</p>
                                    <p v-if="!$v.userDetails.password.containsUppercase">UppderCase</p>
                                    <p v-if="!$v.userDetails.password.containsLowercase">LowerCase</p>
                                    <p v-if="!$v.userDetails.password.containsNumber">Numbers</p>
                                    <p v-if="!$v.userDetails.password.containsSpecial">Special character</p>
                                </div>
                            </b-col>
                            <b-col>
                                <label>Verify Password</label>
                                <b-form-input type="password" v-model="$v.userDetails.password2.$model" 
                                              @blur="$v.userDetails.password2.$touch()"></b-form-input>
                                <div v-if="$v.userDetails.password2.$error" class="text-danger font-weight-400">
                                    <p v-if="!$v.userDetails.password2.sameAsPassword">Passwords do not match</p>
                                    <p v-if="!$v.userDetails.password2.required">This is a required field</p>
                                </div>
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
                                        <b-button variant="primary" squared @click="showUserAddModal" :disabled="$v.userDetails.$invalid" class="ml-2">Save</b-button>
                                    </div>
                                </div>
                            </b-col>
                        </b-row>
                    </B-form>
                </b-card>
            </b-col>
        </b-row>
        <b-modal id="UserAdd" hide-footer hide-header-close class="text-center" title="Add User">
            <p>Are you sure you want to add <span class="text-success font-weight-bold font-italic">{{userDetails.userName}}</span> as a User</p>
            <hr class="mx-3">
            <b-row>
                <b-col>
                    <div class="d-flex justify-content-end">
                        <div>
                            <b-button variant="outline-red" squared @click="hideUserAddModal" class="ml-2">Cancel</b-button>
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
import {email, minLength, required, sameAs} from "vuelidate/lib/validators";

export default {
    name: "createUser",
    data: () => ({
        userDetails: {
            userName: null,
            firstName: null,
            surname: null,
            email: null,
            password:null,
            password2: null,
        }
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
        ...mapActions(["createUser"]),
        
        goBack() {
            this.$router.push({name: 'adminView'})
        },
        showUserAddModal() {
            this.$bvModal.show('UserAdd')
        },
        hideUserAddModal() {
            this.$bvModal.hide('UserAdd')
        },
        save() {
            this.$store.commit('setUserCreateRequest', this.userDetails)
                this.createUser()
                .then(() => {
                    this.$router.push({name: 'adminView'})
                })
        },
    },
    computed: {},
    validations: {
        userDetails: {
            userName: {required},
            firstName: {required, minLength: minLength(3)},
            surname: {required, minLength: minLength(5)},
            email: {required, email},
            password:{
                required, 
                minLength: minLength(6),
                containsUppercase: function(value) {
                    return /[A-Z]/.test(value)
                },
                containsLowercase: function(value) {
                    return /[a-z]/.test(value)
                },
                containsNumber: function(value) {
                    return /[0-9]/.test(value)
                },
                containsSpecial: function(value) {
                    return /[#?!@$%^&*-]/.test(value)
                }},
            password2: {required, sameAsPassword: sameAs('password')},
        }
    },
}
</script>

<style scoped>

</style>