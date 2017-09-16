using SQLite.Net.Attributes;

namespace ControlePortao.Model
{
    public class Configuracao
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string Server { get; set; }

        public string User { get; set; }

        public string Password { get; set; }
    }
}