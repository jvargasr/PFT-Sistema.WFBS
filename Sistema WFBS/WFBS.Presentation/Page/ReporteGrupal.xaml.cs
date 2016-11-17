using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Lógica de interacción para ReporteGrupal.xaml
    /// </summary>
    public partial class ReporteGrupal : System.Windows.Controls.Page
    {
        List<PerfilesdeCargo> Perfiles = new List<PerfilesdeCargo>();
        public ReporteGrupal()
        {
            Collections col = new Collections();
            Logger.log("Se ingresa a reporte grupal");
            InitializeComponent();

            btnTodas.Visibility = Visibility.Hidden;
            lblUserInfo.Content = Global.NombreUsuario;
            lblUserInfo.Content = Global.NombreUsuario;

            Perfiles = col.ReadAllPerfilesdeCargo();
            TabControl1.ItemsSource = Perfiles;

        }
        private void myTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PerfilesdeCargo PerfilSeleccionado=(PerfilesdeCargo)TabControl1.SelectedItem;
            string[] areaspc = new string[] { "" };
            if (PerfilSeleccionado.id_areas != null)
            {
                areaspc = PerfilSeleccionado.id_areas.Split(',');
                this.dgEvaluaciones_Loaded(areaspc);

            }
        }
        private void dgEvaluaciones_Loaded(string[] areaspc)
        {
            Collections col = new Collections();
            //dgUsuarios.Columns[3].Visibility = Visibility.Collapsed;

            List<Area> areas = col.ReadAllAreas();
            List<Competencia> competencias = col.ReadAllCompetencias();
            List<float> brechas = new List<float>();
            List<PerfilesdeCargo> perfilesdecargo = new List<PerfilesdeCargo>();

            //Calcular cantidad máxima de notas, para definir el ancho de la tabla
            int nbrechas = 0;          
              
            foreach (Area a in areas)
            {
                if(areaspc.Contains(a.Id_area.ToString()))
                { 
                    foreach (Competencia com in competencias)
                    {
                        brechas = col.ObtenerNotasCompetencia((int)a.Id_area, (int)com.Id_com);
                        if (brechas.Count > nbrechas)
                            nbrechas = brechas.Count;

                    }
                }
            }

            //dar formato a la tabla
            DataTable table = new DataTable();
            DataColumn column;
            column = table.Columns.Add();
            column.ColumnName = "Cargo";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Competencia";
            column.DataType = typeof(string);
            for (int i = 0; i < nbrechas; i++)
            {
                column = table.Columns.Add();
                column.ColumnName = "N"+(i+1);
                column.DataType = typeof(string);
            }
            column = table.Columns.Add();
            column.ColumnName = "Brecha Promedio";
            column.DataType = typeof(string);
            //Final formato tabla

            //Listar resultados en la tabla
            DataRow row;
            float sumabrechas = 0;

            foreach (Area a in areas)
            {
                if (areaspc.Contains(a.Id_area.ToString()))
                {
                    foreach (Competencia com in competencias)
                    {
                        sumabrechas = 0;
                        brechas = col.ObtenerNotasCompetencia((int)a.Id_area, (int)com.Id_com);
                        if (brechas.Count > 0)
                        {
                            row = table.NewRow();
                            row["Cargo"] = a.area;
                            row["Competencia"] = com.Nombre;
                            for (int i = 0; i < brechas.Count; i++)
                            {
                                row["N" + (i + 1)] = brechas[i];
                                sumabrechas = brechas[i] + sumabrechas;
                            }
                            row["Brecha Promedio"] = (sumabrechas / brechas.Count).ToString("0.0");
                            table.Rows.Add(row);
                        }
                    }
                }
            }

            dgEvaluaciones.ItemsSource = table.AsDataView();

        }

        private void btnDescargar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgEvaluaciones.SelectAllCells();
                dgEvaluaciones.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, dgEvaluaciones);
                String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                String result = (string)Clipboard.GetData(DataFormats.Text);
                dgEvaluaciones.UnselectAllCells();
                string dir = @"C:\Reportes por grupo";
                if (!Directory.Exists(dir))
                {
                    DirectoryInfo di = Directory.CreateDirectory(dir);
                }
                string arc = @"C:\Reportes por grupo\ReporteGrupal.xls";
                int i = 1;
                while(File.Exists(arc))
                {
                    arc = @"C:\Reportes por grupo\ReporteGrupal" + i+".xls";
                    i++;
                }
                System.IO.StreamWriter file1 = new System.IO.StreamWriter(arc);
                file1.WriteLine(result.Replace(',', ' '));
                file1.Close();
                MessageBox.Show("El reporte se ha descargado en la carpeta Reportes de evaluaciones, ubicada en su disco duro");
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique que posea la carpeta 'Reportes de evaluaciones' en su disco C", "Alerta");
            }
        }

        private void btnBajoNivel_Click(object sender, RoutedEventArgs e)
        {
            Collections col = new Collections();
            //dgUsuarios.Columns[3].Visibility = Visibility.Collapsed;

            List<Area> areas = col.ReadAllAreas();
            List<Competencia> competencias = col.ReadAllCompetencias();
            List<float> brechas = new List<float>();

            //Calcular cantidad máxima de notas, para definir el ancho de la tabla
            int nbrechas = 0;
            foreach (Area a in areas)
            {
                foreach (Competencia com in competencias)
                {
                    brechas = col.ObtenerNotasCompetencia((int)a.Id_area, (int)com.Id_com);
                    if (brechas.Count > nbrechas)
                        nbrechas = brechas.Count;

                }
            }

            //dar formato a la tabla
            DataTable table = new DataTable();
            DataColumn column;
            column = table.Columns.Add();
            column.ColumnName = "Cargo";
            column.DataType = typeof(string);

            column = table.Columns.Add();
            column.ColumnName = "Competencia";
            column.DataType = typeof(string);
            for (int i = 0; i < nbrechas; i++)
            {
                column = table.Columns.Add();
                column.ColumnName = "N" + (i + 1);
                column.DataType = typeof(string);
            }
            column = table.Columns.Add();
            column.ColumnName = "Brecha Promedio";
            column.DataType = typeof(string);
            //Final formato tabla

            //Listar resultados en la tabla
            DataRow row;
            float sumabrechas = 0;
            foreach (Area a in areas)
            {
                foreach (Competencia com in competencias)
                {
                    sumabrechas = 0;
                    brechas = col.ObtenerNotasCompetencia((int)a.Id_area, (int)com.Id_com);
                    if (brechas.Count > 0)
                    {
                        row = table.NewRow();
                        row["Cargo"] = a.area;
                        row["Competencia"] = com.Nombre;
                        for (int i = 0; i < brechas.Count; i++)
                        {
                            row["N" + (i + 1)] = brechas[i];
                            sumabrechas = brechas[i] + sumabrechas;
                        }
                        row["Brecha Promedio"] = (sumabrechas / brechas.Count).ToString("0.0");
                        if((sumabrechas / brechas.Count)<com.Nivel_Optimo)
                        table.Rows.Add(row);
                    }
                }
            }
            btnTodas.Visibility = Visibility.Visible;
            dgEvaluaciones.ItemsSource = table.AsDataView();
        }
        private void btnTodas_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            ReporteGrupal nextPage = new ReporteGrupal();
            navService.Navigate(nextPage);
        }
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Page2 nextPage = new Page2();
            navService.Navigate(nextPage);
        }
    }
}
