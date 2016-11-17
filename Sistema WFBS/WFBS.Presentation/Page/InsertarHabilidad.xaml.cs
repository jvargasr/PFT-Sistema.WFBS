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
    /// Lógica de interacción para InsertarHabilidad.xaml
    /// </summary>
    public partial class InsertarHabilidad : System.Windows.Controls.Page
    {
        //Collections col = new Collections();
        ColeccionCompetencia colCom = new ColeccionCompetencia();
        Habilidad hab = new Habilidad();
        int id_comp;
        public InsertarHabilidad(int id_com)
        {
            InitializeComponent();
            lblUserInfo.Content = Global.NombreUsuario;

            id_comp = id_com;

        }

        private void cmbId_Competencia_Loaded(object sender, RoutedEventArgs e)
        {
            int select = 0, i = 0;
            List<Competencia> competencias = colCom.ReadAllCompetencias();
            foreach (Competencia item in competencias)
            {
                if (item.Id_competencia == id_comp)
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

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            List<Competencia> competencias = colCom.ReadAllCompetencias();
            try
            {
                Habilidad hab = new Habilidad();
                if (txtNombre.Text.Length > 0 && txtNombre.Text.Trim() != "")
                {
                    foreach (Competencia c in competencias)
                    {
                        if (c.Nombre == (string)cmbId_Competencia.SelectedItem)
                        {
                            hab.Id_Competencia = Convert.ToInt32(c.Id_competencia);
                        }
                    }
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

                    string xml = hab.Serializar();
                    WFBS.Presentation.ServiceWCF.ServiceWFBSClient servicio = new WFBS.Presentation.ServiceWCF.ServiceWFBSClient();

                    if (servicio.CrearHabilidad(xml))
                    {
                        MessageBox.Show("Habilidad agregada correctamente", "Éxito!");
                        this.Limpiar();
                        NavigationService navService = NavigationService.GetNavigationService(this);
                        MantenedorHabilidades nextPage = new MantenedorHabilidades(hab.Id_Competencia);
                        navService.Navigate(nextPage);
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar la Habilidad, verifique que los datos sean correctos", "Aviso");

                    }
                }
                else
                {
                    MessageBox.Show("El campo Descripción es obligatorio", "Aviso");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo agregar la Habilidad!", "Alerta");
            }
        }
        
    }
}
