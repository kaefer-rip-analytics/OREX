import { useState } from 'react'
import { createUsers } from '../services/ManterUsuarioService'
import type { CreateUserRequest } from '../types/usuarioRequest'

export function useCreateUser() {
  const [carregando, setCarregando] = useState(false)

  async function executar(request: CreateUserRequest) {
    try {
      setCarregando(true)

      return await createUsers(request)
    } finally {
      setCarregando(false)
    }
  }

  return {
    criar: executar,
    carregando,
  }
}