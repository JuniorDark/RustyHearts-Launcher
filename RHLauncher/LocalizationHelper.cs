using System.Globalization;
using System.Resources;

namespace RHLauncher.RHLauncher
{
    public class LocalizationHelper
    {
        public static void LoadLocalizedStrings(
            string lang,
            List<Button> buttons,
            List<ImageList> imageLists,
            Dictionary<string, List<ImageList>> languageImageLists)
        {
            CultureInfo cultureInfo;
            if (languageImageLists.ContainsKey(lang))
            {
                // If the language is supported, get the corresponding image lists
                List<ImageList> langSpecificImageLists = languageImageLists[lang];
                for (int i = 0; i < buttons.Count && i < langSpecificImageLists.Count; i++)
                {
                    buttons[i].ImageList = langSpecificImageLists[i];
                }

                cultureInfo = new CultureInfo(lang);
            }
            else
            {
                // Default to English if the language is not supported
                cultureInfo = new CultureInfo("en-US"); // English culture
                for (int i = 0; i < buttons.Count && i < imageLists.Count; i++)
                {
                    buttons[i].ImageList = imageLists[i];
                }
            }

            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            // Load the appropriate resource file based on the selected language
            _ = new
            ResourceManager(typeof(LocalizedStrings));
            LocalizedStrings.Culture = cultureInfo;
        }
    }
}
