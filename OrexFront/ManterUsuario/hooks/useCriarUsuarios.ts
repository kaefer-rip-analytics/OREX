import { useState } from 'react'
import { criarUsuario } from '../services/ManterUsuarioService'
import type { CriarUsuarioRequest } from '../types/usuarioRequest'

export function useCriarUsuarios() {
  const [carregando, setCarregando] = useState(false)

  async function executar(request: CriarUsuarioRequest) {
    try {
      setCarregando(true)

      return await criarUsuario(request)
    } finally {
      setCarregando(false)
    }
  }

  return {
    criar: executar,
    carregando,
  }
}