using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace WebServiceContracts
{
    [ServiceContract]
    public interface ICompanyService
    {
        [OperationContract]
        IEnumerable<Company> GetAllCompanies();
    }
}
