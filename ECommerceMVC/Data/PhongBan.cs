﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace ECommerceMVC.Data;

public partial class PhongBan
{
    public string MaPb { get; set; }

    public string TenPb { get; set; }

    public string ThongTin { get; set; }

    public virtual ICollection<PhanCong> PhanCongs { get; set; } = new List<PhanCong>();

    public virtual ICollection<PhanQuyen> PhanQuyens { get; set; } = new List<PhanQuyen>();
}