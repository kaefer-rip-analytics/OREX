export interface User {
  id: string
  nome: string
  email: string
  roles: string[]
  ativo: boolean
  dtCadastro: string
  dtAtualizacao?: string | null
}