using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Data.OleDb;
using ADOX;
using ADODB;
using OfficeOpenXml;
using System.Xml.Linq;
using OfficeOpenXml.Style;

namespace XMLmanipulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        //help class for the single report case
        public class BDG
        {
            public string file;
            public int numBDGLinks;
            public int id;
            public DateTime BDGStart;
            public bool check = false;
            public int getLinks()
            {
                return this.numBDGLinks;
            }
            public string getFile()
            {
                return this.file;
            }
            public void setLinks(int nl)
            {
                this.numBDGLinks = nl;
            }
            public void setFile(string f)
            {
                this.file = f;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        static List<DateTime> SortAscending(List<DateTime> list)
        {
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        private void btnBrowse1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                lblFirstFile.Text = file;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            folderBrowserDialog1.SelectedPath = @"G:\A&O Fase 3 (Keten)\RRPClcins\20130819v2";
            DialogResult result = folderBrowserDialog1.ShowDialog();
    
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                labelRiskcoFolder.Text = folder;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = @"G:\A&O_RRP_Test_20130730\915 v3 in_out";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath;
                labelFolderAO.Text = folder;

            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                lblSecondFile.Text = file;
            }
        }

        private void btnBrowse3_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                lblThirdFile.Text = file;
            }
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string folder = folderBrowserDialog1.SelectedPath; ;
                lblLocation.Text = folder;
            }
        }

        public string[] extraFeatures(XmlNode nodeRiskco, XmlNode nodeAO, int numFRiskco, int numFAO, string x)
        {


            int excludeList = 0;
            string marker = "";
            string commastring = "";

            if (numFRiskco > numFAO)
            {

                string[] namesRiskco = getFeaturesNames(nodeRiskco);
                string[] namesAO = getFeaturesNames(nodeAO);
                string[] missingNames = new string[numFRiskco - numFAO + 1];
                int counter = 0;

                for (int t1 = 0; t1 < numFRiskco; t1++)
                {


                    if (!(namesAO.Contains(namesRiskco[t1])))
                    {
                        //first check if the feature is registratiedatum or startdate
                        //we are not interested in the difference of these features, its redundant
                        if (namesRiskco[t1].Equals("registratiedatum") || namesRiskco[t1].Equals("startdate"))
                        {
                            excludeList++;
                        }
                        else
                        {

                            missingNames[counter] = namesRiskco[t1];
                            counter++;

                            commastring += x + namesRiskco[t1] + ",exist,missing, , , , \r\n";
                        }
                    }

                }

            }

            else
            {

                string[] namesRiskco = getFeaturesNames(nodeRiskco);
                string[] namesAO = getFeaturesNames(nodeAO);
                string[] missingNames = new string[numFAO - numFRiskco + 1];
                int counter = 0;
                for (int t1 = 0; t1 < numFAO; t1++)
                {


                    if (!(namesRiskco.Contains(namesAO[t1])))
                    {
                        if (namesAO[t1].Equals("registratiedatum") || namesAO[t1].Equals("startdate"))
                        {

                            excludeList++;

                        }
                        else
                        {
                            missingNames[counter] = namesAO[t1];
                            counter++;

                            commastring += x + namesAO[t1] + ",missing,exist, , , , \r\n";
                        }
                    }

                }

            }
            //if registratiedatum and startdate were found
            if (excludeList == 2)
                marker = "2";

            return new string[] { commastring, marker };

        }

        public string[] differenceValues(XmlNode nodeRiskco, XmlNode nodeAO, int numFRiskco, int numFAO, XmlNamespaceManager menRiskco, XmlNamespaceManager menAO, string x, string flag)
        {

            string commastring = "";

            string xmlRiskcooo = "<" + nodeRiskco.Name + " id=" + "\"" + nodeRiskco.Attributes["ext-id"].Value.ToString() + "\"" + " ext-id=" + "\"" + nodeRiskco.Attributes["ext-id"].Value.ToString() + "\"" + ">";
            xmlRiskcooo.Insert(xmlRiskcooo.Length, "</b>").Insert(0, "<b>");
            string xmlAOOO = "<" + nodeAO.Name + " id=" + "\"" + nodeAO.Attributes["id"].Value.ToString() + "\"" + " ext-id=" + "\"" + nodeAO.Attributes["ext-id"].Value.ToString() + "\"" + ">";
            xmlAOOO.Insert(xmlAOOO.Length, "</b>").Insert(0, "<b>");
            int comparisonCounterFeatures = 0;

            string xmlRiskco = "";
            string xmlAO = "";

            //this one will serve for the values comparisment 
            //string commastring1 = "";
            string[] namesRiskco = getFeaturesNames(nodeRiskco);
            string[] namesAO = getFeaturesNames(nodeAO);
            if (numFRiskco > numFAO)
            {

                for (int t1 = 0; t1 < numFRiskco; t1++)
                {
                    //comparing same features
                    if (namesAO.Contains(namesRiskco[t1]))
                    {
                        comparisonCounterFeatures++;
                        string selectFeature = "";
                        if (flag == "OP")
                            selectFeature = ".//deafult:" + namesRiskco[t1];
                        if (flag == "PP")
                            selectFeature = ".//df:" + namesRiskco[t1];
                        XmlNode featureRiskco = nodeRiskco.SelectSingleNode(selectFeature, menRiskco);
                        XmlNode featureAO = nodeAO.SelectSingleNode(selectFeature, menAO);

                        //the number of values inside the specific feature differs
                        if (featureRiskco.ChildNodes.Count != featureAO.ChildNodes.Count)
                        {
                            int numValuesRiskco = featureRiskco.ChildNodes.Count;
                            int numValuesAO = featureAO.ChildNodes.Count;
                        
                             

                            if ((!xmlRiskco.Contains(featureRiskco.Name)) && !xmlAO.Contains(featureAO.Name))
                            {
                                xmlRiskco += FormatXml(featureRiskco.OuterXml) + "\r\n";
                                xmlAO += FormatXml(featureAO.OuterXml) + "\r\n";

                                int cRRP = CountLinesInFile(FormatXml(featureRiskco.OuterXml));
                                int cAO = CountLinesInFile(FormatXml(featureAO.OuterXml));
//make the excel more readable, alýgn the lýnes from both Rýskco //and AOXml
                                if (cAO > cRRP)
                                {
                                    for (int i = 0; i < cAO - cRRP; i++)
                                    {

                                        xmlRiskco += "\r\n";
                                    }
                                }

                                if (cRRP > cAO)
                                {
                                    for (int i = 0; i < cRRP - cAO; i++)
                                    {

                                        xmlAO += "\r\n";
                                    }
                                }
                            }

                            commastring += x + namesRiskco[t1] + "," + featureRiskco.ChildNodes.Count + "," + featureAO.ChildNodes.Count + ", , , , \r\n";
                        }

                        //here we are on the level of comparing the same features from both files
                        // int min = Math.Min(featureRiskco.ChildNodes.Count, featureAO.ChildNodes.Count);

                        if (featureRiskco.ChildNodes.Count > featureAO.ChildNodes.Count)
                        {
                            for (int valueID = 0; valueID < featureRiskco.ChildNodes.Count; valueID++)
                            {

                                XmlNode valueRiskco = featureRiskco.ChildNodes[valueID];

                                string dateRiskco = valueRiskco.Attributes["date"].Value;

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
//only ýf ýts the approptiate date format 
                                        if ((Char.IsDigit(vR, 0) == true) && (Char.IsDigit(vA, 0) == true) && !(vR.Contains("-")) && !(vA.Contains("-")))
                                        {
                                            double innerRiskco = Convert.ToDouble(valueRiskco.InnerText);
                                            double innerAO = Convert.ToDouble(valueAO.InnerText);
//round the values found to two decimals
                                            innerRiskco = Math.Round(innerRiskco, 2);
                                            innerAO = Math.Round(innerAO, 2);

                                            if (innerRiskco != innerAO)
                                            {
                                                commastring += x + namesRiskco[t1] + "," + innerRiskco + "," + innerAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",value, \r\n";
                                               

                                                if ((!xmlRiskco.Contains(featureRiskco.Name)) && !xmlAO.Contains(featureAO.Name))
                                                {
                                                    xmlRiskco += FormatXml(nodeRiskco.SelectSingleNode(".//deafult:" + namesRiskco[t1], menRiskco).OuterXml) + "\r\n \r\n";
                                                    xmlAO += FormatXml(nodeAO.SelectSingleNode(".//deafult:" + namesRiskco[t1], menAO).OuterXml) + "\r\n \r\n";

                                                    int cRRP = CountLinesInFile(FormatXml(featureRiskco.OuterXml));
                                                    int cAO = CountLinesInFile(FormatXml(featureAO.OuterXml));

                                                    if (cAO > cRRP)
                                                    {
                                                        for (int i = 0; i < cAO - cRRP; i++)
                                                        {

                                                            xmlRiskco += "\r\n";
                                                        }
                                                    }

                                                    if (cRRP > cAO)
                                                    {
                                                        for (int i = 0; i < cRRP - cAO; i++)
                                                        {

                                                            xmlAO += "\r\n";
                                                        }
                                                    }
                                                }


                                             
                                            }
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

                                for (int valueIDAO = 0; valueIDAO < featureAO.ChildNodes.Count; valueIDAO++)
                                {
                                    XmlNode valueRiskco = featureRiskco.ChildNodes[valueIDAO];
                                    string dateRiskco = valueRiskco.Attributes["date"].Value;

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
                                            {
                                                commastring += x + namesRiskco[t1] + "," + innerRiskco + "," + innerAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",value, \r\n";
                                                if ((!xmlRiskco.Contains(featureRiskco.Name)) && !xmlAO.Contains(featureAO.Name))
                                                {
                                                    xmlRiskco += FormatXml(nodeRiskco.SelectSingleNode(".//deafult:" + namesRiskco[t1], menRiskco).OuterXml) + "\r\n \r\n";
                                                    xmlAO += FormatXml(nodeAO.SelectSingleNode(".//deafult:" + namesRiskco[t1], menAO).OuterXml) + "\r\n \r\n";

                                                    int cRRP = CountLinesInFile(FormatXml(featureRiskco.OuterXml));
                                                    int cAO = CountLinesInFile(FormatXml(featureAO.OuterXml));

                                                    if (cAO > cRRP)
                                                    {
                                                        for (int i = 0; i < cAO - cRRP; i++)
                                                        {

                                                            xmlRiskco += "\r\n";
                                                        }
                                                    }

                                                    if (cRRP > cAO)
                                                    {
                                                        for (int i = 0; i < cRRP - cAO; i++)
                                                        {

                                                            xmlAO += "\r\n";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }



                    }
                }
            }

            else
            { //AO has more features or the have the same
                for (int t1 = 0; t1 < numFAO; t1++)
                {
                    if (namesRiskco.Contains(namesAO[t1]))
                    {

                        comparisonCounterFeatures++;
                        string selectFeature = "";
                        if (flag == "OP")
                            selectFeature = ".//deafult:" + namesRiskco[t1];
                        if (flag == "PP")
                            selectFeature = ".//df:" + namesRiskco[t1];

                        XmlNode featureRiskco = nodeRiskco.SelectSingleNode(selectFeature, menRiskco);
                        XmlNode featureAO = nodeAO.SelectSingleNode(selectFeature, menAO);

                        //the number of values inside the specific feature differs
                        if (featureRiskco.ChildNodes.Count != featureAO.ChildNodes.Count)
                        {

                            if ((!xmlRiskco.Contains(featureRiskco.Name)) && !xmlAO.Contains(featureAO.Name))
                            {
                                xmlRiskco += FormatXml(featureRiskco.OuterXml) + "\r\n";
                                xmlAO += FormatXml(featureAO.OuterXml) + "\r\n";

                                int cRRP = CountLinesInFile(FormatXml(featureRiskco.OuterXml));
                                int cAO = CountLinesInFile(FormatXml(featureAO.OuterXml));

                                if (cAO > cRRP)
                                {
                                    for (int i = 0; i < cAO - cRRP; i++)
                                    {

                                        xmlRiskco += "\r\n";
                                    }
                                }

                                if (cRRP > cAO)
                                {
                                    for (int i = 0; i < cRRP - cAO; i++)
                                    {

                                        xmlAO += "\r\n";
                                    }
                                }
                            }

                            commastring += x + namesRiskco[t1] + "," + featureRiskco.ChildNodes.Count + "," + featureAO.ChildNodes.Count + ", , , , \r\n";
                        }


                        //going to the values part!!
                        if (featureRiskco.ChildNodes.Count > featureAO.ChildNodes.Count)
                        {
                            for (int valueID = 0; valueID < featureRiskco.ChildNodes.Count; valueID++)
                            {

                                XmlNode valueRiskco = featureRiskco.ChildNodes[valueID];

                                string dateRiskco = valueRiskco.Attributes["date"].Value;

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
                                            {
                                                commastring += x + namesRiskco[t1] + "," + innerRiskco + "," + innerAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",value, \r\n";
                                                if ((!xmlRiskco.Contains(featureRiskco.Name)) && !xmlAO.Contains(featureAO.Name))
                                                {
                                                    xmlRiskco += FormatXml(nodeRiskco.SelectSingleNode(".//deafult:" + namesRiskco[t1], menRiskco).OuterXml) + "\r\n \r\n";
                                                    xmlAO += FormatXml(nodeAO.SelectSingleNode(".//deafult:" + namesRiskco[t1], menAO).OuterXml) + "\r\n \r\n";

                                                    int cRRP = CountLinesInFile(FormatXml(featureRiskco.OuterXml));
                                                    int cAO = CountLinesInFile(FormatXml(featureAO.OuterXml));

                                                    if (cAO > cRRP)
                                                    {
                                                        for (int i = 0; i < cAO - cRRP; i++)
                                                        {

                                                            xmlRiskco += "\r\n";
                                                        }
                                                    }

                                                    if (cRRP > cAO)
                                                    {
                                                        for (int i = 0; i < cRRP - cAO; i++)
                                                        {

                                                            xmlAO += "\r\n";
                                                        }
                                                    }
                                                }
                                            }
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
                                            {
                                                commastring += x + namesRiskco[t1] + "," + innerRiskco + "," + innerAO + ",valueNode" + valueID + "," + "valueNode" + valueID + ",value, \r\n";
                                                if ((!xmlRiskco.Contains(featureRiskco.Name)) && !xmlAO.Contains(featureAO.Name))
                                                {
                                                    xmlRiskco += FormatXml(nodeRiskco.SelectSingleNode(".//deafult:" + namesRiskco[t1], menRiskco).OuterXml) + "\r\n \r\n";
                                                    xmlAO += FormatXml(nodeAO.SelectSingleNode(".//deafult:" + namesRiskco[t1], menAO).OuterXml) + "\r\n \r\n";

                                                    int cRRP = CountLinesInFile(FormatXml(featureRiskco.OuterXml));
                                                    int cAO = CountLinesInFile(FormatXml(featureAO.OuterXml));

                                                    if (cAO > cRRP)
                                                    {
                                                        for (int i = 0; i < cAO - cRRP; i++)
                                                        {

                                                            xmlRiskco += "\r\n";
                                                        }
                                                    }

                                                    if (cRRP > cAO)
                                                    {
                                                        for (int i = 0; i < cRRP - cAO; i++)
                                                        {

                                                            xmlAO += "\r\n";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                        }

                    }
                }
            }
            if (xmlRiskco == "")
                xmlRiskcooo = "";
            if (xmlAO == "")
                xmlAOOO = "";
            return new string[] { commastring, xmlRiskco, xmlAO, xmlRiskcooo, xmlAOOO };
        }

        public string[] getFeaturesNames(XmlNode node)
        {
            XmlNode featuresNode = node.ChildNodes[0];
            string[] namesArray = new string[featuresNode.ChildNodes.Count + 1];
            int counter = 0;
            foreach (XmlNode item in featuresNode)
            {

                namesArray[counter] = item.Name;
                counter++;
            }
            return namesArray;
        }


        //valuesNode counts the number of features in a node, the total number of values in them and the number of BDG links if needed
        public int[] valuesNode(XmlNode node, int flag)
        {
            XmlNodeList childNode = node.ChildNodes;
            XmlNode featureNode = childNode[0];
            XmlNode linksNode = childNode[1];

            if (!(featureNode == null))
            {
                //counting the number of features
                int numberOfFeatures = featureNode.ChildNodes.Count;
                //the total number of values detected in between the feature tags
                int totalNumberOfValues = 0;
                int numberBDGs = 0;

                // foreach node inside feature calculate the number of values
                foreach (XmlNode item in featureNode.ChildNodes)
                {
                    if (!(item.Name == "registratiedatum" || item.Name == "startdate"))
                        totalNumberOfValues += item.ChildNodes.Count;

                }

                //function call from the 'BDG' part count the number of BDG links
                if (flag == 1)
                    foreach (XmlNode item in linksNode.ChildNodes)
                    {
                        if (item.Name == "BDG")
                            numberBDGs = item.ChildNodes.Count;

                    }


                if (flag == 1)
                    return new int[] { numberOfFeatures, totalNumberOfValues, numberBDGs };

                else if (flag == 2)
                    return new int[] { numberOfFeatures, totalNumberOfValues };

                else
                    return new int[] { };
            }

            return new int[] { };
        }


        public string startDateCalc(XmlNode node)
        {
            string date = "";

            // foreach node inside feature calculate the number of values
            foreach (XmlNode item in node.ChildNodes)
            {

                if (item.Name == "dt_bdg_start" && item.ChildNodes[1] != null)
                    date = item.ChildNodes[1].InnerText;

            }
            return date;
        }

//for the excel file
        public string startDateCalcHelp(XmlNode node)
        {
            string date = "";


            foreach (XmlNode item in node.ChildNodes)
            {

                if (item.Name == "dt_bdg_start" && item.ChildNodes[1] != null)
                    date = "\r\n" + FormatXml(item.OuterXml) + "\r\n";

            }
            return date;
        }

        public string endDateCalc(XmlNode node)
        {
            string date = "";

            // foreach node inside feature calculate the number of values
            foreach (XmlNode item in node.ChildNodes)
            {

                if (item.Name == "dt_bdg_einde")
                {
                    //check just in case of invalid node
                    if (item.ChildNodes[1] == null)
                        date = "";
                    else
                        date = item.ChildNodes[1].InnerText;
                }
            }
            return date;
        }


        public string endDateCalcHelp(XmlNode node)
        {
            string date = "";

            // foreach node inside feature calculate the number of values
            foreach (XmlNode item in node.ChildNodes)
            {

                if (item.Name == "dt_bdg_einde")
                {
                    //check just in case of invalid node
                    if (item.ChildNodes[1] == null)
                        date = "";
                    else
                        date = "\r\n" + FormatXml(item.OuterXml) + "\r\n";
                }
            }
            return date;
        }
        //COMPARISON OF TWO FILES
        private void button1_Click(object sender, EventArgs e)
        {
            comparison(lblLocation.Text);


        }

        //main CLCIN comparison function
        private string[] comparison(string helppath, string firstIDAO = "")
        {

            string[] outputArray = new string[5];

            //list of possible differences
            string[] errorsList = { "numOfFeatures", "numOfValues", "numOfBDGLinks", "startingDate", "birthDate", "deathDate" };

            string fileRiskco = lblFirstFile.Text;
            string fileAO = lblSecondFile.Text;
            string bdginfoRRP = "";
            string bdginfoAO = "";
            string xmlR = "";
            string xmlA = "";
            //the output string
            string commaseparated = "";

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

            //flag for type of file
            bool typefile = false;

            //check if Natuurlijk_persoon node exist to no what kind of file we deal with
            if (xmlAO.DocumentElement.SelectSingleNode("//deafult:Natuurlijk_persoon", menAO) != null)
                typefile = true;


            //compare the BPFSAG part

            XmlNode bpfsagAO = xmlAO.DocumentElement.SelectSingleNode("//deafult:Bedrijfsgenoot_BPFSAG", menAO);
            XmlNode bpfsagRiskco = xmlRiskco.DocumentElement.SelectSingleNode("//deafult:Bedrijfsgenoot_BPFSAG", menRiskco);

            int[] valuesRiskco = valuesNode(bpfsagRiskco, 1);
            int[] valuesAO = valuesNode(bpfsagAO, 1);


            commaseparated += "RiskCoFile,A&OFile,idnode,idnode1,difference,difRiskCo,difA&O,valueNodeID1,valueNodeID2,flag, \r\n";

            string x = Path.GetFileNameWithoutExtension(lblFirstFile.Text) + ".clcin," + Path.GetFileNameWithoutExtension(lblSecondFile.Text) + ".clcin," + "Bedrijfsgenoot_BPFSAG,Bedrijfsgenoot_BPFSAG,";

            for (int i = 0; i < valuesAO.Count(); i++)
            {
                if (i == 0)
                {
                    int checkInt = 0;

                    //get the marker string from extra features, see if startdate and registratiedatum were detected
                    string check = extraFeatures(bpfsagRiskco, bpfsagAO, valuesRiskco[i], valuesAO[i], x)[1];


                    if (!check.Equals(""))
                        checkInt = int.Parse(check.ToString());


                    //if they were then reduce the number of features from the file they exist in
                    if (checkInt != 0)
                    {
                        if (valuesRiskco[i] == valuesAO[i] + checkInt)
                            valuesRiskco[i] = valuesRiskco[i] - checkInt;

                        if (valuesAO[i] == valuesRiskco[i] + checkInt)
                            valuesAO[i] = valuesAO[i] - checkInt;

                    }
                    else
                    {
                        //now get the extra features detected
                        commaseparated += extraFeatures(bpfsagRiskco, bpfsagAO, valuesRiskco[i], valuesAO[i], x)[0];

                    }
                }


                if (valuesRiskco[i] != valuesAO[i])
                {

                    commaseparated += x + errorsList[i] + "," + valuesRiskco[i].ToString() + "," + valuesAO[i].ToString() + ", , , ,\r\n";



                }
            }
            string[] output = new string[4];
            output = differenceValues(bpfsagRiskco, bpfsagAO, valuesRiskco[0], valuesAO[0], menRiskco, menAO, x, "OP");

            commaseparated += output[0];
            bdginfoRRP += output[3];
            bdginfoAO += output[4];
            if (!xmlR.Contains(bdginfoRRP))
                xmlR += bdginfoRRP + "\r\n";

            xmlR += output[1];

            if (!xmlA.Contains(bdginfoAO))
                xmlA += bdginfoAO + "\r\n";

            xmlA += output[2];



            //The BDG part
            //going to the links

            XmlNode bdgRiskco = xmlRiskco.DocumentElement.SelectSingleNode("//deafult:BDG", menRiskco);
            XmlNode bdgAO = xmlAO.DocumentElement.SelectSingleNode("//deafult:BDG", menAO);

            string[] idRiskcoBDGs = new string[bdgRiskco.ChildNodes.Count];
            string[] idAOBDGs = new string[bdgAO.ChildNodes.Count];

            string startendRiskco = "BDG_ID, bdg_start, bdg_end, \r\n";
            string startendAO = "BDG_ID, bdg_start, bdg_end, \r\n";
            int comparisonCounter = 0;

            int numLinksRiskco = bdgRiskco.ChildNodes.Count;
            int numLinksAO = bdgAO.ChildNodes.Count;


            string helpstring = Path.GetFileNameWithoutExtension(lblFirstFile.Text) + ".clcin," + Path.GetFileNameWithoutExtension(lblSecondFile.Text) + ".clcin,";

            //going trought each of the inner BDGs
            for (int i = 0; i < bdgRiskco.ChildNodes.Count; i++)
            {

                //this will give us the feature part of the inner BDG
                XmlNode innerBDGFeaturesRiskco = bdgRiskco.ChildNodes[i].ChildNodes[0];

                //getting the person ID
                idRiskcoBDGs[i] = bdgRiskco.ChildNodes[i].Attributes["ext-id"].Value;

                string startdateRiskco = startDateCalc(innerBDGFeaturesRiskco);

                string enddateRiskco = endDateCalc(innerBDGFeaturesRiskco);

                startendRiskco += idRiskcoBDGs[i] + "," + startdateRiskco + "," + enddateRiskco + ",\r\n";

                //calculating the numFeatures and numValues
                int[] valuesInnerBDGRiskco = valuesNode(bdgRiskco.ChildNodes[i], 2);

                //start with the comparison
                for (int k = 0; k < bdgAO.ChildNodes.Count; k++)
                {

                    XmlNode innerBDGFeaturesAO = bdgAO.ChildNodes[k].ChildNodes[0];

                    string startdateAO = startDateCalc(innerBDGFeaturesAO);

                    string enddateAO = endDateCalc(innerBDGFeaturesAO);
                    idAOBDGs[k] = bdgAO.ChildNodes[k].Attributes["id"].Value;

                    if (i == 0)
                        startendAO += idAOBDGs[k] + "," + startdateAO + "," + enddateAO + ",\r\n";

                    //we are comparing the BDGs that have the same starting date-those should be identical
                    if ((startdateRiskco.Equals(startdateAO) && enddateRiskco.Equals(enddateAO)) || (enddateAO == "" && startdateRiskco.Equals(startdateAO)))
                    {
                        comparisonCounter++;

                        int[] valuesInnerBDGAO = valuesNode(bdgAO.ChildNodes[k], 2);



                        for (int j = 0; j < valuesInnerBDGAO.Count(); j++)
                        {
                            if (j == 0)
                            {
                                int checkInt = 0;


                                string check = extraFeatures(bdgRiskco.ChildNodes[i], bdgAO.ChildNodes[k], valuesInnerBDGRiskco[0], valuesInnerBDGAO[0], helpstring + "BDG_ID_" + idRiskcoBDGs[i] + "," + "BDG_ID_" + idAOBDGs[k] + ",")[1];


                                if (!check.Equals(""))
                                    checkInt = int.Parse(check.ToString());



                                if (checkInt != 0)
                                {
                                    if (valuesInnerBDGRiskco[j] == valuesInnerBDGAO[j] + checkInt)
                                        valuesInnerBDGRiskco[j] = valuesInnerBDGRiskco[j] - checkInt;

                                    if (valuesInnerBDGAO[j] == valuesInnerBDGRiskco[j] + checkInt)
                                        valuesInnerBDGAO[j] = valuesInnerBDGAO[j] - checkInt;

                                }
                                else
                                {
                                    commaseparated += extraFeatures(bdgRiskco.ChildNodes[i], bdgAO.ChildNodes[k], valuesInnerBDGRiskco[j], valuesInnerBDGAO[j], helpstring + "BDG_ID_" + idRiskcoBDGs[i] + "," + "BDG_ID_" + idAOBDGs[k] + ",")[0];

                                }
                            }

                            if (valuesInnerBDGRiskco[j] != valuesInnerBDGAO[j])
                            {

                                commaseparated += helpstring + "BDG_ID_" + idRiskcoBDGs[i] + "," + "BDG_ID_" + idAOBDGs[k] + "," + errorsList[j] + "," + valuesInnerBDGRiskco[j].ToString() + "," + valuesInnerBDGAO[j].ToString() + ", , , , \r\n";



                            }



                        }

                        string h = differenceValues(bdgRiskco.ChildNodes[i], bdgAO.ChildNodes[k], valuesInnerBDGRiskco[0], valuesInnerBDGAO[0], menRiskco, menAO, helpstring + "BDG_ID_" + idRiskcoBDGs[i] + "," + "BDG_ID_" + idAOBDGs[k] + ",", "OP")[0];
                        commaseparated += h;
                        string[] helpr = differenceValues(bdgRiskco.ChildNodes[i], bdgAO.ChildNodes[k], valuesInnerBDGRiskco[0], valuesInnerBDGAO[0], menRiskco, menAO, helpstring + "BDG_ID_" + idRiskcoBDGs[i] + "," + "BDG_ID_" + idAOBDGs[k] + ",", "OP");
                        bdginfoRRP = helpr[3];
                        bdginfoAO = helpr[4];

                        if (!xmlR.Contains(bdginfoRRP))
                            xmlR += bdginfoRRP + "\r\n";

                        xmlR += helpr[1];


                        if (!xmlA.Contains(bdginfoAO))
                            xmlA += bdginfoAO + "\r\n";

                        xmlA += helpr[2];



                        if (helpr[1] != ""&&helpr[2]!="")
                        {
                            //put the start and end date to the xmls output

                            xmlA += "\r\n" + startDateCalcHelp(innerBDGFeaturesAO) + "\r\n";
                            xmlR += "\r\n" + startDateCalcHelp(innerBDGFeaturesRiskco) + "\r\n";

                            xmlA += "\r\n" + endDateCalcHelp(innerBDGFeaturesAO) + "\r\n";
                            xmlR += "\r\n" + endDateCalcHelp(innerBDGFeaturesRiskco) + "\r\n";
                        }


                    }//end string equals check


                }

            }


            //the BPFSAG_Regelingkenmerken part
            XmlNode regelingRiskco = bpfsagRiskco.ChildNodes[1].SelectSingleNode(".//deafult:BPFSAG_Regelingkenmerken", menRiskco).FirstChild;
            XmlNode regelingAO = bpfsagAO.ChildNodes[1].SelectSingleNode(".//deafult:BPFSAG_Regelingkenmerken", menAO).FirstChild;

            int[] valuesRegelingRiskco = valuesNode(regelingRiskco, 2);
            int[] valuesRegelingAO = valuesNode(regelingAO, 2);

            string x1 = Path.GetFileNameWithoutExtension(lblFirstFile.Text) + ".clcin," + Path.GetFileNameWithoutExtension(lblSecondFile.Text) + ".clcin," + "BPFSAG_Regelingkenmerken,BPFSAG_Regelingkenmerken,";

            for (int i = 0; i < valuesRegelingAO.Count(); i++)
            {
                if (i == 0)
                {
                    int checkInt = 0;

                    string check = extraFeatures(regelingRiskco, regelingAO, valuesRegelingRiskco[0], valuesRegelingAO[0], x1)[1];


                    if (!check.Equals(""))
                        checkInt = int.Parse(check.ToString());



                    if (checkInt != 0)
                    {
                        if (valuesRegelingRiskco[i] == valuesRegelingAO[i] + checkInt)
                            valuesRegelingRiskco[i] = valuesRegelingRiskco[i] - checkInt;

                        if (valuesRegelingAO[i] == valuesRegelingRiskco[i] + checkInt)
                            valuesRegelingAO[i] = valuesRegelingAO[i] - checkInt;

                    }
                    else
                    {
                        commaseparated += extraFeatures(regelingRiskco, regelingAO, valuesRegelingRiskco[0], valuesRegelingAO[0], x1)[0];

                    }

                }
                if (valuesRegelingRiskco[i] != valuesRegelingAO[i])
                {

                    commaseparated += x1 + errorsList[i] + "," + valuesRegelingRiskco[i].ToString() + "," + valuesRegelingAO[i].ToString() + ", , , , \r\n";

                }

            }


            string[] hh = differenceValues(regelingRiskco, regelingAO, valuesRegelingRiskco[0], valuesRegelingAO[0], menRiskco, menAO, x1, "OP");
            commaseparated += hh[0];


            bdginfoRRP = hh[3];
            bdginfoAO = hh[4];

            if (!xmlR.Contains(bdginfoRRP))
                xmlR += bdginfoRRP + "\r\n";

            xmlR += hh[1];


            if (!xmlA.Contains(bdginfoAO))
                xmlA += bdginfoAO + "\r\n";

            xmlA += hh[2];



            //the BPFSAG_Productkenmerken part
            XmlNode productRiskco = bpfsagRiskco.ChildNodes[1].SelectSingleNode(".//deafult:BPFSAG_Productkenmerken", menRiskco).FirstChild;
            XmlNode productAO = bpfsagAO.ChildNodes[1].SelectSingleNode(".//deafult:BPFSAG_Productkenmerken", menAO).FirstChild;

            int[] valuesProductRiskco = valuesNode(productRiskco, 2);
            int[] valuesProductAO = valuesNode(productAO, 2);

            string x2 = Path.GetFileNameWithoutExtension(lblFirstFile.Text) + ".clcin," + Path.GetFileNameWithoutExtension(lblSecondFile.Text) + ".clcin," + "BPFSAG_Productkenmerken,BPFSAG_Productkenmerken,";

            for (int i = 0; i < valuesProductAO.Count(); i++)
            {
                if (i == 0)
                {
                    int checkInt = 0;


                    string check = extraFeatures(productRiskco, productAO, valuesProductRiskco[0], valuesProductAO[0], x2)[1];


                    if (!check.Equals(""))
                        checkInt = int.Parse(check.ToString());



                    if (checkInt != 0)
                    {
                        if (valuesProductRiskco[i] == valuesProductAO[i] + checkInt)
                            valuesProductRiskco[i] = valuesProductRiskco[i] - checkInt;

                        if (valuesProductAO[i] == valuesProductRiskco[i] + checkInt)
                            valuesProductAO[i] = valuesProductAO[i] - checkInt;

                    }
                    else
                    {
                        commaseparated += extraFeatures(productRiskco, productAO, valuesProductRiskco[i], valuesProductAO[i], x2)[0];

                    }

                }
                if (valuesProductRiskco[i] != valuesProductAO[i])
                {

                    commaseparated += x2 + errorsList[i] + "," + valuesProductRiskco[i].ToString() + "," + valuesProductAO[i].ToString() + ", , , , \r\n";

                }

            }
            string[] hh1 = differenceValues(productRiskco, productAO, valuesProductRiskco[0], valuesProductAO[0], menRiskco, menAO, x2, "OP");
            commaseparated += hh1[0];


            bdginfoRRP = hh1[3];
            bdginfoAO = hh1[4];

            if (!xmlR.Contains(bdginfoRRP))
                xmlR += bdginfoRRP + "\r\n";

            xmlR += hh1[1];


            if (!xmlA.Contains(bdginfoAO))
                xmlA += bdginfoAO + "\r\n";

            xmlA += hh1[2];

            //the optional BPFSAG_Partner / BPFSAG_Kind part
            if (typefile == true)
            {

                int[] childFVRiskco = new int[2];
                int[] childFVAO = new int[2];
                int[] partnerFVRiskco = new int[2];
                int[] partnerFVAO = new int[2];

                XmlNode nodePartnerRiskco = null;
                XmlNode nodePartnerAO = null;
                XmlNode nodeChildRiskco = null;
                XmlNode nodeChildAO = null;



                //Riskco
                //Partner node

                if (xmlRiskco.DocumentElement.SelectSingleNode(".//deafult:BPFSAG_Partner", menRiskco) != null)
                {
                    nodePartnerRiskco = xmlRiskco.DocumentElement.SelectSingleNode(".//deafult:BPFSAG_Partner", menRiskco).FirstChild;
                    partnerFVRiskco = valuesNode(nodePartnerRiskco, 2);
                }

                //Child node

                if (xmlRiskco.DocumentElement.SelectSingleNode(".//deafult:BPFSAG_Kind", menRiskco) != null)
                {
                    nodeChildRiskco = xmlRiskco.DocumentElement.SelectSingleNode(".//deafult:BPFSAG_Kind", menRiskco).FirstChild;
                    childFVRiskco = valuesNode(nodeChildRiskco, 2);
                }


                //A&O
                //Partner node

                if (xmlAO.DocumentElement.SelectSingleNode(".//deafult:BPFSAG_Partner", menAO).FirstChild != null)
                {
                    nodePartnerAO = xmlAO.DocumentElement.SelectSingleNode(".//deafult:BPFSAG_Partner", menAO).FirstChild;
                    partnerFVAO = valuesNode(nodePartnerAO, 2);
                }
                //Child node

                if (xmlAO.DocumentElement.SelectSingleNode(".//deafult:BPFSAG_Kind", menAO).FirstChild != null)
                {
                    nodeChildAO = xmlAO.DocumentElement.SelectSingleNode(".//deafult:BPFSAG_Kind", menAO).FirstChild;
                    childFVAO = valuesNode(nodeChildAO, 2);
                }


                if (partnerFVAO.Length != 0)
                    for (int i = 0; i < partnerFVAO.Length; i++)
                    {
                        if (partnerFVRiskco[i] != partnerFVAO[i])
                            commaseparated += Path.GetFileNameWithoutExtension(lblFirstFile.Text) + ".clcin," + Path.GetFileNameWithoutExtension(lblSecondFile.Text) + ".clcin," + "BPFSAG_Partner,BPFSAG_Partner," + errorsList[i] + "," + partnerFVRiskco[i].ToString() + "," + partnerFVAO[i].ToString() + ", , , ,\r\n";
                    }

                if (childFVAO.Length != 0)
                    for (int i = 0; i < childFVAO.Length; i++)
                    {
                        if (childFVRiskco[i] != childFVAO[i])
                            commaseparated += Path.GetFileNameWithoutExtension(lblFirstFile.Text) + ".clcin," + Path.GetFileNameWithoutExtension(lblSecondFile.Text) + ".clcin," + "BPFSAG_Kind,BPFSAG_Kind," + errorsList[i] + "," + childFVRiskco[i].ToString() + "," + childFVAO[i].ToString() + ", , , ,\r\n";
                    }

                if (partnerFVRiskco[0] != 0 && partnerFVAO[0] != 0)
                {
                    string[] hh2 = differenceValues(nodePartnerRiskco, nodePartnerAO, partnerFVRiskco[0], partnerFVAO[0], menRiskco, menAO, Path.GetFileNameWithoutExtension(lblFirstFile.Text) + ".clcin," + Path.GetFileNameWithoutExtension(lblSecondFile.Text) + ".clcin,BPFSAG_Partner,BPFSAG_Partner,", "OP");
                    commaseparated += hh2[0];




                    bdginfoRRP = hh2[3];
                    bdginfoAO = hh2[4];

                    if (!xmlR.Contains(bdginfoRRP))
                        xmlR += bdginfoRRP + "\r\n";

                    xmlR += hh2[1];


                    if (!xmlA.Contains(bdginfoAO))
                        xmlA += bdginfoAO + "\r\n";

                    xmlA += hh2[2];
                }
                if (childFVRiskco[0] != 0 && childFVAO[0] != 0)
                {
                    string[] hh3 = differenceValues(nodeChildRiskco, nodeChildAO, childFVRiskco[0], childFVAO[0], menRiskco, menAO, Path.GetFileNameWithoutExtension(lblFirstFile.Text) + ".clcin," + Path.GetFileNameWithoutExtension(lblSecondFile.Text) + ".clcin,BPFSAG_Kind,BPFSAG_Kind,", "OP");
                    commaseparated += hh3[0];
                    bdginfoRRP = hh3[3];
                    bdginfoAO = hh3[4];

                    if (!xmlR.Contains(bdginfoRRP))
                        xmlR += bdginfoRRP + "\r\n";

                    xmlR += hh3[1];


                    if (!xmlA.Contains(bdginfoAO))
                        xmlA += bdginfoAO + "\r\n";

                    xmlA += hh3[2];


                }

                //now the Natuurlijk part
                XmlNode natRiskco = xmlRiskco.DocumentElement.SelectSingleNode(".//deafult:Natuurlijk_persoon", menRiskco);
                XmlNode natAO = xmlAO.DocumentElement.SelectSingleNode(".//clc:CalculationData", menAO).LastChild;

                string birthdateRiskco = natRiskco.SelectSingleNode(".//deafult:dt_geboorte", menRiskco).FirstChild.InnerText;
                string birthdateAO = natAO.SelectSingleNode(".//deafult:dt_geboorte", menAO).FirstChild.InnerText;

                if (!birthdateRiskco.Equals(birthdateAO))
                    commaseparated += Path.GetFileNameWithoutExtension(lblFirstFile.Text) + ".clcin," + Path.GetFileNameWithoutExtension(lblSecondFile.Text) + ".clcin," + "Natuurlijk_persoon,Natuurlijk_persoon," + errorsList[4] + "," + birthdateRiskco + "," + birthdateAO + ", , , ,\r\n";

                string deathdateRiskco = natRiskco.SelectSingleNode(".//deafult:dt_overlijden", menRiskco).FirstChild.InnerText;
                string deathdateAO = natAO.SelectSingleNode(".//deafult:dt_overlijden", menAO).FirstChild.InnerText;

                if (!deathdateRiskco.Equals(deathdateAO))
                    commaseparated += Path.GetFileNameWithoutExtension(lblFirstFile.Text) + ".clcin," + Path.GetFileNameWithoutExtension(lblSecondFile.Text) + ".clcin," + "Natuurlijk_persoon,Natuurlijk_persoon," + errorsList[5] + "," + deathdateRiskco + "," + deathdateAO + ", , , ,\r\n";

            }



            int numberOfLines = CountLinesInFile(commaseparated);
            //write a file only if there are differences noticed
            if (numberOfLines != 1)
            {

                string outputFileName = Path.GetFileNameWithoutExtension(lblSecondFile.Text) + "_&_" + Path.GetFileNameWithoutExtension(lblFirstFile.Text);

                string help1 = helppath + "\\" + outputFileName + ".txt";
                System.IO.StreamWriter file = new System.IO.StreamWriter(@help1);
                file.WriteLine(commaseparated);
                file.Close();
            }
            outputArray[0] = commaseparated;
            outputArray[1] = startendRiskco;
            outputArray[2] = startendAO;
            outputArray[3] = xmlR;
            outputArray[4] = xmlA;
            return outputArray;

        }
        //SINGLE REPORT
        private void button1_Click_1(object sender, EventArgs e)
        {
            XmlReaderSettings readerSettings = new XmlReaderSettings();

            readerSettings.IgnoreComments = true;

            XmlReader reader = XmlReader.Create(@lblThirdFile.Text, readerSettings);
            XmlDocument doc1 = new XmlDocument();
            doc1.Load(reader);

            XmlElement root = doc1.DocumentElement;


            XmlNamespaceManager men = new XmlNamespaceManager(doc1.NameTable);
            men.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
            //Riskco and A&O use different deafult namespace
            if (lblThirdFile.Text.Contains("Case"))
                men.AddNamespace("deafult", "Deployment:Bedrijfsgenoot_BPFSAG");
            else
                men.AddNamespace("deafult", "http://www.example.org/Bedrijfsgenoot_BPFSAG");




            //getting to the Bedrijfsgenoot_BPFSAG tag
            XmlNode BPFSAG;
            BPFSAG = (XmlNode)root.ChildNodes[1].FirstChild;

            //list of his child nodes
            XmlNodeList childBPFSAG = BPFSAG.ChildNodes;


            XmlNode featureNode = childBPFSAG[0];
            XmlNode linksNode = childBPFSAG[1];

            //working with feature part
            //counting the number of features
            int numberOfFeatures = featureNode.ChildNodes.Count;
            //the total number of values detected in between the feature tags
            int totalNumberOfValues = 0;


            // foreach node inside feature calculate the number of values
            foreach (XmlNode item in featureNode.ChildNodes)
            {

                totalNumberOfValues += item.ChildNodes.Count;
            }

            //till now we have the Nr of Features and the total Nr of Values

            //number of BDG Links
            int numBDGLinks = linksNode.ChildNodes.Count;
            int[] featuresPerBDG = new int[100];
            //try this later
            //XmlNode BDG = linksNode.SelectSingleNode(".//BPFSAG_Regelingkenmerken");

            List<List<int>> results = new List<List<int>>();

            List<List<string>> names = new List<List<string>>();
            List<DateTime> bdg_start = new List<DateTime>();
            int[] vpf_RegelingkenmerkenValues = new int[150];
            int[] vpf_ProductkenmerkenValues = new int[150];
            string[] vpf_RegelingkenmerkenNames = new string[150];
            string[] vpf_ProductkenmerkenNames = new string[150];
            int counterLinks = 0;
            List<BDG> BDGobjects = new List<BDG>();


            int[] totNumValues = new int[150];

            //the BDGs calculations
            foreach (XmlNode item in linksNode.ChildNodes)
            {

                if (item.Name == "BDG")
                {

                    int numberBDGLinks = item.ChildNodes.Count;

                    //per each BDG

                    foreach (XmlNode innerBDG in item.ChildNodes)
                    {

                        BDG obj = new BDG();

                        XmlNode fInnerBDG = innerBDG.ChildNodes[0];
                        //new
                        obj.id = counterLinks;

                        featuresPerBDG[counterLinks] = fInnerBDG.ChildNodes.Count;
                        List<int> helpListValues = new List<int>();
                        List<string> helpListNames = new List<string>();
                        int countValues = 0;
                        //and now we can work with it
                        foreach (XmlNode fBDG in fInnerBDG.ChildNodes)
                        {
                            //get dt_bdg_start values
                            if (fBDG.Name == "dt_bdg_start")
                            {
                                //new
                                obj.BDGStart = Convert.ToDateTime(fBDG.ChildNodes[1].InnerText);

                                bdg_start.Add(Convert.ToDateTime(fBDG.ChildNodes[1].InnerText));
                                // test += fBDG.ChildNodes[1].InnerText + "\n\r";
                            }
                            //now we are one level lower than the feature tags-to calculate the number of values
                            helpListValues.Add(fBDG.ChildNodes.Count);
                            //features coresponding the values
                            helpListNames.Add(fBDG.Name);
                            countValues += fBDG.ChildNodes.Count;
                        }
                        totNumValues[counterLinks] = countValues;
                        results.Add(helpListValues);
                        names.Add(helpListNames);
                        BDGobjects.Add(obj);
                        countValues = 0;
                        counterLinks++;
                    }


                }

                if (item.Name == "BPFSAG_Regelingkenmerken")
                {

                    XmlNode actualBDG = item.FirstChild;
                    XmlNode featuresBDG = actualBDG.ChildNodes[0];
                    vpf_RegelingkenmerkenValues = new int[featuresBDG.ChildNodes.Count];
                    vpf_RegelingkenmerkenNames = new string[featuresBDG.ChildNodes.Count];
                    int counter = 0;
                    //and now we can work with it
                    foreach (XmlNode fBDG in featuresBDG.ChildNodes)
                    {

                        //now we are one level lower than the feature tags-to calculate the number of values
                        vpf_RegelingkenmerkenValues[counter] = fBDG.ChildNodes.Count;
                        vpf_RegelingkenmerkenNames[counter] = fBDG.Name;
                        counter = counter + 1;
                    }


                }

                if (item.Name == "OP_Productpakket")
                {
                    //we are actually interested in his child node Productkenmerken
                    XmlNode linksProductpakket = item.FirstChild.ChildNodes[1];

                    //     XmlNode BPFSAG_Productkenmerken = linksProductpakket.FirstChild.FirstChild;
                    //  XmlNode BPFSAG_Productkenmerken = linksProductpakket.SelectSingleNode(".//deafult1:BPFSAG_Productkenmerken", men);
                    XmlNode BPFSAG_Productkenmerken = linksNode.SelectSingleNode(".//deafult:BPFSAG_Productkenmerken", men);
                    XmlNode featuresProductkenmerken = BPFSAG_Productkenmerken.FirstChild.ChildNodes[0];
                    vpf_ProductkenmerkenValues = new int[featuresProductkenmerken.ChildNodes.Count];
                    vpf_ProductkenmerkenNames = new string[featuresProductkenmerken.ChildNodes.Count];
                    int counter = 0;
                    //and now we can work with it
                    foreach (XmlNode fBDG in featuresProductkenmerken.ChildNodes)
                    {

                        //now we are one level lower than the feature tags-to calculate the number of values
                        vpf_ProductkenmerkenValues[counter] = fBDG.ChildNodes.Count;
                        vpf_ProductkenmerkenNames[counter] = fBDG.Name;
                        counter = counter + 1;
                    }

                }

            }
            string lines = "BPFSAG id, numOfFeatures, numOfValues, numBDGLinks \r\n";
            lines += "Bedrijfsgenoot_BPFSAG, " + numberOfFeatures.ToString() + "," + totalNumberOfValues.ToString() +
               "," + counterLinks.ToString() + "\r\n";

            ////now temp will have the start dates of all
            List<DateTime> temp = new List<DateTime>();
            foreach (BDG t in BDGobjects)
            {
                temp.Add(t.BDGStart);
            }
            //foreach (List<DateTime> a in bdg_start) {   
            //  temp.Add(a[0]);
            //}

            SortAscending(temp);

            int i = 500;
            lines += "\r\n BDG, bdg_start_date, featuresPerBDG, totalValues \r\n";
            foreach (DateTime a in temp)
            {
                foreach (BDG b in BDGobjects)
                {
                    if (DateTime.Compare(a, b.BDGStart) == 0)
                    {
                        i = b.id;
                        if (b.check == true)
                            continue;
                        else
                        {
                            //  lines += "\r\n";
                            lines += " BDG" + i.ToString() + ",";
                            lines += a.Date.ToString() + ", ";
                            lines += featuresPerBDG[i].ToString() + ", ";
                            lines += totNumValues[i].ToString() + " \r\n";
                            b.check = true;
                        }
                    }


                }

            }

            foreach (BDG b in BDGobjects)
            {

                b.check = false;
            }


            lines += "\r\n BDG, featureName, valuesPerFeature \r\n";
            foreach (DateTime str in temp)
            {
                foreach (BDG b in BDGobjects)
                {

                    if (DateTime.Compare(str, b.BDGStart) == 0)
                    {

                        i = b.id;
                        if (b.check == true)
                            continue;
                        else
                        {
                            List<int> helpValues = results[i];
                            List<string> helpNames = names[i];
                            // lines += "\r\n";


                            for (int j = 0; j < featuresPerBDG[i]; j++)
                            {

                                lines += " BDG" + i.ToString() + ",";
                                lines += helpNames[j] + ", ";
                                lines += helpValues[j].ToString();

                                lines += "\r\n";

                            }
                            b.check = true;

                        }
                    }

                }
            }

            lines += "\r\n BDG id, number of values";

            lines += "\r\n BPFSAG_Regelingkenmerken, " + vpf_RegelingkenmerkenValues.Length.ToString() + "\r\n";
            lines += "\r\n BDG id, featureName, valuesPerFeature \r\n";
            for (int j = 0; j < vpf_RegelingkenmerkenValues.Length; j++)
            {

                lines += "BPFSAG_Regelingkenmerken,";
                lines += vpf_RegelingkenmerkenNames[j] + ", ";
                lines += vpf_RegelingkenmerkenValues[j].ToString();

                lines += "\r\n";

            }
            //  lines = lines.Remove(lines.Length - 1, 1) + ";";
            lines += "\r\n BDG id, number of values";
            lines += "\r\n BPFSAG_Productkenmerken, " + vpf_ProductkenmerkenValues.Length.ToString() + "\r\n";

            lines += "\r\n BDG id, featureName, valuesPerFeature \r\n";
            for (int j = 0; j < vpf_ProductkenmerkenValues.Length; j++)
            {
                lines += "BPFSAG_Productkenmerken, ";

                lines += vpf_ProductkenmerkenNames[j] + ", ";
                lines += vpf_ProductkenmerkenValues[j].ToString();



                lines += "\r\n";
            }
            // lines = lines.Remove(lines.Length - 1, 1) + ";";
            // Write the string to a file.

            string outputFileName = "\\" + "Report_" + Path.GetFileNameWithoutExtension(lblThirdFile.Text) + ".txt";
            string help = lblLocation.Text + outputFileName;
            System.IO.StreamWriter file = new System.IO.StreamWriter(@help);
            file.WriteLine(lines);

            file.Close();
        }


        //ALL FILES generated-comparýng vast number of fýles //automatýcally!!!
        private void btnMultipleFiles_Click(object sender, EventArgs e)
        {


            string riskcoFolder = labelRiskcoFolder.Text;
            //for the last test check
            // string riskcoFolder = @"\\PC14TEMP\\c$\temp\4\ES";
            string aoFolder = labelFolderAO.Text;
            //the CLCOUT results folder
            string diffPersonPath = lblDiff.Text;

            if (diffPersonPath != "Select the folder with the CLCOUT results")
            {
                //AO directory
                System.IO.DirectoryInfo dirr = new System.IO.DirectoryInfo(@aoFolder);

                System.IO.DirectoryInfo[] subDirsAO = dirr.GetDirectories();

                //Riskco directory
                System.IO.DirectoryInfo dirrRiskco = new System.IO.DirectoryInfo(@riskcoFolder);

                System.IO.DirectoryInfo[] subDirsRiskco = dirrRiskco.GetDirectories();
                //mode can be ES, GBS or BS
                string mode = dirrRiskco.Name;
                //the excel file containing table information for the output
                string fileExcel = lblLocation.Text + "\\CLCINResults" + mode + ".xlsx";
                var excel = new ExcelPackage(new FileInfo(fileExcel));
                var ws = excel.Workbook.Worksheets.Add("Sheet1");




                int[] numRRP = new int[12];
                int[] numAO = new int[12];
                string[] pakket = new string[12];
                int[] good = new int[12];
                int[] bad = new int[12];
                int[] compared = new int[12];
                double[] percent = new double[12];
                //pakket counter
                int countPak = 0;




                foreach (System.IO.DirectoryInfo dirInfoR in subDirsRiskco)
                {



                    foreach (System.IO.DirectoryInfo dirInfoAO in subDirsAO)
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

                        //comparing files of the corresponding folders
                        if (rrpName == dirInfoAO.Name)
                        {
                            string fileDif = "";

                            riskcoFolder = dirInfoR.FullName;
                            aoFolder = dirInfoAO.FullName;

                            //get the file that gives info about the persons for which there is a difference between the clcout files 

                            fileDif = diffPersonPath + "\\" + rrpName + mode + "_CLCOUT\\extraFiles\\differencePersonIDs.txt";

                            //AO clcout files
                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@aoFolder);
                            System.IO.FileInfo[] files = dir.GetFiles("*.clcin");

                            //Riskco clcout files
                            System.IO.DirectoryInfo dirRiskco = new System.IO.DirectoryInfo(@riskcoFolder);
                            System.IO.FileInfo[] filesRiskco = dirRiskco.GetFiles("*.clcin");

                            //the current paket examined
                            string forPakket = dirInfoAO.Name;

                            countPak++;

                            string details = "";
                            int counterDif = 0;
                            int counterComp = 0;
                            //creating the separate paket folders
                            string folder = forPakket + "_" + mode + "_CLCIN";

                            string helppath = lblLocation.Text + "\\" + folder;


                            //different types of ID markers
                            string firstIDAOBPFSAG = "PersonID,";
                            string firstIDAOBDG = "PersonID,";
                            string firstIDAORegeling = "PersonID,";
                            string firstIDAOProductkenmerken = "PersonID,";
                            string firstIDAOrelatives = "PersonID,";

                            //creating new folders
                            string pathstartendBDGRiskco = helppath + "\\" + "startendBDGRiskco";
                            DirectoryInfo dir1 = Directory.CreateDirectory(pathstartendBDGRiskco);

                            string ex = helppath + "\\" + "excelFiles";
                            DirectoryInfo dir11 = Directory.CreateDirectory(ex);

                            string pathstartendBDGAO = helppath + "\\" + "startendBDGAO";
                            DirectoryInfo dir2 = Directory.CreateDirectory(pathstartendBDGAO);

                            string pathExtra = helppath + "\\" + "extraFiles";
                            DirectoryInfo dir3 = Directory.CreateDirectory(pathExtra);




                            details += "Number of RRP Files= " + filesRiskco.Count() + "; Number od AO Files=" + files.Count() + "\r\n fileRRP ; file AO; number of differences";
                            numRRP[countPak] = filesRiskco.Count();
                            numAO[countPak] = files.Count();
                            pakket[countPak] = forPakket;
                            string idPerson = "fileRiskco, fileA&O, PersonID, \r\n";
                            //files and appropriate number of bdg links
                            string fileNumBDG = "XML, number of BDGs, \r\n";

                            string noDifferencesID = "PersonID, \r\n";
                            string yesDifferencesID = "PersonID, \r\n";

                            string all_differences = "RiskCoFile,A&OFile,idnode,idnode1,difference,difRiskCo,difA&O,valueNodeID1,valueNodeID2,flag, \r\n";

                            string tablename = "";

                            //the differencePersonID file may not exist if the clcout are the same!
                            if (File.Exists(fileDif))
                                using (StreamReader rreader = new StreamReader(fileDif))
                                {

                                    //line contain person ID
                                    string linePerson;

                                    //going through each person that has a difference in clcout
                                    while ((linePerson = rreader.ReadLine()) != null)
                                    {     //looping trough each file in the AO folder
                                        int counter = 0;
                                        foreach (System.IO.FileInfo f in files)
                                        {

                                            //the file to compare with
                                            string firstIDAO = f.Name.Substring(0, 9);

                                            //comparing only appropriate files(same person ID) 
                                            if (firstIDAO == linePerson)
                                            {



                                                string fileAO = f.Name;
                                                string path = f.FullName;


                                                XmlReaderSettings readerSettings = new XmlReaderSettings();

                                                XmlReader reader = XmlReader.Create(@path, readerSettings);
                                                XmlDocument doc1 = new XmlDocument();
                                                doc1.Load(reader);



                                                XmlNamespaceManager menAO = new XmlNamespaceManager(doc1.NameTable);
                                                menAO.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
                                                menAO.AddNamespace("df", "Deployment:Bedrijfsgenoot_BPFSAG");
                                                tablename = doc1.DocumentElement.SelectSingleNode("clc:DeplR", menAO).Attributes["dep-name"].Value;

                                                ////needed to get the file from the Riskco folder
                                                string fileRiskco = "";
                                                string partnerIDAOFile = "";
                                                int starti = f.Name.IndexOf(";");
                                                int endi = f.Name.LastIndexOf(";");
                                                //the second id
                                                partnerIDAOFile = f.Name.Substring(starti + 1, endi - starti - 1);


                                                string pathR = riskcoFolder + "\\";

                                                //variable that marks if a comparison will be made
                                                bool exist = false;



                                                //checking if there is a file in the riskco folder with the id got from a&o
                                                foreach (System.IO.FileInfo fr in filesRiskco)
                                                {
                                                    //the first 10 letters of the riskco filename
                                                    string frisk = fr.Name.Substring(0, 9);

                                                    int startir = fr.Name.IndexOf(";");
                                                    int endir = fr.Name.LastIndexOf(";");
                                                    string partnerIDRFile = fr.Name.Substring(startir + 1, endir - startir - 1);


                                                    //if the corresponding IDs from the RRP and A&O files are the same then compare the files
                                                    if (frisk.Equals(firstIDAO) && partnerIDRFile.Equals(partnerIDAOFile))
                                                    {
                                                        fileRiskco = fr.Name;
                                                        exist = true;

                                                    }



                                                }

                                                idPerson += fileRiskco + ", " + fileAO + ", " + firstIDAO + ",\r\n";

                                                if (exist)
                                                {
                                                    counterComp++;

                                                    XmlReader reader1 = XmlReader.Create(@pathR + fileRiskco, readerSettings);
                                                    XmlDocument docR = new XmlDocument();
                                                    docR.Load(reader1);

                                                    XmlNamespaceManager menRiskco = new XmlNamespaceManager(docR.NameTable);
                                                    menRiskco.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
                                                    menRiskco.AddNamespace("deafult", "http://www.example.org/Bedrijfsgenoot_BPFSAG");

                                                    //getting the number of BDG Links
                                                    int numBDGAO = doc1.DocumentElement.SelectSingleNode(".//df:BDG", menAO).ChildNodes.Count;
                                                    int numBDGRiskco = docR.DocumentElement.SelectSingleNode(".//deafult:BDG", menRiskco).ChildNodes.Count;

                                                    fileNumBDG += fileRiskco + " , " + numBDGRiskco + ",\r\n" + fileAO + " , " + numBDGAO + ",\r\n \r\n";


                                                    //setting the label texts so that we can call button event
                                                    lblFirstFile.Text = riskcoFolder + "\\" + fileRiskco;
                                                    lblSecondFile.Text = aoFolder + "\\" + fileAO;
                                                    counter++;

                                                    //use the same function as comparing two files
                                                    string[] array = comparison(helppath, firstIDAO: firstIDAO);

                                                    //all the differences separated by commas
                                                    string helpstring = array[0];
                                                    //bdg start and end dates for RRP file
                                                    string startEndRiskco = array[1];
                                                    //bdg start and end dates for A&O file
                                                    string startEndAO = array[2];
                                                    //the differences in xml format for RRP
                                                    string xmlR = array[3];
                                                    //the differences in xml format for A&O
                                                    string xmlA = array[4];




                                                    //bdg_start and bdg_einde for riskco file
                                                    string textRiskco = "bdgDates" + firstIDAO + ".txt";
                                                    string bdgDatesOutputRiskco = pathstartendBDGRiskco + "\\" + textRiskco;
                                                    System.IO.StreamWriter file = new System.IO.StreamWriter(@bdgDatesOutputRiskco);
                                                    file.WriteLine(startEndRiskco);
                                                    file.Close();


                                                    int index = fileAO.IndexOf('.');
                                                    string textAO = fileAO.Substring(0, index + 1) + ".txt";

                                                    //bdg_start and bdg_einde for a&o file
                                                    string bdgDatesOutputAO = pathstartendBDGAO + "\\" + textAO;
                                                    System.IO.StreamWriter file1 = new System.IO.StreamWriter(@bdgDatesOutputAO);
                                                    file1.WriteLine(startEndAO);
                                                    file1.Close();



                                                    int numberOfLines = CountLinesInFile(helpstring);

                                                    details += fileRiskco + " ; " + fileAO + "; " + (numberOfLines - 1).ToString() + "\r\n";

                                                    //if only the header part is present in the diff file
                                                    if (numberOfLines == 1)
                                                    {
                                                        //no differences found in the current comparison
                                                        noDifferencesID += firstIDAO + ",\r\n";
                                                    }
                                                    else
                                                    {
                                                        //first remove the starting header line
                                                        helpstring = RemoveLine(helpstring, 0);

                                                        //index to start writing the xml output in the excel
                                                        int begin = CountLinesInFile(helpstring) + 4;


                                                        //creating a sheet of details excel file
                                                        if (!(xmlR == "" && xmlA == ""))
                                                        {

                                                            int i = fileRiskco.IndexOf('.');
                                                            string sheetname = fileRiskco.Substring(0, i);

                                                            string fileExcel1 = helppath + "\\excelFiles\\AllPersons" + forPakket + ".xlsx";

                                                            var excel1 = new ExcelPackage(new FileInfo(fileExcel1));
                                                            var ws1 = excel1.Workbook.Worksheets.Add(sheetname);
                                                            ws1.Cells[begin - 2, 2].Value = "RRP";
                                                            ws1.Cells[begin - 2, 4].Value = "A&O";

                                                            ws1.Cells[begin - 2, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                            ws1.Cells[begin - 2, 2].Style.Fill.BackgroundColor.SetColor(Color.Red);

                                                            ws1.Cells[begin - 2, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                            ws1.Cells[begin - 2, 4].Style.Fill.BackgroundColor.SetColor(Color.Red);

                                                            //start writing into the excel file
                                                            using (excel1)
                                                            {


                                                                //first write down all the differences in the first lines of the excel sheet
                                                                using (StringReader reader11h = new StringReader(helpstring))
                                                                {
                                                                    int row1h = 1;
                                                                    string lineeeh;
                                                                    //looping trought each line of the file
                                                                    while ((lineeeh = reader11h.ReadLine()) != null)
                                                                    {
                                                                        //   ws1.Cells[row1h, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                        //  ws1.Cells[row1h, 12].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);


                                                                        ws1.Cells[row1h, 1].Value = lineeeh;
                                                                        row1h++;
                                                                    }
                                                                }


                                                                //start writing the RRP xml file
                                                                using (StringReader reader11 = new StringReader(xmlR))
                                                                {
                                                                    int row1 = begin;
                                                                    string lineee;
                                                                    //looping trought each line of the file
                                                                    while ((lineee = reader11.ReadLine()) != null)
                                                                    {
                                                                        //meaning we are into the BDG tags
                                                                        if (lineee.Contains("id="))
                                                                        {
                                                                            ws1.Cells[row1, 2].Style.Font.Bold = true;
                                                                            ws1.Cells[row1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                            ws1.Cells[row1, 2].Style.Fill.BackgroundColor.SetColor(Color.Blue);

                                                                        }
                                                                        //for the feature name tags
                                                                        if (!lineee.Equals("") && !lineee.Equals(" "))
                                                                            if (lineee.Contains("xmlns=") || lineee.Substring(0, 2).Contains("</"))
                                                                            {
                                                                                ws1.Cells[row1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                                ws1.Cells[row1, 2].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                                                            }

                                                                        ws1.Cells[row1, 2].Value = lineee;
                                                                        row1++;




                                                                    }
                                                                }

                                                                //same for the A&O xml writing
                                                                using (StringReader reader111 = new StringReader(xmlA))
                                                                {
                                                                    int row2 = begin;
                                                                    string lineee1;
                                                                    //looping trought each line of the file
                                                                    while ((lineee1 = reader111.ReadLine()) != null)
                                                                    {
                                                                        if (lineee1.Contains("id="))
                                                                        {
                                                                            ws1.Cells[row2, 4].Style.Font.Bold = true;
                                                                            ws1.Cells[row2, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                            ws1.Cells[row2, 4].Style.Fill.BackgroundColor.SetColor(Color.Blue);
                                                                        }
                                                                        if (!lineee1.Equals("") && !lineee1.Equals(" "))
                                                                            if (lineee1.Contains("xmlns=") || lineee1.Substring(0, 2).Contains("</"))
                                                                            {
                                                                                ws1.Cells[row2, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                                ws1.Cells[row2, 4].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                                                            }
                                                                        ws1.Cells[row2, 4].Value = lineee1;
                                                                        row2++;
                                                                    }
                                                                }

                                                                ws1.Column(2).AutoFit();
                                                                ws1.Column(4).AutoFit();
                                                                excel1.Save();



                                                            }//end using excel




                                                        }
                                                        //end creation of details file


                                                        counterDif++;
                                                        yesDifferencesID += firstIDAO + ",\r\n";
                                                        all_differences += RemoveLine(helpstring, 1);
                                                        string line;
                                                        //do the check here about what kind of differences were noticed
                                                        using (StringReader rdr = new StringReader(helpstring))
                                                        {
                                                            while ((line = rdr.ReadLine()) != null)
                                                            {
                                                                string diff = line.Split(',')[3];

                                                                if (diff == "Bedrijfsgenoot_BPFSAG" && (!firstIDAOBPFSAG.Contains(firstIDAO)))
                                                                    firstIDAOBPFSAG += firstIDAO + ",\r\n";
                                                                else if (diff == "BPFSAG_Regelingkenmerken" && (!firstIDAORegeling.Contains(firstIDAO)))
                                                                    firstIDAORegeling += firstIDAO + ",\r\n";
                                                                else if (diff == "BPFSAG_Productkenmerken" && (!firstIDAOProductkenmerken.Contains(firstIDAO)))
                                                                    firstIDAOProductkenmerken += firstIDAO + ",\r\n";
                                                                else if (diff != "Bedrijfsgenoot_BPFSAG" && diff != "BPFSAG_Regelingkenmerken" && diff != "BPFSAG_Productkenmerken" && diff != "idnode1" && (!firstIDAOBDG.Contains(firstIDAO)))
                                                                    firstIDAOBDG += firstIDAO + ",\r\n";
                                                            }

                                                        }

                                                    }


                                                    fileAO = "";
                                                    fileRiskco = "";


                                                }



                                            }

                                        }//END FOREACH AO FILE

                                    }




                                }



                            //writing down all the results

                            double num = (double)counterDif / counterComp;
                            details += "Files Compared= " + counterComp.ToString() + "; Files Difference=" + counterDif.ToString() + "Files OK= " + (counterComp - counterDif).ToString() + " Percent of Sucess= " + (100 - num * 100).ToString() + "%";
                            string bdgLinksOutput1 = pathExtra + "\\" + "DETAILS.txt";

                            good[countPak] = counterComp - counterDif;
                            bad[countPak] = counterDif;
                            percent[countPak] = 100 - num * 100;
                            compared[countPak] = counterComp;

                            //string help = lblLocation.Text + test;
                            System.IO.StreamWriter fffile = new System.IO.StreamWriter(@bdgLinksOutput1);
                            fffile.WriteLine(details);
                            fffile.Close();


                            //output file with number of bdg links
                            string bdgLinksOutput = pathExtra + "\\" + "BDGLinksComparison.txt";
                            //string help = lblLocation.Text + test;
                            System.IO.StreamWriter ffile = new System.IO.StreamWriter(@bdgLinksOutput);
                            ffile.WriteLine(fileNumBDG);
                            ffile.Close();

                            //output of all differences
                            string allDiffOutput = pathExtra + "\\" + "allDifferences.txt";
                            System.IO.StreamWriter fille1 = new System.IO.StreamWriter(@allDiffOutput);
                            fille1.WriteLine(all_differences);
                            fille1.Close();

                            //output the person ids
                            string allDiffID = pathExtra + "\\" + "allIDS" + forPakket + ".txt";
                            System.IO.StreamWriter file3 = new System.IO.StreamWriter(@allDiffID);
                            file3.WriteLine(idPerson);
                            file3.Close();

                            //the good/diff file
                            string basicinfo = "There are " + (CountLinesInFile(noDifferencesID) - 1).ToString() + " comparison ended with no differences, "
                                + " and " + (CountLinesInFile(yesDifferencesID) - 1).ToString() + " comparison with differences found. ";
                            string basic = pathExtra + "\\" + "basicinfo" + forPakket + ".txt";
                            System.IO.StreamWriter file4 = new System.IO.StreamWriter(@basic);
                            file4.WriteLine(basicinfo);
                            file4.Close();

                            if (!yesDifferencesID.Equals(""))
                            //the IDs of persons when difference noticed
                            {
                                string diffID = pathExtra + "\\" + "differencePersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file5 = new System.IO.StreamWriter(@diffID);
                                file5.WriteLine(yesDifferencesID);
                                file5.Close();
                            }
                            if (!noDifferencesID.Equals(""))
                            {   //IDs of persons that had no differences
                                string nodiffID = pathExtra + "\\" + "NOdifferencePersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file6 = new System.IO.StreamWriter(@nodiffID);
                                file6.WriteLine(noDifferencesID);
                                file6.Close();
                            }
                            //The JustID parts
                            if (!firstIDAOBPFSAG.Equals(""))
                            {
                                string a = pathExtra + "\\" + "BPFSAGPersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file7 = new System.IO.StreamWriter(@a);
                                file7.WriteLine(firstIDAOBPFSAG);
                                file7.Close();
                            }
                            if (!firstIDAOBDG.Equals(""))
                            {
                                string b = pathExtra + "\\" + "BDGPersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file8 = new System.IO.StreamWriter(@b);
                                file8.WriteLine(firstIDAOBDG);
                                file8.Close();
                            }
                            if (!firstIDAORegeling.Equals(""))
                            {
                                string c = pathExtra + "\\" + "REGELINGPersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file9 = new System.IO.StreamWriter(@c);
                                file9.WriteLine(firstIDAORegeling);
                                file9.Close();
                            }
                            if (!firstIDAOProductkenmerken.Equals(""))
                            {
                                string d = pathExtra + "\\" + "PRODUCTKENMERKENPersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file10 = new System.IO.StreamWriter(@d);
                                file10.WriteLine(firstIDAOProductkenmerken);
                                file10.Close();
                            }

                            //call a function that gives seperate details about each feature difference detected
                            outputFileGenerator(all_differences, 1, helppath);

                            //database part
                            if (cbDatabase.Checked == true)
                            {
                                //first check if a database already exist at the folder location
                                //if it does, delete it and create a new one
                                if (File.Exists(@helppath + "\\DatabaseXml.accdb"))
                                {
                                    File.Delete(@helppath + "\\DatabaseXml.accdb");

                                }

                                //create an empty database
                                database_create(helppath);
                                //but first check if it exists

                                //fill the database with data
                                database(all_differences, tablename, helppath);

                                //go trought each file in the extra features folder
                                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@helppath + "\\" + "detailsDifferences");
                                //we know the number of clcin files in A&0
                                int numF = dir.GetFiles("*.clcin").Count();

                                System.IO.FileInfo[] filesDir = directory.GetFiles("*.txt");



                                //looping trough each file in the detailsDifferences folder


                                foreach (System.IO.FileInfo f in filesDir)
                                {
                                    string fAO = f.Name;
                                    int ind = fAO.IndexOf('.');
                                    string tname = fAO.Substring(0, ind);
                                    string text = "";

                                    using (StreamReader sr = new StreamReader(f.FullName))
                                    {
                                        text = sr.ReadToEnd();

                                    }


                                    database(text, tname, helppath);

                                }
                            }
                        }
                    }
                }

                //making the excel table
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

                // excel1.Save();
            }

                //same stuff without the output part
            else if (diffPersonPath=="Select the folder with the CLCOUT results")
            {

                //AO directory
                System.IO.DirectoryInfo dirr = new System.IO.DirectoryInfo(@aoFolder);

                System.IO.DirectoryInfo[] subDirsAO = dirr.GetDirectories();

                //Riskco directory
                System.IO.DirectoryInfo dirrRiskco = new System.IO.DirectoryInfo(@riskcoFolder);

                System.IO.DirectoryInfo[] subDirsRiskco = dirrRiskco.GetDirectories();
                //mode can be ES, GBS or BS
                string mode = dirrRiskco.Name;
                //the excel file containing table information for the output
                string fileExcel = lblLocation.Text + "\\CLCINResults" + mode + ".xlsx";
                var excel = new ExcelPackage(new FileInfo(fileExcel));
                var ws = excel.Workbook.Worksheets.Add("Sheet1");




                int[] numRRP = new int[12];
                int[] numAO = new int[12];
                string[] pakket = new string[12];
                int[] good = new int[12];
                int[] bad = new int[12];
                int[] compared = new int[12];
                double[] percent = new double[12];
                //pakket counter
                int countPak = 0;




                foreach (System.IO.DirectoryInfo dirInfoR in subDirsRiskco)
                {



                    foreach (System.IO.DirectoryInfo dirInfoAO in subDirsAO)
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

                        //comparing files of the corresponding folders
                        if (rrpName == dirInfoAO.Name)
                        {
                         

                            riskcoFolder = dirInfoR.FullName;
                            aoFolder = dirInfoAO.FullName;

                            
                            //AO clcout files
                            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@aoFolder);
                            System.IO.FileInfo[] files = dir.GetFiles("*.clcin");

                            //Riskco clcout files
                            System.IO.DirectoryInfo dirRiskco = new System.IO.DirectoryInfo(@riskcoFolder);
                            System.IO.FileInfo[] filesRiskco = dirRiskco.GetFiles("*.clcin");

                            //the current paket examined
                            string forPakket = dirInfoAO.Name;

                            countPak++;

                            string details = "";
                            int counterDif = 0;
                            int counterComp = 0;
                            //creating the separate paket folders
                            string folder = forPakket + "_" + mode + "_CLCIN";

                            string helppath = lblLocation.Text + "\\" + folder;


                            //different types of ID markers
                            string firstIDAOBPFSAG = "PersonID,";
                            string firstIDAOBDG = "PersonID,";
                            string firstIDAORegeling = "PersonID,";
                            string firstIDAOProductkenmerken = "PersonID,";
                            string firstIDAOrelatives = "PersonID,";

                            //creating new folders
                            string pathstartendBDGRiskco = helppath + "\\" + "startendBDGRiskco";
                            DirectoryInfo dir1 = Directory.CreateDirectory(pathstartendBDGRiskco);

                            string ex = helppath + "\\" + "excelFiles";
                            DirectoryInfo dir11 = Directory.CreateDirectory(ex);

                            string pathstartendBDGAO = helppath + "\\" + "startendBDGAO";
                            DirectoryInfo dir2 = Directory.CreateDirectory(pathstartendBDGAO);

                            string pathExtra = helppath + "\\" + "extraFiles";
                            DirectoryInfo dir3 = Directory.CreateDirectory(pathExtra);




                            details += "Number of RRP Files= " + filesRiskco.Count() + "; Number od AO Files=" + files.Count() + "\r\n fileRRP ; file AO; number of differences";
                            numRRP[countPak] = filesRiskco.Count();
                            numAO[countPak] = files.Count();
                            pakket[countPak] = forPakket;
                            string idPerson = "fileRiskco, fileA&O, PersonID, \r\n";
                            //files and appropriate number of bdg links
                            string fileNumBDG = "XML, number of BDGs, \r\n";

                            string noDifferencesID = "PersonID, \r\n";
                            string yesDifferencesID = "PersonID, \r\n";

                            string all_differences = "RiskCoFile,A&OFile,idnode,idnode1,difference,difRiskCo,difA&O,valueNodeID1,valueNodeID2,flag, \r\n";

                            string tablename = "";

                            //the differencePersonID file may not exist if the clcout are the same!
                        

                                    //line contain person ID
                                    string linePerson;

                                 
                                         //looping trough each file in the AO folder
                                        int counter = 0;
                                        foreach (System.IO.FileInfo f in files)
                                        {

                                            //the file to compare with
                                            string firstIDAO = f.Name.Substring(0, 9);

                                         


                                                string fileAO = f.Name;
                                                string path = f.FullName;


                                                XmlReaderSettings readerSettings = new XmlReaderSettings();

                                                XmlReader reader = XmlReader.Create(@path, readerSettings);
                                                XmlDocument doc1 = new XmlDocument();
                                                doc1.Load(reader);



                                                XmlNamespaceManager menAO = new XmlNamespaceManager(doc1.NameTable);
                                                menAO.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
                                                menAO.AddNamespace("df", "Deployment:Bedrijfsgenoot_BPFSAG");
                                                tablename = doc1.DocumentElement.SelectSingleNode("clc:DeplR", menAO).Attributes["dep-name"].Value;

                                                ////needed to get the file from the Riskco folder
                                                string fileRiskco = "";
                                                string partnerIDAOFile = "";
                                                int starti = f.Name.IndexOf(";");
                                                int endi = f.Name.LastIndexOf(";");
                                                //the second id
                                                partnerIDAOFile = f.Name.Substring(starti + 1, endi - starti - 1);


                                                string pathR = riskcoFolder + "\\";

                                                //variable that marks if a comparison will be made
                                                bool exist = false;



                                                //checking if there is a file in the riskco folder with the id got from a&o
                                                foreach (System.IO.FileInfo fr in filesRiskco)
                                                {
                                                    //the first 10 letters of the riskco filename
                                                    string frisk = fr.Name.Substring(0, 9);

                                                    int startir = fr.Name.IndexOf(";");
                                                    int endir = fr.Name.LastIndexOf(";");
                                                    string partnerIDRFile = fr.Name.Substring(startir + 1, endir - startir - 1);


                                                    //if the corresponding IDs from the RRP and A&O files are the same then compare the files
                                                    if (frisk.Equals(firstIDAO) && partnerIDRFile.Equals(partnerIDAOFile))
                                                    {
                                                        fileRiskco = fr.Name;
                                                        exist = true;

                                                    }



                                                }

                                                idPerson += fileRiskco + ", " + fileAO + ", " + firstIDAO + ",\r\n";

                                                if (exist)
                                                {
                                                    counterComp++;

                                                    XmlReader reader1 = XmlReader.Create(@pathR + fileRiskco, readerSettings);
                                                    XmlDocument docR = new XmlDocument();
                                                    docR.Load(reader1);

                                                    XmlNamespaceManager menRiskco = new XmlNamespaceManager(docR.NameTable);
                                                    menRiskco.AddNamespace("clc", "http://www.solcorp.com/ns/ProductXpress/CalculationInputOutput/CalculatorElement");
                                                    menRiskco.AddNamespace("deafult", "http://www.example.org/Bedrijfsgenoot_BPFSAG");

                                                    //getting the number of BDG Links
                                                    int numBDGAO = doc1.DocumentElement.SelectSingleNode(".//df:BDG", menAO).ChildNodes.Count;
                                                    int numBDGRiskco = docR.DocumentElement.SelectSingleNode(".//deafult:BDG", menRiskco).ChildNodes.Count;

                                                    fileNumBDG += fileRiskco + " , " + numBDGRiskco + ",\r\n" + fileAO + " , " + numBDGAO + ",\r\n \r\n";


                                                    //setting the label texts so that we can call button event
                                                    lblFirstFile.Text = riskcoFolder + "\\" + fileRiskco;
                                                    lblSecondFile.Text = aoFolder + "\\" + fileAO;
                                                    counter++;

                                                    //use the same function as comparing two files
                                                    string[] array = comparison(helppath, firstIDAO: firstIDAO);

                                                    //all the differences separated by commas
                                                    string helpstring = array[0];
                                                    //bdg start and end dates for RRP file
                                                    string startEndRiskco = array[1];
                                                    //bdg start and end dates for A&O file
                                                    string startEndAO = array[2];
                                                    //the differences in xml format for RRP
                                                    string xmlR = array[3];
                                                    //the differences in xml format for A&O
                                                    string xmlA = array[4];




                                                    //bdg_start and bdg_einde for riskco file
                                                    string textRiskco = "bdgDates" + firstIDAO + ".txt";
                                                    string bdgDatesOutputRiskco = pathstartendBDGRiskco + "\\" + textRiskco;
                                                    System.IO.StreamWriter file = new System.IO.StreamWriter(@bdgDatesOutputRiskco);
                                                    file.WriteLine(startEndRiskco);
                                                    file.Close();


                                                    int index = fileAO.IndexOf('.');
                                                    string textAO = fileAO.Substring(0, index + 1) + ".txt";

                                                    //bdg_start and bdg_einde for a&o file
                                                    string bdgDatesOutputAO = pathstartendBDGAO + "\\" + textAO;
                                                    System.IO.StreamWriter file1 = new System.IO.StreamWriter(@bdgDatesOutputAO);
                                                    file1.WriteLine(startEndAO);
                                                    file1.Close();



                                                    int numberOfLines = CountLinesInFile(helpstring);

                                                    details += fileRiskco + " ; " + fileAO + "; " + (numberOfLines - 1).ToString() + "\r\n";

                                                    //if only the header part is present in the diff file
                                                    if (numberOfLines == 1)
                                                    {
                                                        //no differences found in the current comparison
                                                        noDifferencesID += firstIDAO + ",\r\n";
                                                    }
                                                    else
                                                    {
                                                        //first remove the starting header line
                                                        helpstring = RemoveLine(helpstring, 0);

                                                        //index to start writing the xml output in the excel
                                                        int begin = CountLinesInFile(helpstring) + 4;


                                                        //creating a sheet of details excel file
                                                        if (!(xmlR == "" && xmlA == ""))
                                                        {

                                                            int i = fileRiskco.IndexOf('.');
                                                            string sheetname = fileRiskco.Substring(0, i);

                                                            string fileExcel1 = helppath + "\\excelFiles\\AllPersons" + forPakket + ".xlsx";

                                                            var excel1 = new ExcelPackage(new FileInfo(fileExcel1));
                                                            var ws1 = excel1.Workbook.Worksheets.Add(sheetname);
                                                            ws1.Cells[begin - 2, 2].Value = "RRP";
                                                            ws1.Cells[begin - 2, 4].Value = "A&O";

                                                            ws1.Cells[begin - 2, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                            ws1.Cells[begin - 2, 2].Style.Fill.BackgroundColor.SetColor(Color.Red);

                                                            ws1.Cells[begin - 2, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                            ws1.Cells[begin - 2, 4].Style.Fill.BackgroundColor.SetColor(Color.Red);

                                                            //start writing into the excel file
                                                            using (excel1)
                                                            {


                                                                //first write down all the differences in the first lines of the excel sheet
                                                                using (StringReader reader11h = new StringReader(helpstring))
                                                                {
                                                                    int row1h = 1;
                                                                    string lineeeh;
                                                                    //looping trought each line of the file
                                                                    while ((lineeeh = reader11h.ReadLine()) != null)
                                                                    {
                                                                        //   ws1.Cells[row1h, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                        //  ws1.Cells[row1h, 12].Style.Fill.BackgroundColor.SetColor(Color.LightYellow);


                                                                        ws1.Cells[row1h, 1].Value = lineeeh;
                                                                        row1h++;
                                                                    }
                                                                }


                                                                //start writing the RRP xml file
                                                                using (StringReader reader11 = new StringReader(xmlR))
                                                                {
                                                                    int row1 = begin;
                                                                    string lineee;
                                                                    //looping trought each line of the file
                                                                    while ((lineee = reader11.ReadLine()) != null)
                                                                    {
                                                                        //meaning we are into the BDG tags
                                                                        if (lineee.Contains("id="))
                                                                        {
                                                                            ws1.Cells[row1, 2].Style.Font.Bold = true;
                                                                            ws1.Cells[row1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                            ws1.Cells[row1, 2].Style.Fill.BackgroundColor.SetColor(Color.Blue);

                                                                        }
                                                                        //for the feature name tags
                                                                        if (!lineee.Equals("") && !lineee.Equals(" "))
                                                                            if (lineee.Contains("xmlns=") || lineee.Substring(0, 2).Contains("</"))
                                                                            {
                                                                                ws1.Cells[row1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                                ws1.Cells[row1, 2].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                                                            }

                                                                        ws1.Cells[row1, 2].Value = lineee;
                                                                        row1++;




                                                                    }
                                                                }

                                                                //same for the A&O xml writing
                                                                using (StringReader reader111 = new StringReader(xmlA))
                                                                {
                                                                    int row2 = begin;
                                                                    string lineee1;
                                                                    //looping trought each line of the file
                                                                    while ((lineee1 = reader111.ReadLine()) != null)
                                                                    {
                                                                        if (lineee1.Contains("id="))
                                                                        {
                                                                            ws1.Cells[row2, 4].Style.Font.Bold = true;
                                                                            ws1.Cells[row2, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                            ws1.Cells[row2, 4].Style.Fill.BackgroundColor.SetColor(Color.Blue);
                                                                        }
                                                                        if (!lineee1.Equals("") && !lineee1.Equals(" "))
                                                                            if (lineee1.Contains("xmlns=") || lineee1.Substring(0, 2).Contains("</"))
                                                                            {
                                                                                ws1.Cells[row2, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                                                ws1.Cells[row2, 4].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                                                            }
                                                                        ws1.Cells[row2, 4].Value = lineee1;
                                                                        row2++;
                                                                    }
                                                                }

                                                                ws1.Column(2).AutoFit();
                                                                ws1.Column(4).AutoFit();
                                                                excel1.Save();



                                                            }//end using excel




                                                        }
                                                        //end creation of details file


                                                        counterDif++;
                                                        yesDifferencesID += firstIDAO + ",\r\n";
                                                        all_differences += RemoveLine(helpstring, 1);
                                                        string line;
                                                        //do the check here about what kind of differences were noticed
                                                        using (StringReader rdr = new StringReader(helpstring))
                                                        {
                                                            while ((line = rdr.ReadLine()) != null)
                                                            {
                                                                string diff = line.Split(',')[3];

                                                                if (diff == "Bedrijfsgenoot_BPFSAG" && (!firstIDAOBPFSAG.Contains(firstIDAO)))
                                                                    firstIDAOBPFSAG += firstIDAO + ",\r\n";
                                                                else if (diff == "BPFSAG_Regelingkenmerken" && (!firstIDAORegeling.Contains(firstIDAO)))
                                                                    firstIDAORegeling += firstIDAO + ",\r\n";
                                                                else if (diff == "BPFSAG_Productkenmerken" && (!firstIDAOProductkenmerken.Contains(firstIDAO)))
                                                                    firstIDAOProductkenmerken += firstIDAO + ",\r\n";
                                                                else if (diff != "Bedrijfsgenoot_BPFSAG" && diff != "BPFSAG_Regelingkenmerken" && diff != "BPFSAG_Productkenmerken" && diff != "idnode1" && (!firstIDAOBDG.Contains(firstIDAO)))
                                                                    firstIDAOBDG += firstIDAO + ",\r\n";
                                                            }

                                                        }

                                                    }


                                                    fileAO = "";
                                                    fileRiskco = "";


                                                }



                                            

                                        }//END FOREACH AO FILE

                                    




                                



                            //writing down all the results

                            double num = (double)counterDif / counterComp;
                            details += "Files Compared= " + counterComp.ToString() + "; Files Difference=" + counterDif.ToString() + "Files OK= " + (counterComp - counterDif).ToString() + " Percent of Sucess= " + (100 - num * 100).ToString() + "%";
                            string bdgLinksOutput1 = pathExtra + "\\" + "DETAILS.txt";

                            good[countPak] = counterComp - counterDif;
                            bad[countPak] = counterDif;
                            percent[countPak] = 100 - num * 100;
                            compared[countPak] = counterComp;

                            //string help = lblLocation.Text + test;
                            System.IO.StreamWriter fffile = new System.IO.StreamWriter(@bdgLinksOutput1);
                            fffile.WriteLine(details);
                            fffile.Close();


                            //output file with number of bdg links
                            string bdgLinksOutput = pathExtra + "\\" + "BDGLinksComparison.txt";
                            //string help = lblLocation.Text + test;
                            System.IO.StreamWriter ffile = new System.IO.StreamWriter(@bdgLinksOutput);
                            ffile.WriteLine(fileNumBDG);
                            ffile.Close();

                            //output of all differences
                            string allDiffOutput = pathExtra + "\\" + "allDifferences.txt";
                            System.IO.StreamWriter fille1 = new System.IO.StreamWriter(@allDiffOutput);
                            fille1.WriteLine(all_differences);
                            fille1.Close();

                            //output the person ids
                            string allDiffID = pathExtra + "\\" + "allIDS" + forPakket + ".txt";
                            System.IO.StreamWriter file3 = new System.IO.StreamWriter(@allDiffID);
                            file3.WriteLine(idPerson);
                            file3.Close();

                            //the good/diff file
                            string basicinfo = "There are " + (CountLinesInFile(noDifferencesID) - 1).ToString() + " comparison ended with no differences, "
                                + " and " + (CountLinesInFile(yesDifferencesID) - 1).ToString() + " comparison with differences found. ";
                            string basic = pathExtra + "\\" + "basicinfo" + forPakket + ".txt";
                            System.IO.StreamWriter file4 = new System.IO.StreamWriter(@basic);
                            file4.WriteLine(basicinfo);
                            file4.Close();

                            if (!yesDifferencesID.Equals(""))
                            //the IDs of persons when difference noticed
                            {
                                string diffID = pathExtra + "\\" + "differencePersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file5 = new System.IO.StreamWriter(@diffID);
                                file5.WriteLine(yesDifferencesID);
                                file5.Close();
                            }
                            if (!noDifferencesID.Equals(""))
                            {   //IDs of persons that had no differences
                                string nodiffID = pathExtra + "\\" + "NOdifferencePersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file6 = new System.IO.StreamWriter(@nodiffID);
                                file6.WriteLine(noDifferencesID);
                                file6.Close();
                            }
                            //The JustID parts
                            if (!firstIDAOBPFSAG.Equals(""))
                            {
                                string a = pathExtra + "\\" + "BPFSAGPersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file7 = new System.IO.StreamWriter(@a);
                                file7.WriteLine(firstIDAOBPFSAG);
                                file7.Close();
                            }
                            if (!firstIDAOBDG.Equals(""))
                            {
                                string b = pathExtra + "\\" + "BDGPersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file8 = new System.IO.StreamWriter(@b);
                                file8.WriteLine(firstIDAOBDG);
                                file8.Close();
                            }
                            if (!firstIDAORegeling.Equals(""))
                            {
                                string c = pathExtra + "\\" + "REGELINGPersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file9 = new System.IO.StreamWriter(@c);
                                file9.WriteLine(firstIDAORegeling);
                                file9.Close();
                            }
                            if (!firstIDAOProductkenmerken.Equals(""))
                            {
                                string d = pathExtra + "\\" + "PRODUCTKENMERKENPersonIDs" + forPakket + ".txt";
                                System.IO.StreamWriter file10 = new System.IO.StreamWriter(@d);
                                file10.WriteLine(firstIDAOProductkenmerken);
                                file10.Close();
                            }

                            //call a function that gives seperate details about each feature difference detected
                            outputFileGenerator(all_differences, 1, helppath);

                            //database part
                            if (cbDatabase.Checked == true)
                            {
                                //first check if a database already exist at the folder location
                                //if it does, delete it and create a new one
                                if (File.Exists(@helppath + "\\DatabaseXml.accdb"))
                                {
                                    File.Delete(@helppath + "\\DatabaseXml.accdb");

                                }

                                //create an empty database
                                database_create(helppath);
                                //but first check if it exists

                                //fill the database with data
                                database(all_differences, tablename, helppath);

                                //go trought each file in the extra features folder
                                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@helppath + "\\" + "detailsDifferences");
                                //we know the number of clcin files in A&0
                                int numF = dir.GetFiles("*.clcin").Count();

                                System.IO.FileInfo[] filesDir = directory.GetFiles("*.txt");



                                //looping trough each file in the detailsDifferences folder


                                foreach (System.IO.FileInfo f in filesDir)
                                {
                                    string fAO = f.Name;
                                    int ind = fAO.IndexOf('.');
                                    string tname = fAO.Substring(0, ind);
                                    string text = "";

                                    using (StreamReader sr = new StreamReader(f.FullName))
                                    {
                                        text = sr.ReadToEnd();

                                    }


                                    database(text, tname, helppath);

                                }
                            }
                        }
                    }
                }

                //making the excel table
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

                // excel1.Save();
            }
        }

        public void outputFileGenerator(string allDiff, int flag, string outputLocation)
        {
            if (!(allDiff == ""))
            {
                string pathdetailsDifferences = outputLocation + "\\" + "detailsDifferences";
                DirectoryInfo dir1 = Directory.CreateDirectory(pathdetailsDifferences);

                //read the file containing all the differences
                using (StringReader reader = new StringReader(allDiff))
                {
                    int totNumOfLines = CountLinesInFile(allDiff);

                    string line;
                    string line1;
                    string diff = "";
                    string subDiff = "";
                    string diffCounter = "difference, occurences \r\n";
                    string[] diff_examined = new string[totNumOfLines];
                    diff_examined[0] = "xxxxxx";
                    int c = 1;
                    int line_counter = -1;


                    while ((line = reader.ReadLine()) != null)
                    {

                        line_counter++;

                        //this should be changed made more general
                        if (flag == 3)
                            diff = line.Split(',')[2];
                        if (flag == 1)
                            diff = line.Split(',')[4];


                        //the particular difference is already examined
                        if (!(diff_examined.Contains(diff)) && !(diff.Contains("difference")))


                        //dealing with new difference
                        //here we should create a new file
                        {
                            string linesIncluded = "RiskCoFile,A&OFile,idnode,idnode1,difRiskCo,difA&O,valueNodeID1,valueNodeID2,flag, \r\n";
                            //include the current line in the output file
                            //remove the difference from the line (needed for better reading the output)
                            string toRemove = diff + ",";
                            int index = line.IndexOf(toRemove);

                            string cleanLine = (index < 0)
                                ? line
                                : line.Remove(index, toRemove.Length);

                            linesIncluded += cleanLine + "\r\n";
                            cleanLine = "";
                            diff_examined[c] = diff;
                            c++;

                            diffCounter += diff + ",";

                            using (StringReader reader1 = new StringReader(allDiff))
                            {
                                while ((line1 = reader1.ReadLine()) != null)
                                {

                                    if (flag == 3)
                                        subDiff = line1.Split(',')[2];
                                    if (flag == 1)
                                        subDiff = line1.Split(',')[4];


                                    //we found a line with the same difference
                                    if (diff == subDiff)
                                    {
                                        string toRemove1 = subDiff + ",";
                                        int index1 = line1.IndexOf(toRemove1);
                                        string cleanLine1 = (index < 0)
                                            ? line1
                                            : line1.Remove(index1, toRemove1.Length);
                                        linesIncluded += cleanLine1 + "\r\n";
                                        cleanLine1 = "";
                                    }


                                }//end while
                            }//end using string reader1

                            //write the new file here!!!
                            string filename = diff + ".txt";

                            string diffOutput = pathdetailsDifferences + "\\" + filename;
                            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@diffOutput);
                            linesIncluded = RemoveLine(linesIncluded, 2);
                            diffCounter += (CountLinesInFile(linesIncluded) - 1).ToString() + "\r\n";
                            linesIncluded = linesIncluded.Insert(0, "RiskCoFile,A&OFile,idnode,idnode1,difRiskCo,difA&O,valueNodeID1,valueNodeID2,flag, \r\n");
                            file1.WriteLine(linesIncluded);
                            file1.Close();


                            linesIncluded = "";

                        }//end if case



                    }//end reading lines(while)

                    string diffC = pathdetailsDifferences + "\\" + "DIFFERENCES_COUNTER.txt";
                    System.IO.StreamWriter file2 = new System.IO.StreamWriter(@diffC);
                    file2.WriteLine(diffCounter);
                    file2.Close();

                }//end string reader
            }
        }

        //function for removing a line from a string
        public static string RemoveLine(string input, int i)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return string.Join(Environment.NewLine, lines.Skip(i));
        }

        public int CountLinesInFile(string f)
        {
            int count = 0;
            using (StringReader r = new StringReader(f))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    count++;
                }
            }
            return count;
        }

        private void btnDA_Click(object sender, EventArgs e)
        {
            Form2 detailsForm = new Form2();
            detailsForm.Show();
        }






        private void btnMutatie_Click(object sender, EventArgs e)
        {
            Form8 mutatieForm = new Form8();
            mutatieForm.Show();
        }


        private void database(string allD, string tablename, string helppath)
        {

            /////////////////////////////////////////////////////////////////

            //creating new table in the existing database

            string tableSQL = "CREATE TABLE " + tablename + " (ID TEXT(100), ";
            string tableInput = "INSERT INTO " + tablename + " ([ID],";
            string tableParameters = " VALUES (@ID,";

            //get the first line of the file
            string firstline = allD.Substring(0, allD.IndexOf(Environment.NewLine));

            //num of fields in the table
            int numFields = firstline.Split(',').Length - 1;
            bool hasPersonID = false;
            ADODB.Connection cnn = new ADODB.Connection();

            Table mainTable = new Table();
            mainTable.Name = tablename;

            ADOX.Catalog cat = new ADOX.Catalog();

            //start adding the columns to the table
            mainTable.Columns.Append("ID");

            for (int i = 0; i < numFields; i++)
            {
                string field = firstline.Split(',')[i];
                int index = field.IndexOf('&');
                if (index != -1)
                    field = field.Remove(index, 1);

                if (field == "PersonID")
                    hasPersonID = true;

                tableSQL += field + " TEXT(100)" + ", ";
                tableInput += "[" + field + "],";
                tableParameters += "@" + field + ",";
                mainTable.Columns.Append(field);


                //last part of the string
                if ((i == numFields - 1))
                {
                    tableSQL.Substring(0, tableSQL.Length - 3);
                    tableInput = tableInput.Substring(0, tableInput.Length - 1);
                    tableParameters = tableParameters.Substring(0, tableParameters.Length - 1);
                }

            }
            char lastInput = tableInput[tableInput.Length - 1];
            if (lastInput.Equals(','))
                tableInput = tableInput.Remove(tableInput.LastIndexOf(','), 1);
            char lastPar = tableParameters[tableParameters.Length - 1];
            if (lastPar.Equals(','))
                tableParameters = tableParameters.Remove(tableParameters.LastIndexOf(','), 1);
            //tableSQL = tableSQL.Remove(tableSQL.LastIndexOf(','), 1);
            //tableInput = tableInput.Remove(tableInput.LastIndexOf(','), 1);
            //tableParameters = tableParameters.Remove(tableParameters.LastIndexOf(','), 1);
            if (numFields > 1 && hasPersonID == false)
            {
                tableSQL += ",PersonID TEXT(100) )";
                tableInput += ",[PersonID] )";
                tableParameters += ",@PersonID )";

                mainTable.Columns.Append("PersonID");
            }
            else
            {
                tableSQL += ")";
                tableInput += ")";
                tableParameters += ")";
            }


            //add the table and close the connection (try to find better solution)
            string databasePath = helppath + "\\DatabaseXml.accdb";
            cnn.Open(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databasePath + ";" + "Jet OLEDB:Engine Type=5");
            cat.ActiveConnection = cnn;
            cat.Tables.Append(mainTable);



            if (cnn != null)
            {
                cnn.Close();
            }
            cat.ActiveConnection = null;
            cat = null;



            ///////////////////////////////////////////////////

            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databasePath);
            ////con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databasePath;
            con.Open();

            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;


            //the table is created , now fill it with data

            int id_counter = 0;
            string line = "";

            //delete the 'label row' from the input data
            allD = RemoveLine(allD, 1);


            using (StringReader reader = new StringReader(allD))
            {

                while ((line = reader.ReadLine()) != null && line != "")
                {

                    string b = line.Split(',')[0];
                    //for the personID
                    string l = b.Split('.')[0];
                    //check if we are dealing with valid line
                    if (!b.Equals("RiskCoFile"))
                    {
                        //first count the number of occurences
                        int count = line.Split(',').Length - 1;





                        id_counter++;
                        //now put the data in the database
                        string t = tableInput + tableParameters;

                        cmd.CommandText = t;
                        cmd.Parameters.AddWithValue("@ID", id_counter.ToString());
                        for (int i = 0; i < numFields; i++)
                        {

                            string valuefield = line.Split(',')[i];

                            string x = "@" + firstline.Split(',')[i];
                            cmd.Parameters.AddWithValue(x, valuefield);

                        }
                        cmd.Parameters.AddWithValue("@PersonID", l);

                        cmd.Connection = con;

                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();






                    }

                }






                //}//end reading the difference file

                con.Close();


            }
        }


        private void database_create(string helppath)
        {

            ADOX.Catalog cat = new ADOX.Catalog();
            string databasePath = helppath + "\\DatabaseXml.accdb";
            cat.Create(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + databasePath + "; Jet OLEDB:Engine Type=5");
            ADODB.Connection con = cat.ActiveConnection as ADODB.Connection;

            con.Close();
        }

        string FormatXml(string xml)
        {
            var stringBuilder = new StringBuilder();

            var element = XElement.Parse(xml);

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;

            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                element.Save(xmlWriter);
            }

            return stringBuilder.ToString();
        }


        public bool IsFileInUse(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("'path' cannot be null or empty.", "path");

            try
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read)) { }
            }
            catch (IOException)
            {

                return true;
            }

            return false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //file dialog
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = folderBrowserDialog1.SelectedPath;

                lblDiff.Text = file;
            }
        }



    }



    }
