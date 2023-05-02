# icarus

**Recursos necessários:**
* Docker & docker compose
- .Net 7.0 e Asp.Net 7.0
* Dotnet Entity Frame Work

---
## Criando kong-net.

```
docker network create kong-net
``` 

#### Inciar containers.
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

## Swagger & Rotas
#### Para acessar o swagger digite a seguinte url no browser:
```
http://localhost:8085
```
![image](https://user-images.githubusercontent.com/108486349/235678436-33b01ab0-a161-42cb-8f0b-d94c90a39daa.png)

### para acessar as configuraçãos do gateway no explore do swagger digite a seguinte url

```
/api.json
```
