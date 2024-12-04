using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using CrudOperation.Model;

namespace CrudOperation.Service
{
    public class UserService : IUserService
    {

        string jsonFile = string.Empty;

        public UserService()
        {

            jsonFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UserContact.json");
        }



        public async Task<List<UserContact>> GetResult()
        {
            var json = await File.ReadAllTextAsync(jsonFile);
            List<JToken> results = JObject.Parse(json)["UserContact"].ToList();
            List<UserContact> contactslist = new List<UserContact>();

            foreach (var item in results)
            {
                UserContact userContact = JsonConvert.DeserializeObject<UserContact>(item.ToString());
                contactslist.Add(userContact);
            }


            return contactslist;
        }

        public async Task<bool> AddResult([FromBody] UserContact userContact)
        {
            var json = await File.ReadAllTextAsync(jsonFile);
            var jsonObj = JObject.Parse(json);
            var ContactArray = jsonObj.GetValue("UserContact") as JArray;

            var AutoIncremntArray = jsonObj.GetValue("AutoIncrement") as JArray;

            if (ContactArray.Count == 0 && AutoIncremntArray.Count == 0)
            {
                userContact.Id = 1;                                                      // for 1 time User Add data 
            }
            else if (ContactArray.Count >= 0 && AutoIncremntArray.Count > 0)
            {
                var results = AutoIncremntArray.ToObject<List<AutoIncrement>>().Select(x => x).ToList();
                var lastkey = results.LastOrDefault();
                userContact.Id = lastkey.Id + 1;                                        // Auto-Increment
            }

            var newContact = "{'id':" + userContact.Id + ", 'FirstName': '" + userContact.FirstName + "',  'LastName': '" + userContact.LastName + "', 'Email' : '" + userContact.Email + "'}";
            var Incrementkey = "{'id': " + userContact.Id + "}";



            var newKey = JObject.Parse(Incrementkey);
            AutoIncremntArray.Add(newKey);

            var newAddedContact = JObject.Parse(newContact);
            ContactArray.Add(newAddedContact);

            jsonObj["UserContact"] = ContactArray;
            jsonObj["AutoIncrement"] = AutoIncremntArray;
            string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonFile, newJsonResult);

            return true;
        }



        public async Task<bool> DeleteResult(int id)
        {

            var json = await File.ReadAllTextAsync(jsonFile);
            var jsonObj = JObject.Parse(json);

            var addArrary = jsonObj.GetValue("UserContact") as JArray;
            var results = addArrary.ToObject<List<UserContact>>().Select(x => x).ToList();
            var removeItem = results.Where(x => x.Id == id).FirstOrDefault() as UserContact;
            results.Remove(removeItem);
            jsonObj["UserContact"] = JArray.FromObject(results);
            var test = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonFile, test);

            return true;
        }


        public async Task<bool> UpdateResult(int id, UserContact userContact)
        {


            var json = await File.ReadAllTextAsync(jsonFile);
            var jsonObj = JObject.Parse(json);
            var addArrary = jsonObj.GetValue("UserContact") as JArray;
            var results = addArrary.ToObject<List<UserContact>>();

            var updateRecord = results.Where((x) => x.Id == id);

            updateRecord.ToList().ForEach(x =>
            {
                x.Email = userContact.Email;
                x.FirstName = userContact.FirstName;
                x.LastName = userContact.LastName;

            });
            jsonObj["UserContact"] = JArray.FromObject(results); ;
            string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonFile, newJsonResult);
            return true;
        }
    }
}
