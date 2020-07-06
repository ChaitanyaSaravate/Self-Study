using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Authentication.ExtendedProtection.Configuration;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebServiceContracts;

namespace WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CompanyService : ICompanyService
    {
        [WebGet]
        //[WebInvoke]
        public IEnumerable<Company> GetAllCompanies()
        {
            List<Company> result = new List<Company>();
            for (int i = 0; i < 100; i++)
            {
                var company = new Company
                {
                    Name = "Tieto",
                    Address1 = "Wing D, EON IT Park, Kharadi, Pune-46",
                    Address2 = "Wing D, EON IT Park, Kharadi, Pune-46",
                    Address3 = "Wing D, EON IT Park, Kharadi, Pune-46",
                    Address4 = "Wing D, EON IT Park, Kharadi, Pune-46",
                    Address5 = "Wing D, EON IT Park, Kharadi, Pune-46",
                    Address6 = "Wing D, EON IT Park, Kharadi, Pune-46",
                    Address7 = "Wing D, EON IT Park, Kharadi, Pune-46",
                    Address8 = "Wing D, EON IT Park, Kharadi, Pune-46",
                };

                List<Employee> employees = new List<Employee>();

                for (int j = 0; j < 1000; j++)
                {
                    employees.Add(new Employee
                    {
                        Name = "Chaitanya Saravate",
                        Address1 = "C-401, Lake Vista, Ambegaon, Pune - 46",
                        Address2 = "C-401, Lake Vista, Ambegaon, Pune - 46",
                        Address3 = "C-401, Lake Vista, Ambegaon, Pune - 46",
                        Address4 = "C-401, Lake Vista, Ambegaon, Pune - 46",
                        Address5 = "C-401, Lake Vista, Ambegaon, Pune - 46",
                        Address6 = "C-401, Lake Vista, Ambegaon, Pune - 46",
                        Address7 = "C-401, Lake Vista, Ambegaon, Pune - 46",
                        Address8 = "C-401, Lake Vista, Ambegaon, Pune - 46",
                    });
                }

                company.Employees = employees;

                result.Add(company);
            }

            return result;
        }
    }
}
