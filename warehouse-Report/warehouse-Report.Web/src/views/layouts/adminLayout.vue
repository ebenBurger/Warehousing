<template>
    <div class="layout">
        <div class="overlay">
            <admin-nav />
            <br />
            <router-view />
        </div>
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

<style scoped lang="scss">

.layout {
    background-image: url("../../assets/warehouse2.jpg");
    background-position: center;
    height: 100vh;
    width: 100vw;
    
    .overlay {
        position: absolute;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background: rgba(0, 0, 0, 0.5);
    }
}

</style>