using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Helpers;
using J2BIOverseasOps.Models;
using RestSharp;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs
{
    [Binding]
    public sealed class DeleteForm
    {
        private IRestResponse<List<GetForms>> _responseFormData;

        [Given(@"I delete all but following forms:")]
        public void GivenIDeleteAllButFollowingForms(Table table)
        {
            var section = (NameValueCollection)ConfigurationManager.GetSection("ApiEndPoints");

            var request = new RestRequest(Method.GET) {Resource = section["get_all_forms"]};
            //TODO Replace following
            var tempBaseUrl =
                new Uri(
                    "http://j2mntosint01.dgtest.local:19081/Jet2.OverseasOps.SystemAdministration.Api.sfApp/Jet2.OverseasOps.SystemAdministration.Api.sfHost/api");
            _responseFormData = RestHelpers.Execute<GetForms>(request, tempBaseUrl);

            var formIdToDelet = new List<string>();
            foreach (var row in table.Rows)
            {
                var ignoreList = row["ignore_form_list"].ConvertStringIntoList();
                foreach (var form in _responseFormData.Data)
                {
                    if (ignoreList.Contains(form.Id)) continue;
                    formIdToDelet.Add(form.Id);
                }
            }

            foreach (var id in formIdToDelet)
            {
                var req = new RestRequest(Method.DELETE)
                {
                    Resource = section["delete_form"].Replace("form_id", id)
                };
                RestHelpers.Execute(req, tempBaseUrl);
            }
        }


    }
}