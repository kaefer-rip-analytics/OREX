import { useState } from 'react'
import { inativarUsuario } from '../services/ManterUsuarioService'

export function useInativarUsuarios() {
  const [carregando, setCarregando] = useState(false)

  async function executar(id: number) {
    try {
      setCarregando(true)

      await inativarUsuario(id)
    } finally {
      setCarregando(false)
    }
  }

  return {
    inativar: executar,
    carregando,
  }
}