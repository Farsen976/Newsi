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
using Newsi.Core;
using Square.Picasso;

namespace Newsi.UI
{
    class ArticleAdapter : ArrayAdapter<Article>
    {

        
        private int resourceLayout = Resource.Layout.Article;
        private List<Article> articles;
        private Context context;
        private TimeSpan publishAt;

        public ArticleAdapter(Context context, List<Article> objects): base (context, 0, objects)
        {
            this.context = context;
            this.articles = objects;
        }


        public new Article GetItem(int position)
        {
            return articles[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
           if(convertView == null)
            {
                convertView = LayoutInflater.FromContext(context).Inflate(resourceLayout, null, false);
            }

            Article currentItem = GetItem(position);

            TextView title = convertView.FindViewById<TextView>(Resource.Id.title);
            TextView publisher = convertView.FindViewById<TextView>(Resource.Id.publisher);
            TextView at = convertView.FindViewById<TextView>(Resource.Id.at);
            ImageView image = convertView.FindViewById<ImageView>(Resource.Id.image);

            title.Text = currentItem.title;
            publisher.Text = currentItem.source.name;
            DateTime currentTime = DateTime.Now;
            publishAt = currentTime.Subtract(currentItem.publishedAt);

            at.Text = renderTime(publishAt);
            
           
            if(currentItem.urlToImage != null)
            {
                Picasso.With(context).Load(currentItem.urlToImage?.Replace("www.", "")).Into(image);
            }
            else
            {
                image.SetImageResource(0);
            }
            //fill in your items
            //holder.Title.Text = "new text here";

            return convertView;
        }

        private string renderTime(TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays > 1)
            {
                 return publishAt.Days + " d";
            }
            if (publishAt.TotalHours > 1)
            {
                return  publishAt.Hours + " h";
            }
            if (publishAt.TotalMinutes > 1)
            {
                return publishAt.Minutes + " m";
            }
            return null;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return articles.Count;
            }
        }

    }
}