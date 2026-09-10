import type { User } from '../types/user'

interface Props {
  users: User[]
  onEditar: (user: User) => void
  onInativar: (user: User) => void
}

export function UserTable({
  users,
  onEditar,
  onInativar,
}: Props) {
  return (
    <div className="overflow-x-auto rounded-lg bg-white shadow">
      <table className="min-w-full">
        <thead className="bg-slate-100">
          <tr>
            <th className="px-4 py-3 text-left">Nome</th>
            <th className="px-4 py-3 text-left">E-mail</th>
            <th className="px-4 py-3 text-left">Perfil</th>
            <th className="px-4 py-3 text-left">Status</th>
            <th className="px-4 py-3 text-right">Ações</th>
          </tr>
        </thead>

        <tbody>
          {users.map((user) => (
            <tr
              key={user.id}
              className="border-t hover:bg-slate-50"
            >
              <td className="px-4 py-3">{user.nome}</td>
              <td className="px-4 py-3">{user.email}</td>
              <td className="px-4 py-3">{user.roles}</td>
              <td className="px-4 py-3">
                <span
                  className={
                    user.ativo
                      ? 'rounded bg-green-100 px-2 py-1 text-green-700'
                      : 'rounded bg-red-100 px-2 py-1 text-red-700'
                  }
                >
                  {user.ativo ? 'Ativo' : 'Inativo'}
                </span>
              </td>
              <td className="space-x-2 px-4 py-3 text-right">
                <button
                  type="button"
                  onClick={() => onEditar(user)}
                  className="rounded bg-yellow-500 px-3 py-1 text-white"
                >
                  Editar
                </button>

                {user.ativo && (
                  <button
                    type="button"
                    onClick={() => onInativar(user)}
                    className="rounded bg-red-600 px-3 py-1 text-white"
                  >
                    Inativar
                  </button>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {users.length === 0 && (
        <p className="p-6 text-center text-slate-500">
          Nenhum usuário encontrado.
        </p>
      )}
    </div>
  )
}