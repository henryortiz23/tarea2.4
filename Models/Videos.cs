using SQLite;

namespace Tarea2._4.Models
{
    public class Videos
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(50)]
        public string nombre { get; set; }

        public byte[] video { get; set; }

    }
}
