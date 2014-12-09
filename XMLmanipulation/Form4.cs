using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace XMLmanipulation
{
    //Form calculating the clcin differences for all productpackets except OP
    public partial class Form4 : Form
    {

        Form1 helpForm = new Form1();

        public Form4()
        {
            InitializeComponent();
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblWhere.Text = folder;

            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                lblFolder.Text = folder;

            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {

            //extra information

            string riskcoFolder = lblFolder.Text;
            string aoFolder = lblorsima.Text;

            //the output string
            string findings="";
            
            int execounter = 0;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@riskcoFolder);
            //we know the number of clcin files in Riskco
            int countRiskco = dir.GetFiles("*.clcin").Count();
     
            //going through each file found in the folder
            System.IO.FileInfo[] filesDirRiskco = dir.GetFiles("*.clcin");


           System.IO.DirectoryInfo dirAO = new System.IO.DirectoryInfo(@aoFolder);

     
            //going through each file found in the folder
            System.IO.FileInfo[] filesDirAO = dirAO.GetFiles("*.clcin");

            foreach (System.IO.FileInfo f in filesDirRiskco)
            {
                //probably this part wont be needed, i suppose that different pakkets will be in seperate folders
                //dealing only with PP files
                if (f.Name.Contains("PP"))
                {
                    string filenameRiskco = f.Name;
                    //take only the first 10 letters of the string- gives the person id
                    filenameRiskco = filenameRiskco.Substring(0, 9);

                    //now loop through the A&O folder and find the file
                    foreach (System.IO.FileInfo fao in filesDirAO)
                    {

                        string filenameAO = fao.Name;
                        //take only the first 10 letters of the string- gives the person id we are interested in
                        filenameAO = filenameAO.Substring(0, 9);

                        //connecting the apprpriate files
                        if (filenameAO.Contains(filenameRiskco))
                        {
                            execounter++;
                            findings += calculation(f.FullName, fao.FullName, f.Name, fao.Name);
                        }

                    }

                }



            }

            findings += "execounter=" + execounter.ToString();

            string help1 = lblWhere.Text + "\\" + "allFindings" + ".txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(@help1);
            file.WriteLine(findings);
            file.Close();
        }

        public string calculation(string filepathRiskco, string filepathAO, string fileRiskco, string fileAO)
        {
            string output = "";
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            string[] errorsList = { "numOfFeatures", "numOfValues", "numOfBDGLinks", "startingDate" };
            // readerSettings.IgnoreComments = true;

            XmlReader reader = XmlReader.Create(@filepathAO, readerSettings);
            XmlDocument doc1 = new XmlDocument();
            doc1.Load(reader);
            XmlNamespaceManager menAO = new XmlNamespaceManager(doc1.NameTable);
            menAO.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
            menAO.AddNamespace("df", "Deployment:Bedrijfsgenoot_BPFSAG");

            XmlReader reader2 = XmlReader.Create(@filepathRiskco, readerSettings);
            XmlDocument doc2 = new XmlDocument();
            doc2.Load(reader2);
            XmlNamespaceManager menRiskco = new XmlNamespaceManager(doc2.NameTable);
            menRiskco.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
            menRiskco.AddNamespace("df", "http://www.example.org/Bedrijfsgenoot_BPFSAG");


            //    string personID = doc1.DocumentElement.SelectSingleNode(".//clc:CalculationData", menAO).FirstChild.Value;
            //   String justID = new String(personID.Where(Char.IsDigit).ToArray());
            //    string npID = "";

            string[] infoNatuurlijkOrsima = new string[1];
            int[] childFVRiskco = new int[2];
            int[] childFVAO = new int[2];
            int[] partnerFVRiskco = new int[2];
            int[] partnerFVAO = new int[2];

            //RISKCO NODES
            //Bedrijfsgenoot_Riskco
            XmlNode bedrijfsigenootRiskco = doc2.DocumentElement.SelectSingleNode(".//df:Bedrijfsgenoot_BPFSAG", menRiskco);
            int[] bedrijfFVRiskco = helpForm.valuesNode(bedrijfsigenootRiskco, 2);
            //Partner node
            XmlNode nodePartnerRiskco = doc2.DocumentElement.SelectSingleNode(".//df:BPFSAG_Partner", menRiskco).FirstChild;
            if(nodePartnerRiskco!=null)
                 partnerFVRiskco = helpForm.valuesNode(nodePartnerRiskco, 2);
            //Child node
            XmlNode nodeChildRiskco = doc2.DocumentElement.SelectSingleNode(".//df:BPFSAG_Kind", menRiskco).FirstChild;
            if (nodeChildRiskco != null)
                childFVRiskco = helpForm.valuesNode(nodeChildRiskco, 2);
            //Riskco BDG
            XmlNode nodeBDGRiskco = doc2.DocumentElement.SelectSingleNode(".//df:BDG", menRiskco);
            // int[] BDGFVRiskco = helpForm.valuesNode(nodeBDGRiskco, 2);
            //Riskco Regelingkenmerken
            XmlNode nodeRegelingRiskco = doc2.DocumentElement.SelectSingleNode(".//df:BPFSAG_Regelingkenmerken", menRiskco).FirstChild;
            int[] regelingFVRiskco = helpForm.valuesNode(nodeRegelingRiskco, 2);
            //BPFSAG_Productkenmerken node
            XmlNode nodePPRiskco = doc2.DocumentElement.SelectSingleNode(".//df:BPFSAG_Productkenmerken", menRiskco).FirstChild;
            int[] ppFVRiskco = helpForm.valuesNode(nodePPRiskco, 2);

            //the naturlijk_persoon part



            //A&O NODES
            //Bedrijfsgenoot_AO
            XmlNode bedrijfsigenootAO = doc1.DocumentElement.SelectSingleNode(".//df:Bedrijfsgenoot_BPFSAG", menAO);
            int[] bedrijfFVAO = helpForm.valuesNode(bedrijfsigenootAO, 2);
            //Partner node
            XmlNode nodePartnerAO = doc1.DocumentElement.SelectSingleNode(".//df:BPFSAG_Partner", menAO).FirstChild;
            if(nodePartnerAO!=null)
            partnerFVAO = helpForm.valuesNode(nodePartnerAO, 2);
            //Child node(maybe some changes will be needed here-check when you get the WZP)
            XmlNode nodeChildAO = doc1.DocumentElement.SelectSingleNode(".//df:BPFSAG_Kind", menAO).FirstChild;
            if (nodeChildAO != null)
                childFVAO = helpForm.valuesNode(nodeChildAO, 2);
            //A&O BDG
            XmlNode nodeBDGAO = doc1.DocumentElement.SelectSingleNode(".//df:BDG", menAO);
            // int[] BDGFVAO = helpForm.valuesNode(nodeBDGAO, 2);
            //A&O Regelingkenmerken
            XmlNode nodeRegelingAO = doc1.DocumentElement.SelectSingleNode(".//df:BPFSAG_Regelingkenmerken", menAO).FirstChild;
            int[] regelingFVAO = helpForm.valuesNode(nodeRegelingAO, 2);
            //PP_Productpakket
            XmlNode nodePPAO = doc1.DocumentElement.SelectSingleNode(".//df:BPFSAG_Productkenmerken", menAO).FirstChild;
            int[] ppFVAO = helpForm.valuesNode(nodePPAO, 2);


            //getting the differences between appropriate nodes

            for (int i = 0; i < bedrijfFVRiskco.Length; i++)
            {
                if (bedrijfFVRiskco[i] != bedrijfFVAO[i])
                    output += fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9) + ",Bedrijfsgenoot_BPFSAG,Bedrijfsgenoot_BPFSAG," + errorsList[i] + "," + bedrijfFVRiskco[i].ToString() + "," + bedrijfFVAO[i].ToString() + ", , , ,\r\n";
            }

            if (partnerFVAO.Length != 0)
                for (int i = 0; i < partnerFVAO.Length; i++)
                {
                    if (partnerFVRiskco[i] != partnerFVAO[i])
                        output += fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9) + ",BPFSAG_Partner,BPFSAG_Partner," + errorsList[i] + "," + partnerFVRiskco[i].ToString() + "," + partnerFVAO[i].ToString() + ", , , ,\r\n";
                }
            if (childFVAO.Length != 0)
                for (int i = 0; i < childFVAO.Length; i++)
                {
                    if (childFVRiskco[i] != childFVAO[i])
                        output += fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9) + ",BPFSAG_Kind,BPFSAG_Kind," + errorsList[i] + "," + childFVRiskco[i].ToString() + "," + childFVAO[i].ToString() + ", , , ,\r\n";
                }

            for (int i = 0; i < regelingFVAO.Length; i++)
            {
                if (regelingFVRiskco[i] != regelingFVAO[i])
                    output += fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9) + ",BPFSAG_Regelingkenmerken,BPFSAG_Regelingkenmerken," + errorsList[i] + "," + regelingFVRiskco[i].ToString() + "," + regelingFVAO[i].ToString() + ", , , ,\r\n";
            }
            for (int i = 0; i < ppFVAO.Length; i++)
            {
                if (ppFVRiskco[i] != ppFVAO[i])
                    output += fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9) + ",BPFSAG_Productkenmerken,BPFSAG_Productkenmerken," + errorsList[i] + "," + ppFVRiskco[i].ToString() + "," + ppFVAO[i].ToString() + ", , , ,\r\n";
            }


            output += helpForm.differenceValues(bedrijfsigenootRiskco, bedrijfsigenootAO, bedrijfFVRiskco[0], bedrijfFVAO[0], menRiskco, menAO, fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9), "PP");
           
            if (partnerFVRiskco[0] != 0 && partnerFVAO[0] != 0)
                output += helpForm.differenceValues(nodePartnerRiskco, nodePartnerAO, partnerFVRiskco[0], partnerFVAO[0], menRiskco, menAO, fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9), "PP");
           
            if (childFVRiskco[0] != 0 && childFVAO[0] != 0)
                output += helpForm.differenceValues(nodeChildRiskco, nodeChildAO, childFVRiskco[0], childFVAO[0], menRiskco, menAO, fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9), "PP");
           
            output += helpForm.differenceValues(nodeRegelingRiskco, nodeRegelingAO, regelingFVRiskco[0], regelingFVAO[0], menRiskco, menAO, fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9), "PP");

            output += helpForm.differenceValues(nodePPRiskco, nodePPAO, ppFVRiskco[0], ppFVAO[0], menRiskco, menAO, fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9), "PP");
         
            //now the BDG part

            int countRiskco = nodeBDGRiskco.ChildNodes.Count;
            int countAO = nodeBDGAO.ChildNodes.Count;

            string[] idRiskcoBDGs = new string[countRiskco];
            string[] idAOBDGs = new string[countAO];

            string startendRiskco = "BDG_ID, bdg_start, bdg_end, \r\n";
            string startendAO = "BDG_ID, bdg_start, bdg_end, \r\n";
            string helpstring = fileRiskco.Substring(0, 9) + "," + fileAO.Substring(0, 9) + ",";

            for (int j = 0; j < countRiskco; j++)
            {

                XmlNode innerBDGFeaturesRiskco = nodeBDGRiskco.ChildNodes[j].ChildNodes[0];
                idRiskcoBDGs[j] = nodeBDGRiskco.ChildNodes[j].Attributes["id"].Value;

                string startdateRiskco = helpForm.startDateCalc(innerBDGFeaturesRiskco);

                string enddateRiskco = helpForm.endDateCalc(innerBDGFeaturesRiskco);

                startendRiskco += idRiskcoBDGs[j] + "," + startdateRiskco + "," + enddateRiskco + ",\r\n";

                //calculating the numFeatures and numValues
                int[] valuesInnerBDGRiskco = helpForm.valuesNode(nodeBDGRiskco.ChildNodes[j], 2);

                //start with the comparison
                for (int k = 0; k < nodeBDGAO.ChildNodes.Count; k++)
                {

                    XmlNode innerBDGFeaturesAO = nodeBDGAO.ChildNodes[k].ChildNodes[0];

                    string startdateAO = helpForm.startDateCalc(innerBDGFeaturesAO);

                    string enddateAO = helpForm.endDateCalc(innerBDGFeaturesAO);
                    idAOBDGs[k] = nodeBDGAO.ChildNodes[k].Attributes["id"].Value;

                    if (j == 0)
                        startendAO += idAOBDGs[k] + "," + startdateAO + "," + enddateAO + ",\r\n";
                    //we are comparing the BDGs that have the same starting date-those should be identical
                    if ((startdateRiskco.Equals(startdateAO) && enddateRiskco.Equals(enddateAO)) || (enddateAO == "" && startdateRiskco.Equals(startdateAO)))
                    {


                        int[] valuesInnerBDGAO = helpForm.valuesNode(nodeBDGAO.ChildNodes[k], 2);



                        for (int p = 0; p < valuesInnerBDGAO.Count(); p++)
                        {
                            if (p == 0)
                            {
                                int checkInt = 0;


                                string check = helpForm.extraFeatures(nodeBDGRiskco.ChildNodes[j], nodeBDGAO.ChildNodes[k], valuesInnerBDGRiskco[0], valuesInnerBDGAO[0], helpstring + "BDG_ID_" + idRiskcoBDGs[j] + "," + "BDG_ID_" + idAOBDGs[k] + ",")[1];


                                if (!check.Equals(""))
                                    checkInt = int.Parse(check.ToString());



                                if (checkInt != 0)
                                {
                                    if (valuesInnerBDGRiskco[p] == valuesInnerBDGAO[p] + checkInt)
                                        valuesInnerBDGRiskco[p] = valuesInnerBDGRiskco[p] - checkInt;

                                    if (valuesInnerBDGAO[p] == valuesInnerBDGRiskco[p] + checkInt)
                                        valuesInnerBDGAO[p] = valuesInnerBDGAO[p] - checkInt;

                                }
                                else
                                {
                                    output += helpForm.extraFeatures(nodeBDGRiskco.ChildNodes[j], nodeBDGAO.ChildNodes[k], valuesInnerBDGRiskco[p], valuesInnerBDGAO[p], helpstring + "BDG_ID_" + idRiskcoBDGs[j] + "," + "BDG_ID_" + idAOBDGs[k] + ",")[0];

                                }
                            }

                            if (valuesInnerBDGRiskco[p] != valuesInnerBDGAO[p])
                            {

                                output += helpstring + "BDG_ID_" + idRiskcoBDGs[j] + "," + "BDG_ID_" + idAOBDGs[k] + "," + errorsList[p] + "," + valuesInnerBDGRiskco[p].ToString() + "," + valuesInnerBDGAO[p].ToString() + ", , , , \r\n";



                            }



                        }

                    } 

                }
            }//end of BDG comparison




            return output;

        }

       private void btnAO_Click(object sender, EventArgs e)
       {
           DialogResult result = folderBrowserDialog1.ShowDialog();
           if (result == DialogResult.OK) // Test result.
           {
               string folder = folderBrowserDialog1.SelectedPath;
               lblorsima.Text = folder;

           }
       }
      
    
    }
}
