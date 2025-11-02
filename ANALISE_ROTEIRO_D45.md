# Análise do Roteiro - Projeto Hamburgueria

## Status Atual do Projeto

Data de referência: 02 de novembro de 2024

---

## 📊 Entregas Concluídas

### ✅ D0 (22/set) — Kickoff + Base do Projeto

**Status: CONCLUÍDO**

| Entrega | Status | Evidência |
|---------|--------|-----------|
| Documento de requisitos | ✅ | Documento externo |
| DER final e dicionário de dados | ✅ | Modelagem no documento e scripts SQL |
| Repositório criado | ✅ | GitHub: kaneki77/System_Hamburger1.1 |
| Estrutura C# com camadas | ✅ | Arquivos .sln e .csproj configurados |

**Critérios de Aceitação:**
- ✅ Requisitos priorizados (MoSCoW) e casos de uso confirmados
- ✅ DER consistente (chaves PK/FK, cardinalidades)
- ✅ Projeto compila com camadas vazias (UI / Domain / Data)

---

### ✅ D15 (07/out) — Modelo Físico + DDL + Conexão C#↔MySQL

**Status: CONCLUÍDO**

| Entrega | Status | Arquivo |
|---------|--------|---------|
| Script DDL completo | ✅ | `ddl_hamburgueria.sql` |
| Semente inicial de dados | ✅ | `dml_hamburgueria.sql` |
| Camada de acesso a dados | ✅ | `DbConnection.cs`, `ClienteRepository.cs` |

**Critérios de Aceitação:**
- ✅ Banco sobe "limpo" do zero e popula dados de exemplo
- ✅ App C# conecta no banco e executa SELECT real

---

### ✅ D30 (22/out) — Regras de Negócio no BD + CRUDs Essenciais

**Status: CONCLUÍDO**

| Entrega | Status | Arquivo |
|---------|--------|---------|
| Triggers/Procedures | ✅ | `triggers_procedures.sql` (Baixa de Estoque, Cálculo de Pedido) |
| CRUDs funcionais | ✅ | `ClienteRepository.cs`, `CategoriaRepository.cs`, `ProdutoRepository.cs` |
| Validação | ✅ | `ClienteService.cs`, `CategoriaService.cs`, `ProdutoService.cs` |

**Critérios de Aceitação:**
- ✅ Trigger/Procedure executa automaticamente regra crítica do domínio
- ✅ CRUDs com validação mínima (campos obrigatórios, formatos)

---

## 🚧 D45 (06/nov) — Fluxos Operacionais + Autenticação/Perfis

**Status: PARCIALMENTE CONCLUÍDO**

### ✅ Implementado

| Entrega | Status | Evidência |
|---------|--------|-----------|
| **Login e controle de acesso por perfil** | ✅ | `FormLogin.cs`, `UsuarioService.cs`, `UsuarioLogado.cs` |
| **Perfis (admin/operador)** | ✅ | Enum `NivelAcesso` (Gerente, Atendente) |
| **Restrição de telas/ações** | ✅ | Controle de acesso no `FormUsuario` |
| **CRUD de Usuários** | ✅ | `FormUsuario.cs`, `UsuarioRepository.cs` |

### ⚠️ Pendente

| Entrega | Status | O que falta |
|---------|--------|-------------|
| **Fluxos principais** | ⚠️ **PENDENTE** | Implementar fluxo de venda/pedido completo |
| **Logs (quem/o quê/quando)** | ⚠️ **PENDENTE** | Criar tabela de logs e sistema de auditoria |
| **Tratamento de exceções** | ⚠️ **PENDENTE** | Implementar tratamento global de erros |

### 📋 Critérios de Aceitação D45

| Critério | Status | Observação |
|----------|--------|------------|
| Fluxo ponta-a-ponta executável | ⚠️ | Falta implementar fluxo de pedido completo |
| Perfis restringem telas/ações | ✅ | Implementado (Gerente vs Atendente) |
| Vídeo de navegação completa | ⚠️ | Aguardando conclusão dos fluxos |
| Tabela de perfis/permissões | ✅ | Documentado em `README_USUARIO.md` |

---

## 🎯 O que precisa ser implementado para concluir D45

### 1. Fluxo de Venda/Pedido (PRIORIDADE ALTA)

**Objetivo:** Implementar o fluxo completo de criação de pedido, desde a seleção do cliente até a finalização da venda.

**Componentes necessários:**

#### **Banco de Dados**
- ✅ Tabela `pedido` (já existe)
- ✅ Tabela `pedido_item` (já existe)
- ✅ Trigger de baixa de estoque (já existe)
- ✅ Procedure de cálculo de total (já existe)

#### **Camada Domain**
- ⚠️ `Pedido.cs` - Entidade Pedido
- ⚠️ `PedidoItem.cs` - Entidade PedidoItem
- ⚠️ `IPedidoRepository.cs` - Interface do repositório
- ⚠️ `PedidoService.cs` - Serviço com validações

#### **Camada Data**
- ⚠️ `PedidoRepository.cs` - Implementação do repositório

#### **Camada UI**
- ⚠️ `FormPedido.cs` - Formulário de criação de pedido
- ⚠️ `FormPedido.Designer.cs` - Designer do formulário

**Funcionalidades do Fluxo:**
1. Seleção de cliente (ComboBox ou busca)
2. Adição de produtos ao pedido (Grid)
3. Cálculo automático de subtotal e total
4. Finalização do pedido
5. Baixa automática de estoque (via trigger)
6. Exibição de confirmação

---

### 2. Sistema de Logs/Auditoria (PRIORIDADE MÉDIA)

**Objetivo:** Registrar todas as ações importantes do sistema (quem fez o quê e quando).

**Componentes necessários:**

#### **Banco de Dados**
```sql
CREATE TABLE log_auditoria (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    acao VARCHAR(100) NOT NULL,
    tabela VARCHAR(50),
    registro_id INT,
    dados_antigos TEXT,
    dados_novos TEXT,
    data_hora DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_usuario) REFERENCES usuario(id_usuario)
);
```

#### **Camada Domain**
- ⚠️ `LogAuditoria.cs` - Entidade Log
- ⚠️ `ILogRepository.cs` - Interface do repositório
- ⚠️ `LogService.cs` - Serviço de logging

#### **Camada Data**
- ⚠️ `LogRepository.cs` - Implementação do repositório

**Ações a serem logadas:**
- Login/Logout de usuários
- Criação, atualização e remoção de registros
- Criação de pedidos
- Alterações de estoque

---

### 3. Tratamento Global de Exceções (PRIORIDADE BAIXA)

**Objetivo:** Capturar e tratar erros de forma centralizada.

**Componentes necessários:**

#### **Camada UI**
- ⚠️ Adicionar `Application.ThreadException` no `Program.cs`
- ⚠️ Criar `FormErro.cs` para exibir erros amigáveis
- ⚠️ Implementar logging de exceções

**Exemplo de implementação:**
```csharp
Application.ThreadException += (sender, e) =>
{
    LogService.RegistrarErro(e.Exception);
    MessageBox.Show("Ocorreu um erro inesperado. Contate o suporte.", 
        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
};
```

---

## 📅 Cronograma Sugerido para Concluir D45

| Data | Tarefa | Tempo Estimado |
|------|--------|----------------|
| **02/nov** | Implementar entidades Pedido e PedidoItem | 1h |
| **02/nov** | Implementar PedidoRepository | 2h |
| **03/nov** | Implementar PedidoService com validações | 2h |
| **03/nov** | Criar FormPedido (UI) | 3h |
| **04/nov** | Testar fluxo completo de pedido | 2h |
| **04/nov** | Criar tabela e entidade de Log | 1h |
| **05/nov** | Implementar LogService e LogRepository | 2h |
| **05/nov** | Integrar logs nas ações principais | 2h |
| **06/nov** | Implementar tratamento global de exceções | 1h |
| **06/nov** | Gravar vídeo de demonstração | 1h |
| **06/nov** | Preparar documentação final D45 | 1h |

**Total estimado: 18 horas**

---

## 🎯 Próximas Entregas (D60 e D30/nov)

### D60 (21/nov) — Relatórios + UX + Qualidade

**Pendente:**
- Relatórios (mín. 2): vendas por período, estoque crítico
- Melhorias de UX: navegação, feedback visual
- Testes: cobertura de cenários críticos

### D30/nov — Release Candidate + Documentação Final

**Pendente:**
- Executável instalável/portável
- Manual do Usuário
- Manual Técnico
- Relatório Final
- Slides de apresentação
- Vídeo de demonstração (3-5 min)

---

## 📊 Resumo do Status

| Entrega | Data Limite | Status | Progresso |
|---------|-------------|--------|-----------|
| D0 | 22/set | ✅ Concluído | 100% |
| D15 | 07/out | ✅ Concluído | 100% |
| D30 | 22/out | ✅ Concluído | 100% |
| **D45** | **06/nov** | ⚠️ **Parcial** | **60%** |
| D60 | 21/nov | ⏳ Pendente | 0% |
| D30/nov | 30/nov | ⏳ Pendente | 0% |

---

## 🚀 Recomendação

Para concluir D45 até 06/nov (4 dias restantes), sugiro focar em:

1. **PRIORIDADE 1:** Implementar fluxo de pedido completo (2-3 dias)
2. **PRIORIDADE 2:** Implementar sistema de logs básico (1 dia)
3. **PRIORIDADE 3:** Gravar vídeo de demonstração (0.5 dia)
4. **OPCIONAL:** Tratamento global de exceções (se sobrar tempo)

**Próximo passo:** Iniciar implementação do fluxo de pedido?
