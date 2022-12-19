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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewProduct ViewProduct = new ViewProduct();
            this.Visible = false;
            ViewProduct.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WorkOrder WorkOrder = new WorkOrder();
            this.Visible = false;
            WorkOrder.ShowDialog();
            this.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WorkProduct WorkProduct = new WorkProduct();
            this.Visible = false;
            WorkProduct.ShowDialog();
            this.Visible = true;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            label2.Text = Auth.User;
            if (Auth.Role == "2")
            {
                button3.Visible = false;
            }
            else
            {
                button3.Visible = true;
            }
        }
    }
}
