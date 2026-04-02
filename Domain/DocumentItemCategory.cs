using System;
using System.Collections.Generic;
using System.Text;

namespace Domain;

public sealed class DocumentItemCategory
{
	public int Id { get; set; }
	public required string Name { get; set; }
}
