using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TestProject.BusinessProcess
{
   
    public class GlobalFunction
    {
        public static DataTable ConvertToDataTable(IEnumerable<XElement> data)
        {
            var table = new DataTable();
            // create the columns
            foreach (var xe in data.First().Descendants())
                if (!table.Columns.Contains(xe.Name.LocalName))
                {
                    table.Columns.Add(xe.Name.LocalName, typeof(string));
                }
            // fill the data
            foreach (var item in data)
            {
                var row = table.NewRow();
                foreach (var xe in item.Descendants())
                    row[xe.Name.LocalName] = xe.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        public string LoadStatus(string filetype, string status)
        {
            try
            {
                if (filetype == "csv" && status != "")
                {
                    if (status == "Approved")
                    {
                        return "A";
                    }
                    else if (status == "Failed")
                    {
                        return "R";
                    }
                    else if (status == "Finished")
                    {
                        return "D";
                    }
                    else
                    {
                        throw new Exception("status doesn't be matched.");
                    }
                }
                else if (filetype == "xml" && status != "")
                {
                    if (status == "Approved")
                    {
                        return "A";
                    }
                    else if (status == "Rejected")
                    {
                        return "R";
                    }
                    else if (status == "Done")
                    {
                        return "D";
                    }
                    else
                    {
                        throw new Exception("status doesn't be matched.");
                    }
                }
                else
                {
                    throw new Exception("Unknown format.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
  

    }
}