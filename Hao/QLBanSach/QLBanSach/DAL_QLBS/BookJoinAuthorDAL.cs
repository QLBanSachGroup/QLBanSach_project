using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLBS;
using ENUM;

namespace DAL_QLBS
{
    public class BookJoinAuthorDAL
    {
        private BookManagementDataContext qlbs = new BookManagementDataContext();
        public BookJoinAuthorDAL()
        {
            
        }
        // Thêm một bản ghi vào bảng book_join_author
        public bool AddBookJoinAuthor(book_join_author newRecord)
        {
            try
            {
                qlbs.book_join_authors.InsertOnSubmit(newRecord);
                qlbs.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Lấy ID tác giả theo ID sách
        public int? GetAuthorIdByBookId(int idBook)
        {
            var record = qlbs.book_join_authors.FirstOrDefault(bja => bja.id_book == idBook);
            return record?.id_author;
        }

        // Lấy tất cả các bản ghi từ bảng book_join_author
        public List<book_join_author> GetAllBookJoinAuthors()
        {
            return qlbs.book_join_authors.ToList();
        }
    }
}
