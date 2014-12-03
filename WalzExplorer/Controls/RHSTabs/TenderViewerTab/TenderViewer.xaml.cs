using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;


namespace WalzExplorer.Controls.RHSTabs.TenderViewerTab
{
    /// <summary>
    /// Interaction logic for TenderViewer.xaml
    /// </summary>
    public partial class TenderViewer : RHSTabContentBase
    {
        DataSet ds = new DataSet();

        public TenderViewer()
        {
            InitializeComponent();
           this.grd.AutoGeneratingColumn += new System.EventHandler<GridViewAutoGeneratingColumnEventArgs>(grd_AutoGeneratingColumn);
        }

        void grd_AutoGeneratingColumn(object sender, GridViewAutoGeneratingColumnEventArgs e)
        {
            string[] ColumnsNumber = { "TotalCost", "TotalHours", "TotalQuantity", "TotalCharge" };

            string[] ColumnsFormatTwoDecimal = { "TotalCost", "TotalHours", "TotalCharge" };

            GridViewDataColumn column = e.Column as GridViewDataColumn;

            if (column != null && ColumnsNumber.Contains(column.DataMemberBinding.Path.Path))
            {
                column.AggregateFunctions.Add(new SumFunction() { Caption = "=" });
                column.TextAlignment = TextAlignment.Right;
            }
            if (column != null && ColumnsNumber.Contains(column.DataMemberBinding.Path.Path))
            {

                column.DataFormatString = "#,###.00";

            }

        }

        public  override  void Update()
        {
            string CmdString = "";
            switch (node.TypeID)
            {
                case "Tender" :
                    CmdString = "SELECT * FROM [dbo].[fnTender.Activity_ListDetail] (" + node.ID + ") ORDER BY SchedCode,ActivityStructure, ItemStep";
                    break;
                case "TenderSchedule":
                    CmdString = "SELECT * FROM [dbo].[fnTender.Activity_ListDetail_GivenScheduleID]( " + node.ID + ") ORDER BY ActivityStructure, ItemStep";
                    break;
                case "TenderComponent":
                    CmdString = "SELECT * FROM [dbo].[fnTender.Activity_ListDetail_GivenActivityID]( " + node.ID + ") ORDER BY ActivityStructure, ItemStep";
                    break;
                default:
                    break;
            }
            string ConString = "Data Source=localhost;Initial Catalog=WalzExplorer;Integrated Security=SSPI;";
            
            
            using (SqlConnection con = new SqlConnection(ConString))
            {
                ds.Clear();
                SqlDataAdapter adapter = new SqlDataAdapter(CmdString, con);
                adapter.Fill(ds);
                grd.ItemsSource = ds;
                grd.Rebind();
            }
         
        }

        
       

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(grd.ItemsSource.ToString());
        }
    }
}
