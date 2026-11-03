import { computed, ref } from "vue";
import { defineStore } from "pinia";

export interface AuthUser {
  uuid: string;
  name: string;
  email: string;
}

export interface LoginResponse {
  token: string;
  uuid: string;
  name: string;
  email: string;
}

const TOKEN_STORAGE_KEY = "auth_token";
const USER_STORAGE_KEY = "auth_user";

export const useAuthStore = defineStore("auth", () => {
  const token = ref<string | null>(localStorage.getItem(TOKEN_STORAGE_KEY));

  const storedUser = localStorage.getItem(USER_STORAGE_KEY);
  const user = ref<AuthUser | null>(storedUser ? JSON.parse(storedUser) : null);

  const isAuthenticated = computed(() => Boolean(token.value));

  function setSession(payload: LoginResponse) {
    token.value = payload.token;
    user.value = {
      uuid: payload.uuid,
      name: payload.name,
      email: payload.email,
    };
    localStorage.setItem(TOKEN_STORAGE_KEY, payload.token);
    localStorage.setItem(USER_STORAGE_KEY, JSON.stringify(user.value));
  }

  function clearSession() {
    token.value = null;
    user.value = null;
    localStorage.removeItem(TOKEN_STORAGE_KEY);
    localStorage.removeItem(USER_STORAGE_KEY);
  }

  return {
    token,
    user,
    isAuthenticated,
    setSession,
    clearSession,
  };
});
