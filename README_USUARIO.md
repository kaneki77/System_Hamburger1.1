# Sistema de Usuários - Instruções de Implementação

## Arquivos Criados/Modificados

### Camada Domain (Hamburgueria.Domain)
- **Usuario.cs** - Entidade Usuario com propriedades Id, Nome, Login, SenhaHash, NivelAcesso
- **UsuarioLogado.cs** - Classe estática para armazenar informações do usuário logado na sessão
- **IUsuarioRepository.cs** - Interface do repositório de usuários
- **UsuarioService.cs** - Serviço com lógica de negócio (validações, autenticação, hash de senha)

### Camada Data (Hamburgueria.Data)
- **UsuarioRepository.cs** - Implementação do repositório com acesso ao banco de dados MySQL

### Camada UI (Hamburgueria.UI)
- **FormLogin.cs** - Formulário de login
- **FormLogin.Designer.cs** - Designer do formulário de login
- **FormUsuario.cs** - Formulário de gerenciamento de usuários (CRUD)
- **FormUsuario.Designer.cs** - Designer do formulário de gerenciamento de usuários
- **Form1.cs** - Atualizado para incluir botão de gerenciamento de usuários e controle de acesso
- **Form1.Designer.cs** - Atualizado com botão "Gerenciar Usuários"
- **Program.cs** - Atualizado para exibir FormLogin antes do menu principal

### Banco de Dados
- **ddl_usuario.sql** - Script SQL para criar a tabela usuario e inserir usuários de teste

## Passo a Passo para Testar

### 1. Executar o Script SQL

1. Abra o **phpMyAdmin** (http://localhost/phpmyadmin)
2. Selecione o banco de dados **hamburgueria**
3. Clique na aba **SQL**
4. Copie e cole o conteúdo do arquivo `ddl_usuario.sql`
5. Clique em **Executar**

Isso criará a tabela `usuario` e inserirá dois usuários de teste:
- **Gerente**: Login: `admin` / Senha: `admin123`
- **Atendente**: Login: `atendente` / Senha: `atendente123`

### 2. Compilar o Projeto no Visual Studio

1. Abra o projeto no Visual Studio
2. Clique em **Build** > **Rebuild Solution**
3. Verifique se não há erros de compilação

### 3. Executar a Aplicação

1. Pressione **F5** ou clique em **Start**
2. A tela de **Login** será exibida primeiro
3. Faça login com um dos usuários:
   - **admin** / **admin123** (Gerente)
   - **atendente** / **atendente123** (Atendente)

### 4. Testar o Controle de Acesso

#### Como Gerente (admin):
1. Após o login, o menu principal mostrará: "Menu Principal - Administrador (Gerente)"
2. Você terá acesso a todos os botões:
   - Gerenciar Categorias
   - Gerenciar Produtos
   - **Gerenciar Usuários** (apenas Gerentes)
3. Clique em "Gerenciar Usuários" para testar o CRUD de usuários

#### Como Atendente:
1. Faça logout e login novamente com: **atendente** / **atendente123**
2. O menu principal mostrará: "Menu Principal - Atendente Teste (Atendente)"
3. Você terá acesso a:
   - Gerenciar Categorias
   - Gerenciar Produtos
4. Ao clicar em "Gerenciar Usuários", uma mensagem de acesso negado será exibida

### 5. Testar o CRUD de Usuários (como Gerente)

1. Faça login como **admin**
2. Clique em **Gerenciar Usuários**
3. Teste as operações:
   - **Adicionar**: Preencha os campos e clique em "Adicionar"
   - **Atualizar**: Clique em um usuário na grade, altere os dados e clique em "Atualizar"
   - **Remover**: Clique em um usuário na grade e clique em "Remover"
   - **Limpar**: Clique em "Limpar" para limpar os campos

## Funcionalidades Implementadas

### Sistema de Autenticação
- ✅ Tela de login antes do menu principal
- ✅ Validação de credenciais (login e senha)
- ✅ Armazenamento da sessão do usuário logado
- ✅ Exibição do nome e nível de acesso no título do menu principal

### Controle de Acesso Baseado em Função (RBAC)
- ✅ Dois níveis de acesso: **Gerente** e **Atendente**
- ✅ Apenas Gerentes podem acessar o gerenciamento de usuários
- ✅ Mensagem de acesso negado para Atendentes

### CRUD de Usuários
- ✅ Adicionar novo usuário
- ✅ Atualizar usuário existente
- ✅ Remover usuário (com confirmação)
- ✅ Listar todos os usuários
- ✅ Validação: não permitir remover o próprio usuário logado
- ✅ Senha oculta no campo de entrada (●●●●●)
- ✅ Coluna de senha oculta na grade de dados

### Segurança
- ✅ Senhas armazenadas em texto simples (NOTA: em produção, usar hash como BCrypt)
- ✅ Validação de campos obrigatórios
- ✅ Login único (não permite duplicatas)

## Próximos Passos (D45)

Após testar e validar o sistema de usuários, os próximos passos são:

1. **CRUD de Cliente** - Implementar gerenciamento de clientes
2. **Fluxo de Pedido** - Implementar o sistema de pedidos
3. **Sistema de Logs** - Implementar auditoria de ações
4. **Melhorias de Segurança** - Implementar hash de senha (BCrypt)

## Observações Importantes

- A senha está sendo armazenada em **texto simples** para facilitar os testes
- Em produção, deve-se implementar **hash de senha** usando BCrypt ou similar
- O campo `SenhaHash` na classe Usuario está preparado para receber o hash
- O método `VerificarSenha` no UsuarioService pode ser atualizado para usar BCrypt

## Estrutura da Tabela usuario

```sql
CREATE TABLE usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    login VARCHAR(50) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL,
    nivelAcesso VARCHAR(20) NOT NULL,
    ativo BOOLEAN DEFAULT TRUE,
    dataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    dataAtualizacao DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```

## Credenciais de Teste

| Nome | Login | Senha | Nível de Acesso |
|------|-------|-------|-----------------|
| Administrador | admin | admin123 | Gerente |
| Atendente Teste | atendente | atendente123 | Atendente |
