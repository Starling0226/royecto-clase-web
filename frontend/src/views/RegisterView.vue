<script lang="ts" setup>
  import { toTypedSchema } from "@vee-validate/zod";
  import { z } from "zod";
  import { useForm } from "vee-validate";
  import { RouterLink, useRouter } from "vue-router";
  import { useAuthStore } from "@/stores/auth";

  const BACKEND_URL = import.meta.env.VITE_BACKEND_URL;
  const router = useRouter();
  const authStore = useAuthStore();

  const schema = toTypedSchema(
    z
      .object({
        name: z
          .string({ message: "El nombre es obligatorio" })
          .min(3, { message: "El nombre debe tener al menos 3 caracteres" })
          .max(100, { message: "El nombre no debe superar los 100 caracteres" }),
        email: z
          .string({ message: "El correo es obligatorio" })
          .email({ message: "El correo es inválido." }),
        password: z
          .string({ message: "La contraseña es obligatoria" })
          .min(8, { message: "La contraseña debe tener mínimo 8 caracteres" }),
        confirmPassword: z
          .string({ message: "Debes confirmar la contraseña" })
          .min(8, { message: "La contraseña debe tener mínimo 8 caracteres" }),
      })
      .refine((data) => data.password === data.confirmPassword, {
        message: "Las contraseñas no coinciden",
        path: ["confirmPassword"],
      }),
  );

  const { errors, handleSubmit, defineField } = useForm({
    validationSchema: schema,
  });

  const [name, nameAttrs] = defineField("name");
  const [email, emailAttrs] = defineField("email");
  const [password, passwordAttrs] = defineField("password");
  const [confirmPassword, confirmPasswordAttrs] = defineField("confirmPassword");

  const onSubmit = handleSubmit(async (values) => {
    try {
      const request = await fetch(`${BACKEND_URL}/v1/auth/register`, {
        headers: {
          "Content-Type": "application/json;charset=utf-8",
        },
        body: JSON.stringify(values),
        method: "POST",
      });
      const response = await request.json();
      console.log(response);

      if (request.ok && response.token) {
        authStore.setSession(response);
        await router.push({ name: "home" });
      }
    } catch (error) {
      console.log(error);
    }
  });
</script>
<template>
  <div class="auth-page">
    <section class="section auth-card">
      <h1 class="title">Register</h1>

      <form class="form" @submit.prevent="onSubmit">
        <fieldset class="form__fieldset">
          <input
            class="form__input"
            type="text"
            placeholder="Name"
            v-model="name"
            v-bind="nameAttrs"
            :class="{ 'form__input--error': errors.name }"
          />
          <span v-if="errors.name" class="form__error">{{ errors.name }}</span>
        </fieldset>

        <fieldset class="form__fieldset">
          <input
            class="form__input"
            type="text"
            placeholder="Email"
            v-model="email"
            v-bind="emailAttrs"
            :class="{ 'form__input--error': errors.email }"
          />
          <span v-if="errors.email" class="form__error">{{ errors.email }}</span>
        </fieldset>

        <fieldset class="form__fieldset">
          <input
            class="form__input"
            type="password"
            placeholder="Password"
            v-model="password"
            v-bind="passwordAttrs"
            :class="{ 'form__input--error': errors.password }"
          />
          <span v-if="errors.password" class="form__error">{{ errors.password }}</span>
        </fieldset>

        <fieldset class="form__fieldset">
          <input
            class="form__input"
            type="password"
            placeholder="Confirm Password"
            v-model="confirmPassword"
            v-bind="confirmPasswordAttrs"
            :class="{ 'form__input--error': errors.confirmPassword }"
          />
          <span v-if="errors.confirmPassword" class="form__error">{{
            errors.confirmPassword
          }}</span>
        </fieldset>

        <button class="form__btn" type="submit">Sign up</button>
      </form>

      <div class="auth-footer">
        <p class="auth-footer__text">
          Already have an account?
          <RouterLink to="/login" class="auth-footer__link">Login</RouterLink>
        </p>
      </div>
    </section>
  </div>
</template>

<style lang="css" scoped>
  .auth-page {
    position: fixed;
    inset: 0;
    min-height: 100vh;
    width: 100vw;
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 2rem 1rem;
    overflow-y: auto;
    background: linear-gradient(135deg, #4b83d8 0%, #6d5bba 50%, #8d58bf 100%);
  }

  .auth-card {
    width: 100%;
    max-width: 24rem;
    background-color: #ffffff;
    border-radius: 0.75rem;
    padding: 3rem 2.5rem 2.5rem;
    box-shadow: 0 18px 40px rgba(0, 0, 0, 0.18);
    display: flex;
    flex-direction: column;
  }

  .title {
    text-align: center;
    font-size: 2rem;
    font-weight: 700;
    color: #1f2937;
    margin-bottom: 2.25rem;
  }

  .form {
    display: flex;
    flex-direction: column;
    gap: 1.5rem;
  }

  .form__fieldset {
    width: 100%;
    display: flex;
    flex-direction: column;
    border: none;
  }

  .form__input {
    width: 100%;
    padding: 0.75rem 0.25rem;
    font-size: 0.95rem;
    font-family: inherit;
    color: #374151;
    background: transparent;
    border: none;
    border-bottom: 2px solid #d1d5db;
    outline: none;
    transition: border-color 0.25s ease;
  }

  .form__input::placeholder {
    color: #9ca3af;
  }

  .form__input:focus {
    border-bottom-color: #6d5bba;
  }

  .form__input--error {
    border-bottom-color: #e11d48;
  }

  .form__error {
    margin-top: 0.4rem;
    font-size: 0.78rem;
    color: #e11d48;
  }

  .form__btn {
    margin-top: 0.75rem;
    width: 100%;
    padding: 0.9rem 1rem;
    font-size: 1rem;
    font-weight: 500;
    font-family: inherit;
    color: #ffffff;
    text-align: center;
    border: none;
    border-radius: 0.25rem;
    background: linear-gradient(90deg, #4b83d8 0%, #8d58bf 100%);
    cursor: pointer;
    box-shadow: 0 6px 16px rgba(109, 91, 186, 0.35);
    transition:
      opacity 0.2s ease,
      transform 0.15s ease;
  }

  .form__btn:hover {
    opacity: 0.93;
  }

  .form__btn:active {
    transform: translateY(1px);
  }

  .auth-footer {
    margin-top: 2.5rem;
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 0.65rem;
    text-align: center;
  }

  .auth-footer__text {
    font-size: 0.85rem;
    color: #6b7280;
  }

  .auth-footer__link {
    color: #3b66b0;
    font-weight: 500;
    text-decoration: none;
    transition: color 0.2s ease;
  }

  .auth-footer__link:hover {
    color: #8d58bf;
    text-decoration: underline;
  }
</style>
