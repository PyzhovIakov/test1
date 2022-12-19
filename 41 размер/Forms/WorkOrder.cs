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
    public partial class WorkOrder : Form
    {
        public WorkOrder()
        {
            InitializeComponent();
        }

        private void WorkOrder_Load(object sender, EventArgs e)
        {
            try
            {
                label2.Text = Auth.User;

            }
            catch (Exception) { MessageBox.Show("Ошибка формы работы с заказами")}
        }
    }
}
