import { useLogin } from '../hooks/useLogin'

import { LoginForm } from '../components/LoginForm'

import type { LoginFormData } from '../schemas/loginSchema'

interface Props {
  onLogin: () => void
}

export function LoginPage({
  onLogin,
}: Props) {
  const {
    login,
    carregando,
    erro,
  } = useLogin()

  async function enviar(
    dados: LoginFormData,
  ) {
    await login(dados)
    onLogin()
  }

  return (
    <main className="flex min-h-screen items-center justify-center bg-slate-100 p-6">
      <section className="w-full max-w-md rounded-xl bg-white p-8 shadow-lg">
        <h1 className="mb-2 text-2xl font-bold text-slate-800">
          OREX
        </h1>

        <p className="mb-6 text-slate-600">
          Acesse sua conta
        </p>

        <LoginForm
          carregando={carregando}
          erro={erro}
          onSubmit={enviar}
        />
      </section>
    </main>
  )
}