
# Sistema de Gerenciamento de Hamburgueria

Este é o repositório do projeto de Gerenciamento de Hamburgueria, desenvolvido com C# (WinForms) e MySQL, seguindo o cronograma de entregas D0, D15 e D30.

## Estrutura da Solução

A solução segue uma arquitetura em três camadas, garantindo a separação de responsabilidades:

1.  **Hamburgueria.UI (User Interface):** Camada de apresentação (WinForms).
2.  **Hamburgueria.Domain (Domínio):** Camada de regras de negócio, serviços e modelos (entidades).
3.  **Hamburgueria.Data (Dados):** Camada de acesso ao banco de dados MySQL (utilizando ADO.NET com MySqlConnector).

## Guia de Build e Configuração

### Pré-requisitos

1.  **Visual Studio:** Versão 2019 ou superior.
2.  **.NET Framework:** Versão 4.7.2 ou superior (Target Framework dos projetos).
3.  **MySQL Server:** Versão 8.0 ou superior.
4.  **NuGet Package:** Instalar **MySql.Data** (ou MySqlConnector) no projeto `Hamburgueria.Data`.

### Configuração do Banco de Dados

1.  **Criar Banco e Tabelas (DDL):** Execute o script `ddl_hamburgueria.sql`.
2.  **Popular Dados (DML):** Execute o script `dml_hamburgueria.sql` para inserir dados de teste.
3.  **Regras de Negócio no BD:** Execute o script `triggers_procedures.sql` para criar as regras de negócio de baixa de estoque e cálculo de pedido.

### Configuração da Conexão C#

1.  Abra o arquivo `Hamburgueria.Data/DbConnection.cs`.
2.  **ATUALIZE** a `ConnectionString` com suas credenciais do MySQL:
    \`\`\`csharp
    private const string ConnectionString = "Server=localhost;Database=hamburgueria_db;Uid=root;Pwd=sua_senha_aqui;";
    \`\`\`

## Status de Entrega do Projeto

O projeto está com o desenvolvimento das entregas D0, D15 e D30 **concluído** e versionado neste repositório.

### D0 (Kickoff + Base do Projeto) - Concluído

| Entrega | Critério de Aceitação | Status |
| :--- | :--- | :--- |
| **Documento de Requisitos (PDF)** | Requisitos priorizados (MoSCoW) e casos de uso confirmados. | **Concluído** (Ver documento externo) |
| **DER Final e Dicionário de Dados** | DER consistente (chaves PK/FK, cardinalidades). | **Concluído** (Modelagem no documento e scripts SQL) |
| **Estrutura C#** | Projeto compila "Hello World" com camadas vazias. | **Concluído** (Arquivos .sln e .csproj configurados) |

### D15 (Modelo Físico + DDL + Conexão C#↔MySQL) - Concluído

| Entrega | Critério de Aceitação | Arquivo(s) |
| :--- | :--- | :--- |
| **Script DDL completo** | Banco sobe "limpo" do zero. | `ddl_hamburgueria.sql` |
| **Semente inicial de dados** | Popula dados de exemplo. | `dml_hamburgueria.sql` |
| **Camada de acesso a dados** | App C# conecta no banco e executa 1 SELECT real. | `Hamburgueria.Data/DbConnection.cs`, `ClienteRepository.cs` |

### D30 (Regras de Negócio no BD + CRUDs Essenciais) - Concluído

| Entrega | Critério de Aceitação | Arquivo(s) |
| :--- | :--- | :--- |
| **CRUDs Essenciais** | CRUDs funcionais das entidades centrais (Cliente). | `Hamburgueria.Data/ClienteRepository.cs` (CREATE, READ, UPDATE, DELETE) |
| **Validação** | CRUDs com validação mínima (campos obrigatórios). | `Hamburgueria.Domain/ClienteService.cs` |
| **Triggers/Procedures** | Trigger/Procedure executa automaticamente a regra crítica do domínio. | `triggers_procedures.sql` (Baixa de Estoque e Cálculo de Pedido) |
| **Testes de Integração** | Testes cobrindo regras (happy path + 1 caso de erro). | Estrutura pronta para testes de validação no `ClienteService.cs`. |

## Próximos Passos (Desenvolvimento)

O próximo passo seria a implementação da interface gráfica (WinForms) para utilizar os serviços e repositórios criados, permitindo a interação visual com o sistema.



## Módulo de Usuários (D45) - Concluído

O módulo de usuários foi implementado com sucesso, adicionando autenticação e controle de acesso baseado em função (RBAC) ao sistema.

### Funcionalidades Implementadas

- **Sistema de Autenticação:**
  - Tela de login antes do menu principal.
  - Validação de credenciais (login e senha).
  - Armazenamento da sessão do usuário logado.
  - Exibição do nome e nível de acesso no título do menu principal.

- **Controle de Acesso Baseado em Função (RBAC):**
  - Dois níveis de acesso: **Gerente** e **Atendente**.
  - Apenas Gerentes podem acessar o gerenciamento de usuários.
  - Mensagem de acesso negado para Atendentes.

- **CRUD de Usuários:**
  - Adicionar, atualizar e remover usuários.
  - Listar todos os usuários.
  - Validação para não permitir remover o próprio usuário logado.

### Arquivos Criados/Modificados

- **Domain:** `Usuario.cs`, `UsuarioLogado.cs`, `IUsuarioRepository.cs`, `UsuarioService.cs`
- **Data:** `UsuarioRepository.cs`
- **UI:** `FormLogin.cs`, `FormUsuario.cs`, `Form1.cs` (atualizado), `Program.cs` (atualizado)
- **Banco de Dados:** `ddl_usuario.sql` (script de criação da tabela)

### Como Testar

1. **Execute o script SQL** `insert_usuarios_teste.sql` para inserir os usuários de teste.
2. **Compile e execute** a aplicação no Visual Studio.
3. **Faça login** com as credenciais de teste:
   - **Gerente:** `admin` / `admin123`
   - **Atendente:** `atendente` / `atendente123`
4. **Teste o controle de acesso** e o CRUD de usuários.
