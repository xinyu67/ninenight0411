using DI.Models;
using DI.Service;

namespace DI.ViewModels
{
    public class UserViewModel
    {
        public List<User> DataList { get; set; }
        public string Search { get; set; }
    }
}
