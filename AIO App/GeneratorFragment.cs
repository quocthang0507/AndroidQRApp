﻿using AIOApp.QRService;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using ZXing.Client.Result;
using Fragment = Android.Support.V4.App.Fragment;

namespace AIOApp
{
	public class GeneratorFragment : Fragment
	{
		private View view;
		private EditText tbxContent;
		private Button btnEncode;
		private ImageView imgQR;
		private Bitmap bitmap;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		private void InitControl()
		{
			tbxContent = view.FindViewById<EditText>(Resource.Id.tbxContent);
			btnEncode = view.FindViewById<Button>(Resource.Id.btnEncode);

			btnEncode.Click += BtnEncode_Click;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			view = inflater.Inflate(Resource.Layout.activity_generator, container, false);
			return view;

			// return base.OnCreateView(inflater, container, savedInstanceState);
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			InitControl();
		}

		private void BtnEncode_Click(object sender, EventArgs e)
		{
			try
			{
				QRScanningService service = new QRScanningService(view.Context, false);
				bitmap = service.Encode(tbxContent.Text);
				ShowAlert(view.Context, "Lưu mã QR", bitmap);
			}
			catch (Exception)
			{

			}
		}

		private void SaveImage(Bitmap bitmap, string filename)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(filename, FileMode.Create);
				bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, fs);
			}
			catch (Exception e)
			{
				Toast.MakeText(view.Context, e.Message, ToastLength.Short).Show();
				return;
			}
			finally
			{
				if (fs != null)
				{
					fs.Flush();
					fs.Close();
				}
			}
			Toast.MakeText(view.Context, $"Save successfully to your phone at {filename}", ToastLength.Long).Show();
		}

		private void ShowAlert(Context context, string title, Bitmap bitmap)
		{
			AlertDialog dialog = null;
			LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
			View dialoglayout = inflater.Inflate(Resource.Layout.activity_qr_result, null);

			imgQR = dialoglayout.FindViewById<ImageView>(Resource.Id.imgResult);
			imgQR.SetImageBitmap(bitmap);

			dialog = new AlertDialog.Builder(context)
			.SetTitle(title)
			.SetPositiveButton("Save", (sender, e) =>
			{
				Java.IO.File storagePath = context.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures);
				string datetime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
				string path = System.IO.Path.Combine(storagePath.ToString(), $"QR_{datetime}.jpg");
				SaveImage(bitmap, path);
			})
			.SetNegativeButton("Cancel", (sender, e) =>
			{
				dialog.Cancel();
			})
			.SetView(dialoglayout)
			.Create();
			dialog.Show();
		}
	}
}