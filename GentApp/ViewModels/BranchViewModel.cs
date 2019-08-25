using GentApp.DataModel;
using GentApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using GentApp.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GentApp.Views;
using System.Threading.Tasks;

namespace GentApp.ViewModels {
	public class BranchViewModel : ViewModelBase {
		private readonly INavigationService _navigationService;
		private readonly EventService _eventService;
		private readonly PromotionService _promotionService;

		public BranchViewModel(INavigationService navigationService) {
			_eventService = new EventService();
			_promotionService = new PromotionService();
			_navigationService = navigationService;
		}

		public CompanyViewModel CompanyViewModel {
			get {
				return SimpleIoc.Default.GetInstance<CompanyViewModel>();
			}
		}

		public async Task SaveBranch() {
			await CompanyViewModel.UpdateBranch(Events, Promotions);
			RaisePropertyChanged(nameof(Events));
			RaisePropertyChanged(nameof(Promotions));
		}

		private List<Promotion> _promotions = new List<Promotion>();
		public List<Promotion> Promotions {
			get {
				return _promotions ?? new List<Promotion>();
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

		public async void AddPromotion(Promotion promotion) {
			Promotions.Add(promotion);
			await SaveBranch();
			_navigationService.NavigateTo(nameof(BranchPromotionsPage));
		}

		public async void EditPromotion(string title, string description, DateTime startdate, DateTime enddate, bool usesCoupon) {
			MySelectedPromotion.Title = title;
			MySelectedPromotion.Description = description;
			MySelectedPromotion.StartDate = startdate;
			MySelectedPromotion.EndDate = enddate;
			MySelectedPromotion.UsesCoupon = usesCoupon;

			CompanyViewModel.SelectedBranch.Promotions[Promotions.FindIndex(p => p.Id.Equals(MySelectedPromotion.Id))] = MySelectedPromotion;

			await _promotionService.Update(MySelectedPromotion);
			RaisePropertyChanged(nameof(Promotions));
			_navigationService.NavigateTo(nameof(BranchPromotionsPage));
		}

		public async void DeletePromotion() {
			CompanyViewModel.SelectedBranch.Promotions.RemoveAt(Promotions.FindIndex(p => p.Id.Equals(MySelectedPromotion.Id)));
			await _promotionService.Delete(MySelectedPromotion);
			RaisePropertyChanged(nameof(Promotions));
			_navigationService.NavigateTo(nameof(BranchPromotionsPage));
		}

		private RelayCommand _loadPromotionsCommand;

		public RelayCommand LoadPromotionsCommand {
			get {
				return _loadPromotionsCommand ?? ( _loadPromotionsCommand = new RelayCommand(() => {
					CurrentPromotions = new List<Promotion>();
					NonCurrentPromotions = new List<Promotion>();
					Promotions = CompanyViewModel.SelectedBranch.Promotions;
					var currentDate = DateTime.Today.Date;
					if ( Promotions != null && Promotions.Count != 0 ) {
						CurrentPromotions = Promotions.Where(p => p.StartDate.Date <= currentDate.Date && p.EndDate.Date >= currentDate.Date).ToList();
						NonCurrentPromotions = Promotions.Except(CurrentPromotions).ToList();
					}
				}));
			}
		}

		private List<Event> _events = new List<Event>();
		public List<Event> Events {
			get {
				return _events ?? new List<Event>();
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

		public async void AddEvent(Event newEvent) {
			Events.Add(newEvent);
			await SaveBranch();
			_navigationService.NavigateTo(nameof(BranchEventsPage));
		}

		public async Task EditEvent(string title, string description, DateTime startDate, DateTime endDate) {
			SelectedEvent.Title = title;
			SelectedEvent.Description = description;
			SelectedEvent.StartDate = startDate;
			SelectedEvent.EndDate = endDate;
			SelectedEvent.Id = Guid.NewGuid().ToString();

			CompanyViewModel.SelectedBranch.Events[Events.FindIndex(e => e.Id.Equals(SelectedEvent.Id))] = SelectedEvent;

			await _eventService.Update(SelectedEvent);
			RaisePropertyChanged(nameof(Events));
			_navigationService.NavigateTo(nameof(BranchEventsPage));
		}

		public async Task DeleteEvent() {
			CompanyViewModel.SelectedBranch.Events.RemoveAt(Events.FindIndex(e => e.Id.Equals(SelectedEvent.Id)));
			await _eventService.Delete(SelectedEvent);
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
