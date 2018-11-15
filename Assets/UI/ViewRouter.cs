using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UI
{
	public class ViewRouter : MonoBehaviour
	{
		public MainView MainView;
		public LoadView LoadView;
		public SaveView SaveView;
		public SettingsView SettingsView;

		public void ShowMainView()
		{
			this.DisableAll();
			this.MainView.Activate();
		}

		public void DisableAll()
		{
			foreach (Transform view in this.transform)
			{
				view.gameObject.SetActive(false);
			}
		}
	}
}