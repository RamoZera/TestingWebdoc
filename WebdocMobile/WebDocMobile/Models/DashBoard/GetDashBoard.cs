namespace WebDocMobile.Models.DashBoard
{


    public class GetDashBoardResponse
    {
        public GetDashBoard result { get; set; }
        public int status { get; set; }
        public object error { get; set; }
    }

    public class GetDashBoard2
    {
        public int documentsComigo { get; set; }
        public int documentsDepartamento { get; set; }
        public int documentsConhecimento { get; set; }
        public int documentsDelegados { get; set; }
        public int processesComigo { get; set; }
        public int processesDepartamento { get; set; }
        public int processesConhecimento { get; set; }
        public int processesDelegados { get; set; }

    }

    public class GetDashBoard
    {
            public int DocumentsCount { get; set; }

            public int DocumentsDepartamento { get; set; }

            public int DocumentsConhecimento { get; set; }

            public int DocumentsDelegados { get; set; }

            public int ProcessesCount { get; set; }

            public int ProcessesDepartamento { get; set; }

            public int ProcessesConhecimento { get; set; }

            public int ProcessesDelegados { get; set; }
        
    }

    public class Document
    {
        public int id { get; set; }
        public string number { get; set; }
        public DateTime? sentData { get; set; }
        public string subject { get; set; }
        public string workflowState { get; set; }
        public string dateString { get; set; }
    }

    public class Processes
    {
        public int id { get; set; }
        public string number { get; set; }
        public DateTime sentData { get; set; }
        public string subject { get; set; }
        public string workflowState { get; set; }
        public string dateString { get; set; }
    }


}

