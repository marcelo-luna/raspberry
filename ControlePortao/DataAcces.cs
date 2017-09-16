using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ControlePortao.Model;

namespace ControlePortao
{
    public class DataAcces : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public DataAcces ()
        {
            string Diretorio = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var Plataform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();

            _conexao = new SQLite.Net.SQLiteConnection(Plataform, System.IO.Path.Combine(Diretorio, "Configuracao.db3"));

            _conexao.CreateTable<Configuracao>();
        }

        public void Insert (Configuracao configuracao)
        {
            _conexao.Insert(configuracao);
        }

        public void Update(Configuracao configuracao)
        {
            _conexao.Update(configuracao);
        }

        public void Delete(Configuracao configuracao)
        {
            _conexao.Delete(configuracao);
        }

        public Configuracao SelectById(int id)
        {
            return _conexao.Table<Configuracao>().FirstOrDefault(c => c.Id == id);
        }

        public List<Configuracao> ListAllConfiguracao()
        {
            return _conexao.Table<Configuracao>().OrderByDescending(c => c.Id).ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}