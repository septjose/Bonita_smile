﻿#pragma checksum "..\..\..\..\Interfaz\Administrador\Ingresar_Permisos.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C2181125DCAD8DE20183A6473140E30A975AD945FB4C522396F101DCFA082ED3"
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
    /// Pagina_Ingresar_Permisos
    /// </summary>
    public partial class Pagina_Ingresar_Permisos : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\..\Interfaz\Administrador\Ingresar_Permisos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbUsuario;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Interfaz\Administrador\Ingresar_Permisos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbClinica;
        
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
            System.Uri resourceLocater = new System.Uri("/bonita_smile_v1;component/interfaz/administrador/ingresar_permisos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Interfaz\Administrador\Ingresar_Permisos.xaml"
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
            this.cmbUsuario = ((System.Windows.Controls.ComboBox)(target));
            
            #line 34 "..\..\..\..\Interfaz\Administrador\Ingresar_Permisos.xaml"
            this.cmbUsuario.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbUsuario_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cmbClinica = ((System.Windows.Controls.ComboBox)(target));
            
            #line 35 "..\..\..\..\Interfaz\Administrador\Ingresar_Permisos.xaml"
            this.cmbClinica.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbClinica_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 36 "..\..\..\..\Interfaz\Administrador\Ingresar_Permisos.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

