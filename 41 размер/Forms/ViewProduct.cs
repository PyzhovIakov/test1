using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace _41_размер.Forms
{
    public partial class ViewProduct : Form
    {
        public ViewProduct()
        {
            InitializeComponent();
        }

        static string sort = "Order by ProductCost ASC";
        static string search = "ProductCost>=0";
        static int max = 100;
        static int min = 0;
        public static string OrderP = "";
        private void ViewProductTable()
        {
            try
            {
                dataGridView1.Rows.Clear();
                DataTable ProductTable = Classes.RequestsDB.SelectTable("Product", search + " " + sort);
                int k = 0;
                int i = 0;
                foreach (DataRow dr in ProductTable.Rows)
                {
                    string path = Directory.GetCurrentDirectory();
                    if (dr["ProductPhoto"].ToString() == "")
                    {
                        path += "\\Images\\picture.png";
                    }
                    else
                    {
                        path += "\\Images\\" + dr["ProductPhoto"].ToString();
                    }
                    Image img = new Bitmap(path);
                    if (dr["ProductDiscountAmount"].ToString() == "")
                    {
                        dataGridView1.Rows.Add(dr["ProductArticleNumber"].ToString(), img,
                            dr["ProductName"].ToString() + "\n" + dr["ProductDescription"].ToString() + "\n"+
                            "Производители: " + dr["ProductManufacturer"].ToString() + "\n" +
                            "Цена: " + dr["ProductCost"].ToString()
                            , dr["ProductDiscountAmount"].ToString());
                    }
                    else
                    {
                        double price = Convert.ToDouble(dr["ProductCost"]) - Convert.ToDouble(dr["ProductCost"]) / 100 * Convert.ToInt32(dr["ProductDiscountAmount"]);
                        dataGridView1.Rows.Add(dr["ProductArticleNumber"].ToString(), img,
                           dr["ProductName"].ToString() + "\n" + dr["ProductDescription"].ToString() + "\n" +
                           "Производители: " + dr["ProductManufacturer"].ToString() + "\n" +
                           "Цена: (" + dr["ProductCost"].ToString()+")" + price
                           , dr["ProductDiscountAmount"].ToString());
                    }
                    if (Convert.ToInt32(dr["ProductDiscountAmount"])>15)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7fff00");
                    }
                    if (Convert.ToInt32(dr["ProductDiscountAmount"]) >= min && Convert.ToInt32(dr["ProductDiscountAmount"]) < max)
                    {
                        dataGridView1.Rows[i].Visible = true;
                        k++;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                    i++;
                }
                DataTable ProductTableCount = Classes.RequestsDB.SelectTable("Product");
                label4.Text=k+" Из "+ProductTableCount.Rows.Count;
            }
            catch (Exception) { MessageBox.Show("Ошибка просмотра таблицы товаров"); }
        }

        private void Add(object sender, EventArgs e)
        {
            int id = dataGridView1.CurrentRow.Index;
            if (OrderP == "")
            {
                OrderP = dataGridView1.Rows[id].Cells["id"].Value.ToString() + ";";
            }
            else
            {
                string[] listp = OrderP.Split(';');
                bool f = true;
                for (int i = 0; i < listp.Length;i++ )
                {
                    if (listp[i] == dataGridView1.Rows[id].Cells["id"].Value.ToString())
                    {
                        f = false;
                        break;
                    }
                }
                if (f)
                {
                    OrderP += dataGridView1.Rows[id].Cells["id"].Value.ToString() + ";";
                }
            }
            button1.Visible = true;
        }

        private void ViewProduct_Load(object sender, EventArgs e)
        {
            try
            {
                label2.Text = Auth.User;
                ViewProductTable();
                button1.Visible = false;
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                dataGridView1.ContextMenuStrip = contextMenuStrip1;
                toolStripMenuItem1.Click += Add;
            }
            catch (Exception) { MessageBox.Show("Ошибка формы просмотра продуктов"); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) { 
                sort = "Order by ProductCost ASC"; 
            }
            else
            {
                sort = "Order by ProductCost DESC"; 
            }
            ViewProductTable();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "") {
                search = "ProductCost>=0";
            }
            else
            {
                search = "ProductName Like '%" + textBox1.Text+"%'";
            }
            ViewProductTable();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0) { min = 0; max = 100; }
            if (comboBox2.SelectedIndex == 1) { min = 0; max = 10; }
            if (comboBox2.SelectedIndex == 2) { min = 10; max = 15; }
            if (comboBox2.SelectedIndex == 3) { min = 15; max = 100; }
            ViewProductTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order Order = new Order();
            this.Visible = false;
            Order.ShowDialog();
            if (OrderP == "")
            {
                button1.Visible = false;
            }
            this.Visible = true;
        }
    }
}
