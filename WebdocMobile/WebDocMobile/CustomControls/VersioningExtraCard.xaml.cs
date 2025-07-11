using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
namespace WebDocMobile.CustomControls;

public partial class VersioningExtraCard : ContentView
{
	public VersioningExtraCard()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty NumberCardTextProperty = BindableProperty.Create(
        propertyName: nameof(NumberCardText),
        returnType: typeof(string),
        declaringType: typeof(VersioningExtraCard),
        defaultValue: "#1",
        defaultBindingMode: BindingMode.TwoWay);

    public string NumberCardText
    {
        get => (string)GetValue(NumberCardTextProperty);
        set { SetValue(NumberCardTextProperty, value); }
    }
}