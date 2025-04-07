
# 🧪 Teste de Nivelamento – C#

Este repositório reúne a solução para os cinco desafios propostos no **Teste de Nivelamento em C#**, contemplando desde lógica de programação e consumo de APIs, até construção de serviços RESTful com boas práticas de segurança, arquitetura e uso de banco de dados.

---

## 📌 Sumário

1. [Cadastro de Conta Bancária (Console)](#1-cadastro-de-conta-bancária-console)  
2. [Consulta de Gols por Time (API Externa)](#2-consulta-de-gols-por-time-api-externa)  
3. [Comandos Git](#3-comandos-git)  
4. [Consulta SQL com Agrupamento](#4-consulta-sql-com-agrupamento)  
5. [API de Conta Corrente](#5-api-de-conta-corrente)

---

## 1. 📘 Cadastro de Conta Bancária (Console)

Aplicação de console em C# que realiza o cadastro de uma conta corrente. O número da conta e Titular é imutável após o cadastro. O depósito inicial é opcional.

### Funcionalidades:
- Cadastro de conta com ou sem depósito inicial;
- Visualização de dados da conta;
- Validações simples de entrada;
- Operações simples de saque e depósito

---

## 2. ⚽ Consulta de Gols por Time (API Externa)

Aplicação em C# que consome uma API pública de partidas de futebol e retorna a quantidade total de gols marcados por um time em um determinado ano.

### Funcionalidades:
- Requisições HTTP com filtro por `team` e `year`;
- Manipulação de JSON;
- Soma dos gols marcados como mandante ou visitante;
---

## 3. 🔧 Comandos Git

Neste desafio, preciamos descobrir quais arquivos sobraram após rodarmos vários comands do Git.
Se referir ao Doc para respostas

### Comandos utilizados:
- `git init`, `git add`, `git commit`, `git branch`, `git rm`;

---

## 4. 📊 Consulta SQL com Agrupamento

Consulta SQL aplicada sobre uma tabela fictícia de registros de atendimento. O objetivo é retornar o **assunto**, **ano** e a **quantidade de ocorrências**, desde que tenham mais de 3 ocorrências no mesmo ano.

```sql
SELECT assunto, ano, COUNT(*) AS quantidade_ocorrencias 
FROM atendimentos
GROUP BY assunto, ano
HAVING COUNT(*) > 3 
ORDER BY ano ASC, quantidade_ocorrencias DESC;
```

---

## 5. 🌐 API de Conta Corrente

API RESTful desenvolvida em **ASP.NET Core** com funcionalidades de movimentação (saque, depósito) e consulta de saldo da conta corrente. Utiliza banco **SQLite** com tabelas previamente criadas.

### Funcionalidades:
- Consulta de saldo;
- Realização de movimentações com validações de saldo;
- Implementação de **CQRS** e **Mediator** com `MediatR`;
- Segurança contra **SQL Injection** usando `Dapper` com parâmetros;
- Implementação de **idempotência** em operações críticas.

### Arquitetura Aplicada:
- **DDD (Domain-Driven Design)**: separação clara de responsabilidades;
- **CQRS**: separação entre comandos (escrita) e queries (leitura);
- **Injeção de Dependência**;
- **Documentação com Swagger**;
- Camadas: `Domain`, `Application`, `Infrastructure`, `Presentation`.

---

## ⚙️ Tecnologias Utilizadas

- C# / .NET 8+
- ASP.NET Core Web API
- Dapper
- Swagger
- MediatR
- SQLite
- Git

---

## 🛡️ Segurança e Boas Práticas

- ✅ SQL seguro com parâmetros via Dapper;
- 🔁 Proteção contra duplicidade com idempotência;
- 🧪 Código validado com testes manuais em diferentes cenários;
- 📚 Código documentado via Swagger.

---

## ▶️ Como Executar

```bash
# Clonar o repositório
git clone https://github.com/seuusuario/teste-nivelamento-csharp.git
cd teste-nivelamento-csharp

# Entrar na pasta da questão desejada Ex: cd Questao1
cd Questao1

# Executar o projeto dotnet da questão
dotnet run

```

Acesse a documentação da API em:  
🔗 `http://localhost:7140/swagger`

---

## ✍️ Autor

Desenvolvido por **Fábio Varela** – teste de nivelamento em C#.

---
