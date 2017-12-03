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

namespace Iara
{
    public static class Config
    {
        public static SQLiteModels.User loggedUser {get;set;}

        public static AlarmManager alarm { get; set; }
    }
}