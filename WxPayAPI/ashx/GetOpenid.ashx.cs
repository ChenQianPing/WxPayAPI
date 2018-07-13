using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LitJson;

namespace WxPayAPI.ashx
{
    /// <summary>
    /// GetOpenid 的摘要说明
    /// </summary>
    public class GetOpenid : IHttpHandler
    {
        public string openid { get; set; }
        public void ProcessRequest(HttpContext context)
        {
            string code = HttpContext.Current.Request.QueryString["code"];

            WxPayData data = new WxPayData();
            data.SetValue("appid", "");
            data.SetValue("secret", "");
            data.SetValue("code", code);
            data.SetValue("grant_type", "authorization_code");
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?" + data.ToUrl();

            // 请求url以获取数据  
            string result = HttpService.Get(url);

            Log.Debug(this.GetType().ToString(), "GetOpenidAndAccessTokenFromCode response : " + result);

            // 保存access_token，用于收货地址获取  
            JsonData jd = JsonMapper.ToObject(result);
            // access_token = (string)jd["access_token"];  

            // 获取用户openid  
            openid = (string)jd["openid"];
            context.Response.Write(openid); // 获取H5调起JS API参数  
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