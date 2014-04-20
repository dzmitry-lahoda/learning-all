using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using Itransition.Training.Data;
using System.Collections.Generic;

/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : System.Web.Services.WebService {

    public AutoComplete () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod]
    public String[] GetCompletionList(String prefixText)
    {
        List<String> list = new List<string>();
        foreach (Tag tag in TagEditor.GetTagsWithTitleLikeThis(prefixText))
        {
            list.Add(tag.Title);
        }
        return list.ToArray();
    }
    
}

