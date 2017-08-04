using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading;
using Android.Media;

namespace Iara
{
    public static class AlarmRingtone
    {
        static private MediaPlayer song = null;

        public static void PlayRingtone(Context context)
        {
            if (context != null && song == null)
            {
                song = MediaPlayer.Create(context, Resource.Raw.song);
                song.Looping = true;
                song.Start();
            }
        }
        public static void StopRingtone(Context context)
        {
            if (context != null && song != null)
            {
                song.Stop();
                song = null;
            }
        }
    }
}