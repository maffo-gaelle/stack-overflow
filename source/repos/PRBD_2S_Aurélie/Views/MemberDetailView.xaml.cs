﻿using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace PRBD_2S_Aurélie
{
    public partial class MemberDetailView : UserControlBase {

        public Member Member { get; set; }

        public MemberDetailView(Member member) {
            InitializeComponent();
            DataContext = this;
            Member = member;
        }
    }
}
