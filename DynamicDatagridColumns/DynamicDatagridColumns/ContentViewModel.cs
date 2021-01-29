using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Printing;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using SharedResx_NetStandard;
using DynamicDatagridColumns.Views;

namespace DynamicDatagridColumns
{

    public class ContentViewModel : INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ContentViewModel()
        {
        }


        public ObservableCollection<DataGridColumn> DataGridColumns { get; set; }
        private WorkSheetInfo SelectedWorkSheet0;
        public WorkSheetInfo SelectedWorkSheet
        {
            get { return SelectedWorkSheet0; }
            set
            {
                if (value == null)
                {
                    SelectedWorkSheet0 = null;
                    return;

                }
                //int i = Items0.IndexOf(value);
                bool initSelect = value.InitSelected, exists = Items0.Contains(value);

                if (!exists)
                {
                    Items0.Add(value);
                }
                else if (!initSelect)
                    WorkSheetInfo.PrevInstance = SelectedWorkSheet0 = value;

                if (initSelect || WorkSheetInfo.PrevInstance == null)
                    WorkSheetInfo.PrevInstance = SelectedWorkSheet0 = value;
            }
        }

        public IEnumerable<WorkSheetInfo> Items { get { return Items0; } }
        private ObservableCollection<WorkSheetInfo> Items0 = new ObservableCollection<WorkSheetInfo>();






        public  ColorsRepository Collection { get; set; }

        private PageMediaSize MediaSize0;
        public PageMediaSize MediaSize
        {
            ///DnD: NOT TO BE FURTHER SIMPLIFIED
            get => MediaSize0 ?? new PageMediaSize(816, 1056);
            set { MediaSize0 = value; }
        }

        public ViewsDatagrid ContextDataGrid { get; set; }
        public bool IsSizedToPage
        {
            get => Items0.Where(i => i.PagingData != null &&
        i.PagingData.GetType() == typeof(ViewsDatagrid)).ToArray().Length == Items0.Count;
        }
        public bool IsSizedToPage2
        {
            get
            {
                var x = Items0.Where(i => i.PagingData == null).ToArray().Length;
                var x2 = Items0.Where(i => i.PagingData != null && i.PagingData.GetType()
                == typeof(ViewsDatagrid)).ToArray().Length;

                var r = x == 0 && x2 == Items0.Count;

                return r;
            }
        }

        public CheckStatus PagingStatus { get; set; }

        //public bool Hybrid { get => _items.Count < 1 || !IsSizedToPage; }

        internal void ResetTimeCode(ref object item)
        {
            ((WorkSheetInfo)item).TimeCode = Stat.AnnualToken;

        }

        public void ResetSelection(TabItem tabItm)
        {
            var eventArgs = new PropertyChangedEventArgs("WorkSheetInfo");
            PropertyChanged(this, eventArgs);
        }
        public void ResetSelection(TabItem tabItm, WorkSheetInfo sel)
        {
            sel.TimeCode = Stat.AnnualToken;
            
            Items0[Items0.IndexOf(sel)] = sel;
        }

    }


    public class ColorsRepository : Collection<WorkSheetInfo>, IColorsRepository
    {
        public ColorsRepository(bool colorEditMode)
        {


            var SharpColors = new List<string>();

            if (!colorEditMode)
            {
                string ColorsList = Stat.AppDir + "WorkSheetInfos";
                var allText = File.ReadAllText(ColorsList);

                if (allText != null && allText.Trim() != "")
                {
                    string[] colList = allText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    SharpColors.AddRange(colList);
                   
                }
            }

            foreach (PropertyInfo property in typeof(Colors).GetProperties())
            {
                var pNm = property.GetValue(null, null).ToString();// property.Name;
                if (!SharpColors.Contains(pNm) && SharpColors.Count > 1 && !colorEditMode)
                    continue;
                Add(new WorkSheetInfo() { Key = property.Name, Val = (Color)property.GetValue(null, null) });
            }

        }
    }

    public interface IColorsRepository { }

    [Serializable()]
    public class WorkSheetInfo
    {
        public WorkSheetInfo()
        {
            TimeCode = Stat.AnnualToken;

        }

        private static WorkSheetInfo TimeCodeUpdate(WorkSheetInfo sheetInfo, string timeCode)
        {
            var newWksInfo = new WorkSheetInfo
            {
                TimeCode = timeCode,

                Criteria = sheetInfo.Criteria,
                InitSelected = sheetInfo.InitSelected,
                PagingData = sheetInfo.PagingData,
                PattnList = sheetInfo.PattnList,
                Val = sheetInfo.Val,
                Key = sheetInfo.Key
            };

            return newWksInfo;

        }
         
        //public WorkSheetInfo TimeCodeUpdate(WorkSheetInfo sheetInfo, string timeCode)
        //{

        //    return new WorkSheetInfo(sheetInfo, timeCode);
        //}

        #region Properties
        public string Key { get; set; }
        public string TimeCode
        {
            get;
            protected internal set;
        }

        public Color Val { get; set; }
        public ICollectionView PattnList { get; set; }
        public string Criteria { get; set; }
        //public Cursor Curso { get; set; }  
        public object PagingData { get; set; }

        public SolidColorBrush PageColor { get => new SolidColorBrush(Val); }

        private bool InitDeSelected0;
        /// <summary>
        /// When 'true' class will not initialized as current selection
        /// but reverts automatically to false after first query. This ensures
        /// that only the 1st TabItem is initially selectec when multiple tabs
        /// are loaded
        /// </summary>
        public bool InitSelected
        {
            get
            {
                var temp = InitDeSelected0;
                InitDeSelected0 = false;
                return temp;
            }
            set { InitDeSelected0 = value; }
        }

        /// <summary>
        /// LastInstance
        /// </summary>
        public static WorkSheetInfo PrevInstance { get; internal set; }



        #endregion

        #region Methosd

        public override string ToString()
        {
            return Key;
        }

        public static explicit operator WorkSheetInfo(TabItem v)
        {
            return v.Content as WorkSheetInfo;
        }

        #endregion
    }


}