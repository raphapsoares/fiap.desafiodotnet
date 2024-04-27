# Projeto com .NET 8, Clean Architecture e Design Patterns

Este projeto foi desenvolvido utilizando .NET 8 e segue os princípios da Clean Architecture, incorporando os design patterns Handler e Command. As APIs foram projetadas com base nos princípios RESTful e um middleware de log foi implementado para melhorar a monitoração na API.

## Tecnologias Utilizadas
- **.NET 8**: Utilizado como o framework principal para o desenvolvimento da aplicação.
- **Clean Architecture**: Adotada para promover a separação de preocupações e facilitar a manutenção e testabilidade do código.
- **Design Patterns (Handler e Command)**: Implementados para facilitar o gerenciamento de solicitações e operações de negócios de forma eficiente.
- **Middleware de Log**: Implementado para monitorar e registrar eventos importantes na API.

## Estrutura do Projeto
A estrutura do projeto segue os princípios da Clean Architecture, com os seguintes diretórios:
- **Api**: A camada API faz parte do nível de apresentação e é crucial para a interação entre o cliente e o servidor. 
- **Domain**: Contém as entidades e regras de negócio da aplicação.
- **Application**: Orquestra as operações de alto nível da aplicação.
- **Infrastructure**: Responsável pela implementação de detalhes externos, como acesso a banco de dados e serviços externos.
- **Ui**: Responsável pela interação com o usuário, incluindo a implementação das APIs e UI.

## Banco de Dados
- **Dapper**: Utilizado como micro ORM para acessar o banco de dados SQL Server, proporcionando desempenho e simplicidade.
- **SQL Server**: Banco de dados utilizado para armazenar os dados da aplicação.

## Testes Unitários
- **Moq**: Utilizado para criar mocks de dependências e facilitar os testes unitários.

## Front-end
- **Razor**: Utilizado no front-end para criar páginas web dinâmicas e interativas.