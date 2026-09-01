import { zodResolver } from '@hookform/resolvers/zod'
import { useEffect } from 'react'
import { Controller, useForm } from 'react-hook-form'
import { usuarioSchema, type UsuarioFormData } from '../schemas/usuarioSchema'
import { perfis } from '../types/perfil'
import type { Usuario } from '../types/usuario'

interface Props {
  usuario?: Usuario | null
  carregando?: boolean
  onSalvar: (dados: UsuarioFormData) => Promise<void>
  onCancelar: () => void
}

export function UsuarioForm({
  usuario,
  carregando = false,
  onSalvar,
  onCancelar,
}: Props) {
  const {
    register,
    control,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<UsuarioFormData>({
    resolver: zodResolver(usuarioSchema),
    defaultValues: {
      nome: '',
      email: '',
      perfil: '',
      ativo: true,
    },
  })

  useEffect(() => {
    reset({
      nome: usuario?.nome ?? '',
      email: usuario?.email ?? '',
      perfil: usuario?.perfil ?? '',
      ativo: usuario?.ativo ?? true,
    })
  }, [usuario, reset])

  return (
    <form
      onSubmit={handleSubmit(onSalvar)}
      className="space-y-4 rounded-lg bg-white p-6 shadow"
    >
      <h2 className="text-xl font-bold">
        {usuario ? 'Editar usuário' : 'Novo usuário'}
      </h2>

      <div>
        <label className="mb-1 block font-medium">
          Nome
        </label>

        <input
          {...register('nome')}
          className="w-full rounded border px-3 py-2"
        />

        {errors.nome && (
          <p className="mt-1 text-sm text-red-600">
            {errors.nome.message}
          </p>
        )}
      </div>

      <div>
        <label className="mb-1 block font-medium">
          E-mail
        </label>

        <input
          type="email"
          {...register('email')}
          className="w-full rounded border px-3 py-2"
        />

        {errors.email && (
          <p className="mt-1 text-sm text-red-600">
            {errors.email.message}
          </p>
        )}
      </div>

      <div>
        <label className="mb-1 block font-medium">
          Perfil
        </label>

        <select
          {...register('perfil')}
          className="w-full rounded border px-3 py-2"
        >
          <option value="">Selecione</option>

          {perfis.map((perfil) => (
            <option key={perfil} value={perfil}>
              {perfil}
            </option>
          ))}
        </select>

        {errors.perfil && (
          <p className="mt-1 text-sm text-red-600">
            {errors.perfil.message}
          </p>
        )}
      </div>

      <div>
        <label className="mb-1 block font-medium">
          Status
        </label>

        <Controller
          name="ativo"
          control={control}
          render={({ field }) => (
            <select
              value={field.value ? 'true' : 'false'}
              onChange={(event) => {
                field.onChange(event.target.value === 'true')
              }}
              onBlur={field.onBlur}
              className="w-full rounded border px-3 py-2"
            >
              <option value="true">Ativo</option>
              <option value="false">Inativo</option>
            </select>
          )}
        />

        {errors.ativo && (
          <p className="mt-1 text-sm text-red-600">
            {errors.ativo.message}
          </p>
        )}
      </div>

      <div className="flex justify-end gap-3">
        <button
          type="button"
          onClick={onCancelar}
          className="rounded border px-4 py-2"
        >
          Cancelar
        </button>

        <button
          disabled={carregando}
          type="submit"
          className="rounded bg-blue-600 px-4 py-2 font-semibold text-white disabled:opacity-50"
        >
          {carregando ? 'Salvando...' : 'Salvar'}
        </button>
      </div>
    </form>
  )
}