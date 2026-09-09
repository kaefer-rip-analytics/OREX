export interface CreateUserRequest {
  nome: string
  email: string
  role: string
  ativo: boolean
  password: string
}

export interface UpdateUserRequest {
  nome: string
  email: string
  role: string
  ativo: boolean
}