using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WebDocMobile.Models;
using WebDocMobile.PageModels.StandardViewModels;

namespace WebDocMobile.PageModels
{
    public class OnboardingPageModel
    {
        #region Properties

        public ObservableCollection<Models.OnboardingModel> OnboardingPages { get; set; } = new ObservableCollection<Models.OnboardingModel>();
        #endregion

        public OnboardingPageModel()
        {
            OnboardingPages.Add(new Models.OnboardingModel
            {
                //IntroLogo = "logo_v2",
                IntroTitle = "Importância da Gestão Documental",
                IntroDescription = "A gestão documental é um aspecto crucial para qualquer organização, pois envolve o controle eficiente do ciclo de vida dos documentos."
            });

            OnboardingPages.Add(new Models.OnboardingModel
            {
                //IntroLogo = "logo_v2",
                IntroTitle = "Impacto na Eficiência dos Processos",
                IntroDescription = "Automatizar o fluxo de documentos reduz o tempo gasto em tarefas manuais, minimiza erros e garante que os documentos corretos cheguem às pessoas certas no momento certo."
            });

            OnboardingPages.Add(new Models.OnboardingModel
            {
                //IntroLogo = "logo_v2",
                IntroTitle = "Colaboração em Tempo Real",
                IntroDescription = "Com a capacidade de gerir documentos enquanto estão em movimento, os utilizadores podem aumentar a eficiência e melhorar a produtividade geral."
            });
        }

        //public ICommand NextOB => new Command(() =>
        //{
        //    if (Position >= OnboardingPages.Count - 1)
        //    {

        //    }
        //    else
        //    {

        //        Position += 1;
        //    }
        //});

    }


}
