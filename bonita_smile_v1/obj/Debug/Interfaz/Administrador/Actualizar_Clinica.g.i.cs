﻿#pragma checksum "..\..\..\..\Interfaz\Administrador\Actualizar_Clinica.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5CEA5908FBC7B50049EEB1C7DD1252814466680A8809343C417C73602673B839"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace bonita_smile_v1 {
    
    
    /// <summary>
    /// Page5_Actualizar
    /// </summary>
    public partial class Page5_Actualizar : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\..\..\Interfaz\Administrador\Actualizar_Clinica.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNombre;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Interfaz\Administrador\Actualizar_Clinica.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbColor;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Interfaz\Administrador\Actualizar_Clinica.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFinalizar;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Interfaz\Administrador\Actualizar_Clinica.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblColor;
        
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
            System.Uri resourceLocater = new System.Uri("/bonita_smile_v1;component/interfaz/administrador/actualizar_clinica.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Interfaz\Administrador\Actualizar_Clinica.xaml"
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
            this.txtNombre = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.cmbColor = ((System.Windows.Controls.ComboBox)(target));
            
            #line 35 "..\..\..\..\Interfaz\Administrador\Actualizar_Clinica.xaml"
            this.cmbColor.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbColor_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnFinalizar = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\..\Interfaz\Administrador\Actualizar_Clinica.xaml"
            this.btnFinalizar.Click += new System.Windows.RoutedEventHandler(this.btnFinalizar_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lblColor = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

