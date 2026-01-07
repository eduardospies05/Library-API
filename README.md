# Library-API
Este projeto gerencia um sistema de biblioteca, controlando o acervo de livros, autores, editoras e categorias, feito para fins de aprendizagem.

### Tecnologias Utilizadas
*  .NET 8
*  ASP NET Core
*  MySQL
*  EntityFramework
*  Swagger

### Funcionalidades e Implementações Técnicas

* **Relacionamento Many-to-Many**: Gerenciamento de múltiplos autores por livro, com lógica de sincronização manual e automática na criação e atualização de registros.
* **Padrão Service**: Utilização de interfaces Services para cada entidade (Author, Book, Publisher, Category)
* **Response Pattern**: Utilização da tecnica Response pattern para melhores respostas da API
* **Data Transfer Objects (DTOs)**: Uso de DTOs específicos para entrada e saída de dados, garantindo a segurança das entidades do banco de dados.

### Como Instalar e Rodar

1. **Configuração do Banco**: Ajuste a string de conexão no arquivo `appsettings.json` com suas credenciais do MySQL.
2. **Atualização do Schema**: Execute o comando abaixo no terminal para criar as tabelas:
   ```bash
   dotnet ef database update
