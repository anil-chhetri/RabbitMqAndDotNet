# Rabbitmq with .net

RabbitMQ is a message broker, It accept and forwards message. Some common jargon used in RabbitMQ are 
- **Producing**: it represent the service that sends message to rabbitmq. 
- **Queue**: it is where message send by producer will be stored. it's memory is bounded by the host's memory and disk limits. It acts as a messaging buffer where producer add message and consumer take those message and leaves acknowledgement.
- **Consuming**: It represent the service that receive messages.

## Installing Rabbitmq

In this sample project, rabbitmq is installed in docker compose using docker-compose.yml file. If we see offical image of rabbitmq in docker hub there are many variant mostly ``docker pull rabbitmq:latest`` and ``docker pull rabbitmq:management``. As we can see one is rabbitmq and next is management, the only difference in these is that in managment the management plugin is already enable which will listen to port: 15672. By using managment plugin we are given web UI through which we can communicate with rabbitmq. Also in ``docker-compose.yml`` file, there is section which defines the username and password for the management UI.