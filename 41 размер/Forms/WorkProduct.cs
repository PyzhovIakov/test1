using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _41_размер.Forms
{
    public partial class WorkProduct : Form
    {
        public WorkProduct()
        {
            InitializeComponent();
        }

        private void WorkProduct_Load(object sender, EventArgs e)
        {
            label2.Text = Auth.User;
            DataTable dt = Classes.RequestsDB.SelectTable("Product");
            dataGridView1.DataSource = dt;
        }
    }
}
