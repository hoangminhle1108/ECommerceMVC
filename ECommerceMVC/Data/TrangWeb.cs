﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ECommerceMVC.Data;

public partial class TrangWeb
{
    public int MaTrang { get; set; }

    public string TenTrang { get; set; }

    public string Url { get; set; }

    public virtual ICollection<PhanQuyen> PhanQuyens { get; set; } = new List<PhanQuyen>();
}