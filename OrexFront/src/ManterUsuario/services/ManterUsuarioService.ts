import axios from 'axios'
import type { Usuario } from '../types/usuario'
import type { UpdateUserRequest,  CreateUserRequest } from '../types/usuarioRequest'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
})

export interface UsuarioFilters {
  nome?: string
  email?: string
  perfil?: string
  ativo?: boolean | ''
}

export async function listUsers(
  filtros: UsuarioFilters = {},
): Promise<Usuario[]> {
  const response = await api.get<Usuario[]>('/Usuario', {
    params: filtros,
  })

  return response.data
}

export async function createUsers(
  request: CreateUserRequest,
): Promise<Usuario> {
  const response = await api.post<Usuario>('/Usuario', request)

  return response.data
}

export async function updateUsers(
  id: number,
  request: UpdateUserRequest,
): Promise<Usuario> {
  const response = await api.put<Usuario>(
    `/Usuario/${id}`,
    request,
  )

  return response.data
}

export async function deactivatedUsers(id: number): Promise<void> {
  await api.delete(`/Usuario/${id}`)
}