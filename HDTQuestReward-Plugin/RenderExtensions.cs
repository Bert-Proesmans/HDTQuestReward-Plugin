using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace HDTQuestReward_Plugin
{
    static class RenderExtensions
    {
        // The width of the image we load.
        public const string PIXEL_WIDTH = "256";

        private const string BASE_URL = "https://art.hearthstonejson.com/";
        // v1/render/latest/enUS/256x/
        // Todo; change language
        private const string RENDER_PATH = "v1/render/latest/enUS/" + PIXEL_WIDTH + "x/";
        private const string RENDER_EXTENSION = ".png";

        public static Uri FullCardRenderURI(string cardID)
        {
            var fullCardPath = BASE_URL + RENDER_PATH + cardID + RENDER_EXTENSION;
            return new Uri(fullCardPath, UriKind.Absolute);
        }

    }

    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class IDToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("HDTQUESTREWARD: IDToImageConverter called");
            string cardID = value as string;

            if(cardID == null)
            {
                return null;
            }

            Uri uri = RenderExtensions.FullCardRenderURI(cardID);
            if(uri != null)
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                // Only store/load the image in memory cache upon request.
                //img.CacheOption = BitmapCacheOption.OnDemand;
                //img.CreateOptions = BitmapCreateOptions.DelayCreation;
                //// Only redownload if the timestamp of the cached resource (if any) is different from the resource online.
                //img.UriCachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.Revalidate);
                img.UriSource = uri;
                // Improves performance of image decoding when unexpected data passes in.
                //img.DecodePixelWidth = int.Parse(RenderExtensions.PIXEL_WIDTH);
                img.EndInit();

                return img;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
