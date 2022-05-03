<template>
    <div>
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
        <b-row class="text-center">
            <b-col>
                <h1>Coming Soon</h1>
            </b-col>
        </b-row>
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
        goBack() {
            this.$router.back()
        },
    },
    computed: {},
}
</script>

<style scoped>

</style>