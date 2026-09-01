import type { Usuario } from '../types/usuario'

interface Props {
  usuarios: Usuario[]
  onEditar: (usuario: Usuario) => void
  onInativar: (usuario: Usuario) => void
}

export function UsuarioTable({
  usuarios,
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
          {usuarios.map((usuario) => (
            <tr
              key={usuario.id}
              className="border-t hover:bg-slate-50"
            >
              <td className="px-4 py-3">{usuario.nome}</td>
              <td className="px-4 py-3">{usuario.email}</td>
              <td className="px-4 py-3">{usuario.perfil}</td>
              <td className="px-4 py-3">
                <span
                  className={
                    usuario.ativo
                      ? 'rounded bg-green-100 px-2 py-1 text-green-700'
                      : 'rounded bg-red-100 px-2 py-1 text-red-700'
                  }
                >
                  {usuario.ativo ? 'Ativo' : 'Inativo'}
                </span>
              </td>
              <td className="space-x-2 px-4 py-3 text-right">
                <button
                  type="button"
                  onClick={() => onEditar(usuario)}
                  className="rounded bg-yellow-500 px-3 py-1 text-white"
                >
                  Editar
                </button>

                {usuario.ativo && (
                  <button
                    type="button"
                    onClick={() => onInativar(usuario)}
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

      {usuarios.length === 0 && (
        <p className="p-6 text-center text-slate-500">
          Nenhum usuário encontrado.
        </p>
      )}
    </div>
  )
}