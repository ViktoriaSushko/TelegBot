using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.Models;

namespace WinFormsApp2
{
    public partial class AddProductForm : Form
    {
        Product product;
        public AddProductForm(Product product)
        {
            InitializeComponent();
            this.product = product;
            SetInitialValue();
        }

        private void SetInitialValue()
        {
            comboBox1.SelectedValue = product.CategoryId;
            textBoxName.Text = product.Name;
            textBoxUnits.Text = product.Units;
            textBoxQuantity.Text=product.Quantity.ToString();
            textBoxPrice.Text = product.Price.ToString();
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            using (SkladContext context = new SkladContext())
            {
                comboBox1.DataSource = null;
                comboBox1.DisplayMember = nameof(Category.CategoryName);
                comboBox1.ValueMember = nameof(Category.Id);
                comboBox1.DataSource = context.Categories.ToList();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                if(int.TryParse(comboBox1.SelectedValue.ToString(), out int id_ctagory)){
                    product.CategoryId = id_ctagory;
                }
                product.Name = textBoxName.Text;
                product.Units=textBoxUnits.Text;
                product.Quantity=int.Parse(textBoxQuantity.Text);
                product.Price=double.Parse(textBoxPrice.Text);
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
