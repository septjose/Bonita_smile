﻿#pragma checksum "..\..\..\..\Interfaz\Recepcionista\Recepcionista_Principal.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0083F2E9D3FDA674192E9DE3848257982D69B93651F9ABE76DD5EBCF9113314E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using bonita_smile_v1;


namespace bonita_smile_v1.Interfaz.Recepcionista {
    
    
    /// <summary>
    /// Recepcionista_Principal
    /// </summary>
    public partial class Recepcionista_Principal : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\..\Interfaz\Recepcionista\Recepcionista_Principal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txt_Titulo;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Interfaz\Recepcionista\Recepcionista_Principal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNombre;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Interfaz\Recepcionista\Recepcionista_Principal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lv_Paciente;
        
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
            System.Uri resourceLocater = new System.Uri("/bonita_smile_v1;component/interfaz/recepcionista/recepcionista_principal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Interfaz\Recepcionista\Recepcionista_Principal.xaml"
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
            this.txt_Titulo = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.txtNombre = ((System.Windows.Controls.TextBox)(target));
            
            #line 44 "..\..\..\..\Interfaz\Recepcionista\Recepcionista_Principal.xaml"
            this.txtNombre.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtNombre_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 46 "..\..\..\..\Interfaz\Recepcionista\Recepcionista_Principal.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 48 "..\..\..\..\Interfaz\Recepcionista\Recepcionista_Principal.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lv_Paciente = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

