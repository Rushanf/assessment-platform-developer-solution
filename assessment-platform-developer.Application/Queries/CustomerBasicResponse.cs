using assessment_platform_developer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace assessment_platform_developer.Application.Queries
{
	[Serializable]
    public class CustomerBasicResponse
    {
        public int ID { get; set; }
		public string Name { get; set; }		
    }
}
