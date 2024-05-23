Whats Done

COde Refactoring to adhere SOLID & CQRS.
Validations =>  Name (Required)
		Email (Format)
		Phone Number (Format)
		Postal/Zip code (Format)

		
Unit tests =>   Email (Format)
		Phone Number (Format)
		Postal/Zip code (Format)

Special Notes: 
	Structured according to the clean archetecture.
	DB integrated just to complete the application.(Hard coded conection string need 	to be removed and need to hadle conection string through DI)
	in the startup project, configs/DependencyInjectionConfig.cs contains DI and 	injecting repositories can change if required (DB or in memory List)	
	For Unit Testing, constructors need to be changed in Customers.aspx. comment the 	default constructor and uncomment commented parameterized constructor. 
	


	