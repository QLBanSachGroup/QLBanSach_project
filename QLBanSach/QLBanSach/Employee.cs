using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QLBS;

namespace QLBanSach
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            this.Load += Employee_Load;
            this.btn_ThemNV.Click += Btn_ThemNV_Click;
        }

        private void Btn_ThemNV_Click(object sender, EventArgs e)
        {
            
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            EmployeeBLL employee = new EmployeeBLL();
            dataGridView1.DataSource = employee.getListEmployee();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
