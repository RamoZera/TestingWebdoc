using CommunityToolkit.Mvvm.Input;
using Telerik.Maui.Controls;

namespace WebDocMobile.CustomControls;

public partial class NotificationCard : RadBorder
{

    public NotificationCard()
	{
		InitializeComponent();

    }

    public static readonly BindableProperty BackgroundIconColorProperty = BindableProperty.Create(
        propertyName: nameof(BackgroundIconColor),
        returnType: typeof(Color),
        declaringType: typeof(NotificationCard),
        defaultValue: Color.FromArgb("#FFB60A"),
        defaultBindingMode: BindingMode.TwoWay);

    public Color BackgroundIconColor
    {
        get => (Color)GetValue(BackgroundIconColorProperty);
        set { SetValue(BackgroundIconColorProperty, value); }
    }

    public static readonly BindableProperty IconSourceProperty = BindableProperty.Create(
       propertyName: nameof(IconSource),
       returnType: typeof(string),
       declaringType: typeof(NotificationCard),
       defaultValue: "icon_notificacoes_white",
       defaultBindingMode: BindingMode.TwoWay);

    public string IconSource
    {
        get => (string)GetValue(IconSourceProperty);
        set { SetValue(IconSourceProperty, value); }
    }

    public static readonly BindableProperty PrimaryTextProperty = BindableProperty.Create(
       propertyName: nameof(PrimaryText),
       returnType: typeof(string),
       declaringType: typeof(NotificationCard),
       defaultValue: "Primary text goes here",
       defaultBindingMode: BindingMode.TwoWay);

    public string PrimaryText
    {
        get => (string)GetValue(PrimaryTextProperty);
        set { SetValue(PrimaryTextProperty, value); }
    }

    public static readonly BindableProperty SecondaryTextProperty = BindableProperty.Create(
       propertyName: nameof(SecondaryText),
       returnType: typeof(string),
       declaringType: typeof(NotificationCard),
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
       declaringType: typeof(NotificationCard),
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
       declaringType: typeof(NotificationCard),
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
       declaringType: typeof(NotificationCard),
       defaultValue:"SwitchLabel",
       defaultBindingMode: BindingMode.TwoWay);

    public string SwitchLabel
    {
        get => (string)GetValue(SwitchLabelProperty);
        set { SetValue(SwitchLabelProperty, value); }
    }

    public static readonly BindableProperty SwitchOnProperty = BindableProperty.Create(
       propertyName: nameof(SwitchOn),
       returnType: typeof(bool),
       declaringType: typeof(NotificationCard),
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
      declaringType: typeof(NotificationCard),
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
      declaringType: typeof(NotificationCard),
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
      defaultBindingMode: BindingMode.TwoWay);

    public bool Visible
    {
        get => (bool)GetValue(VisibleProperty);
        set { SetValue(VisibleProperty, value); }
    }

    private void CloseNotificationCard(object sender, EventArgs e)
    {
        Visible = false;
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