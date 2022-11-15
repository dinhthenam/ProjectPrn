using System;
using System.Collections.Generic;

namespace ProjectPRN.Models
{
    public partial class TblMatHang
    {
        public TblMatHang()
        {
            TblChiTietHds = new HashSet<TblChiTietHd>();
        }

        public string MaHang { get; set; } = null!;
        public string TenHang { get; set; } = null!;
        public string Dvt { get; set; } = null!;
        public float Gia { get; set; }

        public virtual ICollection<TblChiTietHd> TblChiTietHds { get; set; }
    }
}
