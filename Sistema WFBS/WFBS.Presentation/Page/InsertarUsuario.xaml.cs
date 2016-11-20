using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// <summary>holachao
    /// Lógica de interacción para InsertarUsuario.xaml
    /// </summary>
    public partial class InsertarUsuario : System.Windows.Controls.Page
    {
        ColeccionUsuario colUs = new ColeccionUsuario();
        Collections col = new Collections(); //<--- readllAreas
        public InsertarUsuario()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
            rbMasculino.IsChecked = true;
            rbNoObsoleto.IsChecked = true;
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
        }
        private void cmbArea_Loaded(object sender, RoutedEventArgs e)
        {
            List<Area> areas = col.ReadAllAreas();
            foreach (Area item in areas)
            {
                cmbArea.Items.Add(item.area);
            }
            cmbArea.SelectedIndex = 0;
        }
        private void cmbJefe_Loaded(object sender, RoutedEventArgs e)
        {
            List<Usuario> jefes = col.ObtenerJefes();
            foreach (Usuario item in jefes)
            {
                cmbJefe.Items.Add(item.Nombre);
            }
            cmbJefe.SelectedIndex = 0;
        }
        private void cmbPerfil_Loaded(object sender, RoutedEventArgs e)
        {
            List<Perfil> perfiles = col.ReadAllPerfiles();
            foreach (Perfil item in perfiles)
            {
                cmbPerfil.Items.Add(item.perfil);
            }
            cmbPerfil.SelectedIndex = 2;
        }

        private void cmbPerfil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPerfil.SelectedIndex != 2)
            {
                lbJefe.Visibility = Visibility.Hidden;
                cmbJefe.IsEnabled = false;
                cmbJefe.Visibility = Visibility.Hidden;
            }
            else
            {
                cmbJefe.IsEnabled = true;
                cmbJefe.Visibility = Visibility.Visible;
                lbJefe.Visibility = Visibility.Visible;
            }
            if (cmbPerfil.SelectedIndex == 0)
            {
                lbArea.Visibility = Visibility.Hidden;
                cmbArea.IsEnabled = false;
                cmbArea.Visibility = Visibility.Hidden;
            }
            else
            {
                lbArea.Visibility = Visibility.Visible;
                cmbArea.IsEnabled = true;
                cmbArea.Visibility = Visibility.Visible;
            }
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
        }

        private void Limpiar()
        {
            txtNombre.Text = string.Empty;
            txtRut.Text = string.Empty;
            txtPassword.Password = string.Empty;
            txtPassword2.Password = string.Empty;
            rbMasculino.IsChecked = true;
            cmbPerfil.SelectedIndex = 2;
            rbNoObsoleto.IsChecked = true;
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            List<Area> areas = col.ReadAllAreas();
            List<Perfil> perfiles = col.ReadAllPerfiles();
            try
            {
                Usuario us = new Usuario();
                us.Rut = txtRut.Text;
                if (!us.Read())
                {
                    if (txtNombre.Text.Length > 0 && txtRut.Text.Length > 0 && txtPassword.Password.Length > 0)
                    {
                        if (validarRut(txtRut.Text))
                        {
                            if (txtPassword.Password == txtPassword2.Password)
                            {
                                us.Nombre = txtNombre.Text;
                                us.Password = txtPassword.Password;
                                if (cmbPerfil.SelectedIndex == 2)
                                    us.Jefe = cmbJefe.SelectedItem.ToString();
                                else
                                    us.Jefe = "";
                                if (rbFemenino.IsChecked == true)
                                    us.Sexo = "F";
                                if (rbMasculino.IsChecked == true)
                                    us.Sexo = "M";

                                foreach (Area a in areas)
                                {
                                    if (a.area == (string)cmbArea.SelectedItem)
                                    {
                                        us.Id_Area = Convert.ToInt32(a.Id_area);
                                    }
                                }
                                foreach (Perfil p in perfiles)
                                {
                                    if (p.perfil == (string)cmbPerfil.SelectedItem)
                                    {
                                        us.Id_Perfil = p.id_pefil;
                                    }
                                }
                                if (rbNoObsoleto.IsChecked == true)
                                    us.Obsoleto = 0;
                                if (rbSiObsoleto.IsChecked == true)
                                    us.Obsoleto = 1;

                                string xml = us.Serializar();
                                WFBS.Presentation.ServiceWCF.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWCF.ServiceWFBSClient();

                                if (servicio.CrearUsuario(xml))
                                {
                                    MessageBox.Show("Agregado correctamente", "Éxito!");
                                    this.Limpiar();
                                    NavigationService navService = NavigationService.GetNavigationService(this);
                                    MantenedorUsuarios nextPage = new MantenedorUsuarios();
                                    navService.Navigate(nextPage);
                                }
                                else
                                {
                                    MessageBox.Show("No se pudo agregar el Usuario, verifique que los datos sean correctos", "Aviso");

                                }

                            }
                            else
                            {
                                MessageBox.Show("Las contraseñas no coinciden", "Aviso");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Debe ingresar un Rut valido", "Aviso");
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("Debe completar los campos antes de ingresar", "Aviso");
                    }
                }
                else
                {
                    MessageBox.Show("El rut ingresado ya posee un cuenta", "Aviso!");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("No se pudo agregar el Usuario!", "Alerta");
            }
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorUsuarios nextPage = new MantenedorUsuarios();
            navService.Navigate(nextPage);
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
            catch (Exception)
            {
                return false;
            }
        }
    }
}
