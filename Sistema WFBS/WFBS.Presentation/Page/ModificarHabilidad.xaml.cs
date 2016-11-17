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
    /// Lógica de interacción para ModificarHabilidad.xaml
    /// </summary>
    public partial class ModificarHabilidad : System.Windows.Controls.Page
    {
        Collections col = new Collections();
        Habilidad hab = new Habilidad();
        int id_comp;
        public ModificarHabilidad()
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;
        }

        public ModificarHabilidad(int id, int id_com)
        {
            InitializeComponent();
            hab.Id_Habilidad = id;
            hab.Id_Hab = id;
            hab.Read();
            id_comp = id_com;

            txtId_Habilidad.Text = hab.Id_Hab.ToString();
            /* cmbId_Competencia.SelectedIndex = hab.Id_Competencia;*/
            txtNombre.Text = hab.Nombre;
            txtAlternativa.Text = hab.Alternativa_Pregunta;
        }

        private void cmbId_Competencia_Loaded(object sender, RoutedEventArgs e)
        {
            int select = 0, i = 0;
            List<Competencia> competencias = col.ReadAllCompetencias();
            foreach (Competencia item in competencias)
            {
                if (item.Id_com == hab.Id_Competencia)
                {
                    cmbId_Competencia.Items.Add(item.Nombre);
                    select = i;
                    i++;
                }
            }
            cmbId_Competencia.SelectedIndex = select;
            cmbId_Competencia.IsEnabled = false;

        }

        private void cmbOrden_Loaded(object sender, RoutedEventArgs e)
        {
            cmbOrden.Items.Add("0");
            cmbOrden.Items.Add("1");
            cmbOrden.Items.Add("2");
            cmbOrden.Items.Add("3");
            cmbOrden.Items.Add("4");
            cmbOrden.Items.Add("5");
            cmbOrden.SelectedIndex = hab.Orden_Asignado;
        }
        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            this.Limpiar();
        }

        private void Limpiar()
        {
            cmbId_Competencia.SelectedIndex = 0;
            txtNombre.Text = string.Empty;
            cmbOrden.SelectedIndex = 0;
            txtAlternativa.Text = string.Empty;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            List<Competencia> competencias = col.ReadAllCompetencias();
            try
            {
                Habilidad hab = new Habilidad();
                hab.Id_Habilidad = int.Parse(txtId_Habilidad.Text);
                hab.Id_Hab = int.Parse(txtId_Habilidad.Text);
                if (hab.Read())
                {
                    if (txtNombre.Text.Length > 0 && txtNombre.Text.Trim() != "")
                    {

                        hab.Nombre = txtNombre.Text;
                        #region Nivel
                        switch (cmbOrden.SelectedIndex)
                        {
                            case 0:
                                hab.Orden_Asignado = 0;
                                break;
                            case 1:
                                hab.Orden_Asignado = 1;
                                break;
                            case 2:
                                hab.Orden_Asignado = 2;
                                break;
                            case 3:
                                hab.Orden_Asignado = 3;
                                break;
                            case 4:
                                hab.Orden_Asignado = 4;
                                break;
                            case 5:
                                hab.Orden_Asignado = 5;
                                break;

                            default:
                                hab.Orden_Asignado = 0;
                                break;
                        }
                        #endregion Nivel
                        hab.Alternativa_Pregunta = txtAlternativa.Text;
                        hab.Id_Competencia = id_comp;
                        
                        string xml = hab.Serializar();
                        WFBS.Presentation.ServiceWCF.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWCF.ServiceWFBSClient();

                        if (servicio.ActualizarHabilidad(xml))
                        {
                            MessageBox.Show("Actualizado correctamente", "Éxito!");
                            NavigationService navService = NavigationService.GetNavigationService(this);
                            MantenedorHabilidades nextPage = new MantenedorHabilidades(id_comp);
                            navService.Navigate(nextPage);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar la Habilidad de Cargo, verifique que los datos sean correctos", "Aviso");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo Nombre es obligatorio", "Aviso");
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar los campos antes de continuar, verifique que los datos sean correctos", "Aviso");
                }

            }
            catch (Exception)
            {

                MessageBox.Show("No se ha podido actualizar la Habilidad!", "Alerta");
            }
        }
    }
}

