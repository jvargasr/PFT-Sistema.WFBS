using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WFBS.Business.Core;

namespace MasterPages.Page
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>

    public partial class Page1 : System.Windows.Controls.Page
    {

        Usuario us;

        public Page1()
        {
            InitializeComponent();
            
        }


        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            us = new Usuario();

            us.Rut = txtUser.Text;
            us.Password = (string)txtPass.Password;
            try
            {

                if (txtUser.Text.Length > 0 && txtPass.Password.Length > 0)
                {
                    //if (validarRut(txtUser.Text))
                    //{
                        string xml = us.Serializar();
                        WFBS.Presentation.ServiceWCF.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWCF.ServiceWFBSClient();

                        if (servicio.ValidarUsuario(xml))
                        {
                            us.Read();
                            if (servicio.Desactivado(xml))
                            {
                                Global.RutUsuario = us.Rut;
                                Global.NombreUsuario = us.Nombre;
                                Global.AreaInvestigacion = "Por definir";
                                Global.JefeUsuario = us.Jefe;
                                NavigationService navService = NavigationService.GetNavigationService(this);
                                Page2 nextPage = new Page2();
                                navService.Navigate(nextPage);
                            }
                            else
                            {
                                MessageBox.Show("La cuenta utilizada se encuentra Desactivada", "Alerta");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Rut o contraseña inválidos. Asegúrese de entrar con perfil de administrador al sistema", "Error!");
                        }

                    //}
                    //else
                    //{
                    //    MessageBox.Show("Debe ingresar un Rut valido", "Aviso");
                    //}
                }
                else
                {
                    MessageBox.Show("Debe ingresar su RUT y contraseña", "Alerta");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Surgieron inconvenientes al conectarse","Alerta");
            }

        }
        public bool validarRut(string rut)
        {

            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception )
            {
                return false;
            }
        }
    }
}