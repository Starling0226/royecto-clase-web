<script setup lang="ts">
const message = "Reservas de citas médicas @pp";
</script>

<template>
  <div class="simple-container">
    <h1>{{ message }}</h1>
  </div>
</template>

<style lang="css" scoped>
.simple-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 80vh; 
}

h1 {
  text-align: center;
  font-weight: 400;
  color: #4a5568; 
}
</style>
