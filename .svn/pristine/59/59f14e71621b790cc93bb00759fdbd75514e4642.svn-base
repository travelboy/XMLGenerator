using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using OfficeOpenXml;

namespace XMLmanipulation
{
    //The same form can be used for all of the CLCOUT files
    public partial class Form3 : Form
    {
        Form1 form1 = new Form1();

        public Form3()
        {
          
            InitializeComponent();
         
        }

        private void btnOutputO_Click(object sender, EventArgs e)
        {

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath; ;
                lblOutput.Text = folder;
            }
        }

        private void btnRiskCoOut_Click(object sender, EventArgs e)
        {

            folderBrowserDialog1.SelectedPath = @"G:\A&O Fase 3 (Keten)\RRPClcins\20130819v2";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblRiskcoOut.Text = folder;

            }
        }

        private void btnAOOut_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = @"G:\A&O_RRP_Test_20130730\915 v3 in_out";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblAOOut.Text = folder;

            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //the root folders
            string riskcoFolder = lblRiskcoOut.Text;
           // string riskcoFolder = @"\\PC14TEMP\\c$\temp\4\ES";
            string aoFolder = lblAOOut.Text;

        

            int[] numRRP = new int[12];
            int[] numAO = new int[12];
            string[] pakket = new string[12];
            int[] good = new int[12];
            int[] bad=new int[12];
            double[] percent = new double[12];
            int[] compared = new int[12];
            int countPak = 0;


            //match the appropriate directories

            //AO clcout files
            System.IO.DirectoryInfo dirr = new System.IO.DirectoryInfo(@aoFolder);
            System.IO.DirectoryInfo[] subDirsAO = dirr.GetDirectories();
   

            //Riskco clcout files
            System.IO.DirectoryInfo dirrRiskco = new System.IO.DirectoryInfo(@riskcoFolder);
            System.IO.DirectoryInfo[] subDirsRiskco = dirrRiskco.GetDirectories();
            int numDir=dirrRiskco.GetDirectories().Count();
            //the final excel file
            string mode=dirrRiskco.Name;
            string fileExcel = lblOutput.Text + "\\CLCOUTResults"+mode+".xlsx";
            var excel = new ExcelPackage(new FileInfo(fileExcel));
            var ws = excel.Workbook.Worksheets.Add("Sheet1");

            //looping through the subdirectories
            //each subdirectory in RRP
            foreach (System.IO.DirectoryInfo dirInfoR in subDirsRiskco)
            {


               
                foreach (System.IO.DirectoryInfo dirInfoAO in subDirsAO) { 
                                //we are in the right directories
                    string rrpName = "";


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


                    if (rrpName == dirInfoAO.Name)
                    {
                        string forPakket = dirInfoAO.Name;
                        countPak++;
                        ////check if subfolder usefulCLCOUT exists
                        //if (!(dirInfoR.GetDirectories("usefulCLCOUT") == null))
                        //{
                        //     System.IO.DirectoryInfo[] usefulR= dirInfoR.GetDirectories("usefulCLCOUT");
                        //     riskcoFolder = usefulR[0].FullName;
                        //}
                        //else {
                        //    riskcoFolder = dirInfoR.FullName;
                        //}


                        //if (!(dirInfoAO.GetDirectories("usefulCLCOUT") == null))
                        //{
                        //      System.IO.DirectoryInfo[] usefulA= dirInfoAO.GetDirectories("usefulCLCOUT");
                        //    if(!(usefulA[0]==null))
                        //      aoFolder = usefulA[0].FullName;
                        //    else
                        //        aoFolder = dirInfoAO.FullName;
                        //}
                        //else {
                        //    aoFolder = dirInfoAO.FullName;
                        
                        //}
                        riskcoFolder = dirInfoR.FullName;
                        aoFolder = dirInfoAO.FullName;
                        //AO clcout files
                        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@aoFolder);
                        System.IO.FileInfo[] filesOUT = dir.GetFiles("*.clcout");

                        //Riskco clcout files
                        System.IO.DirectoryInfo dirRiskco = new System.IO.DirectoryInfo(@riskcoFolder);
                        System.IO.FileInfo[] filesRiskco = dirRiskco.GetFiles("*.clcout");

                        //// see if the useful folder is already created or not
                        //string s = aoFolder.Substring(aoFolder.Length - 12, 12);
                        //string ss = riskcoFolder.Substring(riskcoFolder.Length - 12, 12);

                        ////clean the folders
                        //if (ss != "usefulCLCOUT")
                        //{
                        //    //the new folders 
                        //    string rrppath = riskcoFolder + "\\" + "usefulCLCOUT";
                        //    DirectoryInfo dir11 = Directory.CreateDirectory(rrppath);

                        //    string aopath = aoFolder + "\\" + "usefulCLCOUT";
                        //    DirectoryInfo dir12 = Directory.CreateDirectory(aopath);




                        //    //count the riskco files
                        //    int numriskcof = dirRiskco.GetFiles("*.clcout").Count();

                        //    foreach (System.IO.FileInfo fa in filesOUT)
                        //    {
                        //        //get the first  ID
                        //        string firstIDAO = fa.Name.Substring(0, 9);

                        //        //get the second ID
                        //        int starti = fa.Name.IndexOf(";");
                        //        int endi = fa.Name.LastIndexOf(";");
                        //        string secondIDAO = fa.Name.Substring(starti + 1, endi - starti - 1);


                        //        foreach (System.IO.FileInfo fr in filesRiskco)
                        //        {

                        //            //get the first  ID
                        //            string firstIDRiskco = fr.Name.Substring(0, 9);

                        //            //get the second ID
                        //            int startir = fr.Name.IndexOf(";");
                        //            int endir = fr.Name.LastIndexOf(";");
                        //            string secondIDRiskco = fr.Name.Substring(startir + 1, endir - startir - 1);

                        //            //file A&O has coresponding RRP file
                        //            if ((firstIDAO.Equals(firstIDRiskco) && secondIDAO.Equals(secondIDRiskco)))
                        //            {
                        //                File.Move(fr.FullName, rrppath + "\\" + fr.Name);
                        //                File.Move(fa.FullName, aopath + "\\" + fa.Name);

                        //            }
                        //        }
                        //    }
                        //    riskcoFolder = rrppath;
                        //    aoFolder = aopath;

                        //    dir = new System.IO.DirectoryInfo(@aoFolder);
                        //    filesOUT = dir.GetFiles("*.clcout");

                        //    dirRiskco = new System.IO.DirectoryInfo(@riskcoFolder);
                        //    filesRiskco = dirRiskco.GetFiles("*.clcout");
                        //}

                        //create the appropriate folder for a pakket results
                        string folder = forPakket + mode+"_CLCOUT";
                        string helppath = lblOutput.Text + "\\" + folder;
                        DirectoryInfo dir33 = Directory.CreateDirectory(helppath);

                        string pathExtra = lblOutput.Text + "\\" +folder+ "\\"+"extraFiles";
                        DirectoryInfo dir3 = Directory.CreateDirectory(pathExtra);

                        int countDiff = 0;
                        int countComp = 0;

                        string noDifferencesID = "";
                        string yesDifferencesID = "";
                        string idPerson = "fileRiskco, fileA&O, PersonID \r\n";
                        string tablename = "";
                        string aoFolderInput = lblAOClcIn.Text;

                        string details = "Number of RRP Files=" + filesRiskco.Count() + "Number of AO Files=" + filesOUT.Count() + "\r\n fileRRP ; fileAO ; numberOfDifferences \r\n";
                        numRRP[countPak] = filesRiskco.Count();
                        numAO[countPak] = filesOUT.Count();
                        pakket[countPak] = forPakket;
                        //we know the number of clcin files in A&0
                        int count = dir.GetFiles("*.clcout").Count();

                        //System.IO.FileInfo[] filesIN = dirIn.GetFiles("*.clcin");


                        string bigger = "";
                        ///////TEST
                        foreach (System.IO.FileInfo f in filesOUT)
                        {
                            string fileAO = f.Name;
                            string path = f.FullName;

                            // string fileAO = "Case " + i.ToString() + ".clcin";
                            XmlReaderSettings readerSettings = new XmlReaderSettings();

                            XmlReader reader = XmlReader.Create(@path, readerSettings);
                            XmlDocument doc1 = new XmlDocument();
                            doc1.Load(reader);



                            XmlNamespaceManager menAO = new XmlNamespaceManager(doc1.NameTable);
                            menAO.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
                            menAO.AddNamespace("df", "Deployment:Bedrijfsgenoot_BPFSAG");
                            //tablename = doc1.DocumentElement.SelectSingleNode("clc:DeplR", menAO).Attributes["dep-name"].Value;

                            ////needed to get the file from the Riskco folder
                            //string personID = doc1.DocumentElement.SelectSingleNode(".//clc:CalculationData", menAO).FirstChild.Value;
                            //String firstIDAO = new String(personID.Where(Char.IsDigit).ToArray());
                            string fileRiskco = "";
                            string partnerIDAOFile = "";
                            int starti = f.Name.IndexOf(";");
                            int endi = f.Name.LastIndexOf(";");
                            partnerIDAOFile = f.Name.Substring(starti + 1, endi - starti - 1);



                            //the file to compare with
                            string firstIDAO = fileAO.Substring(0, 9);

                            string pathR = riskcoFolder + "\\";
                            bool exist = false;



                            //checking if there is a file in the riskco folder with the id got from a&o
                            foreach (System.IO.FileInfo fr in filesRiskco)
                            {
                                //the first 10 letters of the riskco filename
                                string frisk = fr.Name.Substring(0, 9);

                                int startir = fr.Name.IndexOf(";");
                                int endir = fr.Name.LastIndexOf(";");
                                string partnerIDRFile = fr.Name.Substring(startir + 1, endir - startir - 1);



                                if (frisk.Equals(firstIDAO) && partnerIDRFile.Equals(partnerIDAOFile))
                                {
                                    fileRiskco = fr.Name;
                                    exist = true;
                                }



                            }


                            if (exist)
                            {
                                countComp++;
                                idPerson += fileRiskco + ", " + fileAO + ", " + firstIDAO + "\r\n";
                                string x = fileRiskco + "," + fileAO + ",";



                                //do the comparison here
                                string helpstring = PerformClick(pathR + "//" + fileRiskco, path, x,forPakket,helppath);

                                int numberOfLines = form1.CountLinesInFile(helpstring);

                                if (helpstring.Contains("value"))
                                    details += fileRiskco + " ; " + fileAO + " ; " + (numberOfLines - 1).ToString() + "\r\n";

                                if (numberOfLines == 1)
                                {
                                    //no differences found in the current comparison
                                    noDifferencesID += firstIDAO + "\r\n";
                                }

                                else
                                {
                                    countDiff++;
                                    yesDifferencesID += firstIDAO + "\r\n";
                                    bigger += helpstring;
                                }


                                fileAO = "";
                                fileRiskco = "";
                            }




                        }

                        string outputFileName = "all_the_differences";

                        string help = lblOutput.Text + "\\" + folder + "\\" + outputFileName + ".txt";
                        //string help = lblLocation.Text + test;
                        System.IO.StreamWriter file = new System.IO.StreamWriter(@help);



                        file.WriteLine(bigger);
                        file.Close();
                        double num = (double)countDiff / countComp;
                        details += "Files Compared= " + countComp.ToString() + "; Files Difference=" + countDiff.ToString() + "Files OK= " + (countComp - countDiff).ToString() + " Percent of Sucess= " + (100 - num * 100).ToString() + "%";

                        good[countPak] = countComp - countDiff;
                        bad[countPak] = countDiff;
                        percent[countPak] = 100 - num * 100;
                        compared[countPak] = countComp;
                        
                        
                        string difffID = pathExtra + "\\" + "DETAILS.txt";
                        System.IO.StreamWriter ffile5 = new System.IO.StreamWriter(@difffID);
                        ffile5.WriteLine(details);
                        ffile5.Close();


                        if (!yesDifferencesID.Equals(""))
                        {
                            //the IDs of persons when difference noticed
                            string diffID = pathExtra + "\\" + "differencePersonIDs.txt";
                            System.IO.StreamWriter file5 = new System.IO.StreamWriter(@diffID);
                            file5.WriteLine(yesDifferencesID);
                            file5.Close();
                        }

                        string allDiffID = pathExtra + "\\" + "allIDS.txt";
                        System.IO.StreamWriter file3 = new System.IO.StreamWriter(@allDiffID);
                        file3.WriteLine(idPerson);
                        file3.Close();

                        if (!noDifferencesID.Equals(""))
                        { //IDs of persons that had no differences
                            string nodiffID = pathExtra + "\\" + "NOdifferencePersonIDs.txt";
                            System.IO.StreamWriter file6 = new System.IO.StreamWriter(@nodiffID);
                            file6.WriteLine(noDifferencesID);
                            file6.Close();
                        }

                        form1.outputFileGenerator(bigger, 3, helppath);
                    
                    
                    }
                }
            
            }
            //write the excel table
            using (excel)
            {
                ws.Cells[1, 1].Value = "Pakket";
                ws.Cells[1, 2].Value = "# RRP Set";
                ws.Cells[1, 3].Value = "# AO Set";
                ws.Cells[1, 4].Value = "# Compared";
                ws.Cells[1, 5].Value = "# Good";
                ws.Cells[1, 6].Value = "# Bad";
                ws.Cells[1, 7].Value = "% Good";


                for (int i = 1; i <= countPak; i++)
                {

                    ws.Cells[1 + i, 1].Value = pakket[i].ToString();
                    ws.Cells[1 + i, 2].Value = numRRP[i].ToString();
                    ws.Cells[1 + i, 3].Value = numAO[i].ToString();
                    ws.Cells[1 + i, 4].Value = compared[i].ToString();
                    ws.Cells[1 + i, 5].Value = good[i].ToString();
                    ws.Cells[1 + i, 6].Value = bad[i].ToString();
                    ws.Cells[1 + i, 7].Value = percent[i].ToString();
                }
                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).AutoFit();
                ws.Column(4).AutoFit();
                ws.Column(5).AutoFit();
                ws.Column(6).AutoFit();
                ws.Column(7).AutoFit();
                excel.Save();
            }
        }

        private string PerformClick(string riskco,string ao,string x,string folder,string helppath)
        {

            string fileRiskco = riskco;
            string fileAO = ao;

            
    
            XmlReaderSettings readerSettings = new XmlReaderSettings();

            readerSettings.IgnoreComments = true;

            XmlReader reader1 = XmlReader.Create(@fileRiskco, readerSettings);
            XmlDocument xmlRiskco = new XmlDocument();
            xmlRiskco.Load(reader1);

            XmlReader reader2 = XmlReader.Create(@fileAO, readerSettings);
            XmlDocument xmlAO = new XmlDocument();
            xmlAO.Load(reader2);

            XmlNamespaceManager menRiskco = new XmlNamespaceManager(xmlRiskco.NameTable);
            menRiskco.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
            menRiskco.AddNamespace("deafult", "http://www.example.org/Bedrijfsgenoot_BPFSAG");

            XmlNamespaceManager menAO = new XmlNamespaceManager(xmlAO.NameTable);
            menAO.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
            menAO.AddNamespace("deafult", "http://www.example.org/Bedrijfsgenoot_BPFSAG");
            string pakket = folder;
            string sel = ".//deafult:" + pakket + "_Productpakket";
           
            //getting to the appropriate OP_Productpakket feature node!!
            XmlNode nodeRiskco = xmlRiskco.DocumentElement.SelectSingleNode(sel, menRiskco).FirstChild.ChildNodes[0];
            XmlNode nodeAO = xmlAO.DocumentElement.SelectSingleNode(sel, menAO).FirstChild.ChildNodes[0];

            int numberFeaturesRiskco = nodeRiskco.ChildNodes.Count;
            int numberFeaturesAO = nodeAO.ChildNodes.Count;


            string[] namesFeaturesRiskco = new string[numberFeaturesRiskco + 1];
            string[] namesFeaturesAO = new string[numberFeaturesAO + 1];

            int counter = 0;
            foreach (XmlNode item in nodeRiskco)
            {

                namesFeaturesRiskco[counter] = item.Name;
                counter++;
            }

            counter = 0;
            foreach (XmlNode item in nodeAO)
            {

                namesFeaturesAO[counter] = item.Name;
                counter++;
            }


               // string helpComparison = "";
            string commastring = "RiskCoFile, A&OFile, difference, difRiskCo, difA&O \r\n";

            string commastringFeatures = "RiskCoFile, A&OFile, difference, difRiskCo, difA&O \r\n";

            if (numberFeaturesRiskco > numberFeaturesAO)
            {

                for (int t1 = 0; t1 < numberFeaturesRiskco; t1++)
                {
                    //checking if some features are missing
                    if (!(namesFeaturesAO.Contains(namesFeaturesRiskco[t1])))
                    {
                        
                        commastringFeatures += x+ namesFeaturesRiskco[t1] + ",exist,missing \r\n";
                    }

                    //comparing same features
                    if (namesFeaturesAO.Contains(namesFeaturesRiskco[t1]))
                    {


                        string selectFeature = ".//deafult:" + namesFeaturesRiskco[t1];

                        XmlNode featureRiskco = nodeRiskco.SelectSingleNode(selectFeature, menRiskco);
                        XmlNode featureAO = nodeAO.SelectSingleNode(selectFeature, menAO);

                        //the number of values inside the specific feature differs
                        if (featureRiskco.ChildNodes.Count != featureAO.ChildNodes.Count)
                        {

                            commastring += x+namesFeaturesRiskco[t1] + "," + featureRiskco.ChildNodes.Count + "," + featureAO.ChildNodes.Count + " \r\n";
                        }

                        if (featureRiskco.ChildNodes.Count > featureAO.ChildNodes.Count)
                        {
                            for (int valueID = 0; valueID < featureRiskco.ChildNodes.Count; valueID++)
                            {

                                XmlNode valueRiskco = featureRiskco.ChildNodes[valueID];

                                string dateRiskco = valueRiskco.Attributes["date"].Value;
                                dateRiskco = dateRiskco.Substring(0, 10);
                                //compare them only of they share the same date
                                //needed since the different format of showing date in A&O files


                                //  if (!dateRiskco.Equals(dateAO))
                                //      commastring += x + namesRiskco[t1] + "," + "," + dateRiskco + "," + dateAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",attributeValue, \r\n";

                                string vR = valueRiskco.InnerText;

                                for (int valueIDAO = 0; valueIDAO < featureAO.ChildNodes.Count; valueIDAO++)
                                {
                                    XmlNode valueAO = featureAO.ChildNodes[valueIDAO];
                                    string dateAO = valueAO.Attributes["date"].Value;
                                    dateAO = dateAO.Substring(0, 10);
                                    string vA = valueAO.InnerText;
                                    //compare them only if they share equal dates
                                    if (dateRiskco.Equals(dateAO))
                                    {

                                        if ((Char.IsDigit(vR, 0) == true) && (Char.IsDigit(vA, 0) == true) && !(vR.Contains("-")) && !(vA.Contains("-")))
                                        {
                                            double innerRiskco = Convert.ToDouble(valueRiskco.InnerText);
                                            double innerAO = Convert.ToDouble(valueAO.InnerText);

                                            innerRiskco = Math.Round(innerRiskco, 2);
                                            innerAO = Math.Round(innerAO, 2);

                                            if (innerRiskco != innerAO)
                                                commastring += x + namesFeaturesRiskco[t1] + "," + innerRiskco + "," + innerAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",value, \r\n";
                                        }
                                    }
                                    else {

                                        commastring += x + namesFeaturesRiskco[t1] + "," + dateRiskco + "," + dateAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",value, \r\n";
                                    
                                    
                                    }

                                }
                            }

                        }
                        //ao feature values more than riskco feature values
                        else
                        {
                            for (int valueID = 0; valueID < featureAO.ChildNodes.Count; valueID++)
                            {

                                XmlNode valueAO = featureAO.ChildNodes[valueID];

                                string dateAO = valueAO.Attributes["date"].Value;
                                dateAO = dateAO.Substring(0, 10);
                                //compare them only of they share the same date
                                //needed since the different format of showing date in A&O files


                                //  if (!dateRiskco.Equals(dateAO))
                                //      commastring += x + namesRiskco[t1] + "," + "," + dateRiskco + "," + dateAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",attributeValue, \r\n";
                                string vA = "";
                                if(!(valueAO.InnerText==null))
                               vA = valueAO.InnerText;

                                for (int valueIDAO = 0; valueIDAO < featureAO.ChildNodes.Count; valueIDAO++)
                                {
                                    XmlNode valueRiskco = featureRiskco.ChildNodes[valueIDAO];
                                    string dateRiskco = valueRiskco.Attributes["date"].Value;
                                    dateRiskco = dateRiskco.Substring(0, 10);
                                      string vR="";
                                     if(!(valueRiskco.InnerText==null))
                                   vR= valueRiskco.InnerText;
                                    //compare them only if they share equal dates
                                     if (dateAO.Equals(dateRiskco))
                                     {
                                         if (vA != "" && vR != "")
                                             if ((Char.IsDigit(vR, 0) == true) && (Char.IsDigit(vA, 0) == true) && !(vR.Contains("-")) && !(vA.Contains("-")))
                                             {
                                                 double innerRiskco = Convert.ToDouble(valueRiskco.InnerText);
                                                 double innerAO = Convert.ToDouble(valueAO.InnerText);

                                                 innerRiskco = Math.Round(innerRiskco, 2);
                                                 innerAO = Math.Round(innerAO, 2);

                                                 if (innerRiskco != innerAO)
                                                     commastring += x + namesFeaturesRiskco[t1] + "," + innerRiskco + "," + innerAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",value, \r\n";
                                             }
                                     }
                                     else {

                                         commastring += x + namesFeaturesRiskco[t1] + "," + dateRiskco + "," + dateAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",value, \r\n";
                                     
                                     }

                                }
                            }

                        }




                    }
                }

            }
            else
            { //AO has more features or the have the same
                for (int t1 = 0; t1 < numberFeaturesAO; t1++)
                {
                    if (!(namesFeaturesRiskco.Contains(namesFeaturesAO[t1])))
                    {
                        // helpComparison +="   <"+ namesRiskco[t1] + ">  ";
                        commastringFeatures += x+namesFeaturesAO[t1] + ",missing,exist \r\n";
                    }

                    if (namesFeaturesRiskco.Contains(namesFeaturesAO[t1]))
                    {


                        string selectFeature = ".//deafult:" + namesFeaturesAO[t1];

                        XmlNode featureRiskco = nodeRiskco.SelectSingleNode(selectFeature, menRiskco);
                        XmlNode featureAO = nodeAO.SelectSingleNode(selectFeature, menAO);

                        //the number of values inside the specific feature differs
                        if (featureRiskco.ChildNodes.Count != featureAO.ChildNodes.Count)
                        {
                          
                            commastring +=x+ namesFeaturesAO[t1] + "," + featureRiskco.ChildNodes.Count + "," + featureAO.ChildNodes.Count + " \r\n";
                            // helpComparison += " The feature <" + namesAO[t1] + "> has " + featureRiskco.ChildNodes.Count + " values in the Riskco file and " + featureAO.ChildNodes.Count + " in the AO XML. \r\n";
                        }

                        //going to the values part!!
                        if (featureRiskco.ChildNodes.Count > featureAO.ChildNodes.Count)
                        {
                            for (int valueID = 0; valueID < featureRiskco.ChildNodes.Count; valueID++)
                            {

                                XmlNode valueRiskco = featureRiskco.ChildNodes[valueID];

                                string dateRiskco = valueRiskco.Attributes["date"].Value;
                                dateRiskco = dateRiskco.Substring(0, 10);
                                //compare them only of they share the same date
                                //needed since the different format of showing date in A&O files


                                //  if (!dateRiskco.Equals(dateAO))
                                //      commastring += x + namesRiskco[t1] + "," + "," + dateRiskco + "," + dateAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",attributeValue, \r\n";

                                string vR = valueRiskco.InnerText;

                                for (int valueIDAO = 0; valueIDAO < featureAO.ChildNodes.Count; valueIDAO++)
                                {
                                    XmlNode valueAO = featureAO.ChildNodes[valueIDAO];
                                    string dateAO = valueAO.Attributes["date"].Value;
                                    dateAO = dateAO.Substring(0, 10);
                                    string vA = valueAO.InnerText;
                                    //compare them only if they share equal dates
                                    if (dateRiskco.Equals(dateAO))
                                    {

                                        if ((Char.IsDigit(vR, 0) == true) && (Char.IsDigit(vA, 0) == true) && !(vR.Contains("-")) && !(vA.Contains("-")))
                                        {
                                            double innerRiskco = Convert.ToDouble(valueRiskco.InnerText);
                                            double innerAO = Convert.ToDouble(valueAO.InnerText);

                                            innerRiskco = Math.Round(innerRiskco, 2);
                                            innerAO = Math.Round(innerAO, 2);

                                            if (innerRiskco != innerAO)
                                                commastring += x + namesFeaturesRiskco[t1] + "," + innerRiskco + "," + innerAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",value, \r\n";
                                        }
                                    }

                                }
                            }

                        }
                        //ao feature values more than riskco feature values
                        else
                        {
                            for (int valueID = 0; valueID < featureAO.ChildNodes.Count; valueID++)
                            {

                                XmlNode valueAO = featureAO.ChildNodes[valueID];

                                string dateAO = valueAO.Attributes["date"].Value;
                                dateAO = dateAO.Substring(0, 10);
                                //compare them only of they share the same date
                                //needed since the different format of showing date in A&O files


                                //  if (!dateRiskco.Equals(dateAO))
                                //      commastring += x + namesRiskco[t1] + "," + "," + dateRiskco + "," + dateAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",attributeValue, \r\n";

                                string vA = valueAO.InnerText;

                                for (int valueIDAO = 0; valueIDAO < featureRiskco.ChildNodes.Count; valueIDAO++)
                                {
                                    XmlNode valueRiskco = featureRiskco.ChildNodes[valueIDAO];
                                    string dateRiskco = valueRiskco.Attributes["date"].Value;
                                    dateRiskco = dateRiskco.Substring(0, 10);
                                    string vR = valueRiskco.InnerText;
                                    //compare them only if they share equal dates
                                    if (dateAO.Equals(dateRiskco))
                                    {

                                        if ((Char.IsDigit(vR, 0) == true) && (Char.IsDigit(vA, 0) == true) && !(vR.Contains("-")) && !(vA.Contains("-")))
                                        {
                                            double innerRiskco = Convert.ToDouble(valueRiskco.InnerText);
                                            double innerAO = Convert.ToDouble(valueAO.InnerText);

                                            innerRiskco = Math.Round(innerRiskco, 2);
                                            innerAO = Math.Round(innerAO, 2);

                                            if (innerRiskco != innerAO)
                                                commastring += x + namesFeaturesRiskco[t1] + "," + innerRiskco + "," + innerAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",value, \r\n";
                                        }
                                    }

                                }
                            }

                        }



                    }
                }

            }

            string outputFileName = Path.GetFileNameWithoutExtension(ao) + "_&_" + Path.GetFileNameWithoutExtension(riskco);

            if (form1.CountLinesInFile(commastring) != 1)
            {
                string help = helppath + "\\" + outputFileName + ".txt";
                //string help = lblLocation.Text + test;
                System.IO.StreamWriter file = new System.IO.StreamWriter(@help);



                file.WriteLine(commastring);
                file.Close();
            }

            //create new folder where you will write down the extrafeatures
            //the new folders 
            string fpath = helppath + "\\" + "extraFeatures";
            DirectoryInfo dirfeat = Directory.CreateDirectory(fpath);
            //now write down the features
            string helpp = helppath + "\\extraFeatures\\" + outputFileName + ".txt";
            //string help = lblLocation.Text + test;
            System.IO.StreamWriter ffile = new System.IO.StreamWriter(@helpp);



            ffile.WriteLine(commastringFeatures);
            ffile.Close();


            //return only the values differences!!
            return commastring;

        } 

        private void btnClcIn_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblAOClcIn.Text = folder;

            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
