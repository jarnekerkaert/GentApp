﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GentApp.DataModel;
using MetroLog;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GentApp.ViewModels
{
	public class BranchViewModel : ViewModelBase
	{
		private readonly ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<BranchViewModel>();
		//private readonly CompanyService companyService = new CompanyService();
		//private readonly BranchService branchService = new BranchService();
		//private readonly INavigationService _navigationService;

		//public BranchViewModel(INavigationService navigationService)
		public BranchViewModel()
		{
			//_navigationService = navigationService;
			Promotions = new ObservableCollection<Promotion>(DummyDataSource.Promotions);
		}

		public ObservableCollection<Promotion> Promotions { get; set; }

		private Promotion mySelectedPromotion;
		public Promotion MySelectedPromotion
		{
			get { return mySelectedPromotion; }
			set
			{
				if (value != mySelectedPromotion)
				{
					mySelectedPromotion = value;
					RaisePropertyChanged(nameof(MySelectedPromotion));
				}
			}
		}

		public async void AddPromotion(Promotion newPromotion)
		{
			this.Promotions.Add(newPromotion);
			//MainPage.BranchesViewModel.MySelectedBranch.Promotions.ToList().Add(newPromotion);

			//Branch updatedBranch = MainPage.BranchesViewModel.MySelectedBranch;
			//var branchJson = JsonConvert.SerializeObject(updatedBranch);
			//HttpClient client = new HttpClient();
			//var res = await client.PutAsync("http://localhost:50957/api/branches/" + MainPage.BranchesViewModel.MySelectedBranch.Id, new StringContent(branchJson, System.Text.Encoding.UTF8, "application/json"));

			//var promotionJson = JsonConvert.SerializeObject(newPromotion);
			//HttpClient client = new HttpClient();
			//var res = await client.PostAsync("http://localhost:50957/api/promotions", new StringContent(promotionJson, System.Text.Encoding.UTF8, "application/json"));
		}

		public async void EditPromotion(string title, string description, DateTime startdate, DateTime enddate)
		{
			var oldPromotion = MySelectedPromotion;
			if (oldPromotion != null)
			{
				oldPromotion.Title = title;
				oldPromotion.Description = description;
				oldPromotion.StartDate = startdate;
				oldPromotion.EndDate = enddate;
			}

			//	var promotionJson = JsonConvert.SerializeObject(oldPromotion);
			//	HttpClient client = new HttpClient();
			//	var res = await client.PutAsync("http://localhost:50957/api/branches/" + MainPage.BranchesViewModel.MySelectedBranch.Id + "/promotions", new StringContent(promotionJson, System.Text.Encoding.UTF8, "application/json"));
		}

		public void DeletePromotion()
		{
			this.Promotions.Remove(MySelectedPromotion);
		}

	}
}
