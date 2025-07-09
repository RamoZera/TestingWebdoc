using System.Formats.Tar;
using WebDocMobile.Handlers;

namespace WebDocMobile.CustomControls;

public partial class OutlinedControlEntry : Grid
{
	public OutlinedControlEntry()
	{
		InitializeComponent();
    }

    public Entry entry
    {
        get { return txtEntry; }
        set { txtEntry = (BorderlessEntry) value; }
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        propertyName: nameof(Text),
        returnType: typeof(string),
        declaringType: typeof(OutlinedControlEntry),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set { SetValue(TextProperty, value); }
    }

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
      propertyName: nameof(Placeholder),
      returnType: typeof(string),
      declaringType: typeof(OutlinedControlEntry),
      defaultValue: null,
      defaultBindingMode: BindingMode.OneWay);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set { SetValue(PlaceholderProperty, value); }
    }

    private void txtEntry_Focused(object sender, FocusEventArgs e)
    {

        lblPlaceholder.FontSize = 14;
        lblPlaceholder.TextColor = Color.FromRgba("#0074C8");
        lblPlaceholder.Opacity = 1;
        lblPlaceholder.TranslateTo(0, -25, 80, easing: Easing.Linear);
    }

    private void txtEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Text))
        {
            lblPlaceholder.FontSize = 14;
            lblPlaceholder.TextColor = Color.FromRgba("#0074C8");
            lblPlaceholder.Opacity = 1;
            lblPlaceholder.TranslateTo(0, -25, 80, easing: Easing.Linear);
        }
        else
        {
            lblPlaceholder.FontSize = 16;
            lblPlaceholder.TextColor = Colors.Gray;
            lblPlaceholder.Opacity = 0.5;
            lblPlaceholder.TranslateTo(0, 0, 80, easing: Easing.Linear);
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        txtEntry.Focus();
    }
}