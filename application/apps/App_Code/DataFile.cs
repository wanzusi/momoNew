using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;

    public class DataFile
    {
        ArrayList fileContents;
        private DataLogin dh;
        public DataFile()
        {

        }
        public void writeToFile(string fileUrl, ArrayList contentToWrite)
        {
            FileStream fs2 = new FileStream(@fileUrl, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs2);
            if (contentToWrite.Count == 0)
            {
                sw.Close();
                createFile(fileUrl);
            }
            else
            {
                for (int i = 0; i < contentToWrite.Count; i++)
                {
                    sw.WriteLine(contentToWrite[i].ToString());
                }
                sw.Close();
            }
        }
        public ArrayList readFile(string fileUrl)
        {
            fileContents = new ArrayList();
            try
            {

                if (File.Exists(@fileUrl))
                {
                    TextReader tr = new StreamReader(fileUrl);
                    String line = null;
                    fileContents = new ArrayList();
                    while ((line = tr.ReadLine()) != null)
                    {
                        fileContents.Add(line);
                    }
                    tr.Close();
                }
                else
                {
                    createFile(fileUrl);
                    TextReader tr1 = new StreamReader(fileUrl);
                    String line = null;
                    fileContents = new ArrayList();
                    while ((line = tr1.ReadLine()) != null)
                    {
                        fileContents.Add(line);
                    }
                    tr1.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fileContents;
        }
        public void createFile(string fileUrl)
        {
            try
            {
                FileStream fs = new FileStream(@fileUrl, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void renameFile(string sourceUrl, string destinationUrl)
        {
            FileInfo fi = new FileInfo(sourceUrl);
            if (File.Exists(sourceUrl))
            {
                try
                {
                    if (File.Exists(destinationUrl))
                    {
                        File.Delete(destinationUrl);
                        File.Copy(sourceUrl, destinationUrl);
                        File.Delete(sourceUrl);
                    }
                    else
                    {
                        File.Copy(sourceUrl, destinationUrl);
                        File.Delete(sourceUrl);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void deleteFile(string Url)
        {
            FileInfo fi = new FileInfo(Url);
            if (File.Exists(Url))
            {
                try
                {
                    File.Delete(Url);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public string ValidateColumNames(string filePath, string extension)
        {
            string result = "";
            string wrongCols = "";
            string correctCols = "";
            OleDbConnection oledbConn;
            try
            {
                /* connection string  to work with excel file. HDR=Yes - indicates 
                   that the first row contains columnnames, not data. HDR=No - indicates 
                   the opposite. "IMEX=1;" tells the driver to always read "intermixed" 
                   (numbers, dates, strings etc) data columns as text. 
                Note that this option might affect excel sheet write access negative. */
                if (extension.ToUpper().Equals(".XLSX"))
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                      filePath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                }
                else
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.8.0;Data Source=" +
                      filePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';");
                }
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand(); ;
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();

                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                DataTable dt1 = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                String[] excelSheets = new String[dt1.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt1.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                string tableName = excelSheets[0].ToString();
                cmd.CommandText = "SELECT * FROM [" + tableName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "dsSlno");
                DataTable dt = ds.Tables["dsSlno"];
                ArrayList columns = new ArrayList();
                dh = new DataLogin();
                ArrayList excelColumns = dh.GetExcelColNames();
                if (dt.Columns.Count != excelColumns.Count)
                {
                    result = "THE NUMBER OF FIELDS IN YOUR EXCEL FILE SHOULD BE "+excelColumns.Count;
                }
                else
                {
                    ArrayList failedCols = new ArrayList();
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (!excelColumns.Contains(col.ColumnName.ToUpper().Trim()))
                        {
                            failedCols.Add(col.ColumnName);
                            wrongCols = wrongCols + col.ColumnName + ",";
                        }
                    }
                    if (failedCols.Count > 0)
                    {
                        foreach (string s in excelColumns)
                        {
                            correctCols = correctCols + s + ",";
                        }
                        result = "THE FOLLOWING FIELDS IN YOUR EXCEL HAVE INVALID NAMES:" + wrongCols + ". THE CORRECT FIELD NAMES SHOULD BE: " + correctCols;
                    }
                    else
                    {
                        result = "YES";
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public ArrayList readExcelFile(string filePath, string extension)
        {
            ArrayList result = new ArrayList();
            OleDbConnection oledbConn;
            try
            {
                dh = new DataLogin();
                if (extension.ToUpper().Equals(".XLSX"))
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                      filePath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                }
                else
                {
                    oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.8.0;Data Source=" +
                      filePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;';");
                }
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand(); ;
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();

                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                DataTable dt1 = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                String[] excelSheets = new String[dt1.Rows.Count];
                int i = 0;

                // Add the sheet name to the string array.
                foreach (DataRow row in dt1.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }
                string tableName = excelSheets[0].ToString();
                ArrayList excelColumns = dh.GetExcelColNames();
                string cols = "";
                int intCols = excelColumns.Count;
                for (int p = 0; p < intCols; p++)
                {
                    if (p == intCols - 1)
                    {
                        cols = cols + excelColumns[p].ToString().Replace(",","");
                    }
                    else
                    {
                        cols = cols + excelColumns[p].ToString().Replace(",", "") + ",";
                    }
                }
                cmd.CommandText = "SELECT "+cols+" FROM [" + tableName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "dsSlno");
                DataTable dt = ds.Tables["dsSlno"];
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    string h = "";
                    for (int j = 0; j < excelColumns.Count; j++)
                    {
                        if (j == excelColumns.Count - 1)
                        {
                            h = h + dt.Rows[k][j].ToString().Replace(",", " ");
                        }
                        else
                        {
                            h = h + dt.Rows[k][j].ToString().Replace(",", " ") + ",";
                        }
                    }
                    result.Add(h);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

    }