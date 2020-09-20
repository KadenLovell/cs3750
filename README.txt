starting the server:
navigate to the ./Server directory in a console, then run
`dotnet build`
`dotnet run`

starting the client:
navigate to the ./Client directory in a console, then run
`npm install`
`ng serve`

adding the entity framework cli (only needs to ever be ran once):
`dotnet tool install --global dotnet-ef`

creating the database:
Delete the Migrations folder in the ./Server directory:
`dotnet ef migrations add InitialCreate`
`dotnet ef database update`

TOOLS:
adding the angular CLI (only needs to ever be ran once):
`npm install -g @angular/cli`