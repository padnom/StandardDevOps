# culsacdemoapiMT

The goals it's to implements The Standard whith Devops Tools like (Docker,Docker-Compose,RabbitMq,ES...)
- https://github.com/hassanhabib/The-Standard
- https://github.com/hassanhabib/CulDeSacDemoApi

# Elastic stack (ELK) on Docker
 - https://github.com/deviantony/docker-elk
 

## Installation

 ### Launch DockerCompose 
 - via Visual Studio
 - via powershell Into your solution folder launch docker : docker-compose up
 
### RabbitMQConfiguration
go to http://localhost:15672/#/exchanges
login : guest / guest

Into tab **Queues**
 - Add a new Queues : 
 - Name : studentsqueue

Into tab **Exchanges**
- Add a new exchange : 
- Name : CulDeSacApi.Models.Students:Student
- Type : fanout

- Click on Exchanges :
 - Add binding from this exchange

- To queue ==>studentsqueue
- Bind

### SQLServer
Into Visual Studio
 - Add new SQL Server
 - Server Name : dockersqlserver
 - Authentication : Sql Server Authentication
 - userName : sa
 - password : Your_password123 
![image](https://user-images.githubusercontent.com/20400123/195864290-ac7691ad-b372-48b1-87cf-c7b122cccd0c.png)

 
 ### Publish Message 
  - Go to the Url : http://localhost:15672/#/exchanges/%2F/CulDeSacApi.Models.Students%3AStudent
  - Under Section Publish Message with a different guid (https://www.guidgenerator.com/) 

 ```json
 {
  "messageType": [
    "urn:message:CulDeSacApi.Models.Students:Student"
  ],
  "message": {
    "id": "70d62c31-b569-4775-86ea-f3e10a24ea6e",
    "name": "name",
    "libraryAccount": null
  }
}
```

![image](https://user-images.githubusercontent.com/20400123/195865559-0d476670-606d-4e4b-8cd8-c0ddd795db98.png)

Your datas should be register into your tables.
![image](https://user-images.githubusercontent.com/20400123/195866108-13081228-0327-4238-a1f6-cd21da35ecc4.png)

 
## Usage


## Contributing

## License
[MIT](https://choosealicense.com/licenses/mit/)
