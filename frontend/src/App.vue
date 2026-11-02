<script setup lang="ts">
  import { RouterLink, RouterView } from "vue-router";
</script>

<template>
  <header>
    <RouterLink to="/">Home</RouterLink>
    <RouterLink to="/login">Login</RouterLink>
  </header>
  <main class="main">
    <RouterView />
  </main>
</template>

<style scoped></style>
