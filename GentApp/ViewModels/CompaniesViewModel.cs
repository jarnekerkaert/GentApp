using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GentApp.DataModel;
using GentApp.Helpers;
using GentApp.Services;
using MetroLog;

namespace GentApp.ViewModels {
	public class CompaniesViewModel : ViewModelBase {
		private readonly ILogger log = LogManagerFactory.DefaultLogManager.GetLogger<CompaniesViewModel>();
		private readonly CompanyService companyService = new CompanyService();
		private readonly BranchService branchService = new BranchService();
		private readonly SubscriptionService subscriptionService = new SubscriptionService();
		private readonly INavigationService _navigationService;

		public CompaniesViewModel(INavigationService navigationService) {
			_navigationService = navigationService;
			companyService = new CompanyService();
		}

		public UserViewModel UserViewModel
		{
			get
			{
				return SimpleIoc.Default.GetInstance<UserViewModel>();
			}
		}

		private ObservableCollection<Company> _companies;
		public ObservableCollection<Company> Companies {
			get {
				return _companies;
			}

			set {
				_companies = value;
				RaisePropertyChanged(nameof(Companies));
			}
		}

		public Company SelectedCompany { get; set; }

		private Branch selectedBranch;

		public Branch SelectedBranch {
			get { return selectedBranch; }
			set {
				if ( value != selectedBranch ) {
					selectedBranch = value;
					RaisePropertyChanged(nameof(SelectedBranch));
				}
			}
		}
		
		private RelayCommand _companySelectedCommand;

		public RelayCommand CompanySelectedCommand {
			get {
				return _companySelectedCommand = new RelayCommand(() => _navigationService.NavigateTo("CompanyDetailsPage"));
			}
		}
		private RelayCommand _branchSelectedCommand;

		public RelayCommand BranchSelectedCommand {
			get {
				return _branchSelectedCommand = new RelayCommand(() => _navigationService.NavigateTo("BranchDetailsPage"));
			}
		}
		
		private RelayCommand _loadCommand;

		public RelayCommand LoadCommand {
			get {
				return _loadCommand ?? ( _loadCommand = new RelayCommand(async () => {
					Companies = new ObservableCollection<Company>(await companyService.GetAll());
					//MyCompany = Companies[0];
				}
				) );
			}
		}
		
		public async void RefreshCompanies() {
			Companies = new ObservableCollection<Company>(await companyService.GetAll());
			RaisePropertyChanged(nameof(Companies));
		}

		private RelayCommand _subscribeCommand;

		public RelayCommand SubscribeCommand
		{
			get
			{
				return _subscribeCommand ?? (_subscribeCommand = new RelayCommand(async () => {
					if (SubscribedTo)
					{
						Subscription subscription = Subscriptions.Where(s => s.BranchId.Equals(SelectedBranch.Id)).FirstOrDefault();
						Subscriptions.Remove(subscription);
						RaisePropertyChanged(nameof(Subscriptions));
						RaisePropertyChanged(nameof(SubscribedTo));
						await subscriptionService.Unsubscribe(subscription.Id);
					}
					else
					{
						Subscription subscription = new Subscription() { BranchId = SelectedBranch.Id, UserId = UserViewModel.CurrentUser.Id };
						Subscriptions.Add(subscription);
						RaisePropertyChanged(nameof(Subscriptions));
						RaisePropertyChanged(nameof(SubscribedTo));
						await subscriptionService.Subscribe(subscription);
					}
				}
				));
			}
		}

		private ObservableCollection<Subscription> _subscriptions;
		public ObservableCollection<Subscription> Subscriptions
		{
			get
			{
				return _subscriptions;
			}

			set
			{
				_subscriptions = value;
				RaisePropertyChanged(nameof(Subscriptions));
			}
		}

		private RelayCommand _loadSubscriptionsCommand;

		public RelayCommand LoadSubscriptionsCommand
		{
			get
			{
				return _loadSubscriptionsCommand ?? (_loadSubscriptionsCommand = new RelayCommand(async () => {
					Subscriptions = new ObservableCollection<Subscription>(await subscriptionService.GetSubscriptions(UserViewModel.CurrentUser.Id));
				}
				));
			}
		}

		public bool SubscribedTo
		{
			get
			{
				if(SelectedBranch == null)
				{
					return false;
				}
				Subscription subscription = Subscriptions.Where(s => s.BranchId.Equals(SelectedBranch.Id)).DefaultIfEmpty(null).First();
				return subscription != null;
				//return Subscriptions.Where(s => s.BranchId.Equals(SelectedBranch.Id)).Any();
			}
		}

		// for your subscriptions page
		private ObservableCollection<Branch> _branches;
		public ObservableCollection<Branch> Branches
		{
			get
			{
				return _branches;
			}

			set
			{
				_branches = value;
				RaisePropertyChanged(nameof(Branches));
			}
		}

		private RelayCommand _loadBranchesCommand;

		public RelayCommand LoadBranchesCommand
		{
			get
			{
				return _loadBranchesCommand ?? (_loadBranchesCommand = new RelayCommand(async () => {
					Branches = new ObservableCollection<Branch>(await branchService.GetBranches());
				}
				));
			}
		}
	}
}
