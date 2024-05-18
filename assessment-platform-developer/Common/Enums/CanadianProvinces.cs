using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Common.Enums
{
    public enum CanadianProvinces
	{
		Alberta,
		[Description("British Columbia")]
		BritishColumbia,
		Manitoba,
		NewBrunswick,
		[Description("Newfoundland and Labrador")]
		NewfoundlandAndLabrador,
		[Description("Northwest Territories")]
		NovaScotia,
		Ontario,
		[Description("Prince Edward Island")]
		PrinceEdwardIsland,
		Quebec,
		Saskatchewan,
		[Description("Yukon")]
		NorthwestTerritories,
		Nunavut,
		Yukon
	}
}