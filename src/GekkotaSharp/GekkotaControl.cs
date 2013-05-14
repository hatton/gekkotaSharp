using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using Gecko;

namespace Gekkota
{

	public partial class GekkotaControl : UserControl
	{
		private GeckoWebBrowser _browser;

		public GekkotaControl()
		{
			InitializeComponent();
			if (ReallyDesignMode)
				return;
			
		}
		private bool ReallyDesignMode
		{
			get
			{
				return (base.DesignMode || GetService(typeof(IDesignerHost)) != null) ||
					(LicenseManager.UsageMode == LicenseUsageMode.Designtime);
			}
		}

		public string HtmlPath;

		private void GeckoRSharpControl_Load(object sender, EventArgs e)
		{
			if (ReallyDesignMode)
				return;
			EmbeddedRestServer.StartupIfNeeded();

			GeckoFxInitializer.SetUpXulRunner();
			_browser = new Gecko.GeckoWebBrowser();
			_browser.Dock = DockStyle.Fill;
			Controls.Add(_browser);

			if (!string.IsNullOrEmpty(HtmlPath))
			{
				_browser.Navigate("http://localhost:5432/" + HtmlPath);
			}

		}
	}
}
