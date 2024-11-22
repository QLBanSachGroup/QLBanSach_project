using System;
using System.Windows.Forms;
using DTO_QLBS;
using BLL_QLBS;

namespace QLBanSach
{
    public partial class DiscountForm : Form
    {
        private DiscountBLL discountBLL;

        public DiscountForm()
        {
            InitializeComponent();
            discountBLL = new DiscountBLL();
            InitializeForm();
        }

        private void InitializeForm()
        {
            ConfigureEvents();
            LoadDiscounts();
            ConfigureComboBox();
            txtID.Enabled = false;
        }

        private void ConfigureEvents()
        {
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnReset.Click += BtnReset_Click;
            dgvDiscount.CellClick += DgvDiscount_CellClick;
        }

        private void LoadDiscounts()
        {
            try
            {
                dgvDiscount.DataSource = discountBLL.GetAllDiscounts();
                dgvDiscount.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
        }

        private void ConfigureComboBox()
        {
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.AddRange(new[] { "Active", "Inactive" });
            cmbStatus.SelectedIndex = 0;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var discount = new DiscountDTO
                {
                    DiscountName = txtDiscountName.Text,
                    Description = txtDescription.Text,                  
                    DiscountAmount = string.IsNullOrWhiteSpace(txtDiscountAmount.Text) ? (decimal?)null : decimal.Parse(txtDiscountAmount.Text),
                    StartDate = dtpStartDate.Value,
                    EndDate = dtpEndDate.Value,
                    Status = cmbStatus.SelectedItem.ToString()
                };

                if (discountBLL.AddDiscount(discount))
                {
                    MessageBox.Show("Thêm thành công!");
                    LoadDiscounts();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Vui lòng chọn chương trình cần cập nhật.");
                    return;
                }

                var discount = new DiscountDTO
                {
                    ID = int.Parse(txtID.Text),
                    DiscountName = txtDiscountName.Text,
                    Description = txtDescription.Text,                 
                    DiscountAmount = string.IsNullOrWhiteSpace(txtDiscountAmount.Text) ? (decimal?)null : decimal.Parse(txtDiscountAmount.Text),
                    StartDate = dtpStartDate.Value,
                    EndDate = dtpEndDate.Value,
                    Status = cmbStatus.SelectedItem.ToString()
                };

                if (discountBLL.UpdateDiscount(discount))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadDiscounts();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Vui lòng chọn chương trình cần xóa.");
                    return;
                }

                int id = int.Parse(txtID.Text);

                if (discountBLL.DeleteDiscount(id))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadDiscounts();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtDiscountName.Clear();
            txtDescription.Clear();
            txtDiscountAmount.Clear();
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            cmbStatus.SelectedIndex = 0;
        }

        private void DgvDiscount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDiscount.Rows[e.RowIndex];
                txtID.Text = row.Cells["ID"].Value?.ToString();
                txtDiscountName.Text = row.Cells["DiscountName"].Value?.ToString();
                txtDescription.Text = row.Cells["Description"].Value?.ToString();
                txtDiscountAmount.Text = row.Cells["DiscountAmount"].Value?.ToString();
                dtpStartDate.Value = Convert.ToDateTime(row.Cells["StartDate"].Value);
                dtpEndDate.Value = Convert.ToDateTime(row.Cells["EndDate"].Value);
                cmbStatus.SelectedItem = row.Cells["Status"].Value?.ToString();
            }
        }
    }
}
