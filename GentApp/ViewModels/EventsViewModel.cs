using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using GentApp.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GentApp.ViewModels {
	public class EventsViewModel : ViewModelBase {
		private readonly INavigationService _navigationService;
		private readonly EventService _eventService;
		private readonly UserService _userService;
		private bool isNavigated;

		public EventsViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
			_eventService = new EventService();
			_userService = new UserService();
			SubscribedEvents = new ObservableCollection<Event>();
		}
		public UserViewModel UserViewModel {
			get {
				return SimpleIoc.Default.GetInstance<UserViewModel>();
			}
		}

		private ObservableCollection<Event> _events;
		public ObservableCollection<Event> Events {
			get {
				return _events;
			}

			set {
				_events = value;
				RaisePropertyChanged(nameof(Events));
			}
		}

		private Event _selectedEvent;

		public Event SelectedEvent {
			get { return _selectedEvent; }
			set {
				_selectedEvent = value;
				RaisePropertyChanged(nameof(SelectedEvent));
			}
		}

		private RelayCommand _eventSelectedCommand;

		public RelayCommand EventSelectedCommand {
			get {
				return _eventSelectedCommand = new RelayCommand(() => {
				if ( isNavigated && SelectedEvent == null ) {
					isNavigated = false;
				}
				else {
					isNavigated = true;
					_navigationService.NavigateTo(nameof(EventDetailsPage));
					}
				});
			}
		}

		private ObservableCollection<Event> _subscribedEvents;
		public ObservableCollection<Event> SubscribedEvents {
			get {
				return _subscribedEvents;
			}

			set {
				_subscribedEvents = value;
				RaisePropertyChanged(nameof(SubscribedEvents));
			}
		}

		private RelayCommand _loadEventsCommand;

		public RelayCommand LoadEventsCommand {
			get {
				return _loadEventsCommand = new RelayCommand(async () => {
					Events = new ObservableCollection<Event>(await _eventService.GetAll());
					isNavigated = true;
					RaisePropertyChanged(nameof(Events));
				});
			}
		}

		private RelayCommand _loadUpcomingEventsCommand;

		public RelayCommand LoadUpcomingEventsCommand {
			get {
				return _loadUpcomingEventsCommand = new RelayCommand(async () => {
					if ( UserViewModel.LoggedIn ) {
						var subbedBranches = await _userService.GetSubscribedBranches(UserViewModel.CurrentUser.Id);
						List<Event> events = new List<Event>();
						foreach (Branch branch in subbedBranches ) {
							if( branch.Events.Count != 0)
								events.AddRange(branch.Events);
						}
						SubscribedEvents = new ObservableCollection<Event>(events);
					}
					else {
						SubscribedEvents = new ObservableCollection<Event>();
					}

					RaisePropertyChanged(nameof(Events));
				});
			}
		}
	}
}
