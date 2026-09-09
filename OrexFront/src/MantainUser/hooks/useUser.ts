import { useCallback, useEffect, useState } from 'react'
import { listUsers, type UserFilters } from '../services/MantainUserService'
import type { User } from '../types/user'

export function useUser(filtros: UserFilters) {
  const [users, setUsers] = useState<User[]>([])
  const [carregando, setCarregando] = useState(false)
  const [erro, setErro] = useState('')

  const buscar = useCallback(async () => {
    try {
      setCarregando(true)
      setErro('')

      const resultado = await listUsers(filtros)

      setUsers(resultado)
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
    users,
    carregando,
    erro,
    recarregar: buscar,
  }
}