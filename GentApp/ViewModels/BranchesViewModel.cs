using GentApp.DataModel;
using GentApp.Helpers;
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
	public class BranchesViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public ObservableCollection<Branch> Branches { get; set; }
		//public RelayCommand SaveBranchCommand { get; set; }

		private Branch mySelectedBranch;
		public Branch MySelectedBranch
		{
			get { return mySelectedBranch; }
			set
			{
				if (value != mySelectedBranch)
				{
					mySelectedBranch = value; NotifyPropertyChanged("MySelectedBranch");
				}
			}
		}

		public BranchesViewModel()
		{
			Branches = new ObservableCollection<Branch>(DummyDataSource.Branches);
			//SaveBranchCommand = new RelayCommand((p) => SaveBranch(p as Branch));
		}

		public void AddBranch(Branch newBranch)
		{
			this.Branches.Add(newBranch);
		}

		//private void SaveBranch(object p)
		//{
		//	//this.Branches.Add(new Branch()
		//	//{
		//	//	Name = p.ToString(),
		//	//	Address = "Dummy adres",
		//	//	OpeningHours = "24/7"
		//	//});
		//	this.Branches.Add(p as Branch);
		//}

		public async void EditBranch(string name, string address, string openingHours, BranchType type)
		{
			var oldBranch = MySelectedBranch;
			if (oldBranch != null)
			{
				oldBranch.Name = name;
				oldBranch.Address = address;
				oldBranch.OpeningHours = openingHours;
				oldBranch.Type = type;
			}

			//var branchJson = JsonConvert.SerializeObject(oldBranch);
			//HttpClient client = new HttpClient();
			//var res = await client.PutAsync("http://localhost:63187/api/branches/" + MainPage.BranchesViewModel.MySelectedBranch.Id, new StringContent(branchJson, System.Text.Encoding.UTF8, "application/json"));
		}

		public void DeleteBranch()
		{
			this.Branches.Remove(MySelectedBranch);
		}

		private void NotifyPropertyChanged(String propertyName)
		{
			if (null != PropertyChanged)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
