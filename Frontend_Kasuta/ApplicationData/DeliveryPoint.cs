using System;
using System.Collections.Generic;

namespace Frontend_Kasuta.ApplicationData;

public partial class DeliveryPoint
{
    public int DeliveryPointId { get; set; }

    public string Address { get; set; } = null!;

    public string Geo { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
