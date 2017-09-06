using System;
using Xamarin.Forms;
using Nostromo.Interfaces;
using ReactiveUI;

namespace TriRanking
{
	public class ViewPage : ContentPage
	{

		public virtual IPageViewModel ViewModel {
			get {
				return BindingContext as IPageViewModel;
			}
			set {
				BindingContext = value;
			}
		}

		public ViewPage ()
		{
		}

		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();
			this.OnPropertyChanged ("ViewModel");
		}
	}

	public class ViewPage<T> : ViewPage, IViewFor<T>
		where T : class, IPageViewModel
	{
		object IViewFor.ViewModel {
			get {
				return base.ViewModel;
			}

			set {
				base.ViewModel = value as IPageViewModel;
			}
		}

		public new T ViewModel {
			get { return ((IViewFor<T>)this).ViewModel; }
		}

		T IViewFor<T>.ViewModel {
			get {
				return base.ViewModel as T;
			}

			set {
				base.ViewModel = value as T;
			}
		}
	}
}

