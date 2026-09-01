import axios from 'axios'
import type { Usuario } from '../types/usuario'
import type { AtualizarUsuarioRequest,  CriarUsuarioRequest } from '../types/usuarioRequest'

const api = axios.create({
  baseURL: 'https://subtle-intl-tion-definitions.trycloudflare.com/api',
})

export interface UsuarioFilters {
  nome?: string
  email?: string
  perfil?: string
  ativo?: boolean | ''
}

export async function listarUsuarios(
  filtros: UsuarioFilters = {},
): Promise<Usuario[]> {
  const response = await api.get<Usuario[]>('/Usuario', {
    params: filtros,
  })

  return response.data
}

export async function criarUsuario(
  request: CriarUsuarioRequest,
): Promise<Usuario> {
  const response = await api.post<Usuario>('/Usuario', request)

  return response.data
}

export async function atualizarUsuario(
  id: number,
  request: AtualizarUsuarioRequest,
): Promise<Usuario> {
  const response = await api.put<Usuario>(
    `/Usuario/${id}`,
    request,
  )

  return response.data
}

export async function inativarUsuario(id: number): Promise<void> {
  await api.delete(`/Usuario/${id}`)
}