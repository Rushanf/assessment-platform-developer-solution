<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="assessment_platform_developer._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">.NET Platform Developer Assessment - IMPLEMENTED</h1>            
        </section>
        <br />

         <section class="row" aria-labelledby="aspnetTitle">
              <h1 id="whatsDoneTitle">What's Done</h1>         
         </section>
         <br />
        <section class="row" aria-labelledby="whatsDoneTitle">
           
            <br />
            <br />
            <h3>Code Refactoring to adhere to SOLID & CQRS</h3>
            <h3>Validations</h3>
            <ul>
                <li>Name (Required)</li>
                <li>Email (Format)</li>
                <li>Phone Number (Format)</li>
                <li>Postal/Zip code (Format)</li>
            </ul>

            <h3>Unit tests</h3>
            <ul>
                <li>Email (Format)</li>
                <li>Phone Number (Format)</li>
                <li>Postal/Zip code (Format)</li>
            </ul>
             <br /> 
            <br />
            </section>
         <br />
        <section>
            <h3>Special Notes</h3>
            <p>
                Structured according to the clean architecture.
            </p>
            <p>
                DB integrated just to complete the application. (Hard coded connection string needs to be removed and needs to handle connection string through DI)
            </p>
            <p>
                In the startup project, <code>configs/DependencyInjectionConfig.cs</code> contains DI and injecting repositories can change if required (DB or in-memory List).
            </p>
            <p>
                For Unit Testing, constructors need to be changed in <code>Customers.aspx</code>. Comment the default constructor and uncomment the commented parameterized constructor.
            </p>
            
        </section>
         <br />
          <section class="row" aria-labelledby="aspnetTitle">
             <h3>Refactored by Rushan Albreththulage</h3>
                <p>
                    Contact: 672 338 5587
                </p>
                <p>
                    Email: <a href="mailto:rushan.sameera@gmail.com">rushan.sameera@gmail.com</a>
                </p>
                <p>
                    LinkedIn: <a href="https://www.linkedin.com/in/rushan-fernando/" target="_blank">https://www.linkedin.com/in/rushan-fernando/</a>
                </p>       
         </section>
    </main>

</asp:Content>