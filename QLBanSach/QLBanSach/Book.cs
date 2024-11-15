using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QLBS;
using DTO_QLBS;

namespace QLBanSach
{
    public partial class Book : Form
    {
        private BookBLL bookBLL;
        private CategoryBLL categoryBLL;
        private PublisherBLL publisherBLL;
        private AuthorBLL authorBLL;
        public Book()
        {
            InitializeComponent();
            bookBLL = new BookBLL();
            categoryBLL = new CategoryBLL();
            publisherBLL = new PublisherBLL();
            authorBLL = new AuthorBLL();
            dgvBook.CellClick += DgvBook_CellClick;
            btnAdd.Click += BtnAdd_Click;
            btnChoose.Click += BtnChoose_Click;
            btnClear.Click += BtnClear_Click;
            txtID.Enabled = false;
            //================================
            LoadCategories();
            LoadPublishers();
            LoadBooks();
            LoadAuthors();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            // Xóa sạch các trường nhập liệu
            txtID.Clear();
            txtName.Clear();
            txtBarcode.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtDescription.Clear();

            // Đặt lại ComboBox về giá trị mặc định
            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;
            if (cboPublisher.Items.Count > 0)
                cboPublisher.SelectedIndex = 0;
            if (cboAuthor.Items.Count > 0)
                cboAuthor.SelectedIndex = 0;

            // Xóa hình ảnh được chọn
            picBook.Image = null;

            // Đặt lại tiêu điểm vào trường nhập đầu tiên
            txtName.Focus();
        }

        private void BtnChoose_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picBook.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
            string.IsNullOrWhiteSpace(txtBarcode.Text) ||
            string.IsNullOrWhiteSpace(txtPrice.Text) ||
            string.IsNullOrWhiteSpace(txtQuantity.Text) ||
            cboAuthor.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            try
            {
                string barcode = txtBarcode.Text;
                var existingBook = bookBLL.GetAllBooks().FirstOrDefault(b => b.barcode == barcode);

                if (existingBook != null)
                {
                    // Nếu sản phẩm đã tồn tại, hiển thị thông báo xác nhận
                    var result = MessageBox.Show(
                        "Sản phẩm đã tồn tại. Bạn có muốn cập nhật số lượng không?",
                        "Xác nhận cập nhật",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        existingBook.quantity += int.Parse(txtQuantity.Text);
                        bool isUpdated = bookBLL.UpdateBook(new book
                        {
                            id = existingBook.id,
                            name = existingBook.name,
                            barcode = existingBook.barcode,
                            price = existingBook.price,
                            quantity = existingBook.quantity,
                            description = existingBook.description,
                            id_publisher = existingBook.id_publisher,
                            code_category = existingBook.code_category,
                            image = existingBook.image,
                            create_by = "Admin",
                            create_date = DateTime.Now
                        });
                        if (isUpdated)
                        {
                            MessageBox.Show("Số lượng đã được cập nhật.");
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật số lượng thất bại.");
                        }
                    }
                }
                else
                {
                    // Nếu sách chưa tồn tại, thêm sách mới
                    var newBook = new book
                    {
                        name = txtName.Text,
                        barcode = txtBarcode.Text,
                        price = decimal.Parse(txtPrice.Text),
                        quantity = int.Parse(txtQuantity.Text),
                        description = txtDescription.Text,
                        id_publisher = int.Parse(cboPublisher.SelectedValue.ToString()),
                        code_category = cboCategory.SelectedValue.ToString(),
                        image = picBook.Image != null ? ImageToByteArray(picBook.Image) : null,
                        create_by = "Admin",
                        create_date = DateTime.Now
                    };

                    if (bookBLL.AddBook(newBook))
                    {
                        // Lấy ID sách vừa thêm
                        var addedBook = bookBLL.GetAllBooks().FirstOrDefault(b => b.barcode == barcode);
                        if (addedBook != null)
                        {
                            // Thêm vào bảng book_join_author
                            int authorId = int.Parse(cboAuthor.SelectedValue.ToString());
                            var bookJoinAuthorBLL = new BookJoinAuthorBLL();
                            bool isAdded = bookJoinAuthorBLL.AddBookJoinAuthor(addedBook.id, authorId, "Primary");

                            if (isAdded)
                            {
                                MessageBox.Show("Thêm sách và tác giả thành công.");
                            }
                            else
                            {
                                MessageBox.Show("Thêm sách thành công nhưng không thể liên kết với tác giả.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thêm sách thất bại.");
                    }
                }
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void DgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy thông tin từ dòng hiện tại
                DataGridViewRow row = dgvBook.Rows[e.RowIndex];

                // Kiểm tra null trước khi lấy dữ liệu từ từng ô
                int bookId = row.Cells["id"].Value != null ? Convert.ToInt32(row.Cells["id"].Value) : 0;
                int publisherId = row.Cells["id_publisher"].Value != null ? Convert.ToInt32(row.Cells["id_publisher"].Value) : 0;
                string categoryCode = row.Cells["code_category"].Value != null ? row.Cells["code_category"].Value.ToString() : string.Empty;

                // Hiển thị tên tác giả nếu bookId khác 0
                if (bookId != 0)
                {
                    string authorName = bookBLL.GetAuthorName(bookId);
                    cboAuthor.Text = !string.IsNullOrEmpty(authorName) ? authorName : "Không xác định";
                }
                else
                {
                    cboAuthor.Text = string.Empty;
                }

                // Hiển thị tên nhà xuất bản nếu publisherId khác 0
                if (publisherId != 0)
                {
                    string publisherName = bookBLL.GetPublisherName(publisherId);
                    cboPublisher.Text = !string.IsNullOrEmpty(publisherName) ? publisherName : "Không xác định";
                }
                else
                {
                    cboPublisher.Text = string.Empty;
                }

                // Hiển thị tên danh mục nếu categoryCode không rỗng
                if (!string.IsNullOrEmpty(categoryCode))
                {
                    string categoryName = bookBLL.GetCategoryName(categoryCode);
                    cboCategory.Text = categoryName;
                }
                else
                {
                    cboCategory.Text = string.Empty;
                }

                // Hiển thị hình ảnh
                if (bookId != 0)
                {
                    var book = bookBLL.GetAllBooks().FirstOrDefault(b => b.id == bookId);
                    if (book != null && book.image != null)
                    {
                        // Nếu book.image là byte[], chuyển đổi thành Image
                        picBook.Image = ByteArrayToImage(book.image);
                    }
                    else
                    {
                        // Nếu không có ảnh hoặc book.image null
                        picBook.Image = null;
                    }
                }
                else
                {
                    picBook.Image = null;
                }

                if (bookId != 0)
                {
                    // Lấy ID tác giả từ bảng book_join_author
                    var bookJoinAuthorBLL = new BookJoinAuthorBLL();
                    int? authorId = bookJoinAuthorBLL.GetAuthorIdByBookId(bookId);

                    if (authorId.HasValue)
                    {
                        // Hiển thị tên tác giả trong cboAuthor
                        var authors = authorBLL.GetAllAuthors();
                        var selectedAuthor = authors.FirstOrDefault(a => a.id == authorId.Value);
                        if (selectedAuthor != null)
                        {
                            cboAuthor.Text = selectedAuthor.name;
                        }
                        else
                        {
                            cboAuthor.Text = "Không xác định";
                        }
                    }
                    else
                    {
                        cboAuthor.Text = "Không xác định";
                    }
                }
                else
                {
                    cboAuthor.Text = string.Empty;
                }

                // Hiển thị các thông tin khác lên các điều khiển khác nếu cần
                txtID.Text = bookId.ToString();
                txtName.Text = row.Cells["name"].Value?.ToString() ?? string.Empty;
                txtBarcode.Text = row.Cells["barcode"].Value?.ToString() ?? string.Empty;
                txtPrice.Text = row.Cells["price"].Value != null ? row.Cells["price"].Value.ToString() : "0";
                txtQuantity.Text = row.Cells["quantity"].Value != null ? row.Cells["quantity"].Value.ToString() : "0";
                txtDescription.Text = row.Cells["description"].Value?.ToString() ?? string.Empty;
            }
        }

        //=================================
        // Tải danh sách danh mục vào cboCategory
        private void LoadCategories()
        {
            var categories = categoryBLL.GetAllCategories();
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "name";
            cboCategory.ValueMember = "code";
        }

        // Tải danh sách nhà cung cấp vào cboPublisher
        private void LoadPublishers()
        {
            var publishers = publisherBLL.GetAllPublishers();
            cboPublisher.DataSource = publishers;
            cboPublisher.DisplayMember = "name";
            cboPublisher.ValueMember = "id";
        }
        
        // Tải danh sách sách
        private void LoadBooks()
        {
            var books = bookBLL.GetAllBooks();
            dgvBook.DataSource = books.Select(book => new
            {
                book.id,
                book.name,
                book.barcode,
                book.price,
                book.quantity,
                book.description,
                book.id_publisher,
                book.code_category,
                book.image
            }).ToList();
        }

        // Tải danh sách tác giả vào cboAuthor
        private void LoadAuthors()
        {
            var authors = authorBLL.GetAllAuthors();  // authorBLL là một thể hiện của lớp AuthorBLL
            cboAuthor.DataSource = authors;
            cboAuthor.DisplayMember = "name";
            cboAuthor.ValueMember = "id";
        }

        // Chuyển đổi hình ảnh thành mảng byte
        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        // Chuyển đổi mảng byte thành ảnh
        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null || byteArrayIn.Length == 0)
                return null;

            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }

    }
}
