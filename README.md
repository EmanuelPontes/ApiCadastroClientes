ApiCadastroClientes:

Api responsável por realizar autenticação simples utilizando jwt, buscando dados de login(Usuário e senha) no banco de dados;
Permitir CRUD de clientes que possuem: Nome, CPF, Data de Nascimento e uma lista de telefones para contato

Banco de dados Utilizado: Postgresql 

-- necessario instalar :
    Install-Package System.IdentityModel.Tokens.Jwt -Version 6.11.0
    Install-Package Npgsql.EntityFrameworkCore.PostgreSQL
    Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson
