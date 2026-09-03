import { useMemo, useState } from 'react'
import { UsuarioFilters } from '../components/UsuarioFiltos'
import { UsuarioForm } from '../components/UsuarioForm'
import { UsuarioTable } from '../components/UsuarioTabela'
import { useUpdateUser } from '../hooks/useUpdateUser'
import { useCreateUser } from '../hooks/useCreateUser'
import { useDeactivatedUser } from '../hooks/useDeactivatedUser'
import { useUser } from '../hooks/useUser'
import type { UsuarioFilters as Filtros } from '../services/ManterUsuarioService'
import type { Usuario } from '../types/usuario'
import type { UsuarioFormData } from '../schemas/usuarioSchema'

export function ManterUsuarioPage() {
  const [filtros, setFiltros] = useState<Filtros>({
    nome: '',
    email: '',
    perfil: '',
    ativo: '',
  })

  const [usuarioSelecionado, setUsuarioSelecionado] =
    useState<Usuario | null>(null)

  const [mostrarFormulario, setMostrarFormulario] =
    useState(false)

  const filtrosMemoizados = useMemo(
    () => filtros,
    [filtros],
  )

  const {
    usuarios,
    carregando,
    erro,
    recarregar,
  } = useUser(filtrosMemoizados)

  const { criar, carregando: criando } =
    useCreateUser()

  const { atualizar, carregando: atualizando } =
    useUpdateUser()

  const { inativar } =
    useDeactivatedUser()

  async function salvar(dados: UsuarioFormData) {
    
    if (usuarioSelecionado) {
      await atualizar(usuarioSelecionado.id, dados)
    } else {
      await criar(dados)
    }

    setMostrarFormulario(false)
    setUsuarioSelecionado(null)

    await recarregar()
  }

  async function confirmarInativacao(usuario: Usuario) {
    const confirmar = window.confirm(
      `Deseja inativar o usuário ${usuario.nome}?`,
    )

    if (!confirmar) {
      return
    }

    await inativar(usuario.id)
    await recarregar()
  }

  function novoUsuario() {
    setUsuarioSelecionado(null)
    setMostrarFormulario(true)
  }

  function editarUsuario(usuario: Usuario) {
    setUsuarioSelecionado(usuario)
    setMostrarFormulario(true)
  }

  return (
    <main className="min-h-screen p-6">
      <div className="mx-auto max-w-7xl">
        <h1 className="mb-6 text-3xl font-bold text-slate-800">
          Manter usuários
        </h1>

        <UsuarioFilters
          filtros={filtros}
          onChange={setFiltros}
          onNovo={novoUsuario}
        />

        {mostrarFormulario && (
          <div className="mb-6">
            <UsuarioForm
              usuario={usuarioSelecionado}
              carregando={criando || atualizando}
              onSalvar={salvar}
              onCancelar={() => {
                setMostrarFormulario(false)
                setUsuarioSelecionado(null)
              }}
            />
          </div>
        )}

        {erro && (
          <div className="mb-4 rounded bg-red-100 p-4 text-red-700">
            {erro}
          </div>
        )}

        {carregando ? (
          <p className="text-slate-600">
            Carregando usuários...
          </p>
        ) : (
          <UsuarioTable
            usuarios={usuarios}
            onEditar={editarUsuario}
            onInativar={confirmarInativacao}
          />
        )}
      </div>
    </main>
  )
}