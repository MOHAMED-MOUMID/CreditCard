<script setup>
import { ref, onMounted } from 'vue'
import api from '../services/api'
import { useAuthStore } from '../stores/auth'
import { useRouter } from 'vue-router'

// 🔹 Pinia store pour auth
const auth = useAuthStore()
const router = useRouter()

// État des cartes
const cards = ref([])
const newCardNumber = ref('')
const newCardHolder = ref('')
const newExpiration = ref('')

// Récupérer les cartes au montage
onMounted(async () => {
  try {
    const res = await api.get('/cards')
    cards.value = res.data
  } catch (error) {
    console.error(error)
  }
})

// Ajouter une carte
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

// Supprimer une carte
const deleteCard = async (id) => {
  try {
    await api.delete(`/cards/${id}`)
    cards.value = cards.value.filter(c => c.id !== id)
  } catch (err) {
    console.error(err)
  }
}

// 🔴 Fonction de logout
const handleLogout = () => {
  auth.logout()          // Supprime le token depuis Pinia
  router.push('/')       // Redirige vers la page login
}
</script>

<template>
  <div class="max-w-md mx-auto p-4">
    <h2 class="text-2xl font-bold mb-4">Mes cartes</h2>
    
    <ul class="mb-6 space-y-2">
      <li v-for="card in cards" :key="card.id" class="flex justify-between items-center bg-gray-100 p-2 rounded">
        <span>**** **** **** {{ card.cardNumber.slice(-4) }}</span>
        <button @click="deleteCard(card.id)" 
                class="bg-red-500 text-white px-2 py-1 rounded hover:bg-red-600 transition">
          Supprimer
        </button>
      </li>
    </ul>

    <h3 class="text-xl font-semibold mb-2">Ajouter une carte</h3>
    <div class="space-y-2 mb-4">
      <input v-model="newCardNumber" placeholder="Numéro de carte" 
             class="w-full border p-2 rounded"/>
      <input v-model="newCardHolder" placeholder="Nom du titulaire" 
             class="w-full border p-2 rounded"/>
      <input v-model="newExpiration" type="date" 
             class="w-full border p-2 rounded"/>
    </div>
    <button @click="addCard" 
            class="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600 transition mb-4">
      Ajouter
    </button>

    <!-- 🔴 Bouton de déconnexion -->
    <div>
      <button @click="handleLogout"
              class="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600 transition w-full">
        Déconnexion
      </button>
    </div>
  </div>
</template>
