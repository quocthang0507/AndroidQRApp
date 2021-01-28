using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
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
		private ImageView imgResult;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_generator);

			tbxContent = FindViewById<EditText>(Resource.Id.tbxContent);
			btnEncode = FindViewById<Button>(Resource.Id.btnEncode);
			radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup);
			imgResult = FindViewById<ImageView>(Resource.Id.imgResource);

			btnEncode.Click += BtnEncode_Click;

			CreateRadioButton();
		}

		private void BtnEncode_Click(object sender, EventArgs e)
		{
			var intent = new Intent(this, typeof(ResultActivity));
			StartActivity(intent);
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