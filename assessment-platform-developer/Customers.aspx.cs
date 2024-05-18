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

namespace assessment_platform_developer
{
	public partial class Customers : Page
	{
		private static List<CustomerResponse> customers = new List<CustomerResponse>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
				var customerService = testContainer.GetInstance<IQueryHandler<GetAllCustomersQuery, List<CustomerResponse>>>();

				var allCustomers = customerService.Handle();

				ViewState["Customers"] = allCustomers;
			}
			else
			{
				customers = (List<CustomerResponse>)ViewState["Customers"];
			}

			PopulateCustomerListBox();
			PopulateCustomerDropDownLists();
		}

		private void PopulateCustomerDropDownLists()
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
			CountryDropDownList.SelectedValue = ((int)Countries.Canada).ToString();


			var provinceList = Enum.GetValues(typeof(CanadianProvinces))
				.Cast<CanadianProvinces>()
				.Select(p => new ListItem
				{
					Text = p.ToString(),
					Value = ((int)p).ToString()
				})
				.ToArray();

			StateDropDownList.Items.Add(new ListItem(""));
			StateDropDownList.Items.AddRange(provinceList);
		}

		protected void PopulateCustomerListBox()
		{
			CustomersDDL.Items.Clear();

			var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
			var customers = testContainer.GetInstance<IQueryHandler<GetAllCustomersQuery, List<CustomerResponse>>>();
			var allCustomers = customers.Handle();

			var storedCustomers = allCustomers.Select(c => new ListItem(c.Name, c.ID.ToString())).ToArray();
			if (storedCustomers.Length != 0)
			{
				CustomersDDL.Items.AddRange(storedCustomers);
			}

			
			CustomersDDL.Items.Insert(0, new ListItem("Add new customer", "-1"));
		}

		protected void customer_Change(object sender, EventArgs e)
		{
			
			var x = CustomersDDL.SelectedItem.Value;
		}

		protected void AddButton_Click(object sender, EventArgs e)
		{
			var customer = new CreateCustomerCommand
			{
				Name = CustomerName.Text,
				Address = CustomerAddress.Text,
				City = CustomerCity.Text,
				State = StateDropDownList.SelectedValue,
				Zip = CustomerZip.Text,
				Country = CountryDropDownList.SelectedValue,
				Email = CustomerEmail.Text,
				Phone = CustomerPhone.Text,
				Notes = CustomerNotes.Text,
				ContactName = ContactName.Text,
				ContactPhone = CustomerPhone.Text,
				ContactEmail = CustomerEmail.Text
			};

			var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
			var customerService = testContainer.GetInstance<ICommandHandler<CreateCustomerCommand>>();
			customerService.Handle(customer);

			PopulateCustomerListBox();

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
	}
}