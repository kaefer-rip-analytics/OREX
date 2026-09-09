export interface User {
  id: string
  nome: string
  email: string
  role: string
  ativo: boolean
  dtCadastro: string
  dtAtualizacao?: string | null
}