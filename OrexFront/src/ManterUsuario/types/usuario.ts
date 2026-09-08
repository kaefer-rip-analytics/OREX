export interface Usuario {
  id: string
  nome: string
  email: string
  perfil: string
  ativo: boolean
  dtCadastro: string
  dtAtualizacao?: string | null
}