<script setup>
import { ref, onMounted } from 'vue'
import api from '../services/api'

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
      expirationDate: newExpiration.value,
      cardHolderName: newCardHolder.value
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
</script>

<template>
  <div>
    <h2>Mes cartes</h2>
    <ul>
      <li v-for="card in cards" :key="card.id">
        **** **** **** {{ card.cardNumber.slice(-4) }}
        <button @click="deleteCard(card.id)">Supprimer</button>
      </li>
    </ul>

    <h3>Ajouter une carte</h3>
    <input v-model="newCardNumber" placeholder="Numéro de carte"/>
    <input v-model="newCardHolder" placeholder="Nom du titulaire"/>
    <input v-model="newExpiration" type="date"/>
    <button @click="addCard">Ajouter</button>
  </div>
</template>
