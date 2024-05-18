﻿using assessment_platform_developer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace assessment_platform_developer.Application.Queries
{
	[Serializable]
    public class CustomerResponse
    {
        public int ID { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string Country { get; set; }
		public string Notes { get; set; }
		public string ContactName { get; set; }
		public string ContactPhone { get; set; }
		public string ContactEmail { get; set; }
		public string ContactTitle { get; set; }
		public string ContactNotes { get; set; }
    }
}
