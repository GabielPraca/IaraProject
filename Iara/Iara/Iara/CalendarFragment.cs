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
    public class CalendarFragment : DialogFragment,
                                    DatePickerDialog.IOnDateSetListener
    {
        // TAG para elecionar qualquer palavra
        public static readonly string TAG = "X:" + typeof(CalendarFragment).Name.ToUpper();

        // inicializa para previnir nulos
        Action<DateTime> _dateSelectedHandler = delegate { };

        public static CalendarFragment NewInstance(Action<DateTime> onDateSelected)
        {
            CalendarFragment frag = new CalendarFragment();
            frag._dateSelectedHandler = onDateSelected;
            return frag;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                           this,
                                                           currently.Year,
                                                           currently.Month,
                                                           currently.Day);
            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
            Log.Debug(TAG, selectedDate.ToLongDateString());
            _dateSelectedHandler(selectedDate);
        }
    }
}