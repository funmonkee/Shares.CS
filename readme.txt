ComputerShares technical challenge

* see emailed document for detailed specification

USAGE:

This is a .Net core 3.1 console application - ensure the SDK is installed on your machine. 
Use the following command to check the SDK is installed and version 3.1 is available

    dotnet --info

Extract the project. in ther Shares.ConsoleApp folder enter 

    dotnet run 1,2,3,4,5 

Logging is configured using appsettings.json

Tests are run using

    dotnet test

SOLUTION STRUCTURE:

There are 4 projects in the solution
    Shares.ConsoleApp           - the console application project
    Shares.ConsoleApp.UnitTest  - unit test project for the above
    Shares.Library              - the feature library
    Shares.Library.UnitTest     - unit test project for above

NOTES:

The challenge scope is quite small, however within the time spent
    Aim to ensure components are single responsibility /SRP
        - with the exception of the ConsoleApp. If I spent more time on this I would
        remove dependency on the System.Console. And inject an outputer. This would
        also make testing easier.
    I use interfaces to adhere to the OCP
        - for example the SingleTradeService extends ITradeService and could be
        replaced by another implementation; Since the ConsoleApplication uses any 
        ITradeService, it can be extended by adding new algorithms and registering that
        with the DI.
    
    Interfaces are used to ensure different implementations can be susbtitue; and 
    are testable.
    
    I considered extending the current example to include a Multi trade option, in 
    addition to the Single trade option. I would have added additional interface,
    for this extension and used the DI to register implementations. During run-time
    I would request the service provider for a ITraderService, that's also is Multi-bid
    capable implementation. 

    Although, the example code is small I used the .Net Core built in DI to configure
    injection of logging and dependent services.

    I used Xunit test for this project.
    Moq was used for mocking depenencies.

FURTHER DEVELOPMENT

Validation of user input and between components, is very basic (hardly any).   
Given more time, I would take a dependency on the FluentValidation framework.   
The FV provides a cleaner appeach of validation and helps ensure SRP.

Handling validation errors and technical errors, would be another area, I would improve.

I structured the core 'best trade' functionality using interface and service injection.
Again, given more time and a view to building the functionality I would adopt the 
MediatR library.  Aside from the benefits of using a command pattern, the MediatR 
library also includes a behaviour pipeline; This can be used to add 'middlewares',
for validation, logging, error handling and security etc.

NOTES:

I used Visual Studio vode and .Net Core SDK on a Macbook to build this solution.
Various VS Code extensions to support .Net development installed.
I normally use Visual Studio 2019 and MSUnit for tests; However, I tried out XUnit today.

I also used GIT locally; and pushed it to a remote repository:
    
    https://github.com/funmonkee/Shares.CS.git



