# icarus

**Recursos necessários:**
* Docker & docker compose
- .Net 7.0 e Asp.Net 7.0
* Dotnet Entity Frame Work

---
### Instalando o Entity Frame Work
``` dotnet
dotnet tool install --global dotnet-ef
```
---
### Inciando o banco de dados usado pelos serviços
Com os recursos instalados iremos iniciar o banco de dados dos serviços, para isto execute o comando:

``` docker
docker-compose up -d db-services
```
Iremos navegar para as pastas de cada serviço
iniciando pelo Jwt Manager

``` bash
  cd icarus.jwtManager
```
executaremmos o comando do entity frame work para atualizar o banco de dados e criar o banco de usuarios no mysql.

``` dotnet
dotnet ef database update 
```

Agora iremos até a pasta de estoque.

``` bash
  cd icarus.estoque
```

Na pasta do estoque execute o seguinte comando.

``` dotnet
dotnet ef database update 
```

Agora iremos até a pasta de projetos.

``` bash
  cd icarus.projetos
```

Na pasta do projetos execute o seguinte comando.

``` dotnet
dotnet ef database update 
```
Após isso iremos para pasta icarus.service.
``` bash
  cd ..
```
E por fim executaremos o docker compose para subir e criar todos os containers restantes.
```
docker-compose up -d 
``` 
