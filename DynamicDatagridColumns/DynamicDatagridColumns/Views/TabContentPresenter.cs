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
using DynamicDatagridColumns.Model_ViewModels;

namespace DynamicDatagridColumns.Views
{

    /// <summary>
    /// DnD: This class is derived from the [Decorator], it is therefore similar to the Border class
    /// which is also derived from the same [Decorator]. However in this class, it is used as an
    /// encapsulated ContentPresenter with  ConditionalWeakTable (as Dictionary) and some propterties
    /// (such as DataTemplate, SourceName) and methods that are use to respond seamlessly to routed
    /// event emmaning from change in values assigned to DatatContext from the Model-View end of the
    /// MVVM. 
    /// 
    /// This class works with the TabControl object instantiated in the Wpf-Xaml file and  is used as
    /// part of the code-behind in conjuction with the DynamicColumsModule class which provies a
    /// simulated template for its dependency object propts, [DataTemplate]. This requires a 
    /// 
    /// It is designed to hold and repeatedly present the same datagrid but with differing asssingment
    /// to ItemsSource as the selected tab/tabItem of the TabControl changes. It functions as a 'View'
    /// helper class in the MVVM pattern and is responsible for reacting to and handling changes in the
    /// values of DataContext for the 'View's End. Each TabItem in the TabControl causes a corresponding
    /// change (in SourceItem) that is assigned DataContext from 'Model/ModelView' end of the pattern.
    /// This designed inherently causes 'Model-View' to respond to changes in selected tab/tabItem. Such
    /// repsonds is routed to this class from 'Model-View' and this class consequently responds and
    /// effects changes at the 'Views'
    /// </summary>
    [ContentProperty("DataTemplate")]
    public class CachedContentPresenter : Decorator
    {

        ///ConditionalWeakTable is a special Dictionary, which doesn't prohibit garbage-collection
        /// of its keys. Instead it automatically removes garbaged Elements
        private ConditionalWeakTable<object, ContentPresenter> _PresenterCache = new ConditionalWeakTable<object, ContentPresenter>();

        public CachedContentPresenter()
        {
            this.DataContextChanged += (s, e) => UpdatePresentation(e.NewValue);
            if (SourceName == null)
            {
                SourceName = ViewsDatagrid.SourceName;
            }

            //      InitDat = new TextBlock()
        }


        //private void CachedContentPresenter_DataContextChanged2(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    var ctx = this.DataContext as WkSheetModel;

        //    var dftColms = (string[])ctx.DefaultColumNames;
        //    if (dftColms != null && dftColms != ctx.ColumNames)
        //    {
        //        Updating = true;
        //        DataTemplate = GetContexColumns(dftColms);

        //        var ctp = new ContentPresenter { ContentTemplate = DataTemplate };
        //        ctp.SetBinding(ContentPresenter.ContentProperty, new Binding());

        //        this.Child = ctp;
        //    }
        //    else if (!ctx.ColumnIsUniformed)
        //    {
        //        UpdateColumns(ctx);
        //    }

        //    //ctx.CurrentSelection = WkSheetModel.WkBookModel
        //    //    .IndexOf((WkSheetModel)ctx);


        //}

        //private object Ctx { get; }
        //public PropertyChangedEventHandler PropertyChanged;

        private void UpdatePresentation(object e)
        {
            if (UpdatingTemplate)
                return;

            if (e == null && PreviousItem == null)
                return;

            bool uniformityChanged = e == null && PreviousItem != null;

            if (uniformityChanged)
            {
                SelectedItem = SelectedItem.CmdUniform.WkSheet;// PreviousItem;
                PreviousItem = null;


                ///DnD TutResx: This custom dp object, ItemContainerGen, will be very applicable else where
                ///var tabItem = this.ItemContainerGen.ContainerFromIndex(0) as TabItem;                
                ///tabItem.IsSelected = true;

            }
            else
            {
                var prev = PreviousItem = SelectedItem;
                SelectedItem = e as WkSheetViewModel;
                //SheetData = SelectedItem.SheetData;
            }

            var newItem = SelectedItem.SheetData.Header;//  uniformityChanged ? SelectedItem.SheetData : ((WkSheetViewModel)e).SheetData;
            //if ( newItem == null)
            //    return;

            var datTempl = UpdateDataTemplate(PreviousItem == null ? null :
                (string[])PreviousItem.SheetData.ColumNames, (string[])SelectedItem.SheetData.ColumNames, out bool updated);

            ContentPresenter ctp;
            var exists = _PresenterCache.TryGetValue(newItem, out ctp);

            if (!exists || updated)
            {
                UpdatingTemplate = true;

                if (exists)
                    _PresenterCache.Remove(newItem);

                DataTemplate = datTempl;

                ctp = new ContentPresenter { ContentTemplate = DataTemplate };
                ctp.SetBinding(ContentPresenter.ContentProperty, new Binding());
                _PresenterCache.Add(newItem, ctp);

                UpdatingTemplate = false;
            }

            this.Child = ctp;

        }


        public static readonly DependencyProperty DataTemplateProperty = DependencyProperty.Register("DataTemplate",
            typeof(DataTemplate), typeof(CachedContentPresenter), new FrameworkPropertyMetadata(DataTemplate_Changed));
        public DataTemplate DataTemplate
        {
            get { return (DataTemplate)this.GetValue(DataTemplateProperty); }
            set { SetValue(DataTemplateProperty, value); }
        }
        private static void DataTemplate_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ccp = (CachedContentPresenter)sender;
            ccp._PresenterCache = new ConditionalWeakTable<object, ContentPresenter>();
            ccp.UpdatePresentation(ccp.DataContext);
        }

        public static readonly DependencyProperty ItemContainerGenProperty = DependencyProperty.Register("ItemContainerGen",
            typeof(ItemContainerGenerator), typeof(CachedContentPresenter));
        public ItemContainerGenerator ItemContainerGen
        {
            get { return (ItemContainerGenerator)this.GetValue(ItemContainerGenProperty); }
            set { SetValue(ItemContainerGenProperty, value); }
        }




        //public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource",
        //    typeof(string), typeof(CachedContentPresenter));
        //public string ItemsSource
        //{
        //    get { return (string)this.GetValue(ItemsSourceProperty); }
        //    set { SetValue(ItemsSourceProperty, value); }
        //}


        public string SourceName { get; set; }

        //public static readonly DependencyProperty SheetDataProperty = DependencyProperty.Register("SheetData",
        //    typeof(SheetDataModel), typeof(CachedContentPresenter), new FrameworkPropertyMetadata(SheetData_Changed));

        //public SheetDataModel SheetData
        //{
        //    get { return (SheetDataModel)this.GetValue(SheetDataProperty); }
        //    set { SetValue(SheetDataProperty, value); }
        //}

        //private static void SheetData_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{

        //}




        /// <summary>
        /// Updates Colums's Template
        /// </summary>
        /// <param name="prevColmNames"></param>
        /// <param name="selectedColmNames"></param>
        /// <param name="updated"></param>
        /// <returns></returns>
        public DataTemplate UpdateDataTemplate(string[] prevColmNames, string[] selectedColmNames, out bool updated)
        {
            updated = false;
            var colmNames = /*SelectedItem.ColumnIsUniformed ? (string[])SelectedItem.DefaultColumNames :*/ selectedColmNames;

            if (updated = prevColmNames == null)
                return GetContexColumns(colmNames, this);

            ///DnD Upgrade Feature: newCol can be used to Exposed change column's names
            if ((colmNames.Where(c => !prevColmNames.Contains(c))
                                .FirstOrDefault() is string newCol) && (updated = newCol != null))
                return GetContexColumns(colmNames, this);
            else
                return this.DataTemplate ?? GetContexColumns(colmNames, this);
        }
        public static WkSheetViewModel SelectedItem { set; get; }
        private static WkSheetViewModel PreviousItem { set; get; }
        //= PreviousItem ?? new WkSheetModel();
        //private void UpdateColumns(object e)
        //{
        //    PreviousItem = SelectedItem;
        //    SelectedItem = e as WkSheetViewModel;
        //    var colmIDs = (string[])SelectedItem.SheetData.ColumNames;
        //    //ColmList = ColmList;
        //    if (!Updating && IsUpdated(colmIDs, out string newCol))
        //    {
        //        Updating = true;
        //        DataTemplate = null;
        //        DataTemplate = GetContexColumns(colmIDs, this);
        //        Updating = false;
        //    }
        //}

        //private bool IsUpdated(string[] colmList2, out string newColmName)
        //{
        //    newColmName = "";
        //    if (PreviousItem == null) return true;
        //    var ColmList = (string[])PreviousItem.SheetData.ColumNames;
        //    if (ColmList.Length != colmList2.Length
        //        || ((colmList2.Where(c => !ColmList.Contains(c)).FirstOrDefault() is string newCol) && (newColmName = newCol) != null))
        //    {
        //        ColmList = colmList2;
        //        return true;
        //    }
        //    return false;
        //}





        [ThreadStatic()]
        private static bool UpdatingTemplate;// { get; internal set; }
        //public static bool Updating { get; internal set; }
        //public static DataTemplate UpdatedTemplate { get; internal set; }

        //public static IEnumerable SelectedItem2 { set; get; }
        //public static string ToString() { return "CachedContentPresenter"; }

        internal static DynamicColumsModule ColumTemplateModule { get; set; }

        internal static CachedContentPresenter CachedContent { get; set; }
        /// <summary>
        /// DnD This method 
        /// </summary>
        /// <param name="collumnNames"></param>
        /// <returns></returns>
        private static DataTemplate GetContexColumns(string[] collumnNames, CachedContentPresenter cachedContent)
        {

            if (ColumTemplateModule == null || ColumTemplateModule.BaseModule == null)
                ColumTemplateModule = new DynamicColumsModule(cachedContent);

            var items = SelectedItem.SheetData.PattnPageList;
            var datStr = ColumTemplateModule.UpdateElementModule(collumnNames, items.ToArray(), false);

            var stringReader = new StringReader(datStr);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            try
            {
                var dataTemplat = XamlReader.Load(xmlReader) as DataTemplate;
                return dataTemplat;
            }
            catch (Exception ex)
            {

                return null;
            }


        }
    }

}
