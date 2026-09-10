import { zodResolver } from '@hookform/resolvers/zod'
import { useForm } from 'react-hook-form'

import {
  loginSchema,
  type LoginFormData,
} from '../schemas/loginSchema'

interface Props {
  carregando?: boolean
  erro?: string
  onSubmit: (
    dados: LoginFormData,
  ) => Promise<void>
}

export function LoginForm({
  carregando = false,
  erro = '',
  onSubmit,
}: Props) {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      email: '',
      password: '',
    },
  })

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="space-y-4"
    >
      {erro && (
        <div className="rounded bg-red-100 p-3 text-sm text-red-700">
          {erro}
        </div>
      )}

      <div>
        <label className="mb-1 block font-medium">
          E-mail
        </label>

        <input
          type="email"
          autoComplete="email"
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
          Senha
        </label>

        <input
          type="password"
          autoComplete="current-password"
          {...register('password')}
          className="w-full rounded border px-3 py-2"
        />

        {errors.password && (
          <p className="mt-1 text-sm text-red-600">
            {errors.password.message}
          </p>
        )}
      </div>

      <button
        type="submit"
        disabled={carregando}
        className="w-full rounded bg-blue-600 px-4 py-2 font-semibold text-white hover:bg-blue-700 disabled:opacity-50"
      >
        {carregando ? 'Entrando...' : 'Entrar'}
      </button>
    </form>
  )
}