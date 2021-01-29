
using DynamicDatagridColumns.Model_ViewModels;
using SharedResx_NetCore;
using SharedResx_NetStandard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;

//using System.Xaml;
//using System.Xml;
//using System.Xml.Linq;

namespace DynamicDatagridColumns.Views
{
    /// <summary>
    /// Interaction logic for MainWindow2.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var dgColumnStyle = (Style)_viewsDatagrid.FindResource("ColumnHeaderStyle1");

            var pv = this.PattnVw = new PattnViewModel("3-8");
            DataContext = pv.WorkBkItems;


        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            if (_viewsDatagrid._tabControl.SelectedItem == null)
            {
                if (!(_viewsDatagrid._tabControl.ItemContainerGenerator.ContainerFromIndex(0) is TabItem tabItem))
                    return;//[index];

                tabItem.IsSelected = true;
            }
        }
        //private void ChkUnifyColumns_Click(object sender, RoutedEventArgs e)
        //{
        //    //var index = _viewsDatagrid._tabControl.SelectedIndex;
        //    var msg = "activated!";
        //    if (!(bool)chkUnifyColumns.IsChecked)
        //    {
        //        var index = _viewsDatagrid._tabControl.SelectedIndex;
        //        _viewsDatagrid._tabControl.SelectedIndex = -1;
        //        msg = "de-" + msg.Replace("!", "");

        //        var tabItem = _viewsDatagrid._tabControl.ItemContainerGenerator.ContainerFromIndex(index) as TabItem;//[index];
        //        tabItem.IsSelected = true;

        //    }
        //    MessageBox.Show(this, "Colums' uniformity has been " + msg, "Columns Uniformity Changed (" + msg.ToUpper() + ")", MessageBoxButton.OK, MessageBoxImage.Information);

        //}

        public PattnViewModel PattnVw { get; set; }

        private static string DataTemplateModule { get => CachedContentPresenter.ColumTemplateModule.UpdatedModule; }
        private static DynamicColumsModule ColumTemplateModule { get; set; } = ColumTemplateModule ?? new DynamicColumsModule(CachedContentPresenter.CachedContent);


        private void BtnAddTab_Click(object sender, RoutedEventArgs e)
        {

            var criteria = _txNewTab.Text;

            if (criteria == null || criteria == string.Empty)
                return;

            var viewModelData = PattnVw.WorkBkItems;
            //var i = viewModelData.Count;
            var pattn = PattnVw.CollatePattn(criteria);

            foreach (var wkSheet in pattn)
                viewModelData.Add(wkSheet);

            //var dPropt = DgContentTempalate.LoadContent();
            //var resx = DgContentTempalate.Resources;
            //TemplateContent cont = DgContentTempalate.Template;
            //FrameworkElementFactory fwFactory = DgContentTempalate.VisualTree;
            ////fwFactory.SetResourceReference(DataGrid.ColumnHeaderStyleProperty, DgColumnStyle);

        }
        private void BtnAddColumn_Click(object sender, RoutedEventArgs e)
        {

            if (!(_combNewColumn.Text.Trim().Split(new char[] { ',' },
                StringSplitOptions.RemoveEmptyEntries) is string[] colArr))
                return;

            var colmName = _combNewColumn.Text.Trim();

            if (colmName == "" || CachedContentPresenter.ColumTemplateModule == null)
                return;

            var templ = GetCachedTemplate(DataTemplateModule, new string[] { colmName });/// colArr);

            if (templ == null) return;
            var selectedTab = _viewsDatagrid._tabControl.SelectedIndex;

            _viewsDatagrid._tabControl.SelectedItem = null;
            //CachedContentPresenter.UpdatedTemplate = templ;
            //CachedContentPresenter.Updating = true;
            _viewsDatagrid._tabControl.SelectedIndex = selectedTab;

            DependencyProperty dp = CachedContentPresenter.DataTemplateProperty;
        }

        public DataTemplate GetCachedTemplate(string selectedModule, string[] collumnNames)
        {

            if (selectedModule == null)
                return null;
            var colmCollection = ((WkSheetViewModel)CachedContentPresenter.SelectedItem).SheetData.PattnPageList;//[0] as PrintFormat;
            var datStr = ColumTemplateModule.UpdateElementModule(collumnNames, colmCollection.ToArray(), (bool)!chkUnifyColumns.IsChecked);

            var stringReader = new StringReader(datStr);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            try
            {
                var dataTemplat = XamlReader.Load(xmlReader) as DataTemplate;
                return dataTemplat;
            }
            catch (Exception ex)
            {
                //goto rpt;
                return null;
            }


        }
    }

    public static class ConstantStr
    {
        public readonly static string Dummy = @"<Dummy />";
        readonly static string sc = Stat.Septor.ToString();
        public static readonly string Nme = sc + "name" + sc;
        public static readonly string Val = sc + "val" + sc;
        public static readonly string DummyText = @"<DataGridTextColumn Header=""" + Nme + @""" Binding=""{Binding " + Val + @"}""/>";
    }

    //public class DynamicColumsModule
    //{
    //    readonly static string sc = Stat.Septor.ToString();
    //    private static readonly string Nme = sc + "name" + sc;
    //    private static readonly string Val = sc + "val" + sc;
    //    private static readonly string Custm = sc + "custm" + sc;
    //    public readonly static string Dummy = @"<Dummy />";//DnD: Single space is require before the slash
    //    private static readonly string Formt = sc + "fmt" + sc;


    //    //public XmlElement RefedNamespaces { set; get; }
    //    //public Type CustomWrapperType { get; set; }
    //    //public static readonly string Assemb = sc + "assemb" + sc;


    //    public string UpdatedModule { get; private set; }
    //    public string BaseModule { get; private set; }

    //    public XmlReader TemplateData { get; private set; }

    //    public DynamicColumsModule(CachedContentPresenter cachedPresenter)
    //    {
    //        if (cachedPresenter == null)
    //            return;
    //        var type = cachedPresenter.GetType();
    //        var assemb = type.Assembly;
    //        var nameSpace = type.Namespace;
    //        //ModularTemplate=""{Binding ColumNames}""
    //        string datType =
    //             @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
    //                    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
    //                    xmlns:local=""clr-namespace:" + nameSpace + ";assembly=" + assemb + @""" 
    //                    DataType=""{x:Type local:" + type.Name + @"}"">
    //                <local:" + type.Name + @" x:Name=""_tabContentMgr""  >
    //                    <DataTemplate>
    //                        <Border x:Name=""_templateBorder"" >
    //                           " + ConstString.Dummy + @"
    //                        </Border>
    //                    </DataTemplate>
    //                </local:" + type.Name + @" >
    //            </DataTemplate>";
    //        var sourceName = cachedPresenter.InitDat;
    //        var visualNode = @"<DataGrid x:Name=""_TemplateDgrid"" ItemsSource=""{Binding " + sourceName + @"}"" AutoGenerateColumns=""False"" >
    //                                <DataGrid.Columns >
    //                                " + ConstString.Dummy + @"
    //                                </DataGrid.Columns>
    //                          </DataGrid>";

    //        var iniStr = datType.Replace(ConstString.Dummy, visualNode);

    //        //var t = dummy.Replace(name, e[0]).Replace(value, e[1]);
    //        //iniStr = iniStr.Replace(ConstString.Dummy, t.Trim() + "\n" + ConstString.Dummy);


    //        var datStr = iniStr.Replace(ConstString.Dummy, "");

    //        //rpt:
    //        var stringReader = new StringReader(datStr);
    //        this.TemplateData = XmlReader.Create(stringReader);
    //        try
    //        {
    //            //var dataTemplat = XamlReader.Load(this.TemplateData) as DataTemplate;

    //            var templXml = XElement.Parse(iniStr);
    //            this.UpdatedModule = this.BaseModule = ((XElement)templXml.FirstNode).FirstNode.ToString();

    //            //goto rpt;
    //            //return dataTemplat;
    //        }
    //        catch (Exception ex)
    //        {
    //            //goto rpt;
    //            return;
    //        }



    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    //public readonly string DummyText = @"<DataGridTextColumn Header=""" + Nme + @""" Binding=""{Binding " + Val + @"}""/>";



    //    /// <summary>
    //    /// /
    //    /// </summary>
    //    /// <param name="type"></param>
    //    /// <param name="attribs">The the 3rd element should be null excerpt when used to
    //    /// hold custom type's name i.e. ("custom:MyCustomClass") which will also indicates
    //    /// [default:] as the swith case options </param>
    //    /// <returns></returns>
    //    public static string GetContexModul(Type type, string[] attribs)
    //    {
    //        var arr = attribs;// new string[3];
    //        //Array.Copy(attribs, arr, attribs.Length);
    //        ////arr[1] =arr[1]==""?

    //        //return null;
    //        string module, typName, assemb;
    //        typName = assemb = "";
    //        //var assemb = "";
    //        //var typName = type.Name;

    //        if (arr[2] != null && arr[2].Trim() != "")
    //            assemb = @" xmlns:custom=""clr-namespace:" + type.Namespace + ";assembly=" + type.Assembly + @"""";
    //        else typName = type.Name;

    //        switch (typName)
    //        {
    //            case "Boolean":
    //                arr[2] = arr[2] ?? "DataGridCheckBoxColumn";
    //                module = @"<" + Custm + @" Header=""" + Nme + @""" Binding=""{Binding " + Val + @"}""" + assemb + @" />";
    //                break;

    //            case "ListCollectionView":
    //                arr[2] = arr[2] ?? "ComboBox";
    //                module = @"<DataGridTemplateColumn Header=""" + Nme + @""" >
    //                            <DataGridTemplateColumn.CellTemplate  >
    //                                        <DataTemplate  >
    //                                            <" + Custm + @"  ItemsSource=""{Binding " + Val + @"}""" + assemb + @"" + assemb + @" />
    //                                        </DataTemplate>
    //                                    </DataGridTemplateColumn.CellTemplate>
    //                        </DataGridTemplateColumn>";
    //                break;

    //            case "String":
    //            case "Double":
    //            case "Decimal":
    //                arr[2] = arr[2] ?? "DataGridTextColumn";
    //                module = @"<" + Custm + @" Header=""" + Nme + @""" Binding=""{Binding " + Val + Formt + @"}""" + assemb + @"  />";
    //                switch (typName)
    //                {
    //                    case "Double":
    //                        module = module.Replace(Formt, @", StringFormat=N2");
    //                        break;
    //                    case "Decimal":
    //                        module = module.Replace(Formt, @", StringFormat=F3");
    //                        break;
    //                    default:
    //                        module = module.Replace(Formt, "");
    //                        break;
    //                }
    //                break;

    //            default:
    //                ///DnD arr[2] is preloaded
    //                ///DnD Custm is to represent custom class. Expected Format example: "local:MyCustomClass" 
    //                module = @"<DataGridTemplateColumn Header=""" + Nme + @""" Width=""100""" + assemb + @" >
    //                            <DataGridTemplateColumn.CellTemplate>
    //                                <DataTemplate>
    //                                    <" + Custm + @" Height=""17""  Rating=""{Binding " + Val + @"}""  />
    //                                </DataTemplate>
    //                            </DataGridTemplateColumn.CellTemplate>
    //                        </DataGridTemplateColumn>";
    //                break;

    //        }
    //        return module.Replace(Nme, arr[0]).Replace(Val, arr[1]).Replace(Custm, arr[2]);
    //    }



    //    public string UpdateElementModule(string[] columnsIDs, object[] collection, bool cummulative)
    //    {
    //        var baseModule = this.BaseModule;
    //        var existingSiblings = new List<string>();
    //        if (cummulative)
    //        {
    //            ///DnD In order to avoid duplications, it's important to first
    //            /// accertain which Columns are already contained in the BaseModule
    //            baseModule = this.UpdatedModule;
    //            var xl = XElement.Parse(baseModule);
    //            ///DnD Identify the location of the replate ment tag, <dummy />. Note that its
    //            ///string form's whitespaces could have changed hence the verbose conversion
    //            var dumName = XElement.Parse(ConstString.Dummy).Name.LocalName;
    //            while (xl != null && xl.Elements().Where(s => s.Name.LocalName == dumName).FirstOrDefault() == null)
    //                xl = xl.FirstNode as XElement;

    //            if (xl == null) return null;

    //            existingSiblings = xl.Elements().Select(n => (n.Attributes().Where(a
    //               => a.Name.LocalName.ToLower() == "header").FirstOrDefault())).Where(a
    //               => a != null).Select(a => a.Value).ToList<String>();
    //        }
    //        var iniStr = baseModule;
    //        foreach (var collumnName in columnsIDs)
    //        {
    //            string[] args = new string[] { collumnName, EditText(collumnName, "Col")/*.Replace("/", "")*/, null };

    //            var colmItems = collection[0];// ((WkSheetModel)CachedContentPresenter.SelectedItem).PattnPageList[0] as PrintFormat;

    //            var objName = colmItems.GetType().GetProperties().Where(p => p.Name.ToLower() == args[1].ToLower())
    //                .FirstOrDefault();

    //            if ((collumnName == Xt.Rating) is bool isRating && isRating)
    //            {
    //                args[0] = Xt.Consistence;
    //                args[2] = Xt.custom + RatingControl.ToString();
    //            }

    //            var type = isRating ? typeof(RatingControl) :
    //                (objName != null ? objName.PropertyType : typeof(string));

    //            var newCollm = DynamicColumsModule.GetContexModul(type, args);

    //            /* ///DnD ToDo: GUI_Settings, forestores columns duplication */
    //            if (existingSiblings.Contains(args[0]))
    //                continue;
    //            else existingSiblings.Add(args[0]);

    //            iniStr = iniStr.Replace(ConstString.Dummy, newCollm.Trim() + "\n" + ConstString.Dummy);
    //        }
    //        if (cummulative)
    //            this.UpdatedModule = iniStr;
    //        return iniStr.Replace(Dummy, "");
    //    }

    //    private static string EditText(string inpStr, string replaceWord)
    //    {
    //        string str = "";

    //        if (inpStr.IndexOfAny(CharCheck.SymbolChars) is int i && i < 0)
    //            str = inpStr;
    //        else
    //        if (inpStr.Remove(i) is string i2 && i2.Length > 3)
    //            str = i2;
    //        else
    //        {
    //            var chs = inpStr.Where(t => CharCheck.SymbolChars.ToList().IndexOf(t) < 0).ToArray();// !CharCheck.SymbolChars.Contains((char)t).ToString()).ToArray());
    //            str = chs.Aggregate("", (a, b) => a += b.ToString());

    //        }
    //        if (str.Length < 3)
    //            str = replaceWord + "C " + Stat.AnnualToken.Replace("_", "");

    //        return str.Trim();
    //    }

    //    //    Aggregate("0", (last, next) => Convert.ToInt64(next.Replace("_", "")) >
    //    //            Convert.ToInt64(last.Replace("_", "")) ? next : last);
    //    //fontSizes.Aggregate(0.0, (longest, next) => next > longest? next : longest)
    //}
}
//var strDat = @"<DataTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
//   < " + type.Name + @" x:Name = ""_tabContentMgr"" >

//        < DataTemplate >

//            < Border x: Name = ""_dgBorderTemplate"" >

//                  < DataGrid x: Name = ""_dgTemplate""  ItemsSource = ""{Binding PattnPageList}""   AutoGenerateColumns = ""False"" >

//                            < DataGrid.Columns >

//                                < DataGridTextColumn Header = ""S/No"" Binding = ""{Binding SNo}"" />

//                                   < DataGridTextColumn Header = ""Names"" Binding = ""{Binding Names}"" />

//                                      < DataGridTextColumn Header = ""Net"" Binding = ""{Binding Net}"" />

//                                         < !--< DataGridTextColumn Header = ""PPID"" Binding = ""{Binding PPID}"" /> -->

//                                         </ DataGrid.Columns >

//                                     </ DataGrid >

//                                 </ Border >

//                             </ DataTemplate >

//                         </ local:CachedContentPresenter >

//                      </ DataTemplate >";



//private static string TemplateModule { get; set; }
//private static string DataGridNodes(System.Xml.Linq.XElement firstNode, string[] inpExl)
//{

//    var sc = Stat.Septor;
//    string name = sc + "name" + sc;
//    string value = sc + "val" + sc;

//    ///DnD name and value can be replace sepetately if need be
//    var dummy = @"<DataGridTextColumn Header=""" + name + @""" Binding=""{Binding " + value + @"}""/>";
//    string iniStr =
//        @" <Border x:Name=""_dgBorderTemplate"">
//            <DataGrid x:Name=""_dgTemplate"" ItemsSource=""{Binding PattnPageList}"" AutoGenerateColumns=""False"">
//                <DataGrid.Columns>
//                    <DataGridTextColumn Header=""S/No"" Binding=""{Binding SNo}""/>
//                    <DataGridTextColumn Header=""Names"" Binding=""{Binding Names}""/>
//                    <DataGridTextColumn Header=""Net"" Binding=""{Binding Net}""/>
//                    <DataGridTextColumn Header=""Debit"" Binding=""{Binding Gross}""/>
//                    " + dummy + @"
//                </DataGrid.Columns>                      
//            </DataGrid>    
//        </Border> ";

//    ///DnD the invalid replacement to be extended to include others 
//    var dualArr = inpExl.Select(s => (s + "\n" + s.Replace("/", ""))
//        .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();

//    foreach (var e in dualArr)
//    {
//        var t = dummy.Replace(name, e[0]).Replace(value, e[1]) + "\r\n" + dummy;
//        iniStr = iniStr.Replace(dummy, t);
//    }
//    iniStr = iniStr.Replace(dummy, "");

//    if (firstNode != null && firstNode.Value != "")
//    {
//        firstNode.Value = iniStr;
//        iniStr = firstNode.ToString();
//    }

//    return iniStr;
//}


//public static object TestTemp;
//private DataTemplate ConcatXaml(Type type, string inpExl)
//      {
//          //var type = typeof(CachedContentPresenter);

//          var assemb = type.Assembly.GetName();
//          var nameSpace = type.Namespace;

//          //var sc = Stat.Septor;
//          //string name = sc + "name" + sc;
//          //string value = sc + "val" + sc;

//          ///DnD name and value can be replace sepetately if need be
//          //var dummy = @"<DataGridTextColumn Header=""" + name + @""" Binding=""{Binding " + value + @"}""/>";
//          var dummy = inpExl;
//          string iniStr =
//          @"<DataTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
//                  xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
//                  xmlns:local=""clr-namespace:" + nameSpace + ";assembly=" + assemb + @""" 
//                  DataType=""{x:Type local:" + type.Name + @"}"" > 
//                  " + dummy + @"
//          </DataTemplate>";
//          //@"<DataTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
//          //         xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
//          //         xmlns:local=""clr-namespace:" + nameSpace + ";assembly=" + assemb + @""" 
//          //         DataType=""{x:Type TabControl}"" >                    
//          //         <local:" + type.Name + @" x:Name=""_tabContentMgr"" >
//          //             <DataTemplate>
//          //                 <Border x:Name=""_dgBorderTemplate"">
//          //                     <DataGrid x:Name=""_dgTemplate"" ItemsSource=""{Binding PattnPageList}"" AutoGenerateColumns="" False"">
//          //                         <DataGrid.Columns>
//          //                             <DataGridTextColumn Header=""S/No"" Binding=""{Binding SNo}""/>
//          //                             <DataGridTextColumn Header=""Names"" Binding=""{Binding Names}""/>
//          //                             <DataGridTextColumn Header=""Net"" Binding=""{Binding Net}""/>
//          //                             <DataGridTextColumn Header=""Gross"" Binding=""{Binding Gross}""/>
//          //                             <DataGridTextColumn Header=""Debit"" Binding=""{Binding Gross}""/>
//          //                             " + dummy + @"
//          //                         </DataGrid.Columns>                      
//          //                     </DataGrid>    
//          //                 </Border> 
//          //             </DataTemplate>
//          //         </local:" + type.Name + @">                      
//          // </DataTemplate>";

//          //@"<DataTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
//          //        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
//          //        xmlns:local=""clr-namespace:" + nameSpace + ";assembly=" + assemb + @""" 
//          //        DataType=""{x:Type local:" + type.Name + @"}"" > 
//          //    <Border x:Name=""_dgBorderTemplate"">
//          //        <DataGrid x:Name=""_dgTemplate"" ItemsSource=""{Binding PattnPageList}"" AutoGenerateColumns=""False"">
//          //            <DataGrid.Columns>
//          //                <DataGridTextColumn Header=""S/No"" Binding=""{Binding SNo}""/>
//          //                <DataGridTextColumn Header=""Names"" Binding=""{Binding Names}""/>
//          //                <DataGridTextColumn Header=""Net"" Binding=""{Binding Net}""/>
//          //                <DataGridTextColumn Header=""Gross"" Binding=""{Binding Gross}""/>
//          //                <DataGridTextColumn Header=""Debit"" Binding=""{Binding Gross}""/>
//          //                " + dummy + @"
//          //            </DataGrid.Columns>                      
//          //        </DataGrid>    
//          //    </Border> 
//          //</DataTemplate>";


//          /////DnD the invalid replacement to be extended to include others 
//          //var dualArr = inpExl.Select(s => (s + "\n" + s.Replace("/", ""))
//          //    .Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();

//          //foreach (var e in dualArr)
//          //{
//          //    var t = dummy.Replace(name, e[0]).Replace(value, e[1]) + "\r\n" + dummy;
//          //    iniStr = iniStr.Replace(dummy, t);
//          //}
//          //iniStr = iniStr.Replace(dummy, "");


//          var stringReader = new StringReader(iniStr);
//          XmlReader xmlReader = XmlReader.Create(stringReader);
//          try
//          {
//              var dataTemplat = XamlReader.Load(xmlReader) as DataTemplate;
//              //this.TabContentMgr = (CachedContentPresenter)dataTemplat.LoadContent();
//              return dataTemplat;
//          }
//          catch (Exception ex)
//          {
//              return null;
//          }

//      }

//public DataTemplate CreateCachedTemplate(Type type)
//{



//    var assemb = type.Assembly.GetName();
//    var nameSpace = type.Namespace;
//    //<DataTemplate> </DataTemplate>  
//    var strDat =
//       @"<DataTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
//                xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
//                xmlns:local=""clr-namespace:" + nameSpace + ";assembly=" + assemb + @""" 
//                DataType=""{x:Type TabControl}"" >                    
//            <local:" + type.Name + @" x:Name=""_tabContentMgr"" >
//                <DataTemplate>
//                    <Border x:Name=""_dgBorderTemplate"">
//                        <DataGrid x:Name=""_dgTemplate"" ItemsSource=""{Binding PattnPageList}"" AutoGenerateColumns="" False"">
//                            <DataGrid.Columns>
//                                <DataGridTextColumn Header=""S/No"" Binding=""{Binding SNo}""/>
//                                <DataGridTextColumn Header=""Names"" Binding=""{Binding Names}""/>
//                                <DataGridTextColumn Header=""Net"" Binding=""{Binding Net}""/>
//                                <DataGridTextColumn Header=""Gross"" Binding=""{Binding Gross}""/>
//                                <DataGridTextColumn Header=""Debit"" Binding=""{Binding Gross}""/>
//                            </DataGrid.Columns>                      
//                        </DataGrid>    
//                    </Border> 
//                </DataTemplate>
//            </local:" + type.Name + @">
//       </DataTemplate>";

//    var stringReader = new StringReader(strDat);

//    XmlReader xmlReader = XmlReader.Create(stringReader);

//    try
//    {
//        var dataTemplat = XamlReader.Load(xmlReader) as DataTemplate;


//        //var stringReader = new StringReader(strDat);
//        //StringReader stringReader = new StringReader(@"<DataTemplate xmlns=
//        //    ""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
//        //         < " + type.Name + @" x:Name = ""_tabContentMgr"" />
//        //    </ DataTemplate >");
//        //var tetIt = new CachedContentPresenter();
//        //_viewsDatagrid._tabControl.ContentTemplate = this.DgContentTempalate = dataTemplat;
//        //var testTemp = (CachedContentPresenter)dataTemplat.LoadContent();

//        return dataTemplat;

//    }
//    catch (Exception ex)
//    {

//        return null; ;
//    }

//    //return (DataTemplate)XamlReader.Load(XmlReader.Create(strDat));
//    //return (DataTemplate)XamlReader.Load(XmlReader.Create(strDat));

//}
//public DataTemplate CreateTemplate(Type type)
//{
//    var assemb = type.Assembly.GetName();
//    var nameSpace = type.Namespace;
//    //<DataTemplate> </DataTemplate>  
//    var strDat =
//       @"<DataTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
//                xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
//                xmlns:local=""clr-namespace:" + nameSpace + ";assembly=" + assemb + @""" 
//                DataType=""{x:Type TabControl}"" >                    
//                <local:" + type.Name + @" x:Name=""_tabContentMgr"" >
//                    <DataTemplate>
//                        <Border x:Name=""_dgBorderTemplate"">
//                            <DataGrid x:Name=""_dgTemplate"" ItemsSource=""{Binding PattnPageList}"" AutoGenerateColumns="" False"">
//                                <DataGrid.Columns>
//                                    <DataGridTextColumn Header=""S/No"" Binding=""{Binding SNo}""/>
//                                    <DataGridTextColumn Header=""Names"" Binding=""{Binding Names}""/>
//                                    <DataGridTextColumn Header=""Net"" Binding=""{Binding Net}""/>
//                                    <DataGridTextColumn Header=""Gross"" Binding=""{Binding Gross}""/>
//                                    <DataGridTextColumn Header=""Debit"" Binding=""{Binding Gross}""/>
//                                </DataGrid.Columns>                      
//                            </DataGrid>    
//                        </Border> 
//                    </DataTemplate>
//                </local:" + type.Name + @">                      
//        </DataTemplate>";

//    var stringReader = new StringReader(strDat);

//    XmlReader xmlReader = XmlReader.Create(stringReader);

//    try
//    {
//        var dataTemplat = XamlReader.Load(xmlReader) as DataTemplate;


//        //var stringReader = new StringReader(strDat);
//        //StringReader stringReader = new StringReader(@"<DataTemplate xmlns=
//        //    ""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
//        //         < " + type.Name + @" x:Name = ""_tabContentMgr"" />
//        //    </ DataTemplate >");
//        //var tetIt = new CachedContentPresenter();
//        //_viewsDatagrid._tabControl.ContentTemplate = this.DgContentTempalate = dataTemplat;
//        var testTemp = (CachedContentPresenter)dataTemplat.LoadContent();

//        return dataTemplat;

//    }
//    catch (Exception ex)
//    {

//        return null; ;
//    }

//    //return (DataTemplate)XamlReader.Load(XmlReader.Create(strDat));
//    //return (DataTemplate)XamlReader.Load(XmlReader.Create(strDat));

//}

//public DataTemplate CreateCachedTemplate2(Type type)
//{
//    var assemb = type.Assembly.GetName();
//    var nameSpace = type.Namespace;
//    var strDat =
//       @"<DataTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
//                xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
//                xmlns:local=""clr-namespace:" + nameSpace + ";assembly=" + assemb + @""" 
//                DataType=""{x:Type local:" + type.Name + @"}"" > 
//            <Border x:Name=""_dgBorderTemplate"">
//                <DataGrid x:Name=""_dgTemplate"" ItemsSource=""{Binding PattnPageList}"" AutoGenerateColumns=""False"">
//                    <DataGrid.Columns>
//                        <DataGridTextColumn Header=""S/No"" Binding=""{Binding SNo}""/>
//                        <DataGridTextColumn Header=""Names"" Binding=""{Binding Names}""/>
//                        <DataGridTextColumn Header=""Net"" Binding=""{Binding Net}""/>
//                        <DataGridTextColumn Header=""Gross"" Binding=""{Binding Gross}""/>
//                        <DataGridTextColumn Header=""Debit"" Binding=""{Binding Gross}""/>
//                    </DataGrid.Columns>                      
//                </DataGrid>    
//            </Border> 
//        </DataTemplate>";

//    var stringReader = new StringReader(strDat);
//    XmlReader xmlReader = XmlReader.Create(stringReader);

//    try
//    {
//        var dataTemplat = XamlReader.Load(xmlReader) as DataTemplate;
//        var testTemp = (Border)dataTemplat.LoadContent();
//        return dataTemplat;
//    }
//    catch (Exception ex)
//    {
//        return null;
//    }
//}

