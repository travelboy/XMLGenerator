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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnRiskcoFiles_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath; ;
                lblRiskcoFolder.Text = folder;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath; ;
                lblAOFolder.Text = folder;
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            //used for comparison
            string[] errorsList = { "numOfFeatures", "numOfValues", "numOfBDGLinks", "startingDate" };

            //the string we get from the user
            string analyze = txtDetails.Text;

            string fileRiskco1 = analyze.Split(',')[0];
            string fileAO1 = analyze.Split(',')[1];
            //useful information

            string firstID = analyze.Split(',')[2];
            firstID = firstID.Substring(7);
            string secondID = analyze.Split(',')[3];
            secondID = secondID.Substring(7);
            string difference = analyze.Split(',')[4];
            string output = "";


            foreach (string s in errorsList)
            {
                if (difference.Equals(s))
                {
                    output = "No extra information for this difference. Insert one that contains feature name!";

                }
            }

            string fileRiskco = lblRiskcoFolder.Text + "\\" + fileRiskco1;
            string fileAO = lblAOFolder.Text + "\\" + fileAO1;
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
            menAO.AddNamespace("deafult", "Deployment:Bedrijfsgenoot_BPFSAG");

            if (firstID == "Bedrijfsgenoot_BPFSAG") 
            {
                txtXMLRiskco.Text = "";
                //selecting the feature examined
                XmlNode featureNodeRiskco = xmlRiskco.DocumentElement.SelectSingleNode(".//deafult:" + firstID, menRiskco).ChildNodes[0].SelectSingleNode(".//deafult:" + difference, menRiskco); ;
                XmlNode featureNodeAO = xmlAO.DocumentElement.SelectSingleNode(".//deafult:" + firstID, menAO).ChildNodes[0].SelectSingleNode(".//deafult:" + difference, menAO); ;

              
               //there can be a case in which a particular feature has a value null- when we are dealing with missing features
               if (featureNodeRiskco == null)
               {
                
                  txtXMLAO.Text= featureNodeAO.InnerXml;
                  txtXMLRiskco.Text = "The feature <" + difference + "> is missing in " + fileRiskco1;

               }
               else if (featureNodeAO == null)
               {
                  
                   txtXMLRiskco.Text = featureNodeRiskco.InnerXml;
                   txtXMLAO.Text = "The feature <" + difference + "> is missing in " + fileAO1;
               }
               else 
               {//both features are here! Show them!
               
                   txtXMLRiskco.Text = featureNodeRiskco.InnerXml;
                   txtXMLAO.Text = featureNodeAO.InnerXml;
               }
            }



            else if (firstID == "BPFSAG_Productkenmerken" || firstID == "BPFSAG_Regelingkenmerken") 
            {
                txtXMLRiskco.Text = "";
                XmlNode featureNodeRiskco = xmlRiskco.DocumentElement.SelectSingleNode(".//deafult:" + firstID, menRiskco).FirstChild.ChildNodes[0].SelectSingleNode(".//deafult:" + difference, menRiskco); 
                XmlNode featureNodeAO = xmlAO.DocumentElement.SelectSingleNode(".//deafult:" + firstID, menAO).FirstChild.ChildNodes[0].SelectSingleNode(".//deafult:" + difference, menAO); 


                //there can be a case in which a particular feature has a value null- when we are dealing with missing features
                if (featureNodeRiskco == null)
                {
                  
                    txtXMLAO.Text = featureNodeAO.InnerXml;
                    txtXMLRiskco.Text = "The feature <" + difference + "> is missing in " + fileRiskco1;

                }
                else if (featureNodeAO == null)
                {
        

                    txtXMLRiskco.Text = featureNodeRiskco.InnerXml;
                    txtXMLAO.Text = "The feature <" + difference + "> is missing in " + fileAO1;
                }
                else
                {//both features are here! Show them!
                 
                    txtXMLRiskco.Text = featureNodeRiskco.InnerXml;
                    txtXMLAO.Text = featureNodeAO.InnerXml;

                }
            }

            else 

            { 
                //dealing with BDGs
                txtXMLRiskco.Text = "";
                XmlNode featureRiskco;
                XmlNode featureAO;
                string help1 = "";
                string help2 = "";
                int count1 = 0;
                int count2 = 0;
                XmlNodeList BDGRiskco = xmlRiskco.DocumentElement.SelectSingleNode(".//deafult:BDG" , menRiskco).ChildNodes;
                XmlNodeList BDGAO = xmlAO.DocumentElement.SelectSingleNode(".//deafult:BDG", menAO).ChildNodes;
               
                foreach (XmlNode bdgNode in BDGRiskco)
                {
                    string str = bdgNode.Attributes["ext-id"].Value.ToString();
                    if (bdgNode.Attributes["ext-id"].Value.ToString() == firstID) 
                    {
                    //then we got the one we need
                      featureRiskco= bdgNode.ChildNodes[0].SelectSingleNode(".//deafult:" + difference, menRiskco);
                      help1 = featureRiskco.InnerXml;
                      count1++;
                    }
                }

                foreach (XmlNode bdgNode in BDGAO)
                {
                    string str = bdgNode.Attributes["id"].Value.ToString();
                     if (bdgNode.Attributes["id"].Value.ToString() == secondID) 
                    {
                    //then we got the one we need
                         featureAO = bdgNode.ChildNodes[0].SelectSingleNode(".//deafult:" + difference, menAO);
                         help2 = featureAO.InnerXml;
                         count2++;
                    }

                }

                //there can be a case in which a particular feature has a value null- when we are dealing with missing features
                if (count1==0)
                {
                   
                    txtXMLAO.Text = help2;
                    txtXMLRiskco.Text = "The feature <" + difference + "> is missing in " + fileRiskco1;

                }
                else if (count2==0)
                {
                   // string x = "The feature <" + difference + "> is missing in " + fileAO1 + ".\r\n Here is the data from the " + fileRiskco1 + "\r\n";
                   // txtXMLRiskco.Text = x + help1;

                    txtXMLRiskco.Text =help1;
                    txtXMLAO.Text = "The feature <" + difference + "> is missing in " + fileAO1;
                }
                else
                {//both features are here! Show them!
           
                    txtXMLRiskco.Text = help1;
                    txtXMLAO.Text = help2;
                }
                count1 = 0;
                count2 = 0;
            }


            lblAinfo.Text = fileAO1;
            lblRinfo.Text = fileRiskco1;
          




        }
    }
}
