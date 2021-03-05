using Microsoft.AspNetCore.Http;
using Models.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Money.ExceptionHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate Next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.Next(context);
            }
            catch (Exception ex)
            {
                await ExceptionResponseAsync(context, ex);
            }
        }

        private static async Task ExceptionResponseAsync(HttpContext context, Exception ex)
        {
            Error response;
            switch (ex)
            {
                case InsufficientFundsException val:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new Error
                    {
                        ErrorMessage = "Недостаточно средств"
                    };
                    break;
                case NotActiveWalletException val:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new Error
                    {
                        ErrorMessage = "Кошелек заблокирован"
                    };
                    break;
                case ExistCurrencyException val:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new Error
                    {
                        ErrorMessage = "Валюта не найдена"
                    };
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = new Error
                    {
                        ErrorMessage = "Непредвиденная ошибка"
                    };
                    break;
            }
            var result = JsonConvert.SerializeObject(response, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }
    }
}
