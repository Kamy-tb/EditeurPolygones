﻿#pragma checksum "..\..\ChangeColor.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "077CD51C60ED4D3DB91EBDD6AD8F9685E91626E2D0A5DE5F2775C3A61B3F11E1"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
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
using Team5Projet2CP;


namespace Team5Projet2CP {
    
    
    /// <summary>
    /// ChangeColor
    /// </summary>
    public partial class ChangeColor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\ChangeColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas ColorPickerCanvas;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\ChangeColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ColorDescrTBlock;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\ChangeColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OkButton;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\ChangeColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle sampleRec;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\ChangeColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBoxR;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\ChangeColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBoxG;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\ChangeColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBoxB;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\ChangeColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider SliderR;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\ChangeColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider SliderG;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\ChangeColor.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider SliderB;
        
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
            System.Uri resourceLocater = new System.Uri("/Team5Projet2CP;component/changecolor.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChangeColor.xaml"
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
            
            #line 8 "..\..\ChangeColor.xaml"
            ((Team5Projet2CP.ChangeColor)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ColorPickerCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 15 "..\..\ChangeColor.xaml"
            this.ColorPickerCanvas.Loaded += new System.Windows.RoutedEventHandler(this.ColorPickerCanvas_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ColorDescrTBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.OkButton = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\ChangeColor.xaml"
            this.OkButton.Click += new System.Windows.RoutedEventHandler(this.OkButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.sampleRec = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            this.TBoxR = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.TBoxG = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.TBoxB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.SliderR = ((System.Windows.Controls.Slider)(target));
            
            #line 49 "..\..\ChangeColor.xaml"
            this.SliderR.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SliderR_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.SliderG = ((System.Windows.Controls.Slider)(target));
            
            #line 50 "..\..\ChangeColor.xaml"
            this.SliderG.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SliderG_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.SliderB = ((System.Windows.Controls.Slider)(target));
            
            #line 51 "..\..\ChangeColor.xaml"
            this.SliderB.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SliderB_ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

