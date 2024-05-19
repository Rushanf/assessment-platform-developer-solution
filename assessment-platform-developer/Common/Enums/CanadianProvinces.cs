using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Common.Enums
{
    public enum CanadianProvinces
	{
		Alberta = 1,
		[Description("British Columbia")]
		BritishColumbia = 2,
		Manitoba = 3,
		NewBrunswick = 4,
		[Description("Newfoundland and Labrador")]
		NewfoundlandAndLabrador = 5,
		[Description("Northwest Territories")]
		NovaScotia = 6,
		Ontario = 7,
		[Description("Prince Edward Island")]
		PrinceEdwardIsland = 8,
		Quebec = 9,
		Saskatchewan = 10,
		[Description("Yukon")]
		NorthwestTerritories = 11,
		Nunavut = 12,
		Yukon = 13
	}
}