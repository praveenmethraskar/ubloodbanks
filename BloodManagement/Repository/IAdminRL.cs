using BloodManagement.Models;
using System.Collections.Generic;

namespace BloodManagement.Repository
{
    public interface IAdminRL
    {
        public AdminRegisterModel Register(AdminRegisterModel admin);
        public AdminRegisterModel adminLogin(AdminLoginModel loginModel);
        public IEnumerable<AddMoreDetails> Getdetails();

        public HospitalModel Addhospitals(HospitalModel hospital);
        public IEnumerable<HospitalModel> GetHospitals();
    }
}
