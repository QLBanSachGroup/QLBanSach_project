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
using System.IO;

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
            btnChoose.Click += BtnChoose_Click;
            txtID.Enabled = false;
            //==============
            LoadAuthors();
        }

        private void BtnChoose_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    picAuthor.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
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
                // Lấy hình ảnh từ cơ sở dữ liệu dựa trên ID
                int id = int.Parse(txtID.Text);
                var author = authorBLL.GetAllAuthors().FirstOrDefault(a => a.id == id);
                if (author != null && author.image != null)
                {
                    picAuthor.Image = ByteArrayToImage(author.image.ToArray()); // Sử dụng ToArray()
                }
                else
                {
                    picAuthor.Image = null; // Nếu không có ảnh
                }
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
                    email = txtEmail.Text,
                    image = picAuthor.Image != null ? ImageToByteArray(picAuthor.Image) : null
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
                email = txtEmail.Text,
                image = picAuthor.Image != null ? ImageToByteArray(picAuthor.Image) : null
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
            dgvAuthor.DataSource = authors.Select(a => new
            {
                a.id,
                a.name,
                a.address,
                a.bio,
                a.phone,
                a.email
            }).ToList();
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
        private byte[] ImageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
