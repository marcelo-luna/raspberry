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
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace ControlePortao
{
    [Activity(Label = "ToolsActivity")]
    public class ToolsActivity : Activity
    {
        EditText Host;
        EditText Usuario;
        EditText Senha;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            Host = FindViewById<EditText>(Resource.Id.txtIp);
            Usuario = FindViewById<EditText>(Resource.Id.txtUsuario);
            Senha = FindViewById<EditText>(Resource.Id.txtSenha);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Tools);
            // Create your application here

            LoadXMLData();

            Button btnGravar = FindViewById<Button>(Resource.Id.btnGravar);

            btnGravar.Click += delegate
            {
                try
                {
                    Save();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Erro ao gravar arquivo", ToastLength.Short).Show();
                }
            };
        }

        private void LoadXMLData()
        {
            try
            {
                using (XmlTextReader reader = new XmlTextReader(Assets.Open("ServerConfig.xml")))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "host":
                                    Host.Text = reader.ReadString();
                                    break;

                                case "user":
                                    Usuario.Text = reader.ReadString();
                                    break;

                                case "password":
                                    Senha.Text = reader.ReadString();
                                    break;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void Save()
        {
            try
            {
                XDocument xDocument = XDocument.Load(Assets.Open("ServerConfig.xml"));
                XElement root = xDocument.Element("config");
                IEnumerable<XElement> rows = root.Descendants("server");
                XElement firstRow = rows.First();
                firstRow.AddBeforeSelf(
                   new XElement("FirstName", ""),
                   new XElement("LastName", ""));
                xDocument.Save("ServerConfig.xml");
            }
            catch (Exception ex)
            {

            }
        }
    }
}