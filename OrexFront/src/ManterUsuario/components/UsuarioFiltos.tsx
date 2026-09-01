import type { UsuarioFilters } from '../services/ManterUsuarioService'

interface Props {
  filtros: UsuarioFilters
  onChange: (filtros: UsuarioFilters) => void
  onNovo: () => void
}

export function UsuarioFilters({
  filtros,
  onChange,
  onNovo,
}: Props) {
  return (
    <div className="mb-6 rounded-lg bg-white p-4 shadow">
      <div className="grid grid-cols-1 gap-4 md:grid-cols-4">
        <input
          className="rounded border px-3 py-2"
          placeholder="Nome"
          value={filtros.nome ?? ''}
          onChange={(event) =>
            onChange({
              ...filtros,
              nome: event.target.value,
            })
          }
        />

        <input
          className="rounded border px-3 py-2"
          placeholder="E-mail"
          value={filtros.email ?? ''}
          onChange={(event) =>
            onChange({
              ...filtros,
              email: event.target.value,
            })
          }
        />

        <input
          className="rounded border px-3 py-2"
          placeholder="Perfil"
          value={filtros.perfil ?? ''}
          onChange={(event) =>
            onChange({
              ...filtros,
              perfil: event.target.value,
            })
          }
        />

        <select
          className="rounded border px-3 py-2"
          value={String(filtros.ativo ?? '')}
          onChange={(event) => {
            const valor = event.target.value

            onChange({
              ...filtros,
              ativo: valor === ''
                ? ''
                : valor === 'true',
            })
          }}
        >
          <option value="">Todos os status</option>
          <option value="true">Ativos</option>
          <option value="false">Inativos</option>
        </select>

        <button
          type="button"
          onClick={onNovo}
          className="rounded bg-blue-600 px-4 py-2 font-semibold text-white hover:bg-blue-700"
        >
          Novo usuário
        </button>
      </div>
    </div>
  )
}