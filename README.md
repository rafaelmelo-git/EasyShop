EasyShop - Persistência Poliglota (.NET MAUI)

Protótipo de mini e-commerce que usa dois bancos locais ao mesmo tempo:

| Domínio  | Paradigma | Biblioteca | Arquivo |
|----------|-----------|------------|---------|
| Produtos | NoSQL (documentos) | LiteDB | products.db |
| Clientes | SQL (relacional)   | sqlite-net-pcl | customers.db3 |

Integrantes
- Rafael Ferreira Araboni de Melo
- Thales Henrique Fernandes Barreto

Arquitetura em camadas
- Apresentação (Views/, ViewModels/): telas XAML e ViewModels (MVVM). Não acessam banco.
- Negócio (Services/): validações (nome completo, e-mail, e-mail único, preço) e conversão dos atributos dinâmicos.
- Dados (Data/): repositórios com CRUD. CustomerRepository usa SQLite, ProductRepository usa LiteDB.
- Modelos (Models/): Customer (tabela) e Product (documento com Dictionary<string,string> Attributes).

A injeção de dependência está em MauiProgram.cs.

Por que poliglota?
- Clientes têm estrutura fixa e exigem consistência (e-mail único, campos obrigatórios), o que é natural em SQL.
- Produtos têm atributos variáveis (camisa: tamanho/cor; notebook: RAM/processador). No LiteDB isso é salvo no mesmo documento, sem colunas nulas e sem alterar schema.

Como rodar
1. Instale o workload .NET MAUI (dotnet workload install maui) ou o Visual Studio com a carga de trabalho MAUI.
2. Abra a solução e restaure os pacotes NuGet.
3. Execute em Windows, Android ou iOS.

Pacotes: sqlite-net-pcl, SQLitePCLRaw.bundle_green, LiteDB, CommunityToolkit.Mvvm.

Exemplo de uso
- Produtos: Nome `Notebook X`, Preço `3999,90`, Atributos (um por linha): `Processador: i5` / `RAM: 16GB`.
- Clientes: Nome `Maria Silva`, E-mail `maria@email.com`.
