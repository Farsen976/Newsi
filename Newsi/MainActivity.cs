using Android.App;
using Android.OS;
using Newsi.Core;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newsi.UI;
using Android.Content;
using System;
using Android.Support.V7.App;
using Android.Widget;
using Android.Views;

namespace Newsi
{
    [Activity(Label = "Newsi", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private List<Article> articles;
        private ArrayAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Newsi";
            

            CallApi();
            
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.toolbar_menu, menu);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            string textToShow;

            if (item.ItemId == Resource.Id.menu_info)
                textToShow = "Soon available :)";
            else
                textToShow = "menu ! ";

            Toast.MakeText(this, item.TitleFormatted + ":" + textToShow,
                ToastLength.Long).Show();

            return base.OnOptionsItemSelected(item);
        }

        private async void CallApi()
        {
            NewsiAPI newsiAPI = new NewsiAPI();
            RootObject response = await newsiAPI.Task();

            if (response != null)
            {
                ListView list = FindViewById<ListView>(Resource.Id.listArticle);
                adapter = new ArticleAdapter(this, response.articles);
                articles = response.articles;
                list.Adapter = adapter;
                var view = Window.DecorView;
                view.RefreshDrawableState();
                list.ItemClick += List_ItemClick;
            }
        }

        private void List_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var webViewScreen = new Intent(this, typeof(WebViewActivity));
            Bundle b = new Bundle();
            Article article = new Article();
            article = articles[e.Position];
            b.PutString("url", article.url);

            webViewScreen.PutExtras(b);
            StartActivity(webViewScreen);
        }
    }
}

