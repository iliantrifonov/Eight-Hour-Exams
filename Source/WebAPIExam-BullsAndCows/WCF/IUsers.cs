namespace WCF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Text;
    using WCF.DataModels;
    using Web.DataModels;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUsers
    {
        [WebGet(UriTemplate = "?{page=0}")]
        [OperationContract]
        IEnumerable<UserOutputDataModel> GetData(string page);

        //[WebGet(UriTemplate = "/{id}")]
        //[OperationContract]
        //UserDetailedModel GetUser(string id);
    }
}
