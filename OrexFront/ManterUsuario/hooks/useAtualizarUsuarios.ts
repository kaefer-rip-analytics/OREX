import { useState } from 'react'
import { atualizarUsuario } from '../services/ManterUsuarioService'
import type { AtualizarUsuarioRequest } from '../types/usuarioRequest'

export function useAtualizarUsuarios() {
  const [carregando, setCarregando] = useState(false)

  async function executar(
    id: number,
    request: AtualizarUsuarioRequest,
  ) {
    try {
      setCarregando(true)

      return await atualizarUsuario(id, request)
    } finally {
      setCarregando(false)
    }
  }

  return {
    atualizar: executar,
    carregando,
  }
}