using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Container = SimpleInjector.Container;
using assessment_platform_developer.Application.Queries;
using assessment_platform_developer.Application.Commands;
using assessment_platform_developer.Common.Enums;
using System.Text.RegularExpressions;
using assessment_platform_developer.Services;
using System.Reflection.Emit;
using assessment_platform_developer.Common.Helpers;
using assessment_platform_developer.Services.Interfaces;

namespace assessment_platform_developer
{
	public partial class Customers : Page
	{
		private static List<CustomerBasicResponse> customers = new List<CustomerBasicResponse>();
		private readonly ICustomerService customerService; 
		bool isFormValid = true;

		public Customers()
		{
			this.customerService = new CustomerService((Container)HttpContext.Current.Application["DIContainer"]);
		}


		//uncomment for testing
		//public Customers(ICustomerService customerService)
		//{
		//	this.customerService = customerService;
		//}
		public DropDownList TestCountryDropDownList => CountryDropDownList;


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var allCustomers = customerService.GetAllCustomers();               
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

			var allCustomers = customerService.GetAllCustomers();       

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
				var customer = customerService.GetCustomer(Convert.ToInt32(id));
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

		protected void AddButton_Click(object sender, EventArgs e)
		{			
			if(isFormValid){
				var customer = (CreateCustomerCommand)GetMappedCustomerCommandModel(new CreateCustomerCommand());
				customerService.CreateCustomer(customer);

				PopulateCustomerListBox();
				ClearForm();
				ShowMessage("Customer Added");
			}
		}

		protected void UpdateButton_Click(object sender, EventArgs e)
		{
			if(isFormValid){
				var customer = (UpdateCustomerCommand)GetMappedCustomerCommandModel(new UpdateCustomerCommand());
				customerService.UpdateCustomer(customer);

				PopulateCustomerListBox();
				ClearForm();				
				ShowMessage("Customer Updated");
				SetEditMode(false);
			}
		}

		protected void DeleteButton_Click(object sender, EventArgs e)
		{
			customerService.DeleteCustomer(new DeleteCustomerCommand(){ID = Convert.ToInt32(CustomerId.Text) });
			PopulateCustomerListBox();
			ClearForm();
			ShowMessage("Customer Deleted");
			SetEditMode(false);
		}

		//Validations
		public void ZipCodeValidate(object source, ServerValidateEventArgs args)
		{
			if(!isFormValid)
				return;

			string zip = args.Value;
			if (!string.IsNullOrEmpty(zip))
			{
				args.IsValid = false;
				if (Convert.ToInt32(CountryDropDownList.SelectedValue) == ((int)Countries.Canada))
				{
					if (ValidationHelper.IsValidCanadianZiCode(zip))
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
					if (ValidationHelper.IsValidUsZiCode(zip))
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

		public void EmailValidate(object source, ServerValidateEventArgs args)
		{
			if(!isFormValid)
				return;

			string email = args.Value;			
			if (!string.IsNullOrEmpty(email))
			{
				args.IsValid = false;
				
				if (ValidationHelper.IsValidEmail(email))
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

		public void PhoneNumberValidator(object source, ServerValidateEventArgs args)
		{
			if(!isFormValid)
				return;

			string phoneNumber = args.Value;
			if (!string.IsNullOrEmpty(phoneNumber))
			{
				args.IsValid = false;
				if (ValidationHelper.IsValidPhoneNumber(phoneNumber))
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
		
		private void ShowMessage(string message)
		{
			ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "Swal.fire('"+message+"')", true);
		}
	}
}