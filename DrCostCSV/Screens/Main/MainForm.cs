using DrCostCSV.Screens.Main;
using DrCostCSV.Screens.Main.ViewModels;

namespace DrCostCSV
{
	public partial class MainForm : Form, IMainView
	{
		public MainForm()
		{
			InitializeComponent();
		}

		public event EventHandler? DateChanged;

		public void BindDocs(IEnumerable<CategoryExpenseRowViewModel> docs)
		{
			throw new NotImplementedException();
		}
	}
}
