using Assets.Code.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public class ViewRouter : MonoBehaviour
    {
        public HUDController GameHUDView;
        public MainView MainView;
        public LoadView LoadView;
        public SaveView SaveView;
        public SettingsView SettingsView;

        public UnityDependency<MainMenuBackgroundImage> BackgroundImage;

        public void ShowGameHUDView()
        {
            this.DisableAll();
            this.EnableTips();
            this.GameHUDView.Activate();
            this.BackgroundImage.Value.Deactivate();
        }

        public void ShowMainView()
        {
            this.DisableAll();
            this.MainView.Activate();
            this.BackgroundImage.Value.Activate();
        }

        private void DisableAll()
        {
            foreach (Transform view in this.transform)
            {
                view.gameObject.SetActive(false);
            }
        }

        private void EnableTips()
        {
            foreach (Transform view in this.transform)
            {
                if (view.name == "Text")
                {
                    view.gameObject.SetActive(true);
                }
            }
        }
    }
}