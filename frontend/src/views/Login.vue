<template>
  <div class="auth-container">
    <h2>{{ isRegister ? 'Inscription' : 'Connexion' }}</h2>

    <!-- Champ nom (uniquement pour register) -->
    <input
      v-if="isRegister"
      v-model="name"
      placeholder="Nom complet"
    />

    <input
      v-model="email"
      placeholder="Email"
    />

    <input
      v-model="password"
      type="password"
      placeholder="Mot de passe"
    />

    <button @click="isRegister ? handleRegister() : handleLogin()">
      {{ isRegister ? 'Créer un compte' : 'Se connecter' }}
    </button>

    <p class="switch">
      <span v-if="!isRegister">
        Pas de compte ?
        <a @click="isRegister = true">S'inscrire</a>
      </span>

      <span v-else>
        Déjà un compte ?
        <a @click="isRegister = false">Se connecter</a>
      </span>
    </p>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useAuthStore } from '../stores/auth'
import { useRouter } from 'vue-router'

const auth = useAuthStore()
const router = useRouter()

const isRegister = ref(false)

const name = ref('')
const email = ref('')
const password = ref('')

// LOGIN
const handleLogin = async () => {
  try {
    await auth.login(email.value, password.value)
    router.push('/dashboard')
  } catch (err) {
    alert('Erreur de connexion')
    console.error(err)
  }
}

// REGISTER
const handleRegister = async () => {
  try {
    await auth.register({
      name: name.value,
      email: email.value,
      password: password.value
    })
    alert('Compte créé avec succès')
    isRegister.value = false
  } catch (err) {
    alert('Erreur lors de l’inscription')
    console.error(err)
  }
}
</script>

<style scoped>
.auth-container {
  width: 360px;
  margin: 100px auto;
  padding: 30px;
  background: white;
  border-radius: 10px;
  box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
  text-align: center;
  font-family: Arial, sans-serif;
}

.auth-container h2 {
  margin-bottom: 20px;
}

.auth-container input {
  width: 100%;
  padding: 12px;
  margin-bottom: 15px;
  border-radius: 6px;
  border: 1px solid #ccc;
}

.auth-container button {
  width: 100%;
  padding: 12px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 6px;
  font-weight: bold;
  cursor: pointer;
}

.auth-container button:hover {
  background-color: #0056b3;
}

.switch {
  margin-top: 15px;
  font-size: 14px;
}

.switch a {
  color: #007bff;
  cursor: pointer;
  font-weight: bold;
}
</style>
