﻿
using GentApp.DataModel;
using GentApp.Services;
using MetroLog;
using System;
using System.Collections.Generic;
using System.Linq;
using GentApp.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GentApp.Views;

namespace GentApp.ViewModels
{
	public class BranchViewModel : ViewModelBase
	{
		private readonly ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<BranchViewModel>();
		private readonly CompanyService companyService = new CompanyService();
		private readonly PromotionService promotionService = new PromotionService();
		private readonly BranchService branchService = new BranchService();
		private readonly EventService eventService = new EventService();
		private readonly INavigationService _navigationService;
		
		public BranchViewModel(Helpers.INavigationService navigationService)
		{
			_navigationService = navigationService;
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
		}

		public async void EditPromotion(string title, string description, DateTime startdate, DateTime enddate)
		{
			MySelectedPromotion.Title = title;
			MySelectedPromotion.Description = description;
			MySelectedPromotion.StartDate = startdate;
			MySelectedPromotion.EndDate = enddate;
			await promotionService.Update(MySelectedPromotion);
			RaisePropertyChanged(nameof(Promotions));
		}

		public async void DeletePromotion()
		{
			await promotionService.Delete(MySelectedPromotion);
			RaisePropertyChanged(nameof(Promotions));
		}

		private RelayCommand _loadPromotionsCommand;

		public RelayCommand LoadPromotionsCommand
		{
			get
			{
				return _loadPromotionsCommand ?? (_loadPromotionsCommand = new RelayCommand(async () => {
					Promotions = SimpleIoc.Default.GetInstance<CompanyViewModel>().SelectedBranch.Promotions;
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

		private RelayCommand _toAddEventCommand;

		public RelayCommand ToAddEventCommand
		{
			get
			{
				return _toAddEventCommand = new RelayCommand(() => _navigationService.NavigateTo(nameof(AddEventPage)));
			}
		}

	}
}
