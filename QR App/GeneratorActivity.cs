using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using QR.Service;
using System;
using ZXing.Client.Result;

namespace QR
{
	[Activity(Label = "QR Generator", Theme = "@style/AppTheme")]
	public class GeneratorActivity : Activity
	{
		private EditText tbxContent;
		private Button btnEncode;
		private RadioGroup radioGroup;
		private const string EXTRA_QR = "QR CODE RESULT";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_generator);

			InitControl();

			CreateRadioButton();
		}

		private void InitControl()
		{
			tbxContent = FindViewById<EditText>(Resource.Id.tbxContent);
			btnEncode = FindViewById<Button>(Resource.Id.btnEncode);
			radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup);
			
			btnEncode.Click += BtnEncode_Click;
		}

		private void BtnEncode_Click(object sender, EventArgs e)
		{
			try
			{
				QRScanningService service = new QRScanningService(this, false);
				Android.Graphics.Bitmap qr = service.Encode(tbxContent.Text);
				Intent intent = new Intent(this, typeof(ResultActivity));
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
				button = new RadioButton(this);
				button.SetText(typeName, TextView.BufferType.Normal);
				radioGroup.AddView(button);
			}
		}
	}
}