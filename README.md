# icarus

**Recursos necessários:**
* Docker & docker compose
- .Net 7.0 e Asp.Net 7.0
* Dotnet Entity Frame Work

---
## Instalando o Entity Frame Work e configurando o network do docker

#### Para instalar o entity Frame work abra seu terminal e digite o seguinte comando:

``` dotnet
dotnet tool install --global dotnet-ef
```
#### após finalizar a instalação do entity frame work iremos criar uma network no docker para isso use o seguinte comando:

```
docker network create kong-net
``` 

---

## Banco de dados dos serviços
#### Com os recursos instalados iremos iniciar o banco de dados dos serviços, para isto execute o comando:

``` docker
docker-compose up -d db-services
```
#### Iremos navegar para as pastas de cada serviço iniciando pelo Jwt Manager

``` bash
cd icarus.jwtManager
```

#### executaremos o comando do entity frame work para atualizar o banco de dados e criar o banco de usuarios no mysql.

``` dotnet
dotnet ef database update 
```

#### Agora iremos até a pasta de estoque.

``` bash
cd icarus.estoque
```

#### Na pasta do estoque execute o seguinte comando.

``` dotnet
dotnet ef database update 
```

#### Agora iremos até a pasta de projetos.

``` bash
cd icarus.projetos
```

#### Na pasta do projetos execute o seguinte comando.

``` dotnet
dotnet ef database update 
```

---
## Iniciando containeres dos serviços 

#### iremos para pasta icarus.service.
``` bash
cd ..
```
#### E por fim executaremos o docker compose para subir e criar todos os containers restantes.
```
docker-compose up -d 
``` 

## Configurando gateway

#### Após ter todos os serviços abra o navegador e coloque a seguinte url

```
localhost:1337
```
#### Nesta etapa você irá realizar a criação de um novo usuario:
![image](https://user-images.githubusercontent.com/108486349/234555055-d6f8f771-0c3c-4f1b-b866-400bfc2efbe5.png)

#### após criar o login no painel do konga realize a configuração da api gateway kong:
![image](https://user-images.githubusercontent.com/108486349/234555609-3e5fd848-2c50-4ac9-97d2-bb3ca01efcf7.png)

#### vá até a opção **snapshots** e instale o arquivo **snapshot_3.json:**
![image](https://user-images.githubusercontent.com/108486349/234556088-54b1c3c4-8bd1-4cda-b03c-1880a0f6f0ee.png)

#### após ter importado o arquivo vá em detalhes, clique na opção restore e selecione as seguintes opções:
![image](https://user-images.githubusercontent.com/108486349/234556253-a22a84bc-d7c1-45a0-804a-2864d4e674fd.png)

![image](https://user-images.githubusercontent.com/108486349/234556449-15f915a3-f3bb-48a1-95f4-36179d2ac575.png)

![image](https://user-images.githubusercontent.com/108486349/234556772-fd3e29be-047b-4ea4-b0fb-804b6318742d.png)

#### Para verificar as rotas disponiveis basta ir na opção routes

#### url utilizada para acessar endpoints é:

``` 
localhost:8000/
``` 

## para acessar qualquer rota além do login é necessario estar autenticado:
> **Access Tokens são validos por uma hora**
#### para isto crie um usuario  na rota:

``` 
localhost:8000/registro
``` 
####  no corpo da requesição envie:

``` json 
{
  "userName": "string",
  "email": "string",
  "senha": "string",
  "descricao": "string"
}
``` 

#### realize o login na rota:

``` 
localhost:8000/login
``` 
#### no corpo da requesição envie as mesmas credenciais usadas para a criação do usuario:

``` json 
{
  "userName": "string",
  "email": "string",
  "senha": "string",
  "descricao": "string"
}
``` 
#### Para solicitar um novo Access Token:

``` 
localhost:8000/refresh
``` 
#### no corpo da requesição envie as mesmas credenciais usadas para a criação do usuario:
> **Importante enviar a requisição antes de o token expirar pois a rota necessita de autenticação**
``` json 
{
  "userName": "string",
  "token": "string",
  "refreshToken": "string",
}
``` 
--- 
#### para atualizar ou criar um novo projeto:

``` json 
{
  "id": 0,
  "name": "string",
  "status": "string",
  "dataInicio": "2023-04-13T11:36:22.594Z",
  "dataEntrega": "2023-04-13T11:36:22.594Z",
  "chapa": "string",
  "QuantidadeDeChapa": 0,
  "descricao": "string",
  "valor": 0
}
``` 


#### para atualizar ou criar um novo produto em estoque:

``` json 
{
  "nome": "string",
  "quantidade": 0,
  "cor": "string",
  "valorUnitario": 0
}
``` 
----

## Swagger & Rotas
#### Para acessar o swagger digite a seguinte url no browser:
```
http://localhost:8085
```
![image](https://user-images.githubusercontent.com/108486349/235678436-33b01ab0-a161-42cb-8f0b-d94c90a39daa.png)

### para acessar as configuraçãos do gateway no explore do swagger digite a seguinte url

```
http://localhost:8080/api.json
```
