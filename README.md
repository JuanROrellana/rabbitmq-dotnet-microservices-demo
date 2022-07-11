# RabbitMQ dotnet Core Integration
This repo contains a small microservices architecture integrated with RabbitMQ.

Running a RabbitMQ client
```
$docker pull rabbitmq:management
$docker run -d -p 5672:5672 -p 15672:15672 rabbitmq:management 
```

Running the app
```
cd SuperStore.Funds/
dotnet watch run
```

```
cd SuperStore.Carts/
dotnet watch run
```
