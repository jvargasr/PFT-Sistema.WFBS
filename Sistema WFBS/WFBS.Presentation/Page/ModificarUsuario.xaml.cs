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
    /// <summary>
    /// Lógica de interacción para ModificarUsuario.xaml
    /// </summary>
    public partial class ModificarUsuario : System.Windows.Controls.Page
    {
        Collections col = new Collections();
        Usuario us = new Usuario();
        public ModificarUsuario()
        {
            InitializeComponent();
        }

        public ModificarUsuario(string rut)
        {
            InitializeComponent();

            us.Rut = rut;
            us.Read();
            if (us.Sexo[0] == 'M')
                rbMasculino.IsChecked = true;
            else
                rbFemenino.IsChecked = true;
            txtNombre.Text = us.Nombre;
            txtPassword.Password = us.Password;
            txtPassword2.Password = us.Password;
            txtRut.Text = us.Rut;
            if (us.Obsoleto == 0)
                rbNoObsoleto.IsChecked = true;
            else
                rbSiObsoleto.IsChecked = true;
        }
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {

        }

        private void cmbArea_Loaded(object sender, RoutedEventArgs e)
        {
            int select = 0, i = 0;
            int var;
            List<Area> areas = col.ReadAllAreas();
            foreach (Area item in areas)
            {
                var = Convert.ToInt32(item.Id_area);
                cmbArea.Items.Add(item.area);
                if (var == us.Id_Area)
                    select = i;
                i++;
            }
            cmbArea.SelectedIndex = select;
        }
        private void cmbJefe_Loaded(object sender, RoutedEventArgs e)
        {
            int select = 0, i = 0;
            List<Usuario> jefes = col.ObtenerJefes();
            foreach (Usuario item in jefes)
            {
                cmbJefe.Items.Add(item.Nombre);
                if (item.Nombre == us.Jefe)
                    select = i;
                i++;
            }
            cmbJefe.SelectedIndex = select;
        }
        private void cmbPerfil_Loaded(object sender, RoutedEventArgs e)
        {
            int select = 0, i = 0;
            List<Perfil> perfiles = col.ReadAllPerfiles();
            foreach (Perfil item in perfiles)
            {
                cmbPerfil.Items.Add(item.perfil);
                if (item.id_pefil == us.Id_Perfil)
                    select = i;
                i++;
            }
            cmbPerfil.SelectedIndex = select;
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
            txtPassword.Password = string.Empty;
            txtPassword2.Password = string.Empty;
            rbMasculino.IsChecked = true;
            cmbPerfil.SelectedIndex = 2;
            rbNoObsoleto.IsChecked = true;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            List<Area> areas = col.ReadAllAreas();
            List<Perfil> perfiles = col.ReadAllPerfiles();
            try
            {
                Usuario us = new Usuario();
                us.Rut = txtRut.Text;
                if (us.Read())
                {
                    if (txtNombre.Text.Length > 0 && txtRut.Text.Length > 0 && txtPassword.Password.Length > 0)
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

                            if ((servicio.ActualizarUsuario(xml)))
                            {

                            }
                            else
                            {
                                MessageBox.Show("No se pudo actualizar el Usuario, verifique que los datos sean correctos", "Aviso");
                            }
                            us.Update();
                            MessageBox.Show("Actualizado correctamente", "Éxito!");
                            NavigationService navService = NavigationService.GetNavigationService(this);
                            MantenedorUsuarios nextPage = new MantenedorUsuarios();
                            navService.Navigate(nextPage);
                        }
                        else
                        {
                            MessageBox.Show("Las contraseñas ingresadas no coinciden", "Aviso");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe completar los campos antes de continuar", "Aviso");
                    }
                }
                else
                {
                    MessageBox.Show("El Usuario no existe", "Alerta");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("No se ha podido actualizar el Usuario!", "Alerta");
            }
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorUsuarios nextPage = new MantenedorUsuarios();
            navService.Navigate(nextPage);
        }
    }
}
