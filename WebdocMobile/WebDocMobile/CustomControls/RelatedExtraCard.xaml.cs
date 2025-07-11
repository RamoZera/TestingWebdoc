namespace WebDocMobile.CustomControls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RelatedExtraCard : ContentView
{

    public RelatedExtraCard()
    {
        InitializeComponent();

    }

    public static readonly BindableProperty NumberTagTextProperty = BindableProperty.Create(
       propertyName: nameof(NumberTagText),
       returnType: typeof(string),
       declaringType: typeof(RelatedExtraCard),
       defaultValue: "E/72023",
       defaultBindingMode: BindingMode.TwoWay);

    public string NumberTagText
    {
        get => (string)GetValue(NumberTagTextProperty);
        set { SetValue(NumberTagTextProperty, value); }
    }

    public static readonly BindableProperty AvatarTagTextProperty = BindableProperty.Create(
      propertyName: nameof(AvatarTagText),
      returnType: typeof(string),
      declaringType: typeof(RelatedExtraCard),
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
      declaringType: typeof(RelatedExtraCard),
      defaultValue: "01/01/1900 09:52",
      defaultBindingMode: BindingMode.TwoWay);

    public string DateTimeText
    {
        get => (string)GetValue(DateTimeTextProperty);
        set { SetValue(DateTimeTextProperty, value); }
    }

    public static readonly BindableProperty TopicTextProperty = BindableProperty.Create(
     propertyName: nameof(TopicText),
     returnType: typeof(string),
     declaringType: typeof(RelatedExtraCard),
     defaultValue: "Envio de documentação relacionada",
     defaultBindingMode: BindingMode.TwoWay);

    public string TopicText
    {
        get => (string)GetValue(TopicTextProperty);
        set { SetValue(TopicTextProperty, value); }
    }


    public static readonly BindableProperty DocumentTypeTextProperty = BindableProperty.Create(
    propertyName: nameof(DocumentTypeText),
    returnType: typeof(string),
    declaringType: typeof(RelatedExtraCard),
    defaultValue: "Lorem ipsum",
    defaultBindingMode: BindingMode.TwoWay);

    public string DocumentTypeText
    {
        get => (string)GetValue(DocumentTypeTextProperty);
        set { SetValue(DocumentTypeTextProperty, value); }
    }


    public static readonly BindableProperty StateTextProperty = BindableProperty.Create(
   propertyName: nameof(StateText),
   returnType: typeof(string),
   declaringType: typeof(RelatedExtraCard),
   defaultValue: "Lorem ipsum",
   defaultBindingMode: BindingMode.TwoWay);

    public string StateText
    {
        get => (string)GetValue(StateTextProperty);
        set { SetValue(StateTextProperty, value); }
    }

    public static readonly BindableProperty ByTextProperty = BindableProperty.Create(
   propertyName: nameof(ByText),
   returnType: typeof(string),
   declaringType: typeof(RelatedExtraCard),
   defaultValue: "Jeta Corp",
   defaultBindingMode: BindingMode.TwoWay);

    public string ByText
    {
        get => (string)GetValue(ByTextProperty);
        set { SetValue(ByTextProperty, value); }
    }

    public static readonly BindableProperty AvatarTagByTextProperty = BindableProperty.Create(
      propertyName: nameof(AvatarTagByText),
      returnType: typeof(string),
      declaringType: typeof(RelatedExtraCard),
      defaultValue: "J",
      defaultBindingMode: BindingMode.TwoWay);

    public string AvatarTagByText
    {
        get => (string)GetValue(AvatarTagByTextProperty);
        set { SetValue(AvatarTagByTextProperty, value); }
    }

    public static readonly BindableProperty EntityTextProperty = BindableProperty.Create(
  propertyName: nameof(EntityText),
  returnType: typeof(string),
  declaringType: typeof(RelatedExtraCard),
  defaultValue: "Jeta Corp",
  defaultBindingMode: BindingMode.TwoWay);

    public string EntityText
    {
        get => (string)GetValue(EntityTextProperty);
        set { SetValue(EntityTextProperty, value); }
    }
}