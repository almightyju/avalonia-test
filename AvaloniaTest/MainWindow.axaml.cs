using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace AvaloniaTest;
public partial class MainWindow : Window
{
	private AvaloniaList<ListItem> _dgItems;
	public MainWindow()
	{
		InitializeComponent();

		TextBox1.KeyUp += TextBox1_KeyUp;
		TextBox2.AddHandler(InputElement.KeyDownEvent, TextBox2_PreviewKeyDown, RoutingStrategies.Tunnel);

		_dgItems = new()
		{
			new() { Text = "First item" },
			new() { Text = "Second Item" }
		};
		dg1.Items = _dgItems;
	}

	private void TextBox1_KeyUp(object? sender, KeyEventArgs e)
	{
		if(TextBox1.Text?.Length >= 3)
		{
			IInputElement? textBox2 = KeyboardNavigationHandler.GetNext(TextBox1, NavigationDirection.Next);
			FocusManager.Instance!.Focus(textBox2, NavigationMethod.Directional);
		}
	}

	private void TextBox2_PreviewKeyDown(object? sender, KeyEventArgs e)
	{
		if (e.Key != Key.Back || sender is not TextBox textBox2)
			return;

		IInputElement? textBox1 = KeyboardNavigationHandler.GetNext(textBox2, NavigationDirection.Previous);
		FocusManager.Instance!.Focus(textBox1, NavigationMethod.Directional);
		e.Handled = true;
	}

	private void Add_Click(object sender, RoutedEventArgs e)
	{
		_dgItems.Add(new() 
		{
			Text = "Item " + _dgItems.Count 
		});
	}
}

public class ListItem
{
	public string Text { get; set; } = string.Empty;
}