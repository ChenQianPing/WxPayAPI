using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WxPayAPI.ashx
{
    /// <summary>
    /// pay 的摘要说明
    /// </summary>
    public class pay : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string openid = HttpContext.Current.Request.QueryString["openid"];
            string total_fee = HttpContext.Current.Request.QueryString["total_fee"];
            JsApiPay jsApiPay = new JsApiPay(context);
            jsApiPay.openid = openid;
            jsApiPay.total_fee = int.Parse(total_fee);
            WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
            context.Response.Write(jsApiPay.GetJsApiParameters());//获取H5调起JS API参数  
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}