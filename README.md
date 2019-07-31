# Microsevices-Exmaple
Microservice Using .net Core, Ocelot, Rabbitmq, Consul

first install docker form  https://hub.docker.com/editions/community/docker-ce-desktop-windows

then install rabbit mq :
docker pull rabbitmq

Run rabbitmq-server: 
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management


then install Consul :
docker pull consul

Run consul:
docker run -d --name=c1 -p 8500:8500 consul agent -dev -client=0.0.0.0 -bind=0.0.0.0


then go to projects dirctoy and type:

dotnet run 

the project will work sucssfully

