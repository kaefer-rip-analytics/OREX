import { useCallback, useEffect, useState } from 'react'
import { listUsers, type UsuarioFilters } from '../services/ManterUsuarioService'
import type { Usuario } from '../types/usuario'

export function useUser(filtros: UsuarioFilters) {
  const [usuarios, setUsuarios] = useState<Usuario[]>([])
  const [carregando, setCarregando] = useState(false)
  const [erro, setErro] = useState('')

  const buscar = useCallback(async () => {
    try {
      setCarregando(true)
      setErro('')

      const resultado = await listUsers(filtros)

      setUsuarios(resultado)
    } catch {
      setErro('Não foi possível carregar os usuários.')
    } finally {
      setCarregando(false)
    }
  }, [filtros])

  useEffect(() => {
    buscar()
  }, [buscar])

  return {
    usuarios,
    carregando,
    erro,
    recarregar: buscar,
  }
}