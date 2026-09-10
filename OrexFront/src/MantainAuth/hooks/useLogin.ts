import { useState } from 'react'

import {
  login,
} from '../services/AuthService'

import type {
  LoginRequest,
} from '../types/auth'

export function useLogin() {
  const [carregando, setCarregando] =
    useState(false)

  const [erro, setErro] =
    useState('')

  async function executar(
    request: LoginRequest,
  ) {
    try {
      setCarregando(true)
      setErro('')

      return await login(request)
    } catch (error: any) {
      const mensagem =
        error.response?.data?.mensagem ??
        'E-mail ou senha inválidos.'

      setErro(mensagem)

      throw error
    } finally {
      setCarregando(false)
    }
  }

  return {
    login: executar,
    carregando,
    erro,
  }
}