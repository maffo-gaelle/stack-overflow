﻿#pragma checksum "..\..\Connexion.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A81F7C64BA9EC24F87ED6F1E12DADB78BA2652F354AECFC3B8847F6210254FE3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using PRBD_2S_Aurélie;
using PRBD_2S_Aurélie.Properties;
using PRBD_Framework;
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


namespace PRBD_2S_Aurélie {
    
    
    /// <summary>
    /// Connexion
    /// </summary>
    public partial class Connexion : PRBD_Framework.WindowBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 2 "..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PRBD_2S_Aurélie.Connexion window;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUserName;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtPassword;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\Connexion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLogin;
        
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
            System.Uri resourceLocater = new System.Uri("/PRBD_2S_Aurélie;component/connexion.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Connexion.xaml"
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
            this.window = ((PRBD_2S_Aurélie.Connexion)(target));
            return;
            case 2:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.txtUserName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.btnLogin = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

