using System.ComponentModel.DataAnnotations;

namespace HD.EFCore.MySqlGenerator.Models
{
    public class SqlGenerateViewModel
    {
        [Required(ErrorMessage = "连接串不能为空")]
        public string ConnectionString { get; set; } = "Server=localhost;Port=3306;Database=test;Uid=root;Pwd=123456;";

        public string TableNames { get; set; }

        public string NamespaceName { get; set; }

        public string DbContextName { get; set; }

        public string ErrorMsg { get; set; }
    }
}
