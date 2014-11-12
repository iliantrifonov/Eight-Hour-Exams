//namespace Web.Controllers
//{
//    using Data;
//    using Model;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Net;
//    using System.Net.Http;
//    using System.Web.Http;
//    using System.Web.OData;
//    using Web.DataModels;

//    public class TestController : BaseApiController
//    {
//        public TestController(IApplicationData data)
//            : base(data)
//        {
//        }

//        [HttpGet]
//        [EnableQuery]
//        public IHttpActionResult GetOneData()
//        {
//            var all = this.data.Ones.All().Select(o => new
//            {
//                Name = o.Name,
//                Id = o.Id,
//                Collection = o.CollectionTwos.AsQueryable().Select(t => new TestModel
//                {
//                    Name = t.Name,
//                    Id = t.Id
//                })
//            });

//            return Ok(all);
//        }

//        [HttpPost]
//        public IHttpActionResult PostOneData(string name)
//        {
//            var model = new TestModelOne()
//            {
//                Name = name,
//            };

//            this.data.Ones.Add(model);
//            this.data.SaveChanges();

//            var response = this.Request.CreateResponse(HttpStatusCode.Created, new
//            {
//                Name = model.Name,
//                Id = model.Id
//            });

//            response.Headers.Location = new Uri("http://localhost:5541/api/test/" + model.Id.ToString());

//            return ResponseMessage(response);
//        }

//        [HttpDelete]
//        public IHttpActionResult DeleteOneData(int id)
//        {
//            var entity = this.data.Ones.Find(id);

//            if (entity == null)
//            {
//                return BadRequest();
//            }

//            this.data.Ones.Delete(entity);
//            this.data.SaveChanges();

//            return Ok("Deleted item with id" + id);
//        }
//    }
//}
