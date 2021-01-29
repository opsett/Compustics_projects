using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;
using DynamicDatagridColumns.Model_ViewModels;
using SharedResx_NetCore;
using SharedResx_NetStandard;

namespace DynamicDatagridColumns.Views
{

    /// <summary>
    /// DnD: This class is designed to aid the generation of ContentTemplate for a TabControl which has
    /// the appearance of (i) multiple DataGrid with (ii) dynamic Columns and each data grid seemingly 
    /// corresponding to a TabItem of the TabControl. The replication of the datagrid page in each tab
    /// is made possible by the CachedContentPresenter class (see its Class' comments). The mutability
    /// (adding, removing or changing)  of the dagagrid's columns, on the other hand, is made possible
    /// by the use of this class and some of it static methods.
    /// </summary>
    public class DynamicColumsModule
    {
        readonly static string sc = Stat.Septor.ToString();
        private static readonly string Nme = sc + "name" + sc;
        private static readonly string Val = sc + "val" + sc;
        private static readonly string Custm = sc + "custm" + sc;
        public static readonly string Dummy = @"<Dummy />";//DnD: Single space is require before the slash
        private static readonly string Formt = sc + "fmt" + sc;


        //public XmlElement RefedNamespaces { set; get; }
        //public Type CustomWrapperType { get; set; }
        //public static readonly string Assemb = sc + "assemb" + sc;


        public string UpdatedModule { get; private set; }
        public string BaseModule { get; private set; }

        public XmlReader TemplateData { get; private set; }


        /// <summary>TabCotrol's 
        /// </summary>
        /// <param name="cachedPresenter"></param>
        public DynamicColumsModule(CachedContentPresenter cachedPresenter)
        {
            if (cachedPresenter == null)
                return;
            var type = cachedPresenter.GetType();
            var assemb = type.Assembly;
            var nameSpace = type.Namespace;
            //ModularTemplate=""{Binding ColumNames}""
            string datType =
                 @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                        xmlns:local=""clr-namespace:" + nameSpace + ";assembly=" + assemb + @""" 
                        DataType=""{x:Type local:" + type.Name + @"}"">
                    <local:" + type.Name + @" x:Name=""_tabContentMgr""  >
                        <DataTemplate>
                            <Border x:Name=""_templateBorder"" >
                               " + ConstantStr.Dummy + @"
                            </Border>
                        </DataTemplate>
                    </local:" + type.Name + @" >
                </DataTemplate>";
            var sourceName = cachedPresenter.SourceName;
            var visualNode = @"<DataGrid x:Name=""_TemplateDgrid"" ItemsSource=""{Binding " + sourceName + @"}"" AutoGenerateColumns=""False"" >
                                    <DataGrid.Columns >
                                    " + ConstantStr.Dummy + @"
                                    </DataGrid.Columns>
                              </DataGrid>";

            var iniStr = datType.Replace(ConstantStr.Dummy, visualNode);

            //var t = dummy.Replace(name, e[0]).Replace(value, e[1]);
            //iniStr = iniStr.Replace(ConstString.Dummy, t.Trim() + "\n" + ConstString.Dummy);


            var datStr = iniStr.Replace(ConstantStr.Dummy, "");

            //rpt:
            var stringReader = new StringReader(datStr);
            this.TemplateData = XmlReader.Create(stringReader);
            try
            {
                //var dataTemplat = XamlReader.Load(this.TemplateData) as DataTemplate;

                var templXml = XElement.Parse(iniStr);
                this.UpdatedModule = this.BaseModule = ((XElement)templXml.FirstNode).FirstNode.ToString();

                //goto rpt;
                //return dataTemplat;
            }
            catch (Exception ex)
            {
                //goto rpt;
                return;
            }



        }
        /// <summary>
        /// 
        /// </summary>
        //public readonly string DummyText = @"<DataGridTextColumn Header=""" + Nme + @""" Binding=""{Binding " + Val + @"}""/>";



        /// <summary>
        /// DnD: this static class is designed to generate column's data template (in string format)
        /// suitable for the type of data in the parameter [attribs], as indicated by the second parameter [type],
        /// utilizing one or more of the internal data template formats.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attribs"></param>The the 3rd element should be null excerpt when used to
        /// hold custom type's name i.e. ("custom:MyCustomClass") which will also indicates
        /// that [default:] sholud be the [switch] [case] options </param>
        /// <returns></returns>
        public static string GetContexModul(Type type, string[] attribs)
        {
            var arr = attribs;// new string[3];
            //Array.Copy(attribs, arr, attribs.Length);
            ////arr[1] =arr[1]==""?

            //return null;
            string module, typName, assemb;
            typName = assemb = "";
            //var assemb = "";
            //var typName = type.Name;

            if (arr[2] != null && arr[2].Trim() != "")
                assemb = @" xmlns:custom=""clr-namespace:" + type.Namespace + ";assembly=" + type.Assembly + @"""";
            else typName = type.Name;

            switch (typName)
            {
                case "Boolean":
                    arr[2] = arr[2] ?? "DataGridCheckBoxColumn";
                    module = @"<" + Custm + @" Header=""" + Nme + @""" Binding=""{Binding " + Val + @"}""" + assemb + @" />";
                    break;

                case "ListCollectionView":
                    arr[2] = arr[2] ?? "ComboBox";
                    module = @"<DataGridTemplateColumn Header=""" + Nme + @""" >
                                <DataGridTemplateColumn.CellTemplate  >
                                            <DataTemplate  >
                                                <" + Custm + @"  ItemsSource=""{Binding " + Val + @"}""" + assemb + @"" + assemb + @" />
                                            </DataTemplate>
                                        </DataGridTemplateColumn.CellTemplate>
                            </DataGridTemplateColumn>";
                    break;

                case "String":
                case "Double":
                case "Decimal":
                    arr[2] = arr[2] ?? "DataGridTextColumn";
                    module = @"<" + Custm + @" Header=""" + Nme + @""" Binding=""{Binding " + Val + Formt + @"}""" + assemb + @"  />";
                    switch (typName)
                    {
                        case "Double":
                            module = module.Replace(Formt, @", StringFormat=N2");
                            break;
                        case "Decimal":
                            module = module.Replace(Formt, @", StringFormat=F3");
                            break;
                        default:
                            module = module.Replace(Formt, "");
                            break;
                    }
                    break;

                default:
                    ///DnD arr[2] is preloaded
                    ///DnD Custm is to represent custom class. Expected Format example: "local:MyCustomClass" 
                    module = @"<DataGridTemplateColumn Header=""" + Nme + @""" Width=""100""" + assemb + @" >
                                <DataGridTemplateColumn.CellTemplate>
                                    <DataTemplate>
                                        <" + Custm + @" Height=""17""  Rating=""{Binding " + Val + @"}""  />
                                    </DataTemplate>
                                </DataGridTemplateColumn.CellTemplate>
                            </DataGridTemplateColumn>";
                    break;

            }
            return module.Replace(Nme, arr[0]).Replace(Val, arr[1]).Replace(Custm, arr[2]);
        }


        /// <summary>
        /// DnD: This method is designed to use the class property, [BaseModule], which is stores
        /// the updated version of colums's template generates by as an object of this class.
        /// for updating an existing colums's template
        /// </summary>
        /// <param name="columnsIDs"></param>
        /// <param name="collection"></param>
        /// <param name="cummulative"></param>
        /// <returns></returns>
        public string UpdateElementModule(string[] columnsIDs, object[] collection, bool cummulative)
        {
            if (this.BaseModule == null)
                return null;

            var baseModule = this.BaseModule;
            var existingSiblings = new List<string>();
            if (cummulative)
            {
                ///DnD In order to avoid duplications, it's important to first
                /// accertain which Columns are already contained in the BaseModule
                baseModule = this.UpdatedModule;
                var xl = XElement.Parse(baseModule);

                ///DnD Identify the location of the replate ment tag, <dummy />. Note that its
                ///string form's whitespaces could have changed hence the verbose conversion
                var dumName = XElement.Parse(ConstantStr.Dummy).Name.LocalName;
                while (xl != null && xl.Elements().Where(s => s.Name.LocalName == dumName).FirstOrDefault() == null)
                    xl = xl.FirstNode as XElement;

                if (xl == null) return null;

                existingSiblings = xl.Elements().Select(n => (n.Attributes().Where(a
                   => a.Name.LocalName.ToLower() == "header").FirstOrDefault())).Where(a
                   => a != null).Select(a => a.Value).ToList<String>();
            }
            var iniStr = baseModule;
            foreach (var collumnName in columnsIDs)
            {
                string[] args = new string[] { collumnName, EditText(collumnName, "Col")/*.Replace("/", "")*/, null };

                var colmItems = collection[0];// ((WkSheetModel)CachedContentPresenter.SelectedItem).PattnPageList[0] as PrintFormat;

                var objName = colmItems.GetType().GetProperties().Where(p => p.Name.ToLower() == args[1].ToLower())
                    .FirstOrDefault();

                if ((collumnName == Xt.Rating) is bool isRating && isRating)
                {
                    args[0] = Xt.Consistence;
                    args[2] = Xt.custom + RatingControl.ToString();
                }

                var type = isRating ? typeof(RatingControl) :
                    (objName != null ? objName.PropertyType : typeof(string));

                var newCollm = DynamicColumsModule.GetContexModul(type, args);

                /* ///DnD ToDo: GUI_Settings, forestores columns duplication */
                if (existingSiblings.Contains(args[0]))
                    continue;
                else existingSiblings.Add(args[0]);

                iniStr = iniStr.Replace(ConstantStr.Dummy, newCollm.Trim() + "\n" + ConstantStr.Dummy);
            }
            if (cummulative)
                this.UpdatedModule = iniStr;
            return iniStr.Replace(Dummy, "");
        }

        private static string EditText(string inpStr, string replaceWord)
        {
            string str = "";

            if (inpStr.IndexOfAny(CharCheck.SymbolChars) is int i && i < 0)
                str = inpStr;
            else
            if (inpStr.Remove(i) is string i2 && i2.Length > 3)
                str = i2;
            else
            {
                var chs = inpStr.Where(t => CharCheck.SymbolChars.ToList().IndexOf(t) < 0).ToArray();// !CharCheck.SymbolChars.Contains((char)t).ToString()).ToArray());
                str = chs.Aggregate("", (a, b) => a += b.ToString());

            }
            if (str.Length < 3)
                str = replaceWord + "C " + Stat.AnnualToken.Replace("_", "");

            return str.Trim();
        }

        //    Aggregate("0", (last, next) => Convert.ToInt64(next.Replace("_", "")) >
        //            Convert.ToInt64(last.Replace("_", "")) ? next : last);
        //fontSizes.Aggregate(0.0, (longest, next) => next > longest? next : longest)
    }

}
