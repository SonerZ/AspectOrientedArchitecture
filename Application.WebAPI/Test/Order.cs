using System;

namespace Application.WebAPI.Test;

public class Order
{
    public Guid OrderID { get; set; }
    public double Freight { get; set; }
    public int ShipVia { get; set; }
}
