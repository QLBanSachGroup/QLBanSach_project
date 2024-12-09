using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENUM;

namespace QLBanSach
{
    public partial class Menu : UserControl
    {
        private List<Form> openForms = new List<Form>(); // Danh sách các form đang mở
        public Menu()
        {
            InitializeComponent();
            label2.Text = Properties.Settings.Default.username;
            label3.Text = Properties.Settings.Default.role;
            phanQuyenNV();
        }



        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();
            cart.Show();
        }

        private void quảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Book book = new Book();
            book.Show();
        }

        private void quảnLýDanhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Show();
        }

        private void quảnLíNVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
        }

        private void quảnLíKHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Show();
        }

        private void toolStripDropDownButton6_Click(object sender, EventArgs e)
        {
            Bill bill = new Bill();
            bill.Show();
        }

        private void toolStripDropDownButton7_Click(object sender, EventArgs e)
        {
            Discount discount = new Discount();
            discount.Show();
        }

        private void phânQuyềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PhanQuyen phanQuyen = new PhanQuyen();
            phanQuyen.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void quảnLíNCCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Publisher publisher = new Publisher();
            publisher.Show();
        }

        private void quảnLýTácGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Author author = new Author();
            author.Show();
        }

        private void phanQuyenNV()
        {
            if (Properties.Settings.Default.rolecode == Role.EMPLOYEE.ToString())
            {
                toolStripDropDownButton4.Visible = false;
                phânQuyềnToolStripMenuItem.Visible = false;
                toolStripDropDownButton2.Visible = false;
                toolStripDropDownButton5.Visible = false;
            }
        }

        private void toolStripDropDownButton9_Click(object sender, EventArgs e)
        {
            Statistical statistical = new Statistical();
            statistical.Show();
        }
    }
}
