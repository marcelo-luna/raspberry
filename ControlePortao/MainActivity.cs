using Android.App;
using Android.Widget;
using Android.OS;
using Renci.SshNet;
using System.Net;
using System.Threading.Tasks;
using Android.Media;
using System;
using Android.Views;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace ControlePortao
{
    [Activity(Label = "Controle App", MainLauncher = true, Icon = "@drawable/icon2")]
    public class MainActivity : Activity
    {
        string Host = "", User = "", Password = ""; 

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            // Set our view from the "main" layout resource
            //SetContentView (Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "CONTROLE APP";
            LoadConfig();

            Button button = FindViewById<Button>(Resource.Id.btnOpen);

            //SshClient ssh = new SshClient(Host, User, Password);

            try
            {
                //ssh.Connect();
            }
            catch
            {
                Toast.MakeText(this, "Erro ao Estabelecer Conexão", ToastLength.Long).Show();
            }

            button.Click += delegate
            {
                try
                {
                    //ssh.RunCommand("python /home/pi/projetos/automatePortao.py");
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Erro de Conexão", ToastLength.Short).Show();
                }
            };

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            /*Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                ToastLength.Short).Show();*/

            StartActivity(typeof(ToolsActivity));
            return base.OnOptionsItemSelected(item);
        }

        private void LoadConfig()
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
                                    Host = reader.ReadString();
                                    break;

                                case "user":
                                    User = reader.ReadString();
                                    break;

                                case "password":
                                    Password = reader.ReadString();
                                    break;
                            }
                        }
                    }
                }
            }
            catch
            {
                Toast.MakeText(this, "Erro ao ler configurações", ToastLength.Short).Show();
            }
        }
    }
}

