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
    /// Lógica de interacción para IngresarPerfildeCargo.xaml
    /// </summary>
    public partial class IngresarPerfildeCargo : System.Windows.Controls.Page
    {
        WFBS.Business.Core.Collections col = new Collections();
        List<PerfilesdeCargo> perfiles = new List<PerfilesdeCargo>();
        List<Area> areas = new List<Area>();
        public IngresarPerfildeCargo()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
            areas = col.ReadAllAreas();
            foreach (Area item in areas)
            {
                if (item.obs == "No")
                {
                    lbArea.Items.Add(item.area);
                }
            }
            rbNoObsoleto.IsChecked = true;
        }
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
        }
        private void btnToRight_Click(object sender, RoutedEventArgs e)
        {
            lbAreaSeleccionadas.Items.Add(lbArea.SelectedItem);
            lbArea.Items.Remove(lbArea.SelectedItem);
            lbAreaSeleccionadas.Items.Refresh();
            lbArea.Items.Refresh();
            
        }
        private void btnToLeft_Click(object sender, RoutedEventArgs e)
        {
            lbArea.Items.Add(lbAreaSeleccionadas.SelectedItem);
            lbAreaSeleccionadas.Items.Remove(lbAreaSeleccionadas.SelectedItem);
            lbArea.Items.Refresh();            
            lbAreaSeleccionadas.Items.Refresh();

        }
        private void btnAgregarPerfildeCargo_click(object sender, RoutedEventArgs e)
        {
            List<Area> areasSelec = new List<Area>();
            areas = col.ReadAllAreas();
            PerfilesdeCargo pc = new PerfilesdeCargo();
            foreach (string item in lbAreaSeleccionadas.Items)
            {
                foreach (Area a in areas)
                {
                    if(a.area==item)
                    {
                        areasSelec.Add(a);
                    }
                }
            }
            if (lbAreaSeleccionadas.Items.Count == 0)
            {
                MessageBox.Show("Debe seleccionar las Áreas para el Perfil");
            }
            else
            {
                if (txtDescripcion.Text.Length == 0)
                {
                    MessageBox.Show("Debe ingresar una descripción");
                }
                else
                {
                    try
                    {
                        pc.descripcion = txtDescripcion.Text;
                        if (rbNoObsoleto.IsChecked == true)
                            pc.Obsoleto = 0;
                        if (rbSiObsoleto.IsChecked == true)
                            pc.Obsoleto = 1;

                        if (pc.Create(areasSelec))
                        {
                            MessageBox.Show("Agregado correctamente", "Éxito!");
                                NavigationService navService = NavigationService.GetNavigationService(this);
                                MantenedorPerfilesdeCargo nextPage = new MantenedorPerfilesdeCargo();
                                navService.Navigate(nextPage);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar el Perfil de Cargo, verifique que los datos sean correctos", "Aviso");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No se pudo agregar el Perfil de Cargo!", "Alerta");
                    }
                }
            }
        }
        private void Limpiar()
        {
            txtDescripcion.Text = string.Empty;
            rbNoObsoleto.IsChecked = true;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorPerfilesdeCargo nextPage = new MantenedorPerfilesdeCargo();
            navService.Navigate(nextPage);
        }
    }
}
