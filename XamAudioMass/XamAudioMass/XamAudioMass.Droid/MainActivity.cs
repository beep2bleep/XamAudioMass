﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;

namespace XamAudioMass.Droid
{
    [Activity(Label = "XamAudioMass.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            AudioFeedRetriever feedRetriever = new AudioFeedRetriever();

            base.OnCreate(bundle);

            var scrollView = new ScrollView(this)
            {
                LayoutParameters =
                           new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)
            };

            this.SetContentView(scrollView);

            var mainLayout = new LinearLayout(this)
            {
                Orientation = Orientation.Vertical,
                LayoutParameters =
                           new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent)

            };

            // Create a LinearLayout element
            LinearLayout linearLayout = new LinearLayout(this);
            linearLayout.Orientation = Orientation.Vertical;

            foreach (var feed in feedRetriever.AudioFeeds)
            {
                // Get our button from the layout resource,
                // and attach an event to it
                //Button button = FindViewById<Button> (Resource.Id.myButton);

                //button.Click += delegate {
                //	button.Text = string.Format ("{0} clicks!", count++);
                //};
                

                Button audioFeedButton = new Button(this);
                audioFeedButton.Text = feed.Title;

                //var activity2 = new Intent(this, typeof(IntentUriType));
                //activity2.PutExtra("MyData", "Data from Activity1");

                Intent intent = new Intent();
                intent.SetAction(Android.Content.Intent.ActionView);
                intent.SetDataAndType(Android.Net.Uri.Parse(feed.Link), "audio/*");
                ///startActivity(intent);

                audioFeedButton.Click += delegate {
                    StartActivity(intent);
                };

                linearLayout.AddView(audioFeedButton);
            }
            scrollView.AddView(linearLayout);


        }
    }
}