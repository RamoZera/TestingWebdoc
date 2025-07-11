using Telerik.Maui.Controls;

namespace WebDocMobile.CustomControls;

public partial class FrameCard : RadBorder
{
	public FrameCard()
	{
		InitializeComponent();
        this.BackgroundColor = Colors.Transparent;
	}
    public static readonly BindableProperty TitleTagTextProperty = BindableProperty.Create(
      propertyName: nameof(TitleTagText),
      returnType: typeof(string),
      declaringType: typeof(FrameCard),
      defaultValue: "",
      defaultBindingMode: BindingMode.TwoWay);
    public string TitleTagText
    {
        get => (string)GetValue(TitleTagTextProperty);
        set { SetValue(TitleTagTextProperty, value); }
    }

    public static readonly BindableProperty NumberTagTextProperty = BindableProperty.Create(
       propertyName: nameof(NumberTagText),
       returnType: typeof(string),
       declaringType: typeof(FrameCard),
       defaultValue: "Attribute",
       defaultBindingMode: BindingMode.TwoWay);

    public string NumberTagText
    {
        get => (string)GetValue(NumberTagTextProperty);
        set { SetValue(NumberTagTextProperty, value); }
    }

    public static readonly BindableProperty NumberTagBackgroundColorProperty = BindableProperty.Create(
       propertyName: nameof(NumberTagBackgroundColor),
       returnType: typeof(Color),
       declaringType: typeof(FrameCard),
       defaultValue: Color.FromArgb("#EBEBEB"),
       defaultBindingMode: BindingMode.TwoWay);

    public Color NumberTagBackgroundColor
    {
        get => (Color)GetValue(NumberTagBackgroundColorProperty);
        set { SetValue(NumberTagBackgroundColorProperty, value); }
    }

    public static readonly BindableProperty DateTimeTextProperty = BindableProperty.Create(
      propertyName: nameof(DateTimeText),
      returnType: typeof(string),
      declaringType: typeof(FrameCard),
      defaultValue: "01/01/1900 09:52",
      defaultBindingMode: BindingMode.TwoWay);

    public string DateTimeText
    {
        get => (string)GetValue(DateTimeTextProperty);
        set { SetValue(DateTimeTextProperty, value); }
    }

    public static readonly BindableProperty StatusTextProperty = BindableProperty.Create(
     propertyName: nameof(StatusText),
     returnType: typeof(string),
     declaringType: typeof(FrameCard),
     defaultValue: "Obrigatório",
     defaultBindingMode: BindingMode.TwoWay);

    public string StatusText
    {
        get => (string)GetValue(StatusTextProperty);
        set { SetValue(StatusTextProperty, value); }
    }

    public static readonly BindableProperty StatusTextColorProperty = BindableProperty.Create(
     propertyName: nameof(StatusTextColor),
     returnType: typeof(Color),
     declaringType: typeof(FrameCard),
     defaultValue: Color.FromArgb("#DC3545"),
     defaultBindingMode: BindingMode.TwoWay);

    public Color StatusTextColor
    {
        get => (Color)GetValue(StatusTextColorProperty);
        set { SetValue(StatusTextColorProperty, value); }
    }

    public static readonly BindableProperty StatusBackgroundColorProperty = BindableProperty.Create(
      propertyName: nameof(StatusBackgroundColor),
      returnType: typeof(Color),
      declaringType: typeof(FrameCard),
      defaultValue: Color.FromArgb("#FEF4F5"),
      defaultBindingMode: BindingMode.TwoWay);

    public Color StatusBackgroundColor
    {
        get => (Color)GetValue(StatusBackgroundColorProperty);
        set { SetValue(StatusBackgroundColorProperty, value); }
    }

    public static readonly BindableProperty StatusBorderColorProperty = BindableProperty.Create(
     propertyName: nameof(StatusBorderColor),
     returnType: typeof(Color),
     declaringType: typeof(FrameCard),
     defaultValue: Color.FromArgb("#FF5050"),
     defaultBindingMode: BindingMode.TwoWay);

    public Color StatusBorderColor
    {
        get => (Color)GetValue(StatusBorderColorProperty);
        set { SetValue(StatusBorderColorProperty, value); }
    }
    public static readonly BindableProperty DescriptionTextProperty = BindableProperty.Create(
     propertyName: nameof(DescriptionText),
     returnType: typeof(string),
     declaringType: typeof(FrameCard),
     defaultValue: "Description text goes here...",
     defaultBindingMode: BindingMode.TwoWay,
     coerceValue: (bindable, value) =>
     {
        string coercedValue = (string)value;
        if (coercedValue.Length > 46)
            coercedValue = coercedValue.Substring(0, 43) + "...";
        return coercedValue;
     });

    public string DescriptionText
    {
        get => (string)GetValue(DescriptionTextProperty);
        set { SetValue(DescriptionTextProperty, value); }
    }
}
