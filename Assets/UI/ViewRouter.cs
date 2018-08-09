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

		// TODO: Rework to proper view
		public HealthController GameView;

		public void ShowGameView()
		{
			this.DisableAll();
			this.GameView.Activate();
		}

		public void ShowMainView()
		{
			this.DisableAll();
			this.MainView.Activate();
		}

		private void DisableAll()
		{
			foreach (Transform view in this.transform)
			{
				view.gameObject.SetActive(false);
			}
		}
	}
}