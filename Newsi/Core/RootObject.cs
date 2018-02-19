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

namespace Newsi.Core
{
    class RootObject
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<Article> articles { get; set; }
    }
}