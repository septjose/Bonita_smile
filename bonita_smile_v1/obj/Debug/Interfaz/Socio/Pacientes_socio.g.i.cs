﻿#pragma checksum "..\..\..\..\Interfaz\Socio\Pacientes_socio.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "53D1C0DB79C9475E42A55F40E895D0B5B4B9F8E71B5113282954C80A2821C7D9"
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


namespace bonita_smile_v1.Interfaz.Socio {
    
    
    /// <summary>
    /// Pacientes_socio
    /// </summary>
    public partial class Pacientes_socio : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\..\Interfaz\Socio\Pacientes_socio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNombre;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Interfaz\Socio\Pacientes_socio.xaml"
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
            System.Uri resourceLocater = new System.Uri("/bonita_smile_v1;component/interfaz/socio/pacientes_socio.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Interfaz\Socio\Pacientes_socio.xaml"
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
            
            #line 35 "..\..\..\..\Interfaz\Socio\Pacientes_socio.xaml"
            this.txtNombre.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtNombre_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lv_Paciente = ((System.Windows.Controls.ListView)(target));
            return;
            case 3:
            
            #line 110 "..\..\..\..\Interfaz\Socio\Pacientes_socio.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 137 "..\..\..\..\Interfaz\Socio\Pacientes_socio.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Actualizar);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 165 "..\..\..\..\Interfaz\Socio\Pacientes_socio.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Borrar);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
