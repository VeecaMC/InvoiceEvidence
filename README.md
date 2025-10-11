# InvoiceEvidence

## About this solution

This is a layered startup solution based on [Domain Driven Design (DDD)](https://abp.io/docs/latest/framework/architecture/domain-driven-design) practises. All the fundamental ABP modules are already installed. Check the [Application Startup Template](https://abp.io/docs/latest/solution-templates/layered-web-application) documentation for more info.

### Pre-requirements

* [.NET9.0+ SDK](https://dotnet.microsoft.com/download/dotnet)
* [Node v18 or 20](https://nodejs.org/en)

### Configurations

The solution comes with a default configuration that works out of the box. However, you may consider to change the following configuration before running your solution:

- SQL Server connection string located in `appsettings.json` both in `InvoiceEvidence.DbMigrator` and `InvoiceEvidence.HttpApi.Host`.

### Before running the application

* Run `abp install-libs` command on your solution folder to install client-side package dependencies. This step is automatically done when you create a new solution, if you didn't especially disabled it. However, you should run it yourself if you have first cloned this solution from your source control, or added a new client-side package dependency to your solution.
* Run `InvoiceEvidence.DbMigrator` to create the initial database. This step is also automatically done when you create a new solution, if you didn't especially disabled it. This should be done in the first run. It is also needed if a new database migration is added to the solution later.

### Running the aplication

* Run `InvoiceEvidence.HttpApi.Host` project to explore API in swagger or run console client

#### Generating a Signing Certificate

In the production environment, you need to use a production signing certificate. ABP Framework sets up signing and encryption certificates in your application and expects an `openiddict.pfx` file in your application.

To generate a signing certificate, you can use the following command:

```bash
dotnet dev-certs https -v -ep openiddict.pfx -p 9a138d0f-ec75-4d16-ad9f-a1c8300f4fb3
```

> `9a138d0f-ec75-4d16-ad9f-a1c8300f4fb3` is the password of the certificate, you can change it to any password you want.

It is recommended to use **two** RSA certificates, distinct from the certificate(s) used for HTTPS: one for encryption, one for signing.

For more information, please refer to: [OpenIddict Certificate Configuration](https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html#registering-a-certificate-recommended-for-production-ready-scenarios)

> Also, see the [Configuring OpenIddict](https://abp.io/docs/latest/Deployment/Configuring-OpenIddict#production-environment) documentation for more information.

### Solution structure

This is a layered monolith application that consists of the following applications:

* `InvoiceEvidence.DbMigrator`: A console application which applies the migrations and also seeds the initial data. It is useful on development as well as on production environment.

## Application domain  

The application domain consists of two entities `Invoice` and `InvoiceLine`. There is one to many relation between those, where one `Invoice` can have many `InvoiceLine`.  

There are two application services, `InvoiceAppService` used for managing `Invoice` entity and the `InvoiceLineAppService` for managing `InvoiceLine` entity.  

There are two controllers (`InvoiceController` and `InvoiceLineController`) with respect to the number of services, each providing endpoints for controlling the respective service.  

With `InvoiceController` you can easily add a new invoice, change its state or get one particular invoice containing list of its `InvoiceLine`s or even get a paginated sortable list of all invoices.  
With `InvoiceLineController` you can easily add, edit or remove `InvoiceLine` from an existing `Invoice`, provided that the invoice is in state `Created`.  
Once the state is changed to `Approved` or `Paid`, you are not allowed to change its `InvoiceLine` records.  

## Deploying the application

Deploying an ABP application follows the same process as deploying any .NET or ASP.NET Core application. However, there are important considerations to keep in mind. For detailed guidance, refer to ABP's [deployment documentation](https://abp.io/docs/latest/Deployment/Index).

### Additional resources

You can see the following resources to learn more about your solution and the ABP Framework:

* [Web Application Development Tutorial](https://abp.io/docs/latest/tutorials/book-store/part-1)
* [Application Startup Template](https://abp.io/docs/latest/startup-templates/application/index)
