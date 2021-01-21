﻿using Android.App;
using Android.OS;

namespace QR
{
    [Activity(Label = "QR Generator", Theme = "@style/AppTheme")]
    public class GeneratorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_generator);
        }
    }
}