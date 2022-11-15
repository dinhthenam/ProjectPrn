using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectPRN
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void cậpNhậtDữLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateProduct product = new UpdateProduct();
            product.Show();
        }

        private void xửLýBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSale sale = new FormSale();
            sale.Show();
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReport report = new FormReport();
            report.Show();
        }

        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCustomer cus = new UpdateCustomer();
            cus.Show();

        }
    }
}
