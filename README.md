# icarus

Após realizar o clone do repositório instale o Entity framework em sua maquina:


``` dotnet
dotnet tool install --global dotnet-ef
```
Com o Entity FrameWork iremos iniciar o banco de dados dos serviços, para isto execute o comando:

``` docker
docker-compose up -d db-services
```

Com o banco de dados rodando em sua maquina via docker, iremos navegar até os serviços

``` bash
  cd icarus.jwtManager
```
Na pasta do jwtManager execute o seguinte comando:

``` dotnet
dotnet ef database update 
```

Agora iremos até a pasta de estoque:

``` bash
  cd icarus.estoque
```

Na pasta do estoque execute o seguinte comando:

``` dotnet
dotnet ef database update 
```

Agora iremos até a pasta de projetos:

``` bash
  cd icarus.projetos
```

Na pasta do projetos execute o seguinte comando:

``` dotnet
dotnet ef database update 
```
Após isso iremos para pasta icarus.service:
``` bash
  cd ..
```
E executaremos:
```
docker-compose up -d 
``` 
