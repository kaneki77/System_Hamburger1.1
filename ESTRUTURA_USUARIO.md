# Estrutura de Arquivos - Módulo de Usuários

## Visão Geral da Arquitetura em 3 Camadas

```
System_Hamburger1.1/
│
├── Hamburgueria.Domain/          (Camada de Domínio - Entidades e Lógica de Negócio)
│   ├── Usuario.cs                ✅ Entidade Usuario
│   ├── UsuarioLogado.cs          ✅ Classe estática para sessão do usuário
│   ├── IUsuarioRepository.cs     ✅ Interface do repositório
│   └── UsuarioService.cs         ✅ Serviço com validações e autenticação
│
├── Hamburgueria.Data/            (Camada de Dados - Acesso ao Banco de Dados)
│   └── UsuarioRepository.cs      ✅ Implementação do repositório MySQL
│
├── Hamburgueria.UI/              (Camada de Apresentação - Interface Gráfica)
│   ├── FormLogin.cs              ✅ Formulário de login
│   ├── FormLogin.Designer.cs     ✅ Designer do formulário de login
│   ├── FormUsuario.cs            ✅ Formulário de gerenciamento de usuários
│   ├── FormUsuario.Designer.cs   ✅ Designer do formulário de gerenciamento
│   ├── Form1.cs                  ✅ Menu principal (atualizado)
│   ├── Form1.Designer.cs         ✅ Designer do menu principal (atualizado)
│   └── Program.cs                ✅ Ponto de entrada (atualizado com login)
│
├── ddl_usuario.sql               ✅ Script SQL para criar tabela e dados de teste
├── README_USUARIO.md             ✅ Instruções de implementação e teste
└── ESTRUTURA_USUARIO.md          ✅ Este arquivo (estrutura visual)
```

## Fluxo de Execução da Aplicação

```
1. Program.cs (Main)
   │
   ├─→ Inicializa Serviços (DI)
   │   ├── ClienteService
   │   ├── CategoriaService
   │   ├── ProdutoCardapioService
   │   └── UsuarioService ✅ NOVO
   │
   ├─→ Exibe FormLogin ✅ NOVO
   │   │
   │   ├─→ Usuário digita login e senha
   │   ├─→ UsuarioService.Autenticar()
   │   ├─→ UsuarioLogado.Definir() (armazena sessão)
   │   └─→ DialogResult.OK se sucesso
   │
   └─→ Exibe Form1 (Menu Principal)
       │
       ├─→ Título mostra: "Menu Principal - [Nome] ([Nível])"
       │
       └─→ Botões disponíveis:
           ├── Gerenciar Categorias (todos)
           ├── Gerenciar Produtos (todos)
           └── Gerenciar Usuários ✅ NOVO (apenas Gerente)
```

## Fluxo de Dados - Autenticação

```
FormLogin
   │
   │ (1) Usuário digita login e senha
   │
   ├─→ UsuarioService.Autenticar(login, senha)
   │      │
   │      ├─→ UsuarioRepository.GetByLogin(login)
   │      │      │
   │      │      └─→ MySQL: SELECT * FROM usuario WHERE login = ?
   │      │
   │      ├─→ Verifica se usuário existe
   │      ├─→ Compara senha
   │      └─→ Retorna Usuario ou null
   │
   ├─→ Se autenticação OK:
   │      │
   │      ├─→ UsuarioLogado.Definir(usuario)
   │      │      │
   │      │      └─→ Armazena: Id, Nome, Login, NivelAcesso
   │      │
   │      └─→ DialogResult = OK
   │
   └─→ Se autenticação FALHOU:
          │
          └─→ MessageBox "Credenciais inválidas"
```

## Fluxo de Dados - CRUD de Usuários

```
FormUsuario
   │
   ├─→ (1) LISTAR TODOS
   │      │
   │      ├─→ UsuarioService.GetAll()
   │      │      │
   │      │      └─→ UsuarioRepository.GetAll()
   │      │             │
   │      │             └─→ MySQL: SELECT * FROM usuario
   │      │
   │      └─→ DataGridView exibe lista
   │
   ├─→ (2) ADICIONAR
   │      │
   │      ├─→ UsuarioService.Adicionar(nome, login, senha, nivelAcesso)
   │      │      │
   │      │      ├─→ Validações:
   │      │      │   ├── Nome não vazio
   │      │      │   ├── Login não vazio e único
   │      │      │   └── Senha não vazia
   │      │      │
   │      │      └─→ UsuarioRepository.Add(usuario)
   │      │             │
   │      │             └─→ MySQL: INSERT INTO usuario (...)
   │      │
   │      └─→ Recarrega lista
   │
   ├─→ (3) ATUALIZAR
   │      │
   │      ├─→ UsuarioService.Atualizar(id, nome, login, novaSenha, nivelAcesso)
   │      │      │
   │      │      ├─→ Validações similares ao Adicionar
   │      │      │
   │      │      └─→ UsuarioRepository.Update(usuario)
   │      │             │
   │      │             └─→ MySQL: UPDATE usuario SET ... WHERE id = ?
   │      │
   │      └─→ Recarrega lista
   │
   └─→ (4) REMOVER
          │
          ├─→ Verifica se não é o próprio usuário logado
          │
          ├─→ Confirmação do usuário
          │
          ├─→ UsuarioService.Remover(id)
          │      │
          │      └─→ UsuarioRepository.Delete(id)
          │             │
          │             └─→ MySQL: DELETE FROM usuario WHERE id = ?
          │
          └─→ Recarrega lista
```

## Controle de Acesso (RBAC)

```
┌─────────────────────────────────────────────────────────┐
│                    NÍVEIS DE ACESSO                     │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  ┌─────────────────┐         ┌─────────────────┐       │
│  │    GERENTE      │         │   ATENDENTE     │       │
│  └─────────────────┘         └─────────────────┘       │
│          │                            │                 │
│          │                            │                 │
│  ┌───────▼────────────────────────────▼──────────┐     │
│  │  Gerenciar Categorias              ✅ ✅      │     │
│  │  Gerenciar Produtos                ✅ ✅      │     │
│  │  Gerenciar Usuários                ✅ ❌      │     │
│  │  Gerenciar Clientes (futuro)       ✅ ✅      │     │
│  │  Criar Pedidos (futuro)            ✅ ✅      │     │
│  │  Visualizar Relatórios (futuro)    ✅ ❌      │     │
│  └────────────────────────────────────────────────┘     │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

## Validações Implementadas

### FormLogin
- ✅ Login não pode estar vazio
- ✅ Senha não pode estar vazia
- ✅ Credenciais devem existir no banco de dados
- ✅ Senha deve corresponder ao login

### FormUsuario (Adicionar/Atualizar)
- ✅ Nome não pode estar vazio
- ✅ Login não pode estar vazio
- ✅ Login deve ser único (não pode duplicar)
- ✅ Senha não pode estar vazia (ao adicionar)
- ✅ Nível de acesso deve ser selecionado

### FormUsuario (Remover)
- ✅ Deve selecionar um usuário
- ✅ Não pode remover o próprio usuário logado
- ✅ Requer confirmação do usuário

### Controle de Acesso
- ✅ Apenas usuários autenticados podem acessar o sistema
- ✅ Apenas Gerentes podem acessar o FormUsuario
- ✅ Atendentes recebem mensagem de acesso negado

## Banco de Dados - Tabela usuario

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

### Campos:
- **id**: Identificador único (auto incremento)
- **nome**: Nome completo do usuário
- **login**: Login único para autenticação
- **senha**: Senha (atualmente texto simples, futuramente hash)
- **nivelAcesso**: "Gerente" ou "Atendente"
- **ativo**: Flag para desativar usuário sem remover
- **dataCriacao**: Data/hora de criação do registro
- **dataAtualizacao**: Data/hora da última atualização

## Próximas Implementações (D45)

1. **CRUD de Cliente**
   - Entidade: Cliente (Id, Nome, CPF, Telefone, Email, Endereço)
   - FormCliente com CRUD completo
   - Validação de CPF

2. **Fluxo de Pedido**
   - Entidade: Pedido (Id, IdCliente, DataPedido, Status, Total)
   - Entidade: ItemPedido (Id, IdPedido, IdProduto, Quantidade, Subtotal)
   - FormPedido com seleção de cliente e produtos

3. **Sistema de Logs**
   - Entidade: Log (Id, IdUsuario, Acao, Tabela, DataHora)
   - Auditoria de todas as operações CRUD

4. **Melhorias de Segurança**
   - Implementar hash de senha com BCrypt
   - Timeout de sessão
   - Histórico de logins

## Estatísticas do Módulo

- **Total de arquivos criados**: 12
- **Total de arquivos modificados**: 3
- **Linhas de código**: ~1.249
- **Tempo estimado de desenvolvimento**: 4-6 horas
- **Nível de complexidade**: Médio
- **Status**: ✅ Pronto para teste
