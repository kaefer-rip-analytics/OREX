import { api } from '../../services/api'

import type {
  AuthUser,
  LoginRequest,
  LoginResponse,
} from '../types/auth'

export async function login(
  request: LoginRequest,
): Promise<LoginResponse> {
  const response = await api.post<LoginResponse>(
    '/auth/login',
    request,
  )

  localStorage.setItem(
    'access_token',
    response.data.token,
  )

  const user: AuthUser = {
    userId: response.data.userId,
    nome: response.data.nome,
    email: response.data.email,
    roles: response.data.roles,
  }

  localStorage.setItem(
    'auth_user',
    JSON.stringify(user),
  )

  return response.data
}

export function logout(): void {
  localStorage.removeItem('access_token')
  localStorage.removeItem('auth_user')
}

export function getToken(): string | null {
  return localStorage.getItem('access_token')
}

export function isAuthenticated(): boolean {
  return Boolean(getToken())
}

export function getAuthenticatedUser(): AuthUser | null {
  const user = localStorage.getItem('auth_user')

  if (!user) {
    return null
  }

  try {
    return JSON.parse(user) as AuthUser
  } catch {
    return null
  }
}