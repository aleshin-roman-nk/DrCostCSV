using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Reports.GetDocumentTitlesByDay;

public class DocumentTitleDto
{
	public int Id { get; set; }
	public DateTime Date {  get; set; }
	public string? Seller {  get; set; }
	public decimal Sum {  get; set; }
	public string? CurrencyCode { get; set; }
}
