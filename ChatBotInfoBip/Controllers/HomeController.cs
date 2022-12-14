using ChatBotInfoBip.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace ChatBotInfoBip.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult getMessages()
        {
            try
            {
                _logger.LogInformation("Metodo para obtener los mensajes de una conversación");
                var client = new RestClient("https://ejmrvn.api.infobip.com/");
                client.Options.Timeout = -1;
                var request = new RestRequest("ccaas/1/conversations/63BA8541BE6C5304BF72EA41F5099ED5/messages", Method.Get);
                request.Parameters.AddParameter(Parameter.CreateParameter("orderBy", "createdAt:ASC", ParameterType.QueryString));
                request.Parameters.AddParameter(Parameter.CreateParameter("limit", 50, ParameterType.QueryString));
                request.AddHeader("Authorization", "App e528f01bf9a72356e3186d6458b2b9d6-e19736de-1b2a-45fe-810d-7690c9782338");
                request.AddHeader("Accept", "application/json");
                var response = client.Execute(request);

                return Json(response.Content);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Json(new { });
            }
        }

        [HttpPost]
        public JsonResult sendMessage()
        {
            string conversationId = Request.Form["idconver"];
            string message = Request.Form["message"];
            string phone = Request.Form["phone"];
            string type = Request.Form["type"];


            try
            {
                _logger.LogInformation("Metodo para enviar mensajes");
                if (type == "1")
                {
                    
                    ContentMessage bodyText = new ContentMessage();
                    bodyText.url = message;

                    SendMessageInfoBip dataSend = new SendMessageInfoBip();
                    dataSend.to = $"51{phone}";
                    dataSend.content = bodyText;

                    var client = new RestClient("https://ejmrvn.api.infobip.com/");
                    client.Options.Timeout = -1;
                    var request = new RestRequest($"ccaas/1/conversations/{conversationId}/messages", Method.Post);
                    request.AddHeader("Authorization", "App e528f01bf9a72356e3186d6458b2b9d6-e19736de-1b2a-45fe-810d-7690c9782338");
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("x-agent-id", "A630C656D476BA9F24EE076C806BEFB9");

                    request.AddBody(dataSend, "application/json");

                    var response = client.Execute(request);

                    return Json(response);
                }
                else
                {
                    ContentMessage bodyText = new ContentMessage();
                    bodyText.text = $"metodo 2: {message}";

                    SendMessageInfoBip2 dataSend = new SendMessageInfoBip2();
                    dataSend.to = $"51{phone}";
                    dataSend.content = bodyText;

                    var client = new RestClient("https://ejmrvn.api.infobip.com/");
                    client.Options.Timeout = -1;
                    var request = new RestRequest($"whatsapp/1/message/text", Method.Post);
                    request.AddHeader("Authorization", "App e528f01bf9a72356e3186d6458b2b9d6-e19736de-1b2a-45fe-810d-7690c9782338");
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/json");
                    request.AddBody(dataSend, "application/json");
                    var response = client.Execute(request);

                    return Json(response);
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return Json(new { error = e.Message });
            }

        }

        public JsonResult getImage()
        {


            return Json(new { });
        }




        public IActionResult Privacy()
        {
            return View();
        }

    }
}
