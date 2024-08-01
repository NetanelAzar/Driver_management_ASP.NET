using System;
using System.Collections.Generic;

namespace BLL
{
	public class CustomerMessage // מחלקה המייצגת הודעה מלקוח או מלקוחות
	{
		public int MessageID { get; set; } // מזהה ההודעה
		public int CustomerID { get; set; } // מזהה הלקוח השולח
		public string MessageText { get; set; } // טקסט ההודעה
		public DateTime SentDate { get; set; } // תאריך שליחת ההודעה
		public bool IsFromCustomer { get; set; } // האם ההודעה נשלחה על ידי הלקוח או על ידי המנהל
	}
}
