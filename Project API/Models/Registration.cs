using System;
using System.Collections.Generic;

namespace Project_API.Models;

public partial class Registration
{
    public int Id { get; set; }

    public string FName { get; set; } = null!;

    public string LName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Country { get; set; } = null!;
}
