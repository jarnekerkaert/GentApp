using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using GentApp.Views;
using MetroLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GentApp.ViewModels {
	public class BranchesViewModel : ViewModelBase {
		private ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<BranchesViewModel>();
		private readonly INavigationService _navigationService;
		private readonly BranchService _branchService;
		private readonly SubscriptionService _subscriptionService;
		private readonly UserService _userService;
		private bool isNavigated = false;

		public ObservableCollection<Branch> Branches { get; set; }

		private Branch mySelectedBranch;
		public Branch MySelectedBranch {
			get { return mySelectedBranch; }
			set {
				if ( value != mySelectedBranch ) {
					mySelectedBranch = value;
					RaisePropertyChanged(nameof(MySelectedBranch));
				}
			}
		}

		public BranchesViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
			_branchService = new BranchService();
			_subscriptionService = new SubscriptionService();
			_userService = new UserService();
		}

		public UserViewModel UserViewModel {
			get {
				return SimpleIoc.Default.GetInstance<UserViewModel>();
			}
		}

		public async void RetrieveBranchesOfCompany(string id) {
			Branches = new ObservableCollection<Branch>(await _branchService.GetBranchesOfCompany(id));
		}

		public void AddBranch(Branch newBranch) {
			Branches.Add(newBranch);
		}

		public void EditBranch(string name, string address, string openingHours, BranchType type) {
			var oldBranch = MySelectedBranch;
			if ( oldBranch != null ) {
				oldBranch.Name = name;
				oldBranch.Address = address;
				oldBranch.OpeningHours = openingHours;
				oldBranch.Type = type;
			}
		}

		private Branch selectedBranch;

		public Branch SelectedBranch {
			get { return selectedBranch; }
			set {
				selectedBranch = value;
				RaisePropertyChanged(nameof(SelectedBranch));
			}
		}

		private RelayCommand _branchSelectedCommand;

		public RelayCommand BranchSelectedCommand {
			get {
				return _branchSelectedCommand = new RelayCommand(() => {
					if (isNavigated && SelectedBranch == null)
					{
						isNavigated = false;
					}
					else
					{
						isNavigated = true;
						_navigationService.NavigateTo(nameof(BranchDetailsPage));
					}
				});
			}
		}

		public void DeleteBranch() {
			Branches.Remove(MySelectedBranch);
		}

		private RelayCommand _loadBranchesCommand;

		public RelayCommand LoadBranchesCommand {
			get {
				return _loadBranchesCommand = new RelayCommand(async () => {
					Branches = new ObservableCollection<Branch>(await _branchService.GetBranches());
					RaisePropertyChanged(nameof(Branches));
				});
			}
		}

		private RelayCommand _subscribeCommand;

		public RelayCommand SubscribeCommand {
			get {
				return _subscribeCommand = new RelayCommand(async () => {
					if ( SubscribedTo ) {
						Subscription subscription = Subscriptions.FirstOrDefault(s => s.BranchId.Equals(SelectedBranch.Id));
						Subscriptions.Remove(subscription);
						RaisePropertyChanged(nameof(Subscriptions));
						RaisePropertyChanged(nameof(SubscribedTo));
						await _subscriptionService.Unsubscribe(subscription.Id);
					}
					else {
						Subscription subscription = new Subscription() { BranchId = SelectedBranch.Id, UserId = UserViewModel.CurrentUser.Id };
						Subscriptions.Add(subscription);
						RaisePropertyChanged(nameof(Subscriptions));
						RaisePropertyChanged(nameof(SubscribedTo));
						await _subscriptionService.Subscribe(subscription);
					}
				});
			}
		}

		private ObservableCollection<Subscription> _subscriptions;
		public ObservableCollection<Subscription> Subscriptions {
			get {
				return _subscriptions;
			}

			set {
				_subscriptions = value;
				RaisePropertyChanged(nameof(Subscriptions));
			}
		}

		private RelayCommand _loadSubscriptionsCommand;

		public RelayCommand LoadSubscriptionsCommand {
			get {
				return _loadSubscriptionsCommand = new RelayCommand(
						async () => {
							Subscriptions = new ObservableCollection<Subscription>(
								await _subscriptionService.GetSubscriptions(UserViewModel.CurrentUser.Id));
							SubscribedBranches = new ObservableCollection<Branch>(
								await _userService.GetSubscribedBranches(UserViewModel.CurrentUser.Id));
						});
			}
		}

		private RelayCommand _loadFullSubscriptionsCommand;

		public RelayCommand LoadFullSubscriptionsCommand
		{
			get
			{
				return _loadFullSubscriptionsCommand = new RelayCommand(
						async () => {
							Subscriptions = new ObservableCollection<Subscription>(
								await _subscriptionService.GetSubscriptions(UserViewModel.CurrentUser.Id));
							SubscribedBranches = new ObservableCollection<Branch>(
								await _userService.GetSubscribedBranches(UserViewModel.CurrentUser.Id));
							FullSubscribedBranches = new List<SubscribedBranch>();
							foreach (Branch branch in SubscribedBranches)
							{
								var formattedBranch = new SubscribedBranch(branch);
								foreach (Subscription subscription in Subscriptions)
								{
									if (subscription.BranchId.Equals(branch.Id))
									{
										formattedBranch.AmountEvents = subscription.AmountEvents;
										formattedBranch.AmountPromotions = subscription.AmountPromotions;
										FullSubscribedBranches.Add(formattedBranch);
									}
								}
							}
							// of bij getsubscriptions include branch en dan heb je maar 1 foreach nodig
							// maar vergeet dan niet al de oude dingen aan te passen => branch.Id ipv BranchId
						});
			}
		}

		public bool SubscribedTo {
			get {
				if ( SelectedBranch == null ) {
					return false;
				}
				Subscription subscription = Subscriptions.Where(s => s.BranchId.Equals(SelectedBranch.Id)).DefaultIfEmpty(null).First();
				return subscription != null;
			}
		}

		private ObservableCollection<Branch> _subscribedBranches;
		public ObservableCollection<Branch> SubscribedBranches {
			get {
				return _subscribedBranches;
			}

			set {
				_subscribedBranches = value;
				RaisePropertyChanged(nameof(SubscribedBranches));
			}
		}

		private List<SubscribedBranch> _fullSubscribedBranches;
		public List<SubscribedBranch> FullSubscribedBranches
		{
			get
			{
				return _fullSubscribedBranches;
			}

			set
			{
				_fullSubscribedBranches = value;
				RaisePropertyChanged(nameof(FullSubscribedBranches));
			}
		}

		private RelayCommand _loadPromotionsCommand;

		public RelayCommand LoadPromotionsCommand
		{
			get
			{
				return _loadPromotionsCommand ?? (_loadPromotionsCommand = new RelayCommand(async () => {
					Promotions = await _branchService.GetPromotions(SelectedBranch.Id);
					isNavigated = true;
					var currentDate = DateTime.Today.Date;
					CurrentPromotions = Promotions.Where(p => p.StartDate <= currentDate && p.EndDate >= currentDate).ToList();
				}
				));
			}
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
	}
}
