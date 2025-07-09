using WebDocMobile.Handlers;

namespace WebDocMobile.CustomControls;

public partial class OutlinedControlEntry : Grid
{
	public OutlinedControlEntry()
	{
		InitializeComponent();
        txtEntry.Focused += OnEntryFocused;
        txtEntry.Unfocused += OnEntryUnfocused;
        txtEntry.TextChanged += OnEntryTextChanged;
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

    private void OnEntryFocused(object sender, FocusEventArgs e)
    {
        UpdateVisualState();
    }

    private void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        UpdateVisualState();
    }

    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        // The placeholder should float up if the entry is focused OR if it has text.
        string state = txtEntry.IsFocused || !string.IsNullOrEmpty(Text) ? "Focused" : "Normal";
        VisualStateManager.GoToState(this, state);
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        txtEntry.Focus();
    }
}