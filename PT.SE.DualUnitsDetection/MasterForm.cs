using System;
using System.Linq;
using Teamcenter.Services.Strong.Core;
using Teamcenter.Services.Strong.Core._2008_06.DataManagement;
using Teamcenter.Services.Strong.Core._2010_09.DataManagement;
using Teamcenter.Soa.Client.Model;
using Teamcenter.Soa.Common;
using static LanguageExt.Prelude;

namespace TcSOAMasterForm
{
    public static class MasterForm
    {
        public static Tuple<DataManagementService, ModelObject> GetMasterItemRevById(this DataManagementService service, Teamcenter.Soa.Client.Connection connection, string id, string revId)
        {

            // Set Policy in connection for Item Revision.
            ObjectPropertyPolicy policy = new ObjectPropertyPolicy();

            PolicyType itemRevisionPolicyType = new PolicyType("ItemRevision", new string[] { "item_id", "item_revision_id", "IMAN_master_form_rev" });
            itemRevisionPolicyType.SetModifier(PolicyProperty.WITH_PROPERTIES, true);
            policy.AddType(itemRevisionPolicyType);

            SessionService.getService(connection).SetObjectPropertyPolicy(policy);


            var getItemResponse = service.GetItemAndRelatedObjects(new GetItemAndRelatedObjectsInfo[] {
                    new GetItemAndRelatedObjectsInfo{
                        ItemInfo = new ItemInfo{ Ids = new AttrInfo[]{ new AttrInfo{ Name = "item_id", Value = id }}},
                        RevInfo = new RevInfo{ Id = revId, Processing = "Ids", UseIdFirst = true },
                        DatasetInfo = new DatasetInfo{ Filter = new DatasetFilter{ Processing = "None" } }
                }});

            var itemRevOutput = getItemResponse.Output.Single().ItemRevOutput;
            var itemRevision = itemRevOutput.Single().ItemRevision;
            var masterFormRev = itemRevision.IMAN_master_form_rev[0];

            return Tuple(service, masterFormRev);

        }


        public static Tuple<DataManagementService, ModelObject> UpdateFormProperty(this Tuple<DataManagementService, ModelObject> t ,  string Key, string Value)
        {
            var service = t.Item1;
            service.SetProperties(
                new PropInfo[]{
                   new PropInfo
                   {
                        Object = t.Item2,
                        VecNameVal = new NameValueStruct1[]
                        {
                             new NameValueStruct1
                             {
                                 Name = Key,
                                 Values = new string[] { Value }
                             }
                        }
                   }
                },
                new string[] { }
            );

            return t;
        }
    }
}
