
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
		private readonly INavigationService _navigationService;

		public BranchViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public CompanyViewModel CompanyViewModel {
			get {
				return SimpleIoc.Default.GetInstance<CompanyViewModel>();
			}
		}

		private RelayCommand<string> _saveBranchCommand;

		public RelayCommand<string> SaveBranchCommand {
			get {
				return _saveBranchCommand = new RelayCommand<string>(name => {
					CompanyViewModel.EditBranch(Events, Promotions, name);
					RaisePropertyChanged(nameof(Events));
					RaisePropertyChanged(nameof(Promotions));
				});
			}
		}

		private List<Promotion> _promotions;
		public List<Promotion> Promotions
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

		public void AddPromotion(Promotion promotion)
		{
			MySelectedPromotion = promotion;
			Promotions[Promotions.FindIndex(i => i.Equals(MySelectedPromotion))] = MySelectedPromotion;

			SaveBranchCommand.Execute("Promotion");
			RaisePropertyChanged(nameof(Promotions));
		}

		public void EditPromotion(string title, string description, DateTime startdate, DateTime enddate)
		{
			MySelectedPromotion.Title = title;
			MySelectedPromotion.Description = description;
			MySelectedPromotion.StartDate = startdate;
			MySelectedPromotion.EndDate = enddate;

			Promotions[Promotions.FindIndex(i => i.Equals(MySelectedPromotion))] = MySelectedPromotion;

			SaveBranchCommand.Execute("Promotion");
			RaisePropertyChanged(nameof(Promotions));
		}

		public void DeletePromotion()
		{
			Promotions.RemoveAt(Events.FindIndex(i => i.Equals(MySelectedPromotion)));
			SaveBranchCommand.Execute("Branch");
			RaisePropertyChanged(nameof(Promotions));
		}

		private RelayCommand _loadPromotionsCommand;

		public RelayCommand LoadPromotionsCommand
		{
			get
			{
				return _loadPromotionsCommand ?? (_loadPromotionsCommand = new RelayCommand(() => {
					Promotions = CompanyViewModel.SelectedBranch.Promotions;
					var currentDate = DateTime.Today.Date;
					CurrentPromotions = Promotions.Where(p => p.StartDate <= currentDate && p.EndDate >= currentDate).ToList();
					NonCurrentPromotions = Promotions.Except(CurrentPromotions).ToList();
				}
				));
			}
		}

		private List<Event> _events;
		public List<Event> Events
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
				return _loadEventsCommand = new RelayCommand(() => Events = CompanyViewModel.SelectedBranch.Events);
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

		public void AddEvent(string title, string description, DateTime startDate, DateTime endDate) {
			SelectedEvent.Title = title;
			SelectedEvent.Description = description;
			SelectedEvent.StartDate = startDate;
			SelectedEvent.EndDate = endDate;

			Events.Add(SelectedEvent);

			SaveBranchCommand.Execute("Event");
			_navigationService.NavigateTo(nameof(BranchEventsPage));
		}

		public void EditEvent(string title, string description, DateTime startDate, DateTime endDate) {
			SelectedEvent.Title = title;
			SelectedEvent.Description = description;
			SelectedEvent.StartDate = startDate;
			SelectedEvent.EndDate = endDate;

			Events [Events.FindIndex(i => i.Equals(SelectedEvent))] = SelectedEvent;

			SaveBranchCommand.Execute("Event");
			RaisePropertyChanged(nameof(Events));
		}

		public void DeleteEvent()
		{
			Events.RemoveAt(Events.FindIndex(i => i.Equals(SelectedEvent)));
			SaveBranchCommand.Execute("Branch");
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
