using System;
using System.Collections.Generic;
using PixelPirateCodes.Model.Data.Properties;
using UnityEngine;

namespace PixelPirateCodes.Model.Definitions.Localization
{
    public class LocalizationManager
    {
        public static readonly LocalizationManager I;

        private readonly StringPersistentProperty _localeKey = new StringPersistentProperty("en","Localization/current");
        private Dictionary<string, string> _localization;

        public event Action OnLocaleChanged;
        public string LocaleKey => _localeKey.Value;
        
        static LocalizationManager()
        {
            I = new LocalizationManager();
        }

        private LocalizationManager()
        {
            LoadLocale(_localeKey.Value);
        }

        private void LoadLocale(string localeToLoad)
        {
            var def = Resources.Load<LocaleDef>($"Locales/{localeToLoad}");
            _localization = def.GetData();
            _localeKey.Value = localeToLoad;
            OnLocaleChanged?.Invoke();
        }

        public string Localize(string key)
        {
            return _localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
        }

        public void SetLocale(string localeKey)
        {
            LoadLocale(localeKey);
        }
    }
}