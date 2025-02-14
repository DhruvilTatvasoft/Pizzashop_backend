﻿using System;
using System.Collections.Generic;

namespace learn_dotnet.Data;

public partial class Order
{
    public int Orderid { get; set; }

    public int Customerid { get; set; }

    public int Totalpersons { get; set; }

    public int Ordernumber { get; set; }

    public string? Ordercomment { get; set; }

    public int? Orderstatus { get; set; }

    public string Paymentmethod { get; set; } = null!;

    public decimal Subtotalamount { get; set; }

    public int? Taxamount { get; set; }

    public int? Discount { get; set; }

    public decimal Totalamount { get; set; }

    public bool? Issuggesionselected { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Modifiedat { get; set; }

    public int Createdby { get; set; }

    public int Modifiedby { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Orderreview> Orderreviews { get; set; } = new List<Orderreview>();

    public virtual ICollection<Ordertable> Ordertables { get; set; } = new List<Ordertable>();
}
