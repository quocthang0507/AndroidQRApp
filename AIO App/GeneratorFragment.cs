using AIOApp.QRService;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using ZXing.Client.Result;
using Fragment = Android.Support.V4.App.Fragment;

namespace AIOApp
{
	public class GeneratorFragment : Fragment
	{
		private View view;
		private EditText tbxContent;
		private Button btnEncode;
		private RadioGroup radioGroup;
		private const string EXTRA_QR = "QR CODE RESULT";

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		private void InitControl()
		{
			tbxContent = view.FindViewById<EditText>(Resource.Id.tbxContent);
			btnEncode = view.FindViewById<Button>(Resource.Id.btnEncode);
			radioGroup = view.FindViewById<RadioGroup>(Resource.Id.radioGroup);

			btnEncode.Click += BtnEncode_Click;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			view = inflater.Inflate(Resource.Layout.activity_scanner, container, false);
			return view;

			// return base.OnCreateView(inflater, container, savedInstanceState);
		}

		public override void OnActivityCreated(Bundle savedInstanceState)
		{
			base.OnActivityCreated(savedInstanceState);
			InitControl();
			CreateRadioButton();
		}

		private void BtnEncode_Click(object sender, EventArgs e)
		{
			try
			{
				QRScanningService service = new QRScanningService(view.Context, false);
				Bitmap qr = service.Encode(tbxContent.Text);
				Intent intent = new Intent(view.Context, typeof(ResultActivity));
				intent.PutExtra(EXTRA_QR, qr);
				StartActivity(intent);
			}
			catch (Exception)
			{

			}
		}

		private void CreateRadioButton()
		{
			RadioButton button;
			foreach (string typeName in Enum.GetNames(typeof(ParsedResultType)))
			{
				button = new RadioButton(view.Context);
				button.SetText(typeName, TextView.BufferType.Normal);
				radioGroup.AddView(button);
			}
		}

	}
}