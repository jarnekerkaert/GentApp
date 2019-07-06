﻿using GentApp.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.ViewModels
{
	public class BranchViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<Promotion> Promotions { get; set; }

		private Promotion mySelectedPromotion;
		public Promotion MySelectedPromotion
		{
			get { return mySelectedPromotion; }
			set
			{
				if (value != mySelectedPromotion)
				{
					mySelectedPromotion = value; NotifyPropertyChanged("MySelectedPromotion");
				}
			}
		}
		public BranchViewModel()
		{
			Promotions = new ObservableCollection<Promotion>(DummyDataSource.Promotions);

		}

		public void AddPromotion(Promotion newPromotion)
		{
			this.Promotions.Add(newPromotion);
		}

		public void EditPromotion(string title, string description, DateTime startdate, DateTime enddate)
		{
			var oldPromotion = MySelectedPromotion;
			if (oldPromotion != null)
			{
				oldPromotion.Title = title;
				oldPromotion.Description = description;
				oldPromotion.StartDate = startdate;
				oldPromotion.EndDate = enddate;
			}
		}

		public void DeletePromotion()
		{
			this.Promotions.Remove(MySelectedPromotion);
		}

		private void NotifyPropertyChanged(String propertyName)
		{
			if (null != PropertyChanged)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
