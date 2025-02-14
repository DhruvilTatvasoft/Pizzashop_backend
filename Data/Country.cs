using System;
using System.Collections.Generic;

namespace learn_dotnet.Data;

public partial class Country
{
    public int Countryid { get; set; }

    public int Countryname { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Modifiedat { get; set; }

    public int Createdby { get; set; }

    public int Modifiedby { get; set; }

    public virtual ICollection<State> States { get; set; } = new List<State>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
