<template>
    <div>
        <h3>Layout</h3>
        <br />
        <router-view />
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
        this.$store.commit('setApiUrl', this.$root.urlConfig.api_url)
    },
    beforeUpdate() {
    },
    updated() {
    },
    methods: {},
    computed: {},
}
</script>

<style scoped>

</style>