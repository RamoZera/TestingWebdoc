﻿#nullable enable
using WebDocMobile.Handlers;

namespace WebDocMobile.CustomControls;

public partial class OutlinedControlEntry : Grid
{
	public OutlinedControlEntry()
	{
		InitializeComponent();
        UpdateVisualState(false);
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

    private void UpdateVisualState(bool isFocused)
    {
        string state = isFocused || !string.IsNullOrEmpty(Text) ? "Focused" : "Normal";
        VisualStateManager.GoToState(lblPlaceholder, state);
    }

    protected override void OnPropertyChanged(string? propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        if (propertyName == TextProperty.PropertyName)
        {
            UpdateVisualState(txtEntry.IsFocused);
        }
    }

    private void txtEntry_Focused(object? sender, FocusEventArgs e)
    {
        UpdateVisualState(true);
    }

    private void txtEntry_Unfocused(object? sender, FocusEventArgs e)
    {
        UpdateVisualState(false);
    }

    private void TapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        txtEntry.Focus();
    }
}