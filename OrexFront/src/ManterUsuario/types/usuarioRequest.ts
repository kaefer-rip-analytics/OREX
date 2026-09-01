export interface CreateUserRequest {
  nome: string
  email: string
  perfil: string
  ativo: boolean
}

export interface UpdateUserRequest {
  nome: string
  email: string
  perfil: string
  ativo: boolean
}