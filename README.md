# BasketService
A basket service that calculates the price of a given basket deducting the correct value when applying vouchers.

This service uses SQL lite for data storage and makes use of EF core for data access. The component tests can be used to test the service end to end.

## Migrate Database

You need to run migrations against the database to set it up for the basket service.

- Run `Update-Database` from the Package Manager Console - this will apply all the migrations to your database.

The test project will automatically migrate the database when running tests. 
