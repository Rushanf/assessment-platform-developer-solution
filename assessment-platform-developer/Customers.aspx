<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerForm.aspx.cs" Inherits="assessment_platform_developer.Customers" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> RPM Platform Developer Assessment</title>     

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
            </Scripts>    
        </asp:ScriptManager>

        <nav class="navbar navbar-expand-sm navbar-dark bg-dark">
            <div class="container">
                <a class="navbar-brand" runat="server" href="~/">RPM Platform Developer Assessment</a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item">
                            <a class="nav-link" runat="server" href="~/">Home</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" runat="server" href="~/Customers">Customers</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <div class="container mt-5">
            <h2>Customer Registry</h2>
            <asp:DropDownList runat="server" ID="CustomersDDL" CssClass="form-control mb-4" AutoPostBack="true" OnSelectedIndexChanged="Customer_Changed" />

            <div class="card mb-4">
                <div class="card-body">
                    <h3 class="card-title">Add Customer</h3>
                    <asp:TextBox ID="CustomerId" runat="server" hidden></asp:TextBox>
                    
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="CustomerNameLabel" runat="server" CssClass="form-label">Name <span class="text-danger">*</span></asp:Label>
                                <asp:TextBox ID="CustomerName" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="Name is required" ControlToValidate="CustomerName" CssClass="text-danger"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="CustomerEmailLabel" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="CustomerEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CustomValidator ID="ValidateEmail" runat="server" OnServerValidate="EmailValidate"
                                     ControlToValidate="CustomerEmail"
                                     ErrorMessage="Invalid email."
                                     CssClass="text-danger"
                                     SetFocusOnError="True"></asp:CustomValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="CustomerPhoneLabel" runat="server" Text="Phone" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="CustomerPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CustomValidator ID="CustomerPhoneNumberValidator" runat="server" OnServerValidate="PhoneNumberValidator"
                                     ControlToValidate="CustomerPhone"
                                     ErrorMessage="Invalid phone number."
                                     CssClass="text-danger"
                                     SetFocusOnError="True"></asp:CustomValidator>                                
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4"> 
                            <div class="form-group">
                                <asp:Label ID="CustomerAddressLabel" runat="server" Text="Address" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="CustomerAddress" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="CustomerCityLabel" runat="server" Text="City" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="CustomerCity" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>                        
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="CustomerZipLabel" runat="server" Text="Postal/Zip Code" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="CustomerZip" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CustomValidator ID="CustomZipValidator" runat="server" OnServerValidate="ZipCodeValidate"
                                            ControlToValidate="CustomerZip"
                                            ErrorMessage="Invalid zip code."
                                            CssClass="text-danger"
                                            ClientValidationFunction="validatePostalCode"
                                            SetFocusOnError="True"></asp:CustomValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">                        
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="CustomerStateLabel" runat="server" Text="Province/State" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="StateDropDownList" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="CustomerCountryLabel" runat="server" Text="Country" CssClass="form-label"></asp:Label>
                                <asp:DropDownList ID="CountryDropDownList" OnSelectedIndexChanged="Country_Changed" AutoPostBack="true" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="CustomerNotesLabel" runat="server" Text="Notes" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="CustomerNotes" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mb-4">
                <div class="card-body">
                    <h3 class="card-title">Customer Contact Details</h3>
                    
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="ContactNameLabel" runat="server" Text="Name" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="ContactName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="ContactEmailLabel" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="ContactEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CustomValidator 
                                    ID="validateContactEmail" runat="server" OnServerValidate="EmailValidate"
                                    ControlToValidate="ContactEmail"
                                    ErrorMessage="Invalid email."
                                    CssClass="text-danger"
                                    SetFocusOnError="True"></asp:CustomValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label ID="ContactPhoneLabel" runat="server" Text="Phone" CssClass="form-label"></asp:Label>
                                <asp:TextBox ID="ContactPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CustomValidator ID="ContactPhoneNumberValidator" runat="server" OnServerValidate="PhoneNumberValidator"
                                     ControlToValidate="ContactPhone"
                                     ErrorMessage="Invalid phone number."
                                     CssClass="text-danger"
                                     SetFocusOnError="True"></asp:CustomValidator>                                  
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="d-flex">
                <asp:Button ID="AddButton" runat="server" CssClass="btn btn-primary btn-lg mr-2" Text="Add" OnClick="AddButton_Click" CausesValidation="true" />
                <asp:Button ID="EditButton" runat="server" CssClass="btn btn-warning btn-lg mr-2" Text="Update" OnClick="UpdateButton_Click" CausesValidation="true" />
                <asp:Button ID="DeleteButton" runat="server" CssClass="btn btn-danger btn-lg" Text="Delete" OnClick="DeleteButton_Click"/>
            </div>
           
        </div>

        <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>  
        <script src="Scripts/Validation/FormValidation.js"></script> 
      
    </form>
</body>
</html>
