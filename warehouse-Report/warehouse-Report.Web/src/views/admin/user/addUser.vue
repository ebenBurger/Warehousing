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
                                <label>UserName</label>
                                <b-form-input v-model="userDetails.userName"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Email</label>
                                <b-form-input v-model="userDetails.email"></b-form-input>
                            </b-col>
                        </b-row>
                        <b-row>
                            <b-col>
                                <label>Password</label>
                                <b-form-input type="password" v-model="userDetails.password"></b-form-input>
                            </b-col>
                            <b-col>
                                <label>Verify Password</label>
                                <b-form-input type="password" v-model="userDetails.password2"></b-form-input>
                                
                            </b-col>
                        </b-row>
                        <b-row class="text-center">
                            <b-col>
                                <div v-if="userDetails.password !== userDetails.password2" class="text-danger">
                                    The passwords do not match - Please try again.
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
                                        <b-button variant="primary" squared @click="showUserAddModal" class="ml-2">Save</b-button>
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

export default {
    name: "createUser",
    data: () => ({
        userDetails: {
            userName: null,
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
}
</script>

<style scoped>

</style>