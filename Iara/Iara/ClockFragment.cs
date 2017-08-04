using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Iara
{
    public class ClockFragment : DialogFragment,
                                    TimePickerDialog.IOnTimeSetListener
    {
        // TAG para elecionar qualquer palavra
        public static readonly string TAG = "X:" + typeof(ClockFragment).Name.ToUpper();

        // inicializa para previnir nulos
        Action<TimeSpan> _timeSelectedHandler = delegate { };

        public static ClockFragment NewInstance(Action<TimeSpan> onTimeSelected)
        {
            ClockFragment frag = new ClockFragment();
            frag._timeSelectedHandler = onTimeSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            TimeSpan currently = DateTime.Today.TimeOfDay;
            TimePickerDialog dialog = new TimePickerDialog(Activity,
                                                           this,
                                                           currently.Hours,
                                                           currently.Minutes,
                                                           true);
            return dialog;
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            TimeSpan selectedTime = new TimeSpan(hourOfDay, minute, 0);
            Log.Debug(TAG, selectedTime.ToString());
            _timeSelectedHandler(selectedTime);
        }
    }
}