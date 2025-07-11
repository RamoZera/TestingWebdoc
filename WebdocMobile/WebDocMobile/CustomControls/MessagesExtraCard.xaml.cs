namespace WebDocMobile.CustomControls;
using Telerik.Maui.Controls;
using CommunityToolkit.Mvvm.Input;

public partial class MessagesExtraCard : ContentView
{
    public MessagesExtraCard()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty BackgroundIconColorProperty = BindableProperty.Create(
           propertyName: nameof(BackgroundIconColor),
           returnType: typeof(Color),
           declaringType: typeof(MessagesCard),
           defaultValue: Color.FromArgb("#ebebeb"),
           defaultBindingMode: BindingMode.TwoWay
        );

    public Color BackgroundIconColor
    {
        get => (Color)GetValue(BackgroundIconColorProperty);
        set { SetValue(BackgroundIconColorProperty, value); }
    }

    public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
       propertyName: nameof(IconSource),
       returnType: typeof(string),
       declaringType: typeof(MessagesCard),
       defaultValue: "icon_user",
       defaultBindingMode: BindingMode.TwoWay);

    public string IconSource
    {
        get => (string)GetValue(IconSourceProperty);
        set { SetValue(IconSourceProperty, value); }
    }

    public static readonly BindableProperty PrimaryTextProperty = BindableProperty.Create(
       propertyName: nameof(PrimaryText),
       returnType: typeof(string),
       declaringType: typeof(MessagesCard),
       defaultValue: "Primary text goes here",
       defaultBindingMode: BindingMode.TwoWay);

    public string PrimaryText
    {
        get => (string)GetValue(PrimaryTextProperty);
        set { SetValue(PrimaryTextProperty, value); }
    }

    public static readonly BindableProperty HourTextProperty = BindableProperty.Create(
      propertyName: nameof(HourText),
      returnType: typeof(string),
      declaringType: typeof(MessagesCard),
      defaultValue: "9:00",
      defaultBindingMode: BindingMode.TwoWay);

    public string HourText
    {
        get => (string)GetValue(HourTextProperty);
        set { SetValue(HourTextProperty, value); }
    }

    public static readonly BindableProperty SecondaryTextProperty = BindableProperty.Create(
       propertyName: nameof(SecondaryText),
       returnType: typeof(string),
       declaringType: typeof(MessagesCard),
       defaultValue: "Secondary text goes here",
       defaultBindingMode: BindingMode.TwoWay);

    public string SecondaryText
    {
        get => (string)GetValue(SecondaryTextProperty);
        set { SetValue(SecondaryTextProperty, value); }
    }

    public static readonly BindableProperty HyperlinkTextProperty = BindableProperty.Create(
       propertyName: nameof(HyperlinkText),
       returnType: typeof(string),
       declaringType: typeof(MessagesCard),
       defaultValue: "Hyperlink text",
       defaultBindingMode: BindingMode.TwoWay);

    public string HyperlinkText
    {
        get => (string)GetValue(HyperlinkTextProperty);
        set { SetValue(HyperlinkTextProperty, value); }
    }

    public static readonly BindableProperty HyperlinkTextColorProperty = BindableProperty.Create(
       propertyName: nameof(HyperlinkTextColor),
       returnType: typeof(Color),
       declaringType: typeof(MessagesCard),
       defaultValue: Color.FromArgb("#0074C8"),
       defaultBindingMode: BindingMode.TwoWay);

    public Color HyperlinkTextColor
    {
        get => (Color)GetValue(HyperlinkTextColorProperty);
        set { SetValue(HyperlinkTextColorProperty, value); }
    }

    public static readonly BindableProperty SwitchLabelProperty = BindableProperty.Create(
       propertyName: nameof(SwitchLabel),
       returnType: typeof(string),
       declaringType: typeof(MessagesCard),
       defaultValue: "SwitchLabel",
       defaultBindingMode: BindingMode.TwoWay);

    public string SwitchLabel
    {
        get => (string)GetValue(SwitchLabelProperty);
        set { SetValue(SwitchLabelProperty, value); }
    }

    public static readonly BindableProperty SwitchOnProperty = BindableProperty.Create(
       propertyName: nameof(SwitchOn),
       returnType: typeof(bool),
       declaringType: typeof(MessagesCard),
       defaultValue: false,
       defaultBindingMode: BindingMode.TwoWay);

    public bool SwitchOn
    {
        get => (bool)GetValue(SwitchOnProperty);
        set { SetValue(SwitchOnProperty, value); }
    }

    public static readonly BindableProperty SwitchVisibleProperty = BindableProperty.Create(
      propertyName: nameof(SwitchVisible),
      returnType: typeof(bool),
      declaringType: typeof(MessagesCard),
      defaultValue: false,
      defaultBindingMode: BindingMode.TwoWay);

    public bool SwitchVisible
    {
        get => (bool)GetValue(SwitchVisibleProperty);
        set { SetValue(SwitchVisibleProperty, value); }
    }

    //TODO This prop must be linked with the negation of SwitchVisibleProperty or viceversa instead of be a diferente prop
    public static readonly BindableProperty HyperlinkVisibleProperty = BindableProperty.Create(
      propertyName: nameof(HyperlinkVisible),
      returnType: typeof(bool),
      declaringType: typeof(MessagesCard),
      defaultValue: false,
      defaultBindingMode: BindingMode.TwoWay);

    public bool HyperlinkVisible
    {
        get => (bool)GetValue(HyperlinkVisibleProperty);
        set { SetValue(HyperlinkVisibleProperty, value); }
    }

    public static readonly BindableProperty VisibleProperty = BindableProperty.Create(
        propertyName: nameof(Visible),
        returnType: typeof(bool),
        declaringType: typeof(NotificationCard),
        defaultValue: true,
        defaultBindingMode: BindingMode.TwoWay
    );

    public bool Visible
    {
        get => (bool)GetValue(VisibleProperty);
        set { SetValue(VisibleProperty, value); }
    }

    public static readonly BindableProperty BackgroundRightBarColorProperty = BindableProperty.Create(
      propertyName: nameof(BackgroundRightBarColor),
      returnType: typeof(Color),
      declaringType: typeof(NotificationCard),
      defaultValue: Color.FromArgb("#FFB60A"),
      defaultBindingMode: BindingMode.TwoWay);

    public Color BackgroundRightBarColor
    {
        get => (Color)GetValue(BackgroundRightBarColorProperty);
        set { SetValue(BackgroundRightBarColorProperty, value); }
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        propertyName: nameof(Command),
        returnType: typeof(IRelayCommand),
        declaringType: typeof(NotificationCard));

    public IRelayCommand Command
    {
        get { return (IRelayCommand)GetValue(CommandProperty); }
        set { SetValue(CommandProperty, value); }
    }
}