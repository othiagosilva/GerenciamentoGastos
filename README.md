# Gerenciamento de Gastos
Repositório voltado ao controle financeiro pessoal, integrando um ecossistema .NET e React com foco em integridade de dados e performance de relatórios.

## Stack Técnica Local
Frontend: React (Vite) + TypeScript + Tailwind CSS.

Backend: .NET Web API (ASP.NET Core).

Banco de Dados: Oracle SQL (via Dapper/EF).

Comunicação: Axios com tratamento de CORS e JSON Interceptors.

## Como rodar:
Clone o repositório.

Configure a String de Conexão no appsettings.json da API.

Execute dotnet ef database update

Execute dotnet run na pasta da API.

Execute npm install && npm run dev na pasta raiz do frontend.
