using FormCollectionApi.Interfaces;
using FormCollectionApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data;

namespace FormCollectionApi.Services
{
    public class FormService : IFormService
    {
        private readonly IConfiguration _config;

        public FormService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IActionResult> GetProductsList()
        {
            using SqlConnection con = new(_config.GetConnectionString("local"));
            con.Open();

            using SqlCommand command = new("procedureName", con);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("Action", SqlDbType.Int).Value = 2;

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            ArrayList products = new ();

            while (reader.Read())
            {
                products.Add(reader[1]);
            }

            return new OkObjectResult(products);

        }


        public async Task<IActionResult> SaveUserFeedBack(UserFeedBackForm feedBack)
        {
            using SqlConnection con = new(_config.GetConnectionString("local"));
            con.Open();

            using SqlCommand command = new("spLeadFormGeneration", con);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("Action", SqlDbType.Int).Value = 1;
            command.Parameters.AddWithValue("PhoneNumber", SqlDbType.NVarChar).Value = feedBack.PhoneNumber;
            command.Parameters.AddWithValue("ModeofContact", SqlDbType.NVarChar).Value = feedBack.ModeofContact.ToString();
            command.Parameters.AddWithValue("ContactPreference", SqlDbType.NVarChar).Value = feedBack.ContactPreference ? "Yes" : "No";
            command.Parameters.AddWithValue("Email", SqlDbType.NVarChar).Value = feedBack.Email;
            command.Parameters.AddWithValue("Name", SqlDbType.NVarChar).Value = feedBack.Name;
            command.Parameters.AddWithValue("Score", SqlDbType.Decimal).Value = feedBack.Score;

            string products = "";

            foreach (var item in feedBack.Product)
            {
                products += item + ", ";
            }

            products = products.Substring(0, products.Length - 3); //Trimming the last comma
            command.Parameters.AddWithValue("Product", SqlDbType.NVarChar).Value = products;

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            int retvalue = 0;
            if (reader.Read())
            {
                retvalue = reader.GetInt32("RetValue");
            }

            return retvalue == 1 ? new OkObjectResult(new { message = "Successfully Added the product" })
                : new BadRequestObjectResult(new { message = "Adding product failed" });
        }
    }
}
