﻿#pragma checksum "..\..\..\..\Views\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "35B89C416395E631696991E003A0FB4D742F6853"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DynamicDatagridColumns.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DynamicDatagridColumns.Views {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DynamicDatagridColumns.Views.MainWindow MainWind;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAddTab;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox _txNewTab;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAddColumn;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox _combNewColumn;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chkUnifyColumns;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtColmRports;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\Views\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DynamicDatagridColumns.Views.ViewsDatagrid _viewsDatagrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        
        #line 34 "..\..\..\..\Views\MainWindow.xaml"
 void Checked(object sender, RoutedEventArgs e)
                        {
                         var msg = "activated!";
                        if (!(bool)chkUnifyColumns.IsChecked)
                        {
                            var index = _viewsDatagrid._tabControl.SelectedIndex ;
                            _viewsDatagrid._tabControl.SelectedIndex = -1;
                            msg = "de-" + msg.Replace("!", "");

                            var tabItem = _viewsDatagrid._tabControl.ItemContainerGenerator.ContainerFromIndex(index) as TabItem;//[index];
                            tabItem.IsSelected = true;
                        }
                        MessageBox.Show(this, "Colums' uniformity has been " + msg, "Columns Uniformity Changed (" + msg.ToUpper() + ")",
                            MessageBoxButton.OK, MessageBoxImage.Information);                
                        } 
        #line default
        #line hidden
        
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DynamicDatagridColumns;component/views/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainWind = ((DynamicDatagridColumns.Views.MainWindow)(target));
            return;
            case 2:
            this.BtnAddTab = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\Views\MainWindow.xaml"
            this.BtnAddTab.Click += new System.Windows.RoutedEventHandler(this.BtnAddTab_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this._txNewTab = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.BtnAddColumn = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\Views\MainWindow.xaml"
            this.BtnAddColumn.Click += new System.Windows.RoutedEventHandler(this.BtnAddColumn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this._combNewColumn = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.chkUnifyColumns = ((System.Windows.Controls.CheckBox)(target));
            
            #line 31 "..\..\..\..\Views\MainWindow.xaml"
            this.chkUnifyColumns.Click += new System.Windows.RoutedEventHandler(this.Checked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.txtColmRports = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this._viewsDatagrid = ((DynamicDatagridColumns.Views.ViewsDatagrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

