using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Container = SimpleInjector.Container;
using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Application.Queries;
using assessment_platform_developer.Application.Commands;
using assessment_platform_developer.Common.Enums;
using Microsoft.Ajax.Utilities;
using assessment_platform_developer.Domain.Entities;
using System.Web.Services.Description;
using System.Web.Services;
using System.Text.RegularExpressions;

namespace assessment_platform_developer
{
	public partial class Customers : Page
	{
		private static List<CustomerBasicResponse> customers = new List<CustomerBasicResponse>();
		bool isFormValid = true;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var customerService = GetService<IQueryHandler<GetAllCustomersQuery, List<CustomerBasicResponse>>>();
				var allCustomers = customerService.Handle(new GetAllCustomersQuery());

				ViewState["Customers"] = allCustomers;
				PopulateCustomerListBox();				
				PopulateCustomerDropDownLists();
				SetEditMode(false);
			}
			else
			{
				customers = (List<CustomerBasicResponse>)ViewState["Customers"];
			}			
		}

		private void PopulateCustomerListBox()
		{
			CustomersDDL.Items.Clear();

			var customers = GetService<IQueryHandler<GetAllCustomersQuery, List<CustomerBasicResponse>>>();
			var allCustomers = customers.Handle(new GetAllCustomersQuery());

			var storedCustomers = allCustomers.Select(c => new ListItem(c.Name, c.ID.ToString())).ToArray();
			
			Dictionary<string, string> options = new Dictionary<string, string>();
			options.Add("-1", "Add new customer");

			// Declare a Dictionary to hold all the Options with Value and Text.
			for(int i = 0; i < storedCustomers.Length; i++)
			{
				options.Add(storedCustomers[i].Value, storedCustomers[i].Text);
			}
 
			// Bind the Dictionary to the DropDownList.
			CustomersDDL.DataSource = options;
			CustomersDDL.DataTextField = "value";
			CustomersDDL.DataValueField = "key";
			CustomersDDL.DataBind();			
		}

		private void PopulateCustomerDropDownLists()
		{
			PopulateCountries();
			PopulateStates();
		}

		private void PopulateCountries()
		{
			var countryList = Enum.GetValues(typeof(Countries))
				.Cast<Countries>()
				.Select(c => new ListItem
				{
					Text = c.ToString(),
					Value = ((int)c).ToString()
				})
				.ToArray();

			CountryDropDownList.Items.AddRange(countryList);
		}
		private void PopulateStates(int id = 0)
		{					
			StateDropDownList.Items.Clear();
			ListItem[] provinceList = null;
			if(id == (int)Countries.Canada)
			{	
				provinceList = Enum.GetValues(typeof(CanadianProvinces))
					.Cast<CanadianProvinces>()
					.Select(p => new ListItem
					{
						Text = p.ToString(),
						Value = ((int)p).ToString()
					})
					.ToArray();
				
				CountryDropDownList.SelectedValue = ((int)Countries.Canada).ToString();
			}
			if(id == (int)Countries.UnitedStates)
			{				
				provinceList = Enum.GetValues(typeof(USStates))
					.Cast<USStates>()
					.Select(p => new ListItem
					{
						Text = p.ToString(),
						Value = ((int)p).ToString()
					})
					.ToArray();
				CountryDropDownList.SelectedValue = ((int)Countries.UnitedStates).ToString();
			}

			StateDropDownList.Items.Add(new ListItem(""));
			StateDropDownList.Items.AddRange(provinceList);
		}

		protected void Country_Changed(object sender, EventArgs e)
		{
			DropDownList dl = (DropDownList)sender;
			var id = dl.SelectedItem.Value;

			PopulateStates(Convert.ToInt32(id));
		}

		protected void Customer_Changed(object sender, EventArgs e)
		{
			DropDownList dl = (DropDownList)sender;
			var id = dl.SelectedItem.Value;

			if (Convert.ToInt32(dl.SelectedItem.Value) > -1)
			{				
				SetEditMode(true);
				var customer = GetCustomer(Convert.ToInt32(id));
				LoadCustomer(customer);
			}
			else
			{
				SetEditMode(false);
				ClearForm();
			}

		}

		private void SetEditMode(bool isEditMode = false)
		{
			if(isEditMode)
			{
				EditButton.Visible = true;
				DeleteButton.Visible = true;
				AddButton.Visible = false;
			}
			else 
			{ 
				EditButton.Visible = false;
				DeleteButton.Visible = false;
				AddButton.Visible = true;
			}
		}

		private CustomerResponse GetCustomer(int Id)
		{
			var customerService = GetService<IQueryHandler<GetCustomerQuery, CustomerResponse>>();

			return customerService.Handle(new GetCustomerQuery(){ID = Id});
        }

		private TService GetService<TService>() where TService : class
		{
			var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
			return testContainer.GetInstance<TService>();
		}

		protected void AddButton_Click(object sender, EventArgs e)
		{
			if(isFormValid){
				var customer = (CreateCustomerCommand)GetMappedCustomerCommandModel(new CreateCustomerCommand());
				var customerService = GetService<ICommandHandler<CreateCustomerCommand>>();

				customerService.Handle(customer);

				PopulateCustomerListBox();

				ClearForm();
			}
		}

		protected void UpdateButton_Click(object sender, EventArgs e)
		{
			if(isFormValid){
				var customer = (UpdateCustomerCommand)GetMappedCustomerCommandModel(new UpdateCustomerCommand());
				var customerService = GetService<ICommandHandler<UpdateCustomerCommand>>();
			
				customerService.Handle(customer);

				PopulateCustomerListBox();

				ClearForm();
				SetEditMode(false);
			}
		}

		protected void DeleteButton_Click(object sender, EventArgs e)
		{
			var customerService = GetService<ICommandHandler<DeleteCustomerCommand>>();
			
			customerService.Handle(new DeleteCustomerCommand(){ID = Convert.ToInt32(CustomerId.Text) });

			PopulateCustomerListBox();

			ClearForm();
			SetEditMode(false);
		}

		//Validations
		protected void ZipCodeValidate(object source, ServerValidateEventArgs args)
		{
			string zip = args.Value;
			if (!string.IsNullOrEmpty(zip))
			{
				args.IsValid = false;
				if (Convert.ToInt32(CountryDropDownList.SelectedValue) == ((int)Countries.Canada))
				{
					Regex zipRegex = new Regex(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$"); // Define regex for ZIP code format X#X-#X#
					if (zipRegex.IsMatch(zip))
					{
						args.IsValid = true;
						isFormValid = true;
					}
					else
					{
						args.IsValid = false;
						isFormValid = false;
						return;
					}
				}
				else if (Convert.ToInt32(CountryDropDownList.SelectedValue) == ((int)Countries.UnitedStates))
				{
					Regex zipRegex = new Regex(@"^\d{5}(-\d{4})?$"); // Define regex for ZIP code format #####-#### or #####
					if (zipRegex.IsMatch(zip))
					{
						args.IsValid = true;
						isFormValid = true;
					}
					else
					{
						args.IsValid = false;
						isFormValid = false;
						return;
					}
				}
			}
		}

		private void ClearForm()
		{
			CustomerId.Text = string.Empty;
			CustomerName.Text = string.Empty;
			CustomerAddress.Text = string.Empty;
			CustomerEmail.Text = string.Empty;
			CustomerPhone.Text = string.Empty;
			CustomerCity.Text = string.Empty;
			StateDropDownList.SelectedIndex = 0;
			CustomerZip.Text = string.Empty;
			CountryDropDownList.SelectedIndex = 0;
			CustomerNotes.Text = string.Empty;
			ContactName.Text = string.Empty;
			ContactPhone.Text = string.Empty;
			ContactEmail.Text = string.Empty;
		}

		private void LoadCustomer(CustomerResponse customerResponse)
		{
			CustomerId.Text = customerResponse.ID.ToString();
			CustomerName.Text = customerResponse.Name;
			CustomerAddress.Text = customerResponse.Address;
			CustomerEmail.Text = customerResponse.Email;
			CustomerPhone.Text = customerResponse.Phone;
			CustomerCity.Text = customerResponse.City;
			StateDropDownList.SelectedIndex = String.IsNullOrEmpty(customerResponse.State) ? 0 : Convert.ToInt32(customerResponse.State);
			CustomerZip.Text = customerResponse.Zip;
			CountryDropDownList.SelectedIndex = String.IsNullOrEmpty(customerResponse.Country) ? 0 : Convert.ToInt32(customerResponse.Country);
			CustomerNotes.Text = customerResponse.Notes;
			ContactName.Text = customerResponse.ContactName;
			ContactPhone.Text = customerResponse.ContactPhone;
			ContactEmail.Text = customerResponse.ContactEmail;
		}

		private Object GetMappedCustomerCommandModel(CustomerBasicCommand createCustomer)
		{			
				createCustomer.ID = String.IsNullOrEmpty(CustomerId.Text) ? 0 : Convert.ToInt32(CustomerId.Text);
				createCustomer.Name = CustomerName.Text;
				createCustomer.Address = CustomerAddress.Text;
				createCustomer.City = CustomerCity.Text;
				createCustomer.State = StateDropDownList.SelectedValue;
				createCustomer.Zip = CustomerZip.Text;
				createCustomer.Country = CountryDropDownList.SelectedValue;
				createCustomer.Email = CustomerEmail.Text;
				createCustomer.Phone = CustomerPhone.Text;
				createCustomer.Notes = CustomerNotes.Text;
				createCustomer.ContactName = ContactName.Text;
				createCustomer.ContactPhone = ContactPhone.Text;
				createCustomer.ContactEmail = ContactEmail.Text;

			return createCustomer;
		}
		
	}
}