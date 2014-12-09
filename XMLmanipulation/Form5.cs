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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string riskcoFolder = lblRiskcoPath.Text;
            string aoFolder = lblAOPath.Text;
            //change a bit here only if the CLCOUT are in another directory

            System.IO.DirectoryInfo dirAO = new System.IO.DirectoryInfo(@aoFolder);
            System.IO.FileInfo[] filesAOin = dirAO.GetFiles("*.clcin");
            System.IO.FileInfo[] filesAOout = dirAO.GetFiles("*.clcout");


            System.IO.DirectoryInfo dirRiskco = new System.IO.DirectoryInfo(@riskcoFolder);
            System.IO.FileInfo[] filesRiskcoin = dirRiskco.GetFiles("*.clcin");
            System.IO.FileInfo[] filesRiskcoout = dirRiskco.GetFiles("*.clcout");


            //output strings
            string outputCLCIN = "";
            string outputCLCOUT="";

            //case CLCIN
            foreach (System.IO.FileInfo f in filesAOin)
            {

                string nameAOfile = f.Name;
                string pathAOfile = f.FullName;
                XmlReaderSettings readerSettings = new XmlReaderSettings();
                //change the namespaces!!!
                XmlReader readerhelp = XmlReader.Create(@f.FullName, readerSettings);
                XmlDocument doc1help = new XmlDocument();
                doc1help.Load(readerhelp);
                XmlNamespaceManager menAO = new XmlNamespaceManager(doc1help.NameTable);
                menAO.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
                menAO.AddNamespace("df", "Deployment:Bedrijfsgenoot_BPFSAG");
                
                //connect somehow the AO with Riskco file !! 
                //call the compare button
                string pathRiskcofile="";
                string nameRiskcofile="";
                outputCLCIN+=nameRiskcofile+"," +nameAOfile+"," +compare(pathRiskcofile,pathAOfile,"1")+"\r\n";

            }

            //case CLCOUT
            foreach (System.IO.FileInfo f in filesAOout)
            {

                string nameAOfile = f.Name;
                string pathAOfile = f.FullName;
                XmlReaderSettings readerSettings = new XmlReaderSettings();
                //change the namespaces!!!
                XmlReader readerhelp = XmlReader.Create(@f.FullName, readerSettings);
                XmlDocument doc1help = new XmlDocument();
                doc1help.Load(readerhelp);
                XmlNamespaceManager menAO = new XmlNamespaceManager(doc1help.NameTable);
                menAO.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
                menAO.AddNamespace("df", "Deployment:Bedrijfsgenoot_BPFSAG");

                //connect somehow the AO with Riskco file !! 
                //call the compare button
                string pathRiskcofile = "";
                string nameRiskcofile = "";
                outputCLCOUT += nameRiskcofile + "," + nameAOfile + "," + compare(pathRiskcofile, pathAOfile, "2") + "\r\n";

            }
        }
        private string compare(string pathRiskco,string pathAO,string flag){
            string commaseparated = "";

            XmlReaderSettings readerSettings = new XmlReaderSettings();
          
            XmlReader readerR = XmlReader.Create(@pathRiskco, readerSettings);
            XmlDocument docR = new XmlDocument();
            docR.Load(readerR);
            XmlNamespaceManager menRiskco = new XmlNamespaceManager(docR.NameTable);
            menRiskco.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
            menRiskco.AddNamespace("df", "http://www.example.org/Mutatieverloop");

       
            //change the namespaces!!!
            XmlReader readerA = XmlReader.Create(@pathAO, readerSettings);
            XmlDocument docA = new XmlDocument();
            docA.Load(readerA);
            XmlNamespaceManager menAO = new XmlNamespaceManager(docA.NameTable);
            menAO.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
            menAO.AddNamespace("df", "Deployment:Bedrijfsgenoot_BPFSAG");

            if (flag == "1")
            {
                XmlNode nodeRiskcoBS = docR.DocumentElement.SelectSingleNode(".//deafult:OP_BS", menRiskco).FirstChild.FirstChild;
                XmlNode nodeAOBS = docA.DocumentElement.SelectSingleNode(".//deafult:OP_BS", menAO).FirstChild.FirstChild;
                commaseparated += calculationDifferences(nodeRiskcoBS, nodeAOBS, menRiskco, menAO);

                XmlNode nodeRiskcoES = docR.DocumentElement.SelectSingleNode(".//deafult:OP_ES", menRiskco).FirstChild.FirstChild;
                XmlNode nodeAOES = docA.DocumentElement.SelectSingleNode(".//deafult:OP_ES", menAO).FirstChild.FirstChild;
                commaseparated += calculationDifferences(nodeRiskcoES, nodeAOES, menRiskco, menAO);
            }

            if (flag == "2")
            {
                XmlNode nodeRiskcoVerloop = docR.DocumentElement.SelectSingleNode(".//deafult:OP_Verloop", menRiskco).FirstChild.FirstChild;
                XmlNode nodeAOVerloop = docA.DocumentElement.SelectSingleNode(".//deafult:OP_Verloop", menAO).FirstChild.FirstChild;
                commaseparated += calculationDifferences(nodeRiskcoVerloop, nodeAOVerloop, menRiskco, menAO);
            }

            return commaseparated;
        }


        private string calculationDifferences(XmlNode nodeRiskcoBS, XmlNode nodeAOBS,XmlNamespaceManager menRiskco, XmlNamespaceManager menAO)
        {
            //getting the features names
            int numberFeaturesRiskco = nodeRiskcoBS.ChildNodes.Count;
            int numberFeaturesAO = nodeAOBS.ChildNodes.Count;
            string[] namesFeaturesRiskco = new string[numberFeaturesRiskco + 1];
            string[] namesFeaturesAO = new string[numberFeaturesAO + 1];
            string commastring = "";
            string x = "";
            int counter = 0;
            foreach (XmlNode item in nodeRiskcoBS)
            {
                namesFeaturesRiskco[counter] = item.Name;
                counter++;
            }

            counter = 0;
            foreach (XmlNode item in nodeAOBS)
            {
                namesFeaturesAO[counter] = item.Name;
                counter++;
            }



            if (numberFeaturesRiskco > numberFeaturesAO)
            {

                for (int t1 = 0; t1 < numberFeaturesRiskco; t1++)
                {
                    //checking if some features are missing
                    if (!(namesFeaturesAO.Contains(namesFeaturesRiskco[t1])))
                    {
                        // helpComparison +="   <"+ namesRiskco[t1] + ">  ";
                        commastring += x + namesFeaturesRiskco[t1] + ",exist,missing \r\n";
                    }

                    //comparing same features
                    if (namesFeaturesAO.Contains(namesFeaturesRiskco[t1]))
                    {


                        string selectFeature = ".//deafult:" + namesFeaturesRiskco[t1];

                        string featureRiskcoValue = nodeRiskcoBS.SelectSingleNode(selectFeature, menRiskco).InnerText;
                        string featureAOValue = nodeAOBS.SelectSingleNode(selectFeature, menAO).InnerText;

                        //a difference detected
                        if (!featureRiskcoValue.Equals(featureAOValue))
                        {

                            commastring += x + namesFeaturesRiskco[t1] + "," + featureRiskcoValue + "," + featureAOValue + " \r\n";
                        
                        }

                   




                    }
                }

            }
            else
            { //AO has more features or the have the same
                for (int t1 = 0; t1 < numberFeaturesAO; t1++)
                {
                    //checking if some features are missing
                    if (!(namesFeaturesRiskco.Contains(namesFeaturesAO[t1])))
                    {
                       
                        commastring += x + namesFeaturesAO[t1] + ",exist,missing \r\n";
                    }

                    //comparing same features
                    if (namesFeaturesRiskco.Contains(namesFeaturesAO[t1]))
                    {


                        string selectFeature = ".//deafult:" + namesFeaturesAO[t1];

                        string featureRiskcoValue = nodeRiskcoBS.SelectSingleNode(selectFeature, menRiskco).InnerText;
                        string featureAOValue = nodeAOBS.SelectSingleNode(selectFeature, menAO).InnerText;

                        //a difference detected
                        if (!featureRiskcoValue.Equals(featureAOValue))
                        {

                            commastring += x + namesFeaturesAO[t1] + "," + featureRiskcoValue + "," + featureAOValue + " \r\n";

                        }






                    }
                }
            }


            return commastring;
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath; ;
                lblOutput.Text = folder;
            }
        }

        private void btnRiskco_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath; ;
                lblRiskcoPath.Text = folder;
            }
        }

        private void btnAOPath_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath; ;
                lblAOPath.Text = folder;
            }
        }

        private void btnROut_Click(object sender, EventArgs e)
        {

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath; ;
                lblRiskcoOut.Text = folder;
            }
        }

        private void btnAOut_Click(object sender, EventArgs e)
        {

            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath; ;
                lblAOOut.Text = folder;
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
