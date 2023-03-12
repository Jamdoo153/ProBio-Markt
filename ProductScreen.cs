using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pro_Bio_Markt
{
    public partial class ProductScreen : Form
    {
        // Verbindung zur Datenbank
        private SqlConnection databaseConnetion = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\Walke\Documents\ProBio Markt.mdf;Integrated Security = True; Connect Timeout = 30");
        private int lastSelectedProductKey;

        public ProductScreen()
        {
            InitializeComponent();
            showProduct();
        }
        private void btnProductSave_Click(object sender, EventArgs e)
        {
            if (tbxProductName.Text == ""
                || tbxProductBrand.Text == ""
                || tbxProductPrice.Text == ""
                || cmbProductCategorie.Text == "")

            {
                MessageBox.Show("Bitte fülle alle Felder aus");
                return;
            }

            // save product name in database
            string productName = tbxProductName.Text;
            string productBrand = tbxProductBrand.Text;
            string productCategorie = cmbProductCategorie.Text;
            string productPrice = tbxProductPrice.Text;

            string query = string.Format("Insert into Products values('{0}','{1}','{2}','{3}')", productName, productBrand, productCategorie, productPrice);
            ExcuteQuery(query);

            clearProductFields();
            showProduct();
        }

        private void btnProductEdt_Click(object sender, EventArgs e)
        {


            showProduct();
        }

        private void btnProductClear_Click(object sender, EventArgs e)
        {
            clearProductFields();
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Bitte wähle zurrest ein Produkt aus");
                return;
            }

            string query = string.Format("delete from Products where Id={0};", lastSelectedProductKey); 
            ExcuteQuery(query);
            showProduct();
            clearProductFields();
        }

        private void ExcuteQuery(string query)
        {
            databaseConnetion.Open();
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnetion);
            sqlCommand.ExecuteNonQuery();
            databaseConnetion.Close();

        }

        private void showProduct()
        {
            //  open Datenbank
            databaseConnetion.Open();

            string query = "Select * from Products";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnetion);

            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            productsDGV.DataSource = dataSet.Tables[0];

            productsDGV.Columns[0].Visible = false;

            //  schließ Datenbank
            databaseConnetion.Close();
        }

        private void clearProductFields()
        {
            tbxProductName.Text = "";
            tbxProductBrand.Text = "";
            cmbProductCategorie.Text = "";
            tbxProductPrice.Text = "";

        }

        private void productsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxProductName.Text = productsDGV.SelectedRows[0].Cells[1].Value.ToString();
            tbxProductBrand.Text = productsDGV.SelectedRows[0].Cells[2].Value.ToString();
            cmbProductCategorie.Text = productsDGV.SelectedRows[0].Cells[3].Value.ToString();
            tbxProductPrice.Text = productsDGV.SelectedRows[0].Cells[4].Value.ToString();

            lastSelectedProductKey = (int)productsDGV.SelectedRows[0].Cells[0].Value;
            Console.WriteLine(lastSelectedProductKey);


        }
    }
}
