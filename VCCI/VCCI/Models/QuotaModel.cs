using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.WebPages.Html;
using VCCI.DAL;

namespace VCCI.Models
{
	[Table("Quota")]
	public class QuotaModel
	{
		public decimal ID { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string CreatedBy { get; set; }
		public string SelectedDepartment { get; set; }
		public IEnumerable<SelectListItem> Departments
		{
			get
			{
				//return new VCCIEntities().Departments.Select(d =>
				//new SelectListItem
				//{
				//    Text = d.Name,
				//    Value = d.ID.ToString()
				//});
				var db = new VCCIEntities();
				List<SelectListItem> items = new List<SelectListItem>();
				foreach (Department d in db.Departments)
				{
					items.Add(new SelectListItem { Text = d.Name, Value = d.ID.ToString() });
				}
				return items;
			}
		}
	}
}