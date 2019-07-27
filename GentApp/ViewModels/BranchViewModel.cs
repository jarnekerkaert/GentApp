using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GentApp.DataModel;
using GentApp.Services;
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
		private readonly CompanyService companyService = new CompanyService();
		private readonly PromotionService promotionService = new PromotionService();
		private readonly BranchService branchService = new BranchService();
		private readonly EventService eventService = new EventService();
		//private readonly INavigationService _navigationService;

		//public BranchViewModel(INavigationService navigationService)
		public BranchViewModel()
		{
			//_navigationService = navigationService;
			//Promotions = new ObservableCollection<Promotion>(DummyDataSource.Promotions);
		}

		private IEnumerable<Promotion> _promotions;
		public IEnumerable<Promotion> Promotions
		{
			get
			{
				return _promotions;
			}

			set
			{
				_promotions = value;
				RaisePropertyChanged(nameof(Promotions));
			}
		}

		private IEnumerable<Promotion> _currentPromotions;
		public IEnumerable<Promotion> CurrentPromotions
		{
			get
			{
				return _currentPromotions;
			}

			set
			{
				_currentPromotions = value;
				RaisePropertyChanged(nameof(CurrentPromotions));
			}
		}

		private IEnumerable<Promotion> _nonCurrentPromotions;
		public IEnumerable<Promotion> NonCurrentPromotions
		{
			get
			{
				return _nonCurrentPromotions;
			}

			set
			{
				_nonCurrentPromotions = value;
				RaisePropertyChanged(nameof(NonCurrentPromotions));
			}
		}

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

		public async void AddPromotion(Promotion promotion)
		{
			await promotionService.Save(promotion);

			//this.Promotions.Add(newPromotion);
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
			MySelectedPromotion.Title = title;
			MySelectedPromotion.Description = description;
			MySelectedPromotion.StartDate = startdate;
			MySelectedPromotion.EndDate = enddate;
			//await companyService.Update(SimpleIoc.Default.GetInstance<CompaniesViewModel>().MyCompany);
			await promotionService.Update(MySelectedPromotion);
			RaisePropertyChanged(nameof(Promotions));
		}

		public async void DeletePromotion()
		{
			//this.Promotions.Remove(MySelectedPromotion);
			await promotionService.Delete(MySelectedPromotion);
			RaisePropertyChanged(nameof(Promotions));
		}

		private RelayCommand _loadPromotionsCommand;

		public RelayCommand LoadPromotionsCommand
		{
			get
			{
				return _loadPromotionsCommand ?? (_loadPromotionsCommand = new RelayCommand(async () => {
					Promotions = await branchService.GetPromotions(SimpleIoc.Default.GetInstance<CompanyViewModel>().SelectedBranch.Id);
					var currentDate = DateTime.Today.Date;
					CurrentPromotions = Promotions.Where(p => p.StartDate <= currentDate && p.EndDate >= currentDate).ToList();
					NonCurrentPromotions = Promotions.Except(CurrentPromotions).ToList();
				}
				));
			}
		}

		private IEnumerable<Event> _events;
		public IEnumerable<Event> Events
		{
			get
			{
				return _events;
			}

			set
			{
				_events = value;
				RaisePropertyChanged(nameof(Events));
			}
		}

		private RelayCommand _loadEventsCommand;

		public RelayCommand LoadEventsCommand
		{
			get
			{
				return _loadEventsCommand ?? (_loadEventsCommand = new RelayCommand(async () => {
					Events = await branchService.GetEvents(SimpleIoc.Default.GetInstance<CompanyViewModel>().SelectedBranch.Id);
				}
				));
			}
		}

		public async void AddEvent(Event newEvent)
		{
			await eventService.Add(newEvent);
		}

		private Event _selectedEvent;
		public Event SelectedEvent
		{
			get { return _selectedEvent; }
			set
			{
				if (value != _selectedEvent)
				{
					_selectedEvent = value;
					RaisePropertyChanged(nameof(SelectedEvent));
				}
			}
		}

		public async void EditEvent(string title, string description, DateTime startdate, DateTime enddate)
		{
			SelectedEvent.Title = title;
			SelectedEvent.Description = description;
			SelectedEvent.StartDate = startdate;
			SelectedEvent.EndDate = enddate;
			await eventService.Update(SelectedEvent);
			RaisePropertyChanged(nameof(Events));
		}

		public async void DeleteEvent()
		{
			await eventService.Delete(SelectedEvent);
			RaisePropertyChanged(nameof(Events));
		}

	}
}
