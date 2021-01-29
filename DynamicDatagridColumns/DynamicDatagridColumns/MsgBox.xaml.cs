using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DynamicDatagridColumns
{
    /// <summary>
    /// Interaction logic for MsgBox.xaml
    /// </summary>
    public partial class MsgBox : Window
    {
        public MsgBox()
        {

            //_incuededColumns.ItemsSource = this.IncluededColumns;
            //_excuededColumns.ItemsSource = this.ExcuededColumns;
            InitializeComponent();
            MinHeight = MaxHeight = Height = 300;
            MinWidth = MaxWidth = Width = 350;

            //this.Command = ((Model_ViewModels.WkSheetViewModel)DataContext).CmdUniform;
            //this.CommandParameter;
        }

        //public IEnumerable IncluededColumns0;
        public IEnumerable IncluededColumns
        {
            get => _incuededColumns.ItemsSource;
            set
            {
                _incuededColumns.ItemsSource = value;
            }
        }
        //public IEnumerable ExcuededColumns { get; set; }
        public IEnumerable ExcuededColumns
        {
            get => _excuededColumns.ItemsSource;
            set
            {
                _excuededColumns.ItemsSource = value;
            }
        }


        //private readonly string[] ExcludeList0;//=>ExcuededColumns;
        //private  string[] IncludeList0;//=>ExcuededColumns;
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void btnInclude_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button btn) || !btn.Name.Contains("clude"))
                return;

            var excluedMode = btn.Name.Contains("xclude");

            if (excluedMode)
            {
                if (this._incuededColumns.SelectedItems.Count < 1)
                    return;
                var colms = new string[this._incuededColumns.SelectedItems.Count];
                this._incuededColumns.SelectedItems.CopyTo(colms, 0);
                foreach (var col in colms)
                {
                    var index = this._incuededColumns.Items.IndexOf(col);
                    UpdateColums(index, excluedMode);
                }
            }
            else
            {
                if (this._excuededColumns.SelectedItems.Count < 1)
                    return;
                var colms = new string[this._excuededColumns.SelectedItems.Count];
                this._excuededColumns.SelectedItems.CopyTo(colms, 0);

                foreach (var col in colms)
                {
                    var index = this._excuededColumns.Items.IndexOf(col);
                    UpdateColums(index, excluedMode);
                }

            }

            return;
            if (btn.Name.Contains("xclude"))
            {
                if (!(this._incuededColumns.SelectedIndex is int index2) || index2 < 1)
                    if (this._incuededColumns.SelectedItems.Count > 1)
                        foreach (var obj in this._incuededColumns.SelectedItems)
                        {

                            index2 = this._incuededColumns.Items.IndexOf(obj);
                            goto cont;
                        }
                    else
                        return;

                    cont:

                var incList = new List<string>((string[])this._incuededColumns.ItemsSource);
                var colName = incList[index2];
                incList.Remove(colName);
                this._incuededColumns.ItemsSource = incList.ToArray();// source ((string[])this._incuededColumns.ItemsSource).. ..Remove(columnName);

                var xcList = new List<string>((string[])this._excuededColumns.ItemsSource);
                xcList.Add(colName);
                this._excuededColumns.ItemsSource = xcList.ToArray();
            }
            else
            {
                if (!(this._excuededColumns.SelectedIndex is int index) || index < 0)
                    return;


                var incList = new List<string>((string[])this._excuededColumns.ItemsSource);
                var colName = incList[index];
                incList.Remove(colName);
                this._excuededColumns.ItemsSource = incList.ToArray();// source ((string[])this._incuededColumns.ItemsSource).. ..Remove(columnName);

                var xcList = new List<string>((string[])this._incuededColumns.ItemsSource);
                xcList.Add(colName);
                this._incuededColumns.ItemsSource = xcList.ToArray();


            }
        }

        [Bindable(true)]
        [Category("String")]
        [Localizability(LocalizationCategory.Text)]
        public int SelectedIndex { get; set; }

        /// <summary>
        /// DnD Dispays columns selection dialog box and before closing, checks selected colums'
        /// list, [_includedColumns.ItemsSource] against its initially stored value [IncludeList0]
        /// if it  has changed in size (Length) or content during
        /// the updatd
        /// </summary>
        /// <param name="outList"></param>
        /// <returns>Returns 'true' if columns's has changed, otherwise returns 'false'</returns>
        internal bool OutPut(out string[] outList)
        {

            //this.Command = ((Model_ViewModels.WkSheetViewModel)DataContext).CmdUniform;
            //_IndexNo.Text = SelectedIndex.ToString();

            //_indexNo.Text = SelectedIndex.ToString();

            //var FindName("_IndexNo") as Int32;
            var resx = this.Resources["_IndexNo"].ToString();
            this.Resources["_IndexNo"] = SelectedIndex.ToString();
            resx = this.Resources["_IndexNo"].ToString();
            var inList = (string[])(_incuededColumns.ItemsSource as IEnumerable);
            this.ShowDialog();

            outList = (string[])_incuededColumns.ItemsSource;

            var updated = outList.Length != inList.Length || (outList.Where(l => !inList.Contains(l)).FirstOrDefault() != null);

            return updated;




            





        }






        private void UpdateColums(int colIndex, bool excludeMode)
        {
            if (excludeMode)
            {
                //if (!(this._incuededColumns.SelectedIndex is int index) || index < 1)
                //    if (this._incuededColumns.SelectedItems.Count > 1)
                //        foreach (var obj in this._incuededColumns.SelectedItems)
                //        {

                //            index = this._incuededColumns.Items.IndexOf(obj);
                //            goto cont;
                //        }
                //    else
                //        return;

                //    cont:

                var incList = new List<string>((string[])this._incuededColumns.ItemsSource);
                var colName = incList[colIndex];
                incList.Remove(colName);
                this._incuededColumns.ItemsSource = incList.ToArray();// source ((string[])this._incuededColumns.ItemsSource).. ..Remove(columnName);

                var xcList = new List<string>((string[])this._excuededColumns.ItemsSource);
                xcList.Add(colName);
                this._excuededColumns.ItemsSource = xcList.ToArray();
            }
            else
            {
                //if (!(this._excuededColumns.SelectedIndex is int index) || index < 0)
                //    return;


                var incList = new List<string>((string[])this._excuededColumns.ItemsSource);
                var colName = incList[colIndex];
                incList.Remove(colName);
                this._excuededColumns.ItemsSource = incList.ToArray();// source ((string[])this._incuededColumns.ItemsSource).. ..Remove(columnName);

                var xcList = new List<string>((string[])this._incuededColumns.ItemsSource);
                xcList.Add(colName);
                this._incuededColumns.ItemsSource = xcList.ToArray();


            }
        }
    }
}
/// <summary>
/// DnD TutResx: Creating bindable (command) propt in code-behind 
/// [Bindable(true)] 
/// [Category("Action")]
/// [Localizability(LocalizationCategory.NeverLocalize)] 
/// public ICommand Command { get; set; } 
/// [Bindable(true)] 
/// [Category("Action")] 
/// [Localizability(LocalizationCategory.NeverLocalize)] 
/// public object CommandParameter { get; set; }
/// </summary>