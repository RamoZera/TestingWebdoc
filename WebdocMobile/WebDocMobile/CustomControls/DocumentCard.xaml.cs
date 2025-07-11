


using Telerik.Maui.Controls;

namespace WebDocMobile.CustomControls;

public partial class DocumentCard : RadBorder
{
	public DocumentCard()
	{
		InitializeComponent();
		this.BackgroundColor = Colors.White;
	}

    public static readonly BindableProperty NumberTagTextProperty = BindableProperty.Create(
      propertyName: nameof(NumberTagText),
      returnType: typeof(string),
      declaringType: typeof(ProcessCard),
      defaultValue: "E/12345",
      defaultBindingMode: BindingMode.TwoWay);

    public string NumberTagText
    {
        get => (string)GetValue(NumberTagTextProperty);
        set { SetValue(NumberTagTextProperty, value); }
    }

    public static readonly BindableProperty NumberTagBackgroundColorProperty = BindableProperty.Create(
       propertyName: nameof(NumberTagBackgroundColor),
       returnType: typeof(Color),
       declaringType: typeof(ProcessCard),
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
      declaringType: typeof(ProcessCard),
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
     declaringType: typeof(ProcessCard),
     defaultValue: "Em tratamento",
     defaultBindingMode: BindingMode.TwoWay);

    public string StatusText
    {
        get => (string)GetValue(StatusTextProperty);
        set { SetValue(StatusTextProperty, value); }
    }

    public static readonly BindableProperty StatusTextColorProperty = BindableProperty.Create(
     propertyName: nameof(StatusTextColor),
     returnType: typeof(Color),
     declaringType: typeof(ProcessCard),
     defaultValue: Color.FromArgb("#0074C8"),
     defaultBindingMode: BindingMode.TwoWay);

    public Color StatusTextColor
    {
        get => (Color)GetValue(StatusTextColorProperty);
        set { SetValue(StatusTextColorProperty, value); }
    }

    public static readonly BindableProperty StatusBackgroundColorProperty = BindableProperty.Create(
      propertyName: nameof(StatusBackgroundColor),
      returnType: typeof(Color),
      declaringType: typeof(ProcessCard),
      defaultValue: Color.FromArgb("#F6FCFF"),
      defaultBindingMode: BindingMode.TwoWay);

    public Color StatusBackgroundColor
    {
        get => (Color)GetValue(StatusBackgroundColorProperty);
        set { SetValue(StatusBackgroundColorProperty, value); }
    }

    public static readonly BindableProperty DescriptionTextProperty = BindableProperty.Create(
     propertyName: nameof(DescriptionText),
     returnType: typeof(string),
     declaringType: typeof(ProcessCard),
     defaultValue: "Description text goes here...",
     defaultBindingMode: BindingMode.TwoWay,
     coerceValue: (bindable, value) =>
     {
         string coercedValue = (string)value;
         if (coercedValue.Length > 54)
             coercedValue = coercedValue.Substring(0, 51) + "...";
         return coercedValue;
     });

    public string DescriptionText
    {
        get => (string)GetValue(DescriptionTextProperty);
        set { SetValue(DescriptionTextProperty, value); }
    }
}