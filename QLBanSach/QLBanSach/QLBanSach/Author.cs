using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO_QLBS;
using BLL_QLBS;

namespace QLBanSach
{
    public partial class Author : Form
    {
        private AuthorBLL authorBLL;
        public Author()
        {
            InitializeComponent();
            authorBLL = new AuthorBLL();
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += BtnClear_Click;
            dgvAuthor.CellClick += DgvAuthor_CellClick;
            txtID.Enabled = false;
            //==============
            LoadAuthors();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void DgvAuthor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvAuthor.Rows[e.RowIndex];
                txtID.Text = row.Cells["id"].Value.ToString();
                txtName.Text = row.Cells["name"].Value.ToString();
                txtAddress.Text = row.Cells["address"].Value.ToString();
                txtBio.Text = row.Cells["bio"].Value.ToString();
                txtPhone.Text = row.Cells["phone"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text) && int.TryParse(txtID.Text, out int id))
            {
                if (authorBLL.DeleteAuthor(id))
                {
                    MessageBox.Show("Xoá tác giả thành công !");
                    LoadAuthors();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xoá !");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tác giả !");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtID.Text) && int.TryParse(txtID.Text, out int id))
            {
                author author = new author
                {
                    id = id,
                    name = txtName.Text,
                    address = txtAddress.Text,
                    bio = txtBio.Text,
                    phone = txtPhone.Text,
                    email = txtEmail.Text
                };

                if (authorBLL.UpdateAuthor(author))
                {
                    MessageBox.Show("Cập nhật tác giả thành công!");
                    LoadAuthors();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật!");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tác giả hợp lệ để cập nhật.");
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            author author = new author
            {
                name = txtName.Text,
                address = txtAddress.Text,
                bio = txtBio.Text,
                phone = txtPhone.Text,
                email = txtEmail.Text
            };

            if (authorBLL.AddAuthor(author))
            {
                MessageBox.Show("Thêm thành công tác giả mới !");
                LoadAuthors();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm tác giả");
            }
        }

        //============================
        private void LoadAuthors()
        {
            List<author> authors = authorBLL.GetAllAuthors();
            dgvAuthor.DataSource = authors;
        }
        private void ClearInputFields()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtBio.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }
    }
}
