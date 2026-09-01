import { z } from 'zod'

export const usuarioSchema = z.object({
  nome: z
    .string()
    .min(3, 'Informe o nome completo')
    .max(150, 'O nome deve possuir no máximo 150 caracteres'),

  email: z
    .string()
    .email('Informe um e-mail válido'),

  perfil: z
    .string()
    .min(1, 'Selecione um perfil'),

  ativo: z.boolean(),
})

export type UsuarioFormData = z.infer<typeof usuarioSchema>