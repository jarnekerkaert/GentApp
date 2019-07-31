
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

namespace GentApp.ViewModels {
	public class BranchViewModel : ViewModelBase {
		private readonly ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<BranchViewModel>();
		private readonly INavigationService _navigationService;
		private readonly EventService _eventService;

		public BranchViewModel(INavigationService navigationService) {
			_eventService = new EventService();
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
		public List<Promotion> Promotions {
			get {
				return _promotions;
			}

			set {
				_promotions = value;
				RaisePropertyChanged(nameof(Promotions));
			}
		}

		private IEnumerable<Promotion> _currentPromotions;
		public IEnumerable<Promotion> CurrentPromotions {
			get {
				return _currentPromotions;
			}

			set {
				_currentPromotions = value;
				RaisePropertyChanged(nameof(CurrentPromotions));
			}
		}

		private IEnumerable<Promotion> _nonCurrentPromotions;
		public IEnumerable<Promotion> NonCurrentPromotions {
			get {
				return _nonCurrentPromotions;
			}

			set {
				_nonCurrentPromotions = value;
				RaisePropertyChanged(nameof(NonCurrentPromotions));
			}
		}

		private Promotion mySelectedPromotion;
		public Promotion MySelectedPromotion {
			get { return mySelectedPromotion; }
			set {
				if ( value != mySelectedPromotion ) {
					mySelectedPromotion = value;
					RaisePropertyChanged(nameof(MySelectedPromotion));
				}
			}
		}

		public void AddPromotion(Promotion promotion) {
			Promotions.Add(promotion);

			SaveBranchCommand.Execute("Promotion");
			RaisePropertyChanged(nameof(Promotions));
			_navigationService.NavigateTo(nameof(BranchPromotionsPage));
		}

		public void EditPromotion(string title, string description, DateTime startdate, DateTime enddate) {
			MySelectedPromotion.Title = title;
			MySelectedPromotion.Description = description;
			MySelectedPromotion.StartDate = startdate;
			MySelectedPromotion.EndDate = enddate;

			Promotions[Promotions.FindIndex(p => p.Id.Equals(MySelectedPromotion.Id))] = MySelectedPromotion;

			SaveBranchCommand.Execute("Promotion");
			RaisePropertyChanged(nameof(Promotions));
			_navigationService.NavigateTo(nameof(BranchPromotionsPage));
		}

		public void DeletePromotion() {
			Promotions.RemoveAt(Promotions.FindIndex(p => p.Id.Equals(MySelectedPromotion.Id)));
			SaveBranchCommand.Execute("Branch");
			RaisePropertyChanged(nameof(Promotions));
			_navigationService.NavigateTo(nameof(BranchPromotionsPage));
		}

		private RelayCommand _loadPromotionsCommand;

		public RelayCommand LoadPromotionsCommand {
			get {
				return _loadPromotionsCommand ?? ( _loadPromotionsCommand = new RelayCommand(() => {
					Promotions = CompanyViewModel.SelectedBranch.Promotions;
					var currentDate = DateTime.Today.Date;
					if ( Promotions != null && Promotions.Count != 0 ) {
						CurrentPromotions = Promotions.Where(p => p.StartDate <= currentDate && p.EndDate >= currentDate).ToList();
						NonCurrentPromotions = Promotions.Except(CurrentPromotions).ToList();
					}
				}));
			}
		}

		private List<Event> _events;
		public List<Event> Events {
			get {
				return _events;
			}

			set {
				_events = value;
				RaisePropertyChanged(nameof(Events));
			}
		}

		private RelayCommand _loadEventsCommand;

		public RelayCommand LoadEventsCommand {
			get {
				return _loadEventsCommand =
					new RelayCommand(() => Events = CompanyViewModel.SelectedBranch.Events);
			}
		}

		private Event _selectedEvent;
		public Event SelectedEvent {
			get { return _selectedEvent; }
			set {
				if ( value != _selectedEvent ) {
					_selectedEvent = value;
					RaisePropertyChanged(nameof(SelectedEvent));
				}
			}
		}

		public void AddEvent(Event newEvent) {
			Events.Add(newEvent);

			SaveBranchCommand.Execute("Event");
			_navigationService.NavigateTo(nameof(BranchEventsPage));
		}

		public void EditEvent(string title, string description, DateTime startDate, DateTime endDate) {
			SelectedEvent.Title = title;
			SelectedEvent.Description = description;
			SelectedEvent.StartDate = startDate;
			SelectedEvent.EndDate = endDate;
			SelectedEvent.Id = Guid.NewGuid().ToString();

			Events[Events.FindIndex(e => e.Id.Equals(SelectedEvent.Id))] = SelectedEvent;

			SaveBranchCommand.Execute("Event");
			RaisePropertyChanged(nameof(Events));
			_navigationService.NavigateTo(nameof(BranchEventsPage));
		}

		public void DeleteEvent() {
			Events.RemoveAt(Events.FindIndex(e => e.Id.Equals(SelectedEvent.Id)));
			SaveBranchCommand.Execute("Branch");
			RaisePropertyChanged(nameof(Events));
			_navigationService.NavigateTo(nameof(BranchEventsPage));
		}

		private RelayCommand _toAddEventCommand;

		public RelayCommand ToAddEventCommand {
			get {
				return _toAddEventCommand = new RelayCommand(() => _navigationService.NavigateTo(nameof(AddEventPage)));
			}
		}
	}
}
