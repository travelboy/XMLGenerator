using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using System.Xml; 

namespace XMLmanipulation
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //folder dialog
            folderBrowserDialog1.SelectedPath = @"G:\A&O Fase 3 (Keten)\RRPClcins\20130819v2\Mutatieverlopen GBS-ES";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblRRP.Text = folder;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = @"G:\A&O_RRP_Test_20130730\Expected results Mutatieverlopen";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblAO.Text = folder;

            }
        }

        private void btnResults_Click(object sender, EventArgs e)
        {
            string xslxAOFolder = lblAO.Text;
            string rrpFolderPath = lblRRP.Text;

            //Riskco directory
            System.IO.DirectoryInfo dirrRiskco = new System.IO.DirectoryInfo(@rrpFolderPath);

            System.IO.DirectoryInfo[] subDirsRiskco = dirrRiskco.GetDirectories();

            int comparedAssociate = 0;

            string pathExtra = lblOut.Text + "\\" + "Differences";
            DirectoryInfo dir3 = Directory.CreateDirectory(pathExtra);

            string pathExtra1 = lblOut.Text + "\\" + "Stats";
            DirectoryInfo dir13 = Directory.CreateDirectory(pathExtra1);

            string paket = "";

            string output2 = "";
            string output = "RiskCoFile,A&OFile,idnode,idnode1,difference,difRiskCo,difA&O,valueNodeID1,valueNodeID2,flag, \r\n";
            
            //feature counter
            int featureCounter=0;
            int diff = 0;
            int difFiles=0;

            int[] compared = new int[11];

            
            string IDsFolder = lblHelp.Text;

     

            System.IO.DirectoryInfo dirExcel = new System.IO.DirectoryInfo(@xslxAOFolder);
            System.IO.FileInfo[] filesExcel = dirExcel.GetFiles("*.xlsx");


            System.IO.DirectoryInfo dirTxt = new System.IO.DirectoryInfo(@IDsFolder);
            System.IO.FileInfo[] filesTxt = dirTxt.GetFiles("*.txt");

            foreach(FileInfo fexcel in filesExcel)
            {

            var existingFile = new FileInfo(fexcel.FullName);
            
            ////open the xslx file
            using (var package = new ExcelPackage(existingFile))
            {

                ExcelWorkbook workBook = package.Workbook;

                if (workBook != null)
                {

                    if (workBook.Worksheets.Count > 0)
                    {
                        //start with the worksheets
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();
                        string[] headerNames = new string[currentWorksheet.Dimension.End.Column];  

                        output2 += "Number of A&O files= " + (currentWorksheet.Dimension.End.Row - 1).ToString()+"\r\n \r\n file, numDiff \r\n" ;
                        //get the headers
                        for (int colNumber = 1; colNumber <= currentWorksheet.Dimension.End.Column; colNumber++)
                        {
                            headerNames[colNumber-1] = currentWorksheet.Cells[1, colNumber].Value.ToString();
                        }

                        //till the end of the sheet
                        for (int rowNumber = 2; rowNumber <= currentWorksheet.Dimension.End.Row; rowNumber++)
                        {

                            //personID
                            string personID = currentWorksheet.Cells[rowNumber, 1].Value.ToString();

                            //associateID
                             string associateID = "";

                 



                            foreach(FileInfo tFile in filesTxt)
                            {
                                 paket=tFile.Name.Substring(0, tFile.Name.IndexOf("."));
                                //first choose the appropriate txt ID
                                if(fexcel.Name.Contains(paket))

                                {
                                    
                            //see if this ID is usable
                                    using (System.IO.StreamReader reader = new System.IO.StreamReader(tFile.FullName))
                                    {


                                        string linePaket="";
                                        string PSNID = "";
                                        string medeverzID = "";
                                        string associationID = "unknown";

                                       


                                        //reading all lines of the file
                                        //a file may have three columns or one depending on the type of pakket
                                        while ((linePaket = reader.ReadLine()) != null)
                                        {

                                        

                                              PSNID=linePaket.Substring(0,9);

                                         //in this case get additional info
                                           if (paket == "BOP" || paket == "BPP" || paket == "PP" || paket == "WZP" || paket == "BVP" || paket == "GOP" || paket == "GVP"||paket=="TTP")
                                           {

                                               if (linePaket.IndexOf('\t') != -1)
                                               {
                                                   medeverzID = linePaket.Substring(linePaket.IndexOf('\t') + 1, linePaket.LastIndexOf('\t') - linePaket.IndexOf('\t') - 1);

                                                   associationID = linePaket.Substring(linePaket.Length - 9, 9);
                                               }

                                               if(currentWorksheet.Cells[rowNumber, 4].Value!=null)
                                                   associateID = currentWorksheet.Cells[rowNumber, 4].Value.ToString();
                                           }

                                            
                                           
                                                //then go to the next level- RRP files
                                                if (PSNID == personID&&medeverzID==associateID)
                                                {
                                                    int count = 0;
                                                //going trought the RRP files, but first find the right folder
                                                    foreach (System.IO.DirectoryInfo dirInfoR in subDirsRiskco)
                                                    {

                                                        string rrpName = "";

                                                        //needed cos of the naming convention of the rrp paket folders
                                                        switch (dirInfoR.Name)
                                                        {
                                                            case "1":
                                                                rrpName = "VP";
                                                                break;
                                                            case "1004":
                                                                rrpName = "BOP";
                                                                break;
                                                            case "1000":
                                                                rrpName = "IP";
                                                                break;
                                                            case "1001":
                                                                rrpName = "OP";
                                                                break;
                                                            case "1002":
                                                                rrpName = "PP";
                                                                break;
                                                            case "1003":
                                                                rrpName = "WZP";
                                                                break;
                                                            case "1006":
                                                                rrpName = "BPP";
                                                                break;
                                                            case "3":
                                                                rrpName = "BVP";
                                                                break;
                                                            case "1005":
                                                                rrpName = "GOP";
                                                                break;
                                                            case "1007":
                                                                rrpName = "TPP";
                                                                break;
                                                            case "9":
                                                                rrpName = "VPBS";
                                                                break;
                                                        }


                                                        //find the appropriate folder
                                                        if (rrpName == paket)
                                                        { //RRP folder
                                                        

                                                            rrpFolderPath = dirInfoR.FullName;
                                                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@rrpFolderPath);
                                                            System.IO.FileInfo[] files = dir.GetFiles("*.clcout");

                                                           // if (output2.Contains("Number of RRP files="))
                                                                output2 += "Number of RRP files= " + dir.GetFiles("*.clcout").Count().ToString();
                                                            //if (output.Contains("RiskCoFile,A&OFile,idnode"))
                                                      
                                                         

                                                            foreach (FileInfo rrpFile in files)
                                                            {
                                                                count++;
                                                                //choosing the right file
                                                                if (rrpFile.Name.Contains(PSNID))
                                                                {
                                                                    string personRRP = rrpFile.Name.Substring(0, 9);
                                                                    string associationRRP = rrpFile.Name.Substring(rrpFile.Name.IndexOf(";") + 1, rrpFile.Name.LastIndexOf(";") - rrpFile.Name.IndexOf(";") - 1);

                                                                    //compare!!!
                                                                    if (associationRRP == associationID)
                                                                    {
                                                                        comparedAssociate++;

                                                                        string filepath = rrpFile.FullName;
                                                                        XmlReaderSettings readerSettings = new XmlReaderSettings();
                                                                        XmlReader reader1 = XmlReader.Create(filepath, readerSettings);
                                                                        XmlDocument docR = new XmlDocument();
                                                                        docR.Load(reader1);

                                                                        XmlNamespaceManager menRiskco = new XmlNamespaceManager(docR.NameTable);
                                                                        menRiskco.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
                                                                        menRiskco.AddNamespace("deafult", "http://www.example.org/Mutatieverloop");


                                                                        XmlNode verloop = docR.DocumentElement.SelectSingleNode("//deafult:" + paket + "_Verloop", menRiskco).FirstChild;

                                                                        //getting to the features
                                                                        foreach (XmlNode feature in verloop.FirstChild.ChildNodes)
                                                                        {

                                                                            foreach (string xslxFeatureName in headerNames)
                                                                            {

                                                                                //we found appropriate feature for comparesment
                                                                                if (feature.Name.Equals(xslxFeatureName))
                                                                                {
                                                                                    bool help = false;
                                                                                    string valueRRP = feature.InnerText;
                                                                                    string valueAO = currentWorksheet.Cells[rowNumber, featureCounter + 1].Value.ToString();


                                                                                    //not equal values!!!
                                                                                    if (Char.IsDigit(valueAO, 0) == true && Char.IsDigit(valueRRP, 0) == true||(valueRRP.Substring(0,1).Equals('-')&&valueAO.Substring(0,1).Equals('-')))
                                                                                    {
                                                                                        string helprrr="";
                                                                                        string helpao="";
                                                                                        double vRRP = Math.Round(Convert.ToDouble(valueRRP), 0);
                                                                                        double vAO = Math.Round(Convert.ToDouble(valueAO), 0);
                                                                                        //if(valueRRP.IndexOf('.')!=-1)
                                                                                        //helprrr=valueRRP.Substring(0, valueRRP.IndexOf('.'));
                                                                                        //if (valueAO.IndexOf('.') != -1)
                                                                                        //    helpao= valueAO.Substring(0, valueAO.IndexOf('.'));
                                                                                        if (vRRP !=vAO)
                                                                                        {
                                                                                            help = true;
                                                                                            diff++;
                                                                                            string result = Path.GetFileName(lblAO.Text);
                                                                                            output += personID + "_" + associateID + "," + result + "," + paket + "_Verloop," + paket + "_Verloop," + feature.Name + "," + valueRRP + "," + valueAO+ ", , , , \r\n";


                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {

                                                                                        if (!valueRRP.Equals(valueAO) && help == false)
                                                                                        {
                                                                                            diff++;
                                                                                            string result = Path.GetFileName(lblAO.Text);
                                                                                            output += personID + "_" + associateID + "," + result + "," + paket + "_Verloop," + paket + "_Verloop," + feature.Name + "," + valueRRP + "," + valueAO + ", , , , \r\n";


                                                                                        }
                                                                                    }
                                                                           
                                                                                }

                                                                                featureCounter++;
                                                                            }
                                                                            string outputFile = paket + "_Mutatieverloop.txt";
                                                                            string bdgLinksOutput = pathExtra + "\\" + outputFile;
                                                                            //string help = lblLocation.Text + test;
                                                                            System.IO.StreamWriter ffile = new System.IO.StreamWriter(@bdgLinksOutput);
                                                                            ffile.WriteLine(output);
                                                                            ffile.Close();
                                                                            //SEE THIS
                                                                            featureCounter = 0;
                                                                        }//end comparison

                                                                        //write some info about the comparison
                                                                        if (diff != 0)
                                                                        {
                                                                            difFiles++;
                                                                            output2 += rrpFile.Name + ", " + diff.ToString() + "  \r\n";

                                                                        }

                                                                        diff = 0;
                                                                    }
                                                                }

                                                                count = 0;
                                                            }//end looping the RRP files

                                                           
                                                            double num = (double)difFiles / comparedAssociate;
                                                            output2 += "\r\n Total " + comparedAssociate.ToString() + " compared files and in " + difFiles.ToString() + " there was a difference found.\r\n"
                                                                             + "Percent of difference= " + (num * 100).ToString() + " %";

                                                            //second output
                                                            string outputFile2 = paket + "_Stats_Mutatieverloop.txt";
                                                            string bdgLinksOutput1 = pathExtra1 + "\\" + outputFile2;
                                                            //string help = lblLocation.Text + test;
                                                            System.IO.StreamWriter fffile = new System.IO.StreamWriter(@bdgLinksOutput1);
                                                            fffile.WriteLine(output2);
                                                            fffile.Close();
                                                            output = "";
                                                            output2 = "";
                                                        }//found the right rrp folder
                                                    }//end looping the RRP folders
                                                
                                                }//check excel vs txt
                                            
                                      
                                        
                                        }//end reading the appropriate txt file

                                        //we are in the last row of an excel
                                        //if (rowNumber == currentWorksheet.Dimension.End.Row)
                                        //{
                                           
                                        //}
                                

                                }//close the txt file
                         
                       }//check if txt coresponds to excel
                                   

                   }//foreach txt file



                 }//foreach personID in the excel file
              }
             }
            }//using excel file


   
           
            }//foreach Excel file

           
          
            
            }

        private void button3_Click(object sender, EventArgs e)
        {
            //folder dialog
     
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblOut.Text = folder;

            }
        }

        private void btnAsd_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = @"G:\A&O_RRP_Test_20130730\txt IDs";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblHelp.Text = folder;

            }
        }
        }
    }


