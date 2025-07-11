using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
namespace WebDocMobile.CustomControls;

public partial class MovementsExtrasCard : ContentView
{
    public MovementsExtrasCard()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty FromCardTextProperty = BindableProperty.Create(
        propertyName: nameof(FromCardText),
        returnType: typeof(string),
        declaringType: typeof(MovementsExtrasCard),
        defaultValue: "Juridico",
        defaultBindingMode: BindingMode.TwoWay);

    public string FromCardText
    {
        get => (string)GetValue(FromCardTextProperty);
        set { SetValue(FromCardTextProperty, value); }
    }
    public static readonly BindableProperty AvatarTagTextProperty = BindableProperty.Create(
      propertyName: nameof(AvatarTagText),
      returnType: typeof(string),
      declaringType: typeof(MovementsExtrasCard),
      defaultValue: "J",
      defaultBindingMode: BindingMode.TwoWay);

    public string AvatarTagText
    {
        get => (string)GetValue(AvatarTagTextProperty);
        set { SetValue(AvatarTagTextProperty, value); }
    }

    public static readonly BindableProperty DateTimeTextProperty = BindableProperty.Create(
      propertyName: nameof(DateTimeText),
      returnType: typeof(string),
      declaringType: typeof(MovementsExtrasCard),
      defaultValue: "01/01/1900 09:52",
      defaultBindingMode: BindingMode.TwoWay);

    public string DateTimeText
    {
        get => (string)GetValue(DateTimeTextProperty);
        set { SetValue(DateTimeTextProperty, value); }
    }

    public static readonly BindableProperty InformationTextProperty = BindableProperty.Create(
      propertyName: nameof(InformationText),
      returnType: typeof(string),
      declaringType: typeof(RelatedExtraCard),
      defaultValue: "Movimento Automatico",
      defaultBindingMode: BindingMode.TwoWay);

    public string InformationText
    {
        get => (string)GetValue(InformationTextProperty);
        set { SetValue(InformationTextProperty, value); }
    }

    public static readonly BindableProperty ToTextProperty = BindableProperty.Create(
      propertyName: nameof(ToText),
      returnType: typeof(string),
      declaringType: typeof(MovementsExtrasCard),
      defaultValue: "Juridico",
      defaultBindingMode: BindingMode.TwoWay);

    public string ToText
    {
        get => (string)GetValue(ToTextProperty);
        set { SetValue(ToTextProperty, value); }
    }

    public static readonly BindableProperty NumberTextProperty = BindableProperty.Create(
      propertyName: nameof(NumberText),
      returnType: typeof(string),
      declaringType: typeof(MovementsExtrasCard),
      defaultValue: "Juridico",
      defaultBindingMode: BindingMode.TwoWay);

    public string NumberText
    {
        get => (string)GetValue(NumberTextProperty);
        set { SetValue(NumberTextProperty, value); }
    }

}