﻿using System;
using System.Collections.Generic;
using Nostromo;
using TriRanking.Core;
using Xamarin.Forms;

namespace TriRanking
{
	[ViewFor(typeof(HomeViewModel))]
    public partial class HomePage : ViewPage<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
			
            if (ViewModel != null)
			{

                this.pickerAnno.Items.Clear();
                this.pickerAnno.ItemsSource = ViewModel.PickerAnno;
                this.pickerAnno.SelectedIndex = 0;

				this.pickerSesso.Items.Clear();
                this.pickerSesso.ItemsSource = ViewModel.PickerSesso;
				this.pickerSesso.SelectedIndex = 0;

				this.pickerTipoGara.Items.Clear();
                this.pickerTipoGara.ItemsSource = ViewModel.PickerTipoGara;
				this.pickerTipoGara.SelectedIndex = 0;

                this.pickerCategoria.Items.Clear();
                this.pickerCategoria.ItemsSource = ViewModel.PickerCategoria;
                this.pickerCategoria.SelectedIndex = 0;
			}
        }
    }
}
