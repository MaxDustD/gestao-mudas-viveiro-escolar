# PRD — Sistema de Controle de Produção de Mudas para Viveiro Escolar

## 1. Visão Geral

Este documento descreve os requisitos e a modelagem mínima necessária para desenvolver um sistema de controle de produção de mudas em um viveiro escolar. O produto deve permitir o cadastro, o acompanhamento e o controle de lotes de mudas, garantindo o registro de cuidados, uso de canteiros, retiradas e disponibilidade por espécie.

## 2. Objetivos do Produto

- Garantir rastreabilidade de lotes de mudas desde o plantio até a retirada.
- Apoiar o controle de quantidade disponível e evitar retirada indevida.
- Registrar cuidados e eventos relacionados ao desenvolvimento de cada lote.
- Permitir consulta de disponibilidade por espécie e situação do lote.
- Fornecer base clara para a modelagem de dados e regras do sistema.

## 3. Stakeholders

- Professores e coordenadores do viveiro escolar
- Estudantes que utilizam o viveiro para aulas práticas
- Responsáveis pelo manejo e manutenção do viveiro
- Administradores do sistema

## 4. Escopo do Sistema

### Incluído

- Cadastro de espécies de plantas cultivadas no viveiro.
- Cadastro de canteiros onde os lotes são alocados.
- Cadastro de responsáveis por lotes e/ou canteiros.
- Gestão de lotes de mudas com quantidade inicial e disponível.
- Registro de cuidados realizados em lotes.
- Registro de retiradas para doação ou plantio.
- Consultas de disponibilidade por espécie.
- Relatórios de situação e histórico de lotes.

### Excluído (fora de escopo inicial)

- Controle financeiro de insumos e vendas.
- Integração com sistemas externos de inventário.
- Gestão de agendamento de colheita ou plantio.

## 5. Requisitos Funcionais

### RF1 — Espécies

- RF1.1: O sistema deve permitir cadastrar espécies com nome científico e nome comum.
- RF1.2: Deve ser possível editar e listar espécies.
- RF1.3: Deve ser possível consultar disponibilidade agregada por espécie.

### RF2 — Canteiros

- RF2.1: O sistema deve permitir cadastrar canteiros com identificador e descrição.
- RF2.2: Deve ser possível atribuir canteiros a lotes de mudas.
- RF2.3: Deve ser possível listar canteiros e consultar lotes por canteiro.

### RF3 — Responsáveis

- RF3.1: O sistema deve permitir cadastrar responsáveis com nome e contato.
- RF3.2: Deve ser possível associar um responsável a um lote ou a um cuidado.
- RF3.3: Deve ser possível listar responsáveis e seus lotes atribuídos.

### RF4 — Lotes de Mudanças

- RF4.1: O sistema deve permitir cadastrar lotes com as seguintes propriedades:
  - espécie
  - quantidade inicial
  - quantidade disponível
  - data de plantio
  - canteiro
  - status
- RF4.2: Deve impedir a criação de lote sem canteiro.
- RF4.3: Deve permitir acompanhar o desenvolvimento do lote por meio de registros de cuidado.
- RF4.4: Deve permitir encerrar lote com status final.

### RF5 — Cuidados

- RF5.1: O sistema deve permitir registrar cuidados realizados em um lote.
- RF5.2: Cada registro de cuidado deve incluir data, tipo de cuidado, observações e responsável.
- RF5.3: Deve impedir registrar cuidado em lote encerrado.

### RF6 — Retiradas

- RF6.1: O sistema deve permitir registrar retirada de mudas para doação ou plantio.
- RF6.2: Cada retirada deve diminuir a quantidade disponível do lote.
- RF6.3: Deve impedir retirada acima da quantidade disponível.
- RF6.4: Deve armazenar motivo, quantidade retirada, data e destino da retirada.

### RF7 — Consultas e Relatórios

- RF7.1: O sistema deve permitir consultar a disponibilidade de mudas por espécie.
- RF7.2: Deve permitir consultar lotes ativos, encerrados e com quantidade disponível.
- RF7.3: Deve permitir consultar o histórico de cuidados e retiradas por lote.

## 6. Regras de Negócio

- RN1: Lote sempre deve estar associado a um canteiro.
- RN2: Quantidade disponível não pode ser maior que a quantidade inicial.
- RN3: A retirada de mudas não pode reduzir a quantidade disponível abaixo de zero.
- RN4: Cuidados não devem ser registrados quando o lote estiver encerrado.
- RN5: Um lote não pode ser encerrado com quantidade disponível sem justificativa.
- RN6: O status de lote deve refletir se está ativo, em desenvolvimento ou encerrado.

## 7. Modelo de Dados e Entidades

### 7.1 Espécie

- id
- nome_cientifico
- nome_comum
- observacoes

### 7.2 Canteiro

- id
- nome
- descricao
- localizacao (opcional)

### 7.3 Responsável

- id
- nome
- contato
- funcao (opcional)

### 7.4 Lote de Mudas

- id
- especie_id
- canteiro_id
- responsavel_id (opcional)
- quantidade_inicial
- quantidade_disponivel
- data_plantio
- status (ativo, encerrado, cancelado)
- justificativa_encerramento (obrigatório se quantidade_disponivel > 0)
- criado_em
- atualizado_em

### 7.5 Cuidado

- id
- lote_id
- data
- tipo_cuidado
- descricao
- responsavel_id
- observacoes

### 7.6 Retirada

- id
- lote_id
- data
- quantidade
- motivo
- destino (doação, plantio, outro)
- responsavel_id (quem autorizou ou registrou)
- observacoes

## 8. Cenários de Uso Principais

### CU1 — Cadastrar lote de mudas

- Usuário escolhe espécie e canteiro.
- Informa quantidade inicial e data de plantio.
- O sistema define quantidade disponível igual à quantidade inicial.
- O lote entra em status ativo.

### CU2 — Registrar cuidado

- Usuário seleciona lote ativo.
- Informa tipo de cuidado, data, responsável e observações.
- O sistema grava o registro associado ao lote.
- Se o lote estiver encerrado, o sistema bloqueia o registro.

### CU3 — Registrar retirada

- Usuário seleciona lote e informa quantidade e motivo.
- O sistema valida disponibilidade.
- Se a quantidade for válida, o sistema reduz a quantidade disponível.
- Se a quantidade disponível atingir zero, o lote pode permanecer ativo ou ser encerrado dependendo da regra de negócio.

### CU4 — Encerrar lote

- Usuário marca lote como encerrado.
- Se houver quantidade disponível remanescente, deve informar justificativa.
- O sistema altera status para encerrado e impede novos cuidados.

### CU5 — Consultar disponibilidade

- Usuário pesquisa por espécie.
- O sistema apresenta quantidade total disponível por espécie e lista de lotes.

## 9. Critérios de Aceitação

- É possível cadastrar e listar espécies, canteiros, responsáveis, lotes, cuidados e retiradas.
- O sistema bloqueia retirada além do disponível.
- O sistema exige canteiro na criação de lote.
- O sistema impede registros de cuidado em lotes encerrados.
- O sistema exige justificativa de encerramento se sobrarem mudas.
- As consultas retornam disponibilidade correta por espécie.

## 10. Requisitos Não Funcionais

- Interface simples e objetiva para registro e consulta.
- Dados persistidos localmente ou em banco de dados leve.
- Modelo de dados claro e extensível para suportar novos campos.
- Uso de ambiente configurado por variáveis para integração externa.

## 11. Ambiente e Configuração

- O projeto deve expor variáveis de ambiente em um arquivo `.env` para facilitar configuração.
- O arquivo `.env.example` deve documentar a chave de acesso ao serviço de contexto usada no projeto.
- O arquivo `.vscode/mcp.json` define o servidor `context7` e usa a variável de ambiente `CONTEXT7_API_KEY` para autenticação.
- A modelagem deve permitir inclusão de integração com contextos externos sem alterar as regras de negócio centrais.

## 12. Observações

- Este PRD serve como base para a modelagem do sistema.
- A partir daqui, a próxima etapa é transformar as entidades e regras definidas em classes, tabelas e/ou esquemas de dados.
- As validações de status e quantidade devem ser implementadas no domínio do lote.
