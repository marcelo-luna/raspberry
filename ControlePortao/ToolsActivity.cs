using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using ControlePortao.Model;
using ControlePortao.Helper;

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
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Tools);
            // Create your application here

            Host = FindViewById<EditText>(Resource.Id.txtIp);
            Usuario = FindViewById<EditText>(Resource.Id.txtUsuario);
            Senha = FindViewById<EditText>(Resource.Id.txtSenha);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Configurações";

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

            LoadConfig();

        }

        private void LoadConfig()
        {
            try
            {
                Configuracao configuracao = new Configuracao();

                using (var dados = new DataAcces())
                {
                    List<Configuracao> listaConfiguracao = dados.ListAllConfiguracao();
                    if (listaConfiguracao.Count == 0)
                    {
                        dados.Dispose();
                        return;
                    }
                    else
                    {
                        Host.Text = listaConfiguracao[0].Server;
                        Usuario.Text = listaConfiguracao[0].User;
                        Senha.Text = listaConfiguracao[0].Password;
                    }
                }
            }
            catch
            {
                Toast.MakeText(this, "Erro ao ler configurações", ToastLength.Short).Show();
            }
        }

        public override void OnBackPressed()
        {
            this.Finish();
        }

        private void Save()
        {
            try
            {
                Configuracao configuracao = new Configuracao()
                {
                    Server = Host.Text,
                    User = Usuario.Text,
                    Password = Senha.Text
                };

                using (var dados = new DataAcces())
                {
                    List<Configuracao> listaConfiguracao = dados.ListAllConfiguracao();

                    if (listaConfiguracao.Count == 0)
                        dados.Insert(configuracao);
                    else
                    {
                        listaConfiguracao[0].Server = configuracao.Server;
                        listaConfiguracao[0].User = configuracao.User;
                        listaConfiguracao[0].Password = configuracao.Password;

                        dados.Update(listaConfiguracao[0]);
                    }
                }

                TestConnection(configuracao);
            }
            catch (Exception ex)
            {

            }
        }

        private void TestConnection(Configuracao config)
        {
            if (ConnectionHelper.SSHConection(true).IsConnected)
                Toast.MakeText(this, "Conexão Estabelecida!", ToastLength.Short).Show();
            else
                Toast.MakeText(this, "Erro ao Estabelecer Conexão", ToastLength.Short).Show();
        }
    }
}