# Biblioteca Virtual 📚

Este projeto é uma aplicação de **Biblioteca Virtual** desenvolvida em **C#** utilizando **Entity Framework** para gerenciar livros, usuários e empréstimos de forma eficiente.

## 📌 Tecnologias Utilizadas

- **C#**
- **.NET**
- **Entity Framework Core**
- **PostgreSQL** 
- **ASP.NET Core MVC** 
- **Swagger** 

## 🚀 Funcionalidades

- 📖 Cadastro, edição e remoção de livros
- 👤 Cadastro e gerenciamento de usuários
- 🔄 Empréstimo e devolução de livros
- 🔍 Busca de livros por título, autor ou categoria
- 📊 Relatórios básicos sobre os empréstimos

## 📂 Estrutura do Projeto

```
📁 BibliotecaVirtual
│── 📁 Models        # Definição das entidades (Livro, Usuário, Empréstimo...)
│── 📁 Data          # Contexto do Entity Framework e configuração do banco de dados
│── 📁 Controllers   # Lógica de controle para manipular requisições
│── 📁 Views         # (Caso seja MVC) Páginas para interação do usuário
│── 📁 Services      # Serviços de regra de negócio
│── 📁 Migrations    # Histórico de migrações do banco de dados
│── 📄 appsettings.json  # Configuração da aplicação
│── 📄 Program.cs        # Configuração inicial da aplicação
```

## 🔧 Como Rodar o Projeto

1. **Clone o repositório**
```sh
 git clone https://github.com/Tomlofi1/Biblioteca-Virtual.git
```

2. **Navegue até a pasta do projeto**
```sh
 cd Biblioteca-Virtual
```

3. **Configure a conexão com o banco de dados** no `appsettings.json`, ou no Program/ContextoData

4. **Restaure as dependências**
```sh
 dotnet restore
```

5. **Execute as migrações**
```sh
 dotnet ef migrations add InitialCreate
```

7. **Execute as migrações do banco**
```sh
 dotnet ef database update
```

7. **Inicie o servidor**
```sh
 dotnet run
```

## 🛠 Melhorias Futuras

- 📌 Implementar autenticação e autorização
- 📌 Melhorar interface gráfica (se aplicável)
- 📌 Criar testes automatizados
- 📌 Implementar notificações para devoluções pendentes
- 
---
💡 *Sinta-se à vontade para contribuir com melhorias!* 😊

