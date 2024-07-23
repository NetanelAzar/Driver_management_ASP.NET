using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL
{
	public class CustomerMessage
	{
		public int MessageID { get; set; }
		public int CustomerID { get; set; }
		public string MessageText { get; set; }
		public DateTime SentDate { get; set; }
		public bool IsFromCustomer { get; set; }

	}
}