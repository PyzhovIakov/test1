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
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
        }

        private void ViewProductTable()
        {
            try
            {
                dataGridView1.Rows.Clear();
                string[] listp = ViewProduct.OrderP.Split(';');
                for (int i = 0; i < listp.Length; i++)
                {
                    DataTable ProductTable = Classes.RequestsDB.SelectTable("Product", "ProductArticleNumber='" + listp[i]+"'");
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
                                dr["ProductName"].ToString() + "\n" + dr["ProductDescription"].ToString() + "\n" +
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
                               "Цена: (" + dr["ProductCost"].ToString() + ")" + price
                               , dr["ProductDiscountAmount"].ToString());
                        }
                    }
                
                }

            }
            catch (Exception) { MessageBox.Show("Ошибка просмотра таблицы заказываемых товаров"); }
        }

        private void Order_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable pp = Classes.RequestsDB.SelectTable("PickupPoint");
                comboBox2.Items.Clear();
                foreach (DataRow dr in pp.Rows)
                {
                    comboBox2.Items.Add(dr["PickupPointName"].ToString());
                }
                ViewProductTable();
                label2.Text = Auth.User;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка формы заказа");
            }
        }
    }
}
