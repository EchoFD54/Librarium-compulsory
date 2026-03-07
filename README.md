## Librarium - Compulsory assignment
This is an assignment for the Database for developers course at SEA.

### Setup
The docker-compose file can be used to setup the database on a docker container.

The schema can be applied by running ```dotnet ef database update --project src/Librarium.Data --startup-project src/Librarium.Api``` on the root of the repository.

The Api can be started with dotnet run on src/Librarium.Api