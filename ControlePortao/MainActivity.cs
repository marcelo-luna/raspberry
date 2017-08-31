using Android.App;
using Android.Widget;
using Android.OS;
using Renci.SshNet;
using System.Net;
using System.Threading.Tasks;
using Android.Media;
using System;
using Android.Views;

namespace ControlePortao
{
    [Activity(Label = "Controle App", MainLauncher = true, Icon = "@drawable/icon2")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            // Set our view from the "main" layout resource
            //SetContentView (Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "CONTROLE APP";

            Button button = FindViewById<Button>(Resource.Id.btnOpen);

            SshClient ssh = new SshClient("192.168.0.150", "pi", "toor");

            try
            {
                ssh.Connect();
            }
            catch
            {
                Toast.MakeText(this, "Erro ao Estabelecer Conexão", ToastLength.Long).Show();
            }

            button.Click += delegate
            {
                try
                {
                    ssh.RunCommand("python /home/pi/projetos/automatePortao.py");
                }
                catch
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

            StartActivity(typeof(Tools));
            return base.OnOptionsItemSelected(item);
        }
    }
}

