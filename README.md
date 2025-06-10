# Moda Orientada a Objetos

Este projeto foi desenvolvido como trabalho final da disciplina de Programação Orientada a Objetos. O sistema simula um gerenciamento de loja de roupas de grife unissex, implementando os principais conceitos de POO e seguindo rigorosamente o padrão de arquitetura MVC (Model-View-Controller).

## Funcionalidades

O sistema oferece as seguintes funcionalidades através de um menu interativo no console:

- Cadastrar novas roupas no catálogo (com persistência)
- Listar todas as roupas disponíveis
- Adicionar roupas ao carrinho de compras (com persistência)
- Visualizar o carrinho com total da compra
- Finalizar compra com simulação de pagamento
- **Histórico de pedidos** - Visualizar todas as compras finalizadas

## Estrutura do Projeto

O projeto está organizado seguindo o padrão MVC:

### Models

- **Roupa.cs**: Representa uma peça de roupa com ID, nome, marca, tamanho e preço
- **Carrinho.cs**: Gerencia os itens selecionados para compra e calcula o total

### Views

- **RoupaView.cs**: Responsável por toda interação com o usuário no console

### Controllers

- **RoupaController.cs**: Orquestra a lógica da aplicação, fazendo a comunicação entre Models e Views

### Data (Persistência)

- **IRoupaRepository.cs**: Interface para operações de dados das roupas
- **RoupaRepository.cs**: Implementação concreta para persistência das roupas em SQLite
- **ICarrinhoRepository.cs**: Interface para operações de dados do carrinho e pedidos
- **CarrinhoRepository.cs**: Implementação concreta para persistência do carrinho e histórico

### Ponto de Entrada

- **Program.cs**: Método Main que inicia a aplicação

## Tecnologias Utilizadas

- C# (.NET 9.0)
- Console Application
- Padrão MVC
- Princípios SOLID
- **SQLite** - Banco de dados para persistência
- **Microsoft.Data.Sqlite** - Provider para acesso ao SQLite
- **Repository Pattern** - Para abstração da camada de dados

## Como Executar

1. Clone o repositório
2. Abra o projeto em um IDE compatível com C#
3. Execute `dotnet restore` para restaurar as dependências
4. Execute `dotnet build` para compilar o projeto
5. Execute `dotnet run` para iniciar a aplicação
6. Use o menu interativo no console para navegar pelas funcionalidades

### Persistência de Dados

- O sistema automaticamente cria um banco de dados SQLite (`loja.db`) na primeira execução
- Todos os dados (roupas, carrinho e histórico de pedidos) são persistidos automaticamente
- O histórico de pedidos fica disponível mesmo após reiniciar a aplicação

## Alunos

- **Vitor Paladini**
- **Emilly Pessutti**

## Objetivo Acadêmico

Este projeto demonstra a aplicação prática dos conceitos fundamentais de Programação Orientada a Objetos, incluindo:

- **Encapsulamento**: Dados e comportamentos organizados em classes apropriadas
- **Separação de Responsabilidades**: Padrão MVC rigorosamente aplicado
- **Princípios SOLID**: Especialmente Inversão de Dependência com interfaces
- **Repository Pattern**: Abstração da camada de dados para facilitar manutenção
- **Persistência de Dados**: Integração com banco de dados SQLite
- **Organização de Código**: Estrutura clara e modular do projeto

### Melhorias Implementadas

- **Persistência Completa**: Todos os dados são salvos em banco SQLite
- **Histórico de Pedidos**: Funcionalidade adicional para rastrear compras
- **Repository Pattern**: Camada de abstração para operações de banco de dados
- **Transações**: Garantia de consistência dos dados nas operações de compra
