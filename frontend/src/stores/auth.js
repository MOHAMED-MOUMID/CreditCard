import { defineStore } from 'pinia'
import axios from 'axios'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || null
  }),
  actions: {
    async login(email, password) {
      // ⚡ CORRECTION: envoyer { email, password }
      const res = await axios.post('http://localhost:5213/api/auth/login', {
        email,
        password
      })

      this.token = res.data.token
      localStorage.setItem('token', this.token)
    },
    logout() {
      this.token = null
      localStorage.removeItem('token')
    }
  }
})
