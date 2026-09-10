import { useState } from 'react'

import { LoginPage } from './src/MantainAuth/pages/LoginPage'

import { MantainUserPage } from './src/MantainUser/pages/MantainUserPage'

import { isAuthenticated, logout } from './src/MantainAuth/services/AuthService'

function App() {
  const [
    autenticado,
    setAutenticado,
  ] = useState(isAuthenticated())

  function sair() {
    logout()
    setAutenticado(false)
  }

  if (!autenticado) {
    return (
      <LoginPage
        onLogin={() => setAutenticado(true)}
      />
    )
  }

  return (
    <MantainUserPage
    onLogout={sair}
    />
  )
}

export default App