﻿using ESB.Core;
using ESB.Core.Rpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ESB.CallCenter
{
    /// <summary>
    /// 总线请求响应调用接口
    /// </summary>
    public class ESB_InvokeService : IHttpHandler
    {
        ESBProxy esbProxy = ESBProxy.GetInstance();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;

            if (context.Request["ServiceName"] == null || context.Request["MethodName"] == null)
            {
                context.Response.ContentType = "text/html";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(@"<head runat=""server""><title>ESB调用中心</title></head>");
                context.Response.Write("<h1>错误：请传入正确的参数信息！</h1>");
                context.Response.Write(@"<h2>示例：<a target=""_blank"" href=""ESB_InvokeService.ashx?ServiceName=ESB_ASHX&MethodName=HelloWorld&Message=Demo"">ESB_InvokeService.ashx?ServiceName=ESB_ASHX&MethodName=HelloWorld&Message=Demo</a></h2>");
            }
            else
            {
                String serviceName = context.Request["ServiceName"].Trim();
                String methodName = context.Request["MethodName"].Trim();
                String isQueue = context.Request["IsQueue"];

                Int32 version = String.IsNullOrEmpty(context.Request["Version"]) ? 0 : Int32.Parse(context.Request["Version"]);
                String callback = context.Request["callback"];
                String message = GetMessageFromRequest(context.Request);
                String consumerIP = context.Request.UserHostAddress;
                AdvanceInvokeParam aiParam = new AdvanceInvokeParam();
                aiParam.ConsumerIP = consumerIP;

                Int32 queue = 0;
                if (String.IsNullOrEmpty(isQueue) || !Int32.TryParse(isQueue, out queue) || queue < 1)
                {
                    String response = esbProxy.Invoke(serviceName, methodName, message, version, aiParam);
                    if (!String.IsNullOrEmpty(callback))
                    {
                        response = String.Format("{0}({{message:'{1}'}})", callback, response, version);
                    }
                    context.Response.Write(response);
                }
                else
                {
                    try
                    {
                        esbProxy.InvokeQueue(serviceName, methodName, message, version, aiParam);
                        context.Response.Write("OK");
                    }
                    catch (Exception ex)
                    {
                        context.Response.Write(ex.Message);
                    }
                }

            }
        }

        /// <summary>
        /// 从原始请求链接中获取到Message
        /// </summary>
        /// <param name="rawUrl"></param>
        /// <returns></returns>
        private String GetMessageFromRequest(HttpRequest request)
        {
            //--如果取不到Message直接返回空
            if (request["Message"] == null)
                return String.Empty;

            //--如果能够从表单上获取到Message则优先使用
            if (request.Form["Message"] != null)
                return request.Form["Message"];

            //--如果表单上无法取到Message，则表示请求为GET请求，直接从URL上获取数据
            String rawUrl = request.Url.ToString();

            if (rawUrl.Contains("&Message="))
                return rawUrl.Substring(rawUrl.IndexOf("&Message=") + 9);

            throw new Exception("无效的调用方式！");
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