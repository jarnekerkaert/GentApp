using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.DataModel
{
	public class Promotion
	{
		private int _id;
		private DateTime _startDate;
		private DateTime _endDate;
		private string _title;
		private string _description;
		private int _branchId;
		//private int _companyId;
		private string _couponId;

		public int Id { get => _id; set => _id = value; }
		public DateTime StartDate { get => _startDate; set => _startDate = value; }
		public DateTime EndDate { get => _endDate; set => _endDate = value; }
		public string Title { get => _title; set => _title = value; }
		public string Description { get => _description; set => _description = value; }
		public string CouponId { get => _couponId; set => _couponId = value; }
		public int BranchId { get => _branchId; set => _branchId = value; }

		public Promotion(int id, DateTime startDate, DateTime endDate, string title, string description)
		{
			_id = id;
			_startDate = startDate;
			_endDate = endDate;
			_title = title;
			_description = description;
		}

		public Promotion()
		{
		}
	}
}
