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
using Android.Webkit;
using Newsi.Core;

namespace Newsi.UI
{
    [Activity]
    public class WebViewActivity : Activity
    {
        private WebView web_view;
        private string articleUrl;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.WebView);

            web_view = FindViewById<WebView>(Resource.Id.webview);
            web_view.Settings.JavaScriptEnabled = true;
            web_view.SetWebViewClient(new WebViewClient());
            Bundle b = Intent.Extras;
            if(b != null)
            {
                articleUrl = b.GetString("url");
                web_view.LoadUrl(articleUrl);
            }
            
        }
        public class WebViewClient : Android.Webkit.WebViewClient
        {

            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return false;
            }

            
        }
    }

    
}