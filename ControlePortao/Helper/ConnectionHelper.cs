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
using Renci.SshNet;
using ControlePortao.Model;

namespace ControlePortao.Helper
{
    public class ConnectionHelper : Application
    {
        private static SshClient SSH;
        private static string Host = "", User = "", Password = "";

        public static SshClient SSHConection(bool Reconect)
        {
            if (SSH == null || Reconect)
            {
                Connect();
            }

            return SSH;
        }

        private static void Connect()
        {
            LoadConfig();

            try
            {
                SSH = new SshClient(Host, User, Password);
                SSH.ConnectionInfo.Timeout = new TimeSpan(0, 0, 4);
                SSH.Connect();
            }
            catch
            {
                
            }
        }

        private static void LoadConfig()
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
                        Host = listaConfiguracao[0].Server;
                        User = listaConfiguracao[0].User;
                        Password = listaConfiguracao[0].Password;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}