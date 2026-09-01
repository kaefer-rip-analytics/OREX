export interface CriarUsuarioRequest {
  nome: string
  email: string
  perfil: string
  ativo: boolean
}

export interface AtualizarUsuarioRequest {
  nome: string
  email: string
  perfil: string
  ativo: boolean
}