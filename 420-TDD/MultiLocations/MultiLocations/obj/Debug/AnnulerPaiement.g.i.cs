﻿#pragma checksum "..\..\AnnulerPaiement.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4047E98F9F1EB45F8A53D265852C11C62E1ECA5436C3921863AF677E75007D81"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MultiLocations;
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


namespace MultiLocations {
    
    
    /// <summary>
    /// AnnulerPaiement
    /// </summary>
    public partial class AnnulerPaiement : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\AnnulerPaiement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox IDPaiementList;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\AnnulerPaiement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MontantTxt;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\AnnulerPaiement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnAnnuler;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\AnnulerPaiement.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IDLocationTxt;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MultiLocations;component/annulerpaiement.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AnnulerPaiement.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\AnnulerPaiement.xaml"
            ((MultiLocations.AnnulerPaiement)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.IDPaiementList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 13 "..\..\AnnulerPaiement.xaml"
            this.IDPaiementList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.IDPaiementList_OnChange);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MontantTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.BtnAnnuler = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\AnnulerPaiement.xaml"
            this.BtnAnnuler.Click += new System.Windows.RoutedEventHandler(this.BtnAnnuler_OnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.IDLocationTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

