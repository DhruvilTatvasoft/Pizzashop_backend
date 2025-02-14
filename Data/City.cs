using System;
using System.Collections.Generic;

namespace learn_dotnet.Data;

public partial class City
{
    public int Cityid { get; set; }

    public int Cityname { get; set; }

    public int Stateid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Modifiedat { get; set; }

    public int Createdby { get; set; }

    public int Modifiedby { get; set; }

    public virtual State State { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
