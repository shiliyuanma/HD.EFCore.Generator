using System.ComponentModel.DataAnnotations;

namespace HD.EFCore.SqlServerGenerator.Models
{
    public class SqlGenerateViewModel
    {
        [Required(ErrorMessage = "连接串不能为空")]
        public string ConnectionString { get; set; } = "Server=192.168.8.3;Database=FM_OS_V3;User ID=sa;Password=654321;";

        public string TableNames { get; set; }

        public string NamespaceName { get; set; }

        public string DbContextName { get; set; }

        public string ErrorMsg { get; set; }
    }
}
