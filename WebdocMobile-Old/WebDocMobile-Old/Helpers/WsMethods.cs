using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDocMobile.Helpers.WsMethods
{
    public partial class GDProcess
    {

        private string documentExtField;

        private int iDGDTipoProcessoField;

        private string iDGDProcessField;

        private string subjectField;

        private string codeField;

        private string entityProcField;

        private string referenceField;

        private string contribuinteField;

        private System.Nullable<System.DateTime> registryDateField;

        private System.Nullable<System.DateTime> processDateField;

        private WorkFlowState workflowStateField;

        private GDProcessType processTypeField;

        private GDClassifier gDClassifierField;

        private Entity entityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string DocumentExt
        {
            get
            {
                return this.documentExtField;
            }
            set
            {
                this.documentExtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int IDGDTipoProcesso
        {
            get
            {
                return this.iDGDTipoProcessoField;
            }
            set
            {
                this.iDGDTipoProcessoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string IDGDProcess
        {
            get
            {
                return this.iDGDProcessField;
            }
            set
            {
                this.iDGDProcessField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Subject
        {
            get
            {
                return this.subjectField;
            }
            set
            {
                this.subjectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string EntityProc
        {
            get
            {
                return this.entityProcField;
            }
            set
            {
                this.entityProcField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string Contribuinte
        {
            get
            {
                return this.contribuinteField;
            }
            set
            {
                this.contribuinteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 8)]
        public System.Nullable<System.DateTime> RegistryDate
        {
            get
            {
                return this.registryDateField;
            }
            set
            {
                this.registryDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 9)]
        public System.Nullable<System.DateTime> ProcessDate
        {
            get
            {
                return this.processDateField;
            }
            set
            {
                this.processDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public WorkFlowState WorkflowState
        {
            get
            {
                return this.workflowStateField;
            }
            set
            {
                this.workflowStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public GDProcessType ProcessType
        {
            get
            {
                return this.processTypeField;
            }
            set
            {
                this.processTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public GDClassifier GDClassifier
        {
            get
            {
                return this.gDClassifierField;
            }
            set
            {
                this.gDClassifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public Entity Entity
        {
            get
            {
                return this.entityField;
            }
            set
            {
                this.entityField = value;
            }
        }
    }
    public partial class GDProcessType
    {

        private int iDGDProcessTypeField;

        private string gDProcessType1Field;

        private string cODGDProcessTypeField;

        private int iDClassifierField;

        private int iDWorkflowField;

        private int iDGDBookField;

        private bool visibleInGDField;

        private bool visibleInPrinterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int IDGDProcessType
        {
            get
            {
                return this.iDGDProcessTypeField;
            }
            set
            {
                this.iDGDProcessTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GDProcessType", Order = 1)]
        public string GDProcessType1
        {
            get
            {
                return this.gDProcessType1Field;
            }
            set
            {
                this.gDProcessType1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string CODGDProcessType
        {
            get
            {
                return this.cODGDProcessTypeField;
            }
            set
            {
                this.cODGDProcessTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public int IDClassifier
        {
            get
            {
                return this.iDClassifierField;
            }
            set
            {
                this.iDClassifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public int IDWorkflow
        {
            get
            {
                return this.iDWorkflowField;
            }
            set
            {
                this.iDWorkflowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public int IDGDBook
        {
            get
            {
                return this.iDGDBookField;
            }
            set
            {
                this.iDGDBookField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public bool VisibleInGD
        {
            get
            {
                return this.visibleInGDField;
            }
            set
            {
                this.visibleInGDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public bool VisibleInPrinter
        {
            get
            {
                return this.visibleInPrinterField;
            }
            set
            {
                this.visibleInPrinterField = value;
            }
        }
    }
    public partial class WorkFlowState
    {

        private int iDWorkflowStateField;

        private string workflowLabelField;

        private string isGroupField;

        private string movimentoSuportadoNaAppField;

        private string hasInterfaceField;

        private string movimentoInterfaceField;

        private string answerField;

        private string movimentoInterfaceParametersField;

        private string questionField;

        private int iDUserField;

        private int userConhecimentoField;

        private int iDWorkflowField;

        private int iDStateWorkflowField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int IDWorkflowState
        {
            get
            {
                return this.iDWorkflowStateField;
            }
            set
            {
                this.iDWorkflowStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string WorkflowLabel
        {
            get
            {
                return this.workflowLabelField;
            }
            set
            {
                this.workflowLabelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string isGroup
        {
            get
            {
                return this.isGroupField;
            }
            set
            {
                this.isGroupField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string MovimentoSuportadoNaApp
        {
            get
            {
                return this.movimentoSuportadoNaAppField;
            }
            set
            {
                this.movimentoSuportadoNaAppField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string HasInterface
        {
            get
            {
                return this.hasInterfaceField;
            }
            set
            {
                this.hasInterfaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string MovimentoInterface
        {
            get
            {
                return this.movimentoInterfaceField;
            }
            set
            {
                this.movimentoInterfaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string Answer
        {
            get
            {
                return this.answerField;
            }
            set
            {
                this.answerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string MovimentoInterfaceParameters
        {
            get
            {
                return this.movimentoInterfaceParametersField;
            }
            set
            {
                this.movimentoInterfaceParametersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public string Question
        {
            get
            {
                return this.questionField;
            }
            set
            {
                this.questionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public int IDUser
        {
            get
            {
                return this.iDUserField;
            }
            set
            {
                this.iDUserField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public int UserConhecimento
        {
            get
            {
                return this.userConhecimentoField;
            }
            set
            {
                this.userConhecimentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public int IDWorkflow
        {
            get
            {
                return this.iDWorkflowField;
            }
            set
            {
                this.iDWorkflowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public int IDStateWorkflow
        {
            get
            {
                return this.iDStateWorkflowField;
            }
            set
            {
                this.iDStateWorkflowField = value;
            }
        }
    }
    public partial class GDClassifier
    {

        private int iDGDClassifierField;

        private System.Nullable<int> iDGDClassifierParentField;

        private string gDClassifier1Field;

        private string codGDClassifierField;

        private string descriptionField;

        private bool inActiveField;

        private string limitedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int IDGDClassifier
        {
            get
            {
                return this.iDGDClassifierField;
            }
            set
            {
                this.iDGDClassifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 1)]
        public System.Nullable<int> IDGDClassifierParent
        {
            get
            {
                return this.iDGDClassifierParentField;
            }
            set
            {
                this.iDGDClassifierParentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GDClassifier", Order = 2)]
        public string GDClassifier1
        {
            get
            {
                return this.gDClassifier1Field;
            }
            set
            {
                this.gDClassifier1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string CodGDClassifier
        {
            get
            {
                return this.codGDClassifierField;
            }
            set
            {
                this.codGDClassifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public bool InActive
        {
            get
            {
                return this.inActiveField;
            }
            set
            {
                this.inActiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string Limited
        {
            get
            {
                return this.limitedField;
            }
            set
            {
                this.limitedField = value;
            }
        }
    }
    public partial class Entity
    {

        private int iDEntityField;

        private int iDImportedField;

        private string codEntityField;

        private string taxPayerNumberField;

        private string personalIdNumberField;

        private string entity1Field;

        private string addressField;

        private string postalCodeField;

        private string localeField;

        private string phoneField;

        private string mobilePhoneField;

        private string faxField;

        private string emailField;

        private bool visibleField;

        private bool historyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int IDEntity
        {
            get
            {
                return this.iDEntityField;
            }
            set
            {
                this.iDEntityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int IDImported
        {
            get
            {
                return this.iDImportedField;
            }
            set
            {
                this.iDImportedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string CodEntity
        {
            get
            {
                return this.codEntityField;
            }
            set
            {
                this.codEntityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string TaxPayerNumber
        {
            get
            {
                return this.taxPayerNumberField;
            }
            set
            {
                this.taxPayerNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string PersonalIdNumber
        {
            get
            {
                return this.personalIdNumberField;
            }
            set
            {
                this.personalIdNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Entity", Order = 5)]
        public string Entity1
        {
            get
            {
                return this.entity1Field;
            }
            set
            {
                this.entity1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public string Locale
        {
            get
            {
                return this.localeField;
            }
            set
            {
                this.localeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string MobilePhone
        {
            get
            {
                return this.mobilePhoneField;
            }
            set
            {
                this.mobilePhoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string Fax
        {
            get
            {
                return this.faxField;
            }
            set
            {
                this.faxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public bool Visible
        {
            get
            {
                return this.visibleField;
            }
            set
            {
                this.visibleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public bool History
        {
            get
            {
                return this.historyField;
            }
            set
            {
                this.historyField = value;
            }
        }
    }
    public partial class GDDocument
    {

        private string iDGDDocumentField;

        private int iDGDResolucaoField;

        private bool suportePapelField;

        private string subjectField;

        private string codeField;

        private string entityDocField;

        private string referenceField;

        private System.Nullable<System.DateTime> registryDateField;

        private System.Nullable<System.DateTime> deliveredDateField;

        private System.Nullable<System.DateTime> dataDocumentoField;

        private WorkFlowState workflowStateField;

        private GDDocumentType documentTypeField;

        private GDBook gDBookField;

        private GDClassifier gDClassifierField;

        private Entity entityField;

        private int deadlineField;

        private DocumentStateEnum documentStateField;

        private int iDDocumentHolderField;

        private string documentHolderField;

        private bool delegatedField;

        private bool hasPermissionToAddMovimentField;

        private string documentExtField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string IDGDDocument
        {
            get
            {
                return this.iDGDDocumentField;
            }
            set
            {
                this.iDGDDocumentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int IDGDResolucao
        {
            get
            {
                return this.iDGDResolucaoField;
            }
            set
            {
                this.iDGDResolucaoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public bool SuportePapel
        {
            get
            {
                return this.suportePapelField;
            }
            set
            {
                this.suportePapelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Subject
        {
            get
            {
                return this.subjectField;
            }
            set
            {
                this.subjectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string EntityDoc
        {
            get
            {
                return this.entityDocField;
            }
            set
            {
                this.entityDocField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string Reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 7)]
        public System.Nullable<System.DateTime> RegistryDate
        {
            get
            {
                return this.registryDateField;
            }
            set
            {
                this.registryDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 8)]
        public System.Nullable<System.DateTime> DeliveredDate
        {
            get
            {
                return this.deliveredDateField;
            }
            set
            {
                this.deliveredDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true, Order = 9)]
        public System.Nullable<System.DateTime> DataDocumento
        {
            get
            {
                return this.dataDocumentoField;
            }
            set
            {
                this.dataDocumentoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public WorkFlowState WorkflowState
        {
            get
            {
                return this.workflowStateField;
            }
            set
            {
                this.workflowStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public GDDocumentType DocumentType
        {
            get
            {
                return this.documentTypeField;
            }
            set
            {
                this.documentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public GDBook GDBook
        {
            get
            {
                return this.gDBookField;
            }
            set
            {
                this.gDBookField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public GDClassifier GDClassifier
        {
            get
            {
                return this.gDClassifierField;
            }
            set
            {
                this.gDClassifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public Entity Entity
        {
            get
            {
                return this.entityField;
            }
            set
            {
                this.entityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        public int Deadline
        {
            get
            {
                return this.deadlineField;
            }
            set
            {
                this.deadlineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        public DocumentStateEnum DocumentState
        {
            get
            {
                return this.documentStateField;
            }
            set
            {
                this.documentStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        public int IDDocumentHolder
        {
            get
            {
                return this.iDDocumentHolderField;
            }
            set
            {
                this.iDDocumentHolderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        public string DocumentHolder
        {
            get
            {
                return this.documentHolderField;
            }
            set
            {
                this.documentHolderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        public bool Delegated
        {
            get
            {
                return this.delegatedField;
            }
            set
            {
                this.delegatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        public bool HasPermissionToAddMoviment
        {
            get
            {
                return this.hasPermissionToAddMovimentField;
            }
            set
            {
                this.hasPermissionToAddMovimentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        public string DocumentExt
        {
            get
            {
                return this.documentExtField;
            }
            set
            {
                this.documentExtField = value;
            }
        }
    }
    public partial class GDDocumentType
    {

        private int iDGDDocumentTypeField;

        private string gDDocumentType1Field;

        private string cODGDDocumentTypeField;

        private int iDClassifierField;

        private int iDWorkflowField;

        private int iDGDBookField;

        private bool visibleInGDField;

        private bool visibleInPrinterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int IDGDDocumentType
        {
            get
            {
                return this.iDGDDocumentTypeField;
            }
            set
            {
                this.iDGDDocumentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GDDocumentType", Order = 1)]
        public string GDDocumentType1
        {
            get
            {
                return this.gDDocumentType1Field;
            }
            set
            {
                this.gDDocumentType1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string CODGDDocumentType
        {
            get
            {
                return this.cODGDDocumentTypeField;
            }
            set
            {
                this.cODGDDocumentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public int IDClassifier
        {
            get
            {
                return this.iDClassifierField;
            }
            set
            {
                this.iDClassifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public int IDWorkflow
        {
            get
            {
                return this.iDWorkflowField;
            }
            set
            {
                this.iDWorkflowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public int IDGDBook
        {
            get
            {
                return this.iDGDBookField;
            }
            set
            {
                this.iDGDBookField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public bool VisibleInGD
        {
            get
            {
                return this.visibleInGDField;
            }
            set
            {
                this.visibleInGDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public bool VisibleInPrinter
        {
            get
            {
                return this.visibleInPrinterField;
            }
            set
            {
                this.visibleInPrinterField = value;
            }
        }
    }
    public partial class GDBook
    {

        private int iDBookField;

        private string gDBook1Field;

        private string cODGDBookField;

        private string entityRequiredField;

        private string visibleInPrinterField;

        private string visibleInGDField;

        private string processField;

        private GDBookDirection iDGDBookDirectionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int IDBook
        {
            get
            {
                return this.iDBookField;
            }
            set
            {
                this.iDBookField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GDBook", Order = 1)]
        public string GDBook1
        {
            get
            {
                return this.gDBook1Field;
            }
            set
            {
                this.gDBook1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string CODGDBook
        {
            get
            {
                return this.cODGDBookField;
            }
            set
            {
                this.cODGDBookField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string EntityRequired
        {
            get
            {
                return this.entityRequiredField;
            }
            set
            {
                this.entityRequiredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string VisibleInPrinter
        {
            get
            {
                return this.visibleInPrinterField;
            }
            set
            {
                this.visibleInPrinterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string VisibleInGD
        {
            get
            {
                return this.visibleInGDField;
            }
            set
            {
                this.visibleInGDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string Process
        {
            get
            {
                return this.processField;
            }
            set
            {
                this.processField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public GDBookDirection IDGDBookDirection
        {
            get
            {
                return this.iDGDBookDirectionField;
            }
            set
            {
                this.iDGDBookDirectionField = value;
            }
        }
    }
    public enum GDBookDirection
    {

        /// <remarks/>
        NONE,

        /// <remarks/>
        SAIDA,

        /// <remarks/>
        ENTRADA,

        /// <remarks/>
        INTERNO,
    }
    public enum DocumentStateEnum
    {

        /// <remarks/>
        WithMe,

        /// <remarks/>
        WithTeam,

        /// <remarks/>
        ToTakeNotice,

        /// <remarks/>
        PassedMeBy,

        /// <remarks/>
        Archived,

        /// <remarks/>
        Canceled,

        /// <remarks/>
        Concluded,

        /// <remarks/>
        InProcess,

        /// <remarks/>
        SeeAll,

        /// <remarks/>
        NoState,
    }

}
