import axios from 'axios'
import type { User } from '../types/user'
import type { UpdateUserRequest,  CreateUserRequest } from '../types/userRequest'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
})

export interface UserFilters {
  nome?: string
  email?: string
  role?: string
  ativo?: boolean | ''
}

export async function listUsers(
  filtros: UserFilters = {},
): Promise<User[]> {
  const response = await api.get<User[]>('/User', {
    params: filtros,
  })

  return response.data
}

export async function createUsers(
  request: CreateUserRequest,
): Promise<User> {
  const response = await api.post<User>('/User', request)

  return response.data
}

export async function updateUsers(
  id: string,
  request: UpdateUserRequest,
): Promise<User> {
  const response = await api.put<User>(
    `/User/${id}`,
    request,
  )

  return response.data
}

export async function deactivatedUsers(id: string): Promise<void> {
  await api.delete(`/User/${id}`)
}