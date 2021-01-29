using SharedResx_NetStandard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace DynamicDatagridColumns.Model_ViewModels
{


    public class PattnViewModel /*: INotifyPropertyChanged*/
    {

        public PattnViewModel(string criteria)
        {
            ///DnD DebugOnly
            criteria = criteria ?? "0-3";
            WorkBkItems = CollatePattn(criteria);

            //WorkBkModel.CollectionChanged += WorkBkModel_CollectionChanged;
            ViewsCommand.WkBookData = this;
        }

        //private WkSheetViewModel SelectedItem0;
        //protected internal WkSheetViewModel SelectedItem
        //{
        //    set
        //    {
        //        //if (!(e.NewItems[0] is WkSheetViewModel wkSheetViewModel))
        //        //    return;
        //        SelectedItem0 = WorkBkItems[value.SheetData.IndexId] = value;
        //        var arg = new PropertyChangedEventArgs("SelectedItem");
        //        PropertyChanged(this, arg);
        //    }
        //    get => SelectedItem0;
        //}

        //public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //protected internal static WkSheetViewModel SelectedItem0;
        //public WkSheetViewModel SelectedItem
        //{
        //    set
        //    {
        //        if (CmdUniform.PattnArr != null)
        //            SelectedItem0.LastWkSheetIndex = CmdUniform.PattnArr.IndexOf(SelectedItem0);


        //        SelectedItem0 = value;
        //        var eventArgs = new PropertyChangedEventArgs("SelectedItem");
        //        PropertyChanged(this, eventArgs);
        //    }
        //    get => SelectedItem0;
        //}
        //private event PropertyChangedEventHandler PropertyChanged = delegate { };


        //public ViewsCommand CmdUniform { get => new ViewsCommand(SelectedItem); }

        /// <summary>
        /// DnD ToDo: Assing from Setting
        /// </summary>
        private static int _lines = 50;

        public static string[] ColumSample { get => new string[] { "S/No", "PPID", "Names", "GradeStep", "Gross", "CoopDebit", "Net", "Variations", "Rating", "Archived" }; }


        static readonly Random rnd = new Random();


        /// <summary>
        /// DnD DebugOnly Exclude one Randomly selected column name .
        /// </summary>
        private static KeyValuePair<string, IEnumerable> ColCollection
        {
            get
            {
                int exlude = rnd.Next(0, ColumSample.Length);
                var list = new List<string>(ColumSample);
                var rport = "Col: " + (exlude + 1) + " Sheet__~";
                list[exlude] = rport;
                return new KeyValuePair<string, IEnumerable>(rport, list.ToArray());
            }

        }


        //private void PattnData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    var sheet = e.NewItems[0] as WkSheetViewModel;
        //    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(new Action(() => SelectedItem= sheet));
        //}





        public ObservableCollection<WkSheetViewModel> WorkBkItems { get; set; }

        public ObservableCollection<WkSheetViewModel> CollatePattn(string criteria)
        {



            if (criteria == null || criteria == string.Empty)
                return null;
            var strArr = Stat.GetPattnArr(criteria);

            var multidArr = SplitListToPages(strArr, _lines);

            var columnsIndexSeed = (WorkBkItems == null ? 0 : WorkBkItems.Count) + 1;

            var pattnData = new ObservableCollection<WkSheetViewModel>();
            SheetDataModel wkSheet = null;// new WkSheetModel();
            for (int i = 0; i < multidArr.Length; i++)
            {
                var wkArr = multidArr[i];
                var pttnColls = PrintFormat.GetPatterns(wkArr, out PrintFormat[] pf).SourceCollection
                    as ObservableCollection<PrintFormat>;

                var kv = PattnViewModel.ColCollection;
                var header = (columnsIndexSeed + i).ToString().PadLeft(2, '0');
                //wkSheet = new SheetDataModel
                //{
                //    Header = "Sheet_" + header,
                //    PattnPageList = pttnColls,
                //    ColumNames = ((string[])kv.Value).Select(v => v.Contains("~") ? v.Replace("~", header) : v).ToArray()
                //};

                /*DnD NB: Not to be simplified. May caus sync error
                 * when assessed with lamda functio///n */
                var SheetModel = new WkSheetViewModel();

                pattnData.Add(SheetModel);
                var index = pattnData.IndexOf(SheetModel);

                pattnData[index] = new WkSheetViewModel
                {
                    SheetData = new SheetDataModel
                    {
                        IndexId = index,
                        Header = "Sheet_" + header,
                        PattnPageList = pttnColls,
                        ColumNames = ((string[])kv.Value).Select(v => v.Contains("~") ? v.Replace("~", header) : v).ToArray()
                    }

                };///*/


                /*DnD TutResx:
                *Task<int>.Run(() =>
                *{
                *    pattnData.Add( new WkSheetViewModel
                *    {
                *        SheetData = new SheetDataModel
                *        {
                *            IndexId = pattnData.Count,
                *            Header = "Sheet_" + header,
                *            PattnPageList = pttnColls,
                *            ColumNames = ((string[])kv.Value).Select(v => v.Contains("~") ? v.Replace("~", header) : v).ToArray()
                *        }
                       
                *    });
                    
                });/// */


            }
            return pattnData;
        }




        public string[][] SplitListToPages(string[] pttnList, int rowsPerPage)
        {
            return Stat.ReSizeToArr(pttnList, rowsPerPage)
                 .Select(i => (string[])(i.Select(ii => (string)ii).ToArray())).ToArray();
        }

        //public ObservableCollection<WkSheetViewModel> CollatePattn(string criteria)
        //{

        //    if (criteria == null || criteria == string.Empty)
        //        return null;
        //    var strArr = Stat.GetPattnArr(criteria);

        //    var multidArr = SplitListToPages(strArr, _lines);

        //    var columnsIndexSeed = (WorkBkModel == null ? 0 : WorkBkModel.Count) + 1;

        //    var pattnData = new ObservableCollection<WkSheetViewModel>();
        //    SheetDataModel wkSheet = null;// new WkSheetModel();
        //    for (int i = 0; i < multidArr.Length; i++)
        //    {
        //        var wkArr = multidArr[i];
        //        var pttnColls = PrintFormat.GetPatterns(wkArr, out PrintFormat[] pf).SourceCollection
        //            as ObservableCollection<PrintFormat>;

        //        var kv = PattnViewModel.ColCollection;
        //        var header = (columnsIndexSeed + i).ToString().PadLeft(2, '0');
        //        wkSheet = new SheetDataModel
        //        {
        //            Header = "Sheet_" + header,
        //            PattnPageList = pttnColls,
        //            ColumNames = ((string[])kv.Value).Select(v => v.Contains("~") ? v.Replace("~", header) : v).ToArray()
        //        };


        //        var SheetModel = new WkSheetViewModel
        //        {
        //            SheetData = wkSheet
        //        };

        //        pattnData.Add(SheetModel);

        //    }
        //    ///DnD
        //    //if (pattnData.Count > 0 && wk  wkSheet.SelectedItem == null)
        //    //    wkSheet.SelectedItem = pattnData[0];

        //    //pattnData.CollectionChanged += PattnData_CollectionChanged;


        //    return pattnData;
        //}


        //public ObservableCollection<WkSheetModel> CollatePattn(string criteria)
        //{
        //    if (criteria == null || criteria == string.Empty)
        //        return null;
        //    var strArr = Stat.GetPattnArr(criteria);

        //    var multidArr = SplitListToPages(strArr, _lines);

        //    var wkbItems = WorkBkModel.WookBkItems;

        //    var columnsIndexSeed = (wkbItems == null ? 0 : wkbItems.Count) + 1;

        //    var pattnData = new ObservableCollection<WkSheetModel0>();
        //    WkSheetModel wkSheet = null;// new WkSheetModel();
        //    for (int i = 0; i < multidArr.Length; i++)
        //    {
        //        var wkArr = multidArr[i];
        //        var pttnColls = PrintFormat.GetPatterns(wkArr, out PrintFormat[] pf).SourceCollection
        //            as ObservableCollection<PrintFormat>;

        //        var kv = PattnView.ColCollection;
        //        var header = (columnsIndexSeed + i).ToString().PadLeft(2, '0');

        //        wkSheet = new WkSheetModel/*(PattnData)*/
        //        {
        //            Header = "Sheet_" + header,
        //            PattnPageList = pttnColls,
        //            //ColumNames = /*kv.Value,*/((string[])kv.Value).Select(v => v.Contains("Debug") ? v.Replace("Debug", "Debug: " + header) : v).ToArray(),
        //            ColumNames = /*kv.Value,*/((string[])kv.Value).Select(v => v.Contains("~") ? v.Replace("~", header) : v).ToArray(),

        //        };
        //        wkbItems.Add(wkSheet);
        //    }
        //    //wkSheet.CmdUniform=cmd

        //     //wkbItems = pattnData;

        //    if (wkbItems.Count > 0 && WorkBkModel.SelectedItem == null)
        //        WorkBkModel.SelectedItem = wkbItems[0];

        //    wkbItems.CollectionChanged += PattnData_CollectionChanged;
        //    return wkbItems;
        //}

        //public string[][] SplitListToPages() { return SplitListToPages(this.PattnList, this.RowsPerPage == 0 ? 9999 : this.RowsPerPage); }

    }


    /// <summary>
    /// Class is designed to be dynamic hence static fields are employed to hold
    /// generic values (values which stay constant accross all objects of the class
    /// irrespective of which object updated it)
    /// </summary>
    public class WkSheetViewModel
    {

        private SheetDataModel SheetData0;
        public SheetDataModel SheetData
        {
            get => LastSelectedItem0 = ColumnIsUniformed ? AssignDftColumns(SheetData0) : SheetData0;
            protected internal set
            {
                SheetData0 = value;

                //var arg = new PropertyChangedEventArgs("SheetData");
                //PropertyChanged(this, arg);
            }
        }


        //private WkSheetViewModel SelectedItem0;
        //protected internal WkSheetViewModel SelectedItem
        //{
        //    set
        //    {
        //        //if (!(e.NewItems[0] is WkSheetViewModel wkSheetViewModel))
        //        //    return;
        //        SelectedItem0  = value;
        //        var arg = new PropertyChangedEventArgs("SelectedItem");
        //        PropertyChanged(this, arg);
        //    }
        //    get => SelectedItem0;
        //}

        //public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private SheetDataModel AssignDftColumns(SheetDataModel sheetData)
        {
            return new SheetDataModel
            {
                ColumNames = LastSelectedItem0.ColumNames,
                Header = sheetData.Header,
                IndexId = sheetData.IndexId,
                PattnPageList = sheetData.PattnPageList
            };
        }


        private static bool ColumnIsUniformed0;
        public bool ColumnIsUniformed { get => ColumnIsUniformed0; set { ColumnIsUniformed0 = value; } }


        internal static SheetDataModel LastSelectedItem0 { get; set; }
        //protected internal SheetDataModel LastSelectedItem { get => LastSelectedItem0; }
        //public IEnumerable DefaultColumNames { get => ColumnIsUniformed ? LastSelectedItem0.ColumNames : null; }
        public ICollectionView PattnPageList { get => new ListCollectionView(SheetData.PattnPageList); }

        /// <summary>
        /// DnD: To be assigned and refferenced exclusively from [CmdUniform];
        /// </summary>
        //protected internal static SheetDataModel DftWkSheet { get; set; }
        public ViewsCommand CmdUniform { get => new ViewsCommand(this); }

        /// DnD TutRx
        ///Task<int> task = new Task<int>(() => { return LastSelectedItem0.IndexId; });
        ///
    }

    public class SheetDataModel
    {
        public ObservableCollection<PrintFormat> PattnPageList { get; protected internal set; }
        public string Header { get; set; }
        public IEnumerable ColumNames { get; set; }
        /// <summary>
        /// Intended to be used to synchronize and identify the TabItem to which
        /// an object of this class is assigned.
        /// </summary>
        public int IndexId { get; internal set; }
    }


    public class ViewsCommand : ICommand
    {
        public ViewsCommand(WkSheetViewModel wkSheet)
        {
            this.WkSheet = wkSheet;
        }

        public ViewsCommand(SheetDataModel sheetData)
        {
            SelectedSheetData = sheetData;
        }

        //public static WkSheetModel WkSheet;

        public Int32 SelectionIndex { get; set; }
        public WkSheetViewModel WkSheet { get; set; }
        public SheetDataModel SelectedSheetData { get; }
        protected internal static PattnViewModel WkBookData { private get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            var chk = parameter is int index && index != 0;
            ///DnD DebugOnly.
            Execute(null);
            return true;
        }
        public void Execute(object parameter)
        {
            ///DnD RefCode:
            ///if (!(((ObservableCollection<WkSheetModel>)parameter).Select(p => p.CurrentSelection).FirstOrDefault() is int i))
            ///     return;
            if (!(parameter is ObservableCollection<WkSheetViewModel> collectn))
                return;
            //var index = this.SelectionIndex = WkSheet.LastWkSheetIndex;

            //this.PattnArr = collectn;
            //var prev = this.SelectionIndex;
            // index = this.SelectionIndex = WkSheet.SheetData == null ? 0 :
            //    this.PattnArr.IndexOf(WkSheet);

            if (WkSheet.ColumnIsUniformed)
            {
                //WkSheetViewModel.DftWkSheet = WkSheet.LastSelectedItem;

            }
            else
            {
                //WkSheet.SheetData = WkSheetViewModel.DftWkSheet;
                //WkSheetViewModel.DftWkSheet = null;

                //WkBookData[WkSheetViewModel.LastSelectedItem0.IndexId] 
                //    =new WkSheetViewModel { SheetData = WkSheetViewModel.LastSelectedItem0 };

                //WkBookData.SelectedItem = new WkSheetViewModel { SheetData = WkSheetViewModel.LastSelectedItem0 };
                //WkSheet.SheetData = WkSheetViewModel.LastSelectedItem0;
                //var index = Convert.ToInt32(WkSheetViewModel.LastSelectedItem0.IndexId);
                //var wkSht = ViewsCommand.WkBookData[index].SheetData;
                //wkSht.ColumNames = WkSheetViewModel.AppendColums;

            }
        }
    }

}
