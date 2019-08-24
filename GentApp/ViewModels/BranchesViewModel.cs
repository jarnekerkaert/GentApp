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
		private bool isNavigated;

		public ObservableCollection<Branch> Branches { get; set; }

		private List<Branch> branchesFilteredOnName;

		public List<Branch> BranchesFilteredOnName
		{
			get {
				if (branchesFilteredOnName == null)
				{
					branchesFilteredOnName = new List<Branch>();
				}
				return branchesFilteredOnName;
			}
			set
			{
				branchesFilteredOnName = value;
				FilteredBranches.Clear();
				FilteredBranches = BranchesFilteredOnType.Where(x => branchesFilteredOnName.Contains(x)).ToList();
				FilteredBranches = FilteredBranches.Distinct().ToList();
				RaisePropertyChanged(nameof(BranchesFilteredOnName));
				RaisePropertyChanged(nameof(FilteredBranches));
			}
		}
		private List<Branch> branchesFilteredOnType;

		public List<Branch> BranchesFilteredOnType
		{
			get {
				if (branchesFilteredOnType == null)
				{
					branchesFilteredOnType = new List<Branch>();
				}
				return branchesFilteredOnType;
			}
			set
			{
				branchesFilteredOnType = value;
				FilteredBranches.Clear();
				FilteredBranches = BranchesFilteredOnName.Where(x => branchesFilteredOnType.Contains(x)).ToList();
				FilteredBranches = FilteredBranches.Distinct().ToList();
				RaisePropertyChanged(nameof(BranchesFilteredOnType));
				RaisePropertyChanged(nameof(FilteredBranches));
			}
		}
		public List<Branch> FilteredBranches { get; set; }

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
					if ( isNavigated && SelectedBranch == null ) {
						isNavigated = false;
					}
					else {
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
					Branches = new ObservableCollection<Branch>(await _branchService.GetAll());
					FilteredBranches = Branches.ToList();
					BranchesFilteredOnType = Branches.ToList();
					branchesFilteredOnName = Branches.ToList();
					isNavigated = true;
					RaisePropertyChanged(nameof(Branches));
					RaisePropertyChanged(nameof(FilteredBranches));
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

		public async void ClearSubscriptionAmounts(Subscription subscription)
		{
			subscription.AmountEvents = 0;
			subscription.AmountPromotions = 0;
			// subscription.Branch = null;
			await _subscriptionService.Update(subscription);
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

		//private RelayCommand _loadFullSubscriptionsCommand;

		//public RelayCommand LoadFullSubscriptionsCommand
		//{
		//	get
		//	{
		//		return _loadFullSubscriptionsCommand = new RelayCommand(
		//				async () => {
		//					SubscribedBranches = new ObservableCollection<Branch>(
		//						await _userService.GetSubscribedBranches(UserViewModel.CurrentUser.Id));
		//					FullSubscriptions = new ObservableCollection<Subscription>(
		//						await _subscriptionService.GetSubscriptions(UserViewModel.CurrentUser.Id));
		//				});
		//	}
		//}

		//private ObservableCollection<Subscription> _fullSubscriptions;
		//public ObservableCollection<Subscription> FullSubscriptions
		//{
		//	get
		//	{
		//		return _fullSubscriptions;
		//	}

		//	set
		//	{
		//		_fullSubscriptions = value;
		//		RaisePropertyChanged(nameof(FullSubscriptions));
		//	}
		//}
	}
}
