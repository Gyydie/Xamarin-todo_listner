using App1.Models;
using App1.Themes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        public IList<Settings.Themes> Themes { get; private set; }

        object savedTheme = "";

        private Settings.Themes selectedThemes;
        public Settings.Themes SelectedTheme
        {
            get
            {
                if (App.Current.Properties.TryGetValue("theme", out savedTheme))
                {
                    selectedThemes = (Settings.Themes)savedTheme;
                }

                return selectedThemes;
            }
            set
            {
                if (App.Current.Properties.TryGetValue("theme", out savedTheme))
                {
                    App.Current.Properties["theme"] = (int)value;
                }
                else
                {
                    App.Current.Properties.Add("theme", (int)value);
                }

                selectedThemes = value;
            }
        }

        public SettingsViewModel()
        {
            Themes = new List<Settings.Themes>();
            Themes.Add(Settings.Themes.Light);
            Themes.Add(Settings.Themes.Dark);
        }

        public void LoadSettings()
        {
            SelectTheme(SelectedTheme);
        }

        public void SelectTheme(Settings.Themes theme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (theme)
                {
                    case Settings.Themes.Dark:
                        mergedDictionaries.Add(new DarkTheme());
                        break;
                    case Settings.Themes.Light:
                    default:
                        mergedDictionaries.Add(new LightTheme());
                        break;
                }
            }
        }
    }
}
