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
		public HUDController GameHUDView;
		public MainView MainView;
		public LoadView LoadView;
		public SaveView SaveView;
		public SettingsView SettingsView;

		public void ShowGameHUDView()
		{
			this.DisableAll();
			this.GameHUDView.Activate();
		}

		public void ShowMainView()
		{
			this.DisableAll();
			this.MainView.Activate();
		}

		public void DisableAll()
		{
			this.GameHUDView.gameObject.SetActive(false);

			foreach (Transform view in this.transform)
			{
				view.gameObject.SetActive(false);
			}
		}
	}
}