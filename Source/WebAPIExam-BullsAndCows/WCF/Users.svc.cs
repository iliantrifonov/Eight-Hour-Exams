using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using Data;
using Web.DataModels;
using WCF.DataModels;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Users : IUsers
    {
        private IBullsAndCowsData data;
        private const int PerPage = 10;

        public Users()
        {
            this.data = new BullsAndCowsData();
        }

        public IEnumerable<UserOutputDataModel> GetData(string page)
        {
            var intPage = int.Parse(page);

            var users = this.data.Users.All()
                            .OrderBy(u => u.UserName)
                            .Skip(intPage * PerPage)
                            .Take(PerPage)
                            .Select(UserOutputDataModel.ToModel).ToList();

            return users;
        }

        //public UserDetailedModel GetUser(string id)
        //{
        //    var user = this.data.Users.All()
        //                   .Where(u => u.Id == id)
        //                   .Select(u => new UserDetailedModel()
        //                          {
        //                              Id = u.Id,
        //                              Losses = u.LossCount,
        //                              Rank = u.Rank,
        //                              Username = u.UserName,
        //                              Wins = u.WinCount
        //                          })
        //                   .FirstOrDefault();

        //    return user;
        //}
    }
}
