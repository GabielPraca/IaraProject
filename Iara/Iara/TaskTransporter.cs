using System;
using System.Collections.Generic;
using Android.Content;
using System.Linq;

namespace Iara
{
    public static class TaskTransporter
    {
        public static bool isActive;
        public static Context context;
        public static List<string> itens;

        //public static bool needRefresh = false;

        internal static void ResetTaskTransporter()
        {
            isActive = false;
            context = null;
            itens.Clear();
        }

        internal static void CallTaskTransporter(Context eContext, Intent intent)
        {
            isActive = true;
            context = eContext;

            try
            {
                itens = intent.GetStringArrayListExtra("task").ToList();
            }
            catch (Exception)
            {
                //do nothing haha kludge
            }
        }
    }
}