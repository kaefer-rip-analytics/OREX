import { useState } from 'react'
import { deactivatedUsers } from '../services/ManterUsuarioService'

export function useDeactivatedUser() {
  const [carregando, setCarregando] = useState(false)

  async function executar(id: number) {
    try {
      setCarregando(true)

      await deactivatedUsers(id)
    } finally {
      setCarregando(false)
    }
  }

  return {
    inativar: executar,
    carregando,
  }
}