﻿using UnityEngine;

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
			foreach (Transform view in this.transform)
			{
				if (view.name != "Text")
				{
					view.gameObject.SetActive(false);
				}
			}
		}
	}
}