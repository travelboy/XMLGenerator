using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using OfficeOpenXml;
using System.IO;

namespace XMLmanipulation
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = @"G:\A&O_RRP_Test_20130813\Book3.xlsx";
            string output = "";
            int rowcount = 1;
            bool firstcase=false;
            ////RRP folder
            //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@rrpFolderPath);

            //// System.IO.DirectoryInfo dirrRiskco = new System.IO.DirectoryInfo(@riskcoFolder);

            //System.IO.DirectoryInfo[] subDirsRiskco = dir.GetDirectories();

           // foreach (System.IO.DirectoryInfo dirInfoR in subDirsRiskco)
            //{

            var existingFile = new FileInfo(filePath);
            using (var package = new ExcelPackage(existingFile))
            {
                string rrpName = comboBox1.SelectedItem.ToString();

                ExcelWorksheet currentWorksheet = package.Workbook.Worksheets.First();
                //switch (dirInfoR.Name)
                //{
                //    case "1":
                //        rrpName = "vp";
                //        break;
                //    case "1004":
                //        rrpName = "bop";
                //        break;
                //    case "1000":
                //        rrpName = "ip";
                //        break;
                //    case "1001":
                //        rrpName = "op";
                //        break;
                //    case "1002":
                //        rrpName = "pp";
                //        break;
                //    case "1003":
                //        rrpName = "wzp";
                //        break;
                //    case "1006":
                //        rrpName = "bpp";
                //        break;
                //    case "3":
                //        rrpName = "bvp";
                //        break;
                //    case "1005":
                //        rrpName = "gop";
                //        break;
                //    case "1007":
                //        rrpName = "tpp";
                //        break;
                //    case "9":
                //        rrpName = "vpbs";
                //        break;
                //}

              //  System.IO.FileInfo[] files = dirInfoR.GetFiles("*.clcout");
                //pakket type
                string pakket = rrpName;
                //string array of column headers
                string[] headers = { };
                //creation of the excel file
                string fileExcel = lblOut.Text+"\\"+"allPersonIDs_" + pakket + ".xlsx";
                var excel = new ExcelPackage(new FileInfo(fileExcel));
                var ws = excel.Workbook.Worksheets.Add("sghngfnfygfefeh");
                ws.Cells[1, 1].Value = "personID";
                ws.Cells[1, 2].Value = "EINDE_VERWERKING_DATUM";
                ws.Cells[1, 3].Value = "REKENDATUM";

                ws.Cells[1, 4].Value = "GEBOORTEJAAR_BEDRIJFSGENOOT";
                ws.Cells[1, 5].Value = "GESLACHT_BEDRIJFSGENOOT";
                ws.Cells[1, 6].Value = "STATUS";

                using (excel)
                {

                int checkcount = 0;
                string tablename = "standen_" + pakket + "_aga_sag_07022013_02_14_48";

                string database = "";
                
                  if (pakket == "op" || pakket == "ip" || pakket == "bop" || pakket == "bpp" || pakket == "bvp" || pakket == "gop" || pakket == "gvp")
                     {
                
                    database = "AenO_MutatieVerlopen_1";
                  }

                 else
                     {
               
                    database = "AenO_MutatieVerlopen_2";
              
                      }

                string connection = @"Data Source=wikic\sqlexpress;Initial Catalog=" + database + ";Integrated Security=False;User ID=riskco;Password=suiron";

                int count = -1;
                string[] allids = new string[10000];

                int filecount = 0;
                      int a=0;

                   //looping trought the files of a folder
                    //looping trough the IDS

                //foreach (System.IO.FileInfo f in files)
                    for(int i=1;i<=currentWorksheet.Dimension.End.Row;i++)
                {
                    a++;

                    if(a==1)
                        firstcase=true;
                    else
                        firstcase=false;
                     




                    count++;

                    string personID = currentWorksheet.Cells[i,1].Value.ToString();
                    allids[count] = personID;

                    foreach (string s in allids)
                    {

                        if (s == null) { break; }
                        else if (s.Equals(personID))
                        {
                            filecount++;

                        }
                    }

                    //in this case write in the file
                    if (filecount == 1)
                    {
                        //string path = f.FullName;


                        rowcount++;
                        //XmlReaderSettings readerSettings = new XmlReaderSettings();

                        //readerSettings.IgnoreComments = true;

                        //XmlReader reader1 = XmlReader.Create(@path, readerSettings);
                        //XmlDocument xmlRiskco = new XmlDocument();
                        //xmlRiskco.Load(reader1);

                        //XmlNamespaceManager menRiskco = new XmlNamespaceManager(xmlRiskco.NameTable);
                        //menRiskco.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
                        //menRiskco.AddNamespace("deafult", "http://www.example.org/Bedrijfsgenoot_BPFSAG");


                        using (SqlConnection con = new SqlConnection(connection))
                        {
                            checkcount++;
                            string query = "";


                            con.Open();

                            if (pakket == "op" || pakket == "ip" || pakket == "vp" || pakket == "vpbs")
                            {
                                query = "select [PSN_ID_BEDRIJFSGENOOT],[EINDE_VERWERKING_DATUM],[REKENDATUM],[EINDBEREKENING],[WAARDE],[GEBOORTEJAAR_BEDRIJFSGENOOT],[GESLACHT_BEDRIJFSGENOOT],[STATUS] from " + tablename +
                         " where PSN_ID_BEDRIJFSGENOOT=" + personID + "and EINDE_VERWERKING_DATUM='22-01-2012' and REKENDATUM='31-12-2011'";
                            }

                            else
                            {

                                query = "select [PSN_ID_BEDRIJFSGENOOT],[EINDE_VERWERKING_DATUM],[REKENDATUM],[EINDBEREKENING],[WAARDE],[GEBOORTEJAAR_BEDRIJFSGENOOT],[GESLACHT_BEDRIJFSGENOOT],[STATUS],[PSN_ID_PARTNER],[GEBOORTEJAAR_PARTNER],[GESLACHT_PARTNER] from " + tablename +
                                                   " where PSN_ID_BEDRIJFSGENOOT=" + personID + "and EINDE_VERWERKING_DATUM='22-01-2012' and REKENDATUM='31-12-2011'";
                            }


                            SqlCommand cmd = new SqlCommand(query, con);
                            SqlDataReader rdr = null;
                            rdr = cmd.ExecuteReader();

                            pakket = pakket.ToUpper();



                       
                            if (pakket == "OP" || pakket == "IP" || pakket == "VP" || pakket == "VPBS")
                            {

                                int featurecount = 6;
                                
                                    while (rdr.Read())
                                    {
                                        featurecount++;

                                      
                                        string birthdateAO = "";
                                        string sexPersonAO = "";
                                        string statusAO = "";

                                        string endProcessing = rdr["EINDE_VERWERKING_DATUM"].ToString();
                                        string calcDate = rdr["REKENDATUM"].ToString();
                                        string featureAO = rdr["EINDBEREKENING"].ToString();
                                        string valueAO = rdr["WAARDE"].ToString();
                                        if (rdr["GEBOORTEJAAR_BEDRIJFSGENOOT"] != null)
                                            birthdateAO = rdr["GEBOORTEJAAR_BEDRIJFSGENOOT"].ToString();

                                        if (rdr["GESLACHT_BEDRIJFSGENOOT"] != null)
                                            sexPersonAO = rdr["GESLACHT_BEDRIJFSGENOOT"].ToString();

                                        if (rdr["STATUS"] != null)
                                            statusAO = rdr["STATUS"].ToString();
                                      


                                        
                                        if (ws.Cells[rowcount, 1].Value == null)
                                        ws.Cells[rowcount, 1].Value = personID;
                                        if (ws.Cells[rowcount, 2].Value == null)
                                        ws.Cells[rowcount, 2].Value = endProcessing;
                                        if (ws.Cells[rowcount, 3].Value == null)
                                            ws.Cells[rowcount, 3].Value = calcDate;
                                        if (ws.Cells[rowcount, 4].Value == null)
                                            ws.Cells[rowcount, 4].Value = birthdateAO;
                                        if (ws.Cells[rowcount, 5].Value == null)
                                        ws.Cells[rowcount, 5].Value = sexPersonAO;
                                        if (ws.Cells[rowcount, 6].Value == null)
                                        ws.Cells[rowcount, 6].Value = statusAO;

                                        if (firstcase)
                                        {
                                            ws.Cells[1, featurecount].Value = featureAO;
                                            ws.Cells[rowcount, featurecount].Value = valueAO;
                                        }


                                        if (!firstcase)
                                        {

                                            int c = 0;
                                            foreach (string header in headers)
                                            {

                                                if (featureAO == header && firstcase == false)
                                                {
                                                    ws.Cells[rowcount, c+1].Value = valueAO;
                                                    headers[c] = "new";
                                                }


                                                c++;
                                            }
                                            c = 0;
                                        }

                                    }
                        
                            
                                }

                            

                            else
                            {
                                bool secondchance = false;
                                ws.Cells[1, 7].Value = "PSN_ID_PARTNER";
                                ws.Cells[1, 8].Value = "GEBOORTEJAAR_PARTNER";
                                ws.Cells[1, 9].Value = "GESLACHT_PARTNER";

                                int featurecount = 9;
                               
                                    while (rdr.Read())
                                    {
                                      featurecount++;


                                        string endProcessing = rdr["EINDE_VERWERKING_DATUM"].ToString();
                                        string calcDate = rdr["REKENDATUM"].ToString();
                                        string featureAO = rdr["EINDBEREKENING"].ToString();
                                        string valueAO = rdr["WAARDE"].ToString();

                                        string yearPersonID = rdr["GEBOORTEJAAR_BEDRIJFSGENOOT"].ToString();
                                        string sexPersonID = rdr["GESLACHT_BEDRIJFSGENOOT"].ToString();
                                        string statusPersonID = rdr["STATUS"].ToString();
                                        string person2ID = rdr["PSN_ID_PARTNER"].ToString();
                                        string yearperson2ID = rdr["GEBOORTEJAAR_PARTNER"].ToString();
                                        string sexperson2ID = rdr["GESLACHT_PARTNER"].ToString();

                                        if (!(ws.Cells[rowcount, 7].Value == null))
                                            if (ws.Cells[rowcount, 7].Value.ToString() != person2ID)
                                            {
                                                rowcount++;
                                                secondchance = true;
                                            }
                                             
                                            
                                        


                                        if (ws.Cells[rowcount, 1].Value == null)
                                            ws.Cells[rowcount, 1].Value = personID;

                                        if (ws.Cells[rowcount, 2].Value == null)
                                            ws.Cells[rowcount, 2].Value = endProcessing;

                                        if (ws.Cells[rowcount, 3].Value == null)
                                            ws.Cells[rowcount, 3].Value = calcDate;

                                        if (ws.Cells[rowcount, 4].Value == null)
                                            ws.Cells[rowcount, 4].Value = yearPersonID;

                                        if (ws.Cells[rowcount, 5].Value == null)
                                            ws.Cells[rowcount, 5].Value = sexPersonID;

                                        if (ws.Cells[rowcount, 6].Value == null)
                                            ws.Cells[rowcount, 6].Value = statusPersonID;

                                        if (ws.Cells[rowcount, 7].Value == null)
                                            ws.Cells[rowcount, 7].Value = person2ID;

                                        if (ws.Cells[rowcount, 8].Value == null)
                                            ws.Cells[rowcount, 8].Value = yearperson2ID;

                                        if (ws.Cells[rowcount, 9].Value == null)
                                            ws.Cells[rowcount, 9].Value = sexperson2ID;

                                      
                                       
                                        if (firstcase)
                                        {
                                            ws.Cells[1, featurecount].Value = featureAO;
                                            ws.Cells[rowcount, featurecount].Value = valueAO;
                                        }

                                        if (!firstcase)
                                        {
                                            int c = 0;

                                            foreach (string header in headers)
                                            {

                                                if ((featureAO == header && firstcase == false )|| (featureAO == header && secondchance == true))
                                                {
                                                    ws.Cells[rowcount, c+1].Value = valueAO;
                                                    headers[c] = "new";
                                                }
                                                c++;


                                            }
                                            c = 0;
                                        }
                        
                            

                                }



                            }
                            //end of writing to file

                            // }

                        }//close the connection


                    }//stop file analysis
                   
                    
                    headers=GetHeaderColumns(ws);


                    filecount = 0;
                    pakket = pakket.ToLower();

                }//end of files check
                a = 0;
                excel.Save();
                   
                }//end using excel



                //finish looping trought the RRP files
                //output += checkcount.ToString();
                //string outputFile = comboBox1.SelectedItem.ToString() + "_BS_CLCOUT.txt";
                //string outt = lblOut.Text + "\\" + outputFile;
                ////string help = lblLocation.Text + test;
                //System.IO.StreamWriter ffile = new System.IO.StreamWriter(@outt);
                //ffile.WriteLine(output);
                //ffile.Close();


            }//end directory looping
        }
        private void button1_Click(object sender, EventArgs e)
        {

            //folder dialog
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblRRP.Text = folder;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblOut.Text = folder;

            }
        }
       
        public string[] GetHeaderColumns(ExcelWorksheet sheet)
            {
                List<string> columnNames = new List<string>();
                foreach (var firstRowCell in sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, 1, sheet.Dimension.End.Column])
                    columnNames.Add(firstRowCell.Text);
                return columnNames.ToArray();
            }
        
    }
}
