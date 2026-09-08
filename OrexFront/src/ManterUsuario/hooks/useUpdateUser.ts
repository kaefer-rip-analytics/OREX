import { useState } from 'react'
import { updateUsers } from '../services/ManterUsuarioService'
import type { UpdateUserRequest } from '../types/usuarioRequest'

export function useUpdateUser() {
  const [carregando, setCarregando] = useState(false)

  async function executar(
    id: string,
    request: UpdateUserRequest,
  ) {
    try {
      setCarregando(true)

      return await updateUsers(id, request)
    } finally {
      setCarregando(false)
    }
  }

  return {
    atualizar: executar,
    carregando,
  }
}