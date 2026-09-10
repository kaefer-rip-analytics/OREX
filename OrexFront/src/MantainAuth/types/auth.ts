export interface LoginRequest {
  email: string
  password: string
}

export interface LoginResponse {
  token: string
  userId: string
  nome: string
  email: string
  roles: string[]
}

export interface AuthUser {
  userId: string
  nome: string
  email: string
  roles: string[]
}