﻿#pragma checksum "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D98B942636E60CF9CFDCCE96741D6CB9D51019FF3CD079D89D4BAA9D28C3FE16"
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
using bonita_smile_v1.Interfaz.Administrador;


namespace bonita_smile_v1.Interfaz.Administrador {
    
    
    /// <summary>
    /// Ventana_Administrador
    /// </summary>
    public partial class Ventana_Administrador : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lv_Paciente;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtnombre;
        
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
            System.Uri resourceLocater = new System.Uri("/bonita_smile_v1;component/interfaz/administrador/ventana_administrador.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
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
            
            #line 12 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.usu_nuevo_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 13 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.usu_ver_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 14 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.usu_borrar_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 16 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.usu_actualizar_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 19 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.cli_nuevo_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 20 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.cli_ver_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 21 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.cli_borrar_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 23 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.cli_actualizar_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 26 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.pac_nuevo_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 27 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.pac_ver_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 28 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.pac_borrar_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 30 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.pac_actualizar_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.lv_Paciente = ((System.Windows.Controls.ListView)(target));
            
            #line 35 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            this.lv_Paciente.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lv_Paciente_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.txtnombre = ((System.Windows.Controls.TextBox)(target));
            return;
            case 15:
            
            #line 52 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 54 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_3);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 55 "..\..\..\..\Interfaz\Administrador\Ventana_Administrador.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

