using Telerik.Maui.Controls;

namespace WebDocMobile.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KnowledgeExtraCard : ContentView
    {
        public KnowledgeExtraCard()
        {
            InitializeComponent();
        }
        public static readonly BindableProperty NumberTagTextProperty = BindableProperty.Create(
       propertyName: nameof(NumberTagText),
       returnType: typeof(string),
       declaringType: typeof(KnowledgeExtraCard),
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
          declaringType: typeof(KnowledgeExtraCard),
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
          declaringType: typeof(KnowledgeExtraCard),
          defaultValue: "01/01/1900 09:52",
          defaultBindingMode: BindingMode.TwoWay);

        public string DateTimeText
        {
            get => (string)GetValue(DateTimeTextProperty);
            set { SetValue(DateTimeTextProperty, value); }
        }

        public static readonly BindableProperty CommentsTextProperty = BindableProperty.Create(
         propertyName: nameof(CommentsText),
         returnType: typeof(string),
         declaringType: typeof(KnowledgeExtraCard),
         defaultValue: "Envio de documentação relacionada",
         defaultBindingMode: BindingMode.TwoWay);

        public string CommentsText
        {
            get => (string)GetValue(CommentsTextProperty);
            set { SetValue(CommentsTextProperty, value); }
        }


        public static readonly BindableProperty ObservationsTextProperty = BindableProperty.Create(
        propertyName: nameof(ObservationsText),
        returnType: typeof(string),
        declaringType: typeof(KnowledgeExtraCard),
        defaultValue: "Lorem ipsum",
        defaultBindingMode: BindingMode.TwoWay);

        public string ObservationsText
        {
            get => (string)GetValue(ObservationsTextProperty);
            set { SetValue(ObservationsTextProperty, value); }
        }


      

        public static readonly BindableProperty ToTextProperty = BindableProperty.Create(
       propertyName: nameof(ToText),
       returnType: typeof(string),
       declaringType: typeof(KnowledgeExtraCard),
       defaultValue: "Jeta Corp",
       defaultBindingMode: BindingMode.TwoWay);

        public string ToText
        {
            get => (string)GetValue(ToTextProperty);
            set { SetValue(ToTextProperty, value); }
        }

        public static readonly BindableProperty AvatarTagByTextProperty = BindableProperty.Create(
          propertyName: nameof(AvatarTagByText),
          returnType: typeof(string),
          declaringType: typeof(KnowledgeExtraCard),
          defaultValue: "J",
          defaultBindingMode: BindingMode.TwoWay);

        public string AvatarTagByText
        {
            get => (string)GetValue(AvatarTagByTextProperty);
            set { SetValue(AvatarTagByTextProperty, value); }
        }

        public static readonly BindableProperty FromCardTextProperty = BindableProperty.Create(
       propertyName: nameof(FromCardText),
       returnType: typeof(string),
       declaringType: typeof(KnowledgeExtraCard),
       defaultValue: "Juridico",
       defaultBindingMode: BindingMode.TwoWay);

        public string FromCardText
        {
            get => (string)GetValue(FromCardTextProperty);
            set { SetValue(FromCardTextProperty, value); }
        }
    }
}

