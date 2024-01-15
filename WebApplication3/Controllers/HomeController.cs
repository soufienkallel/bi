using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CubeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubeDataController : ControllerBase
    {
        private readonly string connectionString = "Provider=MSOLAP;Data Source=DESKTOP-PN53OUI;Initial Catalog=CommsCube;";

        [HttpGet("query1")]
        public ActionResult GetQuery1Data()
        {
            try
            {
                using (AdomdConnection conn = new AdomdConnection(connectionString))
                {
                    conn.Open();
                    AdomdCommand cmd = conn.CreateCommand();

                    string mdxQuery = @"
                               SELECT 
                                    [Measures].[Sent] ON COLUMNS,
                                    [Age Bands].[Age ID].[Age ID] ON ROWS
                                FROM [COMMS DB]";




                    cmd.CommandText = mdxQuery;

                    // Log the MDX query for debugging purposes
                    Console.WriteLine($"Executing MDX query: {mdxQuery}");

                    AdomdDataReader reader = cmd.ExecuteReader();

                    // Process the data and return JSON
                    var jsonData = ProcessData(reader);

                    // Return JSON data
                    return Ok(new { Data = jsonData, Message = "Query 1 data retrieved successfully" });
                }
            }
            catch (AdomdException ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error retrieving data: {ex.Message}");

                // Handle exceptions and return an appropriate response
                return BadRequest($"Error retrieving data: {ex.Message}");
            }
        }

        private void print(string mdxQuery)
        {
            throw new NotImplementedException();
        }

        private List<Dictionary<string, object>> ProcessData(AdomdDataReader reader)
        {
            var result = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    // Assuming each field is a measure, you may need to adjust accordingly
                    row[reader.GetName(i)] = reader.GetValue(i);
                }

                result.Add(row);
            }

            return result;
        }
    }
}
