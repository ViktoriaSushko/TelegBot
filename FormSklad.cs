using Microsoft.EntityFrameworkCore;
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
    public partial class FormSklad : Form
    {
        public FormSklad()
        {
            InitializeComponent();
        }

        private async void btnShow_Click(object sender, EventArgs e)
        {
            //using(SkladContext context = new SkladContext()) { 
            //    await context.Products.LoadAsync();
            //    dataGridView1.DataSource = context.Products.Local.ToBindingList();
            //}
            UpdateProduct();
        }

        private async void UpdateProduct()
        {
            using (SkladContext context = new SkladContext())
            {
                dataGridView1.DataSource = null;
                var products = context.Products.Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Units,
                    t.Price,
                    t.Quantity,
                    t.Category.CategoryName
                }).ToList();

                dataGridView1.DataSource = products;
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void FormSklad_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private async void btnAddPr_Click(object sender, EventArgs e)
        {
            using (SkladContext context = new SkladContext())
            {
                Product product = new Product();
                AddProductForm addProduct = new AddProductForm(product);
                if (addProduct.ShowDialog() == DialogResult.OK)
                {
                    var name = context.Products.FirstOrDefault(t => t.Name == product.Name);
                    if (name != null)
                    {
                        MessageBox.Show("The product was already exists");
                    }
                    else
                    {
                        context.Products.Add(product);
                        await context.SaveChangesAsync();
                        UpdateProduct();
                    }
                }
            }
        }

        private async void btnEditPr_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (SkladContext context = new SkladContext())
                    {
                        Product? product = await context.Products.FindAsync(id);
                        if (product != null)
                        {
                            AddProductForm addProduct = new AddProductForm(product);
                            if (addProduct.ShowDialog() == DialogResult.OK)
                            {
                                await context.SaveChangesAsync();
                                UpdateProduct();
                            }
                        }
                    }
                }
            }
        }

        private async void btnDelPr_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (int.TryParse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), out int id))
                {
                    using (SkladContext context = new SkladContext())
                    {
                        Product? product = await context.Products.FindAsync(id);
                        if (product != null)
                        {
                            var res=MessageBox.Show($"Do you want to delete product: {product.Name}?", "Delete product", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if(res == DialogResult.OK)
                            {
                                context.Products.Remove(product);
                                await context.SaveChangesAsync();
                                UpdateProduct();
                            }
                        }
                    }
                }
            }
        }
    }
}
