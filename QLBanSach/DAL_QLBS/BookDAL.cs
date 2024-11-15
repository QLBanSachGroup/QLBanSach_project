using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using DTO_QLBS;

namespace DAL_QLBS
{
    public class BookDAL
    {
        private BookManagementDataContext qlbs = new BookManagementDataContext();

        public BookDAL() { }
        // Phương thức lấy tất cả sách
        public List<BookDTO> GetAllBooks()
        {
            return qlbs.books.Select(book => new BookDTO
            {
                id = book.id,
                name = book.name,
                barcode = book.barcode,
                price = book.price,
                quantity = book.quantity,
                description = book.description,
                id_publisher = book.id_publisher,
                code_category = book.code_category,
                image = book.image != null ? book.image.ToArray() : null
            }).ToList();
        }

        // Thêm sách
        public bool AddBook(book book)
        {
            try
            {
                qlbs.books.InsertOnSubmit(book);
                qlbs.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddBook: {ex.Message}");
                return false;
            }
        }


        // Cập nhật sách
        public bool UpdateBook(book book)
        {
            try
            {
                var existingBook = qlbs.books.SingleOrDefault(b => b.id == book.id);
                if (existingBook != null)
                {
                    existingBook.name = book.name;
                    existingBook.barcode = book.barcode;
                    existingBook.price = book.price;
                    existingBook.quantity = book.quantity;
                    existingBook.description = book.description;
                    existingBook.id_publisher = book.id_publisher;
                    existingBook.code_category = book.code_category;
                    existingBook.image = book.image;
                    qlbs.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Xóa sách
        public bool DeleteBook(int bookId)
        {
            try
            {
                var book = qlbs.books.SingleOrDefault(b => b.id == bookId);
                if (book != null)
                {
                    qlbs.books.DeleteOnSubmit(book);
                    qlbs.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Lấy ra tác giả của cuốn sách
        public string GetAuthorNameByBookId(int bookId)
        {
            var authorName = (from bja in qlbs.book_join_authors
                              join a in qlbs.authors on bja.id_author equals a.id
                              where bja.id_book == bookId
                              select a.name).FirstOrDefault();
            return authorName;
        }
    }
}