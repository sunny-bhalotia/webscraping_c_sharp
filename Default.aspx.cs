using System;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page
{
    ArrayList al;
    protected void Page_Load(object sender, EventArgs e)
    {
        al = new ArrayList();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        al.Clear(); //Clearing the arraylist on button click
        string txtKeyWords = "messi";
        HtmlAgilityPack.HtmlDocument htmlSnippet = new HtmlAgilityPack.HtmlDocument();
        ListBox1.Items.Clear();
        StringBuilder sb = new StringBuilder();
        byte[] ResultsBuffer = new byte[65536];
        string SearchResults = "http://google.com/search?q=" + txtKeyWords.Trim();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SearchResults);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        Stream resStream = response.GetResponseStream();
        string tempString = null;
        int count = 0;
        do
        {
            count = resStream.Read(ResultsBuffer, 0, ResultsBuffer.Length);
            if (count != 0)
            {
                tempString = Encoding.ASCII.GetString(ResultsBuffer, 0, count);
                sb.Append(tempString);
            }
        }

        while (count > 0);
        string sbb = sb.ToString();

        HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
        html.OptionOutputAsXml = true;
        html.LoadHtml(sbb);
        HtmlNode doc = html.DocumentNode;


        String resultlinks = "";
        foreach (HtmlNode link in doc.SelectNodes("//a[@href]"))
        {
            //HtmlAttribute att = link.Attributes["href"];
            string hrefValue = link.GetAttributeValue("href", string.Empty);
            if (!hrefValue.ToString().ToUpper().Contains("GOOGLE") && hrefValue.ToString().Contains("/url?q=") && hrefValue.ToString().ToUpper().Contains("HTTP://"))
            {
                int index = hrefValue.IndexOf("&");
                if (index > 0)
                {
                    hrefValue = hrefValue.Substring(0, index);
                    String x = hrefValue.Replace("/url?q=", "").ToString();
                    resultlinks += x + "\n";
                    if (al.Contains(x) == false) {
                        ListBox1.Items.Add(x);
                        al.Add(x); //adding results to arraylist
                    }
                    
                }
            }
        }
        //TextBox1.Text = al.Count.ToString();
        TextBox1.Text = get_result_from_a_website(al[8].ToString());
    }


   /* public String get_text_from_link(String url)
    {
        String result = "";

        var document = new HtmlWeb().Load(url);
        string htmlText = document.DocumentNode.InnerHtml;


        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(htmlText);

        var nodes = doc.DocumentNode.SelectNodes("//script|//style");

        foreach (var node in nodes)
            node.ParentNode.RemoveChild(node);

        string htmlOutput = doc.DocumentNode.OuterHtml;

        foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//p"))
        {
            string x = link.InnerText;
            if((x.Contains("#")==false)&& (x.Contains("@") == false) && (x.Contains("<") == false) && (x.Contains(">") == false))
            result = result + link.InnerText;
        }
        return result;
    }
    */

    public String get_result_from_a_website(String url)
    {
        StringBuilder sb = new StringBuilder();
        byte[] ResultsBuffer = new byte[65536];
        string SearchResults = "https://www.w3.org/services/html2txt?url=" + url.Trim();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SearchResults);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        Stream resStream = response.GetResponseStream();
        string tempString = null;
        int count = 0;
        do
        {
            count = resStream.Read(ResultsBuffer, 0, ResultsBuffer.Length);
            if (count != 0)
            {
                tempString = Encoding.ASCII.GetString(ResultsBuffer, 0, count);
                sb.Append(tempString);
            }
        }

        while (count > 0);
        string sbb = sb.ToString();

        string pattern = "\\[[^]]*\\]";
        string replacement = "";
        Regex rgx = new Regex(pattern);
        string result = rgx.Replace(sbb, replacement);

        //To remove [xyz] which that website returns.



        return result;
    }
}