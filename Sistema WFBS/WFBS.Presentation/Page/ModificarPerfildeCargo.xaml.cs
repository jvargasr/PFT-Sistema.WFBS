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
    public partial class ModificarPerfildeCargo : System.Windows.Controls.Page
    {
        Collections col = new Collections();
        List<Area> areas = new List<Area>();
        PerfilesdeCargo pc = new PerfilesdeCargo();
        public ModificarPerfildeCargo(int id)
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
            areas = col.ReadAllAreas();
            pc.id_perfil_de_cargo = id;
            pc.Id_PC = id;
            pc.Read();
            string[] areaspc = new string[] {""};
            if(pc.id_areas!=null)
                areaspc = pc.id_areas.Split(',');
            foreach (Area item in areas)
            {
                if (item.obs == "No")
                {
                    if (areaspc.Contains(item.Id_area.ToString()))
                    {
                        lbAreaSeleccionadas.Items.Add(item.area);
                    }
                    else
                    {
                        lbArea.Items.Add(item.area);
                    }
                }
            }
            txtDescripcion.Text = pc.descripcion;
            if (pc.Obsoleto == 0)
                rbNoObsoleto.IsChecked = true;
            else
                rbSiObsoleto.IsChecked = true;

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
        private void Limpiar()
        {
            txtDescripcion.Text = string.Empty;
            rbNoObsoleto.IsChecked = true;
        }
        private void btnModificarPerfildeCargo_click(object sender, RoutedEventArgs e)
        {
            List<Area> areasSelec = new List<Area>();
            areas = col.ReadAllAreas();
            foreach (string item in lbAreaSeleccionadas.Items)
            {
                foreach (Area a in areas)
                {
                    if (a.area == item)
                    {
                        areasSelec.Add(a);
                    }
                }
            }
            if (lbAreaSeleccionadas.Items.Count == 0)
            {
                MessageBox.Show("Debe seleccionar las áreas para el Perfil de Cargo", "Aviso");
            }
            else
            {
                if (txtDescripcion.Text.Length == 0)
                {
                    MessageBox.Show("Debe ingresar una descripción", "Aviso");
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
                        if (pc.Update(areasSelec))
                        {
                                MessageBox.Show("Actualizado correctamente", "Éxito!");
                                NavigationService navService = NavigationService.GetNavigationService(this);
                                MantenedorPerfilesdeCargo nextPage = new MantenedorPerfilesdeCargo();
                                navService.Navigate(nextPage);              
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar el Perfil de Cargo, verifique que los datos sean correctos", "Aviso");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No se pudo actualizar el Perfil de Cargo!", "Alerta");
                    }
                }
            }
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            MantenedorPerfilesdeCargo nextPage = new MantenedorPerfilesdeCargo();
            navService.Navigate(nextPage);
        }
    }
}
