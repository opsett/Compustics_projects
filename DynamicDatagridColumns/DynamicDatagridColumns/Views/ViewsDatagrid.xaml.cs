﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DynamicDatagridColumns.Views
{
    /// <summary>
    /// Interaction logic for ViewsDatagrid.xaml
    /// </summary>
    public partial class ViewsDatagrid : UserControl
    {
        public ViewsDatagrid()
        {
            InitializeComponent();
            //Template
            //DataContext = DataContext;

            //this._tabControl.SelectionChanged += _tabControl_SelectionChanged;
            //ViewsDatagrid5.Tabcontrol = this._tabControl;
            //var resx = _tabControl.ContentTemplate.Resources;
            //_tabControl.Template.
            //var v = _tabControl.ContentTemplate;//.FindName(;
            //var v2 = _tabControl.FindResource(Template);
            //var v1 = _tabControl.ContentTemplate.LoadContent();
            //var v2 = _tabControl.FindResource("ContentTemplate");
            //var v4 = v3.Template;// _tabControl.ContentTemplate.FindName("_CachedInitData", _tabControl);
            //var v5 = v4.GetType();
            //var v6 = v4.GetType();
            //var v7 = v4.GetType();

            //var v1 = this.FindResource("_CachedInitData");
            //var v2 = v1 as DataTemplate;
            //var v3 = v2.Resources;

            /////DnD TutRx: Creates an instance of class from Xaml DatataTemplate
            //CachedContentPresenter cachedPresnter = 


            if (!(this.FindResource("_SourceName") is string initData))
                return;
            SourceName = initData.Trim('"');// new CachedContentPresenter(initData);// cachedPresnter;// v2.LoadContent() as CachedContentPresenter;
            //var v5 = v4.ItemsSource;
        }


        /// <summary>
        /// The DataTemplate in is
        /// </summary>
        protected internal static string SourceName { get;  set; }


        //public override void OnApplyTemplate()
        //{

        //    if (_tabControl.SelectedItem == null)
        //    {
        //        if (!(_tabControl.ItemContainerGenerator.ContainerFromIndex(0) is TabItem tabItem))
        //            return;//[index];

        //        tabItem.IsSelected = true;
        //    }
        //    base.OnApplyTemplate();
        //    //_tabControl.SelectedIndex = 0;
        //}
        ////override on

        //private static TabControl Tabcontrol;
        //protected internal static void ResetTabIem(int index)
        //{

        //    Tabcontrol.UpdateLayout();
        //    //var index = targetTab.CmdUniform.PattnArr.IndexOf(targetTab);
        //    if (Tabcontrol.SelectedIndex == index)
        //        return;
        //    //return;
        //    Tabcontrol.SelectedIndex = -1;
        //    Tabcontrol.SelectedIndex = index;
        //    ////Tabcontrol.SelectedIndex = -1;
        //    //Tabcontrol.SelectedIndex = index;
        //    Tabcontrol.UpdateLayout();
        //}

        //internal int SelectedTabIndex { get; set; }
        //public static object SelectedItem { get=>Tabcontrol.SelectedItem; }

        //private void _tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    SelectedTabIndex = ((TabControl)sender).SelectedIndex;
        //}


        //private void _dgTemplate_AutoGeneratedColumns(object sender, EventArgs e)
        //{

        //}


    }

    //public class TemplateSelection : DataTemplateSelector
    //{
    //    public DataTemplate BoolTempl { get; set; }
    //    public DataTemplate RatingTempl { get; set; }
    //    public DataTemplate ComboBoxTempl { get; set; }
    //    public DataTemplate TextTempl { get; set; }
    //    public DataGridTemplateColumn TextBlockColumn { get; set; }
    //    public DataGridTemplateColumn ComboBoxColumn { get; set; }
    //    public DataGridTemplateColumn CheckedBoxColumn { get; set; }
    //    public DataGridTemplateColumn RatingColumn { get; set; }



    //    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    //    {
    //        var frms = new System.Diagnostics.StackTrace().GetFrames();
    //        var v3 = frms[3].GetMethod().Name;

    //        var v1 = container.GetType();
    //        //var v2 = container.ReadLocalValue(co);

    //        var typName = item.ToString();// item.GetType().Name;

    //        switch (typName)
    //        {
    //            case "bool":
    //                return BoolTempl;

    //            case "Variations":
    //                var templ = new DataTemplate(typeof(CachedContentPresenter));
    //                ComboBoxColumn.Header = typName;
    //                ////ComboBoxColumn.CellTemplate.Template.
    //                ////new DataTemplate().
    //                ////templ.Resources.Add("ComboBoxColumn", ComboBoxColumn);
    //                return templ = (DataTemplate)ComboBoxColumn.CellTemplate;//.LoadContent();
    //                                                                         ////return ComboBoxTempl.;
    //                                                                         ////return ComboBoxColumn;
    //                                                                         //return (DataTemplate)ComboBoxColumn;
    //            case "RatingTemplate":
    //                return RatingTempl;

    //            case "TextBlock":
    //                return TextTempl;

    //            default:
    //                return base.SelectTemplate(item, container);

    //        }



    //        //public DataTemplate IncidentTemplate { get; set; }
    //        //public DataTemplate ServiceTemplate { get; set; }

    //        //if (item is IncidentModel) return IncidentTemplate;
    //        //else if (item is ServiceModel) return ServiceTemplate;
    //        //else return base.SelectTemplate(item, container);
    //    }


    //}

    //public class DataGridCollumnTemplate : DataGridTemplateColumn
    //{

    //    public DataTemplate BoolTempl { get; set; }
    //    public DataTemplate RatingTempl { get; set; }
    //    public DataGridTemplateColumn ComboBoxColumn { get; set; }
    //    public DataTemplate TextTempl { get; set; }


    //    //public override 
    //    public DataGridCollumnTemplate() : base()
    //    {
    //        //base.CellTemplate 

    //    }

    //    public new DataTemplate CellTemplate { get { return base.CellTemplate; } set { base.CellTemplate = value; } }

    //    public DataGridTemplateColumn SelectTemplate(object item, DependencyObject container)
    //    {
    //        var frms = new System.Diagnostics.StackTrace().GetFrames();
    //        var v3 = frms[3].GetMethod().Name;

    //        var v1 = container.GetType();
    //        //var v2 = container.ReadLocalValue(co);

    //        var typName = item.ToString();// item.GetType().Name;

    //        switch (typName)
    //        {
    //            //case "bool":
    //            //    return BoolTempl;

    //            case "Variations":
    //                return ComboBoxColumn;

    //            //case "RatingTemplate":
    //            //    return RatingTempl;

    //            //case "TextBlock":
    //            //    return TextTempl;

    //            default:
    //                return null;// new DataTemplate(container); //base.SelectTemplate(item, container);

    //        }
    //    }
    //}

   
}