<script lang="ts" setup>
  import { toTypedSchema } from "@vee-validate/zod";
  import { z } from "zod";
  import { useForm } from "vee-validate";

  // import { ref } from "vue";

  const BACKEND_URL = import.meta.env.VITE_BACKEND_URL;

  const schema = toTypedSchema(
    z.object({
      email: z
        .string({ message: "El correo es obligatorio" })
        .email({ message: "El correo es inválido." }),
      password: z
        .string({ message: "La contraseña es obligatoria" })
        .min(8, { message: "La contraseña debe tener mínimo 8 caracteres" }),
    }),
  );
  const { errors, handleSubmit, defineField } = useForm({
    validationSchema: schema,
  });

  /*
  // emailAttrs | passwordAttrs → contiene los atributos y listeners que Vee Validate necesita para gestionar estos campos.
  */
  const [email, emailAttrs] = defineField("email");
  const [password, passwordAttrs] = defineField("password");

  const onSubmit = handleSubmit(async (values) => {
    try {
      const request = await fetch(`${BACKEND_URL}/v1/auth/login`, {
        headers: {
          "Content-Type": "application/json;charset=utf-8",
        },
        body: JSON.stringify(values),
        method: "POST",
      });
      const response = await request.json();
      console.log(response);
    } catch (error) {
      console.log(error);
    }
  });
</script>
<template>
  <h1 class="title">Login</h1>

  <section class="section">
    <form class="form" @submit.prevent="onSubmit">
      <fieldset class="form__fieldset">
        <input
          class="form__input"
          type="text"
          placeholder="Email"
          v-model="email"
          v-bind="emailAttrs"
          :class="{ 'form__input--name': errors.email }"
        />
        <span>{{ errors.email }}</span>
      </fieldset>
      <fieldset class="form__fieldset">
        <input
          class="form__input"
          type="password"
          placeholder="Contraseña"
          v-model="password"
          v-bind="passwordAttrs"
          :class="{ 'form__input--password': errors.password }"
        />
        <span>{{ errors.password }}</span>
      </fieldset>

      <button class="form__btn" type="submit">Ingresar</button>
    </form>
  </section>
</template>

<style lang="css" scoped>
  .title {
    margin-bottom: 0.5rem;
  }
  .form {
    display: grid;
    gap: 0.75rem;
  }
  .form__fieldset {
    width: 100%;
    display: flex;
    flex-direction: column;
    border: none;
  }

  .form__input,
  .form__btn {
    padding: 0.5rem 1rem;
    font-size: 1.25rem;
  }
  .form__btn {
    cursor: pointer;
  }
  .form__input--name {
    border: 1px solid red;
    outline: 1px solid red;
  }

  .form__input--password {
    border: 1px solid red;
    outline: 1px solid red;
  }
</style>
