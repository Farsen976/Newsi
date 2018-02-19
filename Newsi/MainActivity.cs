using Android.App;
using Android.Widget;
using Android.OS;
using Newsi.Core;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newsi.UI;
using Android.Content;
using Android.Support.V7.App;

namespace Newsi
{
    [Activity(Theme = "@style/MyTheme.Splash")]
    public class MainActivity : AppCompatActivity
    {
        private List<Article> articles;
        private ArrayAdapter adapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            SetContentView(Resource.Layout.Main);
            //var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            //Toolbar will now take on default Action Bar characteristics
            //SetActionBar(toolbar);
            // Set our view from the "main" layout resource
            CallApi();
            
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

