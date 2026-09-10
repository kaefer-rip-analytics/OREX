import { useMemo, useState } from 'react'
import { UserFilters } from '../components/UserFilters'
import { UserForm } from '../components/UserForm'
import { UserTable } from '../components/UserTable'
import { useUpdateUser } from '../hooks/useUpdateUser'
import { useCreateUser } from '../hooks/useCreateUser'
import { useDeactivatedUser } from '../hooks/useDeactivatedUser'
import { useUser } from '../hooks/useUser'
import type { UserFilters as Filtros } from '../services/MantainUserService'
import type { User } from '../types/user'
import type { UserFormData } from '../schemas/userSchema'

interface Props {
  onLogout: () => void
}

export function MantainUserPage({ onLogout }: Props) {
  const [filtros, setFiltros] = useState<Filtros>({
    nome: '',
    email: '',
    role: '',
    ativo: ''
  })

  const [userSelecionado, setUserSelecionado] = useState<User | null>(null)

  const [mostrarFormulario, setMostrarFormulario] = useState(false)

  const filtrosMemoizados = useMemo(() => filtros, [filtros])

  const { users, carregando, erro, recarregar, } = useUser(filtrosMemoizados)

  const { criar, carregando: criando } = useCreateUser()

  const { atualizar, carregando: atualizando } = useUpdateUser()

  const { inativar } = useDeactivatedUser()

  async function salvar(dados: UserFormData) {
    
    if (userSelecionado) {
      await atualizar(userSelecionado.id, dados)
    } else {
      await criar({
        nome: dados.nome,
        email: dados.email,
        role: dados.role,
        ativo: dados.ativo,
        password: dados.password
      })
    }

    setMostrarFormulario(false)
    setUserSelecionado(null)

    await recarregar()
  }

  async function confirmarInativacao(user: User) {
    const confirmar = window.confirm(
      `Deseja inativar o usuário ${user.nome}?`,
    )

    if (!confirmar) {
      return
    }

    await inativar(user.id)
    await recarregar()
  }

  function novoUser() {
    setUserSelecionado(null)
    setMostrarFormulario(true)
  }

  function editarUser(user: User) {
    setUserSelecionado(user)
    setMostrarFormulario(true)
  }

  return (
    <main className="min-h-screen p-6">
      <div className="mx-auto max-w-7xl">
        <header className="mb-6 flex items-center justify-between">
          <h1 className="mb-6 text-3xl font-bold text-slate-800">
            Manter usuários
          </h1>

          <button
            type="button"
            onClick={onLogout}
            className="rounded bg-red-600 px-4 py-2 font-semibold text-white hover:bg-red-700"
          >
            Sair
          </button>
        </header>

          <UserFilters
            filtros={filtros}
            onChange={setFiltros}
            onNovo={novoUser}
          />
          
        {mostrarFormulario && (
          <div className="mb-6">
            <UserForm
              user={userSelecionado}
              carregando={criando || atualizando}
              onSalvar={salvar}
              onCancelar={() => {
                setMostrarFormulario(false)
                setUserSelecionado(null)
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
          <UserTable
            users={users}
            onEditar={editarUser}
            onInativar={confirmarInativacao}
          />
        )}
      </div>
    </main>
  )
}