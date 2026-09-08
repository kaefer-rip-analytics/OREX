export interface CreateUserRequest {
  nome: string
  email: string
  perfil: string
  ativo: boolean
  password: string
}

export interface UpdateUserRequest {
  nome: string
  email: string
  perfil: string
  ativo: boolean
}