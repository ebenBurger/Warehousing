<template>
    <div>
        <client-nav />
        <br />
        <router-view />
    </div>
</template>

<script>
import ClientNav from "@/components/client-nav";
export default {
    components: {ClientNav},
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