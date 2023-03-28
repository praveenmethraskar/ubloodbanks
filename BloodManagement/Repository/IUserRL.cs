using BloodManagement.Models;
using System.Collections.Generic;

namespace BloodManagement.Repository
{
    public interface IUserRL
    {
        public UserRegistrationModel Registration(UserRegistrationModel employee);
        public UserRegistrationModel UserLogin(UserLoginModel loginModel);
        public IEnumerable<UserRegistrationModel> Getusers(int users_id);
        //public UserRegistrationModel Profile(UserRegistrationModel employee);
        public AddMoreDetails Profile(AddMoreDetails employee, int users_id);
        //public UserRegistrationModel Profile(UserRegistrationModel employee, int users_id);
        public IEnumerable<AddMoreDetails> Getprofile(int users_id);
        public IEnumerable<HospitalModel> GetHospitals();

        public Donations EnterDonations(Donations donations, int users_id);

        public IEnumerable<Donations> Listdonations(int users_id);
    }
}
