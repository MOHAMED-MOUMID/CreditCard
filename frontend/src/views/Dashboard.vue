<template>
  <div class="container">
    <h2>Mes cartes</h2>

    <ul class="card-list">
      <li v-for="card in cards" :key="card.id" class="card-item">
        <span>**** **** **** {{ card.cardNumber.slice(-4) }}</span>
        <button class="btn-danger" @click="deleteCard(card.id)">
          Supprimer
        </button>
      </li>
    </ul>

    <h3>Ajouter une carte</h3>

    <div class="form">
      <input v-model="newCardNumber" placeholder="Numéro de carte" />
      <input v-model="newCardHolder" placeholder="Nom du titulaire" />
      <input v-model="newExpiration" type="date" />
    </div>

    <button class="btn-primary" @click="addCard">
      Ajouter
    </button>

    <button class="btn-logout" @click="handleLogout">
      Déconnexion
    </button>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import api from '../services/api'
import { useAuthStore } from '../stores/auth'
import { useRouter } from 'vue-router'

const auth = useAuthStore()
const router = useRouter()

const cards = ref([])
const newCardNumber = ref('')
const newCardHolder = ref('')
const newExpiration = ref('')

onMounted(async () => {
  try {
    const res = await api.get('/cards')
    cards.value = res.data
  } catch (error) {
    console.error(error)
  }
})

const addCard = async () => {
  try {
    const res = await api.post('/cards', {
      cardNumber: newCardNumber.value,
      cardHolderName: newCardHolder.value,
      expirationDate: newExpiration.value
    })
    cards.value.push(res.data)
    newCardNumber.value = ''
    newCardHolder.value = ''
    newExpiration.value = ''
  } catch (err) {
    console.error(err)
  }
}

const deleteCard = async (id) => {
  try {
    await api.delete(`/cards/${id}`)
    cards.value = cards.value.filter(c => c.id !== id)
  } catch (err) {
    console.error(err)
  }
}

const handleLogout = () => {
  auth.logout()
  router.push('/')
}
</script>

<style scoped>
.container {
  max-width: 420px;
  margin: 40px auto;
  padding: 25px;
  background: #ffffff;
  border-radius: 10px;
  box-shadow: 0 8px 20px rgba(0,0,0,0.1);
  font-family: Arial, sans-serif;
}

h2, h3 {
  text-align: center;
  margin-bottom: 15px;
}

.card-list {
  list-style: none;
  padding: 0;
}

.card-item {
  display: flex;
  justify-content: space-between;
  background: #f4f6f8;
  padding: 10px;
  border-radius: 6px;
  margin-bottom: 8px;
}

.form input {
  width: 100%;
  padding: 10px;
  margin-bottom: 10px;
  border-radius: 6px;
  border: 1px solid #ccc;
}

button {
  width: 100%;
  padding: 10px;
  border-radius: 6px;
  border: none;
  cursor: pointer;
}

.btn-primary {
  background: #007bff;
  color: white;
  margin-bottom: 10px;
}

.btn-danger {
  background: #dc3545;
  color: white;
  width: auto;
}

.btn-logout {
  background: #333;
  color: white;
}
</style>
