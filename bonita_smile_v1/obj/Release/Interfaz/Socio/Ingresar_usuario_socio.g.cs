﻿#pragma checksum "..\..\..\..\Interfaz\Socio\Ingresar_usuario_socio.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3A49A597756BCFF3E1F1CE0AA67494A02AD29611AA2F71A023F76CC5B998B0D5"
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


namespace bonita_smile_v1.Interfaz.Socio {
    
    
    /// <summary>
    /// Ingresar_usuario_socio
    /// </summary>
    public partial class Ingresar_usuario_socio : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\..\Interfaz\Socio\Ingresar_usuario_socio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNombre;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Interfaz\Socio\Ingresar_usuario_socio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtApellido;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Interfaz\Socio\Ingresar_usuario_socio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAlias;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Interfaz\Socio\Ingresar_usuario_socio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pwbPassword;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Interfaz\Socio\Ingresar_usuario_socio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbRol;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Interfaz\Socio\Ingresar_usuario_socio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFinalizar;
        
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
            System.Uri resourceLocater = new System.Uri("/bonita_smile_v1;component/interfaz/socio/ingresar_usuario_socio.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Interfaz\Socio\Ingresar_usuario_socio.xaml"
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
            this.txtApellido = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtAlias = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.pwbPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.cmbRol = ((System.Windows.Controls.ComboBox)(target));
            
            #line 40 "..\..\..\..\Interfaz\Socio\Ingresar_usuario_socio.xaml"
            this.cmbRol.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbRol_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnFinalizar = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\..\Interfaz\Socio\Ingresar_usuario_socio.xaml"
            this.btnFinalizar.Click += new System.Windows.RoutedEventHandler(this.btnFinalizar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

