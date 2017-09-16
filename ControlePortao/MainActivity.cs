using Android.App;
using Android.Widget;
using Android.OS;
using Renci.SshNet;
using System;
using Android.Views;
using System.Collections.Generic;
using ControlePortao.Model;
using ControlePortao.Helper;

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
            ActionBar.Title = "Controle APP";

            try
            {
                if (ConnectionHelper.SSHConection(false).IsConnected)
                    Toast.MakeText(this, "Conexão estabelecida!", ToastLength.Long).Show();
                else
                    Toast.MakeText(this, "Dispositivo não conectado, verifique as configurações", ToastLength.Long).Show();
            }
            catch
            {
                Toast.MakeText(this, "Erro ao Estabelecer Conexão", ToastLength.Long).Show();
            }

            Button button = FindViewById<Button>(Resource.Id.btnOpen);
            //ImageButton btnTeste = FindViewById<ImageButton>(Resource.Id.myButton);

            button.Click += delegate
            {
                try
                {
                    ConnectionHelper.SSHConection(false).RunCommand("python /home/pi/projetos/automatePortao.py");
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
            switch (item.ItemId)
            {
                case Resource.Id.configuracoes:
                    StartActivity(typeof(ToolsActivity));
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}

