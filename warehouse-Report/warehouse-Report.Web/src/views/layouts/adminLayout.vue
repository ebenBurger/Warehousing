<template>
    <div>
        <admin-nav /> 
        <br />
        <router-view />
    </div>
</template>

<script>
import AdminNav from "@/components/admin-nav";
export default {
    components: {AdminNav},
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
    methods: {},
    computed: {},
}
</script>

<style scoped>

</style>